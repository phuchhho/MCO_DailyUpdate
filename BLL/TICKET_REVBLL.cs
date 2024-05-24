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
public class TICKET_REVBLL
{
    private TICKET_REVTableAdapter _Adapter = null;

    protected TICKET_REVTableAdapter Adapter
    {
        get
        {
            if (_Adapter == null)
            {
                _Adapter = new TICKET_REVTableAdapter();
            }

            return _Adapter;
        }
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public MCO_DailyUpdate.DAL.MCO.TICKET_REVDataTable TICKET_REV_SELECT()
    {
        return Adapter.TICKET_REV_SELECT();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public MCO_DailyUpdate.DAL.MCO.TICKET_REVDataTable TICKET_REV_SELECT_DPP()
    {
        return Adapter.TICKET_REV_SELECT_DPP();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public MCO_DailyUpdate.DAL.MCO.TICKET_REVDataTable TICKET_REV_Select_DP9_Missing(DateTime From, DateTime To)
    {
        return Adapter.TICKET_REV_Select_DP9_Missing(From, To);
    }
}
