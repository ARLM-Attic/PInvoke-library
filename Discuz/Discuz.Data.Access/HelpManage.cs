#if NET1
#else
using System;
using System.Collections.Generic;
using System.Text;
using Discuz.Config;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace Discuz.Data.Access
{
    public partial class DataProvider : IDataProvider
    {
        //取得分类
        public IDataReader GetHelpList(int id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id", (DbType)OleDbType.Integer, 4, id),
                                        
                                    };
            string sql = "SELECT [id],[title],[message],[pid],[orderby] FROM [" + BaseConfigs.GetTablePrefix + "help] WHERE [pid]=@id OR [id]=@id ORDER BY [pid] ASC, [orderby] ASC";

            return DbHelper.ExecuteReader(CommandType.Text, sql,parms);
    
        
        }


        public IDataReader ShowHelp(int id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@id", (DbType)OleDbType.Integer, 4, id),
                                        
                                    };
            string sql = "SELECT [title],[message],[pid],[orderby] FROM [" + BaseConfigs.GetTablePrefix + "help] WHERE [id]=@id";
            return DbHelper.ExecuteReader(CommandType.Text, sql,parms);

        }


        public IDataReader GetHelpClass()
        {

            string sql = "SELECT [id] FROM [" + BaseConfigs.GetTablePrefix + "help] WHERE [pid]=0 ORDER BY [orderby] ASC";
            return DbHelper.ExecuteReader(CommandType.Text, sql);
        }
        


        public void AddHelp(string title,string message,int pid,int orderby)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@title", (DbType)OleDbType.Char, 100, title),
                                        DbHelper.MakeInParam("@message", (DbType)OleDbType.VarWChar,0,message),
                                        DbHelper.MakeInParam("@pid", (DbType)OleDbType.Integer,4, pid),
                                        DbHelper.MakeInParam("@orderby", (DbType)OleDbType.Integer, 4, orderby)
                                        
                                    };
            string sql = "INSERT INTO [" + BaseConfigs.GetTablePrefix + "help]([title],[message],[pid],[orderby]) VALUES(@title,@message,@pid,@orderby)";
        DbHelper.ExecuteNonQuery(CommandType.Text,sql,parms);
        }

        public void DelHelp(string idlist)
        {
            string sql = "DELETE FROM [" + BaseConfigs.GetTablePrefix + "help] WHERE [id] IN ("+idlist+") OR [pid] IN ("+idlist+")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql);
        }

        public void ModHelp(int id,string title,string message,int pid,int orderby)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@title", (DbType)OleDbType.Char, 100, title),
                                        DbHelper.MakeInParam("@message", (DbType)OleDbType.VarWChar,0,message),
                                        DbHelper.MakeInParam("@pid", (DbType)OleDbType.Integer,4, pid),
                                        DbHelper.MakeInParam("@orderby", (DbType)OleDbType.Integer, 4, orderby),
                                        DbHelper.MakeInParam("@id", (DbType)OleDbType.Integer, 4, id)
                                        
                                    };

            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "help] SET [title]=@title,[message]=@message,[pid]=@pid,[orderby]=@orderby WHERE [id]=@id";
            DbHelper.ExecuteNonQuery(CommandType.Text,sql,parms);
        
        }

       

        public int HelpCount()
        {
            string sql = "SELECT COUNT(*) FROM [" + BaseConfigs.GetTablePrefix + "help]";
            return int.Parse(DbHelper.ExecuteScalar(CommandType.Text, sql).ToString());
        
        }

        public string BindHelpType()

        {
            string sql = "SELECT [id],[title] FROM [" + BaseConfigs.GetTablePrefix + "help] WHERE [pid]=0 ORDER BY [orderby] ASC";
            return sql;
        
        }

        public void UpOrder(string orderby, string id)
        {
            DbParameter[] parms = {
                                        DbHelper.MakeInParam("@orderby", (DbType)OleDbType.Char, 100, orderby),
                                        DbHelper.MakeInParam("@id", (DbType)OleDbType.VarChar, 100,id),
                                        
                                        
                                    };

            string sql = "UPDATE [" + BaseConfigs.GetTablePrefix + "help] SET [ORDERBY]=@orderby  Where id=@id";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, parms);

        }

    }
}
#endif