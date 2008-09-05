using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Discuz.Config;
using Discuz.Data;
using System.Text;
using Discuz.Forum;
using Discuz.Common;
using System.Xml;

namespace Discuz.Install
{
    public class upgrade : System.Web.UI.Page
    {
        public BaseConfigInfo baseconfig = BaseConfigs.GetBaseConfig();
        protected RadioButtonList rblDbtype;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (baseconfig.Dbconnectstring.ToLower().IndexOf("allow") != -1)
                {
                    baseconfig.Dbtype = "MySql";
                    rblDbtype.SelectedValue = "MySql";

                }
                else
                {

                    if (baseconfig.Dbconnectstring.ToLower().IndexOf("oledb") != -1)
                    { //Access版本
                        baseconfig.Dbtype = "Access";
                        rblDbtype.SelectedValue = "Access";
                    }
                    else
                    { //SqlServer版本
                        baseconfig.Dbtype = "SqlServer";
                        rblDbtype.SelectedValue = "SqlServer";
                    }

                }

            }
            else
            {   
                baseconfig.Dbtype = rblDbtype.SelectedValue;
                BaseConfigs.SaveConfig(baseconfig);
                DbHelper.ResetDbProvider();

                if (CheckConnection())
                {
                    Upgrade();
                    Response.Redirect("finish.aspx");
                }
                else
                {
#if NET1
                    this.RegisterClientScriptBlock("Error", "<script type='text/javascript'>alert('数据库连接失败, 请检查dnt.config中的连接字符串是否正确');</script>");
#else
                    this.ClientScript.RegisterClientScriptBlock(this.GetType(), "Error", "<script type='text/javascript'>alert('数据库连接失败, 请检查dnt.config中的连接字符串是否正确');</script>");
#endif
                }
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

        public void Upgrade()
        {
            //当前版本为2.5.0时运行投票升级代码
            if (Utils.ASSEMBLY_VERSION == "2.5.0")
            {
                UpdatePolls();
            }

            #region 处理Email.config
            string emailfile = Utils.GetMapPath(BaseConfigs.GetForumPath + "config/email.config");
            XmlDocument doc = new XmlDocument();
            doc.Load(emailfile);
            if (doc.DocumentElement.Name != typeof(EmailConfigInfo).Name)
            {
                XmlDocument newdoc = new XmlDocument();
                XmlNode declarenode = newdoc.CreateXmlDeclaration("1.0", null, null);

                newdoc.AppendChild(declarenode);
                XmlNode rootnode = newdoc.CreateElement(typeof(EmailConfigInfo).Name);
                //newdoc.
                newdoc.AppendChild(rootnode);
                XmlNodeList xnl = doc.DocumentElement.ChildNodes;

                foreach (XmlNode node in xnl)
                {
                    XmlNode newnode = newdoc.CreateElement(node.Name);
                    if (node.Name == "PluginNameSpace")
                    {                        
                        newnode.InnerXml = "Discuz.Common.SysMailMessage";
                    }
                    else if (node.Name == "DllFileName")
                    {
                        newnode.InnerXml = "Discuz.Common";
                    }
                    else
                    {
                        newnode.InnerXml = node.InnerXml;
                    }
                    newdoc.DocumentElement.AppendChild(newnode);
                }

                newdoc.Save(emailfile);
            }

            #endregion
            
            //升级generalconfig
            GeneralConfigs.GetConfig().Forumurl = "forumindex.aspx";
            GeneralConfigs.Serialiaze(GeneralConfigs.GetConfig(), Server.MapPath("../config/general.config"));


            //运行升级脚本
            #region 表升级

            StringBuilder sb = new StringBuilder();
            using (System.IO.StreamReader objReader = new System.IO.StreamReader(Server.MapPath(string.Format("sqlscript/{0}/upgrade.txt", rblDbtype.SelectedValue.ToLower())), Encoding.UTF8))
            {
                sb.Append(objReader.ReadToEnd());
                objReader.Close();
            }

            foreach (string sqlstr in sb.Replace("dnt_", baseconfig.Tableprefix).ToString().Split(';'))
            {
                //try
                //{
                    DbHelper.ExecuteNonQuery(CommandType.Text, sqlstr);
                //}
                //catch
                //{
                //    ;
                //}
            }
            #endregion

            if (baseconfig.Dbtype == "SqlServer")
            {

                #region 存储过程升级
                sb.Remove(0, sb.Length);
                using (System.IO.StreamReader objReader = new System.IO.StreamReader(Server.MapPath(string.Format("sqlscript/{0}/upgrade1.txt", rblDbtype.SelectedValue.ToLower())), Encoding.UTF8))
                {
                    sb.Append(objReader.ReadToEnd());
                    objReader.Close();
                }

                foreach (string sql in Utils.SplitString(sb.ToString().Trim(), "GO\r\n"))
                {
                    try
                    {
                        if (sql != string.Empty)
                        {
                            DbHelper.ExecuteNonQuery(CommandType.Text, sql.Replace("\"", "'").Replace("dnt_", baseconfig.Tableprefix).ToString());
                        }
                    }
                    catch 
                    { 
                        ;
                    }
                }

                if (DbHelper.Provider.IsStoreProc())
                {
                    DatabaseProvider.GetInstance().UpdatePostSP();
                }
                #endregion
            }


            //创建分表存储过程
            if (DbHelper.Provider.IsStoreProc())
            {
                DatabaseProvider.GetInstance().CreateStoreProc(DatabaseProvider.GetInstance().GetMaxTableListId() + 1);
            }

        }

        private void UpdatePolls()
        {
            try
            {   //如果不报错，表示投票升级成功
                IDataReader readers = DbHelper.ExecuteReader(CommandType.Text, string.Format("select count(displayorder) from {0}polls", baseconfig.Tableprefix));
                readers.Close();
                return ;
            }
            catch
            {
                ;
            }

            StringBuilder sb = new StringBuilder();
            try
            {
                IDataReader readers = DbHelper.ExecuteReader(CommandType.Text, string.Format("select count(1) from {0}pollss", baseconfig.Tableprefix));
                if (readers != null)
                {

                    sb.Append("DROP TABLE " + baseconfig.Tableprefix + "pollss;");

                }
                readers.Close();

            }
            catch
            {
                ;
            }

            try
            {

                IDataReader readers = DbHelper.ExecuteReader(CommandType.Text, string.Format("select count(1) from {0}polloptions", baseconfig.Tableprefix));
                if (readers != null)
                {

                    sb.Append("DROP TABLE " + baseconfig.Tableprefix + "polloptions;");

                }
                readers.Close();
            }
            catch
            {

                ;
            }

            if(!Utils.StrIsNullOrEmpty(sb.ToString()))
            {
                DbHelper.ExecuteNonQuery(sb.ToString());
            }

            string database = baseconfig.Dbtype.ToLower();
            string createpoll = "", createoptions = "",pollidsql = "", renamesql = "";;




            switch (database)
            {
                case "sqlserver":
                    createpoll = string.Format(@"CREATE TABLE [{0}pollss] (
	[pollid] [int] IDENTITY (1, 1) NOT NULL ,
	[tid] [int] NOT NULL ,
	[displayorder] [int] NOT NULL ,
	[multiple] [tinyint] NOT NULL ,
	[visible] [tinyint] NOT NULL ,
	[maxchoices] [smallint] NOT NULL ,
	[expiration] [datetime] NOT NULL ,
	[uid] [int] NOT NULL ,
	[voternames] [ntext] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];ALTER TABLE [{0}pollss] ADD 
	CONSTRAINT [DF_{0}pollss_tid] DEFAULT (0) FOR [tid],
	CONSTRAINT [DF_{0}pollss_multiple] DEFAULT (0) FOR [multiple],
	CONSTRAINT [DF_{0}pollss_visible] DEFAULT (0) FOR [visible],
	CONSTRAINT [DF_{0}pollss_maxchoices] DEFAULT (0) FOR [maxchoices],
	CONSTRAINT [DF_{0}pollss_uid] DEFAULT (0) FOR [uid],
	CONSTRAINT [DF_{0}pollss_voternames] DEFAULT ('') FOR [voternames]", baseconfig.Tableprefix);
                    createoptions = string.Format(@"CREATE TABLE [{0}polloptions] (
	[polloptionid] [int] IDENTITY (1, 1) NOT NULL ,
	[tid] [int] NOT NULL ,
	[pollid] [int] NOT NULL ,
	[votes] [int] NOT NULL ,
	[displayorder] [int] NOT NULL ,
	[polloption] [nvarchar] (80) NOT NULL ,
	[voternames] [ntext] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];ALTER TABLE [{0}polloptions] ADD 
	CONSTRAINT [DF_{0}polloptions_tid] DEFAULT (0) FOR [tid],
	CONSTRAINT [DF_{0}polloptions_pollid] DEFAULT (0) FOR [pollid],
	CONSTRAINT [DF_{0}polloptions_votes] DEFAULT (0) FOR [votes],
	CONSTRAINT [DF_{0}polloptions_displayorder] DEFAULT (0) FOR [displayorder],
	CONSTRAINT [DF_{0}polloptions_polloption] DEFAULT ('') FOR [polloption],
	CONSTRAINT [DF_{0}polloptions_voternames] DEFAULT ('') FOR [voternames],
	CONSTRAINT [PK_{0}polloptions] PRIMARY KEY  CLUSTERED 
	(
		[polloptionid]
	)  ON [PRIMARY]", baseconfig.Tableprefix);
                    break;
                case "access":

                    break;

                case "mysql":
                    createpoll = string.Format(@"CREATE TABLE `{0}pollss` (
             `pollid` int(11) NOT NULL auto_increment,
             `tid` int(11) NOT NULL,
             `displayorder` int(11) NOT NULL,
             `multiple` tinyint(4) NOT NULL,
             `visible` tinyint(4) NOT NULL,
             `maxchoices` smallint(6) NOT NULL,
             `expiration` datetime NOT NULL,
             `uid` int(11) NOT NULL,
             `voternames` mediumtext NOT NULL,
             PRIMARY KEY  (`pollid`)
           ) ENGINE=InnoDB DEFAULT CHARSET=gbk;", baseconfig.Tableprefix);
                    createoptions = string.Format(@"CREATE TABLE `{0}polloptions` (
                   `polloptionid` int(11) NOT NULL auto_increment,
                   `tid` int(11) NOT NULL,
                   `pollid` int(11) NOT NULL,
                   `votes` int(11) NOT NULL,
                   `displayorder` int(11) NOT NULL,
                   `polloption` varchar(80) NOT NULL,
                   `voternames` mediumtext NOT NULL,
                   PRIMARY KEY  (`polloptionid`)
                 ) ENGINE=InnoDB DEFAULT CHARSET=gbk AUTO_INCREMENT=1;", baseconfig.Tableprefix);
                    pollidsql = "SELECT `pollid` FROM " + baseconfig.Tableprefix + "POLLSS ORDER BY POLLID DESC LIMIT 1";
                    renamesql = "RENAME TABLE `" + baseconfig.Tableprefix + "pollss` TO `" + baseconfig.Tableprefix + "polls`;";
                    break;

            }


            DbHelper.ExecuteNonQuery(createpoll);
            DbHelper.ExecuteNonQuery(createoptions);

            //try
            {
                IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, "SELECT tid,polltype,itemnamelist,itemvaluelist,usernamelist,enddatetime,userid FROM " + baseconfig.Tableprefix + "polls");


                while (reader.Read())
                {
                    string userlist = Utils.StrIsNullOrEmpty(reader["usernamelist"].ToString()) ? "" : reader["usernamelist"].ToString();// System.Text.RegularExpressions.Regex.Replace(reader["usernamelist"].ToString(), "\r\n", " ");
                    DbHelper.ExecuteNonQuery("insert into " + baseconfig.Tableprefix + "pollss(tid,multiple,expiration,uid,displayorder,voternames,visible) values(" + Utils.StrToInt(reader["tid"].ToString(), 0) + "," + Utils.StrToInt(reader["polltype"].ToString(), 0) + ",'" + reader["enddatetime"].ToString() + "'," + Utils.StrToInt(reader["userid"].ToString(), 0) + ",0,'" + userlist + "',1)");
                    int pollid = Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, pollidsql), -1); string[] namelist = System.Text.RegularExpressions.Regex.Split(reader["itemnamelist"].ToString(), "\r\n");
                    string[] valuelist = System.Text.RegularExpressions.Regex.Split(reader["itemvaluelist"].ToString(), "\r\n");
                    int tmpvalue = 0;
                    
                    for (int i = 0; i < namelist.Length; i++)
                    {
                        tmpvalue = (i > valuelist.GetUpperBound(0)) ? 0 : Convert.ToInt32(valuelist[i]);//有个能namelist.Length大于valuelist.Length 那么循环的时候 i 的值超过 valuelist索引上届。所以需要处理一下，如果超过那么认为value为0
                        DbHelper.ExecuteNonQuery("insert into " + baseconfig.Tableprefix + "polloptions(tid,polloption,votes,pollid) values(" + Utils.StrToInt(reader["tid"].ToString(), 0) + ",'" + Utils.GetSubString(namelist[i].ToString().Replace("'", "''"), 80, "") + "'," + Utils.StrToInt(tmpvalue, 0) + "," + pollid + ")");
                    }

                }
                reader.Close();
                DbHelper.ExecuteNonQuery("DROP TABLE " + baseconfig.Tableprefix + "POLLS");
                //DbHelper.ExecuteNonQuery("exec   sp_rename   '" + baseconfig.Tableprefix + "pollss','" + baseconfig.Tableprefix + "polls'");
                string[] splitchar = { ";" }; 
                foreach (string s in renamesql.Split(splitchar, StringSplitOptions.RemoveEmptyEntries))
                {
                    DbHelper.ExecuteNonQuery(s);
                }
            }
            //catch
            //{
            //    ;
            //}
        }

    }
}
