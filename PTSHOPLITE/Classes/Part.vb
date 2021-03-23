Imports System.Data.SqlClient
Imports System.Data.OleDb


Public Class Part

    Public Operations() As Integer
    Public Inspections() As Integer
    Public Setups() As Integer
    Public Revision As String
    Public Description As String
    Public AltPartno As String
    Public Customer As String
    Public Partno As String = ""
    Public StockUnits As String
    Public Drawing As String
    Public blankdim1 As Single
    Public blankdim2 As Single
    Public blankdim3 As Single
    Public blanktol1 As String
    Public blanktol2 As String
    Public blanktol3 As String
    Public blankmemo As String
    Public blankunit As String
    Public extrablanks As Single
    Public Comments As String
    Public parteng As PartEngineering
    Public LastJob As String
    Public LastJobDate As Date
    Public modby As String
    Public moddate As Date



    Public Sub New(ByVal olepart As Boolean, Optional PartNumber As String = "None", Optional partid As Integer = 0)
        'Get the part number 
        Partno = PartNumber
        If My.Settings.SQLPT = True Then
            sqlstyle(partid)
        Else
            OLEstyle(PartNumber)
        End If

    End Sub

    Private Sub OLEstyle(ByVal partnumber As String)
        Dim parttable As New DataTable("Parts")
        parttable.Columns.Add("Descrip")
        parttable.Columns.Add("AltPartNo")
        parttable.Columns.Add("Revision")

        Try
            Dim partconnection As New OleDbConnection(My.Settings.E2Database.ToString)
            Dim partcommand As New OleDbCommand("SELECT Descrip, AltPartNo, Revision, StockUnit, DrawNum, User_Number2, User_Number3, User_Number4, User_Text2," &
            " User_Text3, User_Text4, User_Text1, ROUND(User_Number1,3), user_memo1, Comments, CustCode,  LJNo, LJDateFin, ModBy, ModDate FROM Estim WHERE PartNo = @partno", partconnection)
            partcommand.Parameters.AddWithValue("@partno", partnumber)
            Partno = partnumber
            'Part adapter
            partconnection.Open()

            Dim partadapter As New OleDbDataAdapter
            partadapter.SelectCommand = partcommand

            partadapter.Fill(parttable)

            partconnection.Close()



        Catch ex As Exception
            MsgBox("Part Database Error: " & ex.Message & partnumber)
        End Try

        If parttable.Rows.Count > 0 Then
            With parttable.Rows(0)
                Description = .Item(0).ToString
                AltPartno = .Item(1).ToString
                Revision = .Item(2).ToString
                StockUnits = .Item(3).ToString
                Drawing = .Item(4).ToString
                blankdim1 = NotNull(.Item(5), 0)
                blankdim2 = NotNull(.Item(6), 0)
                blankdim3 = NotNull(.Item(7), 0)
                blanktol1 = NotNull(.Item(8), "")
                blanktol2 = NotNull(.Item(9), "")
                blanktol3 = NotNull(.Item(10), "")
                blankunit = NotNull(.Item(11), "")
                extrablanks = NotNull(.Item(12), 0)
                blankmemo = NotNull(.Item(13), "")
                Comments = NotNull(.Item(14), "")
                Customer = NotNull(.Item(15), "")
                LastJob = NotNull(.Item(16), "")
                LastJobDate = NotNull(.Item(17), Date.Parse("1/1/1900"))
                modby = NotNull(.Item(18), "")
                moddate = NotNull(.Item(19), Date.Parse("1/1/1900"))
            End With

        End If
        parteng = New PartEngineering(partnumber)


    End Sub
    'Public Sub Addnewflag(Flagtype As Integer, userid As Integer, Description As String)

    '    'Determine 
    '    ' AddNew to PartEng

    '    Dim partengcon As New SqlConnection(My.Settings.SHOPDB)
    '    Dim partengcomm As New SqlCommand("INSERT Into PartFlags (FlagTypeid, Description, Userholder, DateEntered, Active, Partnumber) Values (@flagtype, @description, @userholder, @dateentered, @active, @partnumber)", partengcon)
    '    partengcomm.Parameters.AddWithValue("@flagtype", Flagtype)
    '    partengcomm.Parameters.AddWithValue("@description", Description)
    '    partengcomm.Parameters.AddWithValue("@userholder", userid)
    '    partengcomm.Parameters.AddWithValue("@dateentered", Now)
    '    partengcomm.Parameters.AddWithValue("@active", True)
    '    partengcomm.Parameters.AddWithValue("@partnumber", Partno)

    '    Try
    '        partengcon.Open()
    '        partengcomm.ExecuteNonQuery()
    '        partengcon.Close()

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub
    Public Sub saveblankinfo()
        Dim blankconn As New OleDbConnection(My.Settings.E2Database)
        Dim blankcom As New OleDbCommand("UPDATE Estim Set User_Number1 = @num1, User_Number2= @num2, User_Number3 = @num3, User_Number4= @num4, " &
                                         "User_Text1= @text1, User_Text2 = @text2, User_Text3= @text3, User_Text4= @text4, user_memo1= @memo1 WHERE PartNo = @partno", blankconn)

        blankcom.Parameters.AddWithValue("@num1", extrablanks)
        blankcom.Parameters.AddWithValue("@num2", blankdim1)
        blankcom.Parameters.AddWithValue("@num3", blankdim2)
        blankcom.Parameters.AddWithValue("@num4", blankdim3)
        blankcom.Parameters.AddWithValue("@text1", blankunit)
        blankcom.Parameters.AddWithValue("@text2", blanktol1)
        blankcom.Parameters.AddWithValue("@text3", blanktol2)
        blankcom.Parameters.AddWithValue("@text4", blanktol3)
        blankcom.Parameters.AddWithValue("@memo1", blankmemo)
        blankcom.Parameters.AddWithValue("@partno", Partno)

        Try
            blankconn.Open()
            blankcom.ExecuteNonQuery()
            blankconn.Close()

        Catch ex As Exception
            blankconn.Close()
            MsgBox("Error Saving Blank Info: " & ex.Message)
        End Try


    End Sub

    Private Sub sqlstyle(Partnumber As String)
        Dim partconnection As New SqlConnection(My.Settings.E2Database)
        Dim partcommand As New SqlCommand("SELECT Descrip, AltPartNo, Revision From Estim WHERE PartNo = @part", partconnection)
        partcommand.Parameters.AddWithValue("@part", Partnumber)

        Dim parttable As New DataTable
        Try
            'Part adapter
            Dim partadapter As New SqlDataAdapter(partcommand)
            partadapter.Fill(parttable)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        With parttable.Rows(0)
            Description = .Item(0).ToString
            AltPartno = .Item(1).ToString
            Revision = .Item(2).ToString
        End With

    End Sub


End Class
