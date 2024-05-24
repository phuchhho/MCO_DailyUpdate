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
public class tblMCO_FINANCEBLL
{
    private tblMCO_FINANCETableAdapter _Adapter = null;

    protected tblMCO_FINANCETableAdapter Adapter
    {
        get
        {
            if (_Adapter == null)
            {
                _Adapter = new tblMCO_FINANCETableAdapter();
            }

            return _Adapter;
        }
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public void tblMCO_FINANCE_Update(decimal ID, decimal AMOUNT, string RECEIPT_NO, decimal RECEIPT_ID, string DOCUMENT, decimal DOCUMENT_ID, decimal DISCOUNT_AMT, string REMARK)
    {
        Adapter.tblMCO_FINANCE_Update(ID, AMOUNT, RECEIPT_NO, RECEIPT_ID, DOCUMENT, DOCUMENT_ID, DISCOUNT_AMT, REMARK, Guid.Parse(System.Configuration.ConfigurationManager.AppSettings["UPDATE_BY"]));
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    public void tblMCO_FINANCE_Update_DPP(decimal ID, decimal AMOUNT, string RECEIPT_NO)
    {
        Adapter.tblMCO_FINANCE_Update_DPP(ID, AMOUNT, RECEIPT_NO, Guid.Parse(System.Configuration.ConfigurationManager.AppSettings["UPDATE_BY"]));
    }


    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    public MCO_DailyUpdate.DAL.MCO.tblMCO_FINANCEDataTable tblMCO_FINANCE_Select(string MCO)
    {
        return Adapter.tblMCO_FINANCE_Select(MCO);
    }
    
    //[System.ComponentModel.DataObjectMethodAttribute
    //(System.ComponentModel.DataObjectMethodType.Select, false)]

    //public MCO.tblMCO_FINANCEDataTable tblMCO_FINANCE_Detail(decimal MCO_ID)
    //{
    //    return Adapter.tblMCO_FINANCE_Detail(MCO_ID);
    //}


    // [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Insert, false)]
    //public int tblMCO_Insert(string MCO, int BookingID)
    //{
    //     string s = Adapter.tblMCO_Insert(MCO, BookingID, UtilityBLL.CurrentLoggedUser()).ToString();
    //     return int.Parse(s);
    //}

     //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
     //public bool tblBooking_IsPnrExisted(string PNR)
     //{
     //    bool blnReturn = true;
     //    MCO.tblBOOKINGDataTable dtPNR = Adapter.tblBooking_IsPnrExisted(PNR);
     //    if (dtPNR.Count == 0)
     //        blnReturn = false;

     //    dtPNR.Dispose();
     //    return blnReturn;
     //}

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public OnlineNotes.TasksDataTable Tasks_Select(int TaskStatusCategoryID, int Type, int Active)
    //{
    //    return Adapter.Tasks_Select(TaskStatusCategoryID, Type, Active);
    //}

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public OnlineNotes.TasksDataTable Tasks_SelectTop(int TaskStatusCategoryID, int Type, int Active)
    //{
    //    return Adapter.Tasks_SelectTop(TaskStatusCategoryID, Type, Active);
    //}


    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public OnlineNotes.TasksDataTable Tasks_SelectMyTask(Guid AssignedTo, int TaskStatusCategoryID, int Type, int Active)
    //{
    //    return Adapter.Tasks_SelectMyTask(AssignedTo, TaskStatusCategoryID, Type, Active);
    //}

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public OnlineNotes.TasksDataTable Tasks_SelectTopMyTask(Guid AssignedTo, int TaskStatusCategoryID, int Type, int Active)
    //{
    //    return Adapter.Tasks_SelectTopMyTask(AssignedTo, TaskStatusCategoryID, Type, Active);
    //}

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public OnlineNotes.TasksDataTable Tasks_SelectByCategory(DateTime FromDate, DateTime Todate, int Category, int Type, int Active)
    //{
    //    return Adapter.Tasks_SelectByCategory(FromDate, Todate, Category, Type, Active);
    //}

    

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
    //public OnlineNotes.TasksDataTable Tasks_Detail(int TaskID)
    //{
    //    return Adapter.Tasks_Detail(TaskID);
    //}

    ////[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    ////public void Delete(int ID, bool Active)
    ////{
    ////    Adapter.Schedules_Delete(ID, Active);
    ////}    

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    //public void Tasks_Update(int ID, int TaskCategoryID, int DepartmentID, string Subject, bool Emergency, int TaskStatusID, string Description, string Contact, string Email, Guid ModifiedBy)
    //{
    //    Adapter.Tasks_Update(ID, TaskCategoryID, DepartmentID, Subject, Emergency, Description, Contact, Email, ModifiedBy);
    //}

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, false)]
    //public void Tasks_Delete(int ID, Guid ModifiedBy)
    //{
    //    Adapter.Tasks_Delete(ID, ModifiedBy);
    //}


    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, false)]
    //public void Tasks_UpdateStatus(int ID, int TaskStatusID)
    //{
    //    Adapter.Tasks_UpdateStatus(ID, TaskStatusID);
    //}
}
