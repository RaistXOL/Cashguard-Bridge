Public Class frm_prelevamento
    Private NRow As Integer = 3
    Private NCol As Integer = 2
    Private oBottone((NRow + 1) * (NCol + 1) - 1) As Button
    Public lDecimal As Boolean = False
    Public nDecimalcount As Integer = 1
    Private Sub cnt_importoprelevamento_Click(sender As Object, e As EventArgs) Handles cnt_importoprelevamento.Click

    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim i


        For i = 0 To oBottone.Length - 4
            oBottone(i) = New Button()
            oBottone(i).Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
            oBottone(i).Text = CStr(oBottone.Length - i - 3)
            oBottone(i).Margin = New Padding(1)
            cnt_layout.Controls.Add(oBottone(i), i Mod (NCol + 1) + 1, i \ (NCol + 1))
            AddHandler oBottone(i).Click, AddressOf ClickHandler
        Next

        'pulsanti speciali (0)
        oBottone(i) = New Button()
        oBottone(i).Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        oBottone(i).Text = "0"
        oBottone(i).Margin = New Padding(1)
        cnt_layout.SetColumnSpan(oBottone(i), 2)
        cnt_layout.Controls.Add(oBottone(i), 2, 3)
        AddHandler oBottone(i).Click, AddressOf ClickHandler


        'pulsanti speciali (,)
        oBottone(i) = New Button()
        oBottone(i).Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        oBottone(i).Text = ","
        oBottone(i).Margin = New Padding(1)
        cnt_layout.Controls.Add(oBottone(i), 1, 3)
        AddHandler oBottone(i).Click, AddressOf ClickHandler


    End Sub

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As System.EventArgs)


        'MsgBox("I am button #" & CType(sender, Button).Text)
        If cnt_importoprelevamento.Text = "0.00" Then
            cnt_importoprelevamento.Text = "0"
        End If

        If CType(sender, Button).Text = "," Then
            cnt_importoprelevamento.Text = cnt_importoprelevamento.Text + CType(sender, Button).Text
            lDecimal = True
        Else
            If cnt_importoprelevamento.Text = "0" Then
                cnt_importoprelevamento.Text = Val(CType(sender, Button).Text)
            Else
                If Not lDecimal Then
                    cnt_importoprelevamento.Text = Val(cnt_importoprelevamento.Text) * 10 + Val(CType(sender, Button).Text)
                Else


                    If Not nDecimalcount = 100 Then
                        nDecimalcount = nDecimalcount * 10
                        cnt_importoprelevamento.Text = Val(cnt_importoprelevamento.Text.Replace(",", ".")) + Val(CType(sender, Button).Text) / nDecimalcount
                    End If

                End If

            End If

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cnt_azzera.Click
        cnt_importoprelevamento.Text = "0.00"
        nDecimalcount = 1
        lDecimal = False
    End Sub

    Private Sub cnt_preleva_Click(sender As Object, e As EventArgs) Handles cnt_preleva.Click
        Dim nAmountRest As Integer = 0
        Dim cMessage As String = Space(100)

        If MsgBox("conferma il prelevamento di " + Format(Val(cnt_importoprelevamento.Text.Replace(",", ".")), "####,##0.00") + " euro ?", MsgBoxStyle.YesNo + MsgBoxStyle.Information, "conferma") = vbYes Then

            oLog.WriteLine(WriteLogLine("(UI) confermato prelavamento di " + cnt_importoprelevamento.Text + " euro"))
            oLog.WriteLine(WriteLogLine("(CG) disabilito pagamento"))
            'frm_main.oCashGuard.disablePayinCG(cMessage)
            oLog.WriteLine(WriteLogLine("(CG) pagamento disabilitato, messaggio:" + cMessage))
            oLog.WriteLine(WriteLogLine("(CG) inizio erogazione"))
            'frm_main.oCashGuard.dispenseCG(Val(cnt_importoprelevamento.Text.Replace(",", ".")) * 100, "", nAmountRest, cMessage)
            oLog.WriteLine(WriteLogLine("(CG) fine erogazione, messaggio:" + cMessage))
            If Not cMessage.ToLower = "done" Then

                MsgBox("errore " & cMessage, MsgBoxStyle.Critical, "errore")
                oLog.WriteLine(WriteLogLine("(CG) errore " + cMessage))
            Else
                oLog.WriteLine(WriteLogLine("(CG) operazione completata " + cMessage))

            End If

            oLog.WriteLine(WriteLogLine("(CG) abilito pagamento"))
            'frm_main.oCashGuard.enablePayinCG(cMessage)
            oLog.WriteLine(WriteLogLine("(CG) pagamento abilitato, messaggio:" + cMessage))

            Me.Close()

        End If

    End Sub
End Class