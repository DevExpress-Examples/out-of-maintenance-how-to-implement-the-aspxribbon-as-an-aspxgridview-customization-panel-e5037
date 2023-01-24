<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Src="~/ASPxGridRibbon.ascx" TagPrefix="dx" TagName="ASPxGridRibbon" %>
<%@ Register Assembly="DevExpress.Web.v13.2, Version=13.2.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title></title>
</head>
<body>
	<h1>How to implement the ASPxRibbon as an ASPxGridView customization panel</h1>
	<form id="form1" runat="server">
		<div>
			<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" KeyFieldName="OrderID" ClientInstanceName="cASPxGridView1">
				<Settings ShowTitlePanel="true" ShowStatusBar="Hidden" />
				<SettingsPopup>
					<EditForm Width="600px" HorizontalAlign="Center" VerticalAlign="Middle" />
				</SettingsPopup>
				<Columns>
					<dx:GridViewDataTextColumn FieldName="OrderID" ReadOnly="True" VisibleIndex="0">
						<EditFormSettings Visible="False" />
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="CustomerID" VisibleIndex="1">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="EmployeeID" VisibleIndex="2">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataDateColumn FieldName="OrderDate" VisibleIndex="3">
					</dx:GridViewDataDateColumn>
					<dx:GridViewDataDateColumn FieldName="RequiredDate" VisibleIndex="4">
					</dx:GridViewDataDateColumn>
					<dx:GridViewDataDateColumn FieldName="ShippedDate" VisibleIndex="5">
					</dx:GridViewDataDateColumn>
					<dx:GridViewDataTextColumn FieldName="ShipVia" VisibleIndex="6">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="Freight" VisibleIndex="7">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ShipName" VisibleIndex="8">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ShipAddress" VisibleIndex="9">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ShipCity" VisibleIndex="10">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ShipRegion" VisibleIndex="11">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ShipPostalCode" VisibleIndex="12">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ShipCountry" VisibleIndex="13">
					</dx:GridViewDataTextColumn>
				</Columns>
				<Templates>
					<TitlePanel>
						<dx:ASPxGridRibbon runat="server" ID="ASPxGridRibbon" EnableXlsExport="true"></dx:ASPxGridRibbon>
					</TitlePanel>
				</Templates>
			</dx:ASPxGridView>
			<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
				SelectCommand="SELECT [OrderID], [CustomerID], [EmployeeID], [OrderDate], [RequiredDate], [ShippedDate], [ShipVia], [Freight], [ShipName], [ShipAddress], [ShipCity], [ShipRegion], [ShipPostalCode], [ShipCountry] FROM [Orders]"></asp:SqlDataSource>
			<br />
			<dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2" KeyFieldName="ProductID" ClientInstanceName="cASPxGridView2">
				<Settings ShowTitlePanel="true" ShowStatusBar="Hidden" />
				<SettingsPopup>
					<EditForm Width="600px" HorizontalAlign="Center" VerticalAlign="Middle" />
				</SettingsPopup>
				<Columns>
					<dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="0">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ProductID" VisibleIndex="1" ReadOnly="True">
						<EditFormSettings Visible="False" />
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="SupplierID" VisibleIndex="2">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="3">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="QuantityPerUnit" VisibleIndex="4">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="5">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="UnitsInStock" VisibleIndex="6">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="UnitsOnOrder" VisibleIndex="7">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="ReorderLevel" VisibleIndex="8">
					</dx:GridViewDataTextColumn>
					<dx:GridViewDataCheckColumn FieldName="Discontinued" VisibleIndex="9">
					</dx:GridViewDataCheckColumn>
				</Columns>
				<Templates>
					<TitlePanel>
						<dx:ASPxGridRibbon runat="server" ID="ASPxGridRibbon1" EnableXlsExport="true"></dx:ASPxGridRibbon>
					</TitlePanel>
				</Templates>
			</dx:ASPxGridView>
			<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
				SelectCommand="SELECT [ProductName], [ProductID], [SupplierID], [CategoryID], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [ReorderLevel], [Discontinued] FROM [Products]">
			</asp:SqlDataSource>
		</div>
	</form>
</body>
</html>