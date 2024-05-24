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
public class tblMCOBLL
{
    private tblMCOTableAdapter _Adapter = null;

    protected tblMCOTableAdapter Adapter
    {
        get
        {
            if (_Adapter == null)
            {
                _Adapter = new tblMCOTableAdapter();
            }

            return _Adapter;
        }
    }


    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public MCO.tblMCODataTable tblMCO_Select(int Lock, string MCO, DateTime FROM_DATE, DateTime TO_DATE)
    //{
        
    //    return Adapter.tblMCO_Select(Lock, MCO, FROM_DATE, TO_DATE);
    //}


    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public MCO.tblMCODataTable tblMCO_Detail(decimal ID)
    //{
    //    return Adapter.tblMCO_Detail(ID);
    //}

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public MCO_DailyUpdate.DAL.MCO.tblMCODataTable tblMCO_IsExisted(string MCO)
    //{
    //    return Adapter.tblMCO_IsExisted(MCO);
    //}


    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public MCO.tblMCODataTable tblMCO_Detail_V3(decimal ID)
    //{
    //    return Adapter.tblMCO_DETAIL_V3(ID);
    //}


    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    //public decimal tblMCO_Insert(string MCO, decimal BookingID)
    //{
    //     string s = Adapter.tblMCO_Insert(MCO, BookingID, UtilityBLL.CurrentLoggedUser()).ToString();
    //     return decimal.Parse(s);
    //}


    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    // public decimal tblMCO_Insert_2(string MCO)
    // {
    //     string s = Adapter.tblMCO_Insert_2(MCO, UtilityBLL.CurrentLoggedUser()).ToString();
    //     return decimal.Parse(s);
    // }


    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    // public void tblMCO_Update(decimal ID, string MCO, int DEPO_PAX, decimal DEPO_AMT, DateTime DEPO_DATE, int PCT)
    // {
    //     //if (DEPO_DATE.ToString("dd/MM/yyyy") == "01/01/1900")
    //     //    Adapter.tblMCO_Update2(ID, DEPO_PAX, DEPO_AMT, UtilityBLL.CurrentLoggedUser()).ToString();
    //     Adapter.tblMCO_Update(ID, MCO, DEPO_PAX, DEPO_AMT, DEPO_DATE, PCT, UtilityBLL.CurrentLoggedUser()).ToString();
    // }

    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    // public void tblMCO_Update2(decimal ID, int DEPO_PAX, decimal DEPO_AMT, int PCT)
    // {
    //     Adapter.tblMCO_Update2(ID, DEPO_PAX, DEPO_AMT, PCT, UtilityBLL.CurrentLoggedUser()).ToString();
    // }

    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    // public void tblMCO_Update_Lock(decimal ID)
    // {
         
    //     Adapter.tblMCO_Update_Lock(ID, UtilityBLL.CurrentLoggedUser());
    // }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public bool tblMCO_IsMcoExisted(string MCO)
    {
        bool blnReturn = true;
        MCO_DailyUpdate.DAL.MCO.tblMCODataTable dtMCO = Adapter.tblMCO_IsMcoExisted(MCO);
        if (dtMCO.Count == 0)
            blnReturn = false;

        dtMCO.Dispose();
        return blnReturn;
    }

    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    // public bool tblMCO_IsUniqueMCO(decimal ID, string MCO)
    // {
    //     bool blnReturn = true;
    //     MCO.tblMCODataTable dtMCO = Adapter.tblMCO_IsUniqueMCO(ID, MCO);
    //     if (dtMCO.Count == 0)
    //         blnReturn = false;

    //     dtMCO.Dispose();
    //     return blnReturn;
    // }

    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    // public DataView tblMCO_Check(DateTime FROM_DATE, DateTime TO_DATE)
    // {

    //     return Adapter.tblMCO_Check(FROM_DATE, TO_DATE).DefaultView;
    // }


    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    // public MCO.tblMCODataTable CRM_MCO_List(string PNR)
    // {
    //     return Adapter.CRM_MCO_List(PNR);
    // }

    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    // public MCO.tblMCODataTable CRM_MCO_List_V3(string PNR)
    // {
    //     return Adapter.CRM_MCO_List_V3(PNR);
    // }
}
