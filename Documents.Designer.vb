<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Documents
    Inherits DevExpress.XtraBars.Ribbon.RibbonForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Documents))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ComboBoxEdit1 = New DevExpress.XtraEditors.ComboBoxEdit()
        Me.RichEditControl1 = New DevExpress.XtraRichEdit.RichEditControl()
        Me.RibbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
        Me.SwitchToSimpleViewItem1 = New DevExpress.XtraRichEdit.UI.SwitchToSimpleViewItem()
        Me.SwitchToDraftViewItem1 = New DevExpress.XtraRichEdit.UI.SwitchToDraftViewItem()
        Me.SwitchToPrintLayoutViewItem1 = New DevExpress.XtraRichEdit.UI.SwitchToPrintLayoutViewItem()
        Me.ToggleShowHorizontalRulerItem1 = New DevExpress.XtraRichEdit.UI.ToggleShowHorizontalRulerItem()
        Me.ToggleShowVerticalRulerItem1 = New DevExpress.XtraRichEdit.UI.ToggleShowVerticalRulerItem()
        Me.ZoomOutItem1 = New DevExpress.XtraRichEdit.UI.ZoomOutItem()
        Me.ZoomInItem1 = New DevExpress.XtraRichEdit.UI.ZoomInItem()
        Me.ViewRibbonPage1 = New DevExpress.XtraRichEdit.UI.ViewRibbonPage()
        Me.DocumentViewsRibbonPageGroup1 = New DevExpress.XtraRichEdit.UI.DocumentViewsRibbonPageGroup()
        Me.ShowRibbonPageGroup1 = New DevExpress.XtraRichEdit.UI.ShowRibbonPageGroup()
        Me.ZoomRibbonPageGroup1 = New DevExpress.XtraRichEdit.UI.ZoomRibbonPageGroup()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.RichEditBarController1 = New DevExpress.XtraRichEdit.UI.RichEditBarController()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RichEditBarController1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(379, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Document Type :"
        '
        'ComboBoxEdit1
        '
        Me.ComboBoxEdit1.Location = New System.Drawing.Point(472, 115)
        Me.ComboBoxEdit1.Name = "ComboBoxEdit1"
        Me.ComboBoxEdit1.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.ComboBoxEdit1.Properties.Items.AddRange(New Object() {"Surat Teguran", "Surat Peringatan", "Surat Dinas"})
        Me.ComboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor
        Me.ComboBoxEdit1.Size = New System.Drawing.Size(143, 20)
        Me.ComboBoxEdit1.TabIndex = 1
        '
        'RichEditControl1
        '
        Me.RichEditControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RichEditControl1.Location = New System.Drawing.Point(0, 144)
        Me.RichEditControl1.MenuManager = Me.RibbonControl1
        Me.RichEditControl1.Name = "RichEditControl1"
        Me.RichEditControl1.Options.Fields.UseCurrentCultureDateTimeFormat = False
        Me.RichEditControl1.Options.MailMerge.KeepLastParagraph = False
        Me.RichEditControl1.ReadOnly = True
        Me.RichEditControl1.Size = New System.Drawing.Size(895, 411)
        Me.RichEditControl1.TabIndex = 2
        Me.RichEditControl1.Text = "RichEditControl1"
        '
        'RibbonControl1
        '
        Me.RibbonControl1.ExpandCollapseItem.Id = 0
        Me.RibbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.RibbonControl1.ExpandCollapseItem, Me.SwitchToSimpleViewItem1, Me.SwitchToDraftViewItem1, Me.SwitchToPrintLayoutViewItem1, Me.ToggleShowHorizontalRulerItem1, Me.ToggleShowVerticalRulerItem1, Me.ZoomOutItem1, Me.ZoomInItem1})
        Me.RibbonControl1.Location = New System.Drawing.Point(0, 0)
        Me.RibbonControl1.MaxItemId = 8
        Me.RibbonControl1.Name = "RibbonControl1"
        Me.RibbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.ViewRibbonPage1})
        Me.RibbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2010
        Me.RibbonControl1.Size = New System.Drawing.Size(895, 144)
        '
        'SwitchToSimpleViewItem1
        '
        Me.SwitchToSimpleViewItem1.Id = 1
        Me.SwitchToSimpleViewItem1.Name = "SwitchToSimpleViewItem1"
        '
        'SwitchToDraftViewItem1
        '
        Me.SwitchToDraftViewItem1.Id = 2
        Me.SwitchToDraftViewItem1.Name = "SwitchToDraftViewItem1"
        '
        'SwitchToPrintLayoutViewItem1
        '
        Me.SwitchToPrintLayoutViewItem1.Id = 3
        Me.SwitchToPrintLayoutViewItem1.Name = "SwitchToPrintLayoutViewItem1"
        '
        'ToggleShowHorizontalRulerItem1
        '
        Me.ToggleShowHorizontalRulerItem1.Id = 4
        Me.ToggleShowHorizontalRulerItem1.Name = "ToggleShowHorizontalRulerItem1"
        '
        'ToggleShowVerticalRulerItem1
        '
        Me.ToggleShowVerticalRulerItem1.Id = 5
        Me.ToggleShowVerticalRulerItem1.Name = "ToggleShowVerticalRulerItem1"
        '
        'ZoomOutItem1
        '
        Me.ZoomOutItem1.Id = 6
        Me.ZoomOutItem1.Name = "ZoomOutItem1"
        '
        'ZoomInItem1
        '
        Me.ZoomInItem1.Id = 7
        Me.ZoomInItem1.Name = "ZoomInItem1"
        '
        'ViewRibbonPage1
        '
        Me.ViewRibbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.DocumentViewsRibbonPageGroup1, Me.ShowRibbonPageGroup1, Me.ZoomRibbonPageGroup1})
        Me.ViewRibbonPage1.Name = "ViewRibbonPage1"
        '
        'DocumentViewsRibbonPageGroup1
        '
        Me.DocumentViewsRibbonPageGroup1.ItemLinks.Add(Me.SwitchToSimpleViewItem1)
        Me.DocumentViewsRibbonPageGroup1.ItemLinks.Add(Me.SwitchToDraftViewItem1)
        Me.DocumentViewsRibbonPageGroup1.ItemLinks.Add(Me.SwitchToPrintLayoutViewItem1)
        Me.DocumentViewsRibbonPageGroup1.Name = "DocumentViewsRibbonPageGroup1"
        '
        'ShowRibbonPageGroup1
        '
        Me.ShowRibbonPageGroup1.ItemLinks.Add(Me.ToggleShowHorizontalRulerItem1)
        Me.ShowRibbonPageGroup1.ItemLinks.Add(Me.ToggleShowVerticalRulerItem1)
        Me.ShowRibbonPageGroup1.Name = "ShowRibbonPageGroup1"
        '
        'ZoomRibbonPageGroup1
        '
        Me.ZoomRibbonPageGroup1.ItemLinks.Add(Me.ZoomOutItem1)
        Me.ZoomRibbonPageGroup1.ItemLinks.Add(Me.ZoomInItem1)
        Me.ZoomRibbonPageGroup1.Name = "ZoomRibbonPageGroup1"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Image = CType(resources.GetObject("SimpleButton1.Image"), System.Drawing.Image)
        Me.SimpleButton1.Location = New System.Drawing.Point(621, 114)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(58, 23)
        Me.SimpleButton1.TabIndex = 3
        Me.SimpleButton1.Text = "View"
        '
        'RichEditBarController1
        '
        Me.RichEditBarController1.BarItems.Add(Me.SwitchToSimpleViewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SwitchToDraftViewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.SwitchToPrintLayoutViewItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleShowHorizontalRulerItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ToggleShowVerticalRulerItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ZoomOutItem1)
        Me.RichEditBarController1.BarItems.Add(Me.ZoomInItem1)
        Me.RichEditBarController1.Control = Me.RichEditControl1
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Image = CType(resources.GetObject("SimpleButton2.Image"), System.Drawing.Image)
        Me.SimpleButton2.Location = New System.Drawing.Point(685, 113)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(75, 23)
        Me.SimpleButton2.TabIndex = 5
        Me.SimpleButton2.Text = "Changes"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(685, 66)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl1.TabIndex = 6
        Me.LabelControl1.Text = "LabelControl1"
        Me.LabelControl1.Visible = False
        '
        'Documents
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 555)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.RichEditControl1)
        Me.Controls.Add(Me.ComboBoxEdit1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RibbonControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Documents"
        Me.Ribbon = Me.RibbonControl1
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Documents"
        CType(Me.ComboBoxEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RibbonControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RichEditBarController1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBoxEdit1 As DevExpress.XtraEditors.ComboBoxEdit
    Friend WithEvents RichEditControl1 As DevExpress.XtraRichEdit.RichEditControl
    Friend WithEvents RibbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl
    Friend WithEvents SwitchToSimpleViewItem1 As DevExpress.XtraRichEdit.UI.SwitchToSimpleViewItem
    Friend WithEvents SwitchToDraftViewItem1 As DevExpress.XtraRichEdit.UI.SwitchToDraftViewItem
    Friend WithEvents SwitchToPrintLayoutViewItem1 As DevExpress.XtraRichEdit.UI.SwitchToPrintLayoutViewItem
    Friend WithEvents ToggleShowHorizontalRulerItem1 As DevExpress.XtraRichEdit.UI.ToggleShowHorizontalRulerItem
    Friend WithEvents ToggleShowVerticalRulerItem1 As DevExpress.XtraRichEdit.UI.ToggleShowVerticalRulerItem
    Friend WithEvents ZoomOutItem1 As DevExpress.XtraRichEdit.UI.ZoomOutItem
    Friend WithEvents ZoomInItem1 As DevExpress.XtraRichEdit.UI.ZoomInItem
    Friend WithEvents ViewRibbonPage1 As DevExpress.XtraRichEdit.UI.ViewRibbonPage
    Friend WithEvents DocumentViewsRibbonPageGroup1 As DevExpress.XtraRichEdit.UI.DocumentViewsRibbonPageGroup
    Friend WithEvents ShowRibbonPageGroup1 As DevExpress.XtraRichEdit.UI.ShowRibbonPageGroup
    Friend WithEvents ZoomRibbonPageGroup1 As DevExpress.XtraRichEdit.UI.ZoomRibbonPageGroup
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents RichEditBarController1 As DevExpress.XtraRichEdit.UI.RichEditBarController
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
End Class
