using System;
using System.Data;
using System.Text;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Config;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Data;

namespace Discuz.Web
{
    /// <summary>
    /// 查看新帖、精华帖
    /// </summary>
    public class showtopiclist : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 在线用户列表
        /// </summary>
        public DataTable onlineuserlist;

        /// <summary>
        /// 在线用户图例
        /// </summary>
        public string onlineiconlist;

#if NET1
		public ShowforumPageTopicInfoCollection topiclist;
        public IndexPageForumInfoCollection subforumlist;
        public PrivateMessageInfoCollection pmlist = new PrivateMessageInfoCollection();
#else
        /// <summary>
        /// 主题列表
        /// </summary>
        public Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo> topiclist;

        //= new Topics.ShowforumPageTopicInfoCollection<ShowforumPageTopicInfo>();

        /// <summary>
        /// 子版块列表
        /// </summary>
        public List<IndexPageForumInfo> subforumlist; //= new Discuz.Common.Generic.List<IndexPageForumInfo>();

        /// <summary>
        /// 短消息列表
        /// </summary>
        public List<PrivateMessageInfo> pmlist; //= new Discuz.Common.Generic.List<PrivateMessageInfo>();
#endif

        /// <summary>
        /// 公告列表
        /// </summary>
        public DataTable announcementlist;

        /// <summary>
        /// 版块信息
        /// </summary>
        public ForumInfo forum;

        /// <summary>
        /// 当前用户管理组信息
        /// </summary>
        public AdminGroupInfo admingroupinfo;

        /// <summary>
        /// 论坛在线总数
        /// </summary>
        public int forumtotalonline;

        /// <summary>
        /// 论坛在线注册用户总数
        /// </summary>
        public int forumtotalonlineuser;

        /// <summary>
        /// 论坛在线游客数
        /// </summary>
        public int forumtotalonlineguest;

        /// <summary>
        /// 论坛在线隐身用户数
        /// </summary>
        public int forumtotalonlineinvisibleuser;

        /// <summary>
        /// 版块Id
        /// </summary>
        public int forumid;

        /// <summary>
        /// 版块名称
        /// </summary>
        public string forumname = "";

        /// <summary>
        /// 子版块数
        /// </summary>
        public int subforumcount;

        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav = "";

        /// <summary>
        /// 是否显示版块密码提示 1为显示, 0不显示
        /// </summary>
        public int showforumlogin;

        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;

        /// <summary>
        /// 主题总数
        /// </summary>
        public int topiccount = 0;

        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount = 0;

        /// <summary>
        /// 分页页码链接
        /// </summary>
        public string pagenumbers = "";

        /// <summary>
        /// 置顶主题数
        /// </summary>
        public int toptopiccount = 0;

        /// <summary>
        /// 版块跳转链接选项
        /// </summary>
        public string forumlistboxoptions;

        /// <summary>
        /// 最近访问的版块选项
        /// </summary>
        public string visitedforumsoptions;

        /// <summary>
        /// 是否允许Rss订阅
        /// </summary>
        public int forumallowrss;

        /// <summary>
        /// 是否显示在线用户列表
        /// </summary>
        public bool showforumonline;

        //public string ignorelink;


        /// <summary>
        /// 排序方式
        /// </summary>
        public int order = 2; //排序字段

        public int direct = 1; //排序方向[默认：降序]

        /// <summary>
        /// 查看方式,digest=精华帖,其他值=新帖
        /// </summary>
        public string type = "";

        /// <summary>
        /// 新帖时限
        /// </summary>
        public int newtopic = 120;

        /// <summary>
        /// 用户选择的版块
        /// </summary>
        public string forums = "";

        /// <summary>
        /// 论坛选择多选框列表
        /// </summary>
        public string forumcheckboxlist = "";
        /// <summary>
        /// 获取绑定相关版块的商品分类信息
        /// </summary>
        public string goodscategoryfid = "{}";
        #endregion

        //后台指定的最大查询贴数
        private int maxseachnumber = 10000;
        private string condition = ""; //查询条件

        protected override void ShowPage()
        {
            if (config.Enablemall > 0)
            {
                goodscategoryfid = Discuz.Plugin.Mall.MallPluginProvider.GetInstance().GetGoodsCategoryWithFid();
            }

            pagetitle = "查看新帖";
            forumallowrss = 0;
            forumid = DNTRequest.GetInt("forumid", -1);
            forum = new ForumInfo();
            admingroupinfo = new AdminGroupInfo();
            if (userid > 0 && useradminid > 0)
            {
                admingroupinfo = AdminGroups.GetAdminGroupInfo(useradminid);
            }

            if (config.Rssstatus == 1)
            {
                AddLinkRss("tools/rss.aspx", "最新主题");
            }

            //当所选论坛为多个时或全部时
        
            if (forumid == -1)
            {
                //用户点选相应的论坛
                if ((DNTRequest.GetString("fidlist").Trim() != ""))
                {
                    forums = DNTRequest.GetString("fidlist");
                }
                else
                {
                    forums = DNTRequest.GetString("forums");
                }
                //获得已选取的论坛列表
                forumcheckboxlist = GetForumCheckBoxListCache(forums);

                //获得有权限的fid
                //string allowviewforums = "";
                if (forums.ToLower() == "all" || forums == "")//如果是选择全部版块
                {
                    //取得所有列表
                    forums = "";//先清空
                    ForumInfo[] objForumInfoList = Forums.GetForumList();
                    foreach (ForumInfo objForumInfo in objForumInfoList)
                    {
                        forums += string.Format(",{0}", objForumInfo.Fid);
                    }
                    forums = GetAllowviewForums(forums.Trim(','));
                }
                else//如果是选择指定版块
                {
                    forums = GetAllowviewForums(forums);
                }
            }
            

            #region 对搜索条件进行检索

            string orderStr = "";

            if (DNTRequest.GetString("search").Trim() != "") //进行指定查询
            {
                //排序的字段
                order = DNTRequest.GetInt("order", 2);
                switch (order)
                {
                    case 1:
                        orderStr = "lastpostid";
                        break;
                    case 2:
                        orderStr = "tid";
                        break;

                    default:
                        orderStr = "tid";
                        break;
                }

                direct = DNTRequest.GetInt("direct", 1);
            }


            //if (DNTRequest.GetString("type").Trim() == "digest")
            //{
            //    type = "digest";
            //    condition += " AND digest>0 ";
            //}

            //if (DNTRequest.GetString("type").Trim() == "newtopic")
            //{
            //    type = "newtopic";

            //    if ((DNTRequest.GetString("newtopic").Trim() != null) && (DNTRequest.GetString("newtopic").Trim() != ""))
            //    {
            //        newtopic = DNTRequest.GetString("newtopic").Trim();
            //        condition += " AND postdatetime>='" + DateTime.Now.AddMinutes(-Convert.ToInt32(newtopic)).ToString() + "'";
            //    }
            //}
            newtopic = DNTRequest.GetInt("newtopic", 120);
            condition =
                DatabaseProvider.GetInstance().GetTopicCountCondition(out type, DNTRequest.GetString("type").ToString(),
                                                                      newtopic);
            if (type == "digest")
            {
                pagetitle = "查看精华";
            }
            if (forums != "")
            {
                //验证重新生成的版块id列表是否合法(需要放入sql语句查询)                
                if (!Utils.IsNumericArray(forums.Split(',')))
                {
                    AddErrLine("错误的Url");
                    return;
                }
                condition += " AND fid IN(" + forums + ")";                
            }
            else if (forumid > 0)
            {
                condition += " AND fid =" + forumid;
            }
            else//无可访问的版块fid存留
            {
                AddErrLine("没有可访问的版块,或者Url参数错误!<br >如果是需要登录的版块,请登录后再访问.");
                return;
            }

            #endregion

            if (forumid > 0)
            {
                forum = Forums.GetForumInfo(forumid);
                forumname = forum.Name;
                pagetitle = Utils.RemoveHtml(forum.Name);
                subforumcount = forum.Subforumcount;
                forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);

                // 是否显示版块密码提示 1为显示, 0不显示
                showforumlogin = 1;
                // 如果版块未设密码
                if (forum.Password == "")
                {
                    showforumlogin = 0;
                }
                else
                {
                    // 如果检测到相应的cookie正确
                    if (Utils.MD5(forum.Password) == ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
                    {
                        showforumlogin = 0;
                    }
                    else
                    {
                        // 如果用户提交的密码正确则保存cookie
                        if (forum.Password == DNTRequest.GetString("forumpassword"))
                        {
                            ForumUtils.WriteCookie("forum" + forumid.ToString() + "password", Utils.MD5(forum.Password));
                            showforumlogin = 0;
                        }
                    }
                }

                if (!Forums.AllowView(forum.Viewperm, usergroupid))
                {
                    AddErrLine("您没有浏览该版块的权限");
                    return;
                }
                // 得到子版块列表
                subforumlist =
                    Forums.GetSubForumCollection(forumid, forum.Colcount, config.Hideprivate, usergroupid, config.Moddisplay);
            }

            if (base.newpmcount > 0)
            {
                pmlist = PrivateMessages.GetPrivateMessageCollectionForIndex(userid, 5, 1, 1);
            }
            
            // 得到公告
            announcementlist = Announcements.GetSimplifiedAnnouncementList(nowdatetime, "2999-01-01 00:00:00");

            //得到当前用户请求的页数
            pageid = DNTRequest.GetInt("page", 1);
            //获取主题总数
            topiccount = Topics.GetTopicCount(condition);

            //防止查询数超过系统规定的最大值
            topiccount = maxseachnumber > topiccount ? topiccount : maxseachnumber;

            // 得到Tpp设置
            int tpp = Utils.StrToInt(ForumUtils.GetCookie("tpp"), config.Tpp);
            if (tpp <= 0)
            {
                tpp = config.Tpp;
            }
            //得到用户设置的每页显示主题数
            if (userid != -1)
            {
                ShortUserInfo userinfo = Users.GetShortUserInfo(userid);
                if (userinfo != null)
                {
                    if (userinfo.Tpp > 0)
                    {
                        tpp = userinfo.Tpp;
                    }

                    if (userinfo.Newpm == 0)
                    {
                        base.newpmcount = 0;
                    }
                }
            }


            //获取总页数
            pagecount = topiccount%tpp == 0 ? topiccount/tpp : topiccount/tpp + 1;
            if (pagecount == 0)
            {
                pagecount = 1;
            }
            //修正请求页数中可能的错误
            if (pageid < 1)
            {
                pageid = 1;
            }
            if (pageid > pagecount)
            {
                pageid = pagecount;
            }
            //如果当前页面的返回结果超过系统规定的的范围时，则进行相应删剪
            if ((pageid*tpp) > topiccount)
            {
                tpp = tpp - (pageid*tpp - topiccount);
            }
            if (orderStr == "")
            {
                topiclist =
                    Topics.GetTopicCollectionByType(tpp, pageid, 0, 10, config.Hottopic, forum.Autoclose,
                                                    forum.Topictypeprefix, condition, direct);
            }
            else
            {
                topiclist =
                    Topics.GetTopicCollectionByTypeDate(tpp, pageid, 0, 10, config.Hottopic, forum.Autoclose,
                                                        forum.Topictypeprefix, condition, orderStr, direct);
            }

            //得到页码链接
            if ("".Equals(DNTRequest.GetString("search")))
            {
                pagenumbers =
                    Utils.GetPageNumbers(pageid, pagecount,
                                         string.Format(
                                             "showtopiclist.aspx?type={0}&newtopic={1}&forumid={2}&forums={3}", type,
                                             newtopic, forumid, forums), 8);
            }
            else
            {
                pagenumbers =
                    Utils.GetPageNumbers(pageid, pagecount,
                                         string.Format(
                                             "showtopiclist.aspx?search=1&type={0}&newtopic={1}&order={2}&direct={3}&forumid={4}&forums={5}",
                                             type, newtopic, DNTRequest.GetString("order"),
                                             DNTRequest.GetString("direct"), forumid.ToString(), forums), 8);
            }

            forumlistboxoptions = Caches.GetForumListBoxOptionsCache();
            OnlineUsers.UpdateAction(olid, UserAction.ShowForum.ActionID, forumid, config.Onlinetimeout);

            showforumonline = false;
            if (forumtotalonline < 300 || DNTRequest.GetString("showonline") != "")
            {
                showforumonline = true;
            }

            ForumUtils.UpdateVisitedForumsOptions(forumid);
            visitedforumsoptions = ForumUtils.GetVisitedForumsOptions(config.Visitedforums);
            forumallowrss = forum.Allowrss;
        }

        /// <summary>
        /// 取得当前用户有权访问的版块列表
        /// </summary>
        /// <param name="forums">原始版块列表(用逗号分隔的fid)</param>
        /// <returns>有权访问的版块列表(用逗号分隔的fid)</returns>
        private string GetAllowviewForums(string forums)
        {
            //验证版块id列表是否合法的数字列表                
            if (!Utils.IsNumericArray(forums.Split(',')))
            {
                return "";
            }
            string allowviewforums = "";
            string[] fidlist = forums.Split(',');

            foreach (string strfid in fidlist)
            {                
                int fid = Utils.StrToInt(strfid, 0);
                if (Forums.AllowView(Forums.GetForumInfo(fid).Viewperm, usergroupid))
                {
                    if (Forums.GetForumInfo(fid).Password.Trim() == "" || Utils.MD5(Forums.GetForumInfo(fid).Password.Trim()) == ForumUtils.GetCookie("forum" + strfid.Trim() + "password"))
                    {
                        allowviewforums += string.Format(",{0}", fid);
                    }
                }
            }

            return allowviewforums.Trim(',');
        }


        /// <summary>
        /// 获得已选取的论坛列表
        /// </summary>
        /// <returns>列表内容的html</returns>
        public static string GetForumCheckBoxListCache(string forums)
        {
            StringBuilder sb = new StringBuilder();

            /*
			sb.Append("<script language=\"JavaScript\">\r\n");
            sb.Append("function CheckAll(form)\r\n");
			sb.Append("{\r\n");
		    sb.Append("  for (var i=0;i<form.elements.length;i++)\r\n");
            sb.Append("{\r\n");
            sb.Append("var e = form.elements[i];\r\n");
            sb.Append("if (e.name != 'chkall')\r\n");
            sb.Append("e.checked = form.chkall.checked;\r\n");
            sb.Append("}\r\n");
            sb.Append("}\r\n");

            sb.Append("function SH_SelectOne()\r\n");
            sb.Append("{\r\n");
	        sb.Append("var obj = window.event.srcElement;\r\n");
	        sb.Append("if ( obj.checked == false)\r\n");
	        sb.Append("{\r\n");
		    sb.Append("  document.all.chkall.checked = obj.chcked;\r\n");
		    sb.Append("}\r\n");
            sb.Append("}\r\n");
    		sb.Append("</script>\r\n");
            */
            forums = "," + forums + ",";

            DataTable dt = Forums.GetForumListTable();
            int count = 1;
            foreach (DataRow dr in dt.Rows)
            {
                if (forums.ToLower() == ",all,")
                {
                    sb.AppendFormat(
                        "<td><input id=\"fidlist\" onclick=\"javascript:SH_SelectOne(this)\" type=\"checkbox\" value=\"{0}\"	name=\"fidlist\"  checked/> {1}</td>\r\n",
                        dr["fid"].ToString(), dr["name"].ToString());
                }
                else
                {
                    if (forums.IndexOf("," + dr["fid"].ToString() + ",") >= 0)
                    {
                        sb.AppendFormat(
                            "<td><input id=\"fidlist\" onclick=\"javascript:SH_SelectOne(this)\" type=\"checkbox\" value=\"{0}\"	name=\"fidlist\"  checked/> {1}</td>\r\n",
                            dr["fid"].ToString(), dr["name"].ToString());
                    }
                    else
                    {
                        sb.AppendFormat(
                            "<td><input id=\"fidlist\" onclick=\"javascript:SH_SelectOne(this)\" type=\"checkbox\" value=\"{0}\"	name=\"fidlist\"  /> {1}</td>\r\n",
                            dr["fid"].ToString(), dr["name"].ToString());
                    }
                }

                if (count > 3)
                {
                    sb.Append("			  </tr>\r\n");
                    sb.Append("			  <tr>\r\n");
                    count = 0;
                }
                count++;
            }
            return sb.ToString();
        }
    }
}