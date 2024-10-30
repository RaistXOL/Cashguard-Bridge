Imports System.ComponentModel
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath

Public Class frm_main


    Dim oTextBoxDraw(11) As TextBox
    Dim oTextBoxDrawMax(11) As Label
    Dim oPBDrawMaxSource(11) As ProgBar.ProgBarPlus

    Public Const dLOGIN = 1

    Public Class ArgumentType
        Public nFunzione As Int32
    End Class

    Public Class Operatore
        Public Property cNomeOperatore As String
        Public Property nImportoNonIncassato As Single

    End Class


    Public Class oResult
        Public nFunzione As Int32
        Public lRisultato As Boolean
    End Class

    Public Sub PurgeOldLog(ByVal cPath As String)

        Dim cFiles As String = ""

        If Not cPath = "" Then

            For Each cFiles In Directory.GetFiles(cPath, "*.*", SearchOption.AllDirectories)

                If File.GetCreationTime(cFiles).Date < Date.Now.Date.AddDays(-30) Then
                    File.Delete(cFiles)
                End If
            Next

        End If

    End Sub

    Private Sub frm_main_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim oArgs As ArgumentType = New ArgumentType()
        Dim cBuffer As String

        oTextBoxDraw = {cnt_txt_1, cnt_txt_2, cnt_txt_5, cnt_txt_10, cnt_txt_20, cnt_txt_50, cnt_txt_100, cnt_txt_200, cnt_txt_500, cnt_txt_1000, cnt_txt_2000}
        oTextBoxDrawMax = {cnt_txt_max_1, cnt_txt_max_2, cnt_txt_max_5, cnt_txt_max_10, cnt_txt_max_20, cnt_txt_max_50, cnt_txt_max_100, cnt_txt_max_200, cnt_txt_max_500, cnt_txt_max_1000, cnt_txt_max_2000}
        oPBDrawMaxSource = {cnt_pb_1, cnt_pb_2, cnt_pb_5, cnt_pb_10, cnt_pb_20, cnt_pb_50, cnt_pb_100, cnt_pb_200, cnt_pb_500, cnt_pb_1000, cnt_pb_2000}

        oArgs.nFunzione = 1

        If Not Directory.Exists(Path.GetDirectoryName(cFileLog)) Then
            Directory.CreateDirectory(Path.GetDirectoryName(cFileLog))
        End If

        PurgeOldLog(Path.GetFullPath(cFileLog).Replace(Path.GetFileName(cFileLog), ""))

        oLog.AutoFlush = True

        frm_logviewer.Tag = cFileLog


        Dim opb As Control = New ProgBar.ProgBarPlus






        'frm_main_pagamento.Owner = Me
        'frm_main_pagamento.MdiParent = Me

        'frm_main_pagamento.Show()

        'impostazione iniziale form
        'frm_main_pagamento.Location = New Point(((Me.ClientSize.Width - frm_main_pagamento.Width) - 5) / 2, ((Me.ClientSize.Height - frm_main_pagamento.Height) - 30) / 2)
        cnt_status_levelwarning.Image = Nothing

        'marco come presente permanentemente la finestra incasso (riga 2 - colonna 3)
        'aWindows(1, 2) = 1

        oLog.WriteLine(WriteLogLine("(UI) inizializzazione form completata"))
        oLog.WriteLine(WriteLogLine("(CG) inizializzazione worker"))

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMActivateCashPoint ForceReset='False'/></App>")
        ParseAnswer(cBuffer)

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMPoll/></App>", "CMPoll")
        ParseAnswer(cBuffer)

        cnt_status_cguard_generale.Text = "interfaccia pronta"

    End Sub

    Private Sub frm_main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        frm_main_pagamento.Location = New Point(((Me.ClientSize.Width - frm_main_pagamento.Width) - 5) / 2, ((Me.ClientSize.Height - frm_main_pagamento.Height) - 30) / 2)
    End Sub


    Public Function ParseAnswer(cBuffer As String) As String()

        Dim nTotals As Double = 0
        Dim aFrame() As String = {}
        Dim k

        If Not cBuffer = "" Then
            aFrame = cBuffer.Split({"</Cash>"}, StringSplitOptions.None)
        Else
            Return aFrame
        End If

        For k = 0 To aFrame.Length - 2
            aFrame(k) += "</Cash>"

            Dim oXml = XDocument.Parse(aFrame(k))

            ' analizzo primo nodo
            If oXml.FirstNode.ToString.Contains("CMWarning") Then
                cnt_status_cguard_generale.Text = oXml.Root.Element("CMWarning").Attribute("Description")
            ElseIf oXml.FirstNode.ToString.Contains("CMPoll") Then

                If oXml.XPathSelectElement("//Device[@Name='ENGINE']").Attribute("Status") = "WAIT_COMMAND" Then
                    cnt_status_cprotect_engine.Image = My.Resources.bullet_ball_glass_green
                ElseIf oXml.XPathSelectElement("//Device[@Name='ENGINE']").Attribute("Status").ToString.Contains("WAIT") Then
                    cnt_status_cprotect_engine.Image = My.Resources.bullet_ball_glass_yellow
                End If

                If oXml.XPathSelectElement("//Device[@Name='COINDEVICE']").Attribute("Status") = "READY_IDLE" Then
                    cnt_status_cprotect_coindevice.Image = My.Resources.bullet_ball_glass_green
                ElseIf oXml.XPathSelectElement("//Device[@Name='COINDEVICE']").Attribute("Status").ToString.Contains("ERROR") Then
                    cnt_status_cprotect_coindevice.Image = My.Resources.bullet_ball_glass_red
                End If

                If oXml.XPathSelectElement("//Device[@Name='BILLDEVICE']").Attribute("Status") = "READY_IDLE" Then
                    cnt_status_cprotect_billdevice.Image = My.Resources.bullet_ball_glass_green
                ElseIf oXml.XPathSelectElement("//Device[@Name='BILLDEVICE']").Attribute("Status").ToString.Contains("ERROR") Then
                    cnt_status_cprotect_billdevice.Image = My.Resources.bullet_ball_glass_red
                End If

            ElseIf oXml.FirstNode.ToString.Contains("CMContent") Then

                Dim oNodes = oXml.<Cash>.<CMContent>.DescendantNodes

                nTotals = 0

                For Each oNode In oNodes
                    ' downcasting
                    Dim oXElement As XElement = oNode

                    Dim oControl = CType(Me.Controls.Find("cnt_pb_" + oXElement.@Value, True)(0), ProgBar.ProgBarPlus)
                    Dim nPerc = Val(oXElement.@Quantity) / oControl.Max * 100

                    Console.WriteLine(nPerc)

                    oControl.Value = Val(oXElement.@Quantity)

                    nTotals += (oXElement.@Value / 100) * oXElement.@Quantity

                    If nPerc >= 90 Or nPerc <= 10 Then
                        oControl.BarColorSolid = Color.Red
                    ElseIf nPerc >= 80 Or nPerc <= 20 Then
                        oControl.BarColorSolid = Color.Yellow
                    Else
                        oControl.BarColorSolid = Color.LimeGreen
                    End If

                Next

                cnt_totals.Text = nTotals

            ElseIf oXml.FirstNode.ToString.Contains("CMDropBoxContent") Then

                Dim oNodes = oXml.<Cash>.<CMDropBoxContent>.DescendantNodes
                nTotals += Val(cnt_totals.Text.Replace(",", "."))

                For Each oNode In oNodes

                    ' downcasting
                    Dim oXElement As XElement = oNode

                    nTotals += oXElement.@Denomination / 100 * oXElement.@Quantity

                Next

                cnt_totals.Text = nTotals
                cnt_totals.Text = Format(nTotals, "##,##0.00")

            End If

        Next


        Return aFrame
    End Function

    Private Sub frm_main_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim cMessaggio As String = Space(160)

        If MsgBox("la chiusura dell'applicazione causerà l'interruzione del collegamento fra farmaconsult e il Cashprotect" & vbCr & vbCr & "si desidera continuare?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, "conferma chiusura") = vbNo Then
            e.Cancel = True
        Else
            oLog.WriteLine(WriteLogLine("(UI) utente ha richiesto chiusura interfaccia"))

            cnt_status_cguard_generale.Text = "chiusura cassa in corso..."
            Me.Refresh()

            oTCPClient.Close()

            oLog.Flush()
            oLog.Dispose()
        End If
    End Sub



    Private Sub cnt_timer_polling_Tick(sender As Object, e As EventArgs) Handles cnt_timer_polling.Tick
        Dim aParametri() As String
        Dim k As Byte
        Dim aFiles As String()
        Dim nXMaxWindows As Byte = Me.Width \ frm_vendita.Width
        Dim nYMaxWindows As Byte = Me.Height \ frm_vendita.Height


        Try
            oLog.WriteLine(WriteLogLine("(DBG) inizio ricerca file comandi"))
            aFiles = Directory.GetFiles(My.Settings.cPath, "CPROTECT*.TXT")
            oLog.WriteLine(WriteLogLine("(DBG) fine ricerca file comandi"))

            For Each cFile As String In aFiles

                Using oFileStream As New StreamReader(cFile)
                    aParametri = Split(oFileStream.ReadToEnd(), ";")
                End Using

                If File.GetCreationTime(cFile).Date < Now.Date Or File.GetCreationTime(cFile).Date.Hour < Now.Date.Hour Then
                    File.Delete(cFile)
                End If

                '1 Parametro nel file == CODICE DI RITORNO ( es 1 )
                '2 Parametri nel file == CONTANTE INSERITO ( es imp;500 )
                '4 Parametri nel file == RICHIESTA INCASSO ( es 500;1;2 )
                '3 Parametri nel file == CANCELLA INCASSO ( es -999;1;2 )
                If aParametri.GetLength(0) = 4 Then
                    cnt_timer_polling.Enabled = False
                    oLog.WriteLine(WriteLogLine("(UI) trovato file comandi " + cFile))

                    If aParametri(0) = -999 Then
                        'cnt_log.Items.Add(DateTime.Now.ToLongTimeString + " > trovato file cashguard di annullamento transazione.")

                    Else
                        'cnt_log.Items.Add(DateTime.Now.ToLongTimeString + " > trovato file cashguard: importo =>" & aParametri(0))
                        ' LTrim( Str( nImporto ) ) + ";" + cOper + ";" + cJob + ";" + Operatori( 4, cOperatore )

                        ' posizione
                        k = nWindowsCount 'Application.OpenForms.Count - 2

                        Dim oForm As New frm_vendita

                        oForm.MdiParent = Me
                        oForm.Name = "vendita " + Trim(Str(k \ nXMaxWindows)) + "," + Trim(Str(k Mod nXMaxWindows))
                        oForm.cnt_pollingfilename.Text = cFile
                        oForm.cnt_importovendita.Text = aParametri(0)
                        oForm.cnt_vendita_oper.Text = aParametri(3) + "(" + aParametri(1) + ")"
                        oForm.cnt_vendita_dataora.Text = DateTime.Now.ToString
                        oForm.cnt_vendita_term.Text = aParametri(2)

                        oLog.WriteLine(WriteLogLine("(UI) creata finestra incasso, importo:" + aParametri(0) + " operatore:" + aParametri(1)))

                        Dim lLibero As Boolean = False
                        Dim nPos As Integer = 0

                        Do
                            If aWindows(nPos \ nXMaxWindows, nPos Mod nXMaxWindows) = 0 Then
                                lLibero = True
                            End If

                            nPos = nPos + 1

                        Loop Until lLibero

                        k = nPos - 1

                        oForm.Show()

                        oForm.Top = (IIf(k > 14, k - 14, k) \ nXMaxWindows) * oForm.Height '+ 50
                        oForm.Left = (k Mod nXMaxWindows) * (oForm.Width + 3)

                        aWindows(k \ nXMaxWindows, k Mod nXMaxWindows) = 1
                        oForm.cnt_counterform.Text = Trim(Str(k \ nXMaxWindows)) + "," + Trim(Str(k Mod nXMaxWindows))


                        mnu_elenco_finestre.DropDownItems.Add("vendita " + Trim(Str(k))).Name = "vendita " + Trim(Str(k \ nXMaxWindows)) + "," + Trim(Str(k Mod nXMaxWindows))

                        nWindowsCount = nWindowsCount + 1
                    End If

                    File.Delete(cFile)
                    cnt_timer_polling.Enabled = True
                    oLog.WriteLine(WriteLogLine("(UI) gestione terminata, finestra aperta in posizione " + Str(nXMaxWindows) + "." + LTrim(Str(k Mod nXMaxWindows))))

                Else

                End If

            Next

        Catch ex As Exception

            If TypeOf ex Is IOException Then
                oLog.WriteLine(WriteLogLine("(UI) errore: " + ex.Message + " " + ex.StackTrace))
            Else
                cnt_timer_polling.Enabled = False
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Errore")
                oLog.WriteLine(WriteLogLine("(UI) errore: " + ex.Message + " " + ex.StackTrace))
            End If


        End Try
    End Sub

    Private Sub mnu_elenco_finestre_DropDownItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles mnu_elenco_finestre.DropDownItemClicked

        Application.OpenForms.Item(e.ClickedItem.Name).WindowState = FormWindowState.Normal

    End Sub


    Private Sub ReimpostaCashguardToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Dim cMessaggio As String = Space(160)
        'oCashGuard.resetCG(cMessaggio)
        cnt_status_cguard_generale.Text = cMessaggio
    End Sub

    Private Sub ConfiguraInterfacciaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfiguraInterfacciaToolStripMenuItem.Click
        frm_configurazione.ShowDialog()
    End Sub

    Private Sub VisualizzaLogToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VisualizzaLogToolStripMenuItem.Click
        frm_logviewer.ShowDialog()
    End Sub

    Private Sub ReimpostaPosizioneFinestreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles mnu_importiaperti.Click

        Dim nOpenForms As Byte = Application.OpenForms.Count
        Dim k As Byte
        Dim nImporto As Single = 0
        Dim arrList As New ArrayList()


        For k = 1 To nOpenForms - 1

            If Application.OpenForms(k).Name.Contains("vendita") Then

                Dim query = From operatore As Operatore In arrList
                            Where operatore.cNomeOperatore = Application.OpenForms(k).Controls("groupbox1").Controls("cnt_vendita_oper").Text
                            Select operatore

                If query.Count > 0 Then
                    'Dim oOperatore As Operatore
                    For Each operatore As Operatore In query
                        operatore.cNomeOperatore = Application.OpenForms(k).Controls("groupbox1").Controls("cnt_vendita_oper").Text
                        operatore.nImportoNonIncassato = operatore.nImportoNonIncassato + Val(Application.OpenForms(k).Controls("cnt_importovendita").Text)
                        nImporto += Val(Application.OpenForms(k).Controls("cnt_importovendita").Text)
                    Next

                Else

                    Dim oOperatore As New Operatore With {.cNomeOperatore = Application.OpenForms(k).Controls("groupbox1").Controls("cnt_vendita_oper").Text, .nImportoNonIncassato = Val(Application.OpenForms(k).Controls("cnt_importovendita").Text)}
                    nImporto += Val(Application.OpenForms(k).Controls("cnt_importovendita").Text)
                    arrList.Add(oOperatore)
                End If
            End If

        Next

        If arrList.Count > 0 Then
            frm_report.dg_operatori.DataSource = arrList
            frm_report.dg_operatori.Columns("cNomeOperatore").HeaderText = "Nome Operatore (terminale)"
            frm_report.dg_operatori.Columns("nImportoNonIncassato").HeaderText = "Importo non incassato in €"
        End If
        frm_report.cnt_totale_rapportino.Text = Format(nImporto, "#####,##0.00")
        frm_report.ShowDialog()

    End Sub

    Private Sub frm_preleva_Click(sender As Object, e As EventArgs)
        frm_prelevamento.ShowDialog()
        frm_prelevamento.Dispose()
    End Sub

    Private Sub cnt_status_inve_Click(sender As Object, e As EventArgs) Handles cnt_status_inve.Click
        Dim cBuffer

        cnt_panel_levels.Visible = Not cnt_panel_levels.Visible

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMContent/></App>", "CMContent")
        ParseAnswer(cBuffer)

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMDropBoxContent/></App>", "CMDropBoxContent")
        ParseAnswer(cBuffer)

    End Sub

    ' multiple events handler
    Private Sub cnt_pb_nnn_Click(sender As Object, e As EventArgs) Handles cnt_pb_500.Click, cnt_pb_2.Click, cnt_pb_1.Click, cnt_pb_50.Click, cnt_pb_5.Click, cnt_pb_2000.Click, cnt_pb_200.Click, cnt_pb_20.Click, cnt_pb_1000.Click, cnt_pb_100.Click, cnt_pb_10.Click

        Dim cnt_pb = DirectCast(sender, ProgBar.ProgBarPlus)

        'Dim cnt_panel_draw As New Panel

        'cnt_panel_draw.Parent = Me

        cnt_panel_draw.Top = cnt_pb.Top + cnt_panel_levels.Top + 2 + IIf(cnt_pb.Name.ContainsAny({"500", "1000", "2000"}), GroupBox2.Top - 21, 0)
        cnt_panel_draw.Left = cnt_pb.Left + cnt_panel_levels.Left + 8
        cnt_panel_draw.Width = cnt_pb.Width
        'cnt_panel_draw.Height = cnt_pb.Height + 50
        cnt_panel_draw.Tag = cnt_pb.Name
        cnt_lbl_draw.Text = "preleva " + Val(cnt_pb.Name.Substring(7) / 100).ToString + " €"
        cnt_panel_draw.BringToFront()
        cnt_panel_draw.Visible = True



    End Sub

    Private Sub cnt_pb_1_Load(sender As Object, e As EventArgs) Handles cnt_pb_1.Load

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        cnt_panel_draw.Visible = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox(cnt_panel_draw.Tag)
        'Dim cnt_pb = DirectCast(cnt_panel_draw.Tag, ProgBar.ProgBarPlus)

        Dim oControl = CType(Me.Controls.Find(cnt_panel_draw.Tag, True)(0), ProgBar.ProgBarPlus)
        Dim cBuffer

        'MsgBox(oControl.Value)
        'MsgBox("<App><CMUnloadElement><Element Denomination=""" + Val(oControl.Name.Substring(7)).ToString + """ Keep=""" + (oControl.Value - Val(cnt_draw_qty.Text)).ToString + """/></CMUnloadElement></App>")

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMUnloadElement><Element Denomination=""" + Val(oControl.Name.Substring(7)).ToString + """ Keep=""" + (oControl.Value - Val(cnt_draw_qty.Text)).ToString + """/></CMUnloadElement></App>", "CMUnloadElement/")

        ParseAnswer(cBuffer)


        cnt_panel_draw.Visible = False

    End Sub

    Private Sub cnt_btn_add_Click(sender As Object, e As EventArgs) Handles cnt_btn_fill_start.Click

        Dim cBuffer

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMStartFilling /></App>", "CMStartFilling")
        ParseAnswer(cBuffer)

        cnt_panel_fill.Top = cnt_panel_levels.Top
        cnt_panel_fill.Left = cnt_panel_levels.Left
        'cnt_panel_fill.Width = cnt_pb.Width
        cnt_panel_fill.BringToFront()
        cnt_panel_fill.Visible = True

    End Sub

    Private Sub cnt_btn_fill_end_Click(sender As Object, e As EventArgs) Handles cnt_btn_fill_end.Click
        Dim cBuffer

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMEndFilling /></App>", "CMEndFilling")

        ParseAnswer(cBuffer)

        cnt_panel_fill.Visible = False

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMContent/></App>", "CMContent")
        ParseAnswer(cBuffer)

        cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMDropBoxContent/></App>", "CMDropBoxContent")
        ParseAnswer(cBuffer)

    End Sub


    Private Sub cnt_txt_nnn_TextChanged(sender As Object, e As EventArgs) Handles cnt_txt_1.TextChanged, cnt_txt_500.TextChanged, cnt_txt_50.TextChanged, cnt_txt_5.TextChanged, cnt_txt_2000.TextChanged, cnt_txt_200.TextChanged, cnt_txt_20.TextChanged, cnt_txt_2.TextChanged, cnt_txt_1000.TextChanged, cnt_txt_100.TextChanged, cnt_txt_10.TextChanged

        Dim cnt_draw_tbox = DirectCast(sender, TextBox)

        Dim k As Byte
        Dim nTotal As Double = 0
        Dim oControl As New Label

        Dim cMaxInve = oTextBoxDrawMax.First(Function(s) s.Name = "cnt_txt_max_" + cnt_draw_tbox.Name.Replace("cnt_txt_", "")).Text

        If Val(cnt_draw_tbox.Text) > Val(cMaxInve) Then

            cnt_draw_tbox.Text = cMaxInve
        End If

        For k = 0 To 10
            nTotal += (Val(oTextBoxDraw(k).Name.Substring(8)) / 100) * Val(oTextBoxDraw(k).Text)
        Next

        cnt_lbl_draw_total.Text = Format(nTotal, "#####,##0.00")

    End Sub

    Private Sub cnt_draw_full_confirm_Click(sender As Object, e As EventArgs) Handles cnt_draw_full_confirm.Click

        Dim lAlmenoUno As Boolean = False
        Dim cBuffer As String = ""
        Dim cPayload As String = "<App><CMUnloadElement>"

        For k = 0 To 10

            If Val(oTextBoxDraw(k).Text) > 0 Then
                lAlmenoUno = lAlmenoUno Or True
                cPayload += "<Element Denomination=""" + Val(oTextBoxDraw(k).Name.Substring(8).Replace(",", ".")).ToString + """ Keep=""" + (Val(oTextBoxDrawMax(k).Text) - Val(oTextBoxDraw(k).Text)).ToString + """/>"
            End If

        Next

        cPayload += "</CMUnloadElement></App>"

        MsgBox(cPayload)

        If lAlmenoUno Then
            cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, cPayload, "CMUnloadElementStarted/")
            ParseAnswer(cBuffer)

            'cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMContent/></App>", "CMContent")
            'ParseAnswer(cBuffer)

            'cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMDropBoxContent/></App>", "CMDropBoxContent")
            'ParseAnswer(cBuffer)

        End If

        cnt_panel_draw_complete.Visible = False

    End Sub

    Private Sub cnt_btn_draw_Click(sender As Object, e As EventArgs) Handles cnt_btn_draw.Click

        Dim k

        For k = 0 To 10

            oTextBoxDrawMax(k).Text = oPBDrawMaxSource(k).Value
            oTextBoxDraw(k).Text = ""

        Next

        cnt_panel_draw_complete.Top = cnt_panel_levels.Top + cnt_lbl_header_levels.Height
        cnt_panel_draw_complete.Left = cnt_panel_levels.Left
        cnt_panel_draw_complete.Width = cnt_panel_levels.Width
        cnt_panel_draw_complete.Height = cnt_panel_levels.Height - cnt_lbl_header_levels.Height

        cnt_panel_draw_complete.BringToFront()
        cnt_panel_draw_complete.Visible = True


    End Sub

    Private Sub cnt_draw_full_cancel_Click(sender As Object, e As EventArgs) Handles cnt_draw_full_cancel.Click
        cnt_panel_draw_complete.Hide()
    End Sub
End Class
