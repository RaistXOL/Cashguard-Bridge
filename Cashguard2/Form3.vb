﻿Imports System.IO
Imports System.Threading

Public Class frm_vendita
    Public aParametriArray
    Public cAbortPacket = ""


    Private Sub frm_vendita_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        aParametriArray = Split(cnt_counterform.Text, ",",, CompareMethod.Text)
        cAbortPacket = ""

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles cnt_pagavendita.Click

        Dim nResto As Decimal = 0
        Dim cMessaggio As String = Space(160)
        Dim cErrore As String = ""
        Dim nStatus = 0
        Dim cBuffer As String
        Dim cImporto As String = cnt_importovendita.Text
        Dim nImportoInserito As Decimal = 0
        Dim aFrames() As String
        Dim k As Integer
        Dim lTerminaTransazione As Boolean = False
        Dim nPos As Integer = 0

        cnt_btn_cancel.Enabled = False


        For nPos = 1 To My.Application.OpenForms.Count - 1
            If Not My.Application.OpenForms.Item(nPos).Handle = My.Application.OpenForms.Item(0).ActiveMdiChild.Handle Then
                My.Application.OpenForms.Item(nPos).Enabled = False
            End If
        Next

        If cnt_pagavendita.Text = "annulla incasso" Then
            ' cosa molto brutta:
            ' se ritorno qui dopo un click di abort, il do while sotto sta ancora girando
            ' imposto una var pubblica per mandare un pacchetto di annullo.
            ' molto molto brutta :-(
            cAbortPacket = "<App><CMPaymentCancel Reimburse=""True""/></App>"
            Exit Sub
        Else
            cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, "<App><CMSendPayment Amount=""" + (Val(cnt_importovendita.Text) * 100).ToString + """ Sign=""+""/></App>", "CMPaymentStarted")
            cnt_pagavendita.Text = "annulla incasso"
            cnt_pagavendita.BackColor = Color.OrangeRed
        End If

        Do While Not lTerminaTransazione

            cBuffer = ReceiveTCP(My.Settings.cIP_cashprotect, 40001, cAbortPacket)
            cAbortPacket = ""

            aFrames = frm_main.ParseAnswer(cBuffer)

            For k = 0 To aFrames.Length - 1

                Console.WriteLine(aFrames(k) + " " + k.ToString)

                'If aFrames(k).Contains("Amount") Then
                ' nImportoInserito += System.Text.RegularExpressions.Regex.Match(aFrames(k), "[0-9]+").Value / 100
                'End If

                If aFrames(k).Contains("CMPaymentReceived") Then
                    Dim oXml = XDocument.Parse(aFrames(k))
                    nImportoInserito += oXml.<Cash>.<CMPaymentReceived>.@Amount / 100
                    File.WriteAllText(cnt_pollingfilename.Text, "0;" + Str(nImportoInserito))
                End If

                If aFrames(k).Contains("CMError") Then
                    Dim oXml = XDocument.Parse(aFrames(k))

                    frm_main.cnt_status_levelwarning.Image = My.Resources._error
                    frm_main.cnt_status_general.Text = oXml.<Cash>.<CMError>.@Description
                    lTerminaTransazione = True
                End If

                If aFrames(k).Contains("CMPaymentDispensed") AndAlso aFrames(k).Contains("Denomination") Then
                    Dim oXml = XDocument.Parse(aFrames(k))

                    nResto += (Val(oXml.<Cash>.<CMPaymentDispensed>.@Denomination) * oXml.<Cash>.<CMPaymentDispensed>.@Quantity) / 100
                End If

                If aFrames(k).Contains("CMDenominationLevels") Then
                    Dim oXml = XDocument.Parse(aFrames(k))

                    Dim cDenomLog As String = ""

                    Dim oNodes = oXml.<Cash>.<CMDenominationLevels>.DescendantNodes

                    Dim aLevels As New Dictionary(Of String, String)

                    aLevels.Add("HIGH", "ALTO")
                    aLevels.Add("MAX", "MASSIMO")
                    aLevels.Add("LOW", "BASSO")

                    For Each oNode In oNodes
                        ' downcasting
                        Dim oXElement As XElement = oNode
                        cDenomLog += "pezzatura " + (oXElement.@Value / 100).ToString("##0.00") + " livello " + aLevels.Item(oXElement.@Level) & vbCr
                    Next

                    frm_main.cnt_status_levelwarning.Image = My.Resources.warning
                    frm_main.cnt_status_levelwarning.ToolTipText = cDenomLog

                End If

                If aFrames(k).Contains("CMPaymentClosed") Then
                    lTerminaTransazione = True
                End If

            Next

            cnt_importo_inserito.Text = nImportoInserito.ToString("##0.00")

            Application.DoEvents()
            System.Threading.Thread.Sleep(1000)
        Loop

        If nImportoInserito >= Val(cnt_importovendita.Text) Or lTerminaTransazione Then
            aWindows(aParametriArray(0), aParametriArray(1)) = 0
            nWindowsCount = nWindowsCount - 1

            If Not nImportoInserito >= Val(cnt_importovendita.Text) Then
                File.WriteAllText(cnt_pollingfilename.Text, Str(-1))
            Else
                File.WriteAllText(cnt_pollingfilename.Text, Str(-2))
            End If


            MsgBox("pagamento completato." & vbCr & "resto erogato: " & nResto.ToString("##0.00") & " euro", MsgBoxStyle.Information, "Cashguard")
                frm_main.mnu_elenco_finestre.DropDownItems.RemoveByKey("vendita " + Trim(cnt_counterform.Text))

                Me.Close()

            End If

            For nPos = 1 To My.Application.OpenForms.Count - 1

            My.Application.OpenForms.Item(nPos).Enabled = True

        Next


    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        Me.WindowState = FormWindowState.Minimized

        aWindows(aParametriArray(0), aParametriArray(1)) = 0
    End Sub

    Private Sub frm_vendita_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        aParametriArray = Split(cnt_counterform.Text, ",",, CompareMethod.Text)
    End Sub

    Private Sub frm_vendita_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            'MsgBox("minimo")
        ElseIf Me.WindowState = FormWindowState.Normal Then
            'MsgBox("normale")
            'frm_main.LayoutMdi(MdiLayout.TileHorizontal)
        End If
    End Sub


    Private Sub cnt_btn_cancel_Click(sender As Object, e As EventArgs) Handles cnt_btn_cancel.Click
        Me.Close()
        File.WriteAllText(cnt_pollingfilename.Text, Str(-1))
        aWindows(aParametriArray(0), aParametriArray(1)) = 0
    End Sub
End Class