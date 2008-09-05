using System;
using System.Web.UI;


using Discuz.Control;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Data;
using Discuz.Cache;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��Ӽ���
    /// </summary>
    
#if NET1
    public class addidentify : AdminPage
#else
    public partial class addidentify : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox name;
        protected Discuz.Control.UpFile uploadfile;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.Button AddIdentifyInfo;
        #endregion
#endif

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                uploadfile.UpFilePath = Server.MapPath("../../images/identify/");
            }
        }

        private void AddIdentifyInfo_Click(object sender, EventArgs e)
        {
            #region ��Ӽ�����¼

            if (this.CheckCookie())
            {
                string fileName = uploadfile.UpdateFile();
                if (fileName.Trim() == "")
                {
                    base.RegisterStartupScript("PAGE", "alert('û��ѡ�����ͼƬ');window.location.href='forum_identifymanage.aspx';");
                }
                if (DatabaseProvider.GetInstance().AddIdentify(name.Text, fileName))
                {
                    DNTCache.GetCacheService().RemoveObject("/Forum/TopicIdentifys");
                    DNTCache.GetCacheService().RemoveObject("/Forum/TopicIndentifysJsArray");
                    AdminVistLogs.InsertLog(this.userid, this.username, this.usergroupid, this.grouptitle, this.ip, "�����ļ����", name.Text);
                    base.RegisterStartupScript("PAGE", "window.location.href='forum_identifymanage.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("PAGE", "alert('����ʧ�ܣ�����������ԭ�е���ͬ');window.location.href='forum_identifymanage.aspx';");
                }
            }

            #endregion
        }

        #region Web ������������ɵĴ���

        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.AddIdentifyInfo.Click += new EventHandler(this.AddIdentifyInfo_Click);
        }

        #endregion


    }
}