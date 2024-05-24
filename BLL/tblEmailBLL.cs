using System;
using System.Data;
using System.Configuration;
using System.Web;
using MCO_DailyUpdate.DAL.MCOTableAdapters;

/// <summary>
/// 
/// </summary>

[System.ComponentModel.DataObject]
public class tblEmailBLL
{
    private tblEmailTableAdapter _adapter = null;

    protected tblEmailTableAdapter Adapter
    {
        get
        {
            if (_adapter == null)
            {
                _adapter = new tblEmailTableAdapter();
            }

            return _adapter;
        }
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public MCO_DailyUpdate.DAL.MCO.tblEmailDataTable tblEmail_Select()
    {
        return Adapter.tblEmail_Select();
    }


    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public WPA.tblEmailDataTable GetEmailFromUsername(string Username)
    //{
    //    return Adapter.GetEmailFromUsername(Username);
    //}

}
