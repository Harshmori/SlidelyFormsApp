<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SearchSubmissionsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cmbField = New System.Windows.Forms.ComboBox()
        Me.txtSearchValue = New System.Windows.Forms.TextBox()
        Me.btnSubmitSearch = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'cmbField
        '
        Me.cmbField.FormattingEnabled = True
        Me.cmbField.Location = New System.Drawing.Point(31, 56)
        Me.cmbField.Name = "cmbField"
        Me.cmbField.Size = New System.Drawing.Size(352, 24)
        Me.cmbField.TabIndex = 0
        Me.cmbField.Text = "CHOOSE OPTION"
        '
        'txtSearchValue
        '
        Me.txtSearchValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSearchValue.Location = New System.Drawing.Point(31, 106)
        Me.txtSearchValue.Name = "txtSearchValue"
        Me.txtSearchValue.Size = New System.Drawing.Size(352, 22)
        Me.txtSearchValue.TabIndex = 1
        '
        'btnSubmitSearch
        '
        Me.btnSubmitSearch.BackColor = System.Drawing.Color.Green
        Me.btnSubmitSearch.Location = New System.Drawing.Point(31, 160)
        Me.btnSubmitSearch.Name = "btnSubmitSearch"
        Me.btnSubmitSearch.Size = New System.Drawing.Size(352, 40)
        Me.btnSubmitSearch.TabIndex = 2
        Me.btnSubmitSearch.Text = "SUBMIT(CTRL + S)"
        Me.btnSubmitSearch.UseVisualStyleBackColor = False
        '
        'SearchSubmissionsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(420, 248)
        Me.Controls.Add(Me.btnSubmitSearch)
        Me.Controls.Add(Me.txtSearchValue)
        Me.Controls.Add(Me.cmbField)
        Me.KeyPreview = True
        Me.Name = "SearchSubmissionsForm"
        Me.Text = "Harsh Mori, Slidely Task 2 - Search SubmissionsForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmbField As ComboBox
    Friend WithEvents txtSearchValue As TextBox
    Friend WithEvents btnSubmitSearch As Button
End Class
