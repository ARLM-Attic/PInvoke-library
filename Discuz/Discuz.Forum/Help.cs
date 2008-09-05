using System;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;

using Discuz.Entity;
using Discuz.Data;

namespace Discuz.Forum
{
    public class Helps
    {
        /// <summary>
        /// ��ȡ�����б�
        /// </summary>
        /// <returns>�����б�</returns>
        public static ArrayList GetHelpList()
        {
            IDataReader readclass = DatabaseProvider.GetInstance().GetHelpClass();
            ArrayList helplist = new ArrayList();

            if (readclass != null)
            {
                while (readclass.Read())
                {

                    IDataReader reader = DatabaseProvider.GetInstance().GetHelpList(int.Parse(readclass["id"].ToString()));

                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            HelpInfo info = new HelpInfo();
                            info.Id = int.Parse(reader["id"].ToString());
                            info.Title = reader["title"].ToString();
                            info.Message = reader["message"].ToString();
                            info.Pid = int.Parse(reader["pid"].ToString());
                            info.Orderby = int.Parse(reader["orderby"].ToString());
                            helplist.Add(info);
                        }
                        reader.Close();
                    }
                }
                readclass.Close();
            }
            return helplist;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns>��������</returns>
        public static HelpInfo getmessage(int id)
        {
            IDataReader reader = DatabaseProvider.GetInstance().ShowHelp(id);
            HelpInfo info = new HelpInfo();
            if (reader != null)
            {
                if (reader.Read())
                {
                    info.Title = reader["title"].ToString();
                    info.Message = reader["message"].ToString();
                    info.Pid = int.Parse(reader["pid"].ToString());
                    info.Orderby = int.Parse(reader["orderby"].ToString());
                    return info;
                }
                reader.Close();
            }
            return null;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns>��������</returns>
        public static int helpcount()
        {

            return DatabaseProvider.GetInstance().HelpCount();
        }

        /// <summary>
        /// ���°�����Ϣ
        /// </summary>
        /// <param name="id">����ID</param>
        /// <param name="title">��������</param>
        /// <param name="message">��������</param>
        /// <param name="pid">����</param>
        /// <param name="orderby">����ʽ</param>
        public static void updatehelp(int id, string title, string message, int pid, int orderby)
        {
            DatabaseProvider.GetInstance().ModHelp(id, title, message, pid, orderby);
        }

        /// <summary>
        /// ���Ӱ���
        /// </summary>
        /// <param name="title">��������</param>
        /// <param name="message">��������</param>
        /// <param name="pid">����</param>
        public static void addhelp(string title, string message, int pid)
        {
            int count = helpcount();
            DatabaseProvider.GetInstance().AddHelp(title, message, pid, count);
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="idlist">����ID����</param>
        public static void delhelp(string idlist)
        {
            DatabaseProvider.GetInstance().DelHelp(idlist);

        }

        /// <summary>
        /// ���ذ����ķ����б��SQL���
        /// </summary>
        /// <returns>�����ķ����б��SQL���</returns>
        public static string bindhelptype()
        {

            return DatabaseProvider.GetInstance().BindHelpType();
        }


        /// <summary>
        /// ͨ��PID��ȷ���Ƿ�Ϊ����
        /// </summary>
        /// <param name="pid">���ڵķ���ID</param>
        /// <returns>�Ƿ�Ϊ����</returns>
        public static bool choosepage(int pid)
        {

            if (pid == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// ��ȡ���������Լ���Ӧ��������
        /// </summary>
        /// <param name="helpid"></param>
        /// <returns>���������Լ���Ӧ��������</returns>
        public static ArrayList GetHelpList(int helpid)
        {
            IDataReader reader = DatabaseProvider.GetInstance().GetHelpList(helpid);
            ArrayList helplist = new ArrayList();
            if (reader != null)
            {
                while (reader.Read())
                {
                    HelpInfo info = new HelpInfo();
                    info.Id = int.Parse(reader["id"].ToString());
                    info.Title = reader["title"].ToString();
                    info.Message = reader["message"].ToString();
                    info.Pid = int.Parse(reader["pid"].ToString());
                    info.Orderby = int.Parse(reader["orderby"].ToString());
                    helplist.Add(info);
                }
                reader.Close();
            }
            return helplist.Count > 0 ? helplist : null;
        }
    }
}
