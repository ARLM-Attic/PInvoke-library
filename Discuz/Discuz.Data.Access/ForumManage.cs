#if NET1
#else
using System;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Discuz.Data;
using Discuz.Config;
using Discuz.Common;
using Discuz.Entity;
using System.Data.OleDb;

namespace Discuz.Data.Access
{
    public partial class DataProvider : IDataProvider
    {
        /// <summary>
        /// SQL SERVER SQL语句转义
        /// </summary>
        /// <param name="str">需要转义的关键字符串</param>
        /// <param name="pattern">需要转义的字符数组</param>
        /// <returns>转义后的字符串</returns>
        private static string RegEsc(string str)
        {
            string[] pattern ={ @"%", @"_", @"'" };
            foreach (string s in pattern)
            {
                //Regex rgx = new Regex(s);
                //keyword = rgx.Replace(keyword, "\\" + s);
                switch (s)
                {
                    case "%":
                        str = str.Replace(s, "[%]");
                        break;
                    case "_":
                        str = str.Replace(s, "[_]");
                        break;
                    case "'":
                        str = str.Replace(s, "['']");
                        break;

                }

            }
            return str;
        }
        /// <summary>
        /// 添加友情链接
        /// </summary>
        /// <param name="displayorder">显示顺序</param>
        /// <param name="name">名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="note">备注</param>
        /// <param name="logo">Logo地址</param>
        /// <returns></returns>
        public int AddForumLink(int displayorder, string name, string url, string note, string logo)
        {
            
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, displayorder),
                                        DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar, 100, name),
                                        DbHelper.MakeInParam("@url", (DbType)OleDbType.VarWChar, 100, url),
                                        DbHelper.MakeInParam("@note", (DbType)OleDbType.VarWChar, 200, note),
                                        DbHelper.MakeInParam("@logo", (DbType)OleDbType.VarWChar, 100, logo)
                                    };
            string sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "forumlinks] ([displayorder], [name],[url],[note],[logo]) VALUES ("+@displayorder+",'"+@name+"','"+@url+"','"+@note+"','"+@logo+"')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        /// <summary>
        /// 获得所有友情链接
        /// </summary>
        /// <returns></returns>
        public string GetForumLinks()
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "forumlinks]";
        }

        /// <summary>
        /// 删除指定友情链接
        /// </summary>
        /// <param name="forumlinkid"></param>
        /// <returns></returns>
        public int DeleteForumLink(string forumlinkidlist)
        {
            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "forumlinks] WHERE [id] IN (" + forumlinkidlist + ")";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 更新指定友情链接
        /// </summary>
        /// <param name="id">友情链接Id</param>
        /// <param name="displayorder">显示顺序</param>
        /// <param name="name">名称</param>
        /// <param name="url">链接地址</param>
        /// <param name="note">备注</param>
        /// <param name="logo">Logo地址</param>
        /// <returns></returns>
        public int UpdateForumLink(int id, int displayorder, string name, string url, string note, string logo)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id", (DbType)OleDbType.Integer, 4, id),
                                        DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, displayorder),
                                        DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar, 100, name),
                                        DbHelper.MakeInParam("@url", (DbType)OleDbType.VarWChar, 100, url),
                                        DbHelper.MakeInParam("@note", (DbType)OleDbType.VarWChar, 200, note),
                                        DbHelper.MakeInParam("@logo", (DbType)OleDbType.VarWChar, 100, logo)
                                    };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forumlinks] SET [displayorder]="+@displayorder+",[name]='"+@name+"',[url]='"+@url+"',[note]='"+@note+"',[logo]='"+@logo+"' WHERE [id]="+@id;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }


        /// <summary>
        /// 获得首页版块列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetForumIndexListTable()
        {
            //string commandText = string.Format("SELECT CASE WHEN Datediff(\"n\", [lastpost], now())<600 THEN 'new' ELSE 'old' END AS [havenew],[{0}forums].*, [{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] WHERE [{0}forums].[parentid] NOT IN (SELECT fid FROM [{0}forums] WHERE [status] < 1 AND [layer] = 0) AND [{0}forums].[status] > 0 AND [layer] <= 1 ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
            string commandText = string.Format("SELECT iif( DATEDIFF(\"n\",[lastpost],now())<600,'new','old' ) AS [havenew],[{0}forums].*,[{0}forums].[fid] as [fid],[{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] WHERE [{0}forums].[parentid] NOT IN (SELECT fid FROM [{0}forums] WHERE [status] < 1 AND [layer] = 0) AND [{0}forums].[status] > 0 AND [layer] <= 1 ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得首页版块列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetForumIndexList()
        {
            //string commandText = string.Format("SELECT CASE WHEN DATEDIFF(n, [lastpost], now())<600 THEN 'new' ELSE 'old' END AS [havenew],[{0}forums].*, [{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] WHERE [{0}forums].[parentid] NOT IN (SELECT fid FROM [{0}forums] WHERE [status] < 1 AND [layer] = 0) AND [{0}forums].[status] > 0 AND [layer] <= 1 ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
            //return DbHelper.ExecuteReader(CommandType.Text, commandText);

            string commandText = string.Format("SELECT iif( DATEDIFF('n',[lastpost],now())<600,'new','old' ) AS [havenew],[{0}forums].*,[{0}forums].[fid] as [fid],[{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] WHERE [{0}forums].[parentid] NOT IN (SELECT fid FROM [{0}forums] WHERE [status] < 1 AND [layer] = 0) AND [{0}forums].[status] > 0 AND [layer] <= 1 ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得简介版论坛首页列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetArchiverForumIndexList()
        {
            string commandText = string.Format("SELECT [{0}forums].[fid], [{0}forums].[name],[{0}forums].[parentidlist], [{0}forums].[status],[{0}forums].[layer], [{0}forumfields].[viewperm] FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid]   ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];
        }

        /// <summary>
        /// 获得子版块列表
        /// </summary>
        /// <param name="fid">版块id</param>
        /// <returns></returns>
        public IDataReader GetSubForumReader(int fid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer,4, fid)
								   };

            String commandText = "SELECT iif( Datediff(\"n\",[lastpost],now())<600,'new','old' ) AS [havenew],[" + BaseConfigs.GetTablePrefix + "forums].*,[" + BaseConfigs.GetTablePrefix + "forums].[fid] As [fid],[" + BaseConfigs.GetTablePrefix + "forumfields].* FROM [" + BaseConfigs.GetTablePrefix + "forums] LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forumfields] ON [" + BaseConfigs.GetTablePrefix + "forums].[fid]=[" + BaseConfigs.GetTablePrefix + "forumfields].[fid] WHERE [parentid] = "+@fid+" AND [status] > 0 ORDER BY [displayorder]";
            
            return DbHelper.ExecuteReader(CommandType.Text, commandText, prams);
        }

        /// <summary>
        /// 获得子版块列表
        /// </summary>
        /// <param name="fid">版块id</param>
        /// <returns></returns>
        public DataTable GetSubForumTable(int fid)
        {
            DbParameter[] prams = {
									   DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer,4, fid)
								   };

            string commandText = string.Format("SELECT CASE WHEN Datediff(n, [lastpost], now())<600 THEN 'new' ELSE 'old' END AS [havenew],[{0}forums].*, [{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] WHERE [parentid] = "+@fid+" AND [status] > 0 ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, commandText, prams).Tables[0];
        }

        /// <summary>
        /// 获得全部版块列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetForumsTable()
        {
            string commandText = string.Format("SELECT [{0}forums].*, [{0}forumfields].* FROM [{0}forums] LEFT JOIN [{0}forumfields] ON [{0}forums].[fid]=[{0}forumfields].[fid] ORDER BY [displayorder]", BaseConfigs.GetTablePrefix);
           
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, commandText).Tables[0];

            dt.Columns[BaseConfigs.GetTablePrefix + "forums.fid"].ColumnName = "fid";

            return dt;
        }

        /// <summary>
        /// 设置当前版块主题数(不含子版块)
        /// </summary>
        /// <param name="fid">版块id</param>
        /// <returns>主题数</returns>
        public int SetRealCurrentTopics(int fid)
        {
            int count = int.Parse(DbHelper.ExecuteScalar(CommandType.Text, string.Format("SELECT COUNT(tid) FROM {0}topics WHERE [displayorder] >= 0 AND [fid]={1}", BaseConfigs.GetTablePrefix, fid)).ToString());
            string commandText = string.Format("UPDATE {0}forums SET [curtopics] =" + count + "  WHERE [fid]={1}", BaseConfigs.GetTablePrefix, fid);
            return DbHelper.ExecuteNonQuery(CommandType.Text, commandText);
     
        }

        public DataTable GetForumListTable()
        {
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, string.Format("SELECT [name], [fid] FROM [{0}forums] WHERE [{0}forums].[parentid] NOT IN (SELECT fid FROM [{0}forums] WHERE [status] < 1 AND [layer] = 0) AND [status] > 0 AND [displayorder] >=0 ORDER BY [displayorder]", BaseConfigs.GetTablePrefix)).Tables[0];

            return dt;
        }

        public string GetTemplates()
        {
            return "SELECT [templateid],[name] FROM [" + BaseConfigs.GetTablePrefix + "templates] ";
        }

        public DataTable GetUserGroupsTitle()
        {
            string sql = "SELECT [groupid],[grouptitle] FROM [" + BaseConfigs.GetTablePrefix + "usergroups]  ORDER BY [groupid] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetUserGroupMaxspacephotosize()
        {
            string sql = "SELECT [groupid],[grouptitle],[maxspacephotosize] FROM [" + BaseConfigs.GetTablePrefix + "usergroups]  ORDER BY [groupid] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetUserGroupMaxspaceattachsize()
        {
            string sql = "SELECT [groupid],[grouptitle],[maxspaceattachsize] FROM [" + BaseConfigs.GetTablePrefix + "usergroups]  ORDER BY [groupid] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetAttachTypes()
        {
            string sql = "SELECT [id],[extension] FROM [" + BaseConfigs.GetTablePrefix + "attachtypes]  ORDER BY [id] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetForums()
        {
            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "forums] ORDER BY [displayorder] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public string GetForumsTree()
        {
            return "SELECT [fid],[name],[parentid] FROM [" + BaseConfigs.GetTablePrefix + "forums] ORDER BY [displayorder] ASC";
        }

        public int GetForumsMaxDisplayOrder()
        {
            return Utils.StrToInt(DbHelper.ExecuteDataset(CommandType.Text, "SELECT MAX(displayorder) FROM [" + BaseConfigs.GetTablePrefix + "forums]").Tables[0].Rows[0][0], 0) + 1;
        }

        public DataTable GetForumsMaxDisplayOrder(int parentid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT MAX([displayorder]) FROM [" + BaseConfigs.GetTablePrefix + "forums]  WHERE [parentid]=" + parentid).Tables[0];
        }
        public void UpdateForumsDisplayOrder(int minDisplayOrder)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]+1  WHERE [displayorder]>" + minDisplayOrder.ToString());
        }

        public void UpdateSubForumCount(int fid)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [subforumcount]=[subforumcount]+1  WHERE [fid]=" + fid.ToString());
        }

        public DataRow GetForum(int fid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + fid.ToString()).Tables[0].Rows[0];
        }

        public DataRowCollection GetModerators(int fid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [username] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [uid] IN(SELECT [uid] FROM [" + BaseConfigs.GetTablePrefix + "moderators] WHERE [inherited]=1 AND [fid]=" + fid + ")").Tables[0].Rows;
        }

        public DataTable GetTopForum(int fid)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 [fid] FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [parentid]=0 AND [layer]=0 AND [fid]=" + fid).Tables[0];
        }

        public int UpdateForum(int fid, string name, int subforumcount, int displayorder)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid),
                                        DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar, 50, name),
                                        DbHelper.MakeInParam("@subforumcount", (DbType)OleDbType.Integer, 4, subforumcount),
                                        DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, displayorder) 
                                    };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [name]='" + @name + "',[subforumcount]=" + @subforumcount + " ,[displayorder]=" + @displayorder + " WHERE [fid]=" + @fid;
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public DataTable GetForumField(int fid, string fieldname)
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 [" + fieldname + "] FROM [" + BaseConfigs.GetTablePrefix + "forumfields] WHERE [fid]=" + fid).Tables[0];
        }

        public int UpdateForumField(int fid, string fieldname)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET [" + fieldname + "]='' WHERE [fid]=" + fid);
        }

        public int UpdateForumField(int fid, string fieldname, string fieldvalue)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET [" + fieldname + "]='" + fieldvalue + "' WHERE [fid]=" + fid);
        }

        public DataRowCollection GetDatechTableIds()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT id FROM [" + BaseConfigs.GetTablePrefix + "tablelist]").Tables[0].Rows;
        }

        public int UpdateMinMaxField(string posttablename, int posttableid)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "tablelist] SET [mintid]=" + GetMinPostTableTid(posttablename) + ",[maxtid]=" + GetMaxPostTableTid(posttablename) + "  WHERE [id]=" + posttableid);
        }

        public DataRowCollection GetForumIdList()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [fid] FROM [" + BaseConfigs.GetTablePrefix + "forums]").Tables[0].Rows;
        }

        public int CreateFullTextIndex(string dbname)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("USE " + dbname + ";");
            //sb.Append("execute sp_fulltext_database 'enable';");
            //return DbHelper.ExecuteNonQuery(CommandType.Text, sb.ToString());
            return 0;
        }

        public int GetMaxForumId()
        {
            return Utils.StrToInt(DbHelper.ExecuteScalar(CommandType.Text, "SELECT iif(ISNULL(MAX(fid)),0,MAX(fid)) FROM " + BaseConfigs.GetTablePrefix + "forums"), 0);
        }

        public DataTable GetForumList()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [fid],[name] FROM [" + BaseConfigs.GetTablePrefix + "forums]").Tables[0];
        }

        public DataTable GetAllForumList()
        {
            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "forums] ORDER BY [displayorder] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public DataTable GetForumInformation(int fid)
        {
            DbParameter[] prams = 
			{
				DbHelper.MakeInParam("@fid",(DbType)OleDbType.Integer, 4,fid)
			};
  
            string sql = "SELECT [" + BaseConfigs.GetTablePrefix + "forums].*, [" + BaseConfigs.GetTablePrefix + "forumfields].* FROM [" + BaseConfigs.GetTablePrefix + "forums] LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forumfields] ON [" + BaseConfigs.GetTablePrefix + "forums].[fid]=[" + BaseConfigs.GetTablePrefix + "forumfields].[fid] WHERE [" + BaseConfigs.GetTablePrefix + "forums].[fid]=@fid";
            DataTable dt;
            dt = DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
            dt.Columns[0].ColumnName = "fid";

            return dt;
        }

        public void SaveForumsInfo(ForumInfo __foruminfo)
        {
            OleDbConnection conn = new OleDbConnection(DbHelper.ConnectionString);
            conn.Open();
            using (OleDbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    DbParameter[] prams = {
					DbHelper.MakeInParam("@name", (DbType)OleDbType.VarChar, 50, __foruminfo.Name),
						DbHelper.MakeInParam("@status", (DbType)OleDbType.Integer, 4, __foruminfo.Status),
						DbHelper.MakeInParam("@colcount", (DbType)OleDbType.SmallInt, 4, __foruminfo.Colcount),
						DbHelper.MakeInParam("@templateid", (DbType)OleDbType.SmallInt, 2, __foruminfo.Templateid),
						DbHelper.MakeInParam("@allowsmilies", (DbType)OleDbType.Integer, 4, __foruminfo.Allowsmilies),
						DbHelper.MakeInParam("@allowrss", (DbType)OleDbType.Integer, 6, __foruminfo.Allowrss),
						DbHelper.MakeInParam("@allowhtml", (DbType)OleDbType.Integer, 4, __foruminfo.Allowhtml),
						DbHelper.MakeInParam("@allowbbcode", (DbType)OleDbType.Integer, 4, __foruminfo.Allowbbcode),
						DbHelper.MakeInParam("@allowimgcode", (DbType)OleDbType.Integer, 4, __foruminfo.Allowimgcode),
						DbHelper.MakeInParam("@allowblog", (DbType)OleDbType.Integer, 4, __foruminfo.Allowblog),
						DbHelper.MakeInParam("@alloweditrules", (DbType)OleDbType.Integer, 4, __foruminfo.Alloweditrules),
						DbHelper.MakeInParam("@allowthumbnail", (DbType)OleDbType.Integer, 4, __foruminfo.Allowthumbnail),
                        DbHelper.MakeInParam("@allowtag",(DbType)OleDbType.Integer,4,__foruminfo.Allowtag),
                        DbHelper.MakeInParam("@istrade", (DbType)OleDbType.Integer, 4, __foruminfo.Istrade),
						DbHelper.MakeInParam("@recyclebin", (DbType)OleDbType.Integer, 4, __foruminfo.Recyclebin),
						DbHelper.MakeInParam("@modnewposts", (DbType)OleDbType.Integer, 4, __foruminfo.Modnewposts),
						DbHelper.MakeInParam("@jammer", (DbType)OleDbType.Integer, 4, __foruminfo.Jammer),
						DbHelper.MakeInParam("@disablewatermark", (DbType)OleDbType.Integer, 4, __foruminfo.Disablewatermark),
						DbHelper.MakeInParam("@inheritedmod", (DbType)OleDbType.Integer, 4, __foruminfo.Inheritedmod),
						DbHelper.MakeInParam("@autoclose", (DbType)OleDbType.SmallInt, 2, __foruminfo.Autoclose),
						DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, __foruminfo.Displayorder),
                        DbHelper.MakeInParam("@allowpostspecial",(DbType)OleDbType.Integer,4,__foruminfo.Allowpostspecial),
                        DbHelper.MakeInParam("@allowspecialonly",(DbType)OleDbType.Integer,4,__foruminfo.Allowspecialonly),
						DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, __foruminfo.Fid)
					};
                    //string sql = "update [" + BaseConfigs.GetTablePrefix + "forums] set [name]=@name,  [status]=@status, [colcount]=@colcount, [templateid]=@templateid,[allowsmilies]=@allowsmilies ,[allowrss]=@allowrss, [allowhtml]=@allowhtml, [allowbbcode]=@allowbbcode, [allowimgcode]=@allowimgcode, [allowblog]=@allowblog,[alloweditrules]=@alloweditrules ,[allowthumbnail]=@allowthumbnail ,[recyclebin]=@recyclebin, [modnewposts]=@modnewposts,[jammer]=@jammer,[disablewatermark]=@disablewatermark,[inheritedmod]=@inheritedmod,[autoclose]=@autoclose,[displayorder]=@displayorder  Where [fid]=@fid";
                    string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [name]=@name, [status]=@status, [colcount]=@colcount, [templateid]=@templateid,"
                       + "[allowsmilies]=@allowsmilies ,[allowrss]=@allowrss, [allowhtml]=@allowhtml, [allowbbcode]=@allowbbcode, [allowimgcode]=@allowimgcode, "
                       + "[allowblog]=@allowblog,[alloweditrules]=@alloweditrules ,[allowthumbnail]=@allowthumbnail ,[allowtag]=@allowtag,[istrade]=@istrade,"
                       + "[recyclebin]=@recyclebin, [modnewposts]=@modnewposts,[jammer]=@jammer,[disablewatermark]=@disablewatermark,[inheritedmod]=@inheritedmod,"
                       + "[autoclose]=@autoclose,[displayorder]=@displayorder,[allowpostspecial]=@allowpostspecial,[allowspecialonly]=@allowspecialonly WHERE [fid]=@fid";
                    DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);

                    DbParameter[] prams1 = {
							DbHelper.MakeInParam("@description", (DbType)OleDbType.VarWChar, 0, __foruminfo.Description),
						DbHelper.MakeInParam("@password", (DbType)OleDbType.VarChar, 16, __foruminfo.Password),
						DbHelper.MakeInParam("@icon", (DbType)OleDbType.VarChar, 255, __foruminfo.Icon),
						DbHelper.MakeInParam("@redirect", (DbType)OleDbType.VarChar, 255, __foruminfo.Redirect),
						DbHelper.MakeInParam("@attachextensions", (DbType)OleDbType.VarChar, 255, __foruminfo.Attachextensions),
						DbHelper.MakeInParam("@rules", (DbType)OleDbType.VarWChar, 0, __foruminfo.Rules),
						DbHelper.MakeInParam("@topictypes", (DbType)OleDbType.VarChar, 0, __foruminfo.Topictypes),
						DbHelper.MakeInParam("@viewperm", (DbType)OleDbType.VarChar, 0, __foruminfo.Viewperm),
						DbHelper.MakeInParam("@postperm", (DbType)OleDbType.VarChar, 0, __foruminfo.Postperm),
						DbHelper.MakeInParam("@replyperm", (DbType)OleDbType.VarChar, 0, __foruminfo.Replyperm),
						DbHelper.MakeInParam("@getattachperm", (DbType)OleDbType.VarChar, 0, __foruminfo.Getattachperm),
						DbHelper.MakeInParam("@postattachperm", (DbType)OleDbType.VarChar, 0, __foruminfo.Postattachperm),
                        DbHelper.MakeInParam("@applytopictype", (DbType)OleDbType.TinyInt, 1, __foruminfo.Applytopictype),
						DbHelper.MakeInParam("@postbytopictype", (DbType)OleDbType.TinyInt, 1, __foruminfo.Postbytopictype),
						DbHelper.MakeInParam("@viewbytopictype", (DbType)OleDbType.TinyInt, 1, __foruminfo.Viewbytopictype),
						DbHelper.MakeInParam("@topictypeprefix", (DbType)OleDbType.TinyInt, 1, __foruminfo.Topictypeprefix),
						DbHelper.MakeInParam("@permuserlist", (DbType)OleDbType.VarWChar, 0, __foruminfo.Permuserlist),
						DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, __foruminfo.Fid)
					};
                 
                    sql = "Update [" + BaseConfigs.GetTablePrefix + "forumfields] Set [description]=@description,[password]=@password,[icon]=@icon,[redirect]=@redirect,"
                      + "[attachextensions]=@attachextensions,[rules]=@rules,[topictypes]=@topictypes,[viewperm]=@viewperm,[postperm]=@postperm,[replyperm]=@replyperm,"
                      + "[getattachperm]=@getattachperm,[postattachperm]=@postattachperm,[applytopictype]=@applytopictype,[postbytopictype]=@postbytopictype,"
                      + "[viewbytopictype]=@viewbytopictype,[topictypeprefix]=@topictypeprefix,[permuserlist]=@permuserlist Where [fid]=@fid";
					
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, sql, prams1);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            conn.Close();
        }

        public int InsertForumsInf(ForumInfo foruminfo)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("@parentid", (DbType)OleDbType.Integer, 2, foruminfo.Parentid),
				DbHelper.MakeInParam("@layer", (DbType)OleDbType.Integer, 4, foruminfo.Layer),
				DbHelper.MakeInParam("@pathlist", (DbType)OleDbType.VarWChar, 3000, foruminfo.Pathlist == null ? " " : foruminfo.Pathlist),
				DbHelper.MakeInParam("@parentidlist", (DbType)OleDbType.VarWChar, 300, foruminfo.Parentidlist== null ? " " : foruminfo.Parentidlist),
				DbHelper.MakeInParam("@subforumcount", (DbType)OleDbType.Integer, 4, foruminfo.Subforumcount),
				DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar, 50, foruminfo.Name),
				DbHelper.MakeInParam("@status", (DbType)OleDbType.Integer, 4, foruminfo.Status),
				DbHelper.MakeInParam("@colcount", (DbType)OleDbType.Integer, 4, foruminfo.Colcount),
				DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, foruminfo.Displayorder),
				DbHelper.MakeInParam("@templateid", (DbType)OleDbType.Integer, 2, foruminfo.Templateid),
				DbHelper.MakeInParam("@allowsmilies", (DbType)OleDbType.Integer, 4, foruminfo.Allowsmilies),
				DbHelper.MakeInParam("@allowrss", (DbType)OleDbType.Integer, 6, foruminfo.Allowrss),
				DbHelper.MakeInParam("@allowhtml", (DbType)OleDbType.Integer, 4, foruminfo.Allowhtml),
				DbHelper.MakeInParam("@allowbbcode", (DbType)OleDbType.Integer, 4, foruminfo.Allowbbcode),
				DbHelper.MakeInParam("@allowimgcode", (DbType)OleDbType.Integer, 4, foruminfo.Allowimgcode),
				DbHelper.MakeInParam("@allowblog", (DbType)OleDbType.Integer, 4, foruminfo.Allowblog),
				DbHelper.MakeInParam("@istrade", (DbType)OleDbType.Integer, 4, foruminfo.Istrade),
				DbHelper.MakeInParam("@alloweditrules", (DbType)OleDbType.Integer, 4, foruminfo.Alloweditrules),
				DbHelper.MakeInParam("@allowthumbnail", (DbType)OleDbType.Integer, 4, foruminfo.Allowthumbnail),
                DbHelper.MakeInParam("@allowtag",(DbType)OleDbType.Integer,4,foruminfo.Allowtag),
				DbHelper.MakeInParam("@recyclebin", (DbType)OleDbType.Integer, 4, foruminfo.Recyclebin),
				DbHelper.MakeInParam("@modnewposts", (DbType)OleDbType.Integer, 4, foruminfo.Modnewposts),
				DbHelper.MakeInParam("@jammer", (DbType)OleDbType.Integer, 4, foruminfo.Jammer),
				DbHelper.MakeInParam("@disablewatermark", (DbType)OleDbType.Integer, 4, foruminfo.Disablewatermark),
				DbHelper.MakeInParam("@inheritedmod", (DbType)OleDbType.Integer, 4, foruminfo.Inheritedmod),
				DbHelper.MakeInParam("@autoclose", (DbType)OleDbType.Integer, 2, foruminfo.Autoclose),                
                DbHelper.MakeInParam("@allowpostspecial",(DbType)OleDbType.Integer,4,foruminfo.Allowpostspecial),
                DbHelper.MakeInParam("@allowspecialonly",(DbType)OleDbType.Integer,4,foruminfo.Allowspecialonly),
			};
            string sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "forums] ([parentid],[layer],[pathlist],[parentidlist],[subforumcount],[name],"
                + "[status],[colcount],[displayorder],[templateid],[allowsmilies],[allowrss],[allowhtml],[allowbbcode],[allowimgcode],[allowblog],"
                + "[istrade],[alloweditrules],[recyclebin],[modnewposts],[jammer],[disablewatermark],[inheritedmod],[autoclose],[allowthumbnail],"
                + "[allowtag],[allowpostspecial],[allowspecialonly]) VALUES (@parentid,@layer,@pathlist,@parentidlist,@subforumcount,@name,@status, @colcount, @displayorder,"
                + "@templateid,@allowsmilies,@allowrss,@allowhtml,@allowbbcode,@allowimgcode,@allowblog,@istrade,@alloweditrules,@recyclebin,"
                + "@modnewposts,@jammer,@disablewatermark,@inheritedmod,@autoclose,@allowthumbnail,@allowtag,@allowpostspecial,@allowspecialonly)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);

            int fid = GetMaxForumId();

            DbParameter[] prams1 = {
				DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid),
				DbHelper.MakeInParam("@description", (DbType)OleDbType.VarWChar, 0, foruminfo.Description),
				DbHelper.MakeInParam("@password", (DbType)OleDbType.VarChar, 16, foruminfo.Password),
				DbHelper.MakeInParam("@icon", (DbType)OleDbType.VarChar, 255, foruminfo.Icon),
				DbHelper.MakeInParam("@postcredits", (DbType)OleDbType.VarChar, 255, foruminfo.Postcredits),
				DbHelper.MakeInParam("@replycredits", (DbType)OleDbType.VarChar, 255, foruminfo.Replycredits),
				DbHelper.MakeInParam("@redirect", (DbType)OleDbType.VarChar, 255, foruminfo.Redirect),
				DbHelper.MakeInParam("@attachextensions", (DbType)OleDbType.VarChar, 255, foruminfo.Attachextensions),
				DbHelper.MakeInParam("@moderators", (DbType)OleDbType.VarWChar, 0, foruminfo.Moderators),
				DbHelper.MakeInParam("@rules", (DbType)OleDbType.VarWChar, 0, foruminfo.Rules),
				DbHelper.MakeInParam("@topictypes", (DbType)OleDbType.VarWChar, 0, foruminfo.Topictypes),
				DbHelper.MakeInParam("@viewperm", (DbType)OleDbType.VarWChar, 0, foruminfo.Viewperm),
				DbHelper.MakeInParam("@postperm", (DbType)OleDbType.VarWChar, 0, foruminfo.Postperm),
				DbHelper.MakeInParam("@replyperm", (DbType)OleDbType.VarWChar, 0, foruminfo.Replyperm),
				DbHelper.MakeInParam("@getattachperm", (DbType)OleDbType.VarWChar, 0, foruminfo.Getattachperm),
				DbHelper.MakeInParam("@postattachperm", (DbType)OleDbType.VarWChar, 0, foruminfo.Postattachperm)
			};
            sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "forumfields] ([fid],[description],[password],[icon],[postcredits],[replycredits],[redirect],[attachextensions],[moderators],[rules],[topictypes],[viewperm],[postperm],[replyperm],[getattachperm],[postattachperm]) VALUES (@fid,@description,@password,@icon,@postcredits,@replycredits,@redirect,@attachextensions,@moderators,@rules,@topictypes,@viewperm,@postperm,@replyperm,@getattachperm,@postattachperm)";
            DbHelper.ExecuteDataset(CommandType.Text, sql, prams1);
            return fid;
        }

        public void SetForumsPathList(string pathlist, int fid)
        {
            DbParameter[] prams = 
            {
			    DbHelper.MakeInParam("@pathlist", (DbType)OleDbType.VarChar, 3000, pathlist),
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
		    };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET pathlist=@pathlist  WHERE [fid]=@fid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void SetForumslayer(int layer, string parentidlist, int fid)
        {
            DbParameter[] prams = 
            {
                DbHelper.MakeInParam("@layer", (DbType)OleDbType.Integer, 2, layer),
                DbHelper.MakeInParam("@parentidlist", (DbType)OleDbType.Char, 300, parentidlist),
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
		    };
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [layer]=@layer WHERE [fid]=@fid", prams);
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [parentidlist]=@parentidlist WHERE [fid]=@fid", prams);
        }

        public int GetForumsParentidByFid(int fid)
        {
            DbParameter[] prams = 
            {
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
		    };
            string sql = "SELECT TOP 1 [parentid] FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE fid=@fid";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, prams));
        }

        public void MovingForumsPos(string currentfid, string targetfid, bool isaschildnode, string extname)
        {
            OleDbConnection conn = new OleDbConnection(DbHelper.ConnectionString);
            conn.Open();

            using (OleDbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //取得当前论坛版块的信息
                    DataRow dr = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 *  FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + currentfid).Tables[0].Rows[0];

                    //取得目标论坛版块的信息
                    DataRow targetdr = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 *  FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + targetfid).Tables[0].Rows[0];

                    //当前论坛版块带子版块时
                    if (DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1 FID FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [parentid]=" + currentfid).Tables[0].Rows.Count > 0)
                    {
                        #region

                        string sqlstring = "";
                        if (isaschildnode) //作为论坛子版块插入
                        {
                            //让位于当前论坛版块(分类)显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={0}",
                                                      Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString()) + 1));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            //更新当前论坛版块的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [parentid]='{1}',[displayorder]='{2}' WHERE [fid]={0}", currentfid, targetdr["fid"].ToString(), Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString().Trim()) + 1));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);
                        }
                        else //作为同级论坛版块,在目标论坛版块之前插入
                        {
                            //让位于包括当前论坛版块显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={0} OR [fid]={1}",
                                                      Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString())),
                                                      targetdr["fid"].ToString());
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            //更新当前论坛版块的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [parentid]='{1}',[displayorder]='{2}'  WHERE [fid]={0}", currentfid, targetdr["parentid"].ToString(), Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString().Trim())));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);
                        }

                        //更新由于上述操作所影响的版块数和帖子数
                        if ((dr["topics"].ToString() != "0") && (Convert.ToInt32(dr["topics"].ToString()) > 0) && (dr["posts"].ToString() != "0") && (Convert.ToInt32(dr["posts"].ToString()) > 0))
                        {
                            if (dr["parentidlist"].ToString().Trim() != "")
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [topics]=[topics]-" + dr["topics"].ToString() + ",[posts]=[posts]-" + dr["posts"].ToString() + "  WHERE [fid] IN(" + dr["parentidlist"].ToString().Trim() + ")");
                            }
                            if (targetdr["parentidlist"].ToString().Trim() != "")
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [topics]=[topics]+" + dr["topics"].ToString() + ",[posts]=[posts]+" + dr["posts"].ToString() + "  WHERE [fid] IN(" + targetdr["parentidlist"].ToString().Trim() + ")");
                            }
                        }

                        #endregion
                    }
                    else //当前论坛版块不带子版
                    {
                        #region

                        //设置旧的父一级的子论坛数
                        DbHelper.ExecuteDataset(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [subforumcount]=[subforumcount]-1 WHERE [fid]=" + dr["parentid"].ToString());

                        //让位于当前节点显示顺序之后的节点全部减1 [起到删除节点的效果]
                        if (isaschildnode) //作为子论坛版块插入
                        {
                            //更新相应的被影响的版块数和帖子数
                            if ((dr["topics"].ToString() != "0") && (Convert.ToInt32(dr["topics"].ToString()) > 0) && (dr["posts"].ToString() != "0") && (Convert.ToInt32(dr["posts"].ToString()) > 0))
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [topics]=[topics]-" + dr["topics"].ToString() + ",[posts]=[posts]-" + dr["posts"].ToString() + " WHERE [fid] IN(" + dr["parentidlist"].ToString() + ")");
                                if (targetdr["parentidlist"].ToString().Trim() != "")
                                {
                                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [topics]=[topics]+" + dr["topics"].ToString() + ",[posts]=[posts]+" + dr["posts"].ToString() + " WHERE [fid] IN(" + targetdr["parentidlist"].ToString() + "," + targetfid + ")");
                                }
                            }

                            //让位于当前论坛版块显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            string sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={0}",
                                                             Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString()) + 1));
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            //设置新的父一级的子论坛数
                            DbHelper.ExecuteDataset(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [subforumcount]=[subforumcount]+1 WHERE [fid]=" + targetfid);

                            string parentidlist = null;
                            if (targetdr["parentidlist"].ToString().Trim() == "0")
                            {
                                parentidlist = targetfid;
                            }
                            else
                            {
                                parentidlist = targetdr["parentidlist"].ToString().Trim() + "," + targetfid;
                            }

                            //更新当前论坛版块的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [parentid]='{1}',[layer]='{2}',[pathlist]='{3}', [parentidlist]='{4}',[displayorder]='{5}' WHERE [fid]={0}",
                                                      currentfid,
                                                      targetdr["fid"].ToString(),
                                                      Convert.ToString(Convert.ToInt32(targetdr["layer"].ToString()) + 1),
                                                      targetdr["pathlist"].ToString().Trim() + "<a href=\"showforum-" + currentfid + extname + "\">" + dr["name"].ToString().Trim().Replace("'","''") + "</a>",
                                                      parentidlist,
                                                      Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString().Trim()) + 1)
                                );
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                        }
                        else //作为同级论坛版块,在目标论坛版块之前插入
                        {
                            //更新相应的被影响的版块数和帖子数
                            if ((dr["topics"].ToString() != "0") && (Convert.ToInt32(dr["topics"].ToString()) > 0) && (dr["posts"].ToString() != "0") && (Convert.ToInt32(dr["posts"].ToString()) > 0))
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE " + BaseConfigs.GetTablePrefix + "forums SET [topics]=[topics]-" + dr["topics"].ToString() + ",[posts]=[posts]-" + dr["posts"].ToString() + "  WHERE [fid] IN(" + dr["parentidlist"].ToString() + ")");
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE " + BaseConfigs.GetTablePrefix + "forums SET [topics]=[topics]+" + dr["topics"].ToString() + ",[posts]=[posts]+" + dr["posts"].ToString() + "  WHERE [fid] IN(" + targetdr["parentidlist"].ToString() + ")");
                            }

                            //让位于包括当前论坛版块显示顺序之后的论坛版块全部加1(为新加入的论坛版块让位结果)
                            string sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]+1 WHERE [displayorder]>={0} OR [fid]={1}",
                                                             Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString()) + 1),
                                                             targetdr["fid"].ToString());
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);

                            //设置新的父一级的子论坛数
                            DbHelper.ExecuteDataset(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums]  SET [subforumcount]=[subforumcount]+1 WHERE [fid]=" + targetdr["parentid"].ToString());
                            string parentpathlist = "";
                            DataTable dt = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 [pathlist] FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + targetdr["parentid"].ToString()).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                parentpathlist = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 [pathlist] FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + targetdr["parentid"].ToString()).Tables[0].Rows[0][0].ToString().Trim();
                            }

                            //更新当前论坛版块的相关信息
                            sqlstring = string.Format("UPDATE [" + BaseConfigs.GetTablePrefix + "forums]  SET [parentid]='{1}',[layer]='{2}',[pathlist]='{3}', [parentidlist]='{4}',[displayorder]='{5}' WHERE [fid]={0}",
                                                      currentfid,
                                                      targetdr["parentid"].ToString(),
                                                      Convert.ToInt32(targetdr["layer"].ToString()),
                                                      parentpathlist + "<a href=\"showforum-" + currentfid + extname + "\">" + dr["name"].ToString().Trim() + "</a>",
                                                      targetdr["parentidlist"].ToString().Trim(),
                                                      Convert.ToString(Convert.ToInt32(targetdr["displayorder"].ToString().Trim()))
                                );
                            DbHelper.ExecuteDataset(trans, CommandType.Text, sqlstring);
                        }

                        #endregion
                    }
                    trans.Commit();
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                conn.Close();
            }
        }

        public bool IsExistSubForum(int fid)
        {
            DbParameter[] prams = 
            {
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
		    };
            string sql = "SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [parentid]=@fid";
            if (DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void DeleteForumsByFid(string postname, string fid)
        {
            OleDbConnection conn = new OleDbConnection(DbHelper.ConnectionString);
            conn.Open();
            using (OleDbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //先取出当前节点的信息
                    DataRow dr = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + fid).Tables[0].Rows[0];

                    //调整在当前节点排序位置之后的节点,做减1操作
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]-1 WHERE [displayorder]>" + dr["displayorder"].ToString());

                    //修改父结点中的子论坛个数
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [subforumcount]=[subforumcount]-1 WHERE  [fid]=" + dr["parentid"].ToString());

                    //删除当前节点的高级属性部分
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "forumfields] WHERE [fid]=" + fid);

                    //删除相关投票的信息
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "polls] WHERE [tid] IN(SELECT [tid] FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [fid]=" + fid + ")");

                    //删除帖子附件表中的信息
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "attachments] WHERE [tid] IN(SELECT [tid] FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [fid]=" + fid + ") OR [pid] IN(SELECT [pid] FROM [" + postname + "] WHERE [fid]=" + fid + ")");

                    //删除相关帖子
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + postname + "] WHERE [fid]=" + fid);

                    //删除相关主题
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [fid]=" + fid);


                    //删除当前节点
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE  [fid]=" + fid);

                    //删除版主列表中的相关信息
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "moderators] WHERE  [fid]=" + fid);

                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                }

            }
            conn.Close();
        }

        public DataTable GetParentIdByFid(int fid)
        {
            DbParameter[] prams = 
            {
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
		    };
            string sql = "SELECT [parentid] From [" + BaseConfigs.GetTablePrefix + "forums] WHERE [inheritedmod]=1 AND [fid]=@fid";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void InsertForumsModerators(string fid, string moderators, int displayorder, int inherited)
        {
            OleDbConnection conn = new OleDbConnection(DbHelper.ConnectionString);
            conn.Open();
            using (OleDbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    int count = displayorder;


                    //数据库中存在的用户
                    string usernamelist = "";
                    //清除已有论坛的版主设置
                    foreach (string username in moderators.Split(','))
                    {
                        if (username.Trim() != "")
                        {
                            DbParameter[] prams =
								{
									DbHelper.MakeInParam("@username", (DbType)OleDbType.VarChar, 20, username.Trim())
								};
                            //先取出当前节点的信息
                            DataTable dt = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 [uid] FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE [groupid]<>7 AND [groupid]<>8 AND [username]=@username", prams).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                DbHelper.ExecuteNonQuery(trans, CommandType.Text, "INSERT INTO [" + BaseConfigs.GetTablePrefix + "moderators] ([uid],[fid],[displayorder],[inherited]) VALUES(" + dt.Rows[0][0].ToString() + "," + fid + "," + count.ToString() + "," + inherited.ToString() + ")");
                                usernamelist = usernamelist + username.Trim() + ",";
                                count++;
                            }
                        }
                    }

                    if (usernamelist != "")
                    {
                        DbParameter[] prams1 =
							{
								DbHelper.MakeInParam("@moderators", (DbType)OleDbType.VarChar, 255, usernamelist.Substring(0, usernamelist.Length - 1))

							};
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET [moderators]=@moderators WHERE [fid] =" + fid, prams1);
                    }
                    else
                    {
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET [moderators]='' WHERE [fid] =" + fid);
                    }

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            conn.Close();
        }

        public DataTable GetFidInForumsByParentid(int parentid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("@parentid", (DbType)OleDbType.Integer, 4, parentid)
			};
            string sql = "SELECT [fid] FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [parentid]=@parentid ORDER BY [displayorder] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void CombinationForums(string sourcefid, string targetfid, string fidlist)
        {
            OleDbConnection conn = new OleDbConnection(DbHelper.ConnectionString);
            conn.Open();
            using (OleDbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    //ChildNode = "0";
                    //string fidlist = ("," + FindChildNode(targetfid)).Replace(",0,", "");
                    //更新帖子与主题的信息
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "topics] SET [fid]=" + targetfid + "  WHERE [fid]=" + sourcefid);
                    //要更新目标论坛的主题数
                    int totaltopics = Convert.ToInt32(DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT COUNT(tid)  FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [fid] IN(" + fidlist + ")").Tables[0].Rows[0][0].ToString());

                    int totalposts = 0;
                    foreach (DataRow postdr in DbHelper.ExecuteDataset(CommandType.Text, "SELECT [id] From [" + BaseConfigs.GetTablePrefix + "tablelist]").Tables[0].Rows)
                    {
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "posts" + postdr["id"].ToString() + "] SET [fid]=" + targetfid + "  WHERE [fid]=" + sourcefid);

                        //要更新目标论坛的帖子数
                        totalposts = totalposts + Convert.ToInt32(DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT COUNT(pid)  FROM [" + BaseConfigs.GetTablePrefix + "posts" + postdr["id"].ToString() + "] WHERE [fid] IN(" + fidlist + ")").Tables[0].Rows[0][0].ToString());
                    }

                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [topics]=" + totaltopics + " ,[posts]=" + totalposts + " WHERE [fid]=" + targetfid);

                    //获取源论坛信息
                    DataRow dr = DbHelper.ExecuteDataset(trans, CommandType.Text, "SELECT TOP 1 * FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + sourcefid).Tables[0].Rows[0];

                    //调整在当前节点排序位置之后的节点,做减1操作
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=[displayorder]-1 WHERE [displayorder]>" + dr["displayorder"].ToString());

                    //修改父结点中的子论坛个数
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [subforumcount]=[subforumcount]-1 WHERE [fid]=" + dr["parentid"].ToString());

                    //删除当前节点的高级属性部分
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "forumfields] WHERE [fid]=" + sourcefid);

                    //删除源论坛版块
                    DbHelper.ExecuteNonQuery(trans, CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid]=" + sourcefid);
                    trans.Commit();

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }
            conn.Close();
        }

        public void UpdateSubForumCount(int subforumcount, int fid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("@subforumcount", (DbType)OleDbType.Integer, 4, subforumcount),
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
			};
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [subforumcount]=@subforumcount WHERE [fid]=@fid";
            DbHelper.ExecuteDataset(CommandType.Text, sql, prams);
        }

        public void UpdateDisplayorderInForumByFid(int displayorder, int fid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, displayorder),
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
			};
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [displayorder]=@displayorder WHERE [fid]=@fid";
            DbHelper.ExecuteDataset(CommandType.Text, sql, prams);
        }

        public DataTable GetMainForum()
        {
            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [layer]=0 Order By [displayorder] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public void SetStatusInForum(int status, int fid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("@status", (DbType)OleDbType.Integer, 4, status),
                DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
			};
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [status]=@status WHERE [fid]=@fid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public DataTable GetForumByParentid(int parentid)
        {
            DbParameter[] prams =
			{
                DbHelper.MakeInParam("@parentid", (DbType)OleDbType.Integer, 4, parentid)
			};
            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [parentid]=@parentid ORDER BY [DisplayOrder]";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, prams).Tables[0];
        }

        public void UpdateStatusByFidlist(string fidlist)
        {
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [status]=0 WHERE [fid] IN(" + fidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void UpdateStatusByFidlistOther(string fidlist)
        {
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET [status]=1 WHERE [status]>1 AND [fid] IN(" + fidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public bool BatchSetForumInf(ForumInfo foruminfo, BatchSetParams bsp, string fidlist)
        {
            StringBuilder forums = new StringBuilder();
            StringBuilder forumfields = new StringBuilder();

            forums.Append("UPDATE [" + BaseConfigs.GetTablePrefix + "forums] SET ");
            if (bsp.SetSetting)
            {
                forums.Append("[Allowsmilies]='" + foruminfo.Allowsmilies + "' ,");
                forums.Append("[Allowrss]='" + foruminfo.Allowrss + "' ,");
                forums.Append("[Allowhtml]='" + foruminfo.Allowhtml + "' ,");
                forums.Append("[Allowbbcode]='" + foruminfo.Allowbbcode + "' ,");
                forums.Append("[Allowimgcode]='" + foruminfo.Allowimgcode + "' ,");
                forums.Append("[Allowblog]='" + foruminfo.Allowblog + "' ,");
                forums.Append("[istrade]='" + foruminfo.Istrade + "' ,");
                forums.Append("[allowpostspecial]='" + foruminfo.Allowpostspecial + "' ,");
                forums.Append("[allowspecialonly]='" + foruminfo.Allowspecialonly + "' ,");
                forums.Append("[Alloweditrules]='" + foruminfo.Alloweditrules + "' ,");
                forums.Append("[allowthumbnail]='" + foruminfo.Allowthumbnail + "' ,");
                forums.Append("[Recyclebin]='" + foruminfo.Recyclebin + "' ,");
                forums.Append("[Modnewposts]='" + foruminfo.Modnewposts + "' ,");
                forums.Append("[Jammer]='" + foruminfo.Jammer + "' ,");
                forums.Append("[Disablewatermark]='" + foruminfo.Disablewatermark + "' ,");
                forums.Append("[Inheritedmod]='" + foruminfo.Inheritedmod + "' ,");
            }
            if (forums.ToString().EndsWith(","))
            {
                forums.Remove(forums.Length - 1, 1);
            }
            forums.Append("WHERE [fid] IN(" + fidlist + ")");


            forumfields.Append("UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET ");

            if (bsp.SetPassWord)
            {
                forumfields.Append("[password]='" + foruminfo.Password + "' ,");
            }

            if (bsp.SetAttachExtensions)
            {
                forumfields.Append("[attachextensions]='" + foruminfo.Attachextensions + "' ,");
            }

            if (bsp.SetPostCredits)
            {
                forumfields.Append("[postcredits]='" + foruminfo.Postcredits + "' ,");
            }

            if (bsp.SetReplyCredits)
            {
                forumfields.Append("[replycredits]='" + foruminfo.Replycredits + "' ,");
            }


            if (bsp.SetViewperm)
            {
                forumfields.Append("[Viewperm]='" + foruminfo.Viewperm + "' ,");
            }

            if (bsp.SetPostperm)
            {
                forumfields.Append("[Postperm]='" + foruminfo.Postperm + "' ,");
            }

            if (bsp.SetReplyperm)
            {
                forumfields.Append("[Replyperm]='" + foruminfo.Replyperm + "' ,");
            }

            if (bsp.SetGetattachperm)
            {
                forumfields.Append("[Getattachperm]='" + foruminfo.Getattachperm + "' ,");
            }

            if (bsp.SetPostattachperm)
            {
                forumfields.Append("[Postattachperm]='" + foruminfo.Postattachperm + "' ,");
            }

            if (forumfields.ToString().EndsWith(","))
            {
                forumfields.Remove(forumfields.Length - 1, 1);
            }

            forumfields.Append("WHERE [fid] IN(" + fidlist + ")");


            OleDbConnection conn = new OleDbConnection(DbHelper.ConnectionString);
            conn.Open();
            using (OleDbTransaction trans = conn.BeginTransaction())
            {
                try
                {
                    if (forums.ToString().IndexOf("SET WHERE") < 0)
                    {
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, forums.ToString());
                    }

                    if (forumfields.ToString().IndexOf("SET WHERE") < 0)
                    {
                        DbHelper.ExecuteNonQuery(trans, CommandType.Text, forumfields.ToString());
                    }
                    trans.Commit();
                }
                catch
                {
                    trans.Rollback();
                    return false;
                }
            }
            return true;
        }

        public IDataReader GetTopForumFids(int lastfid, int statcount)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("@lastfid", (DbType)OleDbType.Integer, 4, lastfid),
			};

            return DbHelper.ExecuteReader(CommandType.Text, "SELECT TOP " + statcount + " [fid] FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [fid] > @lastfid", prams);
        }

        public DataSet GetOnlineList()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [groupid],(SELECT TOP 1 [grouptitle]  FROM [" + BaseConfigs.GetTablePrefix + "usergroups] WHERE [" + BaseConfigs.GetTablePrefix + "usergroups].[groupid]=[" + BaseConfigs.GetTablePrefix + "onlinelist].[groupid]) AS GroupName ,[displayorder],[title],[img] FROM [" + BaseConfigs.GetTablePrefix + "onlinelist] ORDER BY [groupid] ASC");
        }

        public int UpdateOnlineList(int groupid, int displayorder, string img, string title)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@groupid", (DbType)OleDbType.Integer, 4, groupid),
                                        DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer, 4, displayorder),
                                        DbHelper.MakeInParam("@img", (DbType)OleDbType.VarChar, 50, img),
                                        DbHelper.MakeInParam("@title", (DbType)OleDbType.VarWChar, 50, title)
                                    };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "onlinelist] SET [displayorder]=@displayorder,[title]=@title,[img]=@img  WHERE [groupid]=@groupid";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public string GetWords()
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "words]";
        }

        public DataTable GetBadWords()
        { 
            string strsql = "SELECT [find],[replacement] FROM ["+ BaseConfigs.GetTablePrefix + "words] ORDER BY [find]";
            return DbHelper.ExecuteDataset(strsql).Tables[0];
        }

        public int DeleteWord(int id)
        {
            DbParameter parm = DbHelper.MakeInParam("@id", (DbType)OleDbType.Integer, 4, id);

            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "words] WHERE [id]=@id";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, parm);
        }
        public int GetBadWordId(string find)
        {
            string sql = "select id from " + BaseConfigs.GetTablePrefix + "words where find='" + find + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }
        public void UpdateBadWords(string find, string replacement)
        {
            string sql = "update " + BaseConfigs.GetTablePrefix + "words set replacement='" + replacement + "' where find ='" + find + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text,sql);
        }
        public void InsertBadWords(string username, string find,string replacement)
        {
            string sql = "insert into " + BaseConfigs.GetTablePrefix + "words(admin,find,replacement) values('" + username + "','" + find + "','" + replacement + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text,sql);
        }

        public void DeleteBadWords()
        { 
            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "words] ";
            DbHelper.ExecuteNonQuery(CommandType.Text,sql);
        }

        public int UpdateWord(int id, string find, string replacement)
        {
            DbParameter[] prams = {
                    DbHelper.MakeInParam("@id", (DbType)OleDbType.Integer, 4, id),
					DbHelper.MakeInParam("@find", (DbType)OleDbType.VarChar, 255, find),
					DbHelper.MakeInParam("@replacement", (DbType)OleDbType.VarChar, 255, replacement)
				};

            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "words] SET [find]=@find, [replacement]=@replacement WHERE [id]=@id";

            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public int DeleteWords(string idlist)
        {
            return DbHelper.ExecuteNonQuery(CommandType.Text, "DELETE FROM [" + BaseConfigs.GetTablePrefix + "words]  WHERE [ID] IN(" + idlist + ")");
        }

        public bool ExistWord(string find)
        {
            DbParameter parm = DbHelper.MakeInParam("@find", (DbType)OleDbType.VarWChar, 255, find);
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT TOP 1  * FROM [" + BaseConfigs.GetTablePrefix + "words] WHERE [find]=@find", parm).Tables[0].Rows.Count > 0;
        }

        public int AddWord(string username, string find, string replacement)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@username", (DbType)OleDbType.VarWChar, 20, username),
                                        DbHelper.MakeInParam("@find", (DbType)OleDbType.VarWChar, 255, find),
                                        DbHelper.MakeInParam("@replacement", (DbType)OleDbType.VarWChar, 255, replacement)
                                    };
            string sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "words] ([admin], [find], [replacement]) VALUES (@username,@find,@replacement)";

            return DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public bool IsExistTopicType(string typename,int currenttypeid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@typename", (DbType)OleDbType.VarWChar, 30, typename),
                                        DbHelper.MakeInParam("@currenttypeid", (DbType)OleDbType.Integer, 4, currenttypeid)
                                    };
            string sql = "SELECT [typeid] FROM [" + BaseConfigs.GetTablePrefix + "topictypes] WHERE [name]=@typename AND [typeid]<>@currenttypeid";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0].Rows.Count != 0;
        }

        public bool IsExistTopicType(string typename)
        {
            DbParameter parms = DbHelper.MakeInParam("@typename", (DbType)OleDbType.VarWChar, 30, typename);
            string sql = "SELECT TOP 1  * FROM [" + BaseConfigs.GetTablePrefix + "topictypes] WHERE [name]=@typename";
            return DbHelper.ExecuteDataset(CommandType.Text, sql, parms).Tables[0].Rows.Count != 0;
        }

        public string GetTopicTypes()
        {
            return "SELECT [typeid] AS id,[name],[displayorder],[description] FROM [" + BaseConfigs.GetTablePrefix + "topictypes] ORDER BY [displayorder] ASC";
        }

        public DataTable GetExistTopicTypeOfForum()
        {
            string sql = "SELECT [fid],[topictypes] FROM [" + BaseConfigs.GetTablePrefix + "forumfields] WHERE [topictypes] NOT LIKE ''";
            return DbHelper.ExecuteDataset(CommandType.Text,sql).Tables[0];
        }

        public void UpdateTopicTypeForForum(string topictypes,int fid)
        {
            DbParameter[] parms = { 
                                        DbHelper.MakeInParam("@topictypes", (DbType)OleDbType.VarWChar, 0, topictypes),
                                        DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, fid)
                                    };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET [topictypes]=@topictypes WHERE [fid]=@fid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

        public void UpdateTopicTypes(string name, int displayorder,string description,int typeid)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar,100, name),
				DbHelper.MakeInParam("@displayorder", (DbType)OleDbType.Integer,4,displayorder),
				DbHelper.MakeInParam("@description", (DbType)OleDbType.VarChar,500,description),
				DbHelper.MakeInParam("@typeid", (DbType)OleDbType.Integer,4,typeid)
								   };
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "topictypes] SET [name]=@name ,[displayorder]=@displayorder, [description]=@description WHERE [typeid]=@typeid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public void AddTopicTypes(string typename, int displayorder, string description)
        {
            DbParameter[] prams = {
				DbHelper.MakeInParam("@name",(DbType)OleDbType.VarWChar,100, typename),
				DbHelper.MakeInParam("@displayorder",(DbType)OleDbType.Integer,4,displayorder),
				DbHelper.MakeInParam("@description",(DbType)OleDbType.VarChar,500,description)
								  };
            string sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "topictypes] ([name],[displayorder],[description]) VALUES(@name,@displayorder,@description)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
        }

        public int GetMaxTopicTypesId()
        {
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, "SELECT MAX([typeid]) FROM [" + BaseConfigs.GetTablePrefix + "topictypes]").ToString());
        }

        public void DeleteTopicTypesByTypeidlist(string typeidlist)
        {
            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "topictypes]  WHERE [typeid] IN(" + typeidlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text,sql);
        }

        public DataTable GetForumNameIncludeTopicType()
        {
            string sql = "SELECT f1.[fid],[name],[topictypes] FROM [" + BaseConfigs.GetTablePrefix + "forums] AS f1 LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forumfields] AS f2 ON f1.fid=f2.fid";
            return DbHelper.ExecuteDataset(CommandType.Text,sql).Tables[0];
        }

        public DataTable GetForumTopicType()
        {
            string sql = "SELECT [fid],[topictypes] FROM [" + BaseConfigs.GetTablePrefix + "forumfields]";
            return DbHelper.ExecuteDataset(CommandType.Text,sql).Tables[0];
        }

        public void ClearTopicTopicType(int typeid)
        {
            DbParameter pram = DbHelper.MakeInParam("@typeid", (DbType)OleDbType.Integer, 4, typeid);
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "topics] SET [typeid]=0 Where [typeid]=@typeid";
            DbHelper.ExecuteNonQuery(CommandType.Text,sql,pram);
        }

        public string GetTopicTypeInfo()
        {
            return "SELECT [typeid] AS id,[name],[description] FROM [" + BaseConfigs.GetTablePrefix + "topictypes] ORDER BY [displayorder] ASC";
        }

        public string GetTemplateName()
        {
            return "SELECT [templateid],[name] FROM [" + BaseConfigs.GetTablePrefix + "templates]";
        }

        public DataTable GetAttachType()
        {
            return DbHelper.ExecuteDataset(CommandType.Text, "SELECT [id],[extension]  FROM [" + BaseConfigs.GetTablePrefix + "attachtypes]  ORDER BY [id] ASC").Tables[0];
        }

        public void UpdatePermUserListByFid(string permuserlist, int fid)
        {
            DbParameter[] prams = 
			{
				DbHelper.MakeInParam("@permuserlist", (DbType)OleDbType.VarWChar,0,permuserlist),
				DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer,4, fid)
			};
            DbHelper.ExecuteNonQuery(CommandType.Text, "UPDATE [" + BaseConfigs.GetTablePrefix + "forumfields] SET [Permuserlist]=@permuserlist WHERE [fid]=@fid", prams);
        }

        public IDataReader GetTopicsIdentifyItem()
        {
            return DbHelper.ExecuteReader(CommandType.Text, "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "topicidentify]");
        }

        public string ResetTopTopicListSql(int layer, string fid, string parentidlist)
        {

            string filterexpress = "";

            switch (layer)
            {
                case 0:
                    filterexpress = string.Format("[fid]<>{0} AND (',' + TRIM([parentidlist]) + ',' LIKE '%,{1},%')", fid.ToString(), RegEsc(fid.ToString()));
                    break;
                case 1:
                    filterexpress = parentidlist.ToString().Trim();
                    if (filterexpress != string.Empty)
                    {
                        filterexpress =
                            string.Format(
                                "[fid]<>{0} AND ([fid]={1} OR (',' + TRIM([parentidlist]) + ',' LIKE '%,{2},%'))",
                                fid.ToString().Trim(), filterexpress, RegEsc(filterexpress));
                    }
                    else
                    {
                        filterexpress =
                            string.Format(
                                "[fid]<>{0} AND (',' + TRIM([parentidlist]) + ',' LIKE '%,{1},%')",
                                fid.ToString().Trim(), RegEsc(filterexpress));
                    }
                    break;
                default:
                    filterexpress = parentidlist.ToString().Trim();
                    if (filterexpress != string.Empty)
                    {
                        filterexpress = Utils.CutString(filterexpress, 0, filterexpress.IndexOf(","));
                        filterexpress =
                            string.Format(
                                "[fid]<>{0} AND ([fid]={1} OR (',' + TRIM([parentidlist]) + ',' LIKE '%,{2},%'))",
                                fid.ToString().Trim(), filterexpress, RegEsc(filterexpress));
                    }
                    else
                    {
                        filterexpress =
                            string.Format(
                                "[fid]<>{0} AND (',' + TRIM([parentidlist]) + ',' LIKE '%,{1},%')",
                                fid.ToString().Trim(), RegEsc(filterexpress));
                    }
                    break;
            }

            return filterexpress;
        }

        public string showforumcondition(int sqlid, int cond)
        {

            string sql = null;
            switch (sqlid)
            {
                case 1:
                    sql = " AND [typeid]=";
                    break;
                case 2:
                    sql = " AND [postdatetime]>=#" + DateTime.Now.AddDays(-1 * cond).ToString("yyyy-MM-dd HH:mm:ss") + "#";
                    break;

                case 3:
                    sql = "tid";
                    break;

            }
            return sql;

        }

        public string DelVisitLogCondition(string deletemod, string visitid, string deletenum, string deletefrom)
        {
            string condition = null;
            switch (deletemod)
            {
                case "chkall":
                    if (visitid != "")
                        condition = " [visitid] IN(" + visitid + ")";
                    break;
                case "deleteNum":
                    if (deletenum != "" && Utils.IsNumeric(deletenum))
                        condition = " [visitid] NOT IN (SELECT TOP " + deletenum + " [visitid] FROM [" + BaseConfigs.GetTablePrefix + "adminvisitlog] ORDER BY [visitid] DESC)";
                    break;
                case "deleteFrom":
                    if (deletefrom != "")
                        condition = " [postdatetime]<#" + deletefrom + "#";
                    break;
            }
            return condition;
        }


        public string AttachDataBind(string condition, string postname)
        {
            return "SELECT [aid], [attachment], [filename], (SELECT TOP 1 [poster] FROM [" + postname + "] WHERE [" + postname + "].[pid]=[" + BaseConfigs.GetTablePrefix + "attachments].[pid]) AS [poster],(Select TOP 1 [title] FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [" + BaseConfigs.GetTablePrefix + "topics].[tid]=[" + BaseConfigs.GetTablePrefix + "attachments].[tid]) AS [topictitle], [filesize],[downloads]  FROM [" + BaseConfigs.GetTablePrefix + "attachments] " + condition;
        }

        public DataTable GetAttachDataTable(string condition, string postname)
        {
            string sqlstring = "SELECT [aid], [attachment], [filename], (SELECT TOP 1 [poster] FROM [" + postname + "] WHERE [" + postname + "].[pid]=[" + BaseConfigs.GetTablePrefix + "attachments].[pid]) AS [poster],(Select TOP 1 [title] FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [" + BaseConfigs.GetTablePrefix + "topics].[tid]=[" + BaseConfigs.GetTablePrefix + "attachments].[tid]) AS [topictitle], [filesize],[downloads]  FROM [" + BaseConfigs.GetTablePrefix + "attachments] " + condition;
            return DbHelper.ExecuteDataset(sqlstring).Tables[0];
        }


        public bool AuditTopicCount(string condition)
        {

            if (DbHelper.ExecuteDataset("SELECT COUNT(tid) FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE " + condition).Tables[0].Rows[0][0].ToString() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public string AuditTopicBindStr(string condition)
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE " + condition;
        }

        public DataTable AuditTopicBind(string condition)
        {
            DataTable dt = DbHelper.ExecuteDataset("SELECT * FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE " + condition).Tables[0];
            return dt;
        }

        public string AuditNewUserClear(string searchuser,string regbefore, string regip)
        {
            string sqlstring = "";
            sqlstring += " [groupid]=8";
            if (searchuser != "")
            {
                sqlstring += " AND [username] LIKE '%" + searchuser + "%'";
            }
            if (regbefore != "")
            {
                sqlstring += " AND [joindate]<=#" + DateTime.Now.AddDays(-Convert.ToDouble(regbefore)).ToString("yyyy-MM-dd HH:mm:ss") + "# ";
            }

            if (regip != "")
            {
                sqlstring += " AND [regip] LIKE '" + RegEsc(regip) + "%'";
            }

            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "users] WHERE " + sqlstring;
        }

        public string DelMedalLogCondition(string deletemode, string id, string deleteNum, string deleteFrom)
        {
            string condition = "";
            switch (deletemode)
            {
                case "chkall":
                    if (id != "")
                        condition = " [id] IN(" + id + ")";
                    break;
                case "deleteNum":
                    if (deleteNum != "" && Utils.IsNumeric(deleteNum))
                        condition = " [id] NOT IN (SELECT TOP " + deleteNum + " [id] FROM [" + BaseConfigs.GetTablePrefix + "medalslog] ORDER BY [id] DESC)";
                    break;
                case "deleteFrom":
                    if (deleteFrom != "")
                        condition = " [postdatetime]<#" + DateTime.Parse(deleteFrom).ToString("yyyy-MM-dd HH:mm:ss") + "#";
                    break;
            }
            return condition;

        }

        public DataTable MedalsTable(string medalid)
        {

            DataTable dt = DbHelper.ExecuteDataset("SELECT * FROM [" + BaseConfigs.GetTablePrefix + "medals] WHERE [medalid]=" + medalid).Tables[0];
            return dt;
        }

        public string DelModeratorManageCondition(string deletemode, string id, string deleteNum, string deleteFrom)
        {
            string condition = "";
            switch (deletemode)
            {
                case "chkall":
                    if (id!= "")
                        condition = " [id] IN(" + id + ")";
                    break;
                case "deleteNum":
                    if (deleteNum != "" && Utils.IsNumeric(deleteNum))
                        condition = " [id] NOT IN (SELECT TOP " + deleteNum + " [id] FROM [" + BaseConfigs.GetTablePrefix + "moderatormanagelog] ORDER BY [id] DESC)";
                    break;
                case "deleteFrom":
                    if (deleteFrom != "")
                        condition = " [postdatetime]<#" + DateTime.Parse(deleteFrom).ToString("yyyy-MM-dd HH:mm:ss") + "#";
                    break;
            }
            return condition;
        }

        public DataTable GroupNameTable(string groupid)
        {
            DataTable dt = DbHelper.ExecuteDataset("SELECT TOP 1 [grouptitle] FROM [" + BaseConfigs.GetTablePrefix + "usergroups] WHERE [groupid]=" + groupid).Tables[0];
            return dt;
        }

        public string PaymentLogCondition(string deletemode, string id, string deleteNum, string deleteFrom)
        {
            string condition = "";
            switch (deletemode)
            {
                case "chkall":
                    if (id != "")
                        condition = " [id] IN(" + id + ")";
                    break;
                case "deleteNum":
                    if (deleteNum != "" && Utils.IsNumeric(deleteNum))
                        condition = " [id] NOT IN (SELECT TOP " + deleteNum + " [id] FROM [" + BaseConfigs.GetTablePrefix + "paymentlog] ORDER BY [id] DESC)";
                    break;
                case "deleteFrom":
                    if (deleteFrom != "")
                        condition = " [buydate]<#" + DateTime.Parse(deleteFrom).ToString("yyyy-MM-dd HH:mm:ss") + "#";
                    break;
            }
            return condition;
        }

        public string PostGridBind(string posttablename, string condition)
        {
            return "SELECT * FROM [" + posttablename + "] WHERE " + condition.ToString();
        }

        public string DelRateScoreLogCondition(string deletemode, string id, string deleteNum, string deleteFrom)
        {
            string condition = "";
            switch (deletemode)
            {
                case "chkall":
                    if (id != "")
                        condition = " [id] IN(" + id + ")";
                    break;
                case "deleteNum":
                    if (deleteNum != "" && Utils.IsNumeric(deleteNum))
                        condition = " [id] NOT IN (SELECT TOP " + deleteNum + " [id] FROM [" + BaseConfigs.GetTablePrefix + "ratelog] ORDER BY [id] DESC)";
                    break;
                case "deleteFrom":
                    if (deleteFrom != "")
                        condition = " [postdatetime]<#" + DateTime.Parse(deleteFrom).ToString("yyyy-MM-dd HH:mm:ss") + "#";
                    break;
            }
            return condition;
        }

        public void UpdatePostSP()
        {
            #region 更新分表的存储过程
            foreach (DataRow dr in DatabaseProvider.GetInstance().GetDatechTableIds())
            {
                CreateStoreProc(Convert.ToInt16(dr["id"].ToString()));
            }
            #endregion
        }

        public void CreateStoreProc(int tablelistmaxid)
        {
            #region 创建分表存储过程
//            StringBuilder sb = new StringBuilder(@"
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_createpost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_createpost]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getfirstpostid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getfirstpostid]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getpostcount]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getpostcount]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_deletepostbypid]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_deletepostbypid]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getposttree]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getposttree]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getsinglepost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getsinglepost]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_updatepost]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_updatepost]
//
//                    ~
//
//                    if exists (select * from sysobjects where id = object_id(N'[dnt_getnewtopics]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
//                    drop procedure [dnt_getnewtopics]
//
//                    ~
//
//                    CREATE PROCEDURE dnt_createpost
//                    @fid int,
//                    @tid int,
//                    @parentid int,
//                    @layer int,
//                    @poster varchar(20),
//                    @posterid int,
//                    @title nvarchar(60),
//                    @topictitle nvarchar(60),
//                    @postdatetime char(20),
//                    @message ntext,
//                    @ip varchar(15),
//                    @lastedit varchar(50),
//                    @invisible int,
//                    @usesig int,
//                    @htmlon int,
//                    @smileyoff int,
//                    @bbcodeoff int,
//                    @parseurloff int,
//                    @attachment int,
//                    @rate int,
//                    @ratetimes int
//
//                    AS
//
//
//                    DEClARE @postid int
//
//                    DELETE FROM [dnt_postid] WHERE DATEDIFF(n, postdatetime, now()) >5
//
//                    INSERT INTO [dnt_postid] ([postdatetime]) VALUES(now())
//
//                    SELECT @postid=SCOPE_IDENTITY()
//
//                    INSERT INTO [dnt_posts1]([pid], [fid], [tid], [parentid], [layer], [poster], [posterid], [title], [postdatetime], [message], [ip], [lastedit], [invisible], [usesig], [htmlon], [smileyoff], [bbcodeoff], [parseurloff], [attachment], [rate], [ratetimes]) VALUES(@postid, @fid, @tid, @parentid, @layer, @poster, @posterid, @title, @postdatetime, @message, @ip, @lastedit, @invisible, @usesig, @htmlon, @smileyoff, @bbcodeoff, @parseurloff, @attachment, @rate, @ratetimes)
//
//                    IF @parentid=0
//                        BEGIN
//                    		
//                            UPDATE [dnt_posts1] SET [parentid]=@postid WHERE [pid]=@postid
//                        END
//
//                    IF @@ERROR=0
//                        BEGIN
//                            IF  @invisible = 0
//                            BEGIN
//                    		
//                                UPDATE [dnt_statistics] SET [totalpost]=[totalpost] + 1
//                    		
//                    		
//                    		
//                                DECLARE @fidlist AS VARCHAR(1000)
//                                DECLARE @strsql AS VARCHAR(4000)
//                    			
//                                SET @fidlist = '';
//                    			
//                                SELECT @fidlist = ISNULL([parentidlist],'') FROM [dnt_forums] WHERE [fid] = @fid
//                                IF RTRIM(@fidlist)<>''
//	                                BEGIN
//		                                SET @fidlist = RTRIM(@fidlist) + ',' + CAST(@fid AS VARCHAR(10))
//	                                END
//                                ELSE
//	                                BEGIN
//		                                SET @fidlist = CAST(@fid AS VARCHAR(10))
//	                                END
//                            
//                    			
//                                UPDATE [dnt_forums] SET 
//						                                [posts]=[posts] + 1, 
//						                                [todayposts]=CASE 
//										                                WHEN DATEDIFF(day, [lastpost], now())=0 THEN [todayposts]*1 + 1 
//									                                 ELSE 1 
//									                                 END,
//						                                [lasttid]=@tid,	
//                                                        [lasttitle]=@topictitle,
//						                                [lastpost]=@postdatetime,
//						                                [lastposter]=@poster,
//						                                [lastposterid]=@posterid 
//                    							
//				                                WHERE (InStr(',' + RTRIM([fid]) + ',', ',' + (SELECT @fidlist AS [fid]) + ',') > 0)
//                    			
//                    			
//                                UPDATE [dnt_users] SET
//	                                [lastpost] = @postdatetime,
//	                                [lastpostid] = @postid,
//	                                [lastposttitle] = @title,
//	                                [posts] = [posts] + 1,
//	                                [lastactivity] = now()
//                                WHERE [uid] = @posterid
//                            
//                            
//                                IF @layer<=0
//	                                BEGIN
//		                                UPDATE [dnt_topics] SET [replies]=0,[lastposter]=@poster,[lastpost]=@postdatetime,[lastposterid]=@posterid WHERE [tid]=@tid
//	                                END
//                                ELSE
//	                                BEGIN
//		                                UPDATE [dnt_topics] SET [replies]=[replies] + 1,[lastposter]=@poster,[lastpost]=@postdatetime,[lastposterid]=@posterid WHERE [tid]=@tid
//	                                END
//                            END
//
//                            UPDATE [dnt_topics] SET [lastpostid]=@postid WHERE [tid]=@tid
//                            
//                        IF @posterid <> -1
//                            BEGIN
//                                INSERT [dnt_myposts]([uid], [tid], [pid], [dateline]) VALUES(@posterid, @tid, @postid, @postdatetime)
//                            END
//
//                        END
//                    	
//                    SELECT @postid AS postid
//
//                    ~
//
//
//                    CREATE PROCEDURE dnt_getfirstpostid
//                    @tid int
//                    AS
//                    SELECT TOP 1 [pid] FROM [dnt_posts1] WHERE [tid]=@tid ORDER BY [pid]
//
//                    ~
//
//
//                    CREATE PROCEDURE dnt_getpostcount
//                    @tid int
//                    AS
//                    SELECT COUNT(pid) FROM [dnt_posts1] WHERE [tid]=@tid AND [invisible]=0 AND layer>0
//
//                    ~
//
//
//                    CREATE  PROCEDURE dnt_deletepostbypid
//                        @pid int,
//						@chanageposts AS BIT
//                    AS
//
//                        DECLARE @fid int
//                        DECLARE @tid int
//                        DECLARE @posterid int
//                        DECLARE @lastforumposterid int
//                        DECLARE @layer int
//                        DECLARE @postdatetime smalldatetime
//                        DECLARE @poster varchar(50)
//                        DECLARE @postcount int
//                        DECLARE @title nchar(60)
//                        DECLARE @lasttid int
//                        DECLARE @postid int
//                        DECLARE @todaycount int
//                    	
//                    	
//                        SELECT @fid = [fid],@tid = [tid],@posterid = [posterid],@layer = [layer], @postdatetime = [postdatetime] FROM [dnt_posts1] WHERE pid = @pid
//
//                        DECLARE @fidlist AS VARCHAR(1000)
//                    	
//                        SET @fidlist = '';
//                    	
//                        SELECT @fidlist = ISNULL([parentidlist],'') FROM [dnt_forums] WHERE [fid] = @fid
//                        IF RTRIM(@fidlist)<>''
//                            BEGIN
//                                SET @fidlist = RTRIM(@fidlist) + ',' + CAST(@fid AS VARCHAR(10))
//                            END
//                        ELSE
//                            BEGIN
//                                SET @fidlist = CAST(@fid AS VARCHAR(10))
//                            END
//
//
//                        IF @layer<>0
//
//                            BEGIN
//                    			
//								IF @chanageposts = 1
//									BEGIN
//										UPDATE [dnt_statistics] SET [totalpost]=[totalpost] - 1
//
//										UPDATE [dnt_forums] SET 
//											[posts]=[posts] - 1, 
//											[todayposts]=CASE 
//																WHEN DATEPART(yyyy, @postdatetime)=DATEPART(yyyy,now()) AND DATEPART(mm, @postdatetime)=DATEPART(mm,now()) AND DATEPART(dd, @postdatetime)=DATEPART(dd,now()) THEN [todayposts] - 1
//																ELSE [todayposts]
//														END						
//										WHERE (InStr(',' + RTRIM([fid]) + ',', ',' +
//															(SELECT @fidlist AS [fid]) + ',') > 0)
//                    			
//										UPDATE [dnt_users] SET [posts] = [posts] - 1 WHERE [uid] = @posterid
//
//										UPDATE [dnt_topics] SET [replies]=[replies] - 1 WHERE [tid]=@tid
//									END
//                    			
//                                DELETE FROM [dnt_posts1] WHERE [pid]=@pid
//                    			
//                            END
//                        ELSE
//                            BEGIN
//                    		
//                                SELECT @postcount = COUNT([pid]) FROM [dnt_posts1] WHERE [tid] = @tid
//                                SELECT @todaycount = COUNT([pid]) FROM [dnt_posts1] WHERE [tid] = @tid AND DATEDIFF(d, [postdatetime], now()) = 0
//                    			
//								IF @chanageposts = 1
//									BEGIN
//										UPDATE [dnt_statistics] SET [totaltopic]=[totaltopic] - 1, [totalpost]=[totalpost] - @postcount
//		                    			
//										UPDATE [dnt_forums] SET [posts]=[posts] - @postcount, [topics]=[topics] - 1,[todayposts]=[todayposts] - @todaycount WHERE (InStr(',' + RTRIM([fid]) + ',', ',' +(SELECT @fidlist AS [fid]) + ',') > 0)
//		                    			
//										UPDATE [dnt_users] SET [posts] = [posts] - @postcount WHERE [uid] = @posterid
//                    			
//									END
//
//                                DELETE FROM [dnt_posts1] WHERE [tid] = @tid
//                    			
//                                DELETE FROM [dnt_topics] WHERE [tid] = @tid
//                    			
//                            END	
//                    		
//
//                        IF @layer<>0
//                            BEGIN
//                                SELECT TOP 1 @pid = [pid], @posterid = [posterid], @postdatetime = [postdatetime], @title = [title], @poster = [poster] FROM [dnt_posts1] WHERE [tid]=@tid ORDER BY [pid] DESC
//                                UPDATE [dnt_topics] SET [lastposter]=@poster,[lastpost]=@postdatetime,[lastpostid]=@pid,[lastposterid]=@posterid WHERE [tid]=@tid
//                            END
//
//
//
//                        SELECT @lasttid = [lasttid] FROM [dnt_forums] WHERE [fid] = @fid
//
//                    	
//                        IF @lasttid = @tid
//                            BEGIN
//
//                    			
//                    			
//
//                                SELECT TOP 1 @pid = [pid], @tid = [tid],@lastforumposterid = [posterid], @title = [title], @postdatetime = [postdatetime], @poster = [poster] FROM [dnt_posts1] WHERE [fid] = @fid ORDER BY [pid] DESC
//                    			
//                            
//                            
//                                UPDATE [dnt_forums] SET 
//                    			
//	                                [lastpost]=@postdatetime,
//	                                [lastposter]=ISNULL(@poster,''),
//	                                [lastposterid]=ISNULL(@lastforumposterid,'0')
//
//                                WHERE (InStr(',' + RTRIM([fid]) + ',', ',' +
//					                                (SELECT @fidlist AS [fid]) + ',') > 0)
//
//
//                    			
//                                SELECT TOP 1 @pid = [pid], @tid = [tid],@posterid = [posterid], @postdatetime = [postdatetime], @title = [title], @poster = [poster] FROM [dnt_posts1] WHERE [posterid]=@posterid ORDER BY [pid] DESC
//                    			
//                                UPDATE [dnt_users] SET
//                    			
//	                                [lastpost] = @postdatetime,
//	                                [lastpostid] = @pid,
//	                                [lastposttitle] = ISNULL(@title,'')
//                    				
//                                WHERE [uid] = @posterid
//                    			
//                            END
//
//
//                    ~
//
//
//                    CREATE PROCEDURE dnt_getposttree
//                    @tid int
//                    AS
//                    SELECT [pid], [layer], [title], [poster], [posterid],[postdatetime],[message] FROM [dnt_posts1] WHERE [tid]=@tid AND [invisible]=0 ORDER BY [parentid];
//
//
//                    ~
//
//                    CREATE PROCEDURE dnt_getsinglepost
//                    @tid int,
//                    @pid int
//                    AS
//                    SELECT [aid], [tid], [pid], [postdatetime], [readperm], [filename], [description], [filetype], [filesize], [attachment], [downloads] FROM [dnt_attachments] WHERE [tid]=@tid
//
//                    SELECT TOP 1 
//	                                [dnt_posts1].[pid], 
//	                                [dnt_posts1].[fid], 
//	                                [dnt_posts1].[title], 
//	                                [dnt_posts1].[layer],
//	                                [dnt_posts1].[message], 
//	                                [dnt_posts1].[ip], 
//	                                [dnt_posts1].[lastedit], 
//	                                [dnt_posts1].[postdatetime], 
//	                                [dnt_posts1].[attachment], 
//	                                [dnt_posts1].[poster], 
//	                                [dnt_posts1].[invisible], 
//	                                [dnt_posts1].[usesig], 
//	                                [dnt_posts1].[htmlon], 
//	                                [dnt_posts1].[smileyoff], 
//	                                [dnt_posts1].[parseurloff], 
//	                                [dnt_posts1].[bbcodeoff], 
//	                                [dnt_posts1].[rate], 
//	                                [dnt_posts1].[ratetimes], 
//	                                [dnt_posts1].[posterid], 
//	                                [dnt_users].[nickname],  
//	                                [dnt_users].[username], 
//	                                [dnt_users].[groupid],
//                                    [dnt_users].[spaceid],
//                                    [dnt_users].[gender],
//									[dnt_users].[bday], 
//	                                [dnt_users].[email], 
//	                                [dnt_users].[showemail], 
//	                                [dnt_users].[digestposts], 
//	                                [dnt_users].[credits], 
//	                                [dnt_users].[extcredits1], 
//	                                [dnt_users].[extcredits2], 
//	                                [dnt_users].[extcredits3], 
//	                                [dnt_users].[extcredits4], 
//	                                [dnt_users].[extcredits5], 
//	                                [dnt_users].[extcredits6], 
//	                                [dnt_users].[extcredits7], 
//	                                [dnt_users].[extcredits8], 
//	                                [dnt_users].[posts], 
//	                                [dnt_users].[joindate], 
//	                                [dnt_users].[onlinestate], 
//	                                [dnt_users].[lastactivity], 
//	                                [dnt_users].[invisible], 
//	                                [dnt_userfields].[avatar], 
//	                                [dnt_userfields].[avatarwidth], 
//	                                [dnt_userfields].[avatarheight], 
//	                                [dnt_userfields].[medals], 
//	                                [dnt_userfields].[sightml] AS signature, 
//	                                [dnt_userfields].[location], 
//	                                [dnt_userfields].[customstatus], 
//	                                [dnt_userfields].[website], 
//	                                [dnt_userfields].[icq], 
//	                                [dnt_userfields].[qq], 
//	                                [dnt_userfields].[msn], 
//	                                [dnt_userfields].[yahoo], 
//	                                [dnt_userfields].[skype] 
//                    FROM [dnt_posts1] LEFT JOIN [dnt_users] ON [dnt_users].[uid]=[dnt_posts1].[posterid] LEFT JOIN [dnt_userfields] ON [dnt_userfields].[uid]=[dnt_users].[uid] WHERE [dnt_posts1].[pid]=@pid
//
//
//                    ~
//
//                    CREATE PROCEDURE dnt_updatepost
//                        @pid int,
//                        @title nvarchar(160),
//                        @message ntext,
//                        @lastedit nvarchar(50),
//                        @invisible int,
//                        @usesig int,
//                        @htmlon int,
//                        @smileyoff int,
//                        @bbcodeoff int,
//                        @parseurloff int
//                    AS
//                    UPDATE dnt_posts1 SET 
//                        [title]=@title,
//                        [message]=@message,
//                        [lastedit]=@lastedit,
//                        [invisible]=@invisible,
//                        [usesig]=@usesig,
//                        [htmlon]=@htmlon,
//                        [smileyoff]=@smileyoff,
//                        [bbcodeoff]=@bbcodeoff,
//                        [parseurloff]=@parseurloff WHERE [pid]=@pid
//
//
//                    ~
//
//                    CREATE PROCEDURE dnt_getnewtopics 
//                    @fidlist VARCHAR(500)
//                    AS
//                    IF @fidlist<>''
//                    BEGIN
//                    DECLARE @strSQL VARCHAR(5000)
//                    SET @strSQL = 'SELECT TOP 20   [dnt_posts1].[tid], [dnt_posts1].[title], [dnt_posts1].[poster], [dnt_posts1].[postdatetime], [dnt_posts1].[message],[dnt_forums].[name] FROM [dnt_posts1]  LEFT JOIN [dnt_forums] ON [dnt_posts1].[fid]=[dnt_forums].[fid] WHERE  [dnt_forums].[fid] NOT IN ('+@fidlist +')  AND [dnt_posts1].[layer]=0 ORDER BY [dnt_posts1].[pid] DESC' 
//                    END
//                    ELSE
//                    BEGIN
//                    SET @strSQL = 'SELECT TOP 20   [dnt_posts1].[tid], [dnt_posts1].[title], [dnt_posts1].[poster], [dnt_posts1].[postdatetime], [dnt_posts1].[message],[dnt_forums].[name] FROM [dnt_posts1]  LEFT JOIN [dnt_forums] ON [dnt_posts1].[fid]=[dnt_forums].[fid] WHERE [dnt_posts1].[layer]=0 ORDER BY [dnt_posts1].[pid] DESC'
//                    END
//                    EXEC(@strSQL)
//
//               ");

//            sb.Replace("\"", "'").Replace("dnt_posts1", BaseConfigs.GetTablePrefix + "posts" + tablelistmaxid);
//            sb.Replace("maxtablelistid", tablelistmaxid.ToString());
//            sb.Replace("dnt_createpost", BaseConfigs.GetTablePrefix + "createpost" + tablelistmaxid);
//            sb.Replace("dnt_getfirstpostid", BaseConfigs.GetTablePrefix + "getfirstpost" + tablelistmaxid + "id");
//            sb.Replace("dnt_getpostcount", BaseConfigs.GetTablePrefix + "getpost" + tablelistmaxid + "count");
//            sb.Replace("dnt_deletepostbypid", BaseConfigs.GetTablePrefix + "deletepost" + tablelistmaxid + "bypid");
//            sb.Replace("dnt_getposttree", BaseConfigs.GetTablePrefix + "getpost" + tablelistmaxid + "tree");
//            sb.Replace("dnt_getsinglepost", BaseConfigs.GetTablePrefix + "getsinglepost" + tablelistmaxid);
//            sb.Replace("dnt_updatepost", BaseConfigs.GetTablePrefix + "updatepost" + tablelistmaxid);
//            sb.Replace("dnt_getnewtopics", BaseConfigs.GetTablePrefix + "getnewtopics");
//            sb.Replace("dnt_", BaseConfigs.GetTablePrefix);
            
//            DatabaseProvider.GetInstance().CreatePostProcedure(sb.ToString());

           #endregion
        }

        public void UpdateMyTopic()
        {
            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "mytopics]";//清空我的主题表
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
            //重建我的主题表
            sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "mytopics]([uid], [tid], [dateline]) SELECT [posterid],[tid],[postdatetime] FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [posterid]<>-1";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void UpdateMyPost()
        {
            #region
            //string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "myposts]";//清空我的帖子表
            //DbHelper.ExecuteNonQuery(CommandType.Text, sql);
            ////重建我的帖子表
            //StringBuilder sqlstring = new StringBuilder();
            //sqlstring.Append("DECLARE @tableid int\r\n");
            //sqlstring.Append("DECLARE tables_cursor CURSOR FOR SELECT [id] FROM [" + BaseConfigs.GetTablePrefix + "tablelist]\r\n");
            //sqlstring.Append("OPEN tables_cursor\r\n");
            //sqlstring.Append("FETCH NEXT FROM tables_cursor INTO @tableid\r\n");
            //sqlstring.Append("WHILE @@FETCH_STATUS = 0\r\n");
            //sqlstring.Append("BEGIN\r\n");
            //sqlstring.Append("DECLARE @sql varchar(500)\r\n");
            //sqlstring.Append("SET @sql ='INSERT INTO [" + BaseConfigs.GetTablePrefix + "myposts]([uid], [tid], [pid], [dateline])\r\n");
            //sqlstring.Append("SELECT [posterid],[tid],[pid],[postdatetime] FROM [" + BaseConfigs.GetTablePrefix + "posts' + LTRIM(STR(@tableid)) + '] WHERE [posterid]<>-1'\r\n");
            //sqlstring.Append("EXEC(@sql)\r\n");
            //sqlstring.Append("FETCH NEXT FROM tables_cursor INTO @tableid\r\n");
            //sqlstring.Append("END\r\n");
            //sqlstring.Append("CLOSE tables_cursor\r\n");
            //sqlstring.Append("DEALLOCATE tables_cursor\r\n");


            //DbHelper.ExecuteNonQuery(CommandType.Text, sqlstring.ToString());

            #endregion

            string sql = "DELETE * FROM [" + BaseConfigs.GetTablePrefix + "myposts]";//清空我的帖子表
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);


            IDataReader DDR = DbHelper.ExecuteReader(CommandType.Text, "SELECT [id] from [" + BaseConfigs.GetTablePrefix + "tablelist]");
            while (DDR.Read())
            {
                string tempsql = "SELECT [posterid],[tid],[pid],[postdatetime] FROM [" + BaseConfigs.GetTablePrefix + "posts" + DDR["id"].ToString().Trim() + "] WHERE [posterid]<>-1";
                IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, tempsql);
                while (dr.Read())
                {
                    DbHelper.ExecuteNonQuery("INSERT INTO [" + BaseConfigs.GetTablePrefix + "myposts]([uid], [tid], [pid], [dateline]) values(" + dr["posterid"].ToString() + "," + dr["tid"].ToString() + "," + dr["pid"].ToString() + ",#" + dr["postdatetime"].ToString() + "#)");

                }
                dr.Close();

            }

            DDR.Close();
        }

        public string GetAllIdentifySql()
        {
            return "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "topicidentify]";
        }

        public DataTable GetAllIdentify()
        {
            string sql = GetAllIdentifySql();
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public bool UpdateIdentifyById(int id, string name)
        {
            DbParameter[] prams = 
			{
				DbHelper.MakeInParam("@identifyid", (DbType)OleDbType.Integer,4,id),
				DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar,50, name)
			};
            string sql = "SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "topicidentify] WHERE [name]=@name AND [identifyid]<>@identifyid";
            if (int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql,prams).ToString()) != 0)  //有相同的名称存在，更新失败
            {
                return false;
            }
            sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "topicidentify] SET [name]=@name WHERE [identifyid]=@identifyid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
            return true;
        }

        public bool AddIdentify(string name ,string filename)
        {
            DbParameter[] prams = 
			{
				DbHelper.MakeInParam("@name", (DbType)OleDbType.VarWChar,50, name),
				DbHelper.MakeInParam("@filename", (DbType)OleDbType.VarChar,50,filename),
			};
            string sql = "SELECT COUNT(1) FROM [" + BaseConfigs.GetTablePrefix + "topicidentify] WHERE [name]=@name";
            if (int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql,prams).ToString()) != 0)  //有相同的名称存在，插入失败
            {
                return false;
            }
            sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "topicidentify] ([name],[filename]) VALUES(@name,@filename)";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, prams);
            return true;
        }

        public void DeleteIdentify(string idlist)
        {
            string sql = "DELETE [" + BaseConfigs.GetTablePrefix + "topicidentify] WHERE [identifyid] IN (" + idlist + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        /// <summary>
        /// 获取非默认模板数
        /// </summary>
        /// <returns></returns>
        public int GetSpecifyForumTemplateCount()
        {
            string sql = "SELECT COUNT(*) FROM [" + BaseConfigs.GetTablePrefix + "forums] WHERE [templateid] <> 0 AND [templateid]<>" + GeneralConfigs.GetDefaultTemplateID();
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql).ToString());
        }

        public IDataReader GetAttachmentByUid(int uid, string extlist, int pageIndex, int pageSize)
        {
            DbParameter[] prams = 
			{
                DbHelper.MakeInParam("@uid", (DbType)OleDbType.Integer,4,uid),
                DbHelper.MakeInParam("@extlist ", (DbType)OleDbType.VarChar,100,extlist),
                DbHelper.MakeInParam("@pageindex", (DbType)OleDbType.Integer,4,pageIndex),
                DbHelper.MakeInParam("@pagesize", (DbType)OleDbType.Integer,4,pageSize)
			};

            string sql = "";
            if (pageIndex == 1)
            {
                sql = "select TOP @pagesize  *  from [" + BaseConfigs.GetTablePrefix + "myattachments] where  [extname] in (@extlist) and [uid]=@uid order by [aid] desc";
            }
            else
            {
                sql = "select TOP @pagesize *  from [" + BaseConfigs.GetTablePrefix + "myattachments] where [extname] in (@extlist)  and [aid] >(SELECT iff(ISNULL(MAX([aid])),0,MAX([aid])) FROM (SELECT TOP " + (pageIndex - 1) * pageSize + " [aid] FROM [" + BaseConfigs.GetTablePrefix + "myattachments]  where [extname] in (@extlist) ORDER BY aid) as A) and [uid]=@uid ORDER BY aid";
            }
            IDataReader idr = DbHelper.ExecuteReader(CommandType.Text, sql, prams);
            return idr;
         
        }

        public int GetUserAttachmentCount(int uid)
        {
            DbParameter[] prams = 
			{
              DbHelper.MakeInParam("@uid", (DbType)OleDbType.Integer,4,uid)
			};
            string sql = string.Format("SELECT COUNT(1) FROM [{0}] WHERE [UID]=@uid", BaseConfigs.GetTablePrefix + "myattachments");
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, prams).ToString());
        }

        public int GetUserAttachmentCount(int uid, string extlist)
        {

            DbParameter[] prams = 
			{
              DbHelper.MakeInParam("@uid", (DbType)OleDbType.Integer,4,uid)
			};
           
            string sql = string.Format("select count(1) from {0} where [extname] IN ("+extlist+") and [UID]=@uid", BaseConfigs.GetTablePrefix + "myattachments", BaseConfigs.GetTablePrefix);

            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql, prams).ToString());


        }


        public IDataReader GetAttachmentByUid(int uid, int pageIndex, int pageSize) 
        {

            DbParameter[] prams = 
			{
                DbHelper.MakeInParam("@uid", (DbType)OleDbType.Integer,4,uid),
                DbHelper.MakeInParam("@pageindex", (DbType)OleDbType.Integer,4,pageIndex),
                DbHelper.MakeInParam("@pagesize", (DbType)OleDbType.Integer,4,pageSize)
			};

            string sql = "";
            IDataReader idr;
            if (pageIndex == 1)
            {
                sql = "SELECT TOP "+pageSize+"  *  from [" + BaseConfigs.GetTablePrefix + "myattachments]  where  [uid]="+uid+"  order by [aid] desc";
                idr = DbHelper.ExecuteReader(CommandType.Text, sql);
            }
            else
            {
                sql = "SELECT TOP  @pagesize * FROM [" + BaseConfigs.GetTablePrefix + "myattachments] WHERE [aid] >(SELECT ISNULL(MAX([aid]),0) FROM (SELECT TOP " + (pageIndex - 1) * pageSize + " [aid] FROM [" + BaseConfigs.GetTablePrefix + "myattachments]  ORDER BY aid) as A) and [uid]=@uid  ORDER BY [aid]";
                idr = DbHelper.ExecuteReader(CommandType.Text, sql, prams);
            }

            return idr;
        }


        public void DelMyAttachmentByTid(string tidlist)
        {
           DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}myattachments] WHERE [tid] IN ({1})", BaseConfigs.GetTablePrefix, tidlist));
        
        }

        public void DelMyAttachmentByPid(string pidlist)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}myattachments] WHERE [pid] IN ({1})", BaseConfigs.GetTablePrefix, pidlist));

        }

        public void DelMyAttachmentByAid(string aidlist)
        {

            DbHelper.ExecuteNonQuery(CommandType.Text, string.Format("DELETE FROM [{0}myattachments] WHERE [aid] IN ({1})", BaseConfigs.GetTablePrefix, aidlist));
        }

        public IDataReader GetHotTagsListForForum(int count)
        {
            string sql = string.Format("SELECT TOP {0} * FROM [{1}tags] WHERE [fcount] > 0 AND [orderid] > -1 ORDER BY [orderid], [fcount] DESC", count, BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }
        /// <summary>
        /// 返回论坛Tag列表
        /// </summary>
        /// <param name="tagkey">查询关键字</param>
        /// <returns>返回Sql语句</returns>
        public string GetForumTagsSql(string tagname,int type)
        {
            //type 全部0 锁定1 开放2

            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "tags]";
            if (tagname != "")
            {
                sql += " WHERE [tagname] LIKE '%" + RegEsc(tagname) + "%'";
            }

            if (type == 1)
            {
                if (tagname!="")
                {
                    sql += " AND [orderid] < 0 ";
                }
                else
                {
                    sql += " WHERE [orderid] < 0 ";
                }
                
            }

            if (type == 2)
            {
                if (tagname != "")
                {
                    sql += " AND [orderid] >= 0";
                }
                else
                {
                    sql += " WHERE [orderid] >= 0 ";
                }
            }

            sql += " ORDER BY [fcount] DESC";

            return sql;
        }

        public string GetTopicNumber(string tagname,int from, int end, int type)
        { 
            //type 全部0 锁定1 开放

            string sql = "SELECT * FROM [" + BaseConfigs.GetTablePrefix + "tags]";

            if (tagname != "")
            {
                sql += " WHERE [tagname] LIKE '%" + RegEsc(tagname) + "%'";
            }


            if (type == 1)
            {
                if (tagname != "")
                {
                    sql += " AND [orderid] < 0 ";
                    sql += " AND [fcount] between " + from + " AND " + end;
                }
                else
                {
                    sql += " WHERE [orderid] < 0 ";
                    sql += " AND [fcount] between " + from + " AND " + end;
                }
            }

            if (type == 2)
            {
                if (tagname != "")
                {
                    sql += " AND [orderid] >= 0 ";
                    sql += " AND [fcount] between " + from + " AND " + end;
                }
                else
                {
                    sql += " WHERE [orderid] >= 0 ";
                    sql += " AND [fcount] between " + from + " AND " + end;
                }
            }

            
            sql += " ORDER BY [fcount] DESC";

            return sql;
        }


        public void UpdateForumTags(int tagid, int orderid, string color)
        {
            DbParameter[] prams = 
			{
				DbHelper.MakeInParam("@orderid", (DbType)OleDbType.Integer,4, orderid),
                DbHelper.MakeInParam("@color", (DbType)OleDbType.Char,6, color),
				DbHelper.MakeInParam("@tagid", (DbType)OleDbType.Integer,4,tagid)
			};
            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "tags] SET [orderid]=@orderid,[color]=@color WHERE [tagid]=@tagid";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql,prams);
        }
        /// <summary>
        /// 返回所有开放版块列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetOpenForumList()
        {
            string sql = "SELECT [parentid],[fid],[name] FROM (SELECT f.*,ff.[permuserlist],ff.[viewperm] FROM [" + BaseConfigs.GetTablePrefix + "forums] f "
                + "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forumfields] ff ON f.[fid]=ff.[fid]) f "
                + "WHERE [status]=1 AND ([permuserlist] IS NULL OR [permuserlist] LIKE '') AND ([viewperm] IS NULL OR [viewperm] LIKE '' "
                + "OR InStr(',7,',','+[viewperm]+',')<>0) ORDER BY [displayorder] ASC";
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        /// <summary>
        /// 返回所有开放版块列表Sql
        /// </summary>
        /// <returns>开放版块列表Sql</returns>
        public string GetOpenForumListSql()
        {
            string sql = "SELECT [fid],[name],[parentid] FROM (SELECT f.*,ff.[permuserlist],ff.[viewperm] FROM [" + BaseConfigs.GetTablePrefix + "forums] f "
                + "LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forumfields] ff ON f.[fid]=ff.[fid]) f "
                + "WHERE [status]=1 AND ([permuserlist] IS NULL OR [permuserlist] LIKE '') AND ([viewperm] IS NULL OR [viewperm] LIKE '' "
                + "OR InStr(',7,',','+[viewperm]+',')<>0) ORDER BY [displayorder] ASC";
            return sql;
        }

        /// <summary>
        /// 获取提取主题数据
        /// </summary>
        /// <param name="inForumList">指定版块列表</param>
        /// <param name="itemCount">显示数据条数</param>
        /// <param name="givenTids">指定主题</param>
        /// <param name="keyword">关键字</param>
        /// <param name="tags">主题标签</param>
        /// <param name="typesList">主题分类列表</param>
        /// <param name="digestList">精华列表</param>
        /// <param name="displayorderList">置顶列表</param>
        /// <param name="orderBy">排序方式</param>
        /// <returns></returns>
        public DataTable GetExtractTopic(string inForumList, int itemCount, string givenTids, string keyword,
            string tags, string typesList, string digestList, string displayorderList,string orderBy)
        {
            string sql = "SELECT TOP " + itemCount.ToString() + " * FROM [" + BaseConfigs.GetTablePrefix + "topics] WHERE [closed]=0";
            if (inForumList != "")
            {
                sql += " AND [fid] IN (" + inForumList + ")";
            }
            if (givenTids != "")
            {
                sql += " AND [tid] IN (" + givenTids + ")";
            }
            if (keyword != "")
            {
                keyword = keyword.ToLower().Replace(" and ", " ");  //将and替换为空格
                keyword = keyword.Replace(" or ", "|").Replace(" | ","|"); //将or替换为|
                keyword = keyword.Replace("*", "");
                keyword = keyword.Replace(" ", "%' AND [title] LIKE '%");
                keyword = keyword.Replace("|", "%' OR [title] LIKE '%");
                keyword = " AND ([title] LIKE '%" + keyword + "%')";
                sql += keyword;
            }
            if (tags != "")
            {
                tags = "'" + tags.Replace(" ", "','") + "'";
                sql += " AND [tid] IN (SELECT [tid] FROM [" + BaseConfigs.GetTablePrefix + "topictags]";
                sql += " WHERE [tagid] IN (SELECT [tagid] FROM [" + BaseConfigs.GetTablePrefix + "tags] WHERE [tagname] IN (" + tags + ")))";
            }
            if (typesList != "")
            {
                sql += " AND [typeid] IN (" + typesList + ")";
            }
            if (digestList != "")
            {
                sql += " AND [digest] IN (" + digestList + ")";
            }
            if (displayorderList != "")
            {
                sql += " AND [displayorder] IN (" + displayorderList + ")";
            }
            sql += " ORDER BY [" + orderBy + "] DESC";
            sql = "SELECT f.[name] AS [forumname],tt.[name] AS [topictype],t.* FROM (" + sql + ") t LEFT JOIN [" + BaseConfigs.GetTablePrefix + "forums] f ON t.[fid]=f.[fid] LEFT JOIN [" + BaseConfigs.GetTablePrefix + "topictypes] tt ON t.[typeid]=tt.[typeid]";
            DataTable dt = DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            return dt;
        }

        public IDataReader GetHotTopics(int count)
        {
            string commandText = string.Format("SELECT TOP {0} [views], [tid], [title] FROM [{1}topics] WHERE [displayorder]>=0 ORDER BY [views] DESC", count, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetHotReplyTopics(int count)
        {
            string commandText = string.Format("SELECT TOP {0} [replies], [tid], [title] FROM [{1}topics] WHERE [displayorder]>=0 ORDER BY [replies] DESC", count, BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteReader(CommandType.Text, commandText);
        }

        public IDataReader GetTopicBonusLogs(int tid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tid", (DbType)OleDbType.Integer, 4, tid);

            string sql = string.Format("SELECT [tid],[authorid],[answerid],[answername],[extid],SUM(bonus) AS [bonus] FROM [{0}bonuslog] WHERE [tid]=@tid GROUP BY [answerid],[authorid],[tid],[answername],[extid]", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }

        public IDataReader GetTopicBonusLogsByPost(int tid)
        {
            DbParameter parm = DbHelper.MakeInParam("@tid", (DbType)OleDbType.Integer, 4, tid);
            string sql = string.Format("SELECT [pid],[isbest],[bonus],[extid] FROM [{0}bonuslog] WHERE [tid]=@tid", BaseConfigs.GetTablePrefix);

            return DbHelper.ExecuteReader(CommandType.Text, sql, parm);
        }

        public DataTable GetAllOpenForum()
        {
            string sql = string.Format("SELECT * FROM [{0}forums] f LEFT JOIN [{0}forumfields] ff ON f.[fid] = ff.[fid] WHERE f.[autoclose]=0 AND ff.[password]='' AND ff.[redirect]=''", BaseConfigs.GetTablePrefix);
            return DbHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }

        public void UpdateLastPost(ForumInfo foruminfo, PostInfo postinfo)
        {
            DbParameter[] parms ={
                                     DbHelper.MakeInParam("@lasttid", (DbType)OleDbType.Integer, 4, postinfo.Tid),
                                     DbHelper.MakeInParam("@lasttitle", (DbType)OleDbType.VarWChar, 60, postinfo.Topictitle),
                                     DbHelper.MakeInParam("@lastpost", (DbType)OleDbType.DBTimeStamp, 8, postinfo.Postdatetime),
                                     DbHelper.MakeInParam("@lastposterid", (DbType)OleDbType.Integer, 4, postinfo.Posterid),
                                     DbHelper.MakeInParam("@lastposter", (DbType)OleDbType.VarWChar, 20, postinfo.Poster),
                                     DbHelper.MakeInParam("@fid", (DbType)OleDbType.Integer, 4, foruminfo.Fid)
                                 };
            string sql = string.Format("UPDATE [{0}forums] SET [lasttid] = @lasttid, [lasttitle] = @lasttitle, [lastpost] = @lastpost, [lastposterid] = @lastposterid, [lastposter] = @lastposter WHERE [fid] = @fid OR [fid] IN ({1})", BaseConfigs.GetTablePrefix, foruminfo.Parentidlist);
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);
        }

      
    }
}
#endif