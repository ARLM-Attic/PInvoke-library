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
	/// ������־������
	/// </summary>
	public class PaymentLogs
	{

		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <param name="tid">����id</param>
		/// <param name="posterid">�������û�id</param>
		/// <param name="price">�۸�</param>
		/// <param name="netamount"></param>
		/// <returns></returns>
		public static int BuyTopic(int uid, int tid, int posterid, int price, float netamount)
		{

			int tmpprice = price;
            if (price > Scoresets.GetMaxIncPerTopic())
			{
                tmpprice = Scoresets.GetMaxIncPerTopic();
			}

			

			IDataReader  reader = Users.GetShortUserInfoToReader(uid);
			if (reader == null)
			{
				return -2;
			}

			if (!reader.Read())
			{
				reader.Close();
				return -2;
			}

            if (Utils.StrToFloat(reader["extcredits" + Scoresets.GetCreditsTrans().ToString()], 0) < price)
			{
				reader.Close();
				return -1;
			}
			reader.Close();

            DatabaseProvider.GetInstance().BuyTopic(uid, tid, posterid, price, netamount, Scoresets.GetCreditsTrans());
            UserCredits.UpdateUserCredits(uid);
			UserCredits.UpdateUserCredits(posterid);
            return DatabaseProvider.GetInstance().AddPaymentLog(uid, tid, posterid, price, netamount);
			
		}

		/// <summary>
		/// �ж��û��Ƿ��ѹ�������
		/// </summary>
		/// <param name="tid">����id</param>
		/// <param name="uid">�û�id</param>
		/// <returns></returns>
		public static bool IsBuyer(int tid, int uid)
		{
            return DatabaseProvider.GetInstance().IsBuyer(tid, uid);
		}


		/// <summary>
		/// ��ȡָ���û��Ľ�����־
		/// </summary>
		/// <param name="pagesize">ÿҳ����</param>
		/// <param name="currentpage">��ǰҳ</param>
		/// <param name="uid">�û�id</param>
		/// <returns></returns>
		public static DataTable GetPayLogInList(int pagesize,int currentpage , int uid)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetPayLogInList(pagesize, currentpage, uid);
			if (dt!=null)
			{
				DataColumn dc=new DataColumn();
				dc.ColumnName="forumname";
				dc.DataType=System.Type.GetType("System.String");
				dc.DefaultValue="";
				dc.AllowDBNull=false;
				dt.Columns.Add(dc);
                DataTable ForumList = DatabaseProvider.GetInstance().GetForumList();
				foreach(DataRow dr in dt.Rows)
				{
					if(dr["fid"].ToString().Trim() != "")
					{
						foreach(DataRow forumdr in ForumList.Select("fid="+dr["fid"].ToString()))
						{
							dr["forumname"]=forumdr["name"].ToString();
							break;
						}
					}
				}
			}
			return dt;
		}





		/// <summary>
		/// ��ȡָ���û���������־��¼��
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns></returns>
		public static int GetPaymentLogInRecordCount(int uid)
		{
            return DatabaseProvider.GetInstance().GetPaymentLogInRecordCount(uid);	
        }

		/// <summary>
		/// ����ָ���û���֧����־��¼��
		/// </summary>
		/// <param name="pagesize">ÿҳ��¼��</param>
		/// <param name="currentpage">��ǰҳ</param>
		/// <param name="uid">�û�id</param>
		/// <returns></returns>
		public static DataTable GetPayLogOutList(int pagesize,int currentpage , int uid)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetPayLogOutList(pagesize, currentpage, uid);
			if (dt!=null)
			{
				DataColumn dc=new DataColumn();
				dc.ColumnName="forumname";
				dc.DataType=System.Type.GetType("System.String");
				dc.DefaultValue="";
				dc.AllowDBNull=false;
				dt.Columns.Add(dc);
                DataTable ForumList = DatabaseProvider.GetInstance().GetForumList();
				foreach(DataRow dr in dt.Rows)
				{
					if(dr["fid"].ToString().Trim() != "")
					{
						foreach(DataRow forumdr in ForumList.Select("fid="+dr["fid"].ToString()))
						{
							dr["forumname"]=forumdr["name"].ToString();
							break;
						}
					}
				}
			}
			return dt;
		}


		/// <summary>
		/// ����ָ���û�֧����־����
		/// </summary>
		/// <param name="uid">�û�id</param>
		/// <returns></returns>
		public static int GetPaymentLogOutRecordCount(int uid)
		{
            return DatabaseProvider.GetInstance().GetPaymentLogOutRecordCount(uid);	
        }

		/// <summary>
		/// ��ȡָ������Ĺ����¼
		/// </summary>
		/// <param name="pagesize">ÿҳ��¼��</param>
		/// <param name="currentpage">��ǰҳ��</param>
		/// <param name="tid">����id</param>
		/// <returns></returns>
		public static DataTable GetPaymentLogByTid(int pagesize,int currentpage , int tid)
		{
            DataTable dt = DatabaseProvider.GetInstance().GetPaymentLogByTid(pagesize, currentpage, tid);
			if (dt==null)
			{
				dt=new DataTable();
			}
			return dt;
		}

		/// <summary>
		/// ���⹺���ܴ���
		/// </summary>
		/// <param name="tid">����id</param>
		/// <returns></returns>
		public static int GetPaymentLogByTidCount(int tid)
		{
            return DatabaseProvider.GetInstance().GetPaymentLogByTidCount(tid);	
        }


	}
}
