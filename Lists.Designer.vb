<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Lists
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
        Dim GridLevelNode1 As DevExpress.XtraGrid.GridLevelNode = New DevExpress.XtraGrid.GridLevelNode()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Lists))
        Me.GridControl1 = New DevExpress.XtraGrid.GridControl()
        Me.LayoutView1 = New DevExpress.XtraGrid.Views.Layout.LayoutView()
        Me.LayoutViewCard1 = New DevExpress.XtraGrid.Views.Layout.LayoutViewCard()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridControl1
        '
        Me.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill
        GridLevelNode1.RelationName = "Level1"
        Me.GridControl1.LevelTree.Nodes.AddRange(New DevExpress.XtraGrid.GridLevelNode() {GridLevelNode1})
        Me.GridControl1.Location = New System.Drawing.Point(0, 0)
        Me.GridControl1.MainView = Me.LayoutView1
        Me.GridControl1.Name = "GridControl1"
        Me.GridControl1.Size = New System.Drawing.Size(664, 865)
        Me.GridControl1.TabIndex = 0
        Me.GridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.LayoutView1})
        '
        'LayoutView1
        '
        Me.LayoutView1.Appearance.CardCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.LayoutView1.Appearance.CardCaption.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.LayoutView1.Appearance.CardCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.LayoutView1.Appearance.CardCaption.ForeColor = System.Drawing.Color.Black
        Me.LayoutView1.Appearance.CardCaption.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.LayoutView1.Appearance.CardCaption.Options.UseBackColor = True
        Me.LayoutView1.Appearance.CardCaption.Options.UseBorderColor = True
        Me.LayoutView1.Appearance.CardCaption.Options.UseForeColor = True
        Me.LayoutView1.Appearance.FieldCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(242, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.LayoutView1.Appearance.FieldCaption.ForeColor = System.Drawing.Color.Black
        Me.LayoutView1.Appearance.FieldCaption.Options.UseBackColor = True
        Me.LayoutView1.Appearance.FieldCaption.Options.UseForeColor = True
        Me.LayoutView1.Appearance.FieldValue.BackColor = System.Drawing.Color.White
        Me.LayoutView1.Appearance.FieldValue.ForeColor = System.Drawing.Color.Black
        Me.LayoutView1.Appearance.FieldValue.Options.UseBackColor = True
        Me.LayoutView1.Appearance.FieldValue.Options.UseForeColor = True
        Me.LayoutView1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.LayoutView1.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(132, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.LayoutView1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(236, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.LayoutView1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black
        Me.LayoutView1.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical
        Me.LayoutView1.Appearance.FilterCloseButton.Options.UseBackColor = True
        Me.LayoutView1.Appearance.FilterCloseButton.Options.UseBorderColor = True
        Me.LayoutView1.Appearance.FilterCloseButton.Options.UseForeColor = True
        Me.LayoutView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(CType(CType(62, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(185, Byte), Integer))
        Me.LayoutView1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White
        Me.LayoutView1.Appearance.FilterPanel.Options.UseBackColor = True
        Me.LayoutView1.Appearance.FilterPanel.Options.UseForeColor = True
        Me.LayoutView1.Appearance.FocusedCardCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.LayoutView1.Appearance.FocusedCardCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(49, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(197, Byte), Integer))
        Me.LayoutView1.Appearance.FocusedCardCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutView1.Appearance.FocusedCardCaption.Options.UseBackColor = True
        Me.LayoutView1.Appearance.FocusedCardCaption.Options.UseBorderColor = True
        Me.LayoutView1.Appearance.FocusedCardCaption.Options.UseForeColor = True
        Me.LayoutView1.Appearance.HideSelectionCardCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.LayoutView1.Appearance.HideSelectionCardCaption.BorderColor = System.Drawing.Color.FromArgb(CType(CType(106, Byte), Integer), CType(CType(153, Byte), Integer), CType(CType(228, Byte), Integer))
        Me.LayoutView1.Appearance.HideSelectionCardCaption.ForeColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(251, Byte), Integer))
        Me.LayoutView1.Appearance.HideSelectionCardCaption.Options.UseBackColor = True
        Me.LayoutView1.Appearance.HideSelectionCardCaption.Options.UseBorderColor = True
        Me.LayoutView1.Appearance.HideSelectionCardCaption.Options.UseForeColor = True
        Me.LayoutView1.Appearance.SelectedCardCaption.BackColor = System.Drawing.Color.FromArgb(CType(CType(69, Byte), Integer), CType(CType(126, Byte), Integer), CType(CType(217, Byte), Integer))
        Me.LayoutView1.Appearance.SelectedCardCaption.ForeColor = System.Drawing.Color.White
        Me.LayoutView1.Appearance.SelectedCardCaption.Options.UseBackColor = True
        Me.LayoutView1.Appearance.SelectedCardCaption.Options.UseForeColor = True
        Me.LayoutView1.Appearance.SeparatorLine.BackColor = System.Drawing.Color.FromArgb(CType(CType(99, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(196, Byte), Integer))
        Me.LayoutView1.Appearance.SeparatorLine.Options.UseBackColor = True
        Me.LayoutView1.Appearance.ViewBackground.BackColor = System.Drawing.Color.White
        Me.LayoutView1.Appearance.ViewBackground.Options.UseBackColor = True
        Me.LayoutView1.GridControl = Me.GridControl1
        Me.LayoutView1.Name = "LayoutView1"
        Me.LayoutView1.OptionsFind.AlwaysVisible = True
        Me.LayoutView1.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent
        Me.LayoutView1.PaintStyleName = "Flat"
        Me.LayoutView1.TemplateCard = Me.LayoutViewCard1
        '
        'LayoutViewCard1
        '
        Me.LayoutViewCard1.ExpandButtonLocation = DevExpress.Utils.GroupElementLocation.AfterText
        Me.LayoutViewCard1.Name = "LayoutViewCard1"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.SimpleButton1.Appearance.Options.UseBackColor = True
        Me.SimpleButton1.Image = CType(resources.GetObject("SimpleButton1.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(490, 13)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(81, 23)
        Me.SimpleButton1.TabIndex = 1
        Me.SimpleButton1.Text = "Print Data"
        '
        'Lists
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(664, 865)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.GridControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Lists"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Lists"
        CType(Me.GridControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LayoutViewCard1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents GridControl1 As DevExpress.XtraGrid.GridControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LayoutView1 As DevExpress.XtraGrid.Views.Layout.LayoutView
    Friend WithEvents LayoutViewCard1 As DevExpress.XtraGrid.Views.Layout.LayoutViewCard
End Class
