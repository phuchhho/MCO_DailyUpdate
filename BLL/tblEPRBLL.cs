using System;
using System.Data;
using System.Configuration;
using System.Web;
using MCO_DailyUpdate.DAL.EPRTableAdapters;

/// <summary>
/// Summary description for SchedulesBLL
/// </summary>
/// 

[System.ComponentModel.DataObject]
public class tblEPRBLL
{
    private tblEPRTableAdapter _Adapter = null;

    protected tblEPRTableAdapter Adapter
    {
        get
        {
            if (_Adapter == null)
            {
                _Adapter = new tblEPRTableAdapter();
            }

            return _Adapter;
        }
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public MCO_DailyUpdate.DAL.EPR.tblEPRDataTable tblEPR_SELECT_ALERT(int Days)
    {   
        return Adapter.tblEPR_SELECT_ALERT(Days);
    }
}
