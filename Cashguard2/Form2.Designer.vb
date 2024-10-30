<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_main_pagamento
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
        Me.cnt_importoinserito = New System.Windows.Forms.Label()
        Me.cnt_reso_last = New System.Windows.Forms.Button()
        Me.cnt_reso_all = New System.Windows.Forms.Button()
        Me.cnt_change = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(164, 23)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "importo inserito"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cnt_importoinserito
        '
        Me.cnt_importoinserito.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.cnt_importoinserito.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.cnt_importoinserito.Font = New System.Drawing.Font("Digital-7 Mono", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cnt_importoinserito.Location = New System.Drawing.Point(16, 43)
        Me.cnt_importoinserito.Name = "cnt_importoinserito"
        Me.cnt_importoinserito.Size = New System.Drawing.Size(164, 42)
        Me.cnt_importoinserito.TabIndex = 1
        Me.cnt_importoinserito.Text = "0.00"
        Me.cnt_importoinserito.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cnt_reso_last
        '
        Me.cnt_reso_last.Enabled = False
        Me.cnt_reso_last.Location = New System.Drawing.Point(12, 161)
        Me.cnt_reso_last.Name = "cnt_reso_last"
        Me.cnt_reso_last.Size = New System.Drawing.Size(91, 51)
        Me.cnt_reso_last.TabIndex = 2
        Me.cnt_reso_last.Text = "restituisci ultimo inserito"
        Me.cnt_reso_last.UseVisualStyleBackColor = True
        '
        'cnt_reso_all
        '
        Me.cnt_reso_all.Enabled = False
        Me.cnt_reso_all.Location = New System.Drawing.Point(109, 161)
        Me.cnt_reso_all.Name = "cnt_reso_all"
        Me.cnt_reso_all.Size = New System.Drawing.Size(71, 51)
        Me.cnt_reso_all.TabIndex = 3
        Me.cnt_reso_all.Text = "restituisci tutto"
        Me.cnt_reso_all.UseVisualStyleBackColor = True
        '
        'cnt_change
        '
        Me.cnt_change.Enabled = False
        Me.cnt_change.Location = New System.Drawing.Point(12, 103)
        Me.cnt_change.Name = "cnt_change"
        Me.cnt_change.Size = New System.Drawing.Size(168, 52)
        Me.cnt_change.TabIndex = 4
        Me.cnt_change.Text = "cambio contante"
        Me.cnt_change.UseVisualStyleBackColor = True
        '
        'frm_main_pagamento
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(192, 227)
        Me.ControlBox = False
        Me.Controls.Add(Me.cnt_change)
        Me.Controls.Add(Me.cnt_reso_all)
        Me.Controls.Add(Me.cnt_reso_last)
        Me.Controls.Add(Me.cnt_importoinserito)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frm_main_pagamento"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cnt_importoinserito As Label
    Friend WithEvents cnt_reso_last As Button
    Friend WithEvents cnt_reso_all As Button
    Friend WithEvents cnt_change As Button
End Class
