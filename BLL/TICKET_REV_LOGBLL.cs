using System;
using System.Data;
using System.Configuration;
using System.Web;
using MCO_DailyUpdate.DAL.MCOTableAdapters;

/// <summary>
/// Summary description for SchedulesBLL
/// </summary>
/// 

[System.ComponentModel.DataObject]
public class TICKET_REV_LOGBLL
{
    private TICKET_REV_LOGTableAdapter _Adapter = null;

    protected TICKET_REV_LOGTableAdapter Adapter
    {
        get
        {
            if (_Adapter == null)
            {
                _Adapter = new TICKET_REV_LOGTableAdapter();
            }

            return _Adapter;
        }
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    public int TICKET_REV_LOG_ADD(int TICKET_REV_ID, string TKT_NUMBER, int AUTO_UPDATE, int USER_UPDATE, int NO_EXIST)
    {
        string s = Adapter.TICKET_REV_LOG_ADD(TICKET_REV_ID, TKT_NUMBER, AUTO_UPDATE, USER_UPDATE, NO_EXIST).ToString();
        return int.Parse(s);
    }

}
