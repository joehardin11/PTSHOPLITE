
Imports System.Data.SqlClient
Imports System.Data.OleDb

Public Class Order

    Public JobNo As String
    Public Partno As String
    Public Partdesc As String
    Public Status As String
    Public OrderQty As Integer
    Public StockQty As Integer
    Public MakeQty As Integer
    Public duedate As Date
    Public JobNotes As String
    Public Material As Boolean
    Public TotalEstHours As Single
    Public TotalActHours As Single
    Public qtyshiped As Integer
    Public qtytostock As Integer
    Public Revision As String
    Public AltPartNo As String
    Public Customer As String
    Public Order As Integer

    Public Sub New(job As String, oledb As Boolean, poolconnections As Boolean, Optional connectionop As OleDbConnection = Nothing)

        'Find out of a copy of the operation exists in the internal database
        Dim insertconn As New OleDbConnection(My.Settings.E2Database)
        Dim insertcomm As New OleDbCommand("SELECT COUNT(*) FROM OrderScheduling WHERE JobNo = @jobno", insertconn)
        insertcomm.Parameters.AddWithValue("@jobno", job)
        If oledb = True Then


            Dim opquery As String = "SELECT OrderDet.JobNo, OrderDet.PartNo, OrderDet.PartDesc, OrderDet.Status, OrderDet.QtyOrdered, OrderDet.QtyToStock, OrderDet.QtyToMake, " &
            " OrderDet.DueDate, OrderDet.JobNotes, OrderDet.TotalEstHrs, OrderDet.TotalActualHrs, OrderDet.QtyShipped2Cust, OrderDet.QtyShipped2Stock, OrderScheduling.Material, OrderDet.Revision, Estim.AltPartNo, Orders.CustCode, Orders.OrderNo FROM (((OrderDet LEFT JOIN OrderScheduling ON OrderDet.JobNo = OrderScheduling.JobNo)" &
            " LEFT JOIN Estim ON OrderDet.PartNo = Estim.PartNo) LEFT JOIN Orders ON OrderDet.OrderNo = Orders.OrderNo) WHERE OrderDet.JobNo = @jobno"

            Dim jmquery As String = "SELECT SUM(JobMaterials.QtyPosted1) FROM JobMaterials WHERE JobNo=@jobno AND MainPart='Y'"




            Dim jmccomm As OleDbCommand
            Dim selectconn As New OleDbConnection(My.Settings.E2Database)

            Dim selectcomm As OleDbCommand
            Dim jmconn As New OleDbConnection(My.Settings.E2Database)

            If poolconnections = False Then
                selectcomm = New OleDbCommand(opquery, selectconn)
                jmccomm = New OleDbCommand(jmquery, jmconn)

            Else
                selectcomm = New OleDbCommand(opquery, connectionop)
                jmccomm = New OleDbCommand(jmquery, connectionop)
            End If

            selectcomm.Parameters.AddWithValue("@jobno", job)
            jmccomm.Parameters.AddWithValue("@jobno", job)
            Dim datatb As New DataTable
            Dim jmval As New Integer
            Try
                insertconn.Open()
                Dim dread As Integer
                dread = insertcomm.ExecuteScalar

                If dread = 0 Then
                    insertcomm.CommandText = "INSERT INTO OrderScheduling (JobNo) Values (@jobno)"
                    insertcomm.ExecuteNonQuery()
                End If
                insertconn.Close()


                Using dad As New OleDbDataAdapter(selectcomm)
                    dad.Fill(datatb)
                End Using


                jmconn.Open()
                jmval = NotNull(jmccomm.ExecuteScalar, 0)
                jmconn.Close()


            Catch ex As Exception
                insertconn.Close()
                selectconn.Close()
                jmconn.Close()

                'matconn.Close()
                MsgBox("Database Error: " & ex.Message)
                Exit Sub
            End Try
            JobNo = datatb.Rows(0).Item(0)          'JobNo
            Partno = datatb.Rows(0).Item(1)         'Partno
            Partdesc = NotNull(datatb.Rows(0).Item(2), "")     'PartDesc
            Status = datatb.Rows(0).Item(3)
            OrderQty = datatb.Rows(0).Item(4)       'Orderqty
            'StockQty = datatb.Rows(0).Item(5)
            StockQty = jmval
            MakeQty = datatb.Rows(0).Item(6) - StockQty   'makeQty
            duedate = datatb.Rows(0).Item(7)        'DueDate
            JobNotes = NotNull(datatb.Rows(0).Item(8), "")    'JobNotes
            TotalEstHours = datatb.Rows(0).Item(9)      'Esitmateds hours left
            TotalActHours = datatb.Rows(0).Item(10)   'Actual hours
            qtyshiped = datatb.Rows(0).Item(11)
            qtytostock = datatb.Rows(0).Item(12)
            Material = datatb.Rows(0).Item(13)
            Revision = datatb.Rows(0).Item(14)
            AltPartNo = datatb.Rows(0).Item(15)
            Customer = datatb.Rows(0).Item(16)
            Order = datatb.Rows(0).Item(17)

        Else

        End If

    End Sub

    Public Function Getsetup() As Setup


    End Function

    Private Function unitsandtimeHRS(timevalue As Single, unitvalue As String) As Single

        If unitvalue = "H" Then
            Return timevalue
        ElseIf unitvalue = "M" Then
            Return (timevalue / 60)
        Else
            Return timevalue / 3600
        End If


    End Function

    Public Sub updateMaterialstates()

        Dim setmatconn As New OleDbConnection(My.Settings.E2Database)
        Dim setmatcomm As New OleDbCommand("UPDATE Orderscheduling Set Material=@material WHERE JobNo = @jobno", setmatconn)

        setmatcomm.Parameters.AddWithValue("@material", Material)
        setmatcomm.Parameters.AddWithValue("@jobno", JobNo)

        Try

            setmatconn.Open()
            setmatcomm.ExecuteNonQuery()
            setmatconn.Close()

        Catch ex As Exception

            MsgBox(ex.Message)
            setmatconn.Close()

        End Try

    End Sub
End Class
