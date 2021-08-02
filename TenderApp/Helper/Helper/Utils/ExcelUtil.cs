using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Text;

namespace ICSIMemberPaymentLibrary.Helper.Utils
{
    public static class ExcelUtil
    {
        public static DataSet getExcelFileAsDataSet(string filePath, string[] worksheets)
        {
            DataSet dataSet;
            string lower = Path.GetExtension(filePath).ToLower();
            OleDbConnection oleDbConn = new OleDbConnection(string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties={1}", filePath, (lower == ".xlsx" ? "Excel 12.0 Xml" : "Excel 8.0")));
            try
            {
                oleDbConn.Open();
                DataSet oDS = new DataSet();
                string[] strArrays = worksheets;
                for (int i = 0; i < (int)strArrays.Length; i++)
                {
                    string str = strArrays[i];
                    OleDbCommand oleDbComd = new OleDbCommand(string.Format("SELECT * FROM [{0}$]", str), oleDbConn);
                    OleDbDataAdapter oleDbDA = new OleDbDataAdapter()
                    {
                        SelectCommand = oleDbComd
                    };
                    oleDbDA.Fill(oDS, str);
                }
                dataSet = oDS;
            }
            finally
            {
                ((IDisposable)oleDbConn).Dispose();
            }
            return dataSet;
        }

        public static DataSet getExcelFileAsDataSet(string ExcelParserBaseUrl, string filePath, string[] worksheets)
        {
            DataSet dataSet;
            if (!ExcelParserBaseUrl.EndsWith("/"))
            {
                ExcelParserBaseUrl = string.Concat(ExcelParserBaseUrl, "/");
            }
            ExcelParserBaseUrl = string.Concat(ExcelParserBaseUrl, "ExcelParser.ashx");
            string[] strArrays = new string[] { "filePath", filePath, "worksheets", string.Join("|", worksheets) };
            string str = UriUtil.urlAppend(ExcelParserBaseUrl, strArrays);
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(str);
            httpWebRequest.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            using (response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                    if (!response.ContentType.Contains("text/xml"))
                    {
                        throw new Exception(streamReader.ReadToEnd());
                    }
                    using (streamReader)
                    {
                        DataSet oDS = new DataSet();
                        oDS.ReadXml(streamReader);
                        dataSet = oDS;
                    }
                }
            }
            return dataSet;
        }

    }
}
