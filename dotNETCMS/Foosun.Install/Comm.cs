using System;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALProfile;

namespace Foosun.Install
{
    public class Comm
    {
        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }


        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="connStr">数据库联接字符串</param>
        /// <param name="DatabaseName">数据库名称</param>
        /// <param name="Sql">Sql语句</param>
        public static void ExecuteSql(string connStr, string DatabaseName, string Sql)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(Sql, conn);

            conn.Open();
            if (DatabaseName != "##")
            {
                conn.ChangeDatabase(DatabaseName);
            }
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }


        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="connStr">数据库联接字符串</param>
        /// <param name="DatabaseName">数据库名称</param>
        /// <param name="Sql">Sql语句</param>
        public static void ExecuteSql(string connStr,string Sql)
        {
            DbHelper.ExecuteNonQuery(connStr, CommandType.Text, Sql, null);
        }


        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="Sql"></param>
        public static void ExecuteSql(string Sql)
        {
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
    }
}
