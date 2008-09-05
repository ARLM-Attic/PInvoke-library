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
    /// 商品附件管理操作类
    /// </summary>
    public class GoodsAttachments
    {
        /// <summary>
        /// 产生附件
        /// </summary>
        /// <param name="attachmentinfo">附件描述类数组</param>
        /// <returns>附件id数组</returns>
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
        /// 绑定商品附件数组中的参数，返回无效商品附件个数
        /// </summary>
        /// <param name="attachmentinfo">附件类型</param>
        /// <param name="goodsid">商品id</param>
        /// <param name="msg">原有提示信息</param>
        /// <param name="categoryid">商品分类id</param>
        /// <param name="userid">用户id</param>
        /// <returns>无效商品附件个数</returns>
        public static int BindAttachment(Goodsattachmentinfo[] attachmentinfo, int goodsid, StringBuilder msg, int categoryid, int userid)
        {
            int acount = attachmentinfo.Length;
            // 附件查看权限
            string[] readperm = DNTRequest.GetString("readperm") == null ? null : DNTRequest.GetString("readperm").Split(',');
            string[] attachdesc = DNTRequest.GetString("attachdesc") == null ? null : DNTRequest.GetString("attachdesc").Split(',');
            string[] localid = DNTRequest.GetString("localid") == null ? null : DNTRequest.GetString("localid").Split(',');

            //设置无效附件计数器
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
        /// 过滤临时内容中的本地临时标签
        /// </summary>
        /// <param name="aid">广告id</param>
        /// <param name="attachmentinfo">附件信息列表</param>
        /// <param name="tempMessage">临时信息内容</param>
        /// <returns>过滤结果</returns>
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
        /// 获取指定商品id的所有附件信息
        /// </summary>
        /// <param name="goodsid">商品id</param>
        /// <returns>附件信息集合</returns>
        public static GoodsattachmentinfoCollection GetGoodsAttachmentsByGoodsid(int goodsid)
        {
            return DTO.GetGoodsAttachmentInfoList(DbProvider.GetInstance().GetGoodsAttachmentsByGoodsid(goodsid));
        }

        /// <summary>
        /// 获取指定附件id的相关附件信息
        /// </summary>
        /// <param name="aid">附件id</param>
        /// <returns>附件信息</returns>
        public static Goodsattachmentinfo GetGoodsAttachmentsByAid(int aid)
        {
            return DTO.GetGoodsattachmentInfo(DbProvider.GetInstance().GetGoodsAttachmentsByAid(aid));
        }

        /// <summary>
        /// 保存附件信息
        /// </summary>
        /// <param name="goodsattachmentinfo">要保存的附件信息</param>
        /// <returns>是否保存成功</returns>
        public static bool SaveGoodsAttachment(Goodsattachmentinfo goodsattachmentinfo)
        {
            return DbProvider.GetInstance().SaveGoodsAttachment(goodsattachmentinfo);
        }

        /// <summary>
        /// 更新附件信息
        /// </summary>
        /// <param name="aid">附件Id</param>
        /// <param name="readperm">阅读权限</param>
        /// <param name="description">描述</param>
        /// <returns>返回被更新的数量</returns>
        public static bool SaveGoodsAttachment(int aid, int readperm, string description)
        {
            return DbProvider.GetInstance().SaveGoodsAttachment(aid, readperm, description);
        }

        /// <summary>
        /// 删除指定附件id列表的附件信息及其物理文件
        /// </summary>
        /// <param name="aidList">附件id列表</param>
        /// <returns>删除附件数</returns>
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
        /// 删除指定附件id的附件信息及其物理文件
        /// </summary>
        /// <param name="aid">附件id</param>
        /// <returns>删除附件数</returns>
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
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// 获得商品附件信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品附件信息</returns>
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
            /// 获得商品信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回商品信息</returns>
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
            /// 获得商品附件信息(DTO)
            /// </summary>
            /// <param name="dt">要转换的数据表</param>
            /// <returns>返回商品附件信息</returns>
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
