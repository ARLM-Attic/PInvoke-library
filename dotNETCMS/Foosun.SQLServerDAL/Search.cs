using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Search : DbBase, ISearch
    {
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <param name="RecordCount">记录总数</param>
        /// <param name="PageCount">页数</param>
        /// <param name="si">实体类</param>
        /// <returns>返回数据表</returns>
        public DataTable SearchGetPage(string DTable,int PageIndex, int PageSize, out int RecordCount, out int PageCount, Foosun.Model.Search si)
        {
            string allFields = "*";
            string tablesAndWhere = " " + Pre + "News Where isLock=0 And isRecyle=0";
            if (DTable != string.Empty)
            {
                tablesAndWhere = " " + DTable + " Where isLock=0";
            }
            string indexField = "ID";
            string orderField = "Order By ID Desc";

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Key", SqlDbType.NVarChar, 100);
            if (si.tags != null && si.tags != "")
            {
                param[0].Value = "%" + si.tags + "%";
                if (si.type == "tag")
                {
                    tablesAndWhere += " And Tags Like CONVERT(Nvarchar(100), @Key)";
                }
                else if (si.type == "edit")
                {
                    tablesAndWhere += " And editor Like CONVERT(Nvarchar(18), @Key)";
                }
                else if (si.type == "author")
                {
                    tablesAndWhere += " And author Like CONVERT(Nvarchar(100), @Key)";
                }
                else
                {
                    if (DTable != string.Empty)
                    {
                        tablesAndWhere += " And ((Title Like CONVERT(Nvarchar(100), @Key)) Or (Author Like CONVERT(Nvarchar(100), @Key)) Or (Souce Like CONVERT(Nvarchar(100), @Key)) Or (Tags Like CONVERT(Nvarchar(100), @Key)) Or (Content Like CONVERT(Nvarchar(100), @Key)))";
                    }
                    else
                    {
                        tablesAndWhere += " And ((NewsTitle Like CONVERT(Nvarchar(100), @Key)) Or (sNewsTitle Like CONVERT(Nvarchar(100), @Key)) Or (Author Like CONVERT(Nvarchar(100), @Key)) Or (Souce Like CONVERT(Nvarchar(100), @Key)) Or (Tags Like CONVERT(Nvarchar(100), @Key)) Or (Content Like CONVERT(Nvarchar(100), @Key)))";
                    }
                }
            }
            else
            {
                param[0].Value = "";
            }

            param[1] = new SqlParameter("@Pdate", SqlDbType.Int, 4);
            if (si.date != null && si.date != "" && si.date != "0")
            {
                param[1].Value = int.Parse(si.date);
                tablesAndWhere += " And DateDiff(Day,CreatTime ,getdate())<@Pdate";
            }
            else
            {
                param[1].Value = 0;
            }

            param[2] = new SqlParameter("@classid", SqlDbType.NVarChar, 12);
            if (si.classid != null && si.classid != "")
            {
                param[2].Value = si.classid;
                tablesAndWhere += " And ClassID=@classid";
            }
            else
            {
                param[2].Value = "";
            }

            return DbHelper.ExecutePage(allFields, tablesAndWhere, indexField, orderField, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }

        /// <summary>
        /// 取得栏目参数中的新闻保存路径
        /// </summary>
        /// <param name="ClassID">栏目编号</param>
        /// <returns>返回新闻保存路径</returns>
        public string getSaveClassframe(string ClassID)
        {
            string path = "";
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string Sql = "Select SaveClassframe,SavePath From " + Pre + "news_Class Where ClassID=@ClassID";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            if (dr.Read())
            {
                path = dr.GetString(1) + "/" + dr.GetString(0);

            }
            dr.Close(); 
            dr.Dispose();
            return path;
        }


    }
}
