<%@ Control Language="vb" AutoEventWireup="true" CodeFile="ASPxGridRibbon.ascx.vb" Inherits="ASPxGridRibbon" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRibbon" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<div>
	<script type="text/javascript">
		function RibbonCommand(s, e) {
			switch (e.item.name) {
				case "AddRow":
					s.cpGrid.AddNewRow();
					break;
				case "DeleteRow":
					s.cpGrid.DeleteRow(s.cpGrid.GetFocusedRowIndex());
					break;
				case "EditRow":
					s.cpGrid.StartEditRow(s.cpGrid.GetFocusedRowIndex());
					break;
				case "Refresh":
					s.cpGrid.Refresh();
					break;
				case "Export":
						<%=Page.ClientScript.GetPostBackEventReference(Me, "Export")%>
					break;
				case "PreviousPage":
					s.SetItemValueByName("PageNumber", s.GetItemValueByName("PageNumber") - 1);
					s.cpGrid.PrevPage();
					break;
				case "PageNumber":
					s.cpGrid.GotoPage(e.parameter - 1);
					break;
				case "PageSize":
					s.cpGrid.PerformCallback("PageSize:" + e.parameter);
					break;
				case "NextPage":
					s.SetItemValueByName("PageNumber", s.GetItemValueByName("PageNumber") + 1);
					s.cpGrid.NextPage();
					break;
				case "AddFilter":
					s.cpGrid.ShowFilterControl();
					break;
				case "ClearFilter":
					s.cpGrid.ClearFilter();
					break;
				case "BatchEditionMode":
						<%=Page.ClientScript.GetPostBackEventReference(Me, "EditingMode")%>
					break;
				case "DiscardChanges":
					s.cpGrid.CancelEdit();
					break;
				case "SaveChanges":
					s.cpGrid.UpdateEdit();
					break;
				case "Grouping":
					s.cpGrid.PerformCallback("Grouping:" + e.parameter)
					break;
				case "Sorting":
					s.cpGrid.PerformCallback("Sorting:" + e.parameter)
					break;
				case "Filtering":
					s.cpGrid.PerformCallback("Filtering:" + e.parameter)
					break;
			}
		}
	</script>
	<dx:ASPxRibbon ID="ASPxRibbon1" runat="server" Theme="Default" ShowFileTab="False" ActiveTabIndex="0" ViewStateMode="Disabled">
		<ClientSideEvents CommandExecuted="RibbonCommand" />
		<Tabs>
			<dx:RibbonTab Name="EditTab" Text="Edit">
				<Groups>
					<dx:RibbonGroup Name="DataGroup" Text="Data">
						<Items>
							<dx:RibbonButtonItem Name="AddRow" Size="Large" Text="Add">
								<LargeImage AlternateText="Add Row" IconID="actions_addfile_32x32">
								</LargeImage>
								<SmallImage IconID="actions_addfile_16x16" AlternateText="Add Row"></SmallImage>
							</dx:RibbonButtonItem>
							<dx:RibbonButtonItem Name="DeleteRow" Size="Large" Text="Delete">
								<LargeImage AlternateText="Delete Row" IconID="actions_removeitem_32x32">
								</LargeImage>
								<SmallImage IconID="actions_removeitem_16x16" AlternateText="Delete Row"></SmallImage>
							</dx:RibbonButtonItem>
							<dx:RibbonButtonItem Name="EditRow" Size="Large" Text="Edit">
								<LargeImage AlternateText="Edit Row" IconID="edit_edit_32x32">
								</LargeImage>
								<SmallImage AlternateText="Edit Row" IconID="edit_edit_16x16">
								</SmallImage>
							</dx:RibbonButtonItem>
						</Items>
					</dx:RibbonGroup>
					<dx:RibbonGroup Name="RefreshGroup" Text="Refresh">
						<Items>
							<dx:RibbonButtonItem Name="Refresh" Size="Large" Text="Refresh">
								<LargeImage AlternateText="Refresh" IconID="actions_refresh_32x32">
								</LargeImage>
								<SmallImage IconID="actions_refresh_16x16" AlternateText="Refresh"></SmallImage>
							</dx:RibbonButtonItem>
							<dx:RibbonButtonItem Name="Export" Size="Large" Text="Export">
								<LargeImage AlternateText="Export" IconID="export_exporttoxls_32x32">
								</LargeImage>
								<SmallImage AlternateText="Export" IconID="export_exporttoxls_16x16">
								</SmallImage>
							</dx:RibbonButtonItem>
						</Items>
					</dx:RibbonGroup>
					<dx:RibbonGroup Name="BatchEditingGroup" Text="Batch Editing">
						<Items>
							<dx:RibbonToggleButtonItem Name="BatchEditionMode" Text="Batch Mode" Size="Large">
								<LargeImage AlternateText="Batch Mode" IconID="support_example_32x32">
								</LargeImage>
								<SmallImage AlternateText="Batch Mode" IconID="support_example_16x16">
								</SmallImage>
							</dx:RibbonToggleButtonItem>
							<dx:RibbonButtonItem Name="DiscardChanges" Size="Large" Text="Discard">
								<LargeImage AlternateText="Discard" IconID="edit_delete_32x32">
								</LargeImage>
								<SmallImage AlternateText="Discard" IconID="edit_delete_16x16">
								</SmallImage>
							</dx:RibbonButtonItem>
							<dx:RibbonButtonItem Name="SaveChanges" Size="Large" Text="Save">
								<LargeImage AlternateText="Save" IconID="save_saveto_32x32">
								</LargeImage>
								<SmallImage AlternateText="Save" IconID="save_saveto_16x16">
								</SmallImage>
							</dx:RibbonButtonItem>
						</Items>
					</dx:RibbonGroup>
				</Groups>
			</dx:RibbonTab>
			<dx:RibbonTab Name="ViewTab" Text="View">
				<Groups>
					<dx:RibbonGroup Name="NavigationGroup" Text="Navigation">
						<Items>
							<dx:RibbonButtonItem Name="PreviousPage" Size="Large" Text="Previous Page">
								<LargeImage AlternateText="Previous Page" IconID="navigation_backward_32x32">
								</LargeImage>
								<SmallImage IconID="navigation_backward_16x16" AlternateText="Previous Page"></SmallImage>
							</dx:RibbonButtonItem>
							<dx:RibbonSpinEditItem Name="PageNumber" Text="Page">
								<ItemStyle Width="90px" />
								<PropertiesSpinEdit DisplayFormatString="g" Width="50px" AllowNull="false" AllowUserInput="true" NumberType="Integer">
									<ButtonStyle HorizontalAlign="Left"></ButtonStyle>
								</PropertiesSpinEdit>
							</dx:RibbonSpinEditItem>
							<dx:RibbonComboBoxItem Name="PageSize" Text="Size">
								<ItemStyle Width="90px" />
								<Items>
									<dx:ListEditItem Text="10" Value="10" />
									<dx:ListEditItem Text="50" Value="50" />
									<dx:ListEditItem Text="100" Value="100" />
									<dx:ListEditItem Text="500" Value="500" />
								</Items>
								<PropertiesComboBox Width="50px">
								</PropertiesComboBox>
							</dx:RibbonComboBoxItem>
							<dx:RibbonButtonItem Name="NextPage" Size="Large" Text="Next Page">
								<LargeImage AlternateText="Next Page" IconID="navigation_forward_32x32">
								</LargeImage>
								<SmallImage IconID="navigation_forward_16x16" AlternateText="Next Page"></SmallImage>
							</dx:RibbonButtonItem>
						</Items>
					</dx:RibbonGroup>
					<dx:RibbonGroup Name="FilterGroup" Text="Filter">
						<Items>
							<dx:RibbonButtonItem Name="AddFilter" Size="Large" Text="Add Filter">
								<LargeImage AlternateText="Add Filter" IconID="filter_filter_32x32">
								</LargeImage>
								<SmallImage IconID="filter_filter_16x16" AlternateText="Add Filter"></SmallImage>
							</dx:RibbonButtonItem>
							<dx:RibbonButtonItem Name="ClearFilter" Size="Large" Text="Clear Filter">
								<LargeImage AlternateText="Clear Filter" IconID="filter_clearfilter_32x32">
								</LargeImage>
								<SmallImage AlternateText="Clear Filter" IconID="filter_clearfilter_16x16">
								</SmallImage>
							</dx:RibbonButtonItem>
						</Items>
					</dx:RibbonGroup>
				</Groups>
			</dx:RibbonTab>
			<dx:RibbonTab Name="SettingsTab" Text="Settings">
				<Groups>
					<dx:RibbonGroup Text="">
						<Items>
							<dx:RibbonToggleButtonItem BeginGroup="True" Name="Grouping" Text="Grouping">
								<LargeImage AlternateText="Grouping" IconID="actions_group_32x32">
								</LargeImage>
								<SmallImage AlternateText="Grouping" IconID="actions_group_16x16">
								</SmallImage>
							</dx:RibbonToggleButtonItem>
							<dx:RibbonToggleButtonItem Name="Sorting" Text="Sorting">
								<LargeImage AlternateText="Sorting" IconID="data_sortasc_32x32">
								</LargeImage>
								<SmallImage AlternateText="Sorting" IconID="data_sortasc_16x16">
								</SmallImage>
							</dx:RibbonToggleButtonItem>
							<dx:RibbonToggleButtonItem Name="Filtering" Text="Filtering">
								<LargeImage AlternateText="Filtering" IconID="filter_filterbyseries_chart_32x32">
								</LargeImage>
								<SmallImage AlternateText="Filtering" IconID="filter_filterbyseries_chart_16x16">
								</SmallImage>
							</dx:RibbonToggleButtonItem>
						</Items>
					</dx:RibbonGroup>
				</Groups>
			</dx:RibbonTab>
		</Tabs>
	</dx:ASPxRibbon>
	<asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
	<dx:ASPxHiddenField ID="ASPxHiddenField1" runat="server"></dx:ASPxHiddenField>
</div>