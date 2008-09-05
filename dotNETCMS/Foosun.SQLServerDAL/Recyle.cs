using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.SQLServerDAL
{
    public class Recyle : DbBase, IRecyle
    {
        private string SiteID = Foosun.Global.Current.SiteID;
        public Recyle()
        {
        }

        public DataTable getList(string type)
        {
            string str_Sql = GetSql(type);
            string str_TName = Pre + "news";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (type == "NList")
            {
                DataTable dv = getNewsTable();
                if (dv != null)
                {
                    for (int i = 0; i < dv.Rows.Count; i++)
                    {
                        string str_TempName = dv.Rows[i][0].ToString();
                        if (str_TempName.ToUpper() != str_TName.ToUpper())
                        {
                            str_Sql = "Select Id,NewsID,NewsTitle From " + dv.Rows[i][0].ToString() + " Where isRecyle=1 and " +
                                       "SiteID='" + SiteID + "'";
                            DataTable dc = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
                            dt.Merge(dc);
                        }
                    }
                }
            }
            return dt;
        }

        protected string GetSql(string type)
        {
            string str_Sql = "";
            switch (type)
            {
                case "NCList":      //新闻栏目列表
                    str_Sql = "Select Id,ClassID,ClassCName From " + Pre + "news_Class Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "NList":       //新闻列表
                    str_Sql = "Select Id,NewsID,NewsTitle From " + Pre + "news Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "CList":       //频道列表
                    str_Sql = "Select Id,ChannelID,CName From " + Pre + "news_site Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "SList":       //专题列表
                    str_Sql = "Select Id,SpecialID,SpecialCName From " + Pre + "news_special Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "LCList":      //标签栏目列表
                    str_Sql = "Select Id,ClassID,ClassName From " + Pre + "sys_LabelClass Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "LList":       //标签列表
                    str_Sql = "Select Id,LabelID,Label_Name From " + Pre + "sys_Label Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "StCList":     //样式栏目列表
                    str_Sql = "Select Id,ClassID,Sname From " + Pre + "sys_styleclass Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "StList":       //样式列表
                    str_Sql = "Select Id,styleID,StyleName From " + Pre + "sys_LabelStyle Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
                case "PSFList":     //PSF结点列表
                    str_Sql = "Select Id,psfID,psfName From " + Pre + "sys_PSF Where isRecyle=1 and SiteID='" + SiteID + "'";
                    break;
            }
            return str_Sql;
        }

        public void RallNCList()
        {
            string str_Sql = "Update " + Pre + "news_Class Set isRecyle=0 Where SiteID='" + SiteID + "' And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        public void RallNList(string classid)
        {
            DataTable dt = getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    string str_Sql = "Select NewsID,ClassID,NewsProperty,NewsType,DataLib From " + tbname + " Where isRecyle=1 And SiteID='" + SiteID + "' And isRecyle=1";
                    DataTable dv = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
                    if (dv != null)
                    {
                        for (int j = 0; j < dv.Rows.Count; j++)
                        {
                            string str_Sql1 = "";
                            string newsclassid = dv.Rows[j]["ClassID"].ToString();
                            string newsid = dv.Rows[j]["NewsID"].ToString();

                            string Prot = dv.Rows[j]["NewsProperty"].ToString();
                            string[] getProt = Prot.Split(',');
                            int isRec = int.Parse(getProt[0]);
                            int isMarquee = int.Parse(getProt[1]);
                            int isHOT = int.Parse(getProt[2]);
                            int isFilt = int.Parse(getProt[3]);
                            int isTT = int.Parse(getProt[4]);
                            int isAnnouce = int.Parse(getProt[5]);
                            int isWap = int.Parse(getProt[6]);
                            int isJC = int.Parse(getProt[7]);
                            string sTF = "select id from " + Pre + "news_temp where NewsID='" + newsid + "' and DataLib='" + dv.Rows[j]["DataLib"].ToString() + "' order by id desc";
                            DataTable dtTF = DbHelper.ExecuteTable(CommandType.Text, sTF, null);
                            if (dtTF != null)
                            {
                                if (dtTF.Rows.Count == 0)
                                {
                                    int NewsType = int.Parse(dv.Rows[j]["NewsType"].ToString());
                                    DateTime CreatTime = DateTime.Parse(dv.Rows[j]["CreatTime"].ToString());
                                    int isConstr = 0;
                                    if (Foosun.Common.Input.IsInteger(dv.Rows[j]["isConstr"].ToString()))
                                    {
                                        isConstr = int.Parse(dv.Rows[j]["isConstr"].ToString());
                                    }
                                    string ClassID = "";
                                    if (checkParIsDel(newsclassid, "news_Class") == true) { ClassID = classid; }
                                    else { ClassID = dv.Rows[j]["ClassID"].ToString(); }
                                    string Sql = "insert into " + Pre + "news_temp (NewsID,DataLib,NewsType,CreatTime,IsRec,isHot,isTT,isAnnounce,isMarQuee,isConstr,isJC,isWap,isFilt,ClassID)";
                                    Sql += " VALUES ('" + newsid + "','" + dv.Rows[j]["DataLib"].ToString() + "'," + NewsType + ",'" + CreatTime + "'," + isRec + "," + isHOT + "," + isTT + "," + isAnnouce + "," + isMarquee + "," + isConstr + "," + isJC + "," + isWap + "," + isFilt + ",'" + ClassID + "')";
                                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
                                }
                                dtTF.Clear(); dtTF.Dispose();
                            }
                        //删除多余的信息
                        RandFirst:
                            string SqlTf = "select ID from " + Pre + "news_temp order by id desc";
                            DataTable rTF = DbHelper.ExecuteTable(CommandType.Text, SqlTf, null);
                            if (rTF.Rows.Count > 3000)
                            {
                                string SQL1 = "delete from " + Pre + "news_temp where id=(select top 1 id from " + Pre + "news_temp order by id asc)";
                                DbHelper.ExecuteNonQuery(CommandType.Text, SQL1, null);
                                rTF.Clear(); rTF.Dispose();
                                goto RandFirst;
                            }




                            if (checkParIsDel(newsclassid, "news_Class") == true)
                            {
                                str_Sql1 = "Update " + tbname + " Set isRecyle=0,ClassID='" + classid + "' Where SiteID='" + SiteID + "' and NewsID='" + newsid + "'";
                            }
                            else
                            {

                                str_Sql1 = "Update " + tbname + " Set isRecyle=0 Where SiteID='" + SiteID + "' and NewsID='" + newsid + "'";
                            }
                            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
                            raDComment(newsid, false);
                        }
                        dv.Clear();
                        dv.Dispose();
                    }
                }
                dt.Clear();
                dt.Dispose();
            }
        }

        public void RallCList()
        {
            string str_Sql = "Update " + Pre + "news_site Set isRecyle=0 Where SiteID='" + SiteID + "'  And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void RallSList()
        {
            string str_Sql = "Update " + Pre + "news_special Set isRecyle=0 Where SiteID='" + SiteID + "'  And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void RallLCList()
        {
            string str_Sql = "Update " + Pre + "sys_LabelClass Set isRecyle=0 Where SiteID='" + SiteID + "'  And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void RallLList(string classid)
        {
            string str_Sql = "Select LabelID,ClassID From " + Pre + "sys_Label Where isRecyle=1 And SiteID='" + SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (dt != null)
            {
                string str_Sql1 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string labelclassid = dt.Rows[i]["ClassID"].ToString();
                    string labelid = dt.Rows[i]["LabelID"].ToString();

                    if (checkParIsDel(labelclassid, "sys_LabelClass") == true)
                        str_Sql1 = "Update " + Pre + "sys_Label Set isRecyle=0,ClassID='" + classid + "' Where SiteID='" + SiteID + "' And LabelID='" + labelid + "'";
                    else
                        str_Sql1 = "Update " + Pre + "sys_Label Set isRecyle=0 Where SiteID='" + SiteID + "' And LabelID='" + labelid + "'";

                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
                }
                dt.Clear();
                dt.Dispose();
            }
        }
        public void RallStCList()
        {
            string str_Sql = "Update " + Pre + "sys_styleclass Set isRecyle=0 Where SiteID='" + SiteID + "' And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void RallStList(string classid)
        {
            string str_Sql = "Select styleID,ClassID From " + Pre + "sys_styleclass Where isRecyle=1 And SiteID='" + SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (dt != null)
            {
                string str_Sql1 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string styleclassid = dt.Rows[i]["ClassID"].ToString();
                    string styleid = dt.Rows[i]["styleID"].ToString();

                    if (checkParIsDel(styleclassid, "sys_styleclass") == true)
                        str_Sql1 = "Update " + Pre + "sys_LabelStyle Set isRecyle=0,ClassID='" + classid + "' Where SiteID='" + SiteID + "' And styleID='" + styleid + "'";
                    else
                        str_Sql1 = "Update " + Pre + "sys_LabelStyle Set isRecyle=0 Where SiteID='" + SiteID + "' And styleID='" + styleid + "'";

                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
                }
                dt.Clear();
                dt.Dispose();
            }
        }
        public void RallPSFList()
        {
            string str_Sql = "Update " + Pre + "sys_PSF Set isRecyle=0 Where SiteID='" + SiteID + "' And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void DallNCList()
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                DataTable dt = getNewsTable();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tbname = dt.Rows[i][0].ToString();
                        string str_Sql = "Delete From " + tbname + " Where ClassID In (Select ClassID " +
                                         "From " + Pre + "news_Class Where SiteID='" + SiteID + "' And isRecyle=1)";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                        string str_Sql1 = "Delete From " + Pre + "news_Class Where SiteID='" + SiteID + "' And isRecyle=1";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                    }
                    tran.Commit();
                    dt.Clear(); dt.Dispose();
                }
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void DallNList()
        {
            DataTable dt = getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    string str_Sql = "Delete From " + tbname + " Where SiteID='" + SiteID + "' And isRecyle=1";
                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
                }
                dt.Clear(); dt.Dispose();
            }
        }
        public void DallCList()
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                DataTable dt = getNewsTable();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tbname = dt.Rows[i][0].ToString();

                        string str_Sql = "Delete From " + Pre + "API_commentary Where InfoID In(" +
                                         "Select NewsID From " + tbname + " Where SiteID In(" +
                                         "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And SiteID='" + SiteID + "'))";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);

                        string str_Sql1 = "Delete From " + tbname + " Where SiteID In(" +
                                          "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And SiteID='" + SiteID + "')";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);

                        string str_Sql2 = "Delete From " + Pre + "news_Class Where SiteID In(" +
                                          "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And SiteID='" + SiteID + "')";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql2, null);

                        string str_Sql3 = "Delete From " + Pre + "news_special Where SiteID In(" +
                                          "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And SiteID='" + SiteID + "')";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql3, null);

                        string str_Sql4 = "Delete From " + Pre + "news_site Where isRecyle=1 And SiteID='" + SiteID + "'";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql4, null);

                    }
                    tran.Commit();
                }
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void DallSList()
        {
            string Sql = "Delete From " + Pre + "special_news Where SpecialID In (Select SpecialID From " + Pre + "news_special Where isRecyle=1 And SiteID='" + SiteID + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            string str_Sql = "Delete From " + Pre + "news_special Where isRecyle=1 And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void DallLCList()
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_Sql = "Delete From " + Pre + "sys_Label Where ClassID In (Select ClassID " +
                                 "From " + Pre + "sys_LabelClass Where SiteID='" + SiteID + "' And isRecyle=1)";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                string str_Sql1 = "Delete From " + Pre + "sys_LabelClass Where SiteID='" + SiteID + "' And isRecyle=1";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                tran.Commit();
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void DallLList()
        {
            string str_Sql = "Delete From " + Pre + "sys_Label Where isRecyle=1 And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void DallStCList()
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_Sql = "Delete From " + Pre + "sys_LabelStyle Where ClassID In (Select" +
                                 " ClassID From " + Pre + "sys_styleclass Where SiteID='" + SiteID + "' And isRecyle=1)";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                string str_Sql1 = "Delete From " + Pre + "sys_styleclass Where SiteID='" + SiteID + "' And isRecyle=1";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                tran.Commit();
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void DallStList()
        {
            string str_Sql = "Delete From " + Pre + "sys_LabelStyle Where SiteID='" + SiteID + "' And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void DallPSFList()
        {
            string str_Sql = "Delete From " + Pre + "sys_PSF Where isRecyle=1 And SiteID='" + SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void PRNCList(string idstr)
        {
            string[] arr_id = idstr.Split(',');
            for (int i = 0; i < arr_id.Length; i++)
            {
                string classid = arr_id[i].ToString().Replace("'", "");
                string str_Sql = "";

                if (checkParIsDel(classid, "news_Class") == true)
                    str_Sql = "Update " + Pre + "news_Class Set isRecyle=0,ParentID='0' Where SiteID='" + SiteID + "' and ClassID='" + classid + "'";
                else
                    str_Sql = "Update " + Pre + "news_Class Set isRecyle=0 Where SiteID='" + SiteID + "' and ClassID='" + classid + "'";

                DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            }
        }
        public void PRNList(string classid, string idstr)
        {
            DataTable dt = getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    string[] arr_id = idstr.Split(',');
                    for (int j = 0; j < arr_id.Length; j++)
                    {
                        string tempNewsID = arr_id[j].ToString().Replace("'", "");
                        string str_Sql = "Select NewsID,ClassID From " + tbname + " Where isRecyle=1 And " +
                                         "SiteID='" + SiteID + "' And NewsID='" + tempNewsID + "'";
                        DataTable dv = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
                        if (dv != null)
                        {
                            if (dv.Rows.Count > 0)
                            {
                                string str_Sql1 = "";
                                string newsclassid = dv.Rows[0][1].ToString();
                                string newsid = dv.Rows[0][0].ToString();

                                if (checkParIsDel(newsclassid, "news_Class") == true)
                                    str_Sql1 = "Update " + tbname + " Set isRecyle=0,ClassID='" + classid + "' Where SiteID='" + SiteID + "' and NewsID='" + newsid + "'";
                                else
                                    str_Sql1 = "Update " + tbname + " Set isRecyle=0 Where SiteID='" + SiteID + "' and NewsID='" + newsid + "'";

                                DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
                            }
                            dv.Clear(); dv.Dispose();
                        }
                    }
                }
                dt.Clear(); dt.Dispose();
            }
        }
        public void PRCList(string idstr)
        {
            string str_Sql = "Update " + Pre + "news_site Set isRecyle=0 Where SiteID='" + SiteID + "' And ChannelID in(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void PRSList(string idstr)
        {
            string[] arr_id = idstr.Split(',');
            string str_Sql = "";
            for (int i = 0; i < arr_id.Length; i++)
            {
                string Id = arr_id[i].Replace("'", "");
                if (getParentlockTf(Id, "news_special", "SpecialID") == false)
                    str_Sql = "Update " + Pre + "news_special Set isRecyle=0 Where SiteID='" + SiteID + "' And SpecialID='" + Id + "'";
                else
                    str_Sql = "Update " + Pre + "news_special Set isRecyle=0,ParentID='0' Where SiteID='" + SiteID + "' And SpecialID='" + Id + "'";

                DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
            }
        }
        public void PRStCList(string idstr)
        {
            string str_Sql = "Update " + Pre + "sys_styleclass Set isRecyle=0 Where SiteID='" + SiteID + "' And ClassID In (" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void PRStList(string classid, string idstr)
        {
            string str_Sql = "Select ClassID From " + Pre + "sys_styleclass Where isRecyle=1 And " +
                             "SiteID='" + SiteID + "' And ClassID In (" + idstr + ")";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (dt != null)
            {
                string str_Sql1 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string styleclassid = dt.Rows[i]["ClassID"].ToString();
                    string styleid = dt.Rows[i]["styleID"].ToString();

                    if (checkParIsDel(styleclassid, "sys_styleclass") == true)
                        str_Sql1 = "Update " + Pre + "sys_LabelStyle Set isRecyle=0,ClassID='" + styleclassid + "' Where SiteID='" + SiteID + "' And styleID='" + styleid + "'";
                    else
                        str_Sql1 = "Update " + Pre + "sys_LabelStyle Set isRecyle=0 Where SiteID='" + SiteID + "' And styleID='" + styleid + "'";

                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
                }
                dt.Clear(); dt.Dispose();
            }
        }
        public void PRLCList(string idstr)
        {
            string str_Sql = "Update " + Pre + "sys_LabelClass Set isRecyle=0 Where SiteID='" + SiteID + "' And ClassID In (" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void PRLList(string classid, string idstr)
        {
            string str_Sql = "Select LabelID,ClassID From " + Pre + "sys_Label Where isRecyle=1 " +
                             "And SiteID='" + SiteID + "' And LabelID In (" + idstr + ")";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (dt != null)
            {
                string str_Sql1 = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strclassid = dt.Rows[i]["ClassID"].ToString();
                    string strlabelid = dt.Rows[i]["LabelID"].ToString();

                    if (checkParIsDel(strclassid, "sys_LabelClass") == true)
                        str_Sql1 = "Update " + Pre + "sys_Label Set isRecyle=0,ClassID='" + classid + "' Where SiteID='" + SiteID + "' And LabelID='" + strlabelid + "'";
                    else
                        str_Sql1 = "Update " + Pre + "sys_Label Set isRecyle=0 Where SiteID='" + SiteID + "' And LabelID='" + strlabelid + "'";

                    DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql1, null);
                }
                dt.Clear(); dt.Dispose();
            }
        }
        public void PRPSFList(string idstr)
        {
            string str_Sql = "Update " + Pre + "sys_PSF Set isRecyle=0 Where SiteID='" + SiteID + "' And psfID In (" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public void PDNCList(string idstr)
        {
            idstr = getIDStr(idstr, "ClassID,ParentID", "news_Class");
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                DataTable dt = getNewsTable();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tbname = dt.Rows[i][0].ToString();
                        string str_Sql = "Delete From " + tbname + " Where ClassID In (Select ClassID From " + Pre + "news_Class" +
                                         " Where SiteID='" + SiteID + "' And isRecyle=1 And ClassID In(" + idstr + "))";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                        string str_Sql1 = "Delete From " + Pre + "news_Class Where SiteID='" + SiteID + "' And isRecyle=1 And ClassID In(" + idstr + ")";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                    }
                    tran.Commit();
                    dt.Clear(); dt.Dispose();
                }
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void PDNList(string idstr)
        {
            DataTable dt = getNewsTable();
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string tbname = dt.Rows[i][0].ToString();
                    string Sql = "Delete From " + tbname + " Where SiteID='" + SiteID + "' And isRecyle=1 And NewsID In(" + idstr + ")";
                    DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
                }
                dt.Clear(); dt.Dispose();
            }
        }
        public void PDCList(string idstr)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                DataTable dt = getNewsTable();
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string tbname = dt.Rows[i][0].ToString();

                        string str_Sql = "Delete From " + Pre + "API_commentary Where InfoID In(" +
                                         "Select NewsID From " + tbname + " Where SiteID In(" +
                                         "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And ChannelID In(" + idstr + ")))";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);

                        string str_Sql1 = "Delete From " + tbname + " Where SiteID In(" +
                                          "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And ChannelID In(" + idstr + "))";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);

                        string str_Sql2 = "Delete From " + Pre + "news_Class Where SiteID In(" +
                                          "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And ChannelID In(" + idstr + "))";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql2, null);

                        string str_Sql5 = "Delete From " + Pre + "special_news Where SpecialID In (Select SpecialID From "+
                                          "" + Pre + "news_special Where SiteID In(Select ChannelID From " + Pre + " news_site "+
                                          "Where isRecyle=1 And ChannelID In(" + idstr + ")))";
                        DbHelper.ExecuteNonQuery(tran,CommandType.Text, str_Sql5, null);

                        string str_Sql3 = "Delete From " + Pre + "news_special Where SiteID In(" +
                                          "Select ChannelID From " + Pre + " news_site Where isRecyle=1 And ChannelID In(" + idstr + "))";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql3, null);

                        string str_Sql4 = "Delete From " + Pre + "news_site Where isRecyle=1 And ChannelID In(" + idstr + ")";
                        DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql4, null);

                    }
                    tran.Commit();
                }
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void PDSList(string idstr)
        {
            idstr = getIDStr(idstr, "SpecialID,ParentID", "news_special");
            string Sql = "Delete From " + Pre + "special_news Where SpecialID In (" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);

            Sql = "Delete From " + Pre + "news_special where SpecialID in(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void PDStCList(string idstr)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_Sql = "Delete From " + Pre + "sys_LabelStyle Where ClassID In (" + idstr + ") And SiteID='" + SiteID + "' And isRecyle=1";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                string str_Sql1 = "Delete From " + Pre + "sys_styleclass Where SiteID='" + SiteID + "' And isRecyle=1 And ClassID In(" + idstr + ")";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                tran.Commit();
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void PDStList(string idstr)
        {
            string str_Sql = "Delete From " + Pre + "sys_LabelStyle Where SiteID='" + SiteID + "' And isRecyle=1";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void PDLCList(string idstr)
        {
            SqlConnection Conn = new SqlConnection(DBConfig.CmsConString);
            Conn.Open();
            SqlTransaction tran = Conn.BeginTransaction();
            try
            {
                string str_Sql = "Delete From " + Pre + "sys_Label Where ClassID In (" + idstr + ") And SiteID='" + SiteID + "' And isRecyle=1";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql, null);
                string str_Sql1 = "Delete From " + Pre + "sys_LabelClass Where SiteID='" + SiteID + "' And isRecyle=1 And ClassID In(" + idstr + ")";
                DbHelper.ExecuteNonQuery(tran, CommandType.Text, str_Sql1, null);
                tran.Commit();
                Conn.Close();
            }
            catch (SqlException e)
            {
                tran.Rollback();
                Conn.Close();
                throw e;
            }
        }
        public void PDLList(string idstr)
        {
            string str_Sql = "Delete From " + Pre + "sys_Label Where isRecyle=1 And SiteID='" + SiteID + "' And LabelID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        public void PDPSFList(string idstr)
        {
            string str_Sql = "Delete From " + Pre + "sys_PSF Where isRecyle=1 And SiteID='" + SiteID + "' And psfID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------
        //---------------------------------------------------------------------------------------------------------------------------------------
        public DataTable getNewsTable()
        {
            string str_Sql = "Select TableName From " + Pre + "sys_NewsIndex";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable getNewsClass(string idstr)
        {
            string str_Sql = "";
            if (idstr != null && idstr != "" && idstr != string.Empty)
                str_Sql = "Select ClassID,SavePath,SaveClassframe From " + Pre + "news_Class Where SiteID='" + SiteID + "' And isRecyle=1 And ClassID In(" + getIDStr(idstr, "ClassID,ParentID", "news_Class") + ")";
            else
                str_Sql = "Select ClassID,SavePath,SaveClassframe From " + Pre + "news_Class Where SiteID='" + SiteID + "' And isRecyle=1";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable getNews(string classid, string tbname)
        {
            string str_Sql = "";
            if (classid != null && classid != "" && classid != string.Empty)
                str_Sql = "Select NewsID,ClassID,SavePath,FileName,FileEXName From " + tbname + " Where isRecyle=1 And" +
                          " SiteID='" + SiteID + "' And ClassID='" + classid + "'";
            else
                str_Sql = "Select NewsID,ClassID,SavePath,FileName,FileEXName From " + tbname + " Where isRecyle=1 And SiteID='" + SiteID + "'";

            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable getSpeaciList(string idstr)
        {
            string str_Sql = "";
            if (idstr != "" && idstr != null & idstr != string.Empty)
                str_Sql = "Select SpecialID,SavePath,specialEName,saveDirPath,FileName,FileEXName From " + Pre + "news_special Where " +
                          "isRecyle=1 And SiteID='" + SiteID + "' And SpecialID In(" + getIDStr(idstr, "SpecialID,ParentID", "news_special") + ")";
            else
                str_Sql = "Select SpecialID,SavePath,specialEName,saveDirPath,FileName,FileEXName From " +
                          "" + Pre + "news_special Where isRecyle=1 And SiteID='" + SiteID + "'";

            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }
        public DataTable getSite(string idstr)
        {
            string str_Sql = "";
            if (idstr != "" && idstr != null & idstr != string.Empty)
                str_Sql = "Select ChannelID,EName From " + Pre + "news_site Where isRecyle=1 And ParentID='0' And ChannelID In(" + idstr + ") And SiteID='" + SiteID + "'";
            else
                str_Sql = "Select ChannelID,EName From " + Pre + "news_site Where isRecyle=1 And ParentID='0' And SiteID='" + SiteID + "'";

            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }



        protected string getChildId(string id, string col, string tbname)
        {
            string str_Sql = "Select " + col + " From " + Pre + tbname + " Where SiteID='" + SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            string idstr = "'" + id + "'," + getRecursion(dt, id);
            return idstr;
        }

        protected string getRecursion(DataTable dt, string PID)
        {
            DataRow[] row = null;
            string idstr = "";
            row = dt.Select("ParentID='" + PID + "'");
            if (row.Length < 1)
                return idstr;
            else
            {
                foreach (DataRow r in row)
                {
                    idstr += "'" + r[0].ToString() + "',";
                    idstr += getRecursion(dt, r[0].ToString());
                }
            }
            return idstr;
        }

        public string getIDStr(string id, string col, string tbname)
        {
            string[] arr_id = id.Split(',');
            string temp_id = "";
            string temp_id1 = "";
            for (int i = 0; i < arr_id.Length; i++)
            {
                temp_id = arr_id[i].Replace("'", "");
                temp_id1 += getChildId(temp_id, col, tbname);
            }
            temp_id1 = Foosun.Common.Input.CutComma(temp_id1);
            return temp_id1;
        }

        public void raDComment(string NewsID, bool isDel)
        {
            string str_Sql = "";
            if (isDel == true)
                str_Sql = "Delete From " + Pre + "API_commentary Where InfoID='" + NewsID + "'";
            else
                str_Sql = "Update " + Pre + "API_commentary Set isRecyle=0 Where InfoID='" + NewsID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        protected bool checkParIsDel(string classid, string tbname)
        {
            bool rtf = true;
            string str_Sql = "Select isRecyle From " + Pre + tbname + " Where ClassID='" + classid + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() != "1")
                        rtf = false;
                }
                dt.Clear(); dt.Dispose();
            }
            return rtf;
        }

        protected bool getParentlockTf(string ID, string tbname, string classid)
        {
            bool LockTf = false;
            ID = getParentID(ID, tbname, classid);
            string Str_Sql = "Select ParentID,isRecyle From " + Pre + tbname + " where " + classid + "='" + ID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Str_Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["isRecyle"].ToString() == "1")
                        LockTf = true;
                    else
                        LockTf = getParentlockTf(dt.Rows[0]["ParentID"].ToString(), tbname, classid);
                }
                dt.Clear(); dt.Dispose();
            }
            return LockTf;
        }
        protected string getParentID(string ID, string tbname, string classid)
        {
            string strparentid = ID;
            string str_Sql = "Select ParentID,isRecyle From " + Pre + tbname + " where " + classid + "='" + ID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    strparentid = dt.Rows[0]["ParentID"].ToString();
                dt.Clear(); dt.Dispose();
            }
            return strparentid;
        }
    }
}
