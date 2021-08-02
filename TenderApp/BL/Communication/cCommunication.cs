using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using ICSIMemberPaymentLibrary.DAL.Communication;

namespace ICSIMemberPaymentLibrary.BL.Communication
{
    public class cCommunication
    {
        cCommunicationQueries objCommunicationQueries;

        public cCommunication()
        {
            objCommunicationQueries = new cCommunicationQueries();
        }

        public static byte getPriority(MessageTypeEnum type)
        {
            byte num;
            switch (type)
            {
                case MessageTypeEnum.Promotional:
                    {
                        num = 1;
                        break;
                    }
                case MessageTypeEnum.Informational:
                    {
                        num = 2;
                        break;
                    }
                case MessageTypeEnum.Group:
                    {
                        num = 3;
                        break;
                    }
                case MessageTypeEnum.Service:
                    {
                        num = 6;
                        break;
                    }
                case MessageTypeEnum.Transactional:
                    {
                        num = 9;
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("MessageType not supported", "type");
                    }
            }
            return num;
        }

        //private void sendEmailAndSms(object obj, int userId, bool isApprove, string requestType, bool isRemark)
        //{

        //}

        public void enqueueEmail(string receiverName, string receiverEmail, string subject, string body, MessageTypeEnum type, MessageStatusEnum status)
        {
            SqlParameter[] prms = new SqlParameter[10];

            prms[0] = new SqlParameter("@receiverName", SqlDbType.NVarChar,255);
            prms[0].Value = receiverName;

            prms[1] = new SqlParameter("@receiverEmail", SqlDbType.NVarChar,255);
            prms[1].Value = receiverEmail;

            prms[2] = new SqlParameter("@senderName", SqlDbType.NVarChar,255);
            prms[2].Value = "";

            prms[3] = new SqlParameter("@senderEmail", SqlDbType.NVarChar,255);
            prms[3].Value = "";

            prms[4] = new SqlParameter("@subject", SqlDbType.NVarChar,1024);
            prms[4].Value = subject;

            prms[5] = new SqlParameter("@body", SqlDbType.NVarChar,-1);
            prms[5].Value = body;

            prms[6] = new SqlParameter("@priority", SqlDbType.TinyInt);
            prms[6].Value = type;

            prms[7] = new SqlParameter("@createdTime", SqlDbType.DateTime);
            prms[7].Value = DateTime.Now;

            prms[8] = new SqlParameter("@updatedTime", SqlDbType.DateTime);
            prms[8].Value = DateTime.Now;

            prms[9] = new SqlParameter("@status", SqlDbType.TinyInt);
            prms[9].Value = status;

            objCommunicationQueries.AddEmailQueueItem(prms);
            

        }


        public void enqueueSMS(string receiverName, string receiverMobileNumber, string Message, MessageTypeEnum type, MessageStatusEnum status)
        {
            SqlParameter[] prms = new SqlParameter[8];

            prms[0] = new SqlParameter("@receiverName", SqlDbType.NVarChar, 255);
            prms[0].Value = receiverName;

            prms[1] = new SqlParameter("@receiverMobile", SqlDbType.NVarChar, 50);
            prms[1].Value = receiverMobileNumber;

            prms[2] = new SqlParameter("@senderId", SqlDbType.NVarChar, 50);
            prms[2].Value = "";

            prms[3] = new SqlParameter("@messageText", SqlDbType.NVarChar, 1024);
            prms[3].Value = Message;

            prms[4] = new SqlParameter("@priority", SqlDbType.TinyInt);
            prms[4].Value = type;

            prms[5] = new SqlParameter("@createdTime", SqlDbType.DateTime);
            prms[5].Value = DateTime.Now;

            prms[6] = new SqlParameter("@updatedTime", SqlDbType.DateTime);
            prms[6].Value = DateTime.Now;

            prms[7] = new SqlParameter("@status", SqlDbType.TinyInt);
            prms[7].Value = status;

            objCommunicationQueries.AddSmsQueueItem(prms);
        }
    }
}
