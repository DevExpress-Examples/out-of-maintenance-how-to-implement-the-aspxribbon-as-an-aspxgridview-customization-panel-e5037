using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHiddenField;

/// <summary>
/// Summary description for RibbonState
/// </summary>
public class RibbonStateController
{
    ASPxHiddenField clientStateField;
    const string defaultPrefix = "RibbonState_";
    string stateKeyPrefix;
    string stateInitizedKey;

    public RibbonStateController(ASPxHiddenField clientStateField)
    {
        stateKeyPrefix = defaultPrefix + clientStateField.ClientID;
		stateInitizedKey = defaultPrefix + "Initialized" + clientStateField.ClientID;

        if (!IsInitialized)
            Initialize(clientStateField);
        this.clientStateField = clientStateField;
    }

    private bool IsInitialized
    {
        get
        {
            return HttpContext.Current.Items.Contains(stateInitizedKey);
        }
    }

    private void Initialize(ASPxHiddenField clientStateField)
    {
        foreach (KeyValuePair<string, object> pair in clientStateField)
        {
            HttpContext.Current.Items.Add(pair.Key, pair.Value);
        }
        HttpContext.Current.Items.Add(stateInitizedKey, true);
    }

    public GridViewEditingMode EditingMode
    {
        get { return GetStateValue<GridViewEditingMode>("EditingMode"); }
        set { SetStateValue("EditingMode", value); }
    }

    public int PageSize
    {
        get { return GetStateValue<int>("PageSize"); }
        set { SetStateValue("PageSize", value); }
    }

    public bool ShowGroupPanel
    {
        get { return GetStateValue<bool>("Grouping"); }
        set { SetStateValue("Grouping", value); }
    }

    public bool AllowSort
    {
        get { return GetStateValue<bool>("Sorting"); }
        set { SetStateValue("Sorting", value); }
    }

    public bool ShowFilterRow
    {
        get { return GetStateValue<bool>("Filtering"); }
        set { SetStateValue("Filtering", value); }
    }

    public void UpdateHiddenField()
    {
        if (IsInitialized)
            foreach (DictionaryEntry entry in HttpContext.Current.Items)
            {
                string key = entry.Key.ToString();
                if (key.Contains(stateKeyPrefix))
                    clientStateField.Set(key, entry.Value);
            }
    }

    private T GetStateValue<T>(string fieldName)
    {
        string key = stateKeyPrefix + fieldName;
        if (HttpContext.Current.Items.Contains(key))
        {
            if (typeof(T).IsEnum && HttpContext.Current.Items[key].GetType() == typeof(string))
            {
                return (T)Enum.Parse(typeof(T), (string)HttpContext.Current.Items[key], true);
            }
            else
                return (T)HttpContext.Current.Items[key];
        }
        return default(T);
    }


    private void SetStateValue<T>(string fieldName, T value)
    {
        HttpContext.Current.Items[stateKeyPrefix + fieldName] = value;
    }

}