using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net;

namespace Foosun.Web.Install
{
    public partial class step_End : Foosun.Web.UI.BasePage
    {
        private string sitedir = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.CacheControl = "no-cache";
            string GError = Request.QueryString["error"];
            if (GError != "false")
            {
                PageError("请安装管理员，再进行此操作！", "step4.aspx", true);
            }
            string gset = Request.QueryString["start"];
            if (gset != string.Empty && gset != null)
            {
                Response.Write(InsertValue(Request.QueryString["set"]));
                Response.End();
            }
            sitedir = Server.MapPath("~");
            deleteInstallFile();
        }

        protected string InsertValue(string par)
        {
            string ResultStr = string.Empty;
            string s_dbsqlpath1 = string.Empty;
            string RollBar = "0/8";
            try
            {
                switch (par)
                {
                    case "site_param":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/site_param.sql");
                        RollBar = "1/9，参数设置导入成功。";
                        break;
                    case "group":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/group.sql");
                        RollBar = "2/9，会员组，管理员组设置导入成功。";
                        break;
                    case "label":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/label.sql");
                        RollBar = "3/9，内置标签导入成功。";
                        break;
                    case "menu":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/menu.sql");
                        RollBar = "4/9，功能菜单导入成功。";
                        break;
                    case "navi":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/navi.sql");
                        RollBar = "5/9，默认快捷方式导入成功。";
                        break;
                    case "stat":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/stat.sql");
                        RollBar = "6/9，统计参数导入成功。";
                        break;
                    case "friend":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/friend.sql");
                        RollBar = "7/9，友情连接参数导入成功。";
                        break;
                    case "collect":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/collect.sql");
                        RollBar = "8/9，采集参数导入成功。";
                        break;
                    case "help":
                        s_dbsqlpath1 = Server.MapPath("SQL/Value/help.sql");
                        RollBar = "9/9，帮助导入成功。";
                        break;
                    case "domainName":
                        setDomainName();
                        return "";
                }
                if (s_dbsqlpath1 == string.Empty)
                {
                    ResultStr = "发生错误！参数传递错误。";
                }
                else
                {
                    StreamReader sr = File.OpenText(s_dbsqlpath1);
                    string s_sqldefault = sr.ReadToEnd();
                    string s_result = Regex.Replace(s_sqldefault, @"\[[Ff][Ss]_", "[" + Foosun.Config.UIConfig.dataRe, RegexOptions.Compiled);
                    sr.Close();
                    string[] sqlarr = s_result.Split('\n');
                    for (int i = 0; i < sqlarr.Length; i++)
                    {
                        if (sqlarr[i] != "\r" && sqlarr[i] != "" && sqlarr[i] != null)
                        {
                            //Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CmsConString, "##", sqlarr[i]);
                            switch (par)
                            {
                                case "collect":
                                    Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CollectConString, sqlarr[i]);
                                    break;
                                case "help":
                                    Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.HelpConString, sqlarr[i]);
                                    break;
                                default:
                                    Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CmsConString, sqlarr[i]);
                                    break;
                            }

                        }
                    }
                    //Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CmsConString, "##", s_result);
                    if (par == "site_param")
                    {
                        string sql = "insert into [" + Foosun.Config.UIConfig.dataRe + "sys_newsIndex] ([TableName],[CreatTime]) values ('" + Foosun.Config.UIConfig.dataRe + "News','" + DateTime.Now + "');";
                        //Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CmsConString, "##", "insert into [" + Foosun.Config.UIConfig.dataRe + "sys_newsIndex] ([TableName],[CreatTime]) values ('" + Foosun.Config.UIConfig.dataRe + "News','" + DateTime.Now + "');");
                        switch (par)
                        {
                            case "collect":
                                Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CollectConString, sql);
                                break;
                            case "help":
                                Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.HelpConString, sql);
                                deleteAllInstallFile();
                                break;
                            default:
                                Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CmsConString, sql);
                                break;
                        }
                    }

                    ResultStr = RollBar;

                }
            }
            catch (Exception e)
            {
                ResultStr = "发生错误." + e.Message + "";
            }
            if (ResultStr == "9/9，帮助导入成功。")
            {
                deleteAllInstallFile();
            }
            return ResultStr;
        }

        /// <summary>
        /// 删除安装文件
        /// </summary>
        protected void deleteInstallFile()
        {
            string[] arr_file ={ "Index.aspx", "step1.aspx", "step2.aspx", "step3.aspx", "db.sql", "db1.sql", "SQL/CreatData.sql" };
            for (int i = 0; i < arr_file.Length; i++)
            {
                string s_filepath = sitedir + "Install\\" + arr_file[i].ToString();
                if (File.Exists(s_filepath))
                    File.Delete(s_filepath);
            }
        }

        /// <summary>
        /// 删除所有安装文件
        /// </summary>
        protected void deleteAllInstallFile()
        {
            string[] arr_file ={ "Index.aspx", "step1.aspx", "step2.aspx", "step3.aspx", "finishinstall.aspx", "step4.aspx", "step_End.aspx" };
            for (int i = 0; i < arr_file.Length; i++)
            {
                string s_filepath = sitedir + "Install\\" + arr_file[i].ToString();
                if (File.Exists(s_filepath))
                    File.Delete(s_filepath);
            }
            if (Directory.Exists(Server.MapPath("~/install/SQL")))
            {
                Directory.Delete(Server.MapPath("~/install/SQL"), true);
            }
            if (Directory.Exists(Server.MapPath("~/Install/image")))
            {
                Directory.Delete(Server.MapPath("~/Install/image"), true);
            }
            if (Directory.Exists(Server.MapPath("~/Install/css")))
            {
                Directory.Delete(Server.MapPath("~/Install/css"), true);
            }
        }

        /// <summary>
        /// 得到域名
        /// </summary>
        /// <returns></returns>
        private void setDomainName()
        {
            string param = "update fs_sys_param set SiteDomain='" + Request.Url.Authority + "'";
            Foosun.Install.Comm.ExecuteSql(Foosun.Config.DBConfig.CmsConString, param);
        }
    }
}
