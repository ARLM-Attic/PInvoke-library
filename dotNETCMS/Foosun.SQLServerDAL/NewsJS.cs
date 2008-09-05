using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.Global;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class NewsJS : DbBase, INewsJS
    {
        public IList<NewsJSInfo> GetPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, int JsType)
        {
            IList<NewsJSInfo> njs = new List<NewsJSInfo>();
            string SqlWhere = Pre + "news_JS a left join " + Pre + "News_JSFile b on a.jsid=b.jsid where a.SiteID='" + Current.SiteID + "'";
            if (JsType >= 0)
                SqlWhere += " and jsType=" + JsType;
            IDataReader rd = DbHelper.ExecuteReaderPage(DBConfig.CmsConString, "a.id,a.JSName,a.jsType,a.CreatTime,a.jsNum,count(b.id)", SqlWhere, "a.id", "group by a.id,a.jsType,a.JSName,a.jsNum,a.CreatTime", "order by a.id desc", PageIndex, PageSize, out RecordCount, out PageCount, null);
            while (rd.Read())
            {
                NewsJSInfo info = new NewsJSInfo();
                info.Id = rd.GetInt32(0);
                info.JSName = rd.GetString(1);
                info.jsType = (int)rd.GetByte(2);
                info.CreatTime = rd.GetDateTime(3);
                info.jsNum = rd.GetInt32(4);
                info.ActualNum = rd.GetInt32(5);
                njs.Add(info);
            }
            //rd.Close();
            return njs;
        }
        public void Delete(string id)
        {
            if (id.IndexOf("'") >= 0)
                throw new Exception("编号中有非法字符'");
            string Sql = "delete from " + Pre + "news_JS where SiteID='" + Current.SiteID + "' and id in (" + id + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable GetJSFilePage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, int id)
        {
            return DbHelper.ExecutePage("a.ID,a.Njf_title", Pre + "News_JSFile a inner join " + Pre + "News_JS b on a.JsID=b.JsID where a.SiteID='" + Current.SiteID + "' and b.id=" + id, "a.id", "order by a.id", PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        public void RemoveNews(int id)
        {
            string Sql = "delete from " + Pre + "News_JSFile where SiteID='" + Current.SiteID + "' and ID=" + id;
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public NewsJSInfo GetSingle(int id)
        {
            string Sql = "select JsID,jsType,JSName,JsTempletID,jsNum,jsLenTitle,jsLenNavi,jsLenContent,jsContent,SiteID,jsColsNum,jsfilename,jssavepath from " + Pre + "News_JS where SiteID='" + Current.SiteID + "' and id=" + id;
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            if (rd.Read())
            {
                NewsJSInfo js = new NewsJSInfo();
                js.Id = id;
                js.JsID = rd.GetString(0);
                js.jsType = (int)rd.GetByte(1);
                js.JSName = rd.GetString(2);
                js.JsTempletID = rd.GetString(3);
                if (rd.IsDBNull(4)) { js.jsNum = 0; } else { js.jsNum = rd.GetInt32(4); }
                if (rd.IsDBNull(5)) { js.jsLenTitle = 0; } else { js.jsLenTitle = rd.GetInt32(5); }
                if (rd.IsDBNull(6)) { js.jsLenNavi = 0; } else { js.jsLenNavi = rd.GetInt32(6); }
                if (rd.IsDBNull(7)) { js.jsLenContent = 0; } else { js.jsLenContent = rd.GetInt32(7); }
                if (rd.IsDBNull(8)) { js.jsContent = ""; } else { js.jsContent = rd.GetString(8); }
                js.SiteID = rd.GetString(9);
                if (rd.IsDBNull(10)) { js.jsColsNum = 0; } else { js.jsColsNum = rd.GetInt32(10); }
                js.jsfilename = rd.GetString(11);
                js.jssavepath = rd.GetString(12);
                rd.Close();
                return js;
            }
            else
            {
                if (!rd.IsClosed)
                    rd.Close();
                throw new Exception("未找到相关的JS记录!");
            }
        }
        public NewsJSInfo GetSingle(string JsID)
        {
            string Sql = "select JsID,jsType,JSName,JsTempletID,jsNum,jsLenTitle,jsLenNavi,jsLenContent,jsContent,SiteID,jsColsNum,jsfilename,jssavepath,ID from " + Pre + "News_JS where SiteID='" + Current.SiteID + "' and JsID='" + JsID+"'";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, null);
            if (rd.Read())
            {
                NewsJSInfo js = new NewsJSInfo();
                js.Id = Convert.ToInt32(rd["ID"]);
                js.JsID = rd.GetString(0);
                js.jsType = (int)rd.GetByte(1);
                js.JSName = rd.GetString(2);
                js.JsTempletID = rd.GetString(3);
                if (rd.IsDBNull(4)) { js.jsNum = 0; } else { js.jsNum = rd.GetInt32(4); }
                if (rd.IsDBNull(5)) { js.jsLenTitle = 0; } else { js.jsLenTitle = rd.GetInt32(5); }
                if (rd.IsDBNull(6)) { js.jsLenNavi = 0; } else { js.jsLenNavi = rd.GetInt32(6); }
                if (rd.IsDBNull(7)) { js.jsLenContent = 0; } else { js.jsLenContent = rd.GetInt32(7); }
                if (rd.IsDBNull(8)) { js.jsContent = ""; } else { js.jsContent = rd.GetString(8); }
                js.SiteID = rd.GetString(9);
                if (rd.IsDBNull(10)) { js.jsColsNum = 0; } else { js.jsColsNum = rd.GetInt32(10); }
                js.jsfilename = rd.GetString(11);
                js.jssavepath = rd.GetString(12);
                rd.Close();
                return js;
            }
            else
            {
                if (!rd.IsClosed)
                    rd.Close();
                throw new Exception("未找到相关的JS记录!");
            }
        }
        public void Update(NewsJSInfo info)
        {
            Edit(info);
        }
        public string Add(NewsJSInfo info)
        {
            return Edit(info);
        }
        private string Edit(NewsJSInfo info)
        {
            SqlConnection cn = new SqlConnection(DBConfig.CmsConString);
            try
            {
                string RetVal = info.JsID;
                string Sql = "select JSName,jsfilename,jssavepath from " + Pre + "News_JS where SiteID='" + Current.SiteID + "' and (JSName=@JSName or (jsfilename=@jsfilename and jssavepath=@jssavepath))";
                if (info.Id > 0)
                    Sql += " and Id<>" + info.Id;
                SqlParameter[] parm = new SqlParameter[3];
                parm[0] = new SqlParameter("@JSName", SqlDbType.NVarChar, 50);
                parm[0].Value = info.JSName;
                parm[1] = new SqlParameter("@jsfilename", SqlDbType.NVarChar, 50);
                parm[1].Value = info.jsfilename;
                parm[2] = new SqlParameter("@jssavepath", SqlDbType.NVarChar, 200);
                parm[2].Value = info.jssavepath;
                IDataReader rd = DbHelper.ExecuteReader(cn, CommandType.Text, Sql, parm);
                if (rd.Read())
                {
                    string nm = rd.GetString(0);
                    rd.Close();
                    if (nm.Equals(info.JSName))
                    {
                        throw new Exception("JS名称不能重复,该名称已经存在!");
                    }
                    else
                    {
                        rd.Close();
                        throw new Exception("已存在相同的路径和文件名的JS!");
                    }
                }
                if (!rd.IsClosed)
                    rd.Close();
                if (info.Id > 0)
                {
                    Sql = "update " + Pre + "News_JS set JSName=@JSName,JsTempletID=@JsTempletID,jsNum=@jsNum,jsLenTitle=@jsLenTitle,jsLenNavi=@jsLenNavi,";
                    Sql += "jsLenContent=@jsLenContent,jsContent=@jsContent,jsColsNum=@jsColsNum,jsfilename=@jsfilename,jssavepath=@jssavepath where SiteID=@SiteID and Id=" + info.Id;
                }
                else
                {
                    string jsid = Foosun.Common.Rand.Number(12);
                    while (Convert.ToInt32(DbHelper.ExecuteScalar(cn, CommandType.Text, "select count(*) from " + Pre + "News_JS where JsID='" + jsid + "'", null)) > 0)
                    {
                        jsid = Foosun.Common.Rand.Number(12, true);
                    }
                    RetVal = jsid;
                    Sql = "insert into " + Pre + "News_JS (JsID,jsType,JSName,JsTempletID,jsNum,jsLenTitle,jsLenNavi,jsLenContent,jsContent,SiteID,jsColsNum,CreatTime,jsfilename,jssavepath) ";
                    Sql += "values ('" + jsid + "'," + info.jsType + ",@JSName,@JsTempletID,@jsNum,@jsLenTitle,@jsLenNavi,@jsLenContent,@jsContent,@SiteID,@jsColsNum,'" + DateTime.Now + "',@jsfilename,@jssavepath)";
                }
                SqlParameter[] param = new SqlParameter[11];
                param[0] = new SqlParameter("@JSName", SqlDbType.NVarChar, 50);
                param[0].Value = info.JSName;
                param[1] = new SqlParameter("@JsTempletID", SqlDbType.NVarChar, 12);
                param[1].Value = info.JsTempletID;
                param[2] = new SqlParameter("@jsNum", SqlDbType.Int);
                param[2].Value = info.jsNum;
                param[3] = new SqlParameter("@jsLenTitle", SqlDbType.Int);
                param[3].Value = info.jsLenTitle < 0 ? DBNull.Value : (object)info.jsLenTitle;
                param[4] = new SqlParameter("@jsLenNavi", SqlDbType.Int);
                param[4].Value = info.jsLenNavi < 0 ? DBNull.Value : (object)info.jsLenNavi;
                param[5] = new SqlParameter("@jsLenContent", SqlDbType.Int);
                param[5].Value = info.jsLenContent < 0 ? DBNull.Value : (object)info.jsLenContent;
                param[6] = new SqlParameter("@jsContent", SqlDbType.NText);
                param[6].Value = info.jsContent.Equals("") ? DBNull.Value : (object)info.jsContent;
                param[7] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
                param[7].Value = Current.SiteID;
                param[8] = new SqlParameter("@jsColsNum", SqlDbType.Int);
                param[8].Value = info.jsColsNum < 0 ? DBNull.Value : (object)info.jsColsNum;
                param[9] = new SqlParameter("@jsfilename", SqlDbType.NVarChar, 50);
                param[9].Value = info.jsfilename;
                param[10] = new SqlParameter("@jssavepath", SqlDbType.NVarChar, 200);
                param[10].Value = info.jssavepath;
                DbHelper.ExecuteNonQuery(cn, CommandType.Text, Sql, param);
                return RetVal;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
            }
        }
        string INewsJS.GetJsTmpContent(string jstmpid)
        {
            string Sql = "select JSTContent from "+ Pre +"news_JSTemplet where TempletID=@TempletID";
            SqlParameter param = new SqlParameter("@TempletID", jstmpid);
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        DataTable INewsJS.GetJSFiles(string jsid)
        {
            string Sql = "select NewsId from " + Pre + "news_JSFile where JsID=@JsID";
            SqlParameter param = new SqlParameter("@JsID", jsid);
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }

        //<--修改者：吴静岚 时间：2008-06-24
        /// <summary>
        /// 获取自由js调用新闻条数
        /// </summary>
        /// <param name="jsid">JS编号</param>
        /// <returns>查询结果</returns>
        DataTable INewsJS.GetJSNum(string jsid)
        {
            string Sql = "select jsnum from " + Pre + "news_JS where JsID=@JsID";
            SqlParameter param = new SqlParameter("@JsID", jsid);
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        //wjl-->
    }
}
