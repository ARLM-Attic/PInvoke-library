using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;

using Discuz.Common;
using Discuz.Config;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Install;

namespace Discuz.Install
{
    /// <summary>
    /// setup3 的摘要说明. 
    /// </summary>
    public class install1 : SetupPage
    {
        protected System.Web.UI.WebControls.Button ClearDBInfo;
        protected System.Web.UI.WebControls.Button PrevPage;
        protected Discuz.Control.TextBox txtMsg;

        string connectstring = "";
        string tableprefix = "";
        string adminemail = "";
        string setupDbType = "";
        string dbScriptPath = "";//数据库脚本文件存放路径


        private void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["My_DNT.congif"] != null)
                {
                    txtMsg.Text = Session["My_DNT.congif"].ToString();
                }

                DisableSubmitBotton(this, this.ClearDBInfo);

                if ((Session["SystemAdminName"] == null) && (DNTRequest.GetString("isforceload") != "1"))
                {
                    Response.Redirect("step3.aspx");
                }
                else
                {
                    ViewState["SystemAdminName"] = Session["SystemAdminName"].ToString();
                    ViewState["SystemAdminPws"] = Session["SystemAdminPws"].ToString();
                    ViewState["dbname"] = Session["dbname"].ToString();

                    ViewState["Dbconnectstring"] = Session["Dbconnectstring"].ToString();
                    ViewState["Tableprefix"] = Session["Tableprefix"].ToString();
                }
                //设置数据库脚本路径
                if (Request["db"] != null)
                {
                    setupDbType = Request["db"].ToString().ToLower();
                    switch (setupDbType)
                    {
                        case "sqlserver":
                            dbScriptPath = @"sqlscript\sqlserver\";
                            break;
                        case "mysql":
                            dbScriptPath = @"sqlscript\mysql\";
                            break;
                    }
                    ViewState["dbtype"] = setupDbType;

                    //BaseConfigs.ResetConfig();
                }
                ViewState["dbscriptpath"] = dbScriptPath;
            }

        }



        //删除数据库中原有的表和存储过程
        private void DeleteTableAndSP()
        {
            //删除表和存储过程
            connectstring = ViewState["Dbconnectstring"].ToString();
            tableprefix = ViewState["Tableprefix"].ToString();

            StringBuilder sb = new StringBuilder();
            using (StreamReader objReader = new StreamReader(Server.MapPath(ViewState["dbscriptpath"].ToString() + "setup1.sql"), Encoding.UTF8))
            {
                sb.Append(objReader.ReadToEnd());
                objReader.Close();
            }

            if (tableprefix.ToLower() == "dnt_")
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());
            }
            else
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString().Replace("dnt_", tableprefix));
            }
        }
        //建表和存储过程
        private void CreateTableAndSP()
        {
            connectstring = ViewState["Dbconnectstring"].ToString();
            tableprefix = ViewState["Tableprefix"].ToString();

            #region 建表
            StringBuilder sb = new StringBuilder();
            using (StreamReader objReader = new StreamReader(Server.MapPath(ViewState["dbscriptpath"].ToString() + "setup2.1.sql"), Encoding.UTF8))
            {
                sb.Append(objReader.ReadToEnd());
                objReader.Close();
            }

            if (tableprefix.ToLower() == "dnt_")
            {
                DbHelper.ExecuteCommandWithSplitter(sb.ToString());
            }
            else
            {
                DbHelper.ExecuteCommandWithSplitter(sb.ToString().Replace("dnt_", tableprefix));
            }
            #endregion

            #region 建存储过程
            sb.Remove(0, sb.Length);
            using (StreamReader objReader = new StreamReader(Server.MapPath(ViewState["dbscriptpath"].ToString() + "setup2.2.sql"), Encoding.UTF8))
            {
                sb.Append(objReader.ReadToEnd());
                objReader.Close();
            }


            if (tableprefix.ToLower() == "dnt_")
            {
                DbHelper.ExecuteCommandWithSplitter(sb.ToString().Trim().Replace("\"", "'"));
            }
            else
            {
                DbHelper.ExecuteCommandWithSplitter(sb.ToString().Trim().Replace("\"", "'").Replace("dnt_", tableprefix));
            }

            #endregion


            try
            {
                #region 建全文索引
                sb.Remove(0, sb.Length);
                sb.Append("USE [" + ViewState["dbname"].ToString() + "] \r\n");
                sb.Append("execute sp_fulltext_database 'enable';");
                DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());

                sb.Remove(0, sb.Length);
                using (System.IO.StreamReader objReader = new System.IO.StreamReader(Server.MapPath(ViewState["dbscriptpath"].ToString() + "setup2.3.sql"), Encoding.UTF8))
                {
                    sb.Append(objReader.ReadToEnd());
                    objReader.Close();
                }

                if (tableprefix.ToLower() == "dnt_")
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());
                }
                else
                {
                    string sql = sb.ToString().Replace("databaseproperty('dnt'", "databaseproperty('" + ViewState["dbname"].ToString() + "'");
                    DbHelper.ExecuteNonQuery(CommandType.Text, sql.Replace("dnt_", tableprefix));
                }

              

                #endregion
            }
            catch (Exception ex)
            {
                string message = ex.Message.Replace("'", " ");
                message = message.Replace("\\", "/");
                message = message.Replace("\r\n", "\\r\\n");
                message = message.Replace("\r", "\\r");
                message = message.Replace("\n", "\\n");
#if NET1
                    Page.RegisterStartupScript("", "<script>alert('" + message + "');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + message + "');</script>");
#endif
            }

        }

        //初始化新创建的数据库
        private bool InitialDB()
        {
            connectstring = ViewState["Dbconnectstring"].ToString();
            tableprefix = ViewState["Tableprefix"].ToString();
            adminemail = Session["SystemAdminEmail"].ToString();
            try
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader objReader = new StreamReader(Server.MapPath(ViewState["dbscriptpath"].ToString() + "setup3.sql"), Encoding.UTF8))
                {
                    sb.Append(objReader.ReadToEnd());
                    objReader.Close();
                }
                DbHelper.ConnectionString = connectstring;
                if (tableprefix.ToLower() == "dnt_")
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());
                }
                else
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString().Replace("dnt_", tableprefix));
                }
            }
            catch
            {
                ;
            }

            try
            {
                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO [" + tableprefix + "users] ([username],[nickname],[password],[adminid],[groupid],[invisible],[email]) VALUES('" + ViewState["SystemAdminName"].ToString() + "','" + ViewState["SystemAdminName"].ToString() + "','" + Utils.MD5(ViewState["SystemAdminPws"].ToString()) + "','1','1','0','" + adminemail.ToString() + "')");
                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO [" + tableprefix + "userfields] ([uid]) VALUES('1')");
                return true;
            }
            catch
            {
                return false;
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
            this.ClearDBInfo.Click += new EventHandler(this.ClearDBInfo_Click);
            this.PrevPage.Click += new EventHandler(this.PrevPage_Click);
            this.Load += new EventHandler(this.Page_Load);

            if (Discuz.Common.DNTRequest.GetString("isforceload") == "1")
            {
                ClearDBInfo.Enabled = false;
            }
        }
        #endregion

        protected void PrevPage_Click(object sender, EventArgs e)
        {
            Response.Redirect("step3.aspx");
        }

        private void ClearDBInfo_Click(object sender, EventArgs e)
        {
            BaseConfigs.ResetConfig();
            try
            {
                switch (ViewState["dbtype"].ToString())
                {
                    case "sqlserver":
                        DeleteTableAndSP();
                        CreateTableAndSP();
                        break;
                    case "mysql":
                        CreateMysql();
                        return;
                    case "access":
                        InitAccess();
                        return;
                }

            }
            catch (DbException ex)
            {
                string message = ex.Message.Replace("'", " ");
                message = message.Replace("\\", "/");
                message = message.Replace("\r\n", "\\r\\n");
                message = message.Replace("\r", "\\r");
                message = message.Replace("\n", "\\n");
#if NET1
				Page.RegisterStartupScript("", "<script>alert('您的数据库登陆或权限存在问题,请确保信息正确后再运行安装: " + message + "');window.location.href='step3.aspx';</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('您的数据库登陆或权限存在问题,请确保信息正确后再运行安装: " + message + "');window.location.href='step3.aspx';</script>");
#endif
                return;
            }

            #region 数据库初始化 缓存处理
            UpdateCache();


            #endregion
        }

        private void UpdateCache()
        {
            try
            {
                if (InitialDB() == true)
                {
                    try
                    {
                        AdminCaches.ReSetAllCache();
                    }
                    finally
                    {
                        ForumUtils.ClearUserCookie();
                        Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);
                    }
#if NET1
                Page.RegisterStartupScript("PAGE", "<script>window.location.href='./succeed.aspx';</script>");
#else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PAGE", "<script>window.location.href='./succeed.aspx';</script>");
#endif
                }
                else
                {
#if NET1
				Page.RegisterStartupScript( "", "<script>alert('系统未能初始化数据库, 导致安装失败 ,请点击上一步检查相关设置是否正确');</script>");
#else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('系统未能初始化数据库, 导致安装失败 ,请点击上一步检查相关设置是否正确');</script>");
#endif
                    PrevPage.Enabled = false;
                }
            }
            catch (Exception err)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('错误源：" + err.Message + "');</script>");
            }
        }

        private void CreateMysql()
        {
            connectstring = ViewState["Dbconnectstring"].ToString();
            tableprefix = ViewState["Tableprefix"].ToString();
            try
            {

                #region 建表
                StringBuilder sb = new StringBuilder();
                using (StreamReader objReader = new StreamReader(Server.MapPath(ViewState["dbscriptpath"].ToString() + "mysql.sql"), Encoding.UTF8))
                {
                    sb.Append(objReader.ReadToEnd());
                    objReader.Close();
                }

                if (tableprefix.ToLower() == "dnt_")
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());
                }
                else
                {
                    DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString().Replace("dnt_", tableprefix));
                }
                #endregion

                #region 初始化管理员
                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO `" + tableprefix + "users` (`username`,`nickname`,`password`,`adminid`,`groupid`,`invisible`,`email`,`joindate`,`lastvisit`,`lastactivity`,`lastpost`) VALUES('" + ViewState["SystemAdminName"].ToString() + "','" + ViewState["SystemAdminName"].ToString() + "','" + Utils.MD5(ViewState["SystemAdminPws"].ToString()) + "','1','1','0','" + adminemail.ToString() + "',now(),now(),now(),now())");
                DbHelper.ExecuteNonQuery(CommandType.Text, "INSERT INTO `" + tableprefix + "userfields` (`uid`,`medals`,`bio`,`signature`,`sightml`,`authtime`) VALUES('1','','','','',now())");
                #endregion

                try
                {
                    AdminCaches.ReSetAllCache();
                }
                finally
                {
                    ForumUtils.ClearUserCookie();
                    Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);
                }
#if NET1
                Page.RegisterStartupScript("PAGE", "<script>window.location.href='./succeed.aspx';</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PAGE", "<script>window.location.href='./succeed.aspx';</script>");
#endif
            }
            catch
            {
#if NET1
				Page.RegisterStartupScript( "", "<script>alert('系统未能初始化数据库, 导致安装失败 ,请点击上一步检查相关设置是否正确');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('系统未能初始化数据库, 导致安装失败 ,请点击上一步检查相关设置是否正确');</script>");
#endif
                PrevPage.Enabled = false;
            }


        }

        private void InitAccess()
        {
            connectstring = ViewState["Dbconnectstring"].ToString();
            tableprefix = ViewState["Tableprefix"].ToString();
            try
            {
                #region 初始化管理员密码

                string sql = string.Format("update {0}users set [username] = '{1}',[password] = '{2}' WHERE [username]='admin'", tableprefix, ViewState["SystemAdminName"].ToString(),Utils.MD5(ViewState["SystemAdminPws"].ToString()));
                DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                #endregion

                #region 初始化tablelist update createdatetime 字段

                sql = string.Format("update {0}tablelist set [createdatetime] = now() where [id]=1", tableprefix);
                DbHelper.ExecuteNonQuery(CommandType.Text, sql);

                #endregion

                try
                {
                    AdminCaches.ReSetAllCache();
                }
                finally
                {
                    ForumUtils.ClearUserCookie();
                    Utils.WriteCookie(Utils.GetTemplateCookieName(), "", -999999);
                }
#if NET1
                Page.RegisterStartupScript("PAGE", "<script>window.location.href='./succeed.aspx';</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PAGE", "<script>window.location.href='./succeed.aspx';</script>");
#endif
            }
            catch
            {
#if NET1
				Page.RegisterStartupScript( "", "<script>alert('系统未能初始化数据库, 导致安装失败 ,请点击上一步检查相关设置是否正确');</script>");
#else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('系统未能初始化数据库, 导致安装失败 ,请点击上一步检查相关设置是否正确');</script>");
#endif
                PrevPage.Enabled = false;
            }
        }

    }
}