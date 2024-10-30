Public Class frm_main_pagamento


    Private Sub cnt_reso_last_Click(sender As Object, e As EventArgs) Handles cnt_reso_last.Click

        Dim cMessaggio As String = Space(60)
        'frm_main.oCashGuard.regretCG(0, cMessaggio)

        frm_main.cnt_status_cguard_generale.Text = cMessaggio.ToLower
    End Sub

    Private Sub cnt_reso_all_Click(sender As Object, e As EventArgs) Handles cnt_reso_all.Click
        Dim cMessaggio As String = Space(60)
        'frm_main.oCashGuard.regretCG(1, cMessaggio)

        frm_main.cnt_status_cguard_generale.Text = cMessaggio.ToLower
    End Sub

    Private Sub frm_main_pagamento_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub cnt_change_Click(sender As Object, e As EventArgs) Handles cnt_change.Click
        Dim cMessaggio As String = Space(60)
        oLog.WriteLine(WriteLogLine("(UI) richiesto cambio contante, importo " + cnt_importoinserito.Text))
        oLog.WriteLine(WriteLogLine("(CG) inizio richiesta cambio"))
        'frm_main.oCashGuard.changeCG(cMessaggio)
        frm_main.cnt_status_cguard_generale.Text = cMessaggio.ToLower
        oLog.WriteLine(WriteLogLine("(CG) fine richiesta cambio, messaggio: " + cMessaggio))
    End Sub
End Class