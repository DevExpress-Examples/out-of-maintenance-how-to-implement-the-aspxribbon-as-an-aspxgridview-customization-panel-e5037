<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128564634/13.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E5037)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [RibbonStateController.cs](./CS/WebSite/App_Code/RibbonStateController.cs) (VB: [RibbonStateController.vb](./VB/WebSite/App_Code/RibbonStateController.vb))
* [ASPxGridRibbon.ascx](./CS/WebSite/ASPxGridRibbon.ascx) (VB: [ASPxGridRibbon.ascx](./VB/WebSite/ASPxGridRibbon.ascx))
* [ASPxGridRibbon.ascx.cs](./CS/WebSite/ASPxGridRibbon.ascx.cs) (VB: [ASPxGridRibbon.ascx.vb](./VB/WebSite/ASPxGridRibbon.ascx.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
<!-- default file list end -->
# How to implement the ASPxRibbon as an ASPxGridView customization panel
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/e5037/)**
<!-- run online end -->


<p>This example demonstrates how to use an ASPxRibbon in a Web User Control, which can be added to an ASPxGridView's TitleTemplate to represent a control panel for managing common GidView functions.</p>
<p>Due to ASPxGridView specifics, all actions performed with the control require a request to the server in order to take effect. All these requests are performed via callbacks except two operations: switching the control to edit mode and the export operation. These operations are executed via postbacks. For this purpose, we implemented a custom postback event handler.</p>
<p>Another important point is that since the ASPxRibbon is a control intended to be used primarily as a client side static component, it does not keep its state between requests to the server. So, it is necessary to handle saving and restoring of Ribbon items' state on each request. For this purpose, use an ASPxHiddenField.</p>
<p>Please note that in order to enable correct operation of the client side command handler, the following settings need to be adjusted and should not be overridden:</p>


```cs
ASPxGridView.SettingsBehavior.AllowFocusedRow = true;
ASPxGridView.SettingsPager.Visible = false;
```


<p> </p>


```vb
ASPxGridView.SettingsBehavior.AllowFocusedRow = true
ASPxGridView.SettingsPager.Visible = false 

```


<p><strong><br> Note:</strong></p>
<p>Starting with version 17.1, ASPxGridView provides an embedded toolbar that allows implementing similar functionality without using an external ribbon. See the <a href="https://www.devexpress.com/Support/Center/p/T552217">ASPxGridView - Simple implementation of an embedded toolbar</a> example demonstrating this feature. </p>

<br/>


