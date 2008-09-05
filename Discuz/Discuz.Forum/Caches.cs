using System;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Cache;

namespace Discuz.Forum
{
    /// <summary>
    /// ������̳ǰ̨��һЩ����HTML����
    /// </summary>
    public class Caches
    {
        private static object lockHelper = new object();

        /// <summary>
        /// ��ð�������б�
        /// </summary>
        /// <returns>�б����ݵ�html</returns>
        public static string GetForumListBoxOptionsCache()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/UI/ForumListBoxOptions") as string;
            if (str != null)
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            IDataReader dr = DatabaseProvider.GetInstance().GetVisibleForumList();
            try
            {
                while (dr.Read())
                {
                    sb.Append("<option value=\"");
                    sb.Append(dr["fid"].ToString());
                    sb.Append("\">");
                    sb.Append(Utils.GetSpacesString(Utils.StrToInt(dr["layer"].ToString(), 0)));
                    sb.Append(dr["name"].ToString().Trim());
                    sb.Append("</option>");
                }

                cache.AddObject("/Forum/UI/ForumListBoxOptions", sb.ToString());
                return sb.ToString();
            }
            finally
            {
                dr.Close();
            }
        }

        /// <summary>
        /// ǰ̨����б����˵�
        /// </summary>
        /// <param name="usergroupid">�û���id</param>
        /// <param name="userid">��ǰ�û�id</param>
        /// <param name="extname">��չ����</param>
        /// <returns>����б����˵�</returns>
        public static string GetForumListMenuDivCache(int usergroupid, int userid, string extname)
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/ForumListMenuDiv") as string;
            if (str != null)
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            ForumInfo[] foruminfoarray = Forums.GetForumList();

            if (foruminfoarray.Length > 0)
            {
                sb.Append("<div class=\"popupmenu_popup\" id=\"forumlist_menu\" style=\"overflow-y: auto; display:none\">");

                foreach (ForumInfo info in foruminfoarray)
                {

                    if (info.Layer >= 0 && info.Layer <= 1 && info.Status == 1)
                    {
                        //�ж��Ƿ�Ϊ˽����̳
                        if (info.Viewperm != "" && !Utils.InArray(usergroupid.ToString(), info.Viewperm))
                        {
                            //
                        }
                        else
                        {
                            if (info.Layer == 0)
                            {
                                sb.Append("<dl>");
                                sb.Append("<dt>");
                                sb.Append("<a href=\"");
                                sb.Append(Urls.ShowForumAspxRewrite(info.Fid, 0));
                                sb.Append("\">");

                                sb.Append(info.Name);
                                sb.Append("</a></dt>");

                                sb.Append("<dd><ul>");
                                foreach (ForumInfo forum in foruminfoarray)
                                {

                                    if (int.Parse(forum.Parentidlist.Split(',')[0]) == info.Fid && forum.Layer == 1)
                                    {
                                        sb.Append("<li><a href=\"");
                                        sb.Append(Urls.ShowForumAspxRewrite(forum.Fid, 0));
                                        sb.Append("\">");
                                        sb.Append(forum.Name);
                                        sb.Append("</a></li>");
                                    
                                    }
                                
                                }
                                sb.Append("</ul></dd>");
                                sb.Append("</dl>");
                            }
                        }
                    }
                }


            }
            sb.Append("</div>");
            str = sb.ToString().Replace("<dd><ul></ul></dd>", "");
            cache.AddObject("/Forum/ForumListMenuDiv", str);

            return str;

        }


        /// <summary>
        /// ����ģ���б��������html
        ///</summary>
        /// <returns>������html</returns>
        public static string GetTemplateListBoxOptionsCache()
        {
            lock (lockHelper)
            {
                Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
                string str = cache.RetrieveObject("/Forum/UI/TemplateListBoxOptions") as string;
                if (str != null)
                {
                    return str;
                }

                StringBuilder sb = new StringBuilder();
                DataTable dt = Templates.GetValidTemplateList();
                foreach (DataRow dr in dt.Rows)
                {
                    sb.Append("<li class=\"current\">");
                    sb.Append("<a href=\"###\" onclick=\"window.location.href='showtemplate.aspx?templateid=");
                    sb.Append(dr["templateid"].ToString());
                    sb.Append("'\">");
                    sb.Append(dr["name"].ToString().Trim());
                    sb.Append("</a>");
                    sb.Append("</li>");

                }
                cache.AddObject("/Forum/UI/TemplateListBoxOptions", sb.ToString());

                dt.Dispose();
                return sb.ToString();
            }
        }

        /// <summary>
        /// ��ñ������json����
        /// </summary>
        /// <returns>�������json����</returns>
        public static string GetSmiliesCache()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/UI/SmiliesList") as string;
            if (str != null)
            {
                return str;
            }

            StringBuilder builder = new StringBuilder();
            DataTable dt = Smilies.GetSmiliesListDataTable();

            foreach (DataRow drCate in dt.Copy().Rows)
            {
                if (drCate["type"].ToString() == "0")
                {
                    builder.AppendFormat("'{0}': [\r\n", drCate["code"].ToString().Trim().Replace("'", "\\'"));
                    bool flag = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["type"].ToString() == drCate["id"].ToString())
                        {
                            builder.Append("{'code' : '");
                            builder.Append(dr["code"].ToString().Trim().Replace("'", "\\'"));
                            builder.Append("', 'url' : '");
                            builder.Append(dr["url"].ToString().Trim().Replace("'", "\\'"));
                            builder.Append("'},\r\n");
                            flag = true;
                        }
                    }
                    if (builder.Length > 0 && flag)
                        builder.Remove(builder.Length - 3, 3);
                    builder.Append("\r\n],\r\n");
                }

            }
            builder.Remove(builder.Length - 3, 3);

            cache.AddObject("/Forum/UI/SmiliesList", builder.ToString());
            return builder.ToString();
        }

        /// <summary>
        /// ��ȡ��һҳ�ı���
        /// </summary>
        /// <returns>��ȡ��һҳ�ı���</returns>
        public static string GetSmiliesFirstPageCache()
        {
            DNTCache cache = DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/UI/SmiliesListFirstPage") as string;
            if (str != null)
            {
                return str;
            }
            StringBuilder builder = new StringBuilder();
            DataTable dt = Smilies.GetSmiliesListDataTable();
            foreach (DataRow drCate in dt.Copy().Rows)
            {
                if (drCate["type"].ToString() == "0")
                {
                    builder.AppendFormat("'{0}': [\r\n", drCate["code"].ToString().Trim().Replace("'", "\\'"));
                    bool flag = false;

                    int smiliescount = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["type"].ToString() == drCate["id"].ToString() && smiliescount < 16)
                        {
                            builder.Append("{'code' : '");
                            builder.Append(dr["code"].ToString().Trim().Replace("'", "\\'"));
                            builder.Append("', 'url' : '");
                            builder.Append(dr["url"].ToString().Trim().Replace("'", "\\'"));
                            builder.Append("'},\r\n");
                            flag = true;
                            smiliescount++;
                        }
                    }
                    if (builder.Length > 0 && flag)
                        builder.Remove(builder.Length - 3, 3);
                    builder.Append("\r\n],\r\n");

                    break;
                }
            }
            builder.Remove(builder.Length - 3, 3);

            cache.AddObject("/Forum/UI/SmiliesListFirstPage", builder.ToString());
            return builder.ToString();
        }


        /// <summary>
        /// ��ñ�������б�
        /// </summary>
        /// <returns>��������б�</returns>
        public static DataTable GetSmilieTypesCache()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable smilietypes = cache.RetrieveObject("/Forum/UI/SmiliesTypeList") as DataTable;
            if (smilietypes == null || smilietypes.Rows.Count == 0)
            {
                smilietypes = Smilies.GetSmilieTypes();
                cache.AddObject("/Forum/UI/SmiliesTypeList", smilietypes);
            }           
            
            return smilietypes;
        }

        /// <summary>
        /// �������ͼ���javascript���飬����һ��ģ���ж���
        /// </summary>
        /// <returns>ͼ���javascript����</returns>
        public static string GetTopicIconsCache()
        {
            return "";
        }


        /// <summary>
        /// ��ñ༭���Զ��尴ť��Ϣ��javascript����
        /// </summary>
        /// <returns>�������javascript����</returns>
        public static string GetCustomEditButtonList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/UI/CustomEditButtonList") as string;
            if (str != null)
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            IDataReader dr = Editors.GetCustomEditButtonList();
            try
            {
                while (dr.Read())
                {
                    //˵��:[��ǩ��,��Ӧͼ���ļ���,[����1����,����2����,...],[����1Ĭ��ֵ,����2Ĭ��ֵ,...]]
                    //ʵ��["fly","swf.gif",["������flash��ַ","������flash���","������flash�߶�"],["http://","200","200"],3]
                    sb.AppendFormat(",'{0}':['", Utils.ReplaceStrToScript(dr["tag"].ToString()));
                    sb.Append(Utils.ReplaceStrToScript(dr["tag"].ToString()));
                    sb.Append("','");
                    sb.Append(Utils.ReplaceStrToScript(dr["icon"].ToString()));
                    sb.Append("','");
                    sb.Append(Utils.ReplaceStrToScript(dr["explanation"].ToString()));
                    sb.Append("',['");
                    sb.Append(Utils.ReplaceStrToScript(dr["paramsdescript"].ToString()).Replace(",", "','"));
                    sb.Append("'],['");
                    sb.Append(Utils.ReplaceStrToScript(dr["paramsdefvalue"].ToString()).Replace(",", "','"));
                    sb.Append("'],");
                    sb.Append(Utils.ReplaceStrToScript(dr["params"].ToString()));
                    sb.Append("]");
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                }
                cache.AddObject("/Forum/UI/CustomEditButtonList", Utils.ClearBR(sb.ToString()));
                return Utils.ClearBR(sb.ToString());
            }
            finally
            {
                dr.Close();
            }
        }

        /// <summary>
        /// ��������û��б�ͼ��
        /// </summary>
        /// <returns>�����û��б�ͼ��</returns>
        public static string GetOnlineGroupIconList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/UI/OnlineIconList") as string;
            if (str != null && str != "")
            {
                return str;
            }

            StringBuilder sb = new StringBuilder();
            IDataReader dr = DatabaseProvider.GetInstance().GetOnlineGroupIconList();
            try
            {
                while (dr.Read())
                {
                    sb.Append("<img src=\"images/groupicons/");
                    sb.Append(dr["img"].ToString());
                    sb.Append("\" /> ");
                    sb.Append(dr["title"].ToString());
                    sb.Append(" &nbsp; &nbsp; &nbsp; ");
                }
                cache.AddObject("/Forum/UI/OnlineIconList", sb.ToString());
            }
            finally
            {
                dr.Close();
            }
            return sb.ToString();
        }

        /// <summary>
        /// ������������б�
        /// </summary>
        /// <returns>���������б�</returns>
        public static DataTable GetForumLinkList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Forum/ForumLinkList") as DataTable;
            if (dt != null)
            {
                return dt;
            }

            dt = DatabaseProvider.GetInstance().GetForumLinkList();
            if (dt != null && dt.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["note"].ToString().Equals(""))
                    {
                        if (dr["name"].ToString().Equals(""))
                        {
                            dr["name"] = "δ֪";
                        }
                        sb.Append("<a title=\"");
                        sb.Append(dr["name"].ToString());
                        sb.Append("\" href=\"");
                        sb.Append(dr["url"].ToString());
                        sb.Append("\" target=\"_blank\">");

                        if (dr["logo"].ToString().Equals(""))
                        {
                            sb.Append(dr["name"].ToString());
                        }
                        else
                        {
                            sb.Append("<img alt=\"");
                            sb.Append(dr["name"].ToString());
                            sb.Append("\" class=\"friendlinkimg\" src=\"");
                            sb.Append(dr["logo"].ToString());
                            sb.Append("\" />");
                        }
                        sb.Append("</a>\r\n");

                        dr.Delete();
                    }

                }
                if (sb.Length > 0)
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = "$$otherlink$$";
                    dr["url"] = "";
                    dr["note"] = sb.ToString();
                    dr["logo"] = "";
                    dt.Rows.Add(dr);
                }
                dt.AcceptChanges();
            }
            cache.AddObject("/Forum/ForumLinkList", dt);
            return dt;
        }

        /// <summary>
        /// ��������ʽ��̬ʵ��
        /// </summary>
        private static Regex r = new Regex("\\{(\\d+)\\}", Utils.GetRegexCompiledOptions());

        /// <summary>
        /// �������ֹ����б�
        /// </summary>
        /// <returns>�������ֹ����б�����</returns>
        public static string[,] GetBanWordList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string[,] str = cache.RetrieveObject("/Forum/BanWordList") as string[,];
            if (str != null)
            {
                return str;
            }

            DataTable dt = DatabaseProvider.GetInstance().GetBanWordList();
            str = new string[dt.Rows.Count, 2];
            string temp = "";
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                temp = dt.Rows[i]["find"].ToString().Trim();
                foreach (Match m in r.Matches(temp))
                {
                    temp = temp.Replace(m.Groups[0].ToString(), m.Groups[0].ToString().Replace("{", ".{0,"));
                }
                str[i, 0] = temp.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("'", "\'").Replace("[", "\\[").Replace("]", "\\]");
                str[i, 1] = dt.Rows[i]["replacement"].ToString().Trim();
            }
            cache.AddObject("/Forum/BanWordList", str);
            dt.Dispose();
            return str;
        }

        /// <summary>
        /// ��ȡ�Դ�ͷ���б�
        /// </summary>
        /// <returns>�Դ�ͷ���б�</returns>
        public static DataTable GetAvatarList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Forum/CommonAvatarList") as DataTable;
            if (dt != null)
            {
                return dt;
            }

            dt = new DataTable();
            dt.Columns.Add("filename", Type.GetType("System.String"));

            DirectoryInfo dirinfo = new DirectoryInfo(Utils.GetMapPath(BaseConfigs.GetForumPath + "avatars/common/"));
            string extname = "";
            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    extname = file.Extension.ToLower();
                    if (extname.Equals(".jpg") || extname.Equals(".gif") || extname.Equals(".png"))
                    {
                        DataRow dr = dt.NewRow();
                        dr["filename"] = @"avatars/common/" + file.Name;
                        dt.Rows.Add(dr);
                    }
                }
            }
            cache.AddObject("/Forum/CommonAvatarList", dt);
            return dt;
        }


        /// <summary>
        /// ��ø������ַ���
        /// </summary>
        /// <returns>�������ַ���</returns>
        public static string GetJammer()
        {

            ///���������(10 λ����ַ���+ ��վ���� + 10λ����ַ�)
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            string str = cache.RetrieveObject("/Forum/UI/Jammer") as string;

            if (str == null)
            {
                Random rdm1 = new Random(unchecked((int)DateTime.Now.Ticks));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 10; i++)
                {
                    sb.Append(Convert.ToChar(rdm1.Next(1, 256)));
                }
                sb.Append(HttpContext.Current.Request.Url.Authority);
                for (int i = 0; i < 10; i++)
                {
                    sb.Append(Convert.ToChar(rdm1.Next(1, 256)));
                }
                str = sb.ToString();
                str = Utils.HtmlEncode(str);

                if (sb.Length > 0)
                {
                    sb.Remove(0, sb.Length);
                }

                sb.Append("<span style=\"display:none;font-size:0px\">");
                sb.Append(str);
                sb.Append("</span>");
                str = sb.ToString();
                Discuz.Cache.ICacheStrategy ics = new ForumCacheStrategy();
                ics.TimeOut = 720;
                cache.LoadCacheStrategy(ics);
                cache.AddObject("/Forum/UI/Jammer", str);
                cache.LoadDefaultCacheStrategy();

                //cache.AddObject("/Forum/UI/Jammer", str);
            }
         
            return str;
        }


        /// <summary>
        /// ���ѫ���б�
        /// </summary>
        /// <returns>���ѫ���б�</returns>
        public static DataTable GetMedalsList()
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Forum/UI/MedalsList") as DataTable;
            if (dt != null)
            {
                return dt;
            }

            dt = DatabaseProvider.GetInstance().GetMedalsList();
            string forumpath = BaseConfigs.GetBaseConfig().Forumpath;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["available"].ToString() == "1")
                {
                    if (dr["image"].ToString().Trim() != "")
                    {
                        dr["image"] = "<img border=\"0\" src=\"" + forumpath + "images/medals/" + dr["image"] + "\" alt=\"" + dr["name"] + "\" title=\"" + dr["name"] + "\" class=\"medals\" />";
                    }
                    else
                    {
                        dr["image"] = "";
                    }
                }
                else
                {
                    dr["image"] = "";
                }
            }

            cache.AddObject("/Forum/UI/MedalsList", dt);
            return dt;
        }


        /// <summary>
        /// ��ȡָ��id��ѫ���б�html
        /// </summary>
        /// <param name="mdealList">ѫ��id</param>
        /// <returns>ѫ���б�html</returns>
        public static string GetMedalsList(string mdealList)
        {
            DataTable dt = GetMedalsList();
            string[] list = Utils.SplitString(mdealList, ",");
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Length; i++)
            {
                sb.Append(dt.Rows[Utils.StrToInt(list[i], 1) - 1]["image"]);
            }
            return sb.ToString();

        }

        /// <summary>
        /// ���ħ�������б�
        /// </summary>
        /// <returns>ħ�������б�</returns>
        public static DataTable GetMagicList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            DataTable dt = cache.RetrieveObject("/Forum/MagicList") as DataTable;
            if (dt == null || dt.Rows.Count == 0)
            {
                dt = DatabaseProvider.GetInstance().GetMagicList();
                cache.AddObject("/Forum/MagicList", dt);
            }            
            return dt;
        }

        /// <summary>
        /// ��ȡħ�������б���
        /// </summary>
        /// <param name="id">ħ������id</param>
        /// <returns>ħ������</returns>
        public static string GetMagicListItem(int id)
        {
            DataTable dt = GetMagicList();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["id"].ToString() == id.ToString())
                {
                    return dr["magic"].ToString();
                }
            }
            return "";
        }


        /// <summary>
        /// ��ȡ���������
        /// </summary>
        /// <param name="identifyid">����ǩ��id</param>
        /// <returns>���������Ϣ</returns>
        public static TopicIdentify GetTopicIdentify(int identifyid)
        {
            foreach (TopicIdentify ti in GetTopicIdentifyCollection())
            {
                if (ti.Identifyid == identifyid)
                {
                    return ti;
                }
            }
            return new TopicIdentify();
        }

        /// <summary>
        /// ��ȡ�������ͼƬ��ַ��������
        /// </summary>
        /// <returns>�������ͼƬ��ַ��������</returns>
        public static string GetTopicIdentifyFileNameJsArray()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();

            string jsArray = cache.RetrieveObject("/Forum/TopicIndentifysJsArray") as string;

            if (Utils.StrIsNullOrEmpty(jsArray))
            {
                GetTopicIdentifyCollection();
            }

            jsArray = cache.RetrieveObject("/Forum/TopicIndentifysJsArray") as string;

            return jsArray;
        }


        #region �������뷽��

        /// <summary>
        /// ���������������
        /// </summary>
        /// <returns>������������</returns>
#if NET1
        public static System.Collections.SortedList GetTopicTypeArray()
#else
        public static Discuz.Common.Generic.SortedList<int, object> GetTopicTypeArray()
#endif
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
#if NET1
            System.Collections.SortedList topictypeList;
#else
            Discuz.Common.Generic.SortedList<int, object> topictypeList;
#endif
            topictypeList = cache.RetrieveObject("/Forum/TopicTypes") as Discuz.Common.Generic.SortedList<int, object>;

            if (topictypeList == null)
            {
#if NET1
                topictypeList = new System.Collections.SortedList();
#else
                topictypeList = new Discuz.Common.Generic.SortedList<int, object>();
#endif
                DataTable dt = DatabaseProvider.GetInstance().GetTopicTypeList();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((dr["typeid"].ToString() != "") && (dr["name"].ToString() != ""))
                        {
                            topictypeList.Add(Int32.Parse(dr["typeid"].ToString()), dr["name"]);
                        }
                    }
                }

                cache.AddObject("/Forum/TopicTypes", topictypeList);
            }
            return topictypeList;
        }


        /// <summary>
        /// ��ȡ����ǩ��������
        /// </summary>
        /// <returns>����ǩ��������</returns>
#if NET1
        public static TopicIdentifyCollection GetTopicIdentifyCollection()
#else
        
        public static Discuz.Common.Generic.List<TopicIdentify> GetTopicIdentifyCollection()
#endif
        {

            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();

#if NET1
            TopicIdentifyCollection topicidentifyList = cache.RetrieveObject("/Forum/TopicIdentifys") as TopicIdentifyCollection;
#else
            Discuz.Common.Generic.List<TopicIdentify> topicidentifyList = cache.RetrieveObject("/Forum/TopicIdentifys") as Discuz.Common.Generic.List<TopicIdentify>;
#endif
            if (topicidentifyList == null)
            {
#if NET1
                topicidentifyList = new TopicIdentifyCollection();
#else
                topicidentifyList = new Discuz.Common.Generic.List<TopicIdentify>();
#endif
                IDataReader reader = DatabaseProvider.GetInstance().GetTopicsIdentifyItem();
                StringBuilder jsArray = new StringBuilder("<script type='text/javascript'>var topicidentify = { ");

                while (reader.Read())
                {
                    TopicIdentify topic = new TopicIdentify();
                    topic.Identifyid = Int32.Parse(reader["identifyid"].ToString());
                    topic.Name = reader["name"].ToString();
                    topic.Filename = reader["filename"].ToString();

                    topicidentifyList.Add(topic);
                    jsArray.AppendFormat("'{0}':'{1}',", reader["identifyid"].ToString(), reader["filename"].ToString());
                }
                reader.Close();
                jsArray.Remove(jsArray.Length - 1, 1);
                jsArray.Append("};</script>");
                cache.AddObject("/Forum/TopicIdentifys", topicidentifyList);
                cache.AddObject("/Forum/TopicIndentifysJsArray", jsArray.ToString());
            }

            return topicidentifyList;
        }


        #endregion



    }//class end

}
