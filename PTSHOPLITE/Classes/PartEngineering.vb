Imports System.Data.SqlClient


Public Class PartEngineering
    Public ApprovedRev As String
    Public NewPart As Boolean
    Public ModelNotRequired As Boolean
    Public Travellerprint As Integer
    Public MaterialApproved As Boolean
    Public MaterialUser As Integer
    Public materialdate As DateTime
    Public ProcessApproved As Boolean
    Public processuser As Integer
    Public processdate As DateTime
    Public qualityapproved As Boolean
    Public qualityuser As Integer
    Public qualitydate As DateTime
    Public Partnumber As String
    Public PartFlags As DataTable
    Public ActiveFlags As DataTable
    Public InspectionLevel As Integer
    Public DocCtrlapproved As Boolean
    Public DocCtrluser As Integer
    Public DocCtrldate As DateTime
    Public PeerReview As Boolean



    Public Sub New(partholder As String)
        'Get all the info about a part

        Partnumber = partholder
        Dim partengdb As New SqlDatabase(My.Settings.PartDatabaseString)
        Dim partengdt As New DataTable


        Try

            partengdt = partengdb.getdatatable("SELECT * From PartEng Where PartNumber = @parameter", Partnumber)

            If partengdt.Rows.Count = 0 Then

                ApprovedRev = "None"
                AddPartEng()
                partengdt = partengdb.getdatatable("SELECT * From PartEng Where PartNumber = @parameter", Partnumber)
            End If

            ApprovedRev = NotNull(partengdt.Rows(0).Item(1), "None")
            NewPart = NotNull(partengdt.Rows(0).Item(2), False)
            ModelNotRequired = NotNull(partengdt.Rows(0).Item(3), False)
            Travellerprint = NotNull(partengdt.Rows(0).Item(4), -1)
            MaterialApproved = NotNull(partengdt.Rows(0).Item(5), False)
            MaterialUser = NotNull(partengdt.Rows(0).Item(6), -1)
            materialdate = NotNull(partengdt.Rows(0).Item(7), DateTime.Parse("1/1/1900"))
            ProcessApproved = NotNull(partengdt.Rows(0).Item(8), False)
            processuser = NotNull(partengdt.Rows(0).Item(9), -1)
            processdate = NotNull(partengdt.Rows(0).Item(10), DateTime.Parse("1/1/1900"))
            qualityapproved = NotNull(partengdt.Rows(0).Item(11), False)
            qualityuser = NotNull(partengdt.Rows(0).Item(12), -1)
            qualitydate = NotNull(partengdt.Rows(0).Item(13), DateTime.Parse("1/1/1900"))
            InspectionLevel = NotNull(partengdt.Rows(0).Item(15), 0)
            DocCtrlapproved = NotNull(partengdt.Rows(0).Item(16), False)
            DocCtrldate = NotNull(partengdt.Rows(0).Item(17), DateTime.Parse("1/1/1900"))
            DocCtrluser = NotNull(partengdt.Rows(0).Item(18), -1)
            PeerReview = NotNull(partengdt.Rows(0).Item(19), 0)

        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Sub
        End Try


        'Get all flags on a part
        Dim flagtable As New DataTable
        flagtable = GetPartFlags(Partnumber, False)
        ActiveFlags = flagtable

        PartFlags = flagtable


    End Sub

    Public Sub SavePartEng()

        Dim setmatconn As New SqlConnection(My.Settings.PartDatabaseString)
        Dim setmatcomm As New SqlCommand("UPDATE PartEng Set ApprovedRev=@approvedrev, NewPart=@newpart, TravellerPrint=@travprint, MaterialApproved=@matapprove, MaterialUser=@matuser, Materialdate=@matdate, " &
            "ProcessApproved=@processapprove, processuser=@processuser, processdate = @processdate, qualityapproved=@qualityapprove, qualityuser = @qualityuser, qualitydate=@qualitydate, InspectionLevel=@level, " &
                                         "DocConApproved = @docctrlapproved, DocConDate = @docctrldate, DocConUser = @docctrluser, PeerReview=@peerreview  WHERE Partnumber=@partno", setmatconn)

        setmatcomm.Parameters.AddWithValue("@approvedrev", ApprovedRev)
        setmatcomm.Parameters.AddWithValue("@newpart", NewPart)
        setmatcomm.Parameters.AddWithValue("@travprint", Travellerprint)
        setmatcomm.Parameters.AddWithValue("@matapprove", MaterialApproved)
        setmatcomm.Parameters.AddWithValue("@matuser", MaterialUser)
        setmatcomm.Parameters.AddWithValue("@matdate", materialdate)
        setmatcomm.Parameters.AddWithValue("@processapprove", ProcessApproved)
        setmatcomm.Parameters.AddWithValue("@processuser", processuser)
        setmatcomm.Parameters.AddWithValue("@processdate", processdate)
        setmatcomm.Parameters.AddWithValue("@qualityapprove", qualityapproved)
        setmatcomm.Parameters.AddWithValue("@qualityuser", qualityuser)
        setmatcomm.Parameters.AddWithValue("@qualitydate", qualitydate)
        setmatcomm.Parameters.AddWithValue("@level", InspectionLevel)
        setmatcomm.Parameters.AddWithValue("@docctrlapproved", DocCtrlapproved)
        setmatcomm.Parameters.AddWithValue("@docctrldate", DocCtrldate)
        setmatcomm.Parameters.AddWithValue("@docctrluser", DocCtrluser)
        setmatcomm.Parameters.AddWithValue("@peerreview", PeerReview)
        setmatcomm.Parameters.AddWithValue("@partno", Partnumber)

        Try

            setmatconn.Open()
            setmatcomm.ExecuteNonQuery()
            setmatconn.Close()

        Catch ex As Exception

            MsgBox(ex.Message)
            setmatconn.Close()

        End Try

    End Sub


    Private Sub AddPartEng()

        'Determine 
        ' AddNew to PartEng

        Dim partengcon As New SqlConnection(My.Settings.PartDatabaseString)
        Dim partengcomm As New SqlCommand("INSERT Into PartEng (ApprovedRev, Partnumber) Values (@apprev, @partnumber)", partengcon)
        partengcomm.Parameters.AddWithValue("@apprev", ApprovedRev)
        partengcomm.Parameters.AddWithValue("@partnumber", Partnumber)

        Try
            partengcon.Open()
            partengcomm.ExecuteNonQuery()
            partengcon.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        'Add flags for all the new eng requirements

        'Material

        'Process

        'Quality





    End Sub



End Class
