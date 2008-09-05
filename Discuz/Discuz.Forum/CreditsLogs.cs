using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// ����ת����ʷ��¼������
    /// </summary>
    public class CreditsLogs
    {

        /// <summary>
        /// ��ӻ���ת�ʶһ���¼
        /// </summary>
        /// <param name="uid">�û�id</param>
        /// <param name="fromto">����/��</param>
        /// <param name="sendcredits">������������</param>
        /// <param name="receivecredits">�õ���������</param>
        /// <param name="send">������������</param>
        /// <param name="receive">�õ���������</param>
        /// <param name="paydate">ʱ��</param>
        /// <param name="operation">���ֲ���(1=�һ�, 2=ת��)</param>
        /// <returns>ִ��Ӱ�����</returns>
        public static int AddCreditsLog(int uid, int fromto, int sendcredits, int receivecredits, float send, float receive, string paydate, int operation)
        {
            return DatabaseProvider.GetInstance().AddCreditsLog(uid, fromto, sendcredits, receivecredits, send, receive, paydate, operation);
        }

        /// <summary>
        /// ����ָ����Χ�Ļ�����־
        /// </summary>
        /// <param name="pagesize">ҳ��С</param>
        /// <param name="currentpage">��ǰҳ��</param>
        /// <param name="uid">�û�id</param>
        /// <returns>������־</returns>
        public static DataTable GetCreditsLogList(int pagesize, int currentpage, int uid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetCreditsLogList(pagesize, currentpage, uid);
            if (dt != null)
            {

                DataColumn dc = new DataColumn();
                dc.ColumnName = "operationinfo";
                dc.DataType = System.Type.GetType("System.String");
                dc.DefaultValue = "";
                dc.AllowDBNull = false;
                dt.Columns.Add(dc);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["operation"].ToString() == "1")
                    {
                        dr["operationinfo"] = "�һ�";
                    }
                    if (dr["operation"].ToString() == "2")
                    {
                        dr["operationinfo"] = "ת��";
                    }
                }
            }
            dt.Dispose();
            return dt;
        }

        /// <summary>
        /// ���ָ���û��Ļ��ֽ�����ʷ��¼������
        /// </summary>
        /// <param name="uid">�û�id</param>
        /// <returns>��ʷ��¼������</returns>
        public static int GetCreditsLogRecordCount(int uid)
        {
            return DatabaseProvider.GetInstance().GetCreditsLogRecordCount(uid);
        }


    }

}
