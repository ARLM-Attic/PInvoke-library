using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Foosun.DALProfile;
using Foosun.DALFactory;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Psframe : DbBase, IPsframe
    {
        /// <summary>
        /// 删除PSF到回收站
        /// </summary>
        /// <param name="TableName"></param>
        public void Del_PSF(string Psfid)
        {
            string Sql = "Update " + Pre + "sys_PSF Set isRecyle=1 where psfID='" + Psfid.ToString() + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 删除所有PSF到回收站
        /// </summary>
        /// <param name="TableName"></param>
        public void DelAll_PSF()
        {
            string Sql = "Update " + Pre + "sys_PSF Set isRecyle=1 where 1=1 and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 插入psf
        /// </summary>
        /// <param name="uc2"></param>
        public void InsertPSF(Foosun.Model.PSF uc)
        {
            OleDbParameter[] parm = InsertPSFParameters(uc); 
            string Sql = "insert into " + Pre + "sys_PSF (";
            Sql += ""+Database.getParam(parm)+"";
            Sql += ") values (";
            Sql += ""+Database.getAParam(parm)+")";
            
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新psf
        /// </summary>
        /// <param name="uc2"></param>
        public int UpdatePSF(Foosun.Model.PSF uc)
        {
            OleDbParameter[] parm = InsertPSFParameters1(uc);
            string Sql = "Update " + Pre + "sys_PSF set " + Database.getParam(parm) + " where psfID='" + uc.psfID.ToString() + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取PSF构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertPSFParameters(Foosun.Model.PSF uc)
        {
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@psfID", OleDbType.VarWChar, 12);
            param[0].Value = uc.psfID;
            param[1] = new OleDbParameter("@psfName", OleDbType.VarWChar, 30);
            param[1].Value = uc.psfName;
            param[2] = new OleDbParameter("@LocalDir", OleDbType.VarWChar, 200);
            param[2].Value = uc.LocalDir;
            param[3] = new OleDbParameter("@RemoteDir", OleDbType.VarWChar, 200);
            param[3].Value = uc.RemoteDir;
            param[4] = new OleDbParameter("@isAll", OleDbType.Integer, 1);
            param[4].Value = uc.isAll;
            param[5] = new OleDbParameter("@isSub", OleDbType.Integer, 1);
            param[5].Value = uc.isSub;
            param[6] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[6].Value = uc.CreatTime;
            param[7] = new OleDbParameter("@isRecyle", OleDbType.Integer, 1);
            param[7].Value = uc.isRecyle;
            param[8] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[8].Value = uc.SiteID;
            //param[9] = new OleDbParameter("@ID", OleDbType.Integer, 4);
            //param[9].Value = uc.Id;
            return param;
        }
        /// <summary>
        /// 获取PSF构造1
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertPSFParameters1(Foosun.Model.PSF uc)
        {
            OleDbParameter[] param = new OleDbParameter[7];
            param[0] = new OleDbParameter("@psfName", OleDbType.VarWChar, 30);
            param[0].Value = uc.psfName;
            param[1] = new OleDbParameter("@LocalDir", OleDbType.VarWChar, 200);
            param[1].Value = uc.LocalDir;
            param[2] = new OleDbParameter("@RemoteDir", OleDbType.VarWChar, 200);
            param[2].Value = uc.RemoteDir;
            param[3] = new OleDbParameter("@isAll", OleDbType.Integer, 1);
            param[3].Value = uc.isAll;
            param[4] = new OleDbParameter("@isSub", OleDbType.Integer, 1);
            param[4].Value = uc.isSub;
            param[5] = new OleDbParameter("@isRecyle", OleDbType.Integer, 1);
            param[5].Value = uc.isRecyle;
            param[6] = new OleDbParameter("@psfID", OleDbType.VarWChar, 12);
            param[6].Value = uc.psfID;
            return param;
        }

        public DataTable getTitleRecord(string psfName)
        {
            string Sql = "Select psfName From " + Pre + "sys_PSF Where psfName='" + psfName.ToString() + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public int IsExitPSFID(string PSFID)
        {
            string Str = "Select psfID From " + Pre + "sys_PSF where psfID = '" + PSFID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Str, null);
        }

        public DataTable getPSFParam(string psfID)
        {
            string Sql = "Select Id,psfID,psfName,LocalDir,RemoteDir,isSub,isAll,CreatTime,isRecyle,SiteID From " + Pre + "sys_PSF where psfID = '" + psfID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }



        #region 计划任务
        /// <summary>
        /// 检查计划任务ID是否重复
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable getTaskParam(string TaskID)
        {
            string Sql = "Select Id,taskID From " + Pre + "sys_SiteTask where taskID = '" + TaskID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        /// <summary>
        /// 检查计划任务名称是否重复
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable getTaskName(string TaskName)
        {
            string Sql = "Select TaskName From " + Pre + "sys_SiteTask Where TaskName='" + TaskName + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        public int DelOneTask(string taskid)
        {
            string Str_DelOne_Sql = "Delete From " + Pre + "sys_SiteTask where taskID = '" + taskid + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_DelOne_Sql, null);
        }

        public void DelPTask(string boxs)
        {
            string str_sql = "Delete From " + Pre + "sys_SiteTask  where taskID in('" + boxs + "') and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }
        public int DelAllTask()
        {
            string Str_DelAll_Sql = "Delete From " + Pre + "sys_SiteTask where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_DelAll_Sql, null);
        }

        /// <summary>
        /// 获得某一ID的Task 信息
        /// </summary>
        /// <param name="TaskID"></param>
        /// <returns></returns>
        public DataTable getTaskIDInfo(string TaskID)
        {
            string Sql = "Select Id,taskID,TaskName,isIndex,ClassID,News,Special,TimeSet,CreatTime,SiteID From " + Pre + "sys_SiteTask where taskID='" + TaskID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 插入Task
        /// </summary>
        /// <param name="uc2"></param>
        public void insertTask(Foosun.Model.Task uc)
        {
            string Sql = "insert into " + Pre + "sys_SiteTask (";
            Sql += "taskID,TaskName,isIndex,ClassID,News,Special,TimeSet,CreatTime,SiteID";
            Sql += ") values (";
            Sql += "@taskID,@TaskName,@isIndex,@ClassID,@News,@Special,@TimeSet,@CreatTime,@SiteID)";
            OleDbParameter[] parm = insertTaskParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 插入Task
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateTask(Foosun.Model.Task uc)
        {
            string Sql = "Update " + Pre + "sys_SiteTask set taskID=@taskID,TaskName=@TaskName,isIndex=@isIndex,ClassID=@ClassID,News=@News,Special=@Special,TimeSet=@TimeSet,CreatTime=@CreatTime,SiteID=@SiteID where taskID='" + uc.taskID + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            OleDbParameter[] parm = Database.getNewParam(insertTaskParameters(uc),"taskID,TaskName,isIndex,ClassID,News,Special,TimeSet,CreatTime,SiteID");
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取PSF构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] insertTaskParameters(Foosun.Model.Task uc)
        {
            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@taskID", OleDbType.VarWChar, 12);
            param[0].Value = uc.taskID;
            param[1] = new OleDbParameter("@TaskName", OleDbType.VarWChar, 30);
            param[1].Value = uc.TaskName;
            param[2] = new OleDbParameter("@isIndex", OleDbType.Integer, 1);
            param[2].Value = uc.isIndex;
            param[3] = new OleDbParameter("@ClassID", OleDbType.VarWChar);
            param[3].Value = uc.ClassID;
            param[4] = new OleDbParameter("@News", OleDbType.VarWChar);
            param[4].Value = uc.News;
            param[5] = new OleDbParameter("@Special", OleDbType.VarWChar);
            param[5].Value = uc.Special;
            param[6] = new OleDbParameter("@TimeSet", OleDbType.VarWChar, 100);
            param[6].Value = uc.TimeSet;
            param[7] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[7].Value = uc.CreatTime;
            param[8] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[8].Value = uc.SiteID;
            param[9] = new OleDbParameter("@ID", OleDbType.Integer, 4);
            param[9].Value = uc.Id;
            return param;
        }

        #endregion
    }
}
