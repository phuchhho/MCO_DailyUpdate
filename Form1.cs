using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.Common;
using System.IO;

namespace MCO_DailyUpdate
{
    public partial class Form1 : Form
    {
        string connectionString; 

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //EPR Alert
            //EPR_Alert();
            //return;

            //Update EMD DPP
            Update_Finance();            

            // Get Missing MCO
            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
            {
                //GetMissingMCO(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(-1));
                GetMissingMCO(DateTime.Now.AddDays(-22), DateTime.Now.AddDays(-1));
            }

            Application.Exit();
        }

        protected void EPR_Alert()
        {
            int Days = int.Parse(ConfigurationSettings.AppSettings["EPR_Email_Day_Before"]);

            tblEPRBLL obj = new tblEPRBLL();
            DataView dv = obj.tblEPR_SELECT_ALERT(Days).DefaultView;
            string Subject, Body, To, CC;

            foreach (DataRowView rowView in dv)
            {       
                DataRow row = rowView.Row;
                Subject = "Cảnh báo đến hạn cập nhật thông tin EPR: " + row["EPR"] + " - " + row["EPR_NAME"];
                Body = "EPR cần cập nhật thông tin: </br>---------------------------</br>EPR: <b>" + row["EPR"] + "</b></br>EPR NAME: " + row["EPR_NAME"] + "</br>Họ tên: " + row["HRP_NAME"] + "</br>Đơn vị: " + row["HRP_LEVEL2_NAME"] + (row["HRP_LEVEL3_NAME"].ToString().Length > 0 ? " - " + row["HRP_LEVEL3_NAME"] : "") + "</br>Hạn xử lý: " + ((DateTime)row["ALERT"]).ToString("dd/MM/yyyy") + "</br>Nội dung: " + row["Note"];
                To = rowView["Email"].ToString();
                CC = "";
                SendMail(Subject, Body, To, CC);    
            }
                        
            obj = null;
        }

        private void SendMail(string Subject, string Body, string To, string CC)
        {

            //if (!File.Exists(file))
            //    return;

            tblEmailBLL obj = new tblEmailBLL();
            bool EnableSsl = false;
            string Port = "587";
            string Email = "phuchh@vietnamairlines.com";
            string Password = "";
            string Host = ConfigurationSettings.AppSettings["MailServer"].ToString();

            DataView dv2 = obj.tblEmail_Select().DefaultView;

            if (dv2.Count > 0)
            {
                Email = dv2[0]["Email"].ToString().Trim();
                Password = dv2[0]["Password"].ToString().Trim();
                Host = dv2[0]["Host"].ToString();
                Port = dv2[0]["Port"].ToString();
                EnableSsl = dv2[0]["EnableSsl"].ToString() == "1" ? true : false;

            }


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            //mail.IsBodyHtml = false;
            mail.From = new MailAddress(Email, "Soft-SRO", System.Text.Encoding.UTF8);
            mail.Subject = Subject;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            string[] arrTo = To.Split(';');
            foreach (string s in arrTo)
            {
                if (s.Trim().Length > 0)
                    mail.To.Add(s);
            }

            string[] arrCc = CC.Split(';');
            foreach (string s in arrCc)
            {
                if (s.Trim().Length > 0)
                    mail.CC.Add(s);
            }

            mail.Priority = MailPriority.High;
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Credentials = new System.Net.NetworkCredential(Email, Password);
            smtp.EnableSsl = EnableSsl;
            smtp.Port = int.Parse(Port);

            mail.Body = Body;

            //if (File.Exists(file))
            //{
            //    System.Net.Mail.Attachment attachFile = new System.Net.Mail.Attachment(file);
            //    mail.Attachments.Add(attachFile);
            //}

            try
            {
                smtp.Port = 587;
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                try
                {
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    smtp.Send(mail);
                }
                catch (Exception ex1)
                {

                }
            }
        }

        protected void GetMissingMCO(DateTime FromDate, DateTime ToDate)
        { 
            TICKET_REVBLL obj = new TICKET_REVBLL();
            DataView dv = obj.TICKET_REV_Select_DP9_Missing(FromDate, ToDate).DefaultView;
            bool sendMail = false;
            string sourceFile = "";
            string destinationFile = "";

            if (dv.Count == 0)
            {
                DataRowView newRow = dv.AddNew();
                newRow["ID"] = "-1";
                newRow["Row"] = "0";
                newRow["TKT_NUMBER"] = " ";
                newRow["ISSUE_DATE"] = DBNull.Value;
                newRow["PNR"] = " ";
                newRow["PAX_NAME"] = " ";
                newRow["TTL"] = 0;                

                newRow["REV_AGENT"] = "0";
                newRow["SEQ_NUM"] = "0";
                newRow["FOP"] = DBNull.Value;
                newRow["CURR"] = DBNull.Value;
                newRow["FARE"] = DBNull.Value;
                newRow["TAX"] = DBNull.Value;
                newRow["TIME"] = DBNull.Value;
                newRow["FLIGHTS"] = DBNull.Value;
                newRow["SEGMENTS"] = DBNull.Value;
                newRow["CLASSES"] = DBNull.Value;
                newRow["FARE_CLASS"] = DBNull.Value;
                newRow["STATUSES"] = DBNull.Value;
                newRow["EPR_ISSUE"] = DBNull.Value;
                newRow["INT_DOM"] = DBNull.Value;
                newRow["REMARK"] = DBNull.Value;
                newRow["NOTES"] = DBNull.Value;
                newRow["FARE_USD"] = DBNull.Value;
                newRow["TAX_USD"] = DBNull.Value;
                newRow["TTL_USD"] = DBNull.Value;
                newRow["DATA_OK"] = DBNull.Value;                
                newRow["TKT_TYPE"] = DBNull.Value;
                newRow["FROM_TKT"] = DBNull.Value;
                newRow["US_DATE"] = DBNull.Value;
                newRow["YQ"] = DBNull.Value;
                newRow["IATA_CODE"] = DBNull.Value;
                newRow["UPDATE_DATE"] = DBNull.Value;                
                newRow.EndEdit();

      
            }

            int no = 0;
            no = dv.Count;

            if (no > 0)
            {
                sendMail = true;
                // Copy file Sample --> File ngay hien hanh                
                bool retVal = false;
                sourceFile = @"" + ConfigurationSettings.AppSettings["FilePath"].ToString() + ConfigurationSettings.AppSettings["FileSample"].ToString();
                
                bool flagD = false;
                bool flagI = false;
                string strLastRoute = "";
                string strETD = "";
                string strSegment = "";
                int i = 0;
                int j = 0;
                int k = 0;

                destinationFile = @"" + ConfigurationSettings.AppSettings["FilePath"].ToString() + ConfigurationSettings.AppSettings["FileName"].ToString() + FromDate.ToString("ddMMMyyyy") + " den ngay " + ToDate.ToString("ddMMMyyyy") + ".xls";

                System.IO.File.Copy(sourceFile, destinationFile, true);

                //Ghi du lieu vao file excel Hop dong het hieu luc                   

                connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ConfigurationSettings.AppSettings["FilePath"].ToString() + ConfigurationSettings.AppSettings["FileName"].ToString() + FromDate.ToString("ddMMMyyyy") + " den ngay " + ToDate.ToString("ddMMMyyyy") + @".xls;Extended Properties=""Excel 8.0;HDR=YES;""";

                //AddRecord("INSERT INTO [HĐ hết hiệu lực$] ([c0]) VALUES(\" \")");
                AddRecord("INSERT INTO [Sheet1$] ([c1]) VALUES(\"" + ConfigurationSettings.AppSettings["MailSubject"].ToString().ToUpper() + FromDate.ToString("ddMMMyyyy") + " ĐẾN NGÀY " + ToDate.ToString("ddMMMyyyy") + "\")");

                AddRecord("INSERT INTO [Sheet1$] ([c0]) VALUES(\" \")");

                AddRecord("INSERT INTO [Sheet1$] ([c0], [c1], [c2], [c3], [c4], [c5], [c6]) VALUES(\"STT\", \"Số EMD\", \"Ngày xuất\", \"PNR\", \"Tên khách\", \"Giá trị\", \"\")");


                foreach (DataRowView rowView in dv)
                {

                    AddRecord("INSERT INTO [Sheet1$] ([c0], [c1], [c2], [c3], [c4], [c5], [c6]) VALUES(\"" + rowView["ROW"] + "\", \"" + rowView["TKT_NUMBER"] + "\", \"" + rowView["ISSUE_DATE"] + "\", \"" + rowView["PNR"] + "\", \"" + rowView["PAX_NAME"] + "\", \"" + rowView["TTL"] + "\", \"" + "" +"\")");
                }
            }

            if (sendMail)
            {
                string s = ConfigurationManager.AppSettings["MailSubject"] + " từ ngày " + FromDate.ToString("ddMMMyyyy") + " đến ngày " + ToDate.ToString("ddMMMyyyy");
                SendGMail(destinationFile, s, no);
            }

            dv.Dispose();
            obj = null;
        }

        private void AddRecord(string sql)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            //DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            

            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;

                using (DbCommand command = connection.CreateCommand())
                {
                    //command.CommandText = "INSERT INTO [Lịch bay$] ([Hành trình], [Số hiệu chuyến bay]) VALUES(1,\"Florida\")";
                    command.CommandText = sql;// "INSERT INTO [Lịch bay$] ([c1]) VALUES(\"LỊCH BAY VIETNAM AIRLINES NGÀY " + DateTime.Today.ToString("DD/MMM/YYYY") + "\")";
                    /*command.CommandText = "INSERT INTO [Lịch bay$] ([c0]) VALUES(\" \")";
                    command.CommandText = "INSERT INTO [Lịch bay$] ([c1], [c2], [c3], [c4]) VALUES(\"HÀNH TRÌNH\", \"SỐ HIỆU CHUYẾN BAY\", \"GIO CẤT CÁNH\", \"GIỜ HẠ CÁNH\")";
                    command.CommandText = "INSERT INTO [Lịch bay$] ([c0]) VALUES(\"QUỐC NỘI\")";*/
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        private void Update_Finance()
        {
            TICKET_REVBLL objTKT = new TICKET_REVBLL();
            TICKET_REV_LOGBLL objLOG = new TICKET_REV_LOGBLL();
            tblMCOBLL objMCO = new tblMCOBLL();
            tblMCO_FINANCEBLL objFIN = new tblMCO_FINANCEBLL();

            string MCO = "";
            string DPP = "";
            decimal TTL = 0;
            bool exist = false;
            int auto_update = -1;
            //bool user_update = false;
            string ID = "-1";
            string TICKET_REV_ID = "-1";

            int iNoRFD = 0;
            int iRFD = 0;



            // Select RREV format 7388200xxxxxx
            // Update cac truong hop hoan khong phat
            DataView dv = objTKT.TICKET_REV_SELECT().DefaultView;

            foreach (DataRowView rowView in dv)
            {
                DataRow row = rowView.Row;
                if (row["FROM_TKT"] != System.DBNull.Value)
                    MCO = row["FROM_TKT"].ToString();

                if (row["ID"] != System.DBNull.Value)
                    TICKET_REV_ID = row["ID"].ToString();


                if (objMCO.tblMCO_IsMcoExisted(MCO))
                {
                    exist = true;

                    DataView dvFIN = objFIN.tblMCO_FINANCE_Select(MCO).DefaultView;
                    if (dvFIN.Count > 0)
                    {
                        ID = dvFIN[0]["ID"].ToString();
                    }

                    if (ID != "-1")
                    {
                        try
                        {
                            //objFIN.tblMCO_FINANCE_Update(int.Parse(ID), 0, "", -1, "", -1, -1, "");
                            objFIN.tblMCO_FINANCE_Update_DPP(int.Parse(ID), 0, " ");
                            iNoRFD++;
                        }
                        catch (Exception ex)
                        { }
                        finally
                        {
                            auto_update = 1;
                        }


                    }
                    //else
                    //    user_update = true;                    

                    dvFIN.Dispose();
                    dvFIN = null;
                }

                //
                objLOG.TICKET_REV_LOG_ADD(int.Parse(TICKET_REV_ID), MCO, auto_update, -auto_update, exist ? -1 : 1);
                ID = "-1";
                TICKET_REV_ID = "-1";
                MCO = "";
                //DPP = "";
            }

            dv.Dispose();



            // Select DPP
            // Update cac truong hop hoan co phat
            DataView dvDPP = objTKT.TICKET_REV_SELECT_DPP().DefaultView;

            foreach (DataRowView rowView in dvDPP)
            {
                DataRow row = rowView.Row;
                if (row["FROM_TKT"] != System.DBNull.Value)
                    MCO = row["FROM_TKT"].ToString();
                else
                    MCO = "";

                if (row["TKT_NUMBER"] != System.DBNull.Value)
                    DPP = row["TKT_NUMBER"].ToString();
                else
                    DPP = "";

                if (row["TTL"] != System.DBNull.Value)
                    TTL = decimal.Parse(row["TTL"].ToString());
                else
                    TTL = 0;

                if (row["ID"] != System.DBNull.Value)
                    TICKET_REV_ID = row["ID"].ToString();
                else
                    TICKET_REV_ID = "-1";


                if (objMCO.tblMCO_IsMcoExisted(MCO))
                {
                    exist = true;

                    DataView dvFIN = objFIN.tblMCO_FINANCE_Select(MCO).DefaultView;
                    if (dvFIN.Count > 0)
                    {
                        ID = dvFIN[0]["ID"].ToString();
                    }

                    if (ID != "-1")
                    {
                        try
                        {
                            //objFIN.tblMCO_FINANCE_Update(int.Parse(ID), TTL, DPP, -1, "", -1, -1, "");
                            objFIN.tblMCO_FINANCE_Update_DPP(int.Parse(ID), TTL, DPP);
                            iRFD++;
                        }
                        catch (Exception ex)
                        { }
                        finally
                        {
                            auto_update = 1;
                        }


                    }
                    //else
                    //    user_update = true;                    

                    dvFIN.Dispose();
                    dvFIN = null;
                }

                //
                objLOG.TICKET_REV_LOG_ADD(int.Parse(TICKET_REV_ID), MCO, auto_update, -auto_update, exist ? -1 : 1);
                ID = "-1";
                TICKET_REV_ID = "-1";
                MCO = "";
                DPP = "";
                TTL = 0;
            }
            dvDPP.Dispose();


            // Send Mail
            string s = ConfigurationManager.AppSettings["MailSubject2"] + " _ " + DateTime.Now.ToString("ddMMMyyyy");
            SendGMail2(s, iNoRFD, iRFD);
            //


            //dv = null;
            dvDPP = null;
            objFIN = null;
            objMCO = null;
            objLOG = null;
            objTKT = null;
        }

        private void SendGMail2(string Subject, int iNoRFD, int iRFD)
        {
            tblEmailBLL obj = new tblEmailBLL();
            bool EnableSsl = false;
            string Port = "587";
            string Email = "phuchh@vietnamairlines.com";
            string Password = "";
            string Host = "smtp.vietnamairlines.com";

            DataView dv2 = obj.tblEmail_Select().DefaultView;

            if (dv2.Count > 0)
            {
                Email = dv2[0]["Email"].ToString().Trim();
                Password = dv2[0]["Password"].ToString().Trim();
                Host = dv2[0]["Host"].ToString();
                Port = dv2[0]["Port"].ToString();
                EnableSsl = dv2[0]["EnableSsl"].ToString() == "1" ? true : false;

            }


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.From = new MailAddress(Email, "IT SRO", System.Text.Encoding.UTF8);
            mail.Subject = Subject;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            string[] To = ConfigurationSettings.AppSettings["MailTo"].ToString().Split(';');
            foreach (string s in To)
            {
                if (s.Trim().Length > 0)
                    mail.To.Add(s);
            }



            mail.Priority = MailPriority.High;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = Host;
            smtp.Credentials = new System.Net.NetworkCredential(Email, Password);
            smtp.EnableSsl = EnableSsl;
            smtp.Port = int.Parse(Port);

            mail.Body = "MCO Non-Refunded/Non-Penalty: " + iNoRFD.ToString() + "</br>MCO Refunded/Penalty: " + iRFD.ToString() + "</br></br>-------------</br>" + "MCO Daily Update - Finance";             

            try
            {
                smtp.Port = 587;
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception e1)
                {
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    smtp.Send(mail);
                }
                catch (Exception ex1)
                {

                }
            }
        }


        private void SendGMail(string file, string Subject, int number)
        {   

            if (!File.Exists(file))
                return;

            tblEmailBLL obj = new tblEmailBLL();        
            bool EnableSsl = false;
            string Port = "587";
            string Email = "phuchh@vietnamairlines.com";
            string Password = "";
            string Host = "smtp.vietnamairlines.com";            

            DataView dv2 = obj.tblEmail_Select().DefaultView;

            if (dv2.Count > 0)
            {
                Email = dv2[0]["Email"].ToString().Trim();
                Password = dv2[0]["Password"].ToString().Trim();
                Host = dv2[0]["Host"].ToString();
                Port = dv2[0]["Port"].ToString();
                EnableSsl = dv2[0]["EnableSsl"].ToString() == "1" ? true : false;

            }


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.From = new MailAddress(Email, "IT SRO", System.Text.Encoding.UTF8);
            mail.Subject = Subject;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            string[] To = ConfigurationSettings.AppSettings["MailTo"].ToString().Split(';');
            foreach (string s in To)
            {
                if (s.Trim().Length > 0)
                    mail.To.Add(s);
            }

            string[] Cc = ConfigurationSettings.AppSettings["MailCC"].ToString().Split(';');
            foreach (string s in Cc)
            {
                if (s.Trim().Length > 0)
                    mail.CC.Add(s);
            }

         

            mail.Priority = MailPriority.High;

            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();            
            smtp.Host = Host;            
            smtp.Credentials = new System.Net.NetworkCredential(Email, Password);            
            smtp.EnableSsl = EnableSsl;
            smtp.Port = int.Parse(Port);            

            mail.Body = "To: P.PTB/ P.BVDC </br> Số lượng EMD thiếu: " + number.ToString() + "</br></br> ------------------ </br>MCO Daily Update";

            if (File.Exists(file))
            {
                System.Net.Mail.Attachment attachFile = new System.Net.Mail.Attachment(file);
                mail.Attachments.Add(attachFile);
            }

            try
            {
                smtp.Port = 587;
                smtp.EnableSsl = true;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                try { 
                    smtp.Send(mail);
                }
                catch(Exception e1)
                {
                    smtp.EnableSsl = false;
                    smtp.Send(mail);
                }
                
            }
            catch (Exception ex)
            {
                try
                {
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate(object s,
                System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                System.Security.Cryptography.X509Certificates.X509Chain chain,
                System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    smtp.Send(mail);
                }
                catch (Exception ex1)
                {
                
                }
            }
        }
    }
}
