Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web

Partial Public Class ASPxGridRibbon
	Inherits System.Web.UI.UserControl
	Implements IPostBackEventHandler
	Private state As RibbonStateController
	Protected ChildGrid As ASPxGridView
	Private gridExporter As ASPxGridViewExporter

	Private privateEnableXlsExport As Boolean
	Public Property EnableXlsExport() As Boolean
		Get
			Return privateEnableXlsExport
		End Get
		Set(ByVal value As Boolean)
			privateEnableXlsExport = value
		End Set
	End Property

	Protected Overrides Sub OnInit(ByVal e As EventArgs)
		InitializeUserControl()
		SetGridState()
		MyBase.OnInit(e)
	End Sub

	Protected Overrides Sub OnLoad(ByVal e As EventArgs)
		UpdateRibbonItems()
		MyBase.OnLoad(e)
	End Sub

	Private Sub ChildGrid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs)
		HandleGridCallback(e.Parameters)
	End Sub


	Private Sub InitializeUserControl()
		ChildGrid = (CType(Me.NamingContainer, GridViewTitleTemplateContainer)).Grid
		AddHandler ChildGrid.CustomCallback, AddressOf ChildGrid_CustomCallback
		state = New RibbonStateController(ASPxHiddenField1)

		If EnableXlsExport Then
			SetupASPxGridViewExporter()
		End If

		If (Not IsPostBack) Then
			InitializeRibbonStateController()
		End If

		If String.IsNullOrEmpty(ChildGrid.ClientInstanceName) Then
			ASPxRibbon1.ClientSideEvents.Init = String.Format("function (s, e) {{ s.cpGrid = {0} }}",ChildGrid.ClientID)
		Else
			ASPxRibbon1.ClientSideEvents.Init = String.Format("function (s, e) {{ s.cpGrid = {0} }}",ChildGrid.ClientInstanceName)
		End If
	End Sub

	Private Sub SetGridState()
		SetGridInitialState()

		ChildGrid.SettingsEditing.Mode = state.EditingMode
		ChildGrid.SettingsPager.PageSize = state.PageSize
		ChildGrid.Settings.ShowGroupPanel = state.ShowGroupPanel
		ChildGrid.SettingsBehavior.AllowSort = state.AllowSort
		ChildGrid.Settings.ShowFilterRow = state.ShowFilterRow
	End Sub

	Private Sub InitializeRibbonStateController()
		state.EditingMode = GridViewEditingMode.PopupEditForm
		state.PageSize = 10
		state.ShowGroupPanel = False
		state.AllowSort = True
		state.ShowFilterRow = False
	End Sub

	Private Sub UpdateRibbonItems()
		Dim pageNumber As RibbonSpinEditItem = FindRibbonItem(Of RibbonSpinEditItem)(1, 0, "PageNumber")
		pageNumber.PropertiesSpinEdit.MaxValue = ChildGrid.PageCount
		pageNumber.PropertiesSpinEdit.MinValue = 1
		pageNumber.Value = ChildGrid.PageIndex + 1

		FindRibbonItem(Of RibbonComboBoxItem)(1, 0, "PageSize").Value = ChildGrid.SettingsPager.PageSize
		FindRibbonItem(Of RibbonToggleButtonItem)(0, 2, "BatchEditionMode").Checked = ChildGrid.SettingsEditing.Mode = GridViewEditingMode.Batch
		FindRibbonItem(Of RibbonToggleButtonItem)(2, 0, "Grouping").Checked = ChildGrid.Settings.ShowGroupPanel
		FindRibbonItem(Of RibbonToggleButtonItem)(2, 0, "Sorting").Checked = ChildGrid.SettingsBehavior.AllowSort
		FindRibbonItem(Of RibbonToggleButtonItem)(2, 0, "Filtering").Checked = ChildGrid.Settings.ShowFilterRow

		state.UpdateHiddenField()
	End Sub

	Private Function FindRibbonItem(Of T As RibbonItemBase)(ByVal tabIndex As Integer, ByVal groupIndex As Integer, ByVal name As String) As T
		Return TryCast(ASPxRibbon1.Tabs(tabIndex).Groups(groupIndex).Items.FindByName(name), T)
	End Function

	Public Sub RaisePostBackEvent(ByVal eventArgument As String) Implements IPostBackEventHandler.RaisePostBackEvent
		Select Case eventArgument
			Case "EditingMode"
				If ChildGrid.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm Then
					ChildGrid.SettingsEditing.Mode = GridViewEditingMode.Batch
					state.EditingMode = ChildGrid.SettingsEditing.Mode
				Else
					ChildGrid.SettingsEditing.Mode = GridViewEditingMode.PopupEditForm
					state.EditingMode = ChildGrid.SettingsEditing.Mode
				End If
			Case "Export"
				gridExporter.WriteXlsxToResponse()
		End Select
	End Sub
	Private Sub HandleGridCallback(ByVal parameter As String)
		Dim parameters() As String = parameter.Split(":"c)
		Dim target As String = parameters(0)
		Dim eventArgument As String = parameters(1)
		Dim eventArgumentIsTrue As Boolean = eventArgument = "true"
		Select Case target
			Case "PageSize"
				ChildGrid.SettingsPager.PageSize = Convert.ToInt32(eventArgument)
				state.PageSize = ChildGrid.SettingsPager.PageSize
			Case "Grouping"
				Dim groupedColumns As ReadOnlyCollection(Of GridViewDataColumn) = ChildGrid.GetGroupedColumns()
				For Each column As GridViewDataColumn In groupedColumns
					ChildGrid.UnGroup(column)
				Next column
				ChildGrid.Settings.ShowGroupPanel = eventArgumentIsTrue
				state.ShowGroupPanel = ChildGrid.Settings.ShowGroupPanel
			Case "Sorting"
				ChildGrid.ClearSort()
				ChildGrid.SettingsBehavior.AllowSort = eventArgumentIsTrue
				state.AllowSort = ChildGrid.SettingsBehavior.AllowSort
			Case "Filtering"
				ChildGrid.FilterExpression = String.Empty
				ChildGrid.Settings.ShowFilterRow = eventArgumentIsTrue
				state.ShowFilterRow = ChildGrid.Settings.ShowFilterRow
		End Select
		ChildGrid.DataBind()
	End Sub


	Private Sub SetGridInitialState()
		ChildGrid.SettingsBehavior.AllowFocusedRow = True
		ChildGrid.SettingsPager.Visible = False
	End Sub

	Private Sub SetupASPxGridViewExporter()
		gridExporter = New ASPxGridViewExporter With {.ID = "ASPxGridViewExporter", .GridViewID = ChildGrid.ID}
		PlaceHolder1.Controls.Add(gridExporter)
	End Sub

End Class