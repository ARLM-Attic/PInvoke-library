using System;
using System.IO;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Config;
using Discuz.Mall.Data;
using Discuz.Entity;

namespace Discuz.Mall
{
    /// <summary>
    /// ��Ʒ�������������
    /// </summary>
    public class GoodsAttachments
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="attachmentinfo">��������������</param>
        /// <returns>����id����</returns>
        public static int[] CreateAttachments(Goodsattachmentinfo[] attachmentinfo)
        {
            int acount = attachmentinfo.Length;
            int icount = 0;
            int[] aid = new int[acount];
            for (int i = 0; i < acount; i++)
            {
                if (attachmentinfo[i] != null && attachmentinfo[i].Sys_noupload.Equals(""))
                {
                    aid[i] = DbProvider.GetInstance().CreateGoodsAttachment(attachmentinfo[i]);
                    icount++;
                }
            }

            return aid;
        }

      
        /// <summary>
        /// ����Ʒ���������еĲ�����������Ч��Ʒ��������
        /// </summary>
        /// <param name="attachmentinfo">��������</param>
        /// <param name="goodsid">��Ʒid</param>
        /// <param name="msg">ԭ����ʾ��Ϣ</param>
        /// <param name="categoryid">��Ʒ����id</param>
        /// <param name="userid">�û�id</param>
        /// <returns>��Ч��Ʒ��������</returns>
        public static int BindAttachment(Goodsattachmentinfo[] attachmentinfo, int goodsid, StringBuilder msg, int categoryid, int userid)
        {
            int acount = attachmentinfo.Length;
            // �����鿴Ȩ��
            string[] readperm = DNTRequest.GetString("readperm") == null ? null : DNTRequest.GetString("readperm").Split(',');
            string[] attachdesc = DNTRequest.GetString("attachdesc") == null ? null : DNTRequest.GetString("attachdesc").Split(',');
            string[] localid = DNTRequest.GetString("localid") == null ? null : DNTRequest.GetString("localid").Split(',');

            //������Ч����������
            int errorAttachment = 0;
            for (int i = 0; i < acount; i++)
            {
                if (attachmentinfo[i] != null)
                {
                    if (Utils.IsNumeric(localid[i + 1]))
                        attachmentinfo[i].Sys_index = Convert.ToInt32(localid[i + 1]);

                    attachmentinfo[i].Uid = userid;
                    attachmentinfo[i].Goodsid = goodsid;
                    attachmentinfo[i].Categoryid = categoryid;
                    attachmentinfo[i].Postdatetime = Utils.GetDateTime(); ;
                    
                    if (attachdesc != null && !attachdesc[i + 1].Equals(""))
                    {
                        attachmentinfo[i].Description = Utils.HtmlEncode(attachdesc[i + 1]);
                    }

                    if (!attachmentinfo[i].Sys_noupload.Equals(""))
                    {
                        msg.Append("<tr><td align=\"left\">");
                        msg.Append(attachmentinfo[i].Attachment);
                        msg.Append("</td>");
                        msg.Append("<td align=\"left\">");
                        msg.Append(attachmentinfo[i].Sys_noupload);
                        msg.Append("</td></tr>");
                        errorAttachment++;
                    }
                }
            }
            return errorAttachment;
        }

        /// <summary>
        /// ������ʱ�����еı�����ʱ��ǩ
        /// </summary>
        /// <param name="aid">���id</param>
        /// <param name="attachmentinfo">������Ϣ�б�</param>
        /// <param name="tempMessage">��ʱ��Ϣ����</param>
        /// <returns>���˽��</returns>
        public static string FilterLocalTags(int[] aid, Goodsattachmentinfo[] attachmentinfo, string tempMessage)
        {
            Match m;
            Regex r;
            for (int i = 0; i < aid.Length; i++)
            {
                if (aid[i] > 0)
                {

                    r = new Regex(@"\[localimg=(\d{1,}),(\d{1,})\]" + attachmentinfo[i].Sys_index + @"\[\/localimg\]", RegexOptions.IgnoreCase);
                    for (m = r.Match(tempMessage); m.Success; m = m.NextMatch())
                    {
                        tempMessage = tempMessage.Replace(m.Groups[0].ToString(), "[attachimg]" + aid[i] + "[/attachimg]");
                    }

                    r = new Regex(@"\[local\]" + attachmentinfo[i].Sys_index + @"\[\/local\]", RegexOptions.IgnoreCase);
                    for (m = r.Match(tempMessage); m.Success; m = m.NextMatch())
                    {
                        tempMessage = tempMessage.Replace(m.Groups[0].ToString(), "[attach]" + aid[i] + "[/attach]");
                    }

                }
            }

            tempMessage = Regex.Replace(tempMessage, @"\[localimg=(\d{1,}),\s*(\d{1,})\][\s\S]+?\[/localimg\]", string.Empty, RegexOptions.IgnoreCase);
            tempMessage = Regex.Replace(tempMessage, @"\[local\][\s\S]+?\[/local\]", string.Empty, RegexOptions.IgnoreCase);
            return tempMessage;
        }

        /// <summary>
        /// ��ȡָ����Ʒid�����и�����Ϣ
        /// </summary>
        /// <param name="goodsid">��Ʒid</param>
        /// <returns>������Ϣ����</returns>
        public static GoodsattachmentinfoCollection GetGoodsAttachmentsByGoodsid(int goodsid)
        {
            return DTO.GetGoodsAttachmentInfoList(DbProvider.GetInstance().GetGoodsAttachmentsByGoodsid(goodsid));
        }

        /// <summary>
        /// ��ȡָ������id����ظ�����Ϣ
        /// </summary>
        /// <param name="aid">����id</param>
        /// <returns>������Ϣ</returns>
        public static Goodsattachmentinfo GetGoodsAttachmentsByAid(int aid)
        {
            return DTO.GetGoodsattachmentInfo(DbProvider.GetInstance().GetGoodsAttachmentsByAid(aid));
        }

        /// <summary>
        /// ���渽����Ϣ
        /// </summary>
        /// <param name="goodsattachmentinfo">Ҫ����ĸ�����Ϣ</param>
        /// <returns>�Ƿ񱣴�ɹ�</returns>
        public static bool SaveGoodsAttachment(Goodsattachmentinfo goodsattachmentinfo)
        {
            return DbProvider.GetInstance().SaveGoodsAttachment(goodsattachmentinfo);
        }

        /// <summary>
        /// ���¸�����Ϣ
        /// </summary>
        /// <param name="aid">����Id</param>
        /// <param name="readperm">�Ķ�Ȩ��</param>
        /// <param name="description">����</param>
        /// <returns>���ر����µ�����</returns>
        public static bool SaveGoodsAttachment(int aid, int readperm, string description)
        {
            return DbProvider.GetInstance().SaveGoodsAttachment(aid, readperm, description);
        }

        /// <summary>
        /// ɾ��ָ������id�б�ĸ�����Ϣ���������ļ�
        /// </summary>
        /// <param name="aidList">����id�б�</param>
        /// <returns>ɾ��������</returns>
        public static int DeleteGoodsAttachment(string aidList)
        {
            GoodsattachmentinfoCollection goodsattchmentinfocoll = DTO.GetGoodsAttachmentInfoList(DbProvider.GetInstance().GetGoodsAttachmentListByAidList(aidList));
            int goodsid = 0;
            if (goodsattchmentinfocoll != null)
            {
                foreach(Goodsattachmentinfo goodsattachmentinfo in goodsattchmentinfocoll)
                {
                    if (goodsattachmentinfo.Filename.Trim().ToLower().IndexOf("http") < 0)
                    {
                         string attachmentFilePath = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/" + goodsattachmentinfo.Filename);
                         if (Utils.FileExists(attachmentFilePath))
                         {
                             File.Delete(attachmentFilePath);
                         }
                    }

                    goodsid = Utils.StrToInt(goodsattachmentinfo.Goodsid, 0);
                }
            }

            return DbProvider.GetInstance().DeleteGoodsAttachment(aidList);
        }

        /// <summary>
        /// ɾ��ָ������id�ĸ�����Ϣ���������ļ�
        /// </summary>
        /// <param name="aid">����id</param>
        /// <returns>ɾ��������</returns>
        public static bool DeleteGoodsAttachment(int aid)
        {
            Goodsattachmentinfo goodsattachmentinfo = GetGoodsAttachmentsByAid(aid);
            if (goodsattachmentinfo != null)
            {
                if (goodsattachmentinfo.Filename.ToLower().IndexOf("http") < 0)
                {
                    string attachmentFilePath = Utils.GetMapPath(BaseConfigs.GetForumPath + "upload/" + goodsattachmentinfo.Filename);
                    if (Utils.FileExists(attachmentFilePath))
                    {
                        File.Delete(attachmentFilePath);
                    }
                }
            }

            return Discuz.Data.DatabaseProvider.GetInstance().DeleteAttachment(aid) > 0 ? true : false;
        }


        /// <summary>
        /// ����ת��������
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// �����Ʒ������Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>������Ʒ������Ϣ</returns>
            public static Goodsattachmentinfo GetGoodsattachmentInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Goodsattachmentinfo goodsattachmentsinfo = LoadGoodsAttachmentinfo(reader);

                    reader.Close();
                    return goodsattachmentsinfo;
                }
                return null;
            }

            /// <summary>
            /// �����Ʒ��Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>������Ʒ��Ϣ</returns>
            public static GoodsattachmentinfoCollection GetGoodsAttachmentInfoList(IDataReader reader)
            {
                GoodsattachmentinfoCollection goodsattachmentinfocoll = new GoodsattachmentinfoCollection();
                while (reader.Read())
                {
                    Goodsattachmentinfo goodsattachmentinfo = LoadGoodsAttachmentinfo(reader);

                    goodsattachmentinfocoll.Add(goodsattachmentinfo);
                }
                reader.Close();
                return goodsattachmentinfocoll;
            }

            #region Helper
            private static Goodsattachmentinfo LoadGoodsAttachmentinfo(IDataReader reader)
            {
                Goodsattachmentinfo goodsattachmentinfo = new Goodsattachmentinfo();
                goodsattachmentinfo.Aid = Convert.ToInt32(reader["aid"].ToString());
                goodsattachmentinfo.Uid = Convert.ToInt32(reader["uid"].ToString());
                goodsattachmentinfo.Goodsid = Convert.ToInt32(reader["goodsid"].ToString());
                goodsattachmentinfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                goodsattachmentinfo.Postdatetime = reader["postdatetime"].ToString();
                goodsattachmentinfo.Filename = reader["filename"].ToString().Trim();
                goodsattachmentinfo.Description = reader["description"].ToString().Trim();
                goodsattachmentinfo.Filetype = reader["filetype"].ToString().Trim();
                goodsattachmentinfo.Filesize = Convert.ToInt32(reader["filesize"].ToString());
                goodsattachmentinfo.Attachment = reader["attachment"].ToString().Trim();
                return goodsattachmentinfo;
            }
            #endregion

            /// <summary>
            /// �����Ʒ������Ϣ(DTO)
            /// </summary>
            /// <param name="dt">Ҫת�������ݱ�</param>
            /// <returns>������Ʒ������Ϣ</returns>
            public static Goodsattachmentinfo[] GetGoodsattachmentArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Goodsattachmentinfo[] goodsattachmentsinfoarray = new Goodsattachmentinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    goodsattachmentsinfoarray[i] = new Goodsattachmentinfo();
                    goodsattachmentsinfoarray[i].Aid = Convert.ToInt32(dt.Rows[i]["aid"].ToString());
                    goodsattachmentsinfoarray[i].Uid = Convert.ToInt32(dt.Rows[i]["uid"].ToString());
                    goodsattachmentsinfoarray[i].Goodsid = Convert.ToInt32(dt.Rows[i]["goodsid"].ToString());
                    goodsattachmentsinfoarray[i].Categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
                    goodsattachmentsinfoarray[i].Postdatetime = dt.Rows[i]["postdatetime"].ToString();
                    goodsattachmentsinfoarray[i].Filename = dt.Rows[i]["filename"].ToString();
                    goodsattachmentsinfoarray[i].Description = dt.Rows[i]["description"].ToString();
                    goodsattachmentsinfoarray[i].Filetype = dt.Rows[i]["filetype"].ToString();
                    goodsattachmentsinfoarray[i].Filesize = Convert.ToInt32(dt.Rows[i]["filesize"].ToString());
                    goodsattachmentsinfoarray[i].Attachment = dt.Rows[i]["attachment"].ToString();

                }
                dt.Dispose();
                return goodsattachmentsinfoarray;
            }
        }
    }
}
