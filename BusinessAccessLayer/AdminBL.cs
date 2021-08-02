using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessAccessLayer
{
   public class AdminBL
    {

       public DataSet GetLoginDetails(String USERNAME, String PASSWORD)
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.GetLoginDetails(USERNAME, PASSWORD);
        }
        public DataSet ChangeLoginDetails(int userid, String PASSWORD,string changePWD)
       {
           AdminDL objAdmin = new AdminDL();
           return objAdmin.ChangeLoginDetails(userid, PASSWORD, changePWD);
       }
       public DataSet GetTender_ForByUser(int userid)
       {
           AdminDL objAdmin = new AdminDL();
           return objAdmin.GetTender_ForByUser(userid);
       }
       public DataSet GetParticipantData(int TENDERID, int FEETYPE)
       {
           AdminDL objAdmin = new AdminDL();
           return objAdmin.GetParticipantList(TENDERID, FEETYPE);
       }
       public int InsertUpdateTenderDetail(int TENDERID,String TENDERNAME, String TENDERNO, DateTime PUBLISHINGDATE, DateTime CLOSINGDATE, DateTime EXTENDEDDATE, String PUBLISHINGDEPT,
                                     Decimal TENDERFEES, Decimal EMDAMT,int tenderid)
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.InsertTenderDetail(TENDERID, TENDERNAME, TENDERNO, PUBLISHINGDATE, CLOSINGDATE, EXTENDEDDATE, PUBLISHINGDEPT, TENDERFEES, EMDAMT, tenderid);
        }
       
        public DataSet GetTenderDataListView()
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.GetTenderDataList();
        }

        public DataSet GetTenderDataListView_ForDPT(int userid)
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.GetTenderDataList_ForDPT(userid);
        }
        public DataSet GetTenderDataByID(int TenderID)
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.GetTenderDataByTenderID(TenderID);
        }
        public DataSet GetTenderName(string TENDERNO)
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.GetTenderDataByTenderNo(TENDERNO);
        }
        public DataSet GetDeptMentList()
        {
            AdminDL objAdmin = new AdminDL();
            return objAdmin.GetDeptMent();
        }
    }
}
