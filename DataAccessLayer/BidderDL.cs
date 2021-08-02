using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DataAccessLayer
{
    public class BidderDL
    {

        public DataSet GetTenderLOV()
        {
            string procName = "";
            try
            {

                procName = "TNDR_PUBLISH_MASTER_GETDATA";

                SqlParameter[] sparams = new SqlParameter[2];
                sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", 0, SqlDbType.Int);
                sparams[1] = Sqlhelper.BuildSqlParameter("@feetype", "", SqlDbType.VarChar);
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetOTPDETAIL(long mobile, String email,int otp,int id)
        {
            string procName = "";
            try
            {

                procName = "TNDR_GET_OTP";

                SqlParameter[] sparams = new SqlParameter[4];
                sparams[0] = Sqlhelper.BuildSqlParameter("@MOBILE", mobile, SqlDbType.BigInt);
                sparams[1] = Sqlhelper.BuildSqlParameter("@EMAIL", email, SqlDbType.VarChar);
                sparams[2] = Sqlhelper.BuildSqlParameter("@OTP", otp, SqlDbType.Int);
                sparams[3] = Sqlhelper.BuildSqlParameter("@ID", id, SqlDbType.Int);
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetTenderFeeAmt(int tenderid,String feetype)
        {
            string procName = "";
            try
            {

                procName = "TNDR_PUBLISH_MASTER_GETDATA";

                SqlParameter[] sparams = new SqlParameter[2];
                sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", tenderid, SqlDbType.Int);
                sparams[1] = Sqlhelper.BuildSqlParameter("@feetype", feetype, SqlDbType.VarChar);
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataSet GetTenderDateTime(int tenderid)
        {
            string procName = "";
            try
            {

                procName = "TNDR_GETDATE";

                SqlParameter[] sparams = new SqlParameter[1];
                sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", tenderid, SqlDbType.Int);
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetTenderNumber(int tenderid)
        {
            string procName = "";
            try
            {

                procName = "TNDR_GET_TenderNo";

                SqlParameter[] sparams = new SqlParameter[1];
                sparams[0] = Sqlhelper.BuildSqlParameter("@TENDERID", tenderid, SqlDbType.Int);
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetBidderDetailByRequestID(long requestid)
        {
            string procName = "";
            try
            {

                procName = "TNDR_BIDDER_GETTRANSACTIONRequestbyId";

                SqlParameter[] sparams = new SqlParameter[1];
                sparams[0] = Sqlhelper.BuildSqlParameter("@requestid", requestid, SqlDbType.Int);
            
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public DataSet InsertUpdateBidderTransactionData(long RequestId, long PaymentID, int STATUS, String BIDERFARM,
            String ADDRESS,String STATE,String COUNTRY,int PINCODE,String GSTNO,String PAN,String CONTACTPERSON,long MOBILE, 
            String EMAIL,String TENDERID,String FEETYPE,Decimal FEEAMT,int FEEID)
        {
            string procName = "";
            try
            {

                procName = "TNDR_BIDDER_INSERTUPDATETRANSACTION";

                SqlParameter[] sparams = new SqlParameter[17];
                sparams[0] = Sqlhelper.BuildSqlParameter("@RequestId", RequestId, SqlDbType.BigInt);
                sparams[1] = Sqlhelper.BuildSqlParameter("@PaymentID", PaymentID, SqlDbType.BigInt);
                sparams[2] = Sqlhelper.BuildSqlParameter("@STATUS", STATUS, SqlDbType.Int);
                sparams[3] = Sqlhelper.BuildSqlParameter("@BIDERFARM", BIDERFARM, SqlDbType.VarChar);
                sparams[4] = Sqlhelper.BuildSqlParameter("@ADDRESS", ADDRESS, SqlDbType.VarChar);
                sparams[5] = Sqlhelper.BuildSqlParameter("@STATE", STATE, SqlDbType.VarChar);
                sparams[6] = Sqlhelper.BuildSqlParameter("@COUNTRY", COUNTRY, SqlDbType.VarChar);
                sparams[7] = Sqlhelper.BuildSqlParameter("@PINCODE", PINCODE, SqlDbType.Int);
                sparams[8] = Sqlhelper.BuildSqlParameter("@GSTNO", GSTNO, SqlDbType.VarChar);
                sparams[9] = Sqlhelper.BuildSqlParameter("@PAN", PAN, SqlDbType.VarChar);
                sparams[10] = Sqlhelper.BuildSqlParameter("@CONTACTPERSON", CONTACTPERSON, SqlDbType.VarChar);
                sparams[11] = Sqlhelper.BuildSqlParameter("@MOBILE", MOBILE, SqlDbType.BigInt);
                sparams[12] = Sqlhelper.BuildSqlParameter("@EMAIL", EMAIL, SqlDbType.VarChar);
                sparams[13] = Sqlhelper.BuildSqlParameter("@TENDERID", TENDERID, SqlDbType.Int);
                sparams[14] = Sqlhelper.BuildSqlParameter("@FEETYPE", FEETYPE, SqlDbType.VarChar);
                sparams[15] = Sqlhelper.BuildSqlParameter("@FEEAMT", FEEAMT, SqlDbType.Decimal);
                sparams[16] = Sqlhelper.BuildSqlParameter("@FeeID", FEEID, SqlDbType.Int);
                return Sqlhelper.ExecuteDataSet(procName, sparams);
            }
            catch (Exception ex)
            {
                 return null;
            }
        }
    }
}
