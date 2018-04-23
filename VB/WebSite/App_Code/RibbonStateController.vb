Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxHiddenField

''' <summary>
''' Summary description for RibbonState
''' </summary>
Public Class RibbonStateController
	Private clientStateField As ASPxHiddenField
	Private Const defaultPrefix As String = "RibbonState_"
	Private stateKeyPrefix As String
	Private stateInitizedKey As String

	Public Sub New(ByVal clientStateField As ASPxHiddenField)
		stateKeyPrefix = defaultPrefix & clientStateField.ClientID
		stateInitizedKey = defaultPrefix & "Initialized" & clientStateField.ClientID

		If (Not IsInitialized) Then
			Initialize(clientStateField)
		End If
		Me.clientStateField = clientStateField
	End Sub

	Private ReadOnly Property IsInitialized() As Boolean
		Get
			Return HttpContext.Current.Items.Contains(stateInitizedKey)
		End Get
	End Property

	Private Sub Initialize(ByVal clientStateField As ASPxHiddenField)
		For Each pair As KeyValuePair(Of String, Object) In clientStateField
			HttpContext.Current.Items.Add(pair.Key, pair.Value)
		Next pair
		HttpContext.Current.Items.Add(stateInitizedKey, True)
	End Sub

	Public Property EditingMode() As GridViewEditingMode
		Get
			Return GetStateValue(Of GridViewEditingMode)("EditingMode")
		End Get
		Set(ByVal value As GridViewEditingMode)
			SetStateValue("EditingMode", value)
		End Set
	End Property

	Public Property PageSize() As Integer
		Get
			Return GetStateValue(Of Integer)("PageSize")
		End Get
		Set(ByVal value As Integer)
			SetStateValue("PageSize", value)
		End Set
	End Property

	Public Property ShowGroupPanel() As Boolean
		Get
			Return GetStateValue(Of Boolean)("Grouping")
		End Get
		Set(ByVal value As Boolean)
			SetStateValue("Grouping", value)
		End Set
	End Property

	Public Property AllowSort() As Boolean
		Get
			Return GetStateValue(Of Boolean)("Sorting")
		End Get
		Set(ByVal value As Boolean)
			SetStateValue("Sorting", value)
		End Set
	End Property

	Public Property ShowFilterRow() As Boolean
		Get
			Return GetStateValue(Of Boolean)("Filtering")
		End Get
		Set(ByVal value As Boolean)
			SetStateValue("Filtering", value)
		End Set
	End Property

	Public Sub UpdateHiddenField()
		If IsInitialized Then
			For Each entry As DictionaryEntry In HttpContext.Current.Items
				Dim key As String = entry.Key.ToString()
				If key.Contains(stateKeyPrefix) Then
					clientStateField.Set(key, entry.Value)
				End If
			Next entry
		End If
	End Sub

	Private Function GetStateValue(Of T)(ByVal fieldName As String) As T
		Dim key As String = stateKeyPrefix & fieldName
		If HttpContext.Current.Items.Contains(key) Then
			If GetType(T).IsEnum AndAlso HttpContext.Current.Items(key).GetType() Is GetType(String) Then
				Return CType(System.Enum.Parse(GetType(T), CStr(HttpContext.Current.Items(key)), True), T)
			Else
				Return CType(HttpContext.Current.Items(key), T)
			End If
		End If
		Return Nothing
	End Function


	Private Sub SetStateValue(Of T)(ByVal fieldName As String, ByVal value As T)
		HttpContext.Current.Items(stateKeyPrefix & fieldName) = value
	End Sub

End Class