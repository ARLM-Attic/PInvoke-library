using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;

namespace Discuz.Web
{
	/// <summary>
	/// 搜索页面
	/// </summary>
	public class search : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 版块列表
        /// </summary>
		public string forumlist;
        /// <summary>
        /// 搜索缓存Id
        /// </summary>
		public int searchid;
        /// <summary>
        /// 当前页码
        /// </summary>
		public int pageid;
        /// <summary>
        /// 主题数量
        /// </summary>
		public int topiccount;
        /// <summary>
        /// 相册数量
        /// </summary>
		public int albumcount;
        /// <summary>
        /// 日志数量
        /// </summary>
		public int blogcount;
        /// <summary>
        /// 分页数量
        /// </summary>
		public int pagecount;
        /// <summary>
        /// 分页页码链接
        /// </summary>
		public string pagenumbers;
        /// <summary>
        /// 搜索结果数量
        /// </summary>
		public int searchresultcount;
        /// <summary>
        /// 搜索出的主题列表
        /// </summary>
		public DataTable topiclist;
        /// <summary>
        /// 帖子分表列表
        /// </summary>
		public DataTable tablelist;
        /// <summary>
        /// 搜索出的日志列表
        /// </summary>
        public DataTable spacepostlist;
        /// <summary>
        /// 搜索出的相册列表
        /// </summary>
        public DataTable albumlist;
        /// <summary>
        /// 当此值为true时,显示搜索结果提示
        /// </summary>
		public bool searchpost;
        /// <summary>
        /// 搜索类型
        /// </summary>
		public string type = "post";
        /// <summary>
        /// 当前主题页码
        /// </summary>
        public int topicpageid;
        /// <summary>
        /// 主题分页总数
        /// </summary>
        public int topicpagecount;
        /// <summary>
        /// 主题分页页码链接
        /// </summary>
        public string topicpagenumbers;
        /// <summary>
        /// 当前日志分页页码
        /// </summary>
        public int blogpageid;
        /// <summary>
        /// 日志分页总数
        /// </summary>
        public int blogpagecount;
        /// <summary>
        /// 日志分页页码链接
        /// </summary>
        public string blogpagenumbers;
        /// <summary>
        /// 当前相册页码
        /// </summary>
        public int albumpageid;
        /// <summary>
        /// 相册分页总数
        /// </summary>
        public int albumpagecount;
        /// <summary>
        /// 相册分页页码链接
        /// </summary>
        public string albumpagenumbers;
        #endregion

        protected override void ShowPage()
		{
			pagetitle = "搜索";
			searchid = DNTRequest.GetInt("searchid", -1);
			searchresultcount = 0;
			
			if (usergroupinfo.Allowsearch == 0)
			{
				AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有搜索的权限");
				return;
			}

            if (usergroupinfo.Allowsearch == 2 && DNTRequest.GetInt("keywordtype", 0) == 1)
            {
                AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有全文搜索的权限");
                return;
            }

			searchpost = false;
			if (!ispost)
			{
				tablelist = Posts.GetAllPostTableName();
			}
			
			if (DNTRequest.IsPost() || DNTRequest.GetString("posterid") != "")
			{

				searchpost = true;
				//　如果当前用户非管理员并且论坛设定了禁止全文搜索时间段，当前时间如果在其中的一个时间段内，不允许全文搜索
				if (useradminid != 1 && DNTRequest.GetInt("keywordtype", 0) == 1 && usergroupinfo.Disableperiodctrl != 1)
				{
					string visittime = "";
                    if (Scoresets.BetweenTime(config.Searchbanperiods, out visittime))
					{
						AddErrLine("在此时间段( " + visittime + " )内用户不可以进行全文搜索");
						return;
					}
					
				}

				if (useradminid != 1)
				{
					//判断一分钟内搜索的次数是不是超过限制值
					if (!Statistics.CheckSearchCount(config.Maxspm))
					{
						AddErrLine("抱歉,系统在一分钟内搜索的次数超过了系统安全设置的上限,请稍候再试");
						return;
					}


					int Interval = Utils.StrDateDiffSeconds(lastsearchtime, config.Searchctrl);
					if (Interval <= 0)
					{
						AddErrLine("系统规定搜索间隔为" + config.Searchctrl.ToString() + "秒, 您还需要等待 " + (Interval * -1).ToString() + " 秒");
						return;
					}

					//不是管理员，则如果设置搜索扣积分时扣除用户积分
					if (UserCredits.UpdateUserCreditsBySearch(base.userid) == -1)
					{
						AddErrLine("您的积分不足, 不能执行搜索操作");
						return;
					}
				}

				int posterid = DNTRequest.GetInt("posterid", -1);
				int searchtime = DNTRequest.GetInt("searchtime", 0);
				string searchforumid = DNTRequest.GetString("searchforumid").Trim();
				string[] forumidlist = Utils.SplitString(searchforumid, ",");

				if (DNTRequest.GetString("keyword").Equals("") && DNTRequest.GetString("poster").Equals("") && DNTRequest.GetString("posterid").Equals(""))
				{
					AddErrLine("关键字和用户名不能同时为空");
					return;
				}
			
				if (posterid > 0)
				{
					if (!Users.Exists(posterid))
					{
						AddErrLine("指定的用户ID不存在");
						return;
					}
				}
				else if (!DNTRequest.GetString("poster").Equals(""))
				{
					posterid = Users.GetUserID(DNTRequest.GetString("poster"));
					if (posterid == -1)
					{
						AddErrLine("搜索用户名不存在");
						return;
					}
				}
			
				if (!searchforumid.Equals(""))
				{
					foreach (string strid in forumidlist)
					{
						if (!Utils.IsNumeric(strid))
						{
							AddErrLine("非法的搜索版块ID");
							
							return;
						}
					}
				}

				type = DNTRequest.GetString("type").ToLower();
                int keywordtype = DNTRequest.GetInt("keywordtype", 0);
				if (type == "author")
                    keywordtype = 8;

                if (DNTRequest.GetString("keyword") == string.Empty && posterid > 0 && type == string.Empty)
                {
                    type = "author";
                    keywordtype = 8;
                }

				if (type != "")
				{
					if (!Utils.InArray(type, "post,digest,spacepost,album,author"))// (type != "topic") && (type != "digest") && (type != "post"))
					{
						AddErrLine("非法的参数信息");
						return;
					}
        		}

				int posttableid = DNTRequest.GetInt("posttableid", 0);

                searchid = Searches.Search(posttableid, userid, usergroupid, DNTRequest.GetString("keyword").Trim(), posterid, type, searchforumid, keywordtype, searchtime, DNTRequest.GetInt("searchtimetype", 0), DNTRequest.GetInt("resultorder", 0), DNTRequest.GetInt("resultordertype", 0));

                switch (keywordtype)
                { 
                    case 2:
                        type = "spacepost";
                        break;
                    case 3:
                        type = "album";
                        break;
                    case 8:
                        type = "author";
                        break;
                    default:
                        type = string.Empty;
                        break;
                }
				if (searchid > 0)
				{				
					SetUrl("search.aspx?type=" + type + "&searchid=" + searchid.ToString());					
                	
					SetMetaRefresh();
					SetShowBackLink(false);
					AddMsgLine("搜索完毕, 稍后将转到搜索结果页面");					
				}
				else
				{
					AddMsgLine("抱歉, 没有搜索到符合要求的记录");
				}
				return;
			}
			else
			{
				searchid = DNTRequest.GetInt("searchid", -1);
				if (searchid > 0)
				{
					pageid = DNTRequest.GetInt("page", 1);
	
					type = DNTRequest.GetString("type").ToLower();
					if (type != "")
					{
						if (!Utils.InArray(type, "topic,spacepost,album,author"))// (type != "topic") && (type != "digest") && (type != "post"))
						{
							AddErrLine("非法的参数信息");
							return;
						}
					}

					int posttableid = DNTRequest.GetInt("posttableid", 0);
                    switch (type)
                    { 
                        case "spacepost":
                            spacepostlist = Searches.GetSearchCacheList(posttableid, searchid, 16, pageid, out topiccount, type);
                            break;
                        case "album":
                            albumlist = Searches.GetSearchCacheList(posttableid, searchid, 16, pageid, out topiccount, type);
                            break;
                        case "author":
                            blogpageid = DNTRequest.GetInt("blogpage", 1);
                            albumpageid = DNTRequest.GetInt("albumpage", 1);
                            topicpageid = DNTRequest.GetInt("topicpage", 1);
                            spacepostlist = Searches.GetSearchCacheList(posttableid, searchid, 16, blogpageid, out blogcount, "spacepost");
                            albumlist = Searches.GetSearchCacheList(posttableid, searchid, 16, albumpageid, out albumcount, "album");
                            topiclist = Searches.GetSearchCacheList(posttableid, searchid, 16, topicpageid, out topiccount, "");

                            blogpageid = CalculateCurrentPage(blogcount, blogpageid, out blogpagecount);
                            albumpageid = CalculateCurrentPage(albumcount, albumpageid, out albumpagecount);
                            topicpageid = CalculateCurrentPage(topiccount, topicpageid, out topicpagecount);

                            topicpagenumbers = Utils.GetPageNumbers(topicpageid, topicpagecount, "search.aspx?show=topic&type=" + type + "&searchid=" + searchid.ToString() + "&blogpage=" + blogpageid.ToString() + "&albumpage=" + albumpageid.ToString(), 8, "topicpage", "#1");
                            albumpagenumbers = Utils.GetPageNumbers(albumpageid, albumpagecount, "search.aspx?show=album&type=" + type + "&searchid=" + searchid.ToString() + "&blogpage=" + blogpageid.ToString() + "&topicpage=" + topicpageid.ToString(), 8, "albumpage", "#3");
                            blogpagenumbers = Utils.GetPageNumbers(blogpageid, blogpagecount, "search.aspx?show=blog&type=" + type + "&searchid=" + searchid.ToString() + "&topicpage=" + topicpageid.ToString() + "&albumpage=" + albumpageid.ToString(), 8, "blogpage", "#2");

                            
                            return;
                        default:
                            topiclist = Searches.GetSearchCacheList(posttableid, searchid, 16, pageid, out topiccount, type);
                            break;
                    }

                    if (topiccount == 0)
					{
						AddErrLine("不存在的searchid");
						return;
					}
                    CalculateCurrentPage();
                    //得到页码链接
                    pagenumbers = Utils.GetPageNumbers(pageid, pagecount, "search.aspx?type=" + type + "&searchid=" + searchid.ToString(), 8);

					return;
			
				}
				else
				{
					forumlist = Caches.GetForumListBoxOptionsCache();
				}
			}

		}

        private void CalculateCurrentPage()
        {
            //获取总页数
            pagecount = topiccount % 16 == 0 ? topiccount / 16 : topiccount / 16 + 1;
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
        }
        private int CalculateCurrentPage(int listcount, int pageid, out int pagecount)
        {
            //int pagecount;
            //获取总页数
            pagecount = listcount % 16 == 0 ? listcount / 16 : listcount / 16 + 1;
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
            return pageid;
        }
    }
}
