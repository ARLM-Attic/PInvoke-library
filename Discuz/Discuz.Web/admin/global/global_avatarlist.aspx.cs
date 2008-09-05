using System;
using System.IO;
using System.Data;
using System.Web.UI;

using Discuz.Common;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ͷ���б�
    /// </summary>
    public class avatarlist : AdminPage
    {
 
    
        public DataTable avatarfilelist;
        public string avatar = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                #region ����ͷ���б�����

                avatarfilelist = new DataTable("avatarlist");
                avatarfilelist.Columns.Add("filenamepath", Type.GetType("System.String"));
                avatarfilelist.Columns.Add("filename", Type.GetType("System.String"));
                avatarfilelist.Columns.Add("_id", Type.GetType("System.Int32"));

                DirectoryInfo dirinfo = new DirectoryInfo(Server.MapPath("../../avatars/common/"));
                int i = 1;
                foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
                {
                    if (file != null)
                    {
                        string extname = file.Extension.ToLower();

                        if (extname.Equals(".jpg") || extname.Equals(".gif") || extname.Equals(".png"))
                        {
                            DataRow dr = avatarfilelist.NewRow();
                            if (DNTRequest.GetString("path") == "1")
                            {
                                dr["filename"] = file.Name.Split('.')[0];
                            }
                            else
                            {
                                dr["filename"] = "avatars\\common\\" + file.Name;
                            }

                            dr["filenamepath"] = "avatars\\common\\" + file.Name;
                            dr["_id"] = i;
                            i++;
                            avatarfilelist.Rows.Add(dr);
                        }
                    }
                }

                #endregion
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