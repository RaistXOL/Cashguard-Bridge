﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_vendita
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cnt_importovendita = New System.Windows.Forms.Label()
        Me.cnt_pagavendita = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cnt_vendita_term = New System.Windows.Forms.Label()
        Me.cnt_vendita_oper = New System.Windows.Forms.Label()
        Me.cnt_vendita_dataora = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cnt_counterform = New System.Windows.Forms.Label()
        Me.cnt_pollingfilename = New System.Windows.Forms.Label()
        Me.cnt_importo_inserito = New System.Windows.Forms.Label()
        Me.cnt_btn_cancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 9)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(229, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "importo vendita"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cnt_importovendita
        '
        Me.cnt_importovendita.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cnt_importovendita.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.cnt_importovendita.Font = New System.Drawing.Font("Digital-7 Mono", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cnt_importovendita.Location = New System.Drawing.Point(16, 39)
        Me.cnt_importovendita.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_importovendita.Name = "cnt_importovendita"
        Me.cnt_importovendita.Size = New System.Drawing.Size(229, 52)
        Me.cnt_importovendita.TabIndex = 2
        Me.cnt_importovendita.Text = "0.00"
        Me.cnt_importovendita.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cnt_pagavendita
        '
        Me.cnt_pagavendita.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cnt_pagavendita.Location = New System.Drawing.Point(16, 231)
        Me.cnt_pagavendita.Margin = New System.Windows.Forms.Padding(4)
        Me.cnt_pagavendita.Name = "cnt_pagavendita"
        Me.cnt_pagavendita.Size = New System.Drawing.Size(229, 47)
        Me.cnt_pagavendita.TabIndex = 4
        Me.cnt_pagavendita.Text = "paga vendita"
        Me.cnt_pagavendita.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cnt_vendita_term)
        Me.GroupBox1.Controls.Add(Me.cnt_vendita_oper)
        Me.GroupBox1.Controls.Add(Me.cnt_vendita_dataora)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 98)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(229, 129)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "altri dettagli"
        '
        'cnt_vendita_term
        '
        Me.cnt_vendita_term.Location = New System.Drawing.Point(97, 92)
        Me.cnt_vendita_term.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_vendita_term.Name = "cnt_vendita_term"
        Me.cnt_vendita_term.Size = New System.Drawing.Size(124, 16)
        Me.cnt_vendita_term.TabIndex = 5
        Me.cnt_vendita_term.Text = "1"
        '
        'cnt_vendita_oper
        '
        Me.cnt_vendita_oper.Location = New System.Drawing.Point(97, 64)
        Me.cnt_vendita_oper.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_vendita_oper.Name = "cnt_vendita_oper"
        Me.cnt_vendita_oper.Size = New System.Drawing.Size(124, 16)
        Me.cnt_vendita_oper.TabIndex = 4
        Me.cnt_vendita_oper.Text = "gianluca"
        '
        'cnt_vendita_dataora
        '
        Me.cnt_vendita_dataora.Location = New System.Drawing.Point(97, 34)
        Me.cnt_vendita_dataora.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_vendita_dataora.Name = "cnt_vendita_dataora"
        Me.cnt_vendita_dataora.Size = New System.Drawing.Size(124, 16)
        Me.cnt_vendita_dataora.TabIndex = 3
        Me.cnt_vendita_dataora.Text = "01-08-2018 20:30"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(25, 92)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "terminale"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(21, 64)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 17)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "operatore"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(24, 34)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "data/ora"
        '
        'cnt_counterform
        '
        Me.cnt_counterform.AutoSize = True
        Me.cnt_counterform.Location = New System.Drawing.Point(247, 231)
        Me.cnt_counterform.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_counterform.Name = "cnt_counterform"
        Me.cnt_counterform.Size = New System.Drawing.Size(16, 17)
        Me.cnt_counterform.TabIndex = 6
        Me.cnt_counterform.Text = "9"
        Me.cnt_counterform.Visible = False
        '
        'cnt_pollingfilename
        '
        Me.cnt_pollingfilename.AutoSize = True
        Me.cnt_pollingfilename.Location = New System.Drawing.Point(249, 250)
        Me.cnt_pollingfilename.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_pollingfilename.Name = "cnt_pollingfilename"
        Me.cnt_pollingfilename.Size = New System.Drawing.Size(51, 17)
        Me.cnt_pollingfilename.TabIndex = 7
        Me.cnt_pollingfilename.Text = "Label5"
        Me.cnt_pollingfilename.Visible = False
        '
        'cnt_importo_inserito
        '
        Me.cnt_importo_inserito.AutoSize = True
        Me.cnt_importo_inserito.Font = New System.Drawing.Font("Digital-7 Mono", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cnt_importo_inserito.Location = New System.Drawing.Point(196, 71)
        Me.cnt_importo_inserito.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.cnt_importo_inserito.Name = "cnt_importo_inserito"
        Me.cnt_importo_inserito.Size = New System.Drawing.Size(45, 20)
        Me.cnt_importo_inserito.TabIndex = 9
        Me.cnt_importo_inserito.Text = "0.00"
        '
        'cnt_btn_cancel
        '
        Me.cnt_btn_cancel.BackColor = System.Drawing.Color.Transparent
        Me.cnt_btn_cancel.Location = New System.Drawing.Point(218, 9)
        Me.cnt_btn_cancel.Name = "cnt_btn_cancel"
        Me.cnt_btn_cancel.Size = New System.Drawing.Size(26, 28)
        Me.cnt_btn_cancel.TabIndex = 10
        Me.cnt_btn_cancel.Text = "X"
        Me.cnt_btn_cancel.UseVisualStyleBackColor = False
        '
        'frm_vendita
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(261, 284)
        Me.ControlBox = False
        Me.Controls.Add(Me.cnt_btn_cancel)
        Me.Controls.Add(Me.cnt_importo_inserito)
        Me.Controls.Add(Me.cnt_pollingfilename)
        Me.Controls.Add(Me.cnt_counterform)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cnt_pagavendita)
        Me.Controls.Add(Me.cnt_importovendita)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frm_vendita"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cnt_importovendita As Label
    Friend WithEvents cnt_pagavendita As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents cnt_vendita_term As Label
    Friend WithEvents cnt_vendita_oper As Label
    Friend WithEvents cnt_vendita_dataora As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents cnt_counterform As Label
    Friend WithEvents cnt_pollingfilename As Label
    Friend WithEvents cnt_importo_inserito As Label
    Friend WithEvents cnt_btn_cancel As Button
End Class
