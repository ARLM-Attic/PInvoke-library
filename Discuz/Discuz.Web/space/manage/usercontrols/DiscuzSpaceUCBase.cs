using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Space.Entities;
using Discuz.Space.Provider;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Data;
using Discuz.Space.Data;

namespace Discuz.Space.Manage
{
	/// <summary>
	/// DiscuzSpace用户控件基类
	/// </summary>
	public class DiscuzSpaceUCBase: System.Web.UI.UserControl
	{

		protected internal string username = "";

		protected internal int userid = 0;

		public int olid;
	
		protected GeneralConfigInfo config ;
	
		public  string errorinfo = "";

		protected UserInfo _userinfo = new UserInfo();

		protected internal int spaceid = 0;

		protected internal int spaceuid = 0;

		//访问的空间信息
		protected SpaceConfigInfo spaceconfiginfo = new SpaceConfigInfo();

		//是否隐藏模块标题:0为显示,1为隐藏
		public int hidetitle = 0;

		//页面大小
		public int pagesize = 16;


		//后台导航顶部菜单项
		private int topmenu = 0;

		//后台导航左侧菜单项
		private int leftmenu = 0;


        //不带文件名的forumurl地址
        protected string forumurlnopage = "../";

        protected string forumurl = GeneralConfigs.GetConfig().Forumurl;

        protected string configspaceurlnopage = GeneralConfigs.GetConfig().Spaceurl;

		public DiscuzSpaceUCBase()
		{
			userid = Utils.StrToInt(ForumUtils.GetCookie("userid"), -1);
            
			config = GeneralConfigs.GetConfig();
			OnlineUserInfo oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
			olid = oluserinfo.Olid;

			userid = oluserinfo.Userid;
			username = oluserinfo.Username;


			_userinfo = Users.GetUserInfo(userid);

			topmenu = DNTRequest.GetInt("topmenu",0);
			leftmenu = DNTRequest.GetInt("leftmenu",0);

			hidetitle = DNTRequest.GetInt("hidetitle",0);

			spaceuid = DNTRequest.GetInt("spaceuid",0);
			spaceid = DNTRequest.GetInt("spaceid",0);

			
			if(DNTRequest.GetInt("postid",0) > 0)
			{
				SpacePostInfo __spacepostinfo =  BlogProvider.GetSpacepostsInfo(Space.Data.DbProvider.GetInstance().GetSpacePost(DNTRequest.GetInt("postid",0)));
				spaceuid = __spacepostinfo != null? __spacepostinfo.Uid:0;
			}
			
			
			if(spaceuid > 0)
			{
                spaceconfiginfo = BlogProvider.GetSpaceConfigInfo(spaceuid);
				spaceid = spaceconfiginfo.SpaceID;
			}
			else
			{
				if(spaceid > 0)
				{
                    
                   // spaceuid = spaceconfiginfo.UserID;
                    spaceuid=BlogProvider.GetUidBySpaceid(spaceid.ToString());
                    spaceconfiginfo = BlogProvider.GetSpaceConfigInfo(spaceuid);
				}
			}

			if(spaceconfiginfo == null)
			{
				spaceconfiginfo = new SpaceConfigInfo();
			}

			pagesize = spaceconfiginfo.Bpp;


            if (forumurl.ToLower().IndexOf("http://") == 0)
            {
                //去掉http地址中的文件名称
                forumurlnopage = forumurl.Substring(0, forumurl.LastIndexOf('/')) + "/";
            }
            else
            {
                forumurl = "../" + forumurl;
            }


            if (configspaceurlnopage.ToLower().IndexOf("http://") < 0)
            {
                configspaceurlnopage = forumurlnopage;
            }
            else
            {
                configspaceurlnopage = configspaceurlnopage.ToLower().Substring(0, configspaceurlnopage.LastIndexOf('/')) + "/" ;
            }
 	
		}

		public string GetControlLink()
		{
			return "topmenu="+topmenu+"&leftmenu="+leftmenu;
		}

		/// <summary>
		/// 当前用户是否是SPACE的所有人
		/// </summary>
		/// <returns></returns>
		public bool IsHolder()
		{
            SpaceConfigInfo __spaceconfiginfo = BlogProvider.GetSpaceConfigInfo(userid);
			
			if (__spaceconfiginfo == null ) return false;

			if( DNTRequest.GetInt("spaceid",0) == __spaceconfiginfo.SpaceID)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	
		/// <summary>
		/// 写入加载层的信息
		/// </summary>
		/// <param name="ajaxdivname">加载信息层的名称</param>
		/// <param name="ajaxdivcontent">加载信息层的内容</param>
		/// <returns></returns>
		protected string WriteLoadingDiv(string ajaxdivname ,string ajaxdivcontent)
		{
			StringBuilder __stringBuilder =new StringBuilder();

//			__stringBuilder.Append("<div id=\""+ajaxdivname+"\" style=\"display:block;position:relative;border:1px dotted #DBDDD3; background-color:#FDFFF2; margin:4px;width:99%;valign:middle\">\r\n");
//			__stringBuilder.Append("<table>\r\n");
//			__stringBuilder.Append("<tr>\r\n");
//			__stringBuilder.Append("<td>\r\n");
//			__stringBuilder.Append("	<img border=\"0\" src=\"images/ajax_loading.gif\" /></td><td valign=middle>"+ajaxdivcontent+",请稍等.....\r\n");
//			__stringBuilder.Append("</td>\r\n");
//			__stringBuilder.Append("</tr>\r\n");
//			__stringBuilder.Append("</table>\r\n");
//			__stringBuilder.Append("</div>\r\n");

			__stringBuilder.Append("<div id=\""+ajaxdivname+"\" style=\"display:block;position:relative;border:0px; background-color:transparent; margin:4px;valign:middle\">"+ajaxdivcontent+"...</div>\r\n");
			
			return __stringBuilder.ToString();
		}



		/// <summary>
		/// 检查cookie是否有效
		/// </summary>
		/// <returns></returns>
		public bool CheckCookie()
		{
			return true;
		}


		/// <summary>
		/// 分页函数
		/// </summary>
		/// <param name="recordcount">总记录数</param>
		/// <param name="pagesize">每页记录数</param>
		/// <param name="currentpage">当前页数</param>
		public string AjaxPagination(int recordcount, int pagesize, int currentpage, string usercontrolname ,string paramstr ,string divname )
		{
			int allcurrentpage = 0;
			int next = 0;
			int pre = 0;
			int startcount = 0;
			int endcount = 0;
			string currentpagestr = "<BR />";

			if (currentpage < 1) 
            { 
                currentpage = 1; 
            }

			//计算总页数
			if (pagesize != 0)
			{
				allcurrentpage = (recordcount / pagesize);
				allcurrentpage = ((recordcount % pagesize) != 0 ? allcurrentpage + 1 : allcurrentpage);
				allcurrentpage = (allcurrentpage == 0 ? 1 : allcurrentpage);
			}
			next = currentpage + 1;
			pre = currentpage - 1;

            //中间页起始序号
			startcount = (currentpage + 5) > allcurrentpage ? allcurrentpage - 9 : currentpage - 4;
			
            //中间页终止序号
			endcount = currentpage < 5 ? 10 : currentpage + 5;

            //为了避免输出的时候产生负数，设置如果小于1就从序号1开始
			if (startcount < 1) 
            { 
                startcount = 1; 
            }

            //页码+5的可能性就会产生最终输出序号大于总页码，那么就要将其控制在页码数之内
			if (allcurrentpage < endcount)
            { 
                endcount = allcurrentpage; 
            }
		
			if(startcount>1)
			{
                currentpagestr += currentpage > 1 ? "&nbsp;&nbsp;<a href=\"###\"  onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + pre + "');\" title=\"上一页\">上一页</a>" : "";
			}
			
            //当页码数大于1时, 则显示页码
            if (endcount > 1)
            {
                //中间页处理, 这个增加时间复杂度，减小空间复杂度
                for (int i = startcount; i <= endcount; i++)
                {
                    currentpagestr += currentpage == i ? "&nbsp;" + i + "" : "&nbsp;<a href=\"###\"  onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + i + "');\">" + i + "</a>";
                }
            }
			
			if(endcount<allcurrentpage)
			{
                currentpagestr += currentpage != allcurrentpage ? "&nbsp;&nbsp;<a href=\"###\" onclick=\"javascript:AjaxHelper.Updater('" + usercontrolname + "','" + divname + "', 'load=true&" + paramstr + "&currentpage=" + next + "');\" title=\"下一页\">下一页</a>&nbsp;&nbsp;" : "";
			}

            if (endcount > 1)
            {
                currentpagestr += "&nbsp; &nbsp; &nbsp; &nbsp;";
            }

            currentpagestr += "共 " + allcurrentpage + " 页, 当前第 " + currentpage + " 页, 共 " + recordcount + " 条记录";

			return currentpagestr;

		}

		//		//写入加载层的脚本
		//		protected string WriteLoadingDivScript()
		//		{
		//			StringBuilder __stringBuilder =new StringBuilder();
		//
		//			__stringBuilder.Append("<SCRIPT LANGUAGE=\"JavaScript\">\r\n");
		//			__stringBuilder.Append("document.getElementById('"+ajaxdivname+"').style.display = \"none\";\r\n");
		//			__stringBuilder.Append("</SCRIPT>\r\n");
		//	
		//			return __stringBuilder.ToString();
		//		}


	}
}
