using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

public partial class ASPxGridRibbon : System.Web.UI.UserControl, IPostBackEventHandler {
	private RibbonStateController state;
	protected ASPxGridView ChildGrid;
	private ASPxGridViewExporter gridExporter;

	public bool EnableXlsExport { get; set; }

	protected override void OnInit(EventArgs e) {		
		InitializeUserControl();
		SetGridState();
		base.OnInit(e);
	}

	protected override void OnLoad(EventArgs e) {
		UpdateRibbonItems();
		base.OnLoad(e);
	}

	void ChildGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e) {
		HandleGridCallback(e.Parameters);
	}


	private void InitializeUserControl(){
		ChildGrid = ((GridViewTitleTemplateContainer)this.NamingContainer).Grid;
		ChildGrid.CustomCallback += ChildGrid_CustomCallback;
		state = new RibbonStateController(ASPxHiddenField1);

        if (EnableXlsExport)
            SetupASPxGridViewExporter();

		if (!IsPostBack)
			InitializeRibbonStateController();

		ASPxRibbon1.ClientSideEvents.Init = String.Format("function (s, e) {{ s.cpGrid = {0} }}",
			String.IsNullOrEmpty(ChildGrid.ClientInstanceName) ? ChildGrid.ClientID : ChildGrid.ClientInstanceName);
	}

	private void SetGridState() {
		SetGridInitialState();

		ChildGrid.SettingsEditing.Mode = state.EditingMode;
		ChildGrid.SettingsPager.PageSize = state.PageSize;
		ChildGrid.Settings.ShowGroupPanel = state.ShowGroupPanel;
		ChildGrid.SettingsBehavior.AllowSort = state.AllowSort;
		ChildGrid.Settings.ShowFilterRow = state.ShowFilterRow;
	}

	private void InitializeRibbonStateController() {
		state.EditingMode = GridViewEditingMode.PopupEditForm;
		state.PageSize = 10;
		state.ShowGroupPanel = false;
		state.AllowSort = true;
		state.ShowFilterRow = false;
	}

	private void UpdateRibbonItems() {
		RibbonSpinEditItem pageNumber = FindRibbonItem<RibbonSpinEditItem>(1, 0, "PageNumber");
		pageNumber.PropertiesSpinEdit.MaxValue = ChildGrid.PageCount;
		pageNumber.PropertiesSpinEdit.MinValue = 1;
		pageNumber.Value = ChildGrid.PageIndex + 1;

		FindRibbonItem<RibbonComboBoxItem>(1, 0, "PageSize").Value = ChildGrid.SettingsPager.PageSize;
		FindRibbonItem<RibbonToggleButtonItem>(0, 2, "BatchEditionMode").Checked = ChildGrid.SettingsEditing.Mode == GridViewEditingMode.Batch;
		FindRibbonItem<RibbonToggleButtonItem>(2, 0, "Grouping").Checked = ChildGrid.Settings.ShowGroupPanel;
		FindRibbonItem<RibbonToggleButtonItem>(2, 0, "Sorting").Checked = ChildGrid.SettingsBehavior.AllowSort;
		FindRibbonItem<RibbonToggleButtonItem>(2, 0, "Filtering").Checked = ChildGrid.Settings.ShowFilterRow;

		state.UpdateHiddenField();
	}

	private T FindRibbonItem<T>(int tabIndex, int groupIndex, string name) where T : RibbonItemBase {
		return ASPxRibbon1.Tabs[tabIndex].Groups[groupIndex].Items.FindByName(name) as T;
	}

	public void RaisePostBackEvent(string eventArgument) {
		switch (eventArgument) {
			case "EditingMode":
				state.EditingMode = ChildGrid.SettingsEditing.Mode =
					ChildGrid.SettingsEditing.Mode == GridViewEditingMode.PopupEditForm ? GridViewEditingMode.Batch : GridViewEditingMode.PopupEditForm;
				break;
			case "Export":
				gridExporter.WriteXlsxToResponse();
				break;
		}
	}
	private void HandleGridCallback(string parameter) {
		string[] parameters = parameter.Split(':');
		string target = parameters[0];
		string eventArgument = parameters[1];
		bool eventArgumentIsTrue = eventArgument == "true";
		switch (target) {
			case "PageSize":
				state.PageSize = ChildGrid.SettingsPager.PageSize = Convert.ToInt32(eventArgument);
				break;
			case "Grouping":
				ReadOnlyCollection<GridViewDataColumn> groupedColumns = ChildGrid.GetGroupedColumns();
				foreach (GridViewDataColumn column in groupedColumns)
					ChildGrid.UnGroup(column);
				state.ShowGroupPanel = ChildGrid.Settings.ShowGroupPanel = eventArgumentIsTrue;
				break;
			case "Sorting":
				ChildGrid.ClearSort();
				state.AllowSort = ChildGrid.SettingsBehavior.AllowSort = eventArgumentIsTrue;
				break;
			case "Filtering":
				ChildGrid.FilterExpression = String.Empty;
				state.ShowFilterRow = ChildGrid.Settings.ShowFilterRow = eventArgumentIsTrue;
				break;
		}
		ChildGrid.DataBind();
	}


	private void SetGridInitialState() {
		ChildGrid.SettingsBehavior.AllowFocusedRow = true;
		ChildGrid.SettingsPager.Visible = false;
	}

	private void SetupASPxGridViewExporter() {
		gridExporter = new ASPxGridViewExporter {
			ID = "ASPxGridViewExporter",
			GridViewID = ChildGrid.ID
		};
        PlaceHolder1.Controls.Add(gridExporter);
	}

}