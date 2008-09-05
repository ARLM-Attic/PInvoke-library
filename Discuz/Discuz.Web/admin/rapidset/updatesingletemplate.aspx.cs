using System;

using Discuz.Common;
using Discuz.Config;
using Discuz.Forum;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ��������ģ��ҳ��
    /// </summary>
    public class updatesingletemplate : AdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int result = -1;
                string filename = DNTRequest.GetString("filename");
                string path = DNTRequest.GetString("path");

                int templateid = Convert.ToInt32(AdminTemplates.GetAllTemplateList(Utils.GetMapPath(@"..\..\templates\")).Select("directory='" + path + "'")[0]["templateid"].ToString());

                if (filename != "")
                {
                    ForumPageTemplate forumpagetemplate = new ForumPageTemplate();
                    forumpagetemplate.GetTemplate(BaseConfigs.GetForumPath,path, filename, 1, templateid);
                    result = 1;
                }

                Response.Write(result);
                Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
                Response.Expires = -1;
                Response.End();
            }
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
        }

        #endregion
    }
}