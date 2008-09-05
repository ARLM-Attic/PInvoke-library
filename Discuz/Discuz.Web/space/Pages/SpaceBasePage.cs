using Discuz.Common;
using Discuz.Forum;
using Discuz.Space.Entities;
using Discuz.Space.Provider;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Data;
using System.Web;

namespace Discuz.Space.Pages
{
	/// <summary>
	/// SpaceBasePage 的摘要说明。
	/// </summary>
	public class SpaceBasePage : System.Web.UI.Page
	{
        /// <summary>
        /// 当前用户的用户名
        /// </summary>
		protected internal string username = "";

		/// <summary>
		/// 当前用户的用户ID
		/// </summary>
		protected internal int userid = 0;

        /// <summary>
        /// 当前访问的空间Id
        /// </summary>
		protected internal int spaceid = 0;

        /// <summary>
        /// 当前访问的空间主人用户Id
        /// </summary>
		protected internal int spaceuid = 0;

        /// <summary>
        /// 
        /// </summary>
        protected internal string forumpath = BaseConfigs.GetForumPath;

		protected GeneralConfigInfo config ;

		protected int olid;
        protected string spaceurl;
		protected string errorinfo = "";
	
		protected SpaceConfigInfo spaceconfiginfo = new SpaceConfigInfo();

        //不带文件名的forumurl地址
        protected string forumurlnopage = "../";

        protected string forumurl = GeneralConfigs.GetConfig().Forumurl;

		public SpaceBasePage()
		{ 
			spaceid = DNTRequest.GetInt("spaceid",0);
            
            spaceuid = DNTRequest.GetInt("spaceuid", 0);

			userid = Utils.StrToInt(ForumUtils.GetCookie("userid"), -1);

			config = GeneralConfigs.GetConfig();

			OnlineUserInfo oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);

			olid = oluserinfo.Olid;

			userid = oluserinfo.Userid;
	
			username = oluserinfo.Username;            
		
			if(DNTRequest.GetInt("postid",0) > 0)
			{
				SpacePostInfo spacePostInfo =  BlogProvider.GetSpacepostsInfo(Space.Data.DbProvider.GetInstance().GetSpacePost(DNTRequest.GetInt("postid",0)));
				spaceuid = spacePostInfo != null? spacePostInfo.Uid:0;
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
                    spaceuid = BlogProvider.GetUidBySpaceid(spaceid.ToString());
                    spaceconfiginfo = BlogProvider.GetSpaceConfigInfo(spaceuid);
                    //spaceconfiginfo = BlogProvider.GetSpaceConfigInfo(spaceid);
				}
			}

			if(spaceconfiginfo == null)
			{
				spaceconfiginfo = new SpaceConfigInfo();
				spaceconfiginfo.Status = SpaceStatusType.AdminClose;
			}

			if(spaceconfiginfo.Status != SpaceStatusType.Natural)
			{
				Context.Response.Redirect("index.aspx");
			}

            string host = HttpContext.Current.Request.Url.Host;
            int port = HttpContext.Current.Request.Url.Port;
            spaceurl = string.Format("http://{0}{1}{2}space/", host, (port == 80 ? "" : ":" + port), BaseConfigs.GetForumPath);

            if (SpaceActiveConfigs.GetConfig().Enablespacerewrite > 0 && spaceconfiginfo.Rewritename != string.Empty)
            {
                spaceurl += spaceconfiginfo.Rewritename;
            }
            else
            {
                spaceurl += "?uid=" + spaceconfiginfo.UserID;
            }



            if (forumurl.ToLower().IndexOf("http://") == 0)
            {
                //去掉http地址中的文件名称
                forumurlnopage = forumurl.Substring(0, forumurl.LastIndexOf('/')) + "/";
            }
            else
            {
                forumurl = "../" + config.Forumurl;
            }

            //if (config.Statstatus == 1)
            //{
            //    Stats.UpdateStatCount(userid < 1);
            //}

		}

	}
}
