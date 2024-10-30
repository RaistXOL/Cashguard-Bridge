<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_logviewer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frm_logviewer))
        Me.cnt_logtext = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'cnt_logtext
        '
        Me.cnt_logtext.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cnt_logtext.Location = New System.Drawing.Point(0, 0)
        Me.cnt_logtext.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cnt_logtext.Multiline = True
        Me.cnt_logtext.Name = "cnt_logtext"
        Me.cnt_logtext.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.cnt_logtext.Size = New System.Drawing.Size(632, 490)
        Me.cnt_logtext.TabIndex = 0
        Me.cnt_logtext.WordWrap = False
        '
        'frm_logviewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 490)
        Me.Controls.Add(Me.cnt_logtext)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frm_logviewer"
        Me.Text = "visualizzazione log"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cnt_logtext As TextBox
End Class
