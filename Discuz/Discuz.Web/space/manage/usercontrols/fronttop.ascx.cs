using System;
using System.Web;
using Discuz.Forum;
using Discuz.Space.Manage;
using Discuz.Entity;
using Discuz.Config;

#if NET1
#else
using Discuz.Common.Generic;
#endif

using System.Text;

namespace Discuz.Space.Manage
{
	/// <summary>
	///		页面首部控件
	/// </summary>
	public class fronttop : DiscuzSpaceUCBase
	{
		public string spacehttplink = "";

		//是否显示用户面板
		public bool isshowuserpanel = false;

		protected internal string userkey;

        public string tabs = string.Empty;

        //private bool spaceEditable = false;

        protected bool isLogged = false;

        protected string configspaceurl = null;

        protected string configalbumurl = null;

		public fronttop()
        {
            if (userid == -1)
            {
                return;
            }

            //已登录
            ShortUserInfo _user = Users.GetShortUserInfo(userid);

            username = _user.Username;
            spaceid = _user.Spaceid;
            if (_user.Password.Length > 16)
            {
                userkey = _user.Password.Substring(4, 8).Trim();
            }

            configspaceurl = config.Spaceurl;

            if (configspaceurl.ToLower().IndexOf("http://") < 0)
            {
                configspaceurl = forumurlnopage + configspaceurl;
            }

            configalbumurl = config.Albumurl;
            if (configalbumurl.ToLower().IndexOf("http://") < 0)
            {
                configalbumurl = forumurlnopage + configalbumurl;
            }

        }

       
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			config = GeneralConfigs.GetConfig();

//			if (config.Forumurl.EndsWith("/"))
//			{
//				spacehttplink = config.Forumurl + "space/?uid=" + spaceuid;
//			}
//			else
//			{
//				if ((config.Forumurl.IndexOf(".asp") > 0) || (config.Forumurl.IndexOf(config.Extname.Trim()) > 0))
//				{
//					spacehttplink = config.Forumurl.Substring(0, config.Forumurl.LastIndexOf("/")) + "/space/?uid=" + spaceuid;
//				}
//				else
//				{
//					spacehttplink = config.Forumurl + "/space/?uid=" + spaceuid;
//				}
//			}
            //string host = HttpContext.Current.Request.Url.Host;
            //int port = HttpContext.Current.Request.Url.Port;
            //spacehttplink = string.Format("http://{0}{1}{2}space/", host, (port == 80 ? "" : ":" + port), BaseConfigs.GetForumPath);//"http://" + host + (port == 80 ? "" : ":" + port) + BaseConfigs.GetForumPath + "?uid=" + this.spaceuid;
            //if (SpaceActiveConfigs.GetConfig().Enablespacerewrite > 0 && spaceconfiginfo.Rewritename != string.Empty)
            //{
            //    spacehttplink += spaceconfiginfo.Rewritename;
            //}
            //else
            //{
            //    spacehttplink += "?uid=" + spaceconfiginfo.UserID;
            //}

            spacehttplink = config.Spaceurl;
            if (spacehttplink.ToLower().IndexOf("http://") == 0)
            {
                spacehttplink = spacehttplink.Substring(0, spacehttplink.LastIndexOf('/')) + "/";
            }
            else
            {
                spacehttplink = HttpContext.Current.Request.Url.ToString();
                spacehttplink = spacehttplink.Substring(0, spacehttplink.LastIndexOf('/')) + "/";
            }
            if (SpaceActiveConfigs.GetConfig().Enablespacerewrite > 0 && spaceconfiginfo.Rewritename != string.Empty)
            {
                spacehttplink += spaceconfiginfo.Rewritename;
            }
            else
            {
                spacehttplink += "?uid=" + spaceconfiginfo.UserID;
            }


            if (userid > 0)
            {
                isLogged = true;
                //if (userid == spaceconfiginfo.UserID)
                //{
                //    spaceEditable = true;
                //}
            }

			if (spaceid > 0)
			{
				isshowuserpanel = base.IsHolder();

#if NET1
				TabInfoCollection tc = Spaces.GetTabInfoCollectionByUserID(spaceconfiginfo.UserID);
				if (tc == null)
                {
                    tc = new TabInfoCollection();
                }
#else				
                List<TabInfo> tc = Spaces.GetTabInfoCollectionByUserID(spaceconfiginfo.UserID);
				if (tc == null)
                {
                    tc = new List<TabInfo>();
                }
#endif

                

                StringBuilder tabsHtml = new StringBuilder();

                foreach (TabInfo tab in tc)
                {
                    tabsHtml.Append("<li class=\"tab unselectedtab_l\">&nbsp;</li>");
                    string tabformat = "<li id=\"tab{0}_view\" class=\"tab unselectedtab\" style=\"display:block\" onclick=\"";
                    //if (spaceEditable)
                    //{
                    //    tabformat += "_dlsetp('ct={0}');document.location='index.aspx';";
                    //}
                    //else
                    //{
                        tabformat += "document.location='index.aspx'+ds_i()+'tab={0}&uid=" + spaceconfiginfo.UserID + "'";
                    //}
                    tabformat += "\"><span id=\"tab{0}_view_title\">{1}</span></li>";
                    tabsHtml.AppendFormat(tabformat, tab.TabID, tab.TabName);
                    tabsHtml.Append("<li class=\"tab unselectedtab_r\">&nbsp;</li>");
                }

                tabs = tabsHtml.ToString();
			}


		}
		
		#region Web 窗体设计器生成的代码

		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}

		#endregion
	}
}