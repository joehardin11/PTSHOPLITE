Imports System.Text
Imports System.IO
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Data.OleDb
Imports System.Runtime.CompilerServices

Module GeneralUseModule
    'EXtensions

    <Extension()>
    Public Sub Add(Of T)(ByRef arr As T(), item As T)
        If arr IsNot Nothing Then
            Array.Resize(arr, arr.Length + 1)
            arr(arr.Length - 1) = item
        Else
            ReDim arr(0)
            arr(0) = item
        End If

    End Sub
    Public Function IsFileOpen(ByVal file As FileInfo) As Boolean
        Dim stream As FileStream = Nothing
        Try
            stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None)
            stream.Close()
        Catch ex As Exception

            If TypeOf ex Is IOException AndAlso IsFileLocked(ex) Then
                Return True
            End If
        End Try
        Return False

    End Function
    Private Function IsFileLocked(exception As Exception) As Boolean
        Dim errorCode As Integer = Marshal.GetHRForException(exception) And ((1 << 16) - 1)
        Return errorCode = 32 OrElse errorCode = 33
    End Function
    ''' <summary>
    ''' Objecttype = Estimate Or Job
    ''' Value = PartNo Or JobNo
    ''' Actiontype = Edit, Routing, BOM 
    ''' Commentvalue = what you did
    ''' </summary>
    ''' <param name="UserId"></param>
    ''' <param name="Objecttype"></param>
    ''' <param name="Value"></param>
    ''' <param name="Actiontype"></param>
    ''' <param name="Commentvalue"></param>
    Public Sub AddUserTransaction(UserId As String, Objecttype As String, Value As String, Actiontype As String, Commentvalue As String, Optional Modpart As Boolean = False)
        Dim insertconn As New OleDbConnection(My.Settings.E2Database)
        Dim insertcomm As New OleDbCommand("INSERT INTO UserTransactions ([UserID], [Object], [Value], [Action], [TransDate], [Comments]) VALUES (@useridval, @objectvalue, @valueholder1, @actionvalue, @transdate, @commentsvalue)", insertconn)
        insertcomm.Parameters.AddWithValue("@useridval", UserId)
        insertcomm.Parameters.AddWithValue("@objectvalue", Objecttype)
        insertcomm.Parameters.AddWithValue("@valueholder1", Value)
        insertcomm.Parameters.AddWithValue("@actionvalue", Actiontype)
        Dim pval As New OleDbParameter("@transdate", OleDbType.Date)
        pval.Value = Now
        insertcomm.Parameters.Add(pval)
        insertcomm.Parameters.AddWithValue("@commentsvalue", Commentvalue)

        Dim updateconn As New OleDbConnection(My.Settings.E2Database)
        Dim updatecomm As New OleDbCommand("Update Estim Set ModBy = @modby, ModDate = @moddate WHERE PartNo = @partno", updateconn)

        updatecomm.Parameters.AddWithValue("@modby", UserId.ToString)
        Dim upval As New OleDbParameter("@moddate", OleDbType.Date)
        upval.Value = Now
        updatecomm.Parameters.Add(upval)
        updatecomm.Parameters.AddWithValue("@partno", Value)



        Try
            insertconn.Open()
            insertcomm.ExecuteNonQuery()
            insertconn.Close()

            If Modpart = True Then
                updateconn.Open()
                updatecomm.ExecuteNonQuery()
                updateconn.Close()
            End If

        Catch ex As Exception

            updateconn.Close()
            insertconn.Close()
            MsgBox(ex.Message)

        End Try
    End Sub

    Public Sub SaveCSVFile(dtable As DataTable, FileName As String, Locationsave As String)
        Dim filelocationholder As String
        filelocationholder = Locationsave & "\" & FileName & ".csv"
        ExporttoExcel(dtable, filelocationholder, False)
    End Sub
    Public Function SplitParagraph(text As String, Optional maxLength As Integer = 325) As List(Of String)
        Dim substrings As New List(Of String)

        Do Until text.Length = 0
            If text.Length <= maxLength Then
                'There is only one substring left.
                substrings.Add(text)
                text = String.Empty
            Else
                Dim length = maxLength

                'Find the index at or before the maxLength that there is not a letter on both sides of the split.
                Do While Char.IsLetterOrDigit(text(length)) AndAlso Char.IsLetterOrDigit(text(length - 1))
                    length -= 1
                Loop

                substrings.Add(text.Substring(0, length))
                text = text.Substring(length)
            End If
        Loop

        Return substrings
    End Function
    Public Function Truncate(value As String, length As Integer) As String
        ' If argument is too big, return the original string.
        ' ... Otherwise take a substring from the string's start index.
        If length > value.Length Then
            Return value
        Else
            Return value.Substring(0, length)
        End If
    End Function
    Public Function CountCharacter(ByVal value As String, ByVal ch As Char) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In value
            If c = ch Then
                cnt += 1
            End If
        Next
        Return cnt
    End Function

    Public Function imgToByteArray(ByVal image As Image) As Byte()
        Using mStream As New MemoryStream()
            Dim img As Image = Nothing

            img = image
            img.Save(mStream, img.RawFormat)
            Return mStream.ToArray()
        End Using
    End Function

    Public Sub QuickviewDocControl(partholder As Part, userval As UserValues)
        'open a new form
        Dim quickviewform As New SubcontrolForm(1, userval, partholder)

        quickviewform.Show()


    End Sub
    Public Sub QuickviewSetupControl(opholder As Operation, userval As UserValues)
        'open a new form

        Dim quickviewform As New SubcontrolForm(2, userval, opholder)
        quickviewform.Show()


    End Sub

    Public Function Comboboxform(combosource As DataTable, Formtitle As String, valuecolumn As String, viewcolumn As String) As String

        Dim comboholder As New SelectComboForm(combosource, Formtitle, valuecolumn, viewcolumn)

        Dim returnstring As String
        returnstring = ""
        If comboholder.ShowDialog = DialogResult.OK Then
            returnstring = comboholder.ReturnedValue
        End If

        Return returnstring

    End Function
    Public Function ImportExceltoDatatable(filepath As String) As DataTable
        ' string sqlquery= "Select * From [SheetName$] Where YourCondition";
        Dim dt As New System.Data.DataTable

        Try
            Dim ds As New DataSet()
            Dim constring As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & filepath & ";Extended Properties=""Excel 12.0;HDR=YES;"""
            Dim con As New System.Data.OleDb.OleDbConnection(constring & "")

            con.Open()
            Dim myTableName = con.GetSchema("Tables").Rows(0)("TABLE_NAME")
            Dim sqlquery As String = String.Format("SELECT * FROM [{0}]", myTableName) ' "Select * From " & myTableName  
            Dim da As New System.Data.OleDb.OleDbDataAdapter(sqlquery, con)
            da.Fill(ds)
            dt = ds.Tables(0)
            Return dt
        Catch ex As Exception
            MsgBox(Err.Description, MsgBoxStyle.Critical)
            Return dt
        End Try

    End Function
    Public Function Capturepicturebyte() As Byte()
        'Take a picture if a camera is available
        'Dim captureformh As New CaptureForm

        'If captureformh.ShowDialog() = DialogResult.OK Then
        '    'Save picture and add to database under the current setup
        '    Dim imagetaken As Image = captureformh.PictureBox1.Image
        '    'Close Form
        '    captureformh.Close()

        '    Dim mstream As New System.IO.MemoryStream

        '    imagetaken.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)
        '    Dim imagebyte(0) As Byte
        '    imagebyte = mstream.ToArray
        '    Return imagebyte
        'Else
        '    'Do nothing with picture
        '    'Close Form
        '    CaptureForm.Close()

        '    Return Nothing

        '   End If
    End Function

    Public Function IsprocessOpen(processname As String)


        For Each processholder As Process In Process.GetProcesses
            If processholder.ProcessName.Contains(processname) Then

                Return True
            End If

        Next

        Return False



    End Function
    Public Function Datafilterbuilder(filterstring As String, columnstrings As List(Of String))

        Dim filtersearch As String = ""
        filterstring = filterstring.Replace("*", "%")
        Dim firstword As Boolean = True
        Dim firstcol As Boolean = True

        If filterstring.Contains("%") And Len(filterstring) > 1 Then
            Dim searchwords As String() = filterstring.Split(New Char() {"%"c})

            For Each searchword As String In searchwords
                firstcol = True

                If firstword Then
                    For Each searchcolumn As String In columnstrings
                        If firstcol Then
                            filtersearch = "((" & searchcolumn & " LIKE '%" & searchword & "%')"
                        Else
                            filtersearch = filtersearch & " OR (" & searchcolumn & " LIKE '%" & searchword & "%')"
                        End If
                        firstcol = False
                    Next
                    filtersearch = filtersearch & ")"
                Else
                    For Each searchcolumn As String In columnstrings
                        If firstcol Then
                            filtersearch = filtersearch & " AND ((" & searchcolumn & " LIKE '%" & searchword & "%')"
                        Else
                            filtersearch = filtersearch & " OR (" & searchcolumn & " LIKE '%" & searchword & "%')"
                        End If

                        firstcol = False
                    Next
                    filtersearch = filtersearch & ")"

                End If
                firstword = False

            Next

        Else

            For Each searchcolumn As String In columnstrings
                If firstcol = True Then
                    filtersearch = "(" & searchcolumn & " LIKE '%" & filterstring & "%')"
                    firstcol = False
                Else
                    filtersearch = filtersearch & " OR (" & searchcolumn & " LIKE '%" & filterstring & "%')"
                End If
                firstcol = False
            Next




        End If

        Return filtersearch


    End Function

    Public Function ExporttoExcel(dtable As System.Data.DataTable, Saveasname As String, openfile As Boolean) As Boolean
        On Error GoTo 1
        'GET THE file save location
        '--------Columns Name--------------------------------------------------------------------------- 

        Dim sb As StringBuilder = New StringBuilder()
        Dim intClmn As Integer = dtable.Columns.Count

        Dim i As Integer = 0
        For i = 0 To intClmn - 1 Step i + 1
            sb.Append("""" + dtable.Columns(i).ColumnName.ToString() + """")
            If i = intClmn - 1 Then
                sb.Append(" ")
            Else
                sb.Append(",")
            End If
        Next
        sb.Append(vbNewLine)

        '--------Data By  Columns--------------------------------------------------------------------------- 

        Dim row As DataRow
        For Each row In dtable.Rows

            Dim ir As Integer = 0
            For ir = 0 To intClmn - 1 Step ir + 1
                sb.Append("""" + row(ir).ToString().Replace("""", """""") + """")
                If ir = intClmn - 1 Then
                    sb.Append(" ")
                Else
                    sb.Append(",")
                End If

            Next
            sb.Append(vbNewLine)
        Next

        System.IO.File.WriteAllText(Saveasname, sb.ToString())
        If openfile = True Then
            Process.Start(Saveasname)
        End If
        Return True

1:
        MsgBox("Error Saving File")
        Return False


    End Function

    Public Function ReadCSVFile(strFilePath As String, Filename As String) As DataTable
        Dim returnValue As New DataTable


        Dim folder = strFilePath & "\"
        Dim CnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""text;HDR=Yes;FMT=Delimited"";"
        Dim dt As New DataTable
        Using Adp As New OleDbDataAdapter("select * from [" & Filename & "]", CnStr)
            Adp.Fill(dt)
        End Using

        'Dim lines = IO.File.ReadAllLines(strFilePath)
        'Dim tbl = New DataTable
        'Dim colCount = lines.First.Split(","c).Length
        'For i As Int32 = 1 To colCount
        '    tbl.Columns.Add(New DataColumn("Column_" & i, GetType(Int32)))
        'Next
        'For Each line In lines
        '    Dim objFields = From field In line.Split(","c)
        '                    Select CType(Int32.Parse(field), Object)
        '    Dim newRow = tbl.Rows.Add()
        '    newRow.ItemArray = objFields.ToArray()
        'Next

        returnValue = dt

        Return returnValue
    End Function
    Private Sub ReleaseObject(ByVal o As Object)
        Try
            While (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0)
            End While
        Catch
        Finally
            o = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' See if the user has permission... Returns boolean false if rights are not met...
    ''' 1 = Engineering, 2= Setup, 3=QC, 4=Inventory, 5=Admin, 6 =Doc Conontrol
    ''' </summary>
    ''' <param name="User"></param>
    ''' <param name="SingleRight"></param>
    ''' <param name="RightsRequired"></param>
    ''' <returns></returns>
    Public Function CheckPermissions(User As UserValues, SingleRight As Integer, Optional RightsRequired As List(Of Integer) = Nothing, Optional MessageUser As Boolean = True) As Boolean
        'Look to see if the user has rights... LOL

        'Make a list of all the User's Rights
        Dim UserRights As New List(Of Integer)
        If User.EngRights = True Then UserRights.Add(1)
        If User.SetupRights = True Then UserRights.Add(2)
        If User.QCRights = True Then UserRights.Add(3)
        If User.InventoryRights = True Then UserRights.Add(4)
        If User.AdminRights = True Then UserRights.Add(5)
        If User.DocControl = True Then UserRights.Add(6)

        'If no rights list is given, check user rights against single right
        If IsNothing(RightsRequired) = True Then
            If UserRights.Contains(SingleRight) = True Then
                Return True
            Else
                If SingleRight = 1 Then
                    If MessageUser Then MsgBox("You need Engineering Rights to use this function")
                    Return False
                ElseIf SingleRight = 2 Then
                    If MessageUser Then MsgBox("You need Setup Rights to use this function")
                    Return False
                ElseIf SingleRight = 3 Then
                    If MessageUser Then MsgBox("You need Quality Control Rights to use this function")
                    Return False
                ElseIf SingleRight = 4 Then
                    If MessageUser Then MsgBox("You need Inventory Rights to use this function")
                    Return False
                ElseIf SingleRight = 5 Then
                    If MessageUser Then MsgBox("You need Administrator Rights to use this function")
                    Return False
                ElseIf SingleRight = 6 Then
                    If MessageUser Then MsgBox("You need Doc Control Rights to use this function")
                    Return False
                Else
                    If MessageUser Then MsgBox("You do not have rights to use this function")
                    Return False
                End If
            End If
        End If


        'List is given, compare user rights against required rights
        Dim MatchingRights = RightsRequired.Intersect(UserRights).ToList

        If MatchingRights.Count > 0 Then
            Return True
        Else
            If MessageUser Then MsgBox("You do not have rights to use this function")
            Return False
        End If

        'Return True

    End Function
    Public Function StartDocControlProcessPlan_Email(PartHolder As Part)

        Dim oApp As Object
        Dim oMsg As Object
        oApp = CreateObject("Outlook.Application")
        oMsg = oApp.CreateItem(0)
        oMsg.To = My.Settings.doccontrolprocessplanemailaccount
        oMsg.Subject = PartHolder.Partno & "  Rev: " & PartHolder.Revision
        oMsg.body = "Attach Documents for Review" & vbCrLf & "Notes: "
        oMsg.display()
        Return Nothing

    End Function

End Module
