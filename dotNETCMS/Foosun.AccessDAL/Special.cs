using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Special : DbBase, ISpecial
    {
        private string SiteID;
        public Special()
        {
            SiteID = Foosun.Global.Current.SiteID;
        }

        public DataTable getChildList(string classid)
        {
            string str_Sql = "Select Id,SpecialID,SpecialCName,CreatTime,isLock From " + Pre + "news_special " +
                             "Where isRecyle=0 and ParentID='" + classid + "' and SiteID='" + SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }

        public void Lock(string id)
        {
            string idstr = Foosun.Common.Input.CutComma(getChildId(id));
            string str_sql = "Update " + Pre + "news_special Set isLock=1 where SpecialID In(" + idstr + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        public void UnLock(string id)
        {
            bool tempTF = getParentlockTF(id);
            if (tempTF == false)
            {
                string str_sql = "Update " + Pre + "news_special Set isLock=0 where SpecialID='" + id + "'";
                DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
            }
            else
            {
                throw new Exception("当前专题的父专题被锁定,要想解锁此专题请先解锁此专题的父专题!");
            }
        }

        public void RemoveNews(string specialID, string newsID)
        {
            string str_Sql = "Delete From " + Pre + "special_news where SpecialID='" + specialID + "' and NewsID='" + newsID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        public void PDel(string id)
        {
            string tempid = getIDStr(id);
            string str_Sql = "Update " + Pre + "news_special Set isRecyle=1 Where SpecialID in (" + tempid + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        public void PDels(string id)
        {
            string tempid = getIDStr(id);
            string str_Sql = "Delete From " + Pre + "news_special where SpecialID in (" + tempid + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, null);
        }

        public void PLock(string id)
        {
            string tempid = getIDStr(id);
            string str_sql = "Update " + Pre + "news_special Set isLock=1 where SpecialID In (" + tempid + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        public void PUnLock(string id)
        {
            string[] arr_id = id.Split(',');
            string temp_id = "";
            string temp_id1 = "";
            bool locktf = false;
            for (int i = 0; i < arr_id.Length; i++)
            {
                temp_id = arr_id[i].Replace("'", "");
                locktf = getParentlockTF(temp_id);  //检测父类的状态是否被锁定
                if (locktf == false)
                    temp_id1 += "'" + temp_id + "',";
            }
            temp_id1 = Foosun.Common.Input.CutComma(temp_id1);
            if (temp_id1 == null || temp_id1 == "" || temp_id1 == string.Empty)
                throw new Exception("选中专题的父专题被锁定,请先解锁此专题的父专题!");
            string str_sql = "Update " + Pre + "news_special Set isLock=0 where SpecialID In(" + temp_id1 + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        public string getSpicaelNewsNum(string id)
        {
            string str_Sql = "Select Count(Id) From " + Pre + "special_news Where SpecialID='" + id + "'";
            int result = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, str_Sql, null));
            return result.ToString();
        }

        public string Add(Foosun.Model.Special sci)
        {
            OleDbParameter[] param = GetSpecialParameters(sci);

            int result = 0;
            string SpecialID = "";
            string checkSql = "";
            int recordCount = 0;
            SpecialID = Foosun.Common.Rand.Number(12);
            while (true)
            {
                checkSql = "select count(*) from " + Pre + "news_special where SpecialID='" + SpecialID + "'";
                recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
                if (recordCount < 1)
                    break;
                else
                    SpecialID = Foosun.Common.Rand.Number(12, true);
            }
            checkSql = "select count(*) from " + Pre + "news_special where SpecialCName='" + sci.SpecialCName + "'";
            recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("专题中文名称重复,请重新添加!");
            }
            checkSql = "select count(*) from " + Pre + "news_special where specialEName='" + sci.specialEName + "'";
            recordCount = (int)DbHelper.ExecuteScalar(CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("专题英文名称重复,请重新添加!");
            }
            string str_Sql = "Insert Into " + Pre + "news_special(" + Database.getParam(param) + ") Values(" + Database.getAParam(param) + ")";

            result = Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, str_Sql, param));
            return result + "|" + SpecialID;
        }

        public int Edit(Foosun.Model.Special sci)
        {
            OleDbParameter[] param = GetSpecialParameters(sci);
            int result = 0;
            string checkSql = "";
            int recordCount = 0;
            checkSql = "select count(*) from " + Pre + "news_special Where SpecialID<>'" + sci.SpecialID + "' " +
                       " And SpecialCName='" + sci.SpecialCName + "'";
            recordCount = (int)DbHelper.ExecuteScalar( CommandType.Text, checkSql, null);
            if (recordCount > 0)
            {
                throw new Exception("专题中文名称重复,请重新修改!");
            }

            //string Sql = "Update " + Pre + "news_special Set SpecialCName=@SpecialCName," +
            //             "[Domain]=@Domain,isDelPoint=@isDelPoint,Gpoint=@Gpoint,iPoint=@iPoint," +
            //             "GroupNumber=@GroupNumber,saveDirPath=@saveDirPath,SavePath=@SavePath," +
            //             "FileName=@FileName,FileEXName=@FileEXName,NaviPicURL=@NaviPicURL," +
            //             "NaviContent=@NaviContent,Templet=@Templet,NaviPosition=@NaviPosition " +
            //             "Where SpecialID=@SpecialID";
            //result = Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(param,Database.getSqlParam(Sql))));

            string Sql = "Update " + Pre + "news_special Set SpecialCName='"+sci.SpecialCName+"'," +
             "[Domain]='"+sci.Domain+"',isDelPoint="+sci.isDelPoint+",Gpoint="+sci.Gpoint+",iPoint="+sci.iPoint+"," +
             "GroupNumber='"+sci.GroupNumber+"',saveDirPath='"+sci.saveDirPath+"',SavePath='"+sci.SavePath+"'," +
             "FileName='"+sci.FileName+"',FileEXName='"+sci.FileEXName+"',NaviPicURL='"+sci.NaviPicURL+"'," +
             "NaviContent='"+sci.NaviContent+"',Templet='"+sci.Templet+"',NaviPosition='"+sci.NaviPosition+"' " +
             "Where SpecialID='" + sci.SpecialID + "'";
            result = Convert.ToInt32(DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null));
            return result;
        }

        public DataTable getSpeacilInfo(string id)
        {
            string str_Sql = "Select SpecialID,SpecialCName,specialEName,ParentID,[Domain],isDelPoint,Gpoint,iPoint,GroupNumber,FileName," +
                             "FileEXName,NaviPicURL,NaviContent,Templet,isLock,isRecyle,SavePath,saveDirPath,NaviPosition From " +
                             Pre + "news_special Where SiteID='" + Foosun.Global.Current.SiteID + "' And SpecialID='" + id + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }

        protected string getChildId(string id)
        {
            string str_Sql = "Select SpecialID,ParentID From " + Pre + "news_special Where SiteID='" + SiteID + "'";
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

        protected bool getParentlockTF(string id)
        {
            bool LockTF = false;
            id = getParentID(id);
            string str_sql = "Select ParentID,isLock From " + Pre + "news_special where SpecialID='" + id + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["isLock"].ToString() == "1")
                        LockTF = true;
                    else
                        LockTF = getParentlockTF(dt.Rows[0]["ParentID"].ToString());
                }
                dt.Dispose();
                dt.Clear();
            }
            return LockTF;
        }

        protected string getParentID(string id)
        {
            string str_parentid = id;
            string str_sql = "Select ParentID From " + Pre + "news_special where SpecialID='" + id + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                    str_parentid = dt.Rows[0]["ParentID"].ToString();
                dt.Clear();
                dt.Dispose();
            }
            return str_parentid;
        }

        public DataTable getSpecialFileInfo(string id)
        {
            string tempid = getIDStr(id);
            string str_Sql = "Select SpecialID,ParentID,specialEName,SavePath,saveDirPath,FileName,FileEXName From " + Pre +
                             "news_special where SpecialID in(" + tempid + ")";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, str_Sql, null);
            return dt;
        }

        protected string getIDStr(string id)
        {
            string[] arr_id = id.Split(',');
            string temp_id = "";
            string temp_id1 = "";
            for (int i = 0; i < arr_id.Length; i++)
            {
                temp_id = arr_id[i].Replace("'", "");
                temp_id1 += getChildId(temp_id);
            }
            temp_id1 = Foosun.Common.Input.CutComma(temp_id1);
            return temp_id1;
        }

        private OleDbParameter[] GetSpecialParameters(Foosun.Model.Special sc)
        {
            OleDbParameter[] param = new OleDbParameter[21];
            param[0] = new OleDbParameter("@SpecialID", OleDbType.VarWChar, 12);
            param[0].Value = sc.SpecialID;
            param[1] = new OleDbParameter("@SpecialCName", OleDbType.VarWChar, 50);
            param[1].Value = sc.SpecialCName;
            param[2] = new OleDbParameter("@specialEName", OleDbType.VarWChar, 50);
            param[2].Value = sc.specialEName;

            param[3] = new OleDbParameter("@ParentID", OleDbType.VarWChar, 12);
            if (string.IsNullOrEmpty(sc.ParentID.ToString()))
            {
                param[3].Value = 0;
            }
            else
            {
                param[3].Value = sc.ParentID;
            }
            param[4] = new OleDbParameter("@Domain", OleDbType.VarWChar, 100);
            param[4].Value = sc.Domain;
            param[5] = new OleDbParameter("@isDelPoint", OleDbType.Integer, 1);
            param[5].Value = sc.isDelPoint;

            param[6] = new OleDbParameter("@Gpoint", OleDbType.Integer, 8);
            param[6].Value = sc.Gpoint;
            param[7] = new OleDbParameter("@iPoint", OleDbType.Integer, 8);
            param[7].Value = sc.iPoint;
            param[8] = new OleDbParameter("@GroupNumber", OleDbType.VarWChar,200);
            param[8].Value = sc.GroupNumber;

            param[9] = new OleDbParameter("@saveDirPath", OleDbType.VarWChar, 100);
            param[9].Value = sc.saveDirPath;
            param[10] = new OleDbParameter("@SavePath", OleDbType.VarWChar, 100);
            param[10].Value = sc.SavePath;
            param[11] = new OleDbParameter("@FileName", OleDbType.VarWChar, 100);
            param[11].Value = sc.FileName;

            param[12] = new OleDbParameter("@FileEXName", OleDbType.VarWChar, 6);
            param[12].Value = sc.FileEXName;
            param[13] = new OleDbParameter("@NaviPicURL", OleDbType.VarWChar, 200);
            param[13].Value = sc.NaviPicURL;
            param[14] = new OleDbParameter("@NaviContent", OleDbType.VarWChar, 255);
            param[14].Value = sc.NaviContent;

            param[15] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[15].Value = sc.SiteID;
            param[16] = new OleDbParameter("@Templet", OleDbType.VarWChar, 200);
            param[16].Value = sc.Templet;
            param[17] = new OleDbParameter("@isLock", OleDbType.Integer, 1);
            param[17].Value = sc.isLock;

            param[18] = new OleDbParameter("@isRecyle", OleDbType.Integer, 1);
            param[18].Value = sc.isRecyle;
            param[19] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[19].Value = sc.CreatTime;
            param[20] = new OleDbParameter("@NaviPosition", OleDbType.VarWChar,255);
            param[20].Value = sc.NaviPosition;
            return param;
        }

        public IDataReader ToTempletBind(string ParentID)
        {
            OleDbParameter param = new OleDbParameter("@ParentID", ParentID);
            string sql = "select ID,SpecialID,SpecialCName,ParentID,Templet from " + Pre + "news_special where ParentID=@ParentID and isLock=0 and isRecyle=0 order by id desc";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);
        }

        public void BindSPTemplet(string SpecialID, string Templet)
        {
            string sql = "update " + Pre + "news_special set Templet='" + Templet + "' where SpecialID in (" + SpecialID + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, sql, null);
        }
    }

}
