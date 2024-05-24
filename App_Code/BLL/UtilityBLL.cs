using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;

using System.Collections;
/// <summary>
/// Summary description for UtilityBLL
/// </summary>
public static class UtilityBLL
{
    public static string[] ChuSo = new string[10] { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
    public static string[] Tien = new string[6] { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
       
    
    // Check value is number or not 
    public static bool isNumeric(string s)
    {
        Regex objNotWholePattern = new Regex("[^0-9]");
        if (s.Length > 0)
            return !objNotWholePattern.IsMatch(s);

        return false;
    }

    public static string AddZero(string s)
    {
        if (int.Parse(s) < 10)
            return "0" + (int.Parse(s)).ToString();
        else
            return s;
    }

    public static string DDMMYYYY_To_MMDDYYYY(string str)
    {
        if (str.Length == 0)
                return "";
        string[] s;
        s = str.Split('/');
        
        
        return AddZero(s[1]) + "/" + AddZero(s[0]) + "/" + s[2];
    } 

    public static string MMDDYYYY_To_DDMMYYYY(string str)
    {
        string[] s;
        s = str.Split('/');

        return AddZero(s[1]) + "/" + AddZero(s[0]) + "/" + s[2];
    }


    public static string str = " ";

    public static string ToString(decimal number)
    {
        string s = number.ToString("#");
        string[] so = new string[] { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
        string[] hang = new string[] { "", "nghìn", "triệu", "tỷ" };
        int i, j, donvi, chuc, tram;

        bool booAm = false;
        decimal decS = 0;
        //Tung addnew
        try
        {
            decS = Convert.ToDecimal(s.ToString());
        }
        catch
        {
        }
        if (decS < 0)
        {
            decS = -decS;
            s = decS.ToString();
            booAm = true;
        }
        i = s.Length;
        if (i == 0)
            str = so[0] + str;
        else
        {
            j = 0;
            while (i > 0)
            {
                donvi = Convert.ToInt32(s.Substring(i - 1, 1));
                i--;
                if (i > 0)
                    chuc = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    chuc = -1;
                i--;
                if (i > 0)
                    tram = Convert.ToInt32(s.Substring(i - 1, 1));
                else
                    tram = -1;
                i--;
                if ((donvi > 0) || (chuc > 0) || (tram > 0) || (j == 3))
                    str = hang[j] + str;
                j++;
                if (j > 3) j = 1;
                if ((donvi == 1) && (chuc > 1))
                    str = "một " + str;
                else
                {
                    if ((donvi == 5) && (chuc > 0))
                        str = "lăm " + str;
                    else if (donvi > 0)
                        str = so[donvi] + " " + str;
                }
                if (chuc < 0)
                    break;
                else
                {
                    if ((chuc == 0) && (donvi > 0)) str = "lẻ " + str;
                    if (chuc == 1) str = "mười " + str;
                    if (chuc > 1) str = so[chuc] + " mươi " + str;
                }
                if (tram < 0) break;
                else
                {
                    if ((tram > 0) || (chuc > 0) || (donvi > 0)) str = so[tram] + " trăm " + str;
                }
                str = " " + str;
            }
        }
        if (booAm) str = "Âm " + str;
        return str;// = str+ "đồng chẵn";

    }
  
    // Hàm đọc số thành chữ
    public static string DocTienBangChu(long SoTien, string strTail)
    {
        int lan, i;
        long so;
        string KetQua = "", tmp = "";
        int[] ViTri = new int[6];
        if (SoTien < 0) return "Số tiền âm !";
        if (SoTien == 0) return "Không đồng !";
        if (SoTien > 0)
        {
            so = SoTien;
        }
        else
        {
            so = -SoTien;
        }
        //Kiểm tra số quá lớn
        if (SoTien > 8999999999999999)
        {
            SoTien = 0;
            return "";
        }
        ViTri[5] = (int)(so / 1000000000000000);
        so = so - long.Parse(ViTri[5].ToString()) * 1000000000000000;
        ViTri[4] = (int)(so / 1000000000000);
        so = so - long.Parse(ViTri[4].ToString()) * +1000000000000;
        ViTri[3] = (int)(so / 1000000000);
        so = so - long.Parse(ViTri[3].ToString()) * 1000000000;
        ViTri[2] = (int)(so / 1000000);
        ViTri[1] = (int)((so % 1000000) / 1000);
        ViTri[0] = (int)(so % 1000);
        if (ViTri[5] > 0)
        {
            lan = 5;
        }
        else if (ViTri[4] > 0)
        {
            lan = 4;
        }
        else if (ViTri[3] > 0)
        {
            lan = 3;
        }
        else if (ViTri[2] > 0)
        {
            lan = 2;
        }
        else if (ViTri[1] > 0)
        {
            lan = 1;
        }
        else
        {
            lan = 0;
        }
        for (i = lan; i >= 0; i--)
        {
            tmp = DocSo3ChuSo(ViTri[i]);
            KetQua += tmp;
            if (ViTri[i] != 0) KetQua += Tien[i];
            if ((i > 0) && (!string.IsNullOrEmpty(tmp))) KetQua += ",";//&& (!string.IsNullOrEmpty(tmp))
        }
        if (KetQua.Substring(KetQua.Length - 1, 1) == ",") KetQua = KetQua.Substring(0, KetQua.Length - 1);
        KetQua = KetQua.Trim() + strTail;
        return KetQua.Substring(0, 1).ToUpper() + KetQua.Substring(1);
    }
    private static string DocSo3ChuSo(int baso)
    {
        int tram, chuc, donvi;
        string KetQua = "";
        tram = (int)(baso / 100);
        chuc = (int)((baso % 100) / 10);
        donvi = baso % 10;
        if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
        if (tram != 0)
        {
            KetQua += ChuSo[tram] + " trăm";
            if ((chuc == 0) && (donvi != 0)) KetQua += " lẻ";
        }
        if ((chuc != 0) && (chuc != 1))
        {
            KetQua += ChuSo[chuc] + " mươi";
            if ((chuc == 0) && (donvi != 0)) KetQua = KetQua + " lẻ";
        }
        if (chuc == 1) KetQua += " mười";
        switch (donvi)
        {
            case 1:
                if ((chuc != 0) && (chuc != 1))
                {
                    KetQua += " mốt";
                }
                else
                {
                    KetQua += ChuSo[donvi];
                }
                break;
            case 5:
                if (chuc == 0)
                {
                    KetQua += ChuSo[donvi];
                }
                else
                {
                    KetQua += " lăm";
                }
                break;
            default:
                if (donvi != 0)
                {
                    KetQua += ChuSo[donvi];
                }
                break;
        }
        return KetQua;
    }


    public static DataView SabreUpdate2(string PNR)
    {
        DataView dv = null;
        //DataSet ds = null;

        //if (PNR.Length == 6)
        //{

        //    // Use the 'client' variable to call operations on the service.
        //    SabreServiceClient client = new SabreServiceClient();
        //    SABRE_EPRBLL obj = new SABRE_EPRBLL();
        //    string[] epr = obj.SABRE_EPR();

        //    //if (!client.Check_if_Sabre_in_queue())
        //    //{
        //    //    if (!client.Check_if_Sabre_is_bussy())
        //        //{
        //            ds = client.Get_PNR_Data(PNR, true, epr[0], epr[1], true);

        //            if (ds.Tables.Count > 0)
        //            {
        //                dv = ds.Tables[0].DefaultView;
        //                ds.Dispose();         
        //            }

        //    //    }
        //    //}

        //    epr = null;
        //    obj = null;

        //    // Always close the client.
        //    client.Close();
        //    client = null;

            
        //}

        //ds = null;
        return dv;
    }


    public static void SabreUpdate(string PNR)
    {
        //if (PNR.Length == 6)
        //{

        //    // Use the 'client' variable to call operations on the service.
        //    SabreServiceClient client = new SabreServiceClient();
        //    SABRE_EPRBLL obj = new SABRE_EPRBLL();
        //    string[] epr = obj.SABRE_EPR();

        //    //if (!client.Check_if_Sabre_in_queue())
        //    //{
        //    //    if (!client.Check_if_Sabre_is_bussy())
        //    //    {
        //            client.Get_PNR_Data(PNR, true, epr[0], epr[1], true);
        //    //    }
        //    //}

        //    epr = null;
        //    obj = null;

        //    // Always close the client.
        //    client.Close();
        //    client = null;
        //}
    }


    //public static Boolean IsNumeric(string stringToTest)
    //{
    //    int result;
    //    return int.TryParse(stringToTest, out result);
    //}


    public static ArrayList tblZ_Work_MasterList()
    {
        ArrayList al = new ArrayList();

        DataView dv;


        return al;
    }    
}