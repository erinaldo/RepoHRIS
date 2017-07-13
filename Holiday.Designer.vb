<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Holiday
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Holiday))
        Me.Label2 = New System.Windows.Forms.Label()
        Me.date1 = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.date2 = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtdays = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.textreason = New System.Windows.Forms.RichTextBox()
        Me.btnProcess = New DevExpress.XtraEditors.SimpleButton()
        Me.GridControl6 = New DevExpress.XtraGrid.GridControl()
        Me.GridView6 = New DevExpress.XtraGrid.Views.Grid.GridView()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.GridControl6, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(3, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Holiday Start Date :"
        '
        'date1
        '
        Me.date1.Location = New System.Drawing.Point(111, 6)
        Me.date1.Name = "date1"
        Me.date1.Size = New System.Drawing.Size(206, 20)
        Me.date1.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(323, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "To :"
        '
        'date2
        '
        Me.date2.Location = New System.Drawing.Point(350, 5)
        Me.date2.Name = "date2"
        Me.date2.Size = New System.Drawing.Size(206, 20)
        Me.date2.TabIndex = 20
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(63, 37)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(37, 13)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Total :"
        '
        'txtdays
        '
        Me.txtdays.Location = New System.Drawing.Point(110, 35)
        Me.txtdays.Multiline = True
        Me.txtdays.Name = "txtdays"
        Me.txtdays.ReadOnly = True
        Me.txtdays.Size = New System.Drawing.Size(40, 21)
        Me.txtdays.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(153, 38)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(31, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Days"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(2, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "Holiday's Description :"
        '
        'textreason
        '
        Me.textreason.Location = New System.Drawing.Point(110, 64)
        Me.textreason.Name = "textreason"
        Me.textreason.Size = New System.Drawing.Size(353, 71)
        Me.textreason.TabIndex = 27
        Me.textreason.Text = ""
        '
        'btnProcess
        '
        Me.btnProcess.Image = CType(resources.GetObject("btnProcess.Image"), System.Drawing.Image)
        Me.btnProcess.Location = New System.Drawing.Point(468, 110)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(88, 25)
        Me.btnProcess.TabIndex = 28
        Me.btnProcess.Text = "Add Holidays"
        '
        'GridControl6
        '
        Me.GridControl6.Location = New System.Drawing.Point(2, 207)
        Me.GridControl6.MainView = Me.GridView6
        Me.GridControl6.Name = "GridControl6"
        Me.GridControl6.Size = New System.Drawing.Size(555, 315)
        Me.GridControl6.TabIndex = 29
        Me.GridControl6.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.GridView6})
        '
        'GridView6
        '
        Me.GridView6.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView6.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView6.Appearance.ColumnFilterButton.Options.UseBackColor = True
        Me.GridView6.Appearance.ColumnFilterButton.Options.UseBorderColor = True
        Me.GridView6.Appearance.ColumnFilterButton.Options.UseForeColor = True
        Me.GridView6.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView6.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(154, Byte), Integer), CType(CType(190, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.GridView6.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(CType(CType(247, Byte), Integer), CType(CType(251, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView6.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView6.Appearance.ColumnFilterButtonActive.Options.UseBackColor = True
        Me.GridView6.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = True
        Me.GridView6.Appearance.ColumnFilterButtonActive.Options.UseForeColor = True
        Me.GridView6.Appearance.Empty.BackColor = System.Drawing.Color.White
        Me.GridView6.Appearance.Empty.Options.UseBackColor = True
        Me.GridView6.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.EvenRow.Options.UseBackColor = True
        Me.GridView6.Appearance.EvenRow.Options.UseForeColor = True
        Me.GridView6.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView6.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView6.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.GridView6.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.GridView6.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.GridView6.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.GridView6.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.GridView6.Appearance.FilterPanel.Options.UseBackColor = True
        Me.GridView6.Appearance.FilterPanel.Options.UseForeColor = True
        Me.GridView6.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(59, Byte), Integer), CType(CType(97, Byte), Integer), CType(CType(156, Byte), Integer))
        Me.GridView6.Appearance.FixedLine.Options.UseBackColor = True
        Me.GridView6.Appearance.FocusedCell.BackColor = System.Drawing.Color.White
        Me.GridView6.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.FocusedCell.Options.UseBackColor = True
        Me.GridView6.Appearance.FocusedCell.Options.UseForeColor = True
        Me.GridView6.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.GridView6.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White
        Me.GridView6.Appearance.FocusedRow.Options.UseBackColor = True
        Me.GridView6.Appearance.FocusedRow.Options.UseForeColor = True
        Me.GridView6.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView6.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView6.Appearance.FooterPanel.Options.UseBackColor = True
        Me.GridView6.Appearance.FooterPanel.Options.UseBorderColor = True
        Me.GridView6.Appearance.FooterPanel.Options.UseForeColor = True
        Me.GridView6.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView6.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView6.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.GroupButton.Options.UseBackColor = True
        Me.GridView6.Appearance.GroupButton.Options.UseBorderColor = True
        Me.GridView6.Appearance.GroupButton.Options.UseForeColor = True
        Me.GridView6.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView6.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView6.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.GroupFooter.Options.UseBackColor = True
        Me.GridView6.Appearance.GroupFooter.Options.UseBorderColor = True
        Me.GridView6.Appearance.GroupFooter.Options.UseForeColor = True
        Me.GridView6.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.GridView6.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.GroupPanel.Options.UseBackColor = True
        Me.GridView6.Appearance.GroupPanel.Options.UseForeColor = True
        Me.GridView6.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView6.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(216, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.GridView6.Appearance.GroupRow.Font = New System.Drawing.Font("Tahoma", 8.0!, System.Drawing.FontStyle.Bold)
        Me.GridView6.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.GroupRow.Options.UseBackColor = True
        Me.GridView6.Appearance.GroupRow.Options.UseBorderColor = True
        Me.GridView6.Appearance.GroupRow.Options.UseFont = True
        Me.GridView6.Appearance.GroupRow.Options.UseForeColor = True
        Me.GridView6.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView6.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.GridView6.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.GridView6.Appearance.HeaderPanel.Options.UseBackColor = True
        Me.GridView6.Appearance.HeaderPanel.Options.UseBorderColor = True
        Me.GridView6.Appearance.HeaderPanel.Options.UseForeColor = True
        Me.GridView6.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.GridView6.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.GridView6.Appearance.HideSelectionRow.Options.UseBackColor = True
        Me.GridView6.Appearance.HideSelectionRow.Options.UseForeColor = True
        Me.GridView6.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.GridView6.Appearance.HorzLine.Options.UseBackColor = True
        Me.GridView6.Appearance.OddRow.BackColor = System.Drawing.Color.White
        Me.GridView6.Appearance.OddRow.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.OddRow.Options.UseBackColor = True
        Me.GridView6.Appearance.OddRow.Options.UseForeColor = True
        Me.GridView6.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(CType(CType(249, Byte), Integer), CType(CType(252, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.GridView6.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(88, Byte), Integer), CType(CType(129, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.GridView6.Appearance.Preview.Options.UseBackColor = True
        Me.GridView6.Appearance.Preview.Options.UseForeColor = True
        Me.GridView6.Appearance.Row.BackColor = System.Drawing.Color.White
        Me.GridView6.Appearance.Row.ForeColor = System.Drawing.Color.Black
        Me.GridView6.Appearance.Row.Options.UseBackColor = True
        Me.GridView6.Appearance.Row.Options.UseForeColor = True
        Me.GridView6.Appearance.RowSeparator.BackColor = System.Drawing.Color.White
        Me.GridView6.Appearance.RowSeparator.Options.UseBackColor = True
        Me.GridView6.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.GridView6.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White
        Me.GridView6.Appearance.SelectedRow.Options.UseBackColor = True
        Me.GridView6.Appearance.SelectedRow.Options.UseForeColor = True
        Me.GridView6.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.GridView6.Appearance.VertLine.Options.UseBackColor = True
        Me.GridView6.GridControl = Me.GridControl6
        Me.GridView6.Name = "GridView6"
        Me.GridView6.OptionsBehavior.ReadOnly = True
        Me.GridView6.OptionsView.ColumnAutoWidth = False
        Me.GridView6.OptionsView.EnableAppearanceEvenRow = True
        Me.GridView6.OptionsView.EnableAppearanceOddRow = True
        Me.GridView6.PaintStyleName = "Flat"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Image = CType(resources.GetObject("SimpleButton1.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(485, 168)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(56, 23)
        Me.SimpleButton1.TabIndex = 30
        Me.SimpleButton1.Text = "Show"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Location = New System.Drawing.Point(42, 171)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(206, 20)
        Me.DateTimePicker1.TabIndex = 31
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Location = New System.Drawing.Point(273, 171)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(206, 20)
        Me.DateTimePicker2.TabIndex = 32
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(8, 174)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = "Start"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(250, 175)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(20, 13)
        Me.Label6.TabIndex = 34
        Me.Label6.Text = "To"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(-3, 155)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(577, 13)
        Me.Label7.TabIndex = 35
        Me.Label7.Text = "---------------------------------------------------------------------------------" &
    "--------------------------------------------------------------------------------" &
    "-----------------------------"
        '
        'Holiday
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(562, 523)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.GridControl6)
        Me.Controls.Add(Me.btnProcess)
        Me.Controls.Add(Me.textreason)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtdays)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.date2)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.date1)
        Me.Controls.Add(Me.Label2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(578, 562)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(578, 562)
        Me.Name = "Holiday"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Holiday Master"
        CType(Me.GridControl6, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridView6, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label2 As Label
    Friend WithEvents date1 As DateTimePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents date2 As DateTimePicker
    Friend WithEvents Label8 As Label
    Friend WithEvents txtdays As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents textreason As RichTextBox
    Friend WithEvents btnProcess As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents GridControl6 As DevExpress.XtraGrid.GridControl
    Friend WithEvents GridView6 As DevExpress.XtraGrid.Views.Grid.GridView
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents DateTimePicker1 As DateTimePicker
    Friend WithEvents DateTimePicker2 As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
