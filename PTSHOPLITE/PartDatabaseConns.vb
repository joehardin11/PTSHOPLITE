Imports System.Data.OleDb
Imports System.Data.SqlClient

Module PartDatabaseConns
    Public Function OLEFillDGrid(DataGridHold As DataGridView, SelectionString As String, selectionfrom As OleDbParameter, ErrorMessage As String) As DataTable
        Dim oleconn As New OleDbConnection(My.Settings.E2Database)
        Dim olecomm As New OleDbCommand(SelectionString, oleconn)

        'Add the parameter to the command
        olecomm.Parameters.Add(selectionfrom)


        Dim oledataset As New DataSet
        Dim oledatatable As New DataTable

        Try
            'Setup the dataadapter
            Dim olehdataadapter As New OleDbDataAdapter(olecomm)
            olehdataadapter.FillSchema(oledataset, SchemaType.Source, "Table1")
            olehdataadapter.Fill(oledataset, "Table1")
            oledatatable = oledataset.Tables("Table1")
            Return oledatatable
        Catch ex As Exception
            MsgBox("Error Pulling " & ErrorMessage & " Data from E2: " & ex.Message)
        End Try

        'If nothing is returned by now, it will return nothing
        Return Nothing

    End Function

    Public Function IsStringNull(dbobject As Object) As String
        If IsDBNull(dbobject) Then
            Return ""
        Else
            Return dbobject.ToString
        End If

    End Function
    Public Function NotNull(ByVal Value As Object, ByVal DefaultValue As Object) As Object
        If Value Is Nothing Or IsDBNull(Value) Then
            Return DefaultValue
        Else
            Return Value
        End If
    End Function
    Public Function AddOperation(PartID As Part, userholder As UserValues) As Integer
        'Add an operation for the part provided
        Dim addopform As New AddOperationForm(userholder, PartID)

        Dim operationvalue As Integer = -1

        If addopform.ShowDialog = DialogResult.OK Then
            'OperationID
            operationvalue = addopform.operationid

        End If

        Return operationvalue



    End Function
    Public Function PartExists(partstring As String) As Boolean

        'Load the current part information
        Dim partgenstring As String = "SELECT PartNo From Estim WHERE Partno = @partno "


        Try

            Dim partgenconn As New OleDbConnection(My.Settings.E2Database)
            Dim partgencom As New OleDbCommand(partgenstring, partgenconn)
            Dim partnoparameter As New OleDbParameter("@partno", OleDbType.WChar, 30)
            partnoparameter.Value = partstring
            partgencom.Parameters.Add(partnoparameter)

            Dim partgenadapter As New OleDbDataAdapter(partgencom)

            Dim partgendatatable As New DataTable
            partgenadapter.Fill(partgendatatable)

            If partgendatatable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox("part info error: " & ex.Message)
            Return False
        End Try

        Return False
    End Function
    Public Sub DeleteOperation(OperationID As Integer)
        Dim deletestring As String

        deletestring = "DELETE FROM Operation WHERE Id = @id"

        Dim deleteconn As New SqlConnection(My.Settings.SHOPDB)
        Dim deletecomm As New SqlCommand(deletestring, deleteconn)
        deletecomm.Parameters.AddWithValue("@id", OperationID)

        Try
            deleteconn.Open()
            deletecomm.ExecuteNonQuery()
            deleteconn.Close()

        Catch ex As Exception
            deleteconn.Close()
            MsgBox("DELETE DB Error: " & ex.Message)
        End Try

        Deletesetup(OperationID)

    End Sub
    Public Function Teste2connections() As Boolean
        Try
            Dim e2conn As New OleDbConnection(My.Settings.E2Database)

            e2conn.Open()
            If e2conn.State = ConnectionState.Open Then
                Return True
            Else

                MsgBox("No E2 connection found")
                Return False

            End If



        Catch ex As Exception
            MsgBox("No E2 connection found")
            Return False
        End Try



    End Function
    Public Function getgenpartinfo(partid As Part) As DataTable

        'Load the current part information
        Dim partgenstring As String = "SELECT Comments, LJNo, LJDateFin, ModBy, ModDate From Estim WHERE Partno = @partno "

        If partid Is Nothing Then Return Nothing


        Try

            Dim partgenconn As New OleDbConnection(My.Settings.E2Database)
            Dim partgencom As New OleDbCommand(partgenstring, partgenconn)
            Dim partnoparameter As New OleDbParameter("@partno", OleDbType.WChar, 30)
            partnoparameter.Value = partid.Partno
            partgencom.Parameters.Add(partnoparameter)

            Dim partgenadapter As New OleDbDataAdapter(partgencom)

            Dim partgendatatable As New DataTable
            partgenadapter.Fill(partgendatatable)

            Return partgendatatable

        Catch ex As Exception
            MsgBox("Gen part info error: " & ex.Message)
            Return Nothing
        End Try

        Return Nothing

    End Function

    Private Sub Deletesetup(OperationID As Integer)
        Dim deletestring As String

        deletestring = "DELETE FROM SETUP WHERE Operation = @id"

        Dim deleteconn As New SqlConnection(My.Settings.SHOPDB)
        Dim deletecomm As New SqlCommand(deletestring, deleteconn)
        deletecomm.Parameters.AddWithValue("@id", OperationID)

        Try
            'Execute the strings
            deleteconn.Open()
            deletecomm.ExecuteNonQuery()
            deleteconn.Close()

        Catch ex As Exception
            deleteconn.Close()
            MsgBox("DELETE DB Error: " & ex.Message)
        End Try

    End Sub
    Public Sub AddSetup(Opidvalue As Integer, partid As String, description As String)
        '#### Add a setup into the database
        Dim connstring As String = My.Settings.SHOPDB.ToString

        '**** Insert into the database 
        Dim insertstring As String
        insertstring = "INSERT INTO SETUP (PartID, Operation, OPDescription) VALUES (@partid, @operation, @description)"
        'Setup the command and connection
        Dim setupconn As New SqlConnection(connstring)
        Dim setupcomm As New SqlCommand(insertstring, setupconn)
        setupcomm.Parameters.AddWithValue("@partid", partid)
        setupcomm.Parameters.AddWithValue("@operation", Opidvalue)
        setupcomm.Parameters.AddWithValue("@description", description)

        Try
            setupconn.Open()
            setupcomm.ExecuteNonQuery()
            setupconn.Close()
        Catch ex As Exception
            setupconn.Close()
            MsgBox("Error " & ex.Message)
        End Try

    End Sub



    Public Sub AddInspection(Opid As Integer, partid As Part, description As String, userholder As UserValues)
        Dim connstring As String = My.Settings.SHOPDB.ToString

        Dim insertstring As String
        insertstring = "INSERT INTO Inspection (PartID, Operation, Description, EnteredBy, EnteredDate, Revision) VALUES (@partid, @operation, @description, @enteredby, @enterdate, @rev)"

        Dim setupconn As New SqlConnection(connstring)
        Dim setupcomm As New SqlCommand(insertstring, setupconn)
        setupcomm.Parameters.AddWithValue("@partid", partid.Partno)
        setupcomm.Parameters.AddWithValue("@operation", Opid)
        setupcomm.Parameters.AddWithValue("@description", description)
        setupcomm.Parameters.AddWithValue("@enteredby", userholder.Username)
        setupcomm.Parameters.AddWithValue("@enterdate", Today())
        setupcomm.Parameters.AddWithValue("@rev", partid.Revision)

        Try
            setupconn.Open()
            setupcomm.ExecuteNonQuery()
            setupconn.Close()
        Catch ex As Exception
            setupconn.Close()
            MsgBox("Error " & ex.Message)
        End Try

    End Sub
    Public Function GetPartNumbersNoSlashes() As DataTable
        Dim pt_datatable As DataTable

        pt_datatable = Nothing

        If My.Settings.SQLPT = False Then
            pt_datatable = olepartnumbers(True)
        Else
            pt_datatable = sqlpartnumbers(True)
        End If

        Return pt_datatable

    End Function
    Public Function GetPartNumbers() As DataTable
        Dim pt_datatable As DataTable

        pt_datatable = Nothing

        If My.Settings.SQLPT = False Then
            pt_datatable = olepartnumbers()
        Else
            pt_datatable = sqlpartnumbers()
        End If

        Return pt_datatable

    End Function
    Public Function GetSinglePartNumber(partnumber As Integer) As DataTable
        Dim pt_datatable As DataTable

        pt_datatable = Nothing

        If My.Settings.SQLPT = False Then
            pt_datatable = olepartnumbers()
        Else
            pt_datatable = sqlpartnumbers()
        End If

        Return pt_datatable

    End Function

    Private Function sqlpartnumbers(Optional noslashes As Boolean = False) As DataTable
        'Set up connection to the part database
        Dim dbstring As String = My.Settings.E2Database

        Dim dbconn As New SqlConnection(dbstring)
        Dim dtbl As New DataTable


        Try
            dbconn.Open()
            Dim dbfill As New SqlDataAdapter(My.Settings.GetPartQuery, dbstring)
            If noslashes = True Then
                dbfill.SelectCommand.CommandText = My.Settings.GetPartsQueryNoslash
            End If
            Using dbfill
                dbfill.Fill(dtbl)
            End Using

            dbconn.Close()

        Catch ex As Exception
            dbconn.Close()
            MsgBox("Error:" & ex.Message)
            Return Nothing
        End Try


        Return dtbl


    End Function

    Private Function olepartnumbers(Optional Noslashes As Boolean = False) As DataTable
        'Set up connection to the part database
        Try
            Dim dbstring As String = My.Settings.E2Database

            Dim dbconn As New OleDbConnection(dbstring)
            Dim dtbl As New DataTable

            dbconn.Open()
            Dim dbfill As New OleDbDataAdapter(My.Settings.GetPartQuery, dbstring)

            If Noslashes = True Then
                dbfill.SelectCommand.CommandText = My.Settings.GetPartsQueryNoslash
            End If

            dbfill.Fill(dtbl)
            dbconn.Close()

            Return dtbl
        Catch ex As Exception
            MsgBox("Data Table Error: " & ex.Message)
        End Try

        Return Nothing

    End Function

    Public Function ScalarSelect(selectorcommand As SqlCommand)
        Dim outputholder As Object = Nothing

        Dim scalarconn As New SqlConnection(My.Settings.SHOPDB.ToString)
        selectorcommand.Connection = scalarconn

        Try
            scalarconn.Open()
            outputholder = selectorcommand.ExecuteScalar
            scalarconn.Close()

            Return outputholder
        Catch ex As Exception
            scalarconn.Close()
            MsgBox("Database Scalar Query Error: " & ex.Message)
            Return Nothing
        End Try


    End Function
    Public Sub FillComboFromDBsqlCommand(comboholder As ComboBox, fillcomm As SqlCommand, colname As String)


        'if the boolean says to use the sqldatase
        Dim fillcon As New SqlConnection(My.Settings.SHOPDB.ToString)
        fillcomm.Connection = fillcon

        Try
            fillcon.Open()
            Dim dtab As New DataTable
            Using dfill As New SqlDataAdapter(fillcomm)
                dfill.Fill(dtab)
            End Using

            comboholder.DataSource = dtab
            comboholder.DisplayMember = colname
            fillcon.Close()

        Catch ex As Exception
            MsgBox("Combobox filling error: " & ex.Message)
        End Try

    End Sub
    Public Function quoteimport(filelocation As String) As Boolean
        'Open file

        'Check for file formatting


        '














    End Function
    Public Sub FillCombowithDT(comboholder As ComboBox, DTHolder As DataTable, colname As String, Optional valuemember As String = "", Optional oleboolean As Boolean = False, Optional Defaultval As String = "")


        Dim dtable As New DataTable
        dtable = DTHolder
        comboholder.DataSource = dtable
        comboholder.DisplayMember = colname

        If valuemember <> "" Then
            comboholder.ValueMember = valuemember
        End If

    End Sub

    Public Sub FillistviewfromDB(listviewholder As ListView, fillstring As String, oleboolean As Boolean)
        If oleboolean = True Then
            Dim fillcon As New OleDbConnection(My.Settings.E2Database)
            Dim fillcomm As New OleDbCommand(fillstring, fillcon)

            Try
                Dim dtable As New DataTable
                Dim dfiladapter As New OleDbDataAdapter(fillcomm)
                dfiladapter.Fill(dtable)
                Dim itemcount As Integer = dtable.Columns.Count
                For Each fillrow As DataRow In dtable.Rows

                    Dim lvitemadd As New ListViewItem(fillrow.Item(0).ToString)

                    'Add each column of the datatable
                    For itemval = 1 To itemcount
                        lvitemadd.SubItems.Add(fillrow.Item(itemval))
                    Next

                    listviewholder.Items.Add(lvitemadd)

                Next

            Catch ex As Exception

            End Try


        Else
            'if the boolean says to use the sqldatase
            Dim fillcon As New SqlConnection(My.Settings.SHOPDB.ToString)
            Dim fillcom As New SqlCommand(fillstring, fillcon)

            Try
                Dim dtable As New DataTable
                Dim dfiladapter As New SqlDataAdapter(fillcom)
                dfiladapter.Fill(dtable)
                Dim itemcount As Integer = dtable.Columns.Count
                For Each fillrow As DataRow In dtable.Rows

                    Dim lvitemadd As New ListViewItem(fillrow.Item(0).ToString)

                    'Add each column of the datatable
                    For itemval = 1 To itemcount
                        lvitemadd.SubItems.Add(fillrow.Item(itemval))
                    Next

                    listviewholder.Items.Add(lvitemadd)

                Next


            Catch ex As Exception
                MsgBox("Combobox filling error: " & ex.Message)
            End Try

        End If




    End Sub
    Public Function GetDTFromString(fillstring As String, oleboolean As Boolean) As DataTable

        If oleboolean = True Then
            Dim fillcon As New OleDbConnection(My.Settings.E2Database)
            Dim fillcomm As New OleDbCommand(fillstring, fillcon)

            Try
                Dim dtable As New DataTable
                Dim dfiladapter As New OleDbDataAdapter(fillcomm)
                dfiladapter.Fill(dtable)

                Return dtable
            Catch ex As Exception
                MsgBox(ex.Message)
                Return Nothing
            End Try

        Else
            'if the boolean says to use the sqldatase
            Dim fillcon As New SqlConnection(My.Settings.SHOPDB.ToString)
            Dim fillcom As New SqlCommand(fillstring, fillcon)

            Try
                fillcon.Open()
                Dim dtab As New DataTable
                Using dfill As New SqlDataAdapter(fillcom)
                    dfill.Fill(dtab)
                End Using

                Return dtab
                fillcon.Close()

            Catch ex As Exception
                MsgBox("Combobox filling error: " & ex.Message)
                Return Nothing
            End Try

        End If
    End Function

    Public Sub FillComboFromDB(comboholder As ComboBox, fillstring As String, colname As String, oleboolean As Boolean, Optional valuemember As String = "", Optional Defaultval As String = "")

        If oleboolean = True Then
            Dim fillcon As New OleDbConnection(My.Settings.E2Database)
            Dim fillcomm As New OleDbCommand(fillstring, fillcon)

            Try
                Dim dtable As New DataTable
                Dim dfiladapter As New OleDbDataAdapter(fillcomm)
                dfiladapter.Fill(dtable)

                If Defaultval <> "" Then
                    Dim drow As DataRow = dtable.NewRow
                    drow(colname) = Defaultval
                    dtable.Rows.Add(drow)
                End If

                If valuemember <> "" Then
                    comboholder.ValueMember = valuemember
                End If
                comboholder.DataSource = dtable
                comboholder.DisplayMember = colname

            Catch ex As Exception

            End Try


        Else
            'if the boolean says to use the sqldatase
            Dim fillcon As New SqlConnection(My.Settings.SHOPDB.ToString)
            Dim fillcom As New SqlCommand(fillstring, fillcon)

            Try
                fillcon.Open()
                Dim dtab As New DataTable
                Using dfill As New SqlDataAdapter(fillcom)
                    dfill.Fill(dtab)
                End Using

                If Defaultval <> "" Then
                    Dim drow As DataRow = dtab.NewRow
                    drow(colname) = Defaultval
                    dtab.Rows.Add(drow)
                End If
                If valuemember <> "" Then
                    comboholder.ValueMember = valuemember
                End If
                comboholder.DataSource = dtab
                comboholder.DisplayMember = colname
                fillcon.Close()

            Catch ex As Exception
                MsgBox("Combobox filling error: " & ex.Message)
            End Try

        End If
    End Sub

End Module
