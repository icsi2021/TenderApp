using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
namespace BusinessAccessLayer
{
   public  class BidderBL
    {
       public DataSet GetTenderList()
       {
           BidderDL objBidder = new BidderDL();
           return objBidder.GetTenderLOV();
       }
       public DataSet GetTenderAmt(int TENDERID,String feetype)
       {
           BidderDL objBidder = new BidderDL();
           return objBidder.GetTenderFeeAmt(TENDERID, feetype);
       }
       public DataSet GetTenderDateTime(int tenderid)
       {
           BidderDL objBidder = new BidderDL();
           return objBidder.GetTenderDateTime(tenderid);
       }
       public DataSet GetTenderNumber(int tenderid)
       {
           BidderDL objBidder = new BidderDL();
           return objBidder.GetTenderNumber(tenderid);
       }
       public DataSet GetOTPDETAIL(long mobile, String email, int otp, int id)
       {
           BidderDL objBidder = new BidderDL();
           return objBidder.GetOTPDETAIL(mobile, email, otp, id);
       }
       public DataSet GetBidderDetailByRequestID(long requestid)
       {
           BidderDL objBidder = new BidderDL();
           return objBidder.GetBidderDetailByRequestID(requestid);
       }
       public DataSet InsertUpdateBidderTransactionData(long RequestId, long PaymentID, int STATUS, String BIDERFARM,
           String ADDRESS, String STATE, String COUNTRY, int PINCODE, String GSTNO, String PAN, String CONTACTPERSON, long MOBILE,
           String EMAIL, String TENDERID, String FEETYPE, Decimal FEEAMT,int feeid)
       {
           BidderDL objBidder = new BidderDL();
          return objBidder.InsertUpdateBidderTransactionData(RequestId, PaymentID, STATUS, BIDERFARM, ADDRESS, STATE, COUNTRY, PINCODE, GSTNO, PAN, CONTACTPERSON, MOBILE,
               EMAIL, TENDERID, FEETYPE, FEEAMT, feeid);
       }
    }
}
