using System;
using System.IO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text.RegularExpressions;


using Discuz.Common;
using Discuz.Config;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Config.Provider;
using TextBox = Discuz.Control.TextBox;
using DropDownList = Discuz.Control.DropDownList;
using Discuz.Cache;

namespace Discuz.Install
{
    /// <summary>
    /// setup 的摘要说明. 
    /// </summary>
    public class install : SetupPage
    {
        protected TextBox tableprefix;
        protected TextBox forumpath;
        protected TextBox initialcatalog;
        protected TextBox datasource;
        protected TextBox userid;
        protected TextBox password;

        protected TextBox forumtitle;
        protected TextBox forumurl;
        protected TextBox webtitle;
        protected TextBox weburl;

        protected System.Web.UI.WebControls.Button ClearDBInfo;
        //protected System.Web.UI.WebControls.Button upgrade;
        protected Literal msg;

        protected TextBox systemadminname;
        protected TextBox systemadminpws;
        protected TextBox txtDbFileName;
        protected TextBox adminemail;
        protected DropDownList ddlDbType;
        protected System.Web.UI.HtmlControls.HtmlInputCheckBox sponsercheck;

        private string selectDbType = string.Empty;
        private void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //读取默认dnt.config文件内容
                BaseConfigInfo bci = BaseConfigProvider.GetRealBaseConfig();
                if (bci != null)
                {
                    BaseConfigProvider.SetInstance(bci);
                    string truePath = Utils.GetTrueForumPath();
                    truePath = truePath.Substring(0, truePath.Length - 8);
                    //填充界面
                    tableprefix.Text = bci.Tableprefix;
                    forumpath.Text = truePath;
                    FillDatabaseInfo(bci.Dbconnectstring);

                    if (!ReadForumPath())
                    {
                        msg.Visible = true;
                    }
                }
                systemadminpws.AddAttributes("onkeyup", "return loadinputcontext(this);");
            }
        }

        /// <summary>
        /// 从配置文件中的连接字符串填充界面上的数据库配置信息
        /// </summary>
        /// <param name="connectionstring"></param>
        private void FillDatabaseInfo(string connectionstring)
        {

            foreach (string info in connectionstring.Split(';'))
            {
                if (info.ToLower().IndexOf("data source") >= 0)
                {
                    datasource.Text = info.Split('=')[1].Trim();
                    continue;
                }
                if (info.ToLower().IndexOf("initial catalog") >= 0)
                {
                    initialcatalog.Text = info.Split('=')[1].Trim();
                    continue;
                }
                if (info.ToLower().IndexOf("user id") >= 0)
                {
                    userid.Text = info.Split('=')[1].Trim();
                    continue;
                }
                if (info.ToLower().IndexOf("password") >= 0)
                {
                    password.Text = info.Split('=')[1].Trim();
                    continue;
                }
                if (connectionstring.IndexOf("Microsoft.Jet.OleDb") >= 0)
                    datasource.Text = string.Empty;
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
            this.ClearDBInfo.Click += new System.EventHandler(this.ClearDBInfo_Click);
            //this.upgrade.Click += new System.EventHandler(this.upgrade_Click);
            this.Load += new System.EventHandler(this.Page_Load);

            //this.upgrade.Text = "从" + upgradeproductname + "升级";

        }
        #endregion

        #region web.config检查
        //判断web.config文件中的设置是否正确
        public bool ReadForumPath()
        {
            string webconfigpath = Path.Combine(Request.PhysicalApplicationPath, "web.config");

            //如果文件不存在退出
            if (!Utils.FileExists(webconfigpath) && (!Utils.FileExists(Server.MapPath("../web.config"))))
            {
                return false;
            }

            try
            {

                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(webconfigpath);

                bool httpmodenodeexist = false;
                bool globalizationnodeexist = false;
                bool pagesnodeexist = false;
                XmlNodeList topM = xmldoc.DocumentElement.ChildNodes;
                foreach (XmlNode elementtop in topM)
                {
                    if (elementtop.Name.ToLower() == "system.web")
                    {
                        foreach (XmlNode element in elementtop.ChildNodes)
                        {
                            if (element.Name.ToLower() == "httpmodules")
                            {
                                XmlNodeList _node = element.ChildNodes;
                                if (_node.Count > 0)
                                {
                                    foreach (XmlNode el in _node)
                                    {
                                        //XmlNode pathnode = null;
                                        //pathnode = el.SelectSingleNode("//add[@type ='Discuz.Forum.HttpModule, Discuz.Forum']");
                                        //if (pathnode.Attributes["name"].Value != "HttpModule")
                                        //    return false;
                                        if (el.Attributes["type"].Value == "Discuz.Forum.HttpModule, Discuz.Forum" && el.Attributes["name"].Value == "HttpModule")
                                        {
                                            httpmodenodeexist = true;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (element.Name.ToLower() == "globalization")
                            {
                                if (element.Attributes["requestEncoding"].Value != "utf-8")
                                    return false;
                                if (element.Attributes["responseEncoding"].Value != "utf-8")
                                    return false;
                                if (element.Attributes["fileEncoding"].Value != "utf-8")
                                    return false;
                                globalizationnodeexist = true;
                            }

                            if (element.Name.ToLower() == "pages")
                            {
                                if (element.Attributes["validateRequest"].Value != "false")
                                    return false;
                                pagesnodeexist = true;
                            }
                        }
                    }
                }
                if ((httpmodenodeexist) && (globalizationnodeexist) && (pagesnodeexist))
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }

        #endregion

        #region
        /*
		//重写web.config的DNT_ForumPath属性
		public void ResetForumPath(string webconfigpath)
		{
			XmlDocument  xmldoc= new XmlDocument();
			xmldoc.Load(webconfigpath);

			XmlNodeList topM=xmldoc.DocumentElement.ChildNodes;
			foreach(XmlNode element in topM)
			{
				if(element.Name.ToLower()=="appsettings")
				{
					XmlNodeList _node=element.ChildNodes;
					if ( _node.Count >0 ) 
					{
						foreach(XmlNode el in _node)
						{
							XmlNode pathnode=null;
							pathnode=el.SelectSingleNode("//add[@key ='DNT_ForumPath']");
							pathnode.Attributes["value"].Value=forumpath.Text;
						}
					}
				}
			}
			xmldoc.Save(webconfigpath);
		}
		*/
        #endregion

        protected void ClearDBInfo_Click(object sender, EventArgs e)
        {

            #region 验证输入
            //验证密码长度
            if (systemadminpws.Text.Length < 6)
            {
#if NET1
                Page.RegisterStartupScript("", "<script>alert('系统管理员密码长度不能少于6位');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('系统管理员密码长度不能少于6位');</script>");
#endif
                return;
            }
            //验证数据库名为空
            if (initialcatalog.Text.Length == 0 && ddlDbType.SelectedValue != "Access")
            {
#if NET1
                Page.RegisterStartupScript("", "<script>alert('数据库名不能为空');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('数据库名不能为空');</script>");
#endif
                return;
            }

            //验证数据库表前缀不能为数字开头
            if (!Regex.IsMatch(tableprefix.Text, "^[a-zA-Z_](.*)", RegexOptions.IgnoreCase))
            {
#if NET1
                Page.RegisterStartupScript("", "<script>alert('数据库表前缀必须以字母开头');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('数据库表前缀必须以字母开头');</script>");
#endif
                return;
            }
            //验证必须选择数据库类型
            if (ddlDbType.SelectedIndex == 0)
            {
#if NET1
                Page.RegisterStartupScript("", "<script>alert('请选择数据库类型');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请选择数据库类型');</script>");
#endif
                return;
            }

            if (webtitle.Text.Trim() == string.Empty && forumtitle.Text.Trim() != string.Empty)
            {
                webtitle.Text = forumtitle.Text;
            }

            #endregion

            #region 写general.config文件
            try
            {
                DNTCache cache = DNTCache.GetCacheService();
                cache.RemoveObject("/Forum/PostTableName");
                cache.RemoveObject("/Forum/LastPostTableName");


                //记录加密串到general.config
                GeneralConfigInfo __configinfo = GeneralConfigs.Deserialize(Server.MapPath("../config/general.config"));
                __configinfo.Passwordkey = ForumUtils.CreateAuthStr(10);
                __configinfo.Forumtitle = forumtitle.Text;
                __configinfo.Forumurl = forumurl.Text;
                __configinfo.Webtitle = webtitle.Text;
                __configinfo.Weburl = weburl.Text;
                GeneralConfigs.Serialiaze(__configinfo, Server.MapPath("../config/general.config"));
                __configinfo = null;

            }
            catch { ;}
            #endregion

            string connectionString = string.Empty;
            //判断数据库类型，填充数据库字符串
            selectDbType = ddlDbType.SelectedValue;
            //access文件路径
            string path = Utils.GetMapPath(forumpath.Text + "database/" + txtDbFileName.Text);
            switch (selectDbType)
            {
                case "SqlServer":
                    connectionString =
                        string.Format(@"Data Source={0};User ID={1};Password={2};Initial Catalog={3};Pooling=true",
                                      datasource.Text, userid.Text, password.Text, initialcatalog.Text);
                    break;

                case "MySql":
                    connectionString =
                        string.Format(@"Data Source={0};Port=3306;User ID={1};Password={2};Initial Catalog={3};Pooling=true;Allow Zero Datetime=true", datasource.Text, userid.Text, password.Text, initialcatalog.Text);
                    break;

                case "Access":
                    connectionString =
                        string.Format(@"Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Persist Security Info=True;", path);
                    break;
            }

            BaseConfigInfo baseConfig = new BaseConfigInfo();
            baseConfig.Dbconnectstring = connectionString;
            baseConfig.Dbtype = selectDbType;
            baseConfig.Forumpath = forumpath.Text;
            baseConfig.Founderuid = sponsercheck.Checked ? 1 : 0;
            baseConfig.Tableprefix = tableprefix.Text;

            Session["SystemAdminName"] = systemadminname.Text;
            Session["SystemAdminPws"] = systemadminpws.Text;
            Session["dbname"] = initialcatalog.Text;
            Session["Dbconnectstring"] = connectionString;
            Session["Tableprefix"] = tableprefix.Text;
            Session["SystemAdminEmail"] = adminemail.Text.TrimEnd();
            //验证链接

            if (!EditDntConfig(baseConfig))
            {
                ClearDBInfo.Enabled = false;
#if NET1
                Page.RegisterStartupScript( "", "<script>if(confirm('无法把设置写入\"DNT.config\"文件, 系统将把文件内容显示出来, 您可以将内容保存为\"DNT.config\", 然后通过FTP软件上传到网站根目录下.. \\r\\n*注意: DNT.config位于网站根目录, 而非论坛根目录\\r\\n\\r\\n如果要继续运行安装, 请点击\"确定\"按钮. ')) {window.location.href='step4.aspx?isforceload=1';}</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>if(confirm('无法把设置写入\"DNT.config\"文件, 系统将把文件内容显示出来, 您可以将内容保存为\"DNT.config\", 然后通过FTP软件上传到网站根目录下.. \\r\\n*注意: DNT.config位于网站根目录, 而非论坛根目录\\r\\n\\r\\n如果要继续运行安装, 请点击\"确定\"按钮. ')) {window.location.href='step4.aspx?isforceload=1';}else{window.location.href='step3.aspx';}</script>");
#endif
                return;
            }

            string setupDbType = ddlDbType.SelectedValue;
            DbHelper.ResetDbProvider();

            if (!CheckConnection())
            {
#if NET1
                Page.RegisterStartupScript("", "<script>alert('连接数据库失败,请检查您填写的数据库信息。');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('连接数据库失败,请检查您填写的数据库信息。');</script>");
#endif
                return;
            }
            else
            {
                Response.Redirect(Utils.HtmlEncode(String.Format("step4.aspx?db={0}", setupDbType)));
            }
        }


        private bool CheckConnection()
        {
            try
            {
                DbHelper.ExecuteNonQuery("SELECT 1");
                return true;
            }
            catch
            {
                return false;
            }
        }



        private bool EditDntConfig(BaseConfigInfo bci)
        {
            string filename = Utils.GetMapPath("~/DNT.config");

            if (!Utils.FileExists(filename))
            {
                filename = Utils.GetMapPath("/DNT.config");
            }

            try
            {
                SerializationHelper.Save(bci, filename);
                BaseConfigProvider.SetInstance(bci);
                return true;
            }
            catch
            {
                ErrProcess(bci);
            }
            return false;
        }



        private void ErrProcess(BaseConfigInfo config)
        {
            string Mydntconfig = "<?xml version=\"1.0\"?>\r\n";

            Mydntconfig += "<BaseConfigInfo xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">\r\n";
            Mydntconfig += "<Dbconnectstring>" + config.Dbconnectstring + "</Dbconnectstring>\r\n";
            Mydntconfig += "<Tableprefix>" + config.Tableprefix + "</Tableprefix>\r\n";
            Mydntconfig += "<Forumpath>" + config.Forumpath + "</Forumpath>\r\n";
            Mydntconfig += "<Dbtype>" + config.Dbtype + "</Dbtype>\r\n";
            Mydntconfig += "<Founderuid>0</Founderuid>\r\n";
            Mydntconfig += "</BaseConfigInfo>\r\n";

            Session["My_DNT.congif"] = Mydntconfig;
            Session["Dbconnectstring"] = config.Dbconnectstring;
            Session["Tableprefix"] = config.Tableprefix;
        }
    }
}