Imports System.IO

Public Class frm_logviewer
    Private Sub frm_logviewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'cnt_logtext.Text = File.ReadAllText(Me.Tag)

        Dim ofileStream As FileStream = New FileStream(Me.Tag, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
        Dim oStreamReader As New StreamReader(ofileStream)

        cnt_logtext.Text = oStreamReader.ReadToEnd

    End Sub


    Private Sub cnt_logtext_Click(sender As Object, e As EventArgs) Handles cnt_logtext.Click
        cnt_logtext.SelectionStart = cnt_logtext.Text.Length
        cnt_logtext.ScrollToCaret()
    End Sub

    Private Sub frm_logviewer_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        cnt_logtext.SelectionStart = cnt_logtext.Text.Length
        cnt_logtext.ScrollToCaret()
    End Sub
End Class