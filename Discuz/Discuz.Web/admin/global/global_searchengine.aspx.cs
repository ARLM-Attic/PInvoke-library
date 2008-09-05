using System;
using System.Web.UI;

using Discuz.Control;
using Discuz.Forum;
using Discuz.Config;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// 搜索引擎优化
    /// </summary>
    
#if NET1
    public class searchengine : AdminPage
#else
    public partial class searchengine : AdminPage
#endif
    {

#if NET1
        #region 控件声明
        protected Discuz.Control.RadioButtonList archiverstatus;  
        protected Discuz.Web.Admin.TextareaResize seotitle;
        protected Discuz.Web.Admin.TextareaResize seokeywords;
        protected Discuz.Web.Admin.TextareaResize seodescription;
        protected Discuz.Web.Admin.TextareaResize seohead;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button SaveInfo;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadConfigInfo();
            }
        }

        public void LoadConfigInfo()
        {
            #region 加载配置信息

            GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));
            seotitle.Text = __configinfo.Seotitle.ToString();
            seokeywords.Text = __configinfo.Seokeywords.ToString();
            seodescription.Text = __configinfo.Seodescription.ToString();
            seohead.Text = __configinfo.Seohead.ToString();
            archiverstatus.SelectedValue = __configinfo.Archiverstatus.ToString();
            

            #endregion
        }

        private void SaveInfo_Click(object sender, EventArgs e)
        {
            #region 保存设置信息

            if (this.CheckCookie())
            {
                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../../config/general.config"));

                __configinfo.Seotitle = seotitle.Text;
                __configinfo.Seokeywords = seokeywords.Text;
                __configinfo.Seodescription = seodescription.Text;
                __configinfo.Seohead = seohead.Text;
                __configinfo.Archiverstatus = Convert.ToInt16(archiverstatus.SelectedValue);
                
                GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../../config/general.config"));

                AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "搜索引擎优化设置", "");

                base.RegisterStartupScript( "PAGE",  "window.location.href='global_searchengine.aspx';");
            }

            #endregion
        }

        #region Web 窗体设计器生成的代码

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.SaveInfo.Click += new EventHandler(this.SaveInfo_Click);
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}