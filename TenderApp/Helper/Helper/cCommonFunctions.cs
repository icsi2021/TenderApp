using System;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Data;

using ICSIMemberPaymentLibrary.BL.Master;
using ICSIMemberPaymentLibrary.BL.Payment;

namespace ICSIMemberPaymentLibrary.Helper
{
    public class cCommonFunctions
    {
        //string _DateFormat = ConfigurationSettings.AppSettings["DateFormat"].ToString(); //get Format from web.config

        string _DateFormat = ConfigurationManager.AppSettings["DateFormat"].ToString();
        
        //''' <summary>
        //''' CHANGE THE FORMAT OF STRING TYPE DATE(DD/MM/YYYY) INTO GIVEN FORMAT
        //''' </summary>
        //''' <param name="prm_Date">Date String to Convert.</param>
        //''' <param name="prm_Format">Format in this the date should be returned.</param>
        //''' <returns></returns>
        //''' <remarks></remarks>
        public string DateFormat(string prm_Date, string prm_Format)
        {
            if ((prm_Date.ToString().Trim().Length != 0) && (prm_Format.ToString().Trim().Length != 0))
            {
                string l_Date = string.Empty;
                string dd = string.Empty;
                string mm = string.Empty;
                string yy = string.Empty;

                string[] strDate = prm_Date.Split('/');

                switch (_DateFormat)
                {
                    case "dd/MM/yyyy":
                        dd = strDate[0];
                        mm = strDate[1];
                        yy = strDate[2];
                        break;

                    case "MM/dd/yyyy":
                        mm = strDate[0];
                        dd = strDate[1];
                        yy = strDate[2];
                        break;

                    case "yyyy/MM/dd":
                        yy = strDate[0];
                        mm = strDate[1];
                        yy = strDate[2];
                        break;

                }

                l_Date = mm + "/" + dd + "/" + yy;

                return Convert.ToDateTime(l_Date).ToString(prm_Format);
            }
            else
            {
                return "#12:00:00 AM#";
            }

        }

        public string ConvertDate(string prm_Date, string prm_SuppliedFormat, string prm_ConvertedFormat)
        {
            string dt = string.Empty;
            string mn = string.Empty;
            string yr = string.Empty;
            string convertedDate = string.Empty;
            string[] arrDate = prm_Date.Split('/');

            if (prm_SuppliedFormat.Trim().ToUpper() == "DD/MM/YYYY")
            {
                dt = arrDate[0];
                mn = arrDate[1];
                yr = arrDate[2];
            }
            if (prm_SuppliedFormat.Trim().ToUpper() == "MM/DD/YYYY")
            {
                mn = arrDate[0];
                dt = arrDate[1];
                yr = arrDate[2];
            }
            if (prm_SuppliedFormat.Trim().ToUpper() == "YYYY/MM/DD")
            {
                yr = arrDate[0];
                mn = arrDate[1];
                dt = arrDate[2];
            }

            if (dt.Trim().Length == 1)
            {
                dt = "0" + dt;
            }
            if (mn.Trim().Length == 1)
            {
                mn = "0" + mn;
            }

            if (prm_ConvertedFormat.Trim().ToUpper() == "DD/MM/YYYY")
            {
                convertedDate = dt + "/" + mn + "/" + yr;
            }
            if (prm_ConvertedFormat.Trim().ToUpper() == "MM/DD/YYYY")
            {
                convertedDate = mn + "/" + dt + "/" + yr;
            }
            if (prm_ConvertedFormat.Trim().ToUpper() == "YYYY/MM/DD")
            {
                convertedDate = yr + "/" + mn + "/" + dt;
            }

            return convertedDate;
        }

        public string Convert_CHAR8_TO_DDMMYYYY(string prm_DateIn_CHAR8)
        {
            string dt = string.Empty;
            string mn = string.Empty;
            string yr = string.Empty;
            string DateIn_DDMMYYYY = string.Empty;

            yr = prm_DateIn_CHAR8.Substring(0, 4);
            mn = prm_DateIn_CHAR8.Substring(4, 2);
            dt = prm_DateIn_CHAR8.Substring(6, 2);
            DateIn_DDMMYYYY = dt + "/" + mn + "/" + yr;
            return DateIn_DDMMYYYY;
        }
        public string Convert_DDMMYYYY_TO_CHAR8(string prm_DateIn_DDMMYYYY)
        {
            string dt = string.Empty;
            string mn = string.Empty;
            string yr = string.Empty;
            string DateIn_CHAR8 = string.Empty;
            string[] arrDate = prm_DateIn_DDMMYYYY.Trim().Split('/');
            dt = arrDate[0];
            mn = arrDate[1];
            yr = arrDate[2];

            if (dt.Trim().Length == 1)
            {
                dt = "0" + dt;
            }
            if (mn.Trim().Length == 1)
            {
                mn = "0" + mn;
            }

            DateIn_CHAR8 = yr + mn + dt;
            return DateIn_CHAR8;
        }

        public string NumToWordBD(Int64 Num)
        {
            string[] Below20 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six " , "Seven ", "Eight ", "Nine ", "Ten ", 
                                   "Eleven ", "Twelve " , "Thirteen ", "Fourteen ","Fifteen ", "Sixteen " , "Seventeen ","Eighteen " , "Nineteen " };
            string[] Below100 = { "", "", "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string InWords = "";
            if (Num >= 1 && Num < 20)
                InWords += Below20[Num];
            if (Num >= 20 && Num <= 99)
                InWords += Below100[Num / 10] + Below20[Num % 10];
            if (Num >= 100 && Num <= 999)
                InWords += NumToWordBD(Num / 100) + " Hundred " + NumToWordBD(Num % 100);
            if (Num >= 1000 && Num <= 99999)
                InWords += NumToWordBD(Num / 1000) + " Thousand " + NumToWordBD(Num % 1000);
            if (Num >= 100000 && Num <= 9999999)
                InWords += NumToWordBD(Num / 100000) + " Lac " + NumToWordBD(Num % 100000);
            if (Num >= 10000000)
                InWords += NumToWordBD(Num / 10000000) + " Crore " + NumToWordBD(Num % 10000000);
            return InWords;
        }

        public string BuildReceipt(cPayment prm_Pay, DataTable dt)
        {
            StringBuilder SB = new StringBuilder();

            SB.Append("<table align='center' width='800px' style='border-style:solid' cellpadding='2' cellspacing='2' >");
            SB.Append(" <tr>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("<td colspan='6'><img src ='/Styles/logo.jpg' alt = 'Test Image' /></td>");
            SB.Append(" </tr>");
            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'>ICSI GSTIN (UP) Code:      </td>");
            //SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>ICSI-NOIDA Office C-36, Sector-62, Noida(U.P.) 201 309 </td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>(Tel: 0120 – 4239993 – 98 Fax: 0120-4264443, 4264445, email: info@icsi.edu)</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td align='left' width='10%'>Website</td>");
            SB.Append("     <td align='left' width='20%'>WWW.ICSI.EDU</td>");
            SB.Append("     <td width='20%'>&nbsp;</td>");
            SB.Append("     <td width='10%'>&nbsp;</td>");
            SB.Append("     <td width='20%' align='left'>GST NO.:</td>");
            SB.Append("     <td width='20%' align='left'>09AAATT1103F2ZX</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            //SB.Append("     <td align='left'>E-mail</td>");
            //SB.Append("     <td align='left'>csbf@icsi.edu</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td>&nbsp;</td>");
            SB.Append("     <td align='left'>Phone</td>");
            SB.Append("     <td align='left'>011-45341020</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" </tr>");

            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'><font size='5'><b>THE INSTITUTE OF COMPANy SECRETARIES</b></font></td>");
            //SB.Append(" </tr>");
            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'>(Registered under the Societies Registration Act 1860)</td>");
            //SB.Append(" </tr>");
            //SB.Append(" <tr align='center'>");
            //SB.Append("     <td colspan='6'>C/o The Institite of Company Secretaries of India</td>");
            //SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'><font size ='4'><b>RECEIPT</b></font></td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr>");
            SB.Append("     <td width='10%' align='left'>Ack No. :</td>");
            SB.Append("     <td width='40%' align='left'>" + prm_Pay.PayItem.ReceiptNumber + "</td>");
            SB.Append("     <td width='10%' align='right' colspan='3'>Date:</td>");
            SB.Append("     <td width='40%' align='left'>" + Convert.ToDateTime(prm_Pay.PayItem.ApprovalTime).Date.ToString("dd/MM/yyyy") + "</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            if (dt.Rows[0]["FEETYPE"].ToString() == "Earnest Money Deposit (EMD)")
            {
                SB.Append("<td colspan='6' align='left'>Received with thanks from Mr./Ms./M/s " + dt.Rows[0]["BIDERFARM"] + ", <b>Address : </b>" + dt.Rows[0]["ADDRESS"] + ", <b>(GST No.: " + dt.Rows[0]["GSTNO"] + "</b>), a sum of Rs. <b>" + prm_Pay.PayItem.PaymentAmount.ToString("#,##0.00") + " (Rupees " + this.NumToWordBD((Int64)(prm_Pay.PayItem.PaymentAmount)) + "Only ) </b> towards " + prm_Pay.PayItem.PaymentTypeDesc + " for <b>" + dt.Rows[0]["TENDERNAME"] + "</b> <b>Dated " + Convert.ToDateTime(dt.Rows[0]["Created_date"]).Date.ToString("dd/MM/yyyy") + "</b> (<b>No." + dt.Rows[0]["TENDERNO"] + "</b>).</td>");
            }
            else
            {
                SB.Append("<td colspan='6' align='left'>Received with thanks from Mr./Ms./M/s " + dt.Rows[0]["BIDERFARM"] + ", <b>Address : </b>" + dt.Rows[0]["ADDRESS"] + ", <b>(GST No.: " + dt.Rows[0]["GSTNO"] + "</b>), a sum of Rs. <b>" + prm_Pay.PayItem.PaymentAmount.ToString("#,##0.00") + " (Rupees " + this.NumToWordBD((Int64)(prm_Pay.PayItem.PaymentAmount)) + "Only ) </b>including 18% GST towards " + prm_Pay.PayItem.PaymentTypeDesc + " for <b>" + dt.Rows[0]["TENDERNAME"] + "</b>  <b>Dated " + Convert.ToDateTime(dt.Rows[0]["Created_date"]).Date.ToString("dd/MM/yyyy") + "</b> (<b>No." + dt.Rows[0]["TENDERNO"] + "</b>).</td>");
            }
               
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            //SB.Append(" <tr align='right'>");
            //SB.Append("     <td colspan='6'>COMPANY SECRETARIES BENEVOLENT FUND</td>");
            //SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='left'>");
            SB.Append("     <td colspan='6'>");
            SB.Append("         <table align='left'>");
            SB.Append("             <tr>");
            SB.Append("                 <td> Kindly refer the above acknowledgement No. (" + prm_Pay.PayItem.ReceiptNumber + ") for future correspondence.");
            SB.Append("                     ");
            SB.Append("                 </td>");
            SB.Append("             </tr>");
            SB.Append("         </table>");
            SB.Append("     </td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='left'>");
            SB.Append("     <td colspan='6'>This is a system generated electronic receipt; hence no physical signature is required for the purpose of authentication.  This is mandatory on the part of the  bidder to enclose the copy of the system generated receipt along with the bid for verification and checking by ICSI.</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='center'>");
            SB.Append("     <td colspan='6'>ICSI-HQ, 22 INSTITUTIONAL AREA, LODI ROAD, NEW DELHI-110003</td>");
            SB.Append(" </tr>");
            SB.Append(" <tr align='justified'>");
            SB.Append("     <td colspan='6'>&nbsp;</td>");
            SB.Append(" </tr>");
            SB.Append("</table>");

            return SB.ToString();
        }
    }
}
