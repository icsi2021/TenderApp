using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class AdminDL
    {
       public DataSet GetDeptMent()
       {
           string procName = "";
           try
           {

               procName = "TNDR_USR_MSTR_DEPTLIST";
               return Sqlhelper.ExecuteDataSet(procName);
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataSet GetTender_ForByUser(int userid)
       {
           string procName = "";
           try
           {

               procName = "TNDR_PUBLISH_MASTER_GETDATA_BYUser";

               SqlParameter[] sparams = new SqlParameter[1];
               sparams[0] = Sqlhelper.BuildSqlParameter("@USERID", userid, SqlDbType.Int);

               return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }

       public DataSet GetTenderDataList()
       {
           string procName = "";
           try
           {

               procName = "TNDR_PUBLISH_MASTER_DATALIST";
               return Sqlhelper.ExecuteDataSet(procName);
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataSet GetTenderDataList_ForDPT( int userid)
       {
           string procName = "";
           try
           {

               procName = "TNDR_PUBLISH_MASTER_DATALIST_FOR_PUBLISHDEPT";
               SqlParameter[] sparams = new SqlParameter[1];
               sparams[0] = Sqlhelper.BuildSqlParameter("@USERID", userid, SqlDbType.Int);
               return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataSet GetLoginDetails(String USERNAME,String PASSWORD)
       {
           string procName = "";
           try
           {

               procName = "TNDR_USR_MSTR_LOGIN";
               
               SqlParameter[] sparams = new SqlParameter[2];
               sparams[0] = Sqlhelper.BuildSqlParameter("@USERNAME", USERNAME, SqlDbType.VarChar);
               sparams[1] = Sqlhelper.BuildSqlParameter("@PASSWORD", PASSWORD, SqlDbType.VarChar);
              return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }

       public DataSet ChangeLoginDetails(int userid, String PASSWORD,string changePWD)
       {
           string procName = "";
           try
           {

               procName = "TNDR_USR_MSTR_CHANGEPASS";

               SqlParameter[] sparams = new SqlParameter[3];
               sparams[0] = Sqlhelper.BuildSqlParameter("@USERID", userid, SqlDbType.Int);
               sparams[1] = Sqlhelper.BuildSqlParameter("@PASSWORD", PASSWORD, SqlDbType.VarChar);
               sparams[2] = Sqlhelper.BuildSqlParameter("@NEWPASS", changePWD, SqlDbType.VarChar);
               return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataSet GetParticipantList(int TENDERID, int FEETYPE)
       {
           string procName = "";
           try
           {

               procName = "TNDR_BIDDER_TRANSACTIONLIST";

               SqlParameter[] sparams = new SqlParameter[2];
               sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", TENDERID, SqlDbType.Int);
               sparams[1] = Sqlhelper.BuildSqlParameter("@FEETYPE", FEETYPE, SqlDbType.Int);
               return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataSet GetTenderDataByTenderID(int TENDERID)
       {
           string procName = "";
           try
           {

               procName = "TNDR_PUBLISH_MASTER_GETDATABYTENDERID";

               SqlParameter[] sparams = new SqlParameter[1];
               sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", TENDERID, SqlDbType.Int);
               //sparams[1] = Sqlhelper.BuildSqlParameter("@PASSWORD", PASSWORD, SqlDbType.VarChar);
               return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }
       public DataSet GetTenderDataByTenderNo(string TENDERNO)
       {

           string procName = "";
           try
           {

               procName = "TNDR_PUBLISH_MASTER_Number";

               SqlParameter[] sparams = new SqlParameter[1];
               sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERNO", TENDERNO, SqlDbType.Text);
               //sparams[1] = Sqlhelper.BuildSqlParameter("@PASSWORD", PASSWORD, SqlDbType.VarChar);
               return Sqlhelper.ExecuteDataSet(procName, sparams);
           }
           catch (Exception ex)
           {
               return null;
           }
       }

       public int InsertTenderDetail(int TENDERID, String TENDERNAME, String TENDERNO, DateTime PUBLISHINGDATE, DateTime CLOSINGDATE, DateTime EXTENDEDDATE, String PUBLISHINGDEPT,
                                      Decimal TENDERFEES, Decimal EMDAMT,int userid)
       {
         
           string procName = "";
           try
           {

               procName = "TNDR_PUBLISH_MASTER_INSERTUPDATE";

               SqlParameter[] sparams = new SqlParameter[10];
               sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", TENDERID, SqlDbType.Int);
               sparams[1] = Sqlhelper.BuildSqlParameter("@TENDERNAME", TENDERNAME, SqlDbType.VarChar);
               sparams[2] = Sqlhelper.BuildSqlParameter("@TENDERNO", TENDERNO, SqlDbType.VarChar);
               sparams[3] = Sqlhelper.BuildSqlParameter("@PUBLISHINGDATE", PUBLISHINGDATE, SqlDbType.DateTime);
               sparams[4] = Sqlhelper.BuildSqlParameter("@CLOSINGDATE", CLOSINGDATE, SqlDbType.DateTime);
               sparams[5] = Sqlhelper.BuildSqlParameter("@EXTENDEDDATE", EXTENDEDDATE, SqlDbType.DateTime);
               sparams[6] = Sqlhelper.BuildSqlParameter("@PUBLISHINGDEPT", PUBLISHINGDEPT, SqlDbType.VarChar);
               sparams[7] = Sqlhelper.BuildSqlParameter("@TENDERFEES", TENDERFEES, SqlDbType.Decimal);
               sparams[8] = Sqlhelper.BuildSqlParameter("@EMDAMT", EMDAMT, SqlDbType.Decimal);
               sparams[9] = Sqlhelper.BuildSqlParameter("@USERID", userid, SqlDbType.Int);
               Sqlhelper.ExecuteSP(procName,sparams);
               return 1;
           }
           catch (Exception ex)
           {
              return 0;
           }
       }


    }
}
