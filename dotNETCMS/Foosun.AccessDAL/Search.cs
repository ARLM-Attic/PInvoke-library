using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
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

            if (si.tags != null && si.tags != "")
            {
                string key = "'%" + si.tags + "%'";
                if (si.type == "tag")
                {
                    tablesAndWhere += " And Tags Like "+key+"";
                }
                else if (si.type == "edit")
                {
                    tablesAndWhere += " And editor Like "+key+"";
                }
                else if (si.type == "author")
                {
                    tablesAndWhere += " And author Like "+key+"";
                }
                else
                {
                    if (DTable != string.Empty)
                    {
                        tablesAndWhere += " And ((Title Like "+key+") Or (Author Like "+key+") Or (Souce Like "+key+") Or (Tags Like "+key+") Or (Content Like "+key+"))";
                    }
                    else
                    {
                        tablesAndWhere += " And ((NewsTitle Like "+key+") Or (sNewsTitle Like "+key+") Or (Author Like "+key+") Or (Souce Like "+key+") Or (Tags Like "+key+") Or (Content Like "+key+"))";
                    }
                }
            }


            if (si.date != null && si.date != "" && si.date != "0")
            {
                tablesAndWhere += " And DateDiff('d',CreatTime ,Now())<#"+si.date+"#";
            }


            if (si.classid != null && si.classid != "")
            {
                tablesAndWhere += " And ClassID='" + si.classid + "'";
            }


            return DbHelper.ExecutePage(allFields, tablesAndWhere, indexField, orderField, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }

        /// <summary>
        /// 取得栏目参数中的新闻保存路径
        /// </summary>
        /// <param name="ClassID">栏目编号</param>
        /// <returns>返回新闻保存路径</returns>
        public string getSaveClassframe(string ClassID)
        {
            string path = "";
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Sql = "Select SaveClassframe,SavePath From " + Pre + "news_Class Where ClassID=@ClassID";
            IDataReader dr = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            if (dr.Read())
            {
                path = dr.GetString(1) + "/" + dr.GetString(0);

            }
            dr.Close(); dr.Dispose();
            return path;
        }


    }
}
