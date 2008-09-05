using System;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Threading;
using System.Xml;

using Discuz.Forum;
using Discuz.Common;
using Discuz.Config;
using Discuz.Entity;
using Discuz.Common.Xml;


namespace Discuz.Web.Admin
{
    public class top : AdminPage
    {
        public StringBuilder sb = new StringBuilder();

        public int menucount = 0;

        public int olid;
        public string showmenuid;
        public string toptabmenuid;
        public string mainmenulist;
        public string defaulturl;

        protected void Page_Load(object sender, EventArgs e)
        {
            //XmlDocumentExtender doc = new XmlDocumentExtender();
            //doc.Load(Page.Server.MapPath("../xml/navmenu.config"));
            //XmlNodeList mainmenus = doc.SelectNodes("/dataset/toptabmenu");
            //toptabmenuid = mainmenus.Item(0)["id"].InnerText;
            //mainmenulist = mainmenus.Item(0)["mainmenulist"].InnerText;
            //showmenuid = mainmenulist.Split(',')[0];
            //defaulturl = mainmenus.Item(0)["defaulturl"].InnerText;
            if (!Page.IsPostBack)
            {
                config = GeneralConfigs.GetConfig();
                // 如果IP访问列表有设置则进行判断
                if (config.Adminipaccess.Trim() != "")
                {
                    string[] regctrl = Utils.SplitString(config.Adminipaccess, "\n");
                    if (!Utils.InIPArray(DNTRequest.GetIP(), regctrl))
                    {
                        Context.Response.Redirect(BaseConfigs.GetForumPath + "admin/syslogin.aspx");
                        return;
                    }
                }

                //获取当前用户的在线否?
                OnlineUserInfo oluserinfo = new OnlineUserInfo();
                try
                {
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                }
                catch
                {
                    Thread.Sleep(2000);
                    oluserinfo = OnlineUsers.UpdateInfo(config.Passwordkey, config.Onlinetimeout);
                }


                #region 进行权限判断

                UserGroupInfo usergroupinfo = AdminUserGroups.AdminGetUserGroupInfo(oluserinfo.Groupid);
                if (oluserinfo.Userid <= 0 || usergroupinfo.Radminid != 1)
                {
                    Context.Response.Redirect(BaseConfigs.GetForumPath + "admin/syslogin.aspx");
                    return;
                }

                string secques = Users.GetUserInfo(oluserinfo.Userid).Secques;
                // 管理员身份验?
                if (Context.Request.Cookies["dntadmin"] == null || Context.Request.Cookies["dntadmin"]["key"] == null || ForumUtils.GetCookiePassword(Context.Request.Cookies["dntadmin"]["key"].ToString(), config.Passwordkey) != (oluserinfo.Password + secques + oluserinfo.Userid.ToString()))
                {
                    Context.Response.Redirect(BaseConfigs.GetForumPath + "admin/syslogin.aspx");
                    return;
                }
                else
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies["dntadmin"];
                    cookie.Values["key"] = ForumUtils.SetCookiePassword(oluserinfo.Password + secques + oluserinfo.Userid.ToString(), config.Passwordkey);
                    cookie.Expires = DateTime.Now.AddMinutes(30);
                    HttpContext.Current.Response.AppendCookie(cookie);
                }

                #endregion


                //System.Data.DataSet dsSrc = new System.Data.DataSet();
                //dsSrc.ReadXml(Page.Server.MapPath("../xml/navmenu.config"));
                //menucount = dsSrc.Tables["toptabmenu"].Rows.Count;

                //int count = 1;

                //foreach (System.Data.DataRow dr in dsSrc.Tables[2].Rows)
                //{
                //    if (count >= 7)
                //    {
                //        count = 0;
                //    }
                //    sb.Append("<li id=\"NtTab" + dr["id"] + "\" ><div id=\"NtDiv" + dr["id"] + "\"  Class=\"Currenttab" + count + "\"><a href=\"#\" id=\"NtA" + dr["id"] + "\" class=\"CurrentHoverTab" + count + "\" onclick=\"javscript:locationurl('" + dr["mainmenulist"].ToString().Split(',')[0] + "','" + dr["id"] + "','" + dr["mainmenulist"] + "','" + dr["defaulturl"] + "');\" onfocus=\"this.blur();\">" + dr["title"] + "</a></div></li>\r\n");

                //    count++;
                //}

                //dsSrc.Dispose();
            }

        }
    }
}
