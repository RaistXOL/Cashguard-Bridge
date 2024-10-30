Public Class frm_configurazione
    Private Sub cnt_openfolderbrowse_Click(sender As Object, e As EventArgs) Handles cnt_openfolderbrowse.Click
        Dim xRet As DialogResult = cnt_folderbrowse.ShowDialog()

        If xRet = DialogResult.OK Then
            cnt_path_cashguard.Text = cnt_folderbrowse.SelectedPath
        End If

    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        cnt_path_cashguard.Text = My.Settings.cPath
        cnt_cprotect_ip.Text = My.Settings.cIP_cashprotect

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles cnt_save_settings.Click


        My.Settings.cIP_cashprotect = cnt_cprotect_ip.Text
        My.Settings.cPath = cnt_path_cashguard.Text
        My.Settings.Save()
        MsgBox("le impostazioni sono cambiate" & vbCr & "riavviare l'applicazione per rendere effettive le modifiche", vbInformation, "configurazione")
        Me.Close()
    End Sub
End Class