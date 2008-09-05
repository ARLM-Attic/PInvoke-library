using System;
using System.IO;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;

using Discuz.Forum;
using Discuz.Common;
using DataGrid = Discuz.Control.DataGrid;
using TextBox = Discuz.Control.TextBox;
using Discuz.Config;
using Discuz.Data;


namespace Discuz.Web.Admin
{
    /// <summary>
    /// ���ݿⱸ�ݺͻָ�
    /// </summary>

#if NET1
    public class backupandrestore : AdminPage
#else
    public partial class backupandrestore : AdminPage
#endif
    {

#if NET1
        #region �ؼ�����
        protected Discuz.Control.TextBox ServerName;
        protected Discuz.Control.TextBox strDbName;
        protected Discuz.Control.TextBox UserName;
        protected Discuz.Control.TextBox Password;
        protected Discuz.Control.TextBox backupname;
        protected Discuz.Control.Button BackUP;
        protected Discuz.Control.Hint Hint1;
        protected Discuz.Control.DataGrid Grid1;
        protected Discuz.Control.Button Restore;
        protected Discuz.Control.Button DeleteBackup;
        #endregion
#endif


        private static string backuppath = HttpContext.Current.Server.MapPath("backup/");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (DbHelper.Provider.IsBackupDatabase() == true)
            {
                if (!Page.IsPostBack)
                {

                    if (!base.IsFounderUid(userid))
                    {
                        Response.Write(base.GetShowMessage());
                        Response.End();
                        return;
                    }

                    #region �����ݿ����Ӵ���Ϣ

                    string connectionString = BaseConfigs.GetDBConnectString;
                    foreach (string info in connectionString.Split(';'))
                    {
                        if (info.ToLower().IndexOf("data source") >= 0 || info.ToLower().IndexOf("server") >= 0)
                        {
                            ServerName.Text = info.Split('=')[1].Trim();
                            continue;
                        }
                        if (info.ToLower().IndexOf("user id") >= 0 || info.ToLower().IndexOf("uid") >= 0)
                        {
                            UserName.Text = info.Split('=')[1].Trim();
                            continue;
                        }
                        if (info.ToLower().IndexOf("password") >= 0 || info.ToLower().IndexOf("pwd") >= 0)
                        {
                            Password.Text = info.Split('=')[1].Trim();
                            continue;
                        }

                        if (info.ToLower().IndexOf("initial catalog") >= 0 || info.ToLower().IndexOf("database") >= 0)
                        {
                            strDbName.Text = info.Split('=')[1].Trim();
                            break;
                        }
                    }
                    #endregion

                    Grid1.DataSource = buildGridData();
                    Grid1.DataBind();
                }

                backuppath = HttpContext.Current.Server.MapPath("backup/");
            }
            else
            {
                Response.Write("<script>alert('����ʹ�õ����ݿⲻ֧�����߱���!');</script>");

                Response.Write("<script>history.go(-1)</script>");

                Response.End();


            }
        }


        public DataTable buildGridData()
        {
            #region ������

            DataTable templatefilelist = new DataTable("templatefilelist");

            templatefilelist.Columns.Add("id", Type.GetType("System.Int32"));
            templatefilelist.Columns.Add("filename", Type.GetType("System.String"));
            templatefilelist.Columns.Add("createtime", Type.GetType("System.String"));
            templatefilelist.Columns.Add("fullname", Type.GetType("System.String"));

            DirectoryInfo dirinfo = new DirectoryInfo(Server.MapPath("backup"));
            int count = 1;

            foreach (FileSystemInfo file in dirinfo.GetFileSystemInfos())
            {
                if (file.Extension == ".config")
                {
                    DataRow dr = templatefilelist.NewRow();
                    dr["id"] = count;
                    dr["filename"] = file.Name.Substring(0, file.Name.LastIndexOf("."));
                    dr["createtime"] = file.CreationTime.ToString();
                    dr["fullname"] = "backup/" + file.Name;
                    templatefilelist.Rows.Add(dr);
                    count++;
                }
            }
            return templatefilelist;

            #endregion
        }

        public bool BackUPDB(string ServerName, string UserName, string Password, string strDbName, string strFileName)
        {
            #region ���ݿ�ı��ݵĴ���

            strFileName = strFileName.Replace(" ", "_");
            string message = DatabaseProvider.GetInstance().BackUpDatabase(backuppath, ServerName, UserName, Password, strDbName, strFileName);
            if (message != "")
            {
                base.RegisterStartupScript("", "<script language=javascript>alert('�������ݿ�ʧ��,ԭ��:" + message + "!');window.location.href='global_backupandrestore.aspx';</script>");
            }

            return true;
            #endregion
        }

        public bool RestoreDB(string ServerName, string UserName, string Password, string strDbName, string strFileName)
        {
            #region ���ݿ�Ļָ��Ĵ���

            strFileName = strFileName.Replace(" ", "_");
            string message = DatabaseProvider.GetInstance().RestoreDatabase(backuppath, ServerName, UserName, Password, strDbName, strFileName);

            if (message != string.Empty)
            {
                base.RegisterStartupScript("", "<script language=javascript>alert('�ָ����ݿ�ʧ��,ԭ��:" + message + "!');window.location.href='global_backupandrestore.aspx';</script>");

                return false;
            }
            return true;

            #endregion
        }

        private void BackUP_Click(object sender, EventArgs e)
        {
            #region ��ʼ��������

            if (this.CheckCookie())
            {
                if (!base.IsFounderUid(userid))
                {
                    Response.Write(base.GetShowMessage());
                    Response.End();
                    return;
                }


                if (backupname.Text == "")
                {
                    base.RegisterStartupScript("PAGE", "alert('�������Ʋ���Ϊ��');");
                    return;
                }

                aysncallback = new delegateBackUpDatabase(BackUPDB);
                AsyncCallback myCallBack = new AsyncCallback(CallBack);
                aysncallback.BeginInvoke(ServerName.Text, UserName.Text, Password.Text, strDbName.Text, backupname.Text, myCallBack, this.username); //
                LoadRegisterStartupScript("PAGE", "window.location.href='global_backupandrestore.aspx';");
            }

            #endregion
        }

        #region �첽�������ݻ�ָ��Ĵ���

        private delegate bool delegateBackUpDatabase(string ServerName, string UserName, string Password, string strDbName, string strFileName);

        //�첽�����������������Ĵ���
        private delegateBackUpDatabase aysncallback;


        public void CallBack(IAsyncResult e)
        {
            aysncallback.EndInvoke(e);
        }

        #endregion

        public string GetHttpLink(string filename)
        {
            return "<a href=" + filename + ">����</a>";
        }

        private void Restore_Click(object sender, EventArgs e)
        {
            #region �ָ�����

            if (this.CheckCookie())
            {
                if (!base.IsFounderUid(userid))
                {
                    Response.Write(base.GetShowMessage());
                    Response.End();
                    return;
                }

                if (DNTRequest.GetString("id") != "")
                {
                    string id = DNTRequest.GetString("id");
                    if (id.IndexOf(",0") > 0)
                    {
                        base.RegisterStartupScript("", "<script language=javascript>alert('��һ��ֻ��ѡ��һ�����ݽ����ύ!');window.location.href='global_backupandrestore.aspx';</script>");
                        return;
                    }
                    DataRow[] drs = buildGridData().Select("id=" + id.Replace("0 ", ""));

                    aysncallback = new delegateBackUpDatabase(RestoreDB);
                    AsyncCallback myCallBack = new AsyncCallback(CallBack);
                    aysncallback.BeginInvoke(ServerName.Text, UserName.Text, Password.Text, strDbName.Text, drs[0]["filename"].ToString(), myCallBack, this.username); //
                    base.LoadRegisterStartupScript("PAGE", "window.location.href='global_backupandrestore.aspx';");
                }
                else
                {
                    base.RegisterStartupScript("", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_backupandrestore.aspx';</script>");
                }
            }

            #endregion
        }

        private void DeleteBackup_Click(object sender, EventArgs e)
        {
            #region ɾ��ָ���ı����ļ�

            if (DNTRequest.GetString("id") != "")
            {
                string idlist = DNTRequest.GetString("id").Replace("0 ", "");
                foreach (DataRow dr in buildGridData().Select("id IN(" + idlist + ")"))
                {
                    if (Utils.FileExists(Utils.GetMapPath(dr["fullname"].ToString())))
                    {
                        File.Delete(Utils.GetMapPath(dr["fullname"].ToString()));
                    }
                }
                base.RegisterStartupScript("PAGE", "window.location.href='global_backupandrestore.aspx';");
            }
            else
            {
                base.RegisterStartupScript("", "<script>alert('��δѡ���κ�ѡ��');window.location.href='global_backupandrestore.aspx';</script>");
            }

            #endregion
        }

        #region Web ������������ɵĴ���

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.BackUP.Click += new EventHandler(this.BackUP_Click);
            this.Restore.Click += new EventHandler(this.Restore_Click);
            this.DeleteBackup.Click += new EventHandler(this.DeleteBackup_Click);

            Grid1.TableHeaderName = "���ݿⱸ���б�";
        }

        #endregion
    }
}