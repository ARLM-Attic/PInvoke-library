using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Button = Discuz.Control.Button;
using DropDownList = Discuz.Control.DropDownList;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;
using Discuz.Entity;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// 快捷操作
    /// </summary>

#if NET1
    public class shortcut : AdminPage
#else
    public partial class shortcut : AdminPage
#endif
    {

#if NET1
        #region 控件声明
        protected Discuz.Control.TextBox Username;
        protected Discuz.Control.Button EditUser;
        protected Discuz.Control.DropDownTreeList forumid;
        protected Discuz.Control.Button EditForum;
        protected Discuz.Control.DropDownList Usergroupid;
        protected Discuz.Control.Button EditUserGroup;
        protected Discuz.Control.Button UpdateCache;
        protected Discuz.Control.DropDownList Templatepath;
        protected Discuz.Control.Button CreateTemplate;
        protected Discuz.Control.Button UpdateForumStatistics;
        #endregion
#endif

        public string filenamelist = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadTemplateInfo();

            //加载论坛版块信息
            forumid.BuildTree(DatabaseProvider.GetInstance().GetForumsTree());

            LinkDiscuzVersionPage();
        }

        public void LoadTemplateInfo()
        {
            #region 加载模板路径信息

            DirectoryInfo dirinfo = new DirectoryInfo(Server.MapPath("../../templates/" + Templatepath.SelectedValue + "/"));

            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file != null)
                {
                    string extname = file.Extension.ToLower();

                    if (extname.Equals(".htm") && (file.Name.IndexOf("_") != 0))
                    {
                        filenamelist += file.Name.Split('.')[0] + "|";
                    }
                }
            }

            #endregion
        }

        public void LinkDiscuzVersionPage()
        {
            #region 链接官方升级页面
            string linkurl = "http://nt.discuz.net/update/?ver=" + Utils.GetAssemblyVersion() + "&dbtype=" + BaseConfigs.GetDbType;

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(linkurl);

            try
            {
                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.AllowAutoRedirect = false;
                req.Timeout = 1500;

                HttpWebResponse Http_Res = (HttpWebResponse)req.GetResponse();

                if (Http_Res.StatusCode.ToString() != "OK")
                {
                    base.RegisterStartupScript( "form1", "<script language=javascript>document.getElementById('checkveriframe').parentNode.style.height=0+'px';document.getElementById('checkveriframe').src='about:blank';</script>");
                }
                else
                {
                    base.RegisterStartupScript("form1", "<script language=javascript>document.getElementById('checkveriframe').parentNode.style.height=0+'px';document.getElementById('checkveriframe').src='" + linkurl + "';</script>");
                }
            }
            catch
            {
                base.RegisterStartupScript( "form1", "<script language=javascript> if(navigator.appName.indexOf('Explorer') > -1){document.getElementById('checkveriframe').parentElement.innerHTML='&nbsp;&nbsp;&nbsp;  <img src=../images/hint.gif> 无法链接到Discuz!NT官方网站,因此无法得到最新官方信息';}else{document.getElementById('checkveriframe').parentNode.innerHTML='&nbsp;&nbsp;&nbsp;  <img src=../images/hint.gif> 无法链接到Discuz!NT官方网站,因此无法得到最新官方信息';}</script>");
            }

            #endregion
        }

        private void EditForum_Click(object sender, EventArgs e)
        {
            #region 重定向到指定的版块编辑页面

            if (forumid.SelectedValue != "0")
            {
                Response.Redirect("../forum/forum_EditForums.aspx?fid=" + forumid.SelectedValue);
            }
            else
            {
                base.RegisterStartupScript( "", "<script>alert('请您选择有效的论坛版块!');</script>");
            }

            #endregion
        }

        private void EditUserGroup_Click(object sender, EventArgs e)
        {
            #region 重定向到指定的用户组编辑页面

            if (Usergroupid.SelectedValue != "0")
            {
                int groupid = Convert.ToInt32(Usergroupid.SelectedValue);
                if (groupid <= 8)
                {
                    Response.Redirect("../global/global_editsysadminusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }

                int radminid = DatabaseProvider.GetInstance().GetRAdminIdByGroup(Utils.StrToInt(Usergroupid.SelectedValue, 0));
                if (radminid == 0)
                {
                    Response.Redirect("../global/global_editusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }
                if (radminid > 0)
                {
                    Response.Redirect("../global/global_editadminusergroup.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }
                if (radminid < 0)
                {
                    Response.Redirect("../global/global_editusergroupspecial.aspx?groupid=" + Usergroupid.SelectedValue);
                    return;
                }

            }
            else
            {
                base.RegisterStartupScript( "", "<script>alert('请您选择有效的用户组!');</script>");
            }

            #endregion
        }

        private void UpdateForumStatistics_Click(object sender, EventArgs e)
        {
            #region 更新论坛统计信息

            if (this.CheckCookie())
            {
                AdminCaches.ReSetStatistics();
                base.RegisterStartupScript( "PAGE",  "window.location.href='shortcut.aspx';");
            }

            #endregion
        }

        private void UpdateCache_Click(object sender, EventArgs e)
        {
            #region 更新所有缓存

            if (this.CheckCookie())
            {
                AdminCaches.ReSetAllCache();
                base.RegisterStartupScript( "PAGE",  "window.location.href='shortcut.aspx';");
            }

            #endregion
        }

        private void CreateTemplate_Click(object sender, EventArgs e)
        {
            #region 生成指定模板

            if (this.CheckCookie())
            {
                App_Code.Globals.BuildTemplate(Templatepath.SelectedValue);

                base.RegisterStartupScript( "PAGE", "window.location.href='shortcut.aspx';");
            }

            #endregion
        }

        private void EditUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("../global/global_usergrid.aspx?username=" + Username.Text.Trim());
        }


        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.EditUser.Click += new EventHandler(this.EditUser_Click);
            this.EditForum.Click += new EventHandler(this.EditForum_Click);
            this.EditUserGroup.Click += new EventHandler(this.EditUserGroup_Click);
            this.UpdateCache.Click += new EventHandler(this.UpdateCache_Click);
            this.CreateTemplate.Click += new EventHandler(this.CreateTemplate_Click);
            this.UpdateForumStatistics.Click += new EventHandler(this.UpdateForumStatistics_Click);
            this.Load += new EventHandler(this.Page_Load);

            //装入有效的模板信息项
            foreach (DataRow dr in AdminTemplates.GetAllTemplateList(Utils.GetMapPath(@"..\..\templates\")).Rows)
            {
                if (dr["valid"].ToString() == "1")
                {
                    Templatepath.Items.Add(new ListItem(dr["name"].ToString(), dr["directory"].ToString()));
                }
            }
            Username.AddAttributes("onkeydown", "if(event.keyCode==13) return(document.forms(0).EditUser.focus());");
            Usergroupid.AddTableData(DatabaseProvider.GetInstance().GetUserGroupsStr());
        }

        #endregion

    }
}