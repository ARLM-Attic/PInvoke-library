using System;
using System.Data;
using System.Data.SqlClient;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;
using System.Data.Common;

namespace Foosun.SQLServerDAL
{
    public class UserMisc : DbBase, IUserMisc
    {
        public DataTable getSiteList()
        {
            string Sql = "select ID,ChannelID,CName from " + Pre + "news_site where IsURL=0 and isRecyle=0 and isLock=0 order by id asc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        #region 菜单部分
        public IDataReader Navilist(string UserNum)
        {
            string getS = "";
            string SQLTF = "select am_ID from " + Pre + "api_Navi where am_position='99999' and siteID='" + Foosun.Global.Current.SiteID + "' ";
            object obj = DbHelper.ExecuteScalar(CommandType.Text, SQLTF, null);
            if (obj != null)
            {
                getS = " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            }
            else
            {
                getS = " and SiteID='0'";
            }
            string Sql = "select am_ID,am_ClassID,Am_position,am_Name,am_FilePath,am_target,am_type,siteID,userNum,isSys,mainURL From " + Pre + "api_Navi where Am_position='00000' " + getS + " order by am_orderID asc,am_ID desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        public IDataReader aplist(string UserNum)
        {
            string getS = "";
            string SQLTF = "select am_ID from " + Pre + "api_Navi where am_position='99999' and siteID='" + Foosun.Global.Current.SiteID + "' ";
            object obj = DbHelper.ExecuteReader(CommandType.Text, SQLTF, null);
            if (obj != null)
            {
                getS = " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            }
            else
            {
                getS = " and SiteID='0'";
            }
            string Sql = "Select am_ID,am_ClassID,Am_position,am_Name,am_FilePath,am_target,am_type,siteID,userNum,isSys From " + Pre + "api_Navi where Am_position='99999' " + getS + " order by am_orderID asc,am_ID desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        public DataTable calendar(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select id,logID,Title,Content,userNum,LogDateTime From " + Pre + "user_userlogs Where (DATEDIFF(d, LogDateTime, getdate())<=datenum) and Usernum=@UserNum and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable messageChar(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select id,Rec_UserNum From " + Pre + "user_message Where Rec_UserNum=@UserNum and isRead=0 and isRdel=0 and isRecyle=0 order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public IDataReader ShortcutList(string UserNum, int _num)
        {
            SqlParameter[] param = new SqlParameter[] 
{
    new SqlParameter("@UserNum", UserNum), 
    new SqlParameter("@_num", _num) 
};
            string Sql = "Select id,QMID,qName,FilePath,usernum,siteid From " + Pre + "API_Qmenu where ismanage=@_num and (UserNum=@UserNum or UserNum='0') order by OrderID desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }

        public IDataReader menuNavilist(string stype, string UserNum)
        {
            SqlParameter[] param = new SqlParameter[]{
            new SqlParameter("@stype", stype),
new SqlParameter("@UserNum", UserNum)
};
            string getS = "";
            string SQLTF = "select am_ID from " + Pre + "api_Navi where am_ParentID=@stype and siteID='" + Foosun.Global.Current.SiteID + "' ";
            object obj = DbHelper.ExecuteScalar(CommandType.Text, SQLTF, param);
            if (obj != null)
            {
                getS = " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            }
            else
            {
                getS = " and SiteID='0'";
            }
            string Sql = "Select am_ID,am_ClassID,Am_position,am_Name,am_FilePath,am_target,am_type,siteID,userNum,isSys,popCode From " + Pre + "api_Navi where am_ParentID=@stype " + getS + " order by am_orderID asc,am_ID desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }
        /// <summary>
        /// 得到菜单
        /// </summary>
        /// <returns></returns>
        public DataTable ManagemenuNavilist()
        {
            string Sql = "Select am_id,api_IdentID,am_ClassID,Am_position,am_Name,Am_Ename,am_FilePath,am_target,am_ParentID,am_type,am_orderID,isSys From " + Pre + "API_Navi where Am_position='00000' and SiteID='" + Foosun.Global.Current.SiteID + "' order by am_orderID desc,am_id asc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }
        /// <summary>
        /// 得到子菜单
        /// </summary>
        /// <returns></returns>
        public DataTable ManagechildmenuNavilist(string pID)
        {
            SqlParameter param = new SqlParameter("@pID", pID);
            string Sql = "Select am_id,api_IdentID,am_ClassID,Am_position,am_Name,Am_Ename,am_FilePath,am_target,am_ParentID,am_type,am_orderID,isSys From " + Pre + "API_Navi where am_ParentID=@pID and SiteID='" + Foosun.Global.Current.SiteID + "' order by am_orderID desc,am_id asc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        /// <summary>
        /// 得到菜单编号是否重复
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public DataTable getManageChildNaviRecord(string am_ClassID)
        {
            SqlParameter param = new SqlParameter("@am_ClassID", am_ClassID);
            string Sql = "Select am_ClassID From " + Pre + "API_Navi Where am_ClassID=@am_ClassID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        /// <summary>
        /// 得到菜单某个记录值
        /// </summary>
        /// <param name="pID"></param>
        /// <returns></returns>
        public DataTable GetNaviEditID(int nID)
        {
            SqlParameter param = new SqlParameter("@nID", nID);
            string Sql = "Select am_id,api_IdentID,am_ClassID,Am_position,am_Name,Am_Ename,am_FilePath,am_target,am_ParentID,am_type,am_orderID,isSys,popCode From " + Pre + "API_Navi where am_id=@nID and SiteID='" + Foosun.Global.Current.SiteID + "' order by am_orderID desc,am_id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        /// <summary>
        /// 得到顶部菜单
        /// </summary>
        /// <returns></returns>
        public DataTable Getparentidlist()
        {
            string Sql = "Select am_id,api_IdentID,am_ClassID,Am_position,am_Name,Am_Ename,am_FilePath,am_target,am_ParentID,am_type,am_orderID,isSys,popCode From " + Pre + "API_Navi where Am_position='00000' and SiteID='" + Foosun.Global.Current.SiteID + "' order by am_orderID desc,am_id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到顶部子菜单
        /// </summary>
        /// <returns></returns>
        public DataTable Getchildparentidlist(string pID)
        {
            SqlParameter param = new SqlParameter("@pID", pID);
            string Sql = "Select am_id,api_IdentID,am_ClassID,Am_position,am_Name,Am_Ename,am_FilePath,am_target,am_ParentID,am_type,am_orderID,isSys,popCode From " + Pre + "API_Navi where am_ParentID=@pID and SiteID='" + Foosun.Global.Current.SiteID + "' order by am_orderID desc,am_id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }
        /// <summary>
        /// 插入菜单新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void InsertManageMenu(Foosun.Model.UserInfo7 uc2)
        {
            string Sql = "insert into " + Pre + "API_Navi (";
            Sql += "api_IdentID,am_ClassID,Am_position,am_Name,am_FilePath,am_target,am_ParentID,am_type,am_creatTime,am_orderID,[isSys],siteID,userNum,popCode";
            Sql += ") values (";
            Sql += "@api_IdentID,@am_ClassID,@Am_position,@am_Name,@am_FilePath,@am_target,@am_ParentID,@am_type,@am_creatTime,@am_orderID,@isSys,'" + Foosun.Global.Current.SiteID + "',@userNum,@popCode)";
            SqlParameter[] parm = InsertManageMenuParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        ///更新菜单
        /// </summary>
        /// <param name="uc2"></param>
        public void EditManageMenu(Foosun.Model.UserInfo7 uc2)
        {
            string Sql = "Update " + Pre + "API_Navi set am_ParentID=@am_ParentID,Am_position=@Am_position,am_Name=@am_Name,am_FilePath=@am_FilePath,am_target=@am_target,am_type=@am_type,am_orderID=@am_orderID,isSys=@isSys,popCode=@popCode where am_ID=" + uc2.am_ID + " " + Foosun.Common.Public.getSessionStr() + "";
            SqlParameter[] parm = InsertManageMenuParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        ///更新菜单
        /// </summary>
        /// <param name="uc2"></param>
        public void EditManageMenu1(Foosun.Model.UserInfo7 uc2)
        {
            string Sql = "Update " + Pre + "API_Navi set am_Name=@am_Name,am_orderID=@am_orderID,popCode=@popCode where am_ID=" + uc2.am_ID + " " + Foosun.Common.Public.getSessionStr() + "";
            SqlParameter[] parm = InsertManageMenuParameters2(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo7构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] InsertManageMenuParameters(Foosun.Model.UserInfo7 uc1)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@api_IdentID", SqlDbType.NVarChar, 30);
            param[0].Value = uc1.api_IdentID;
            param[1] = new SqlParameter("@am_ClassID", SqlDbType.NVarChar, 12);
            param[1].Value = uc1.am_ClassID;
            param[2] = new SqlParameter("@Am_position", SqlDbType.NVarChar, 5);
            param[2].Value = uc1.Am_position;
            param[3] = new SqlParameter("@am_Name", SqlDbType.NVarChar, 20);
            param[3].Value = uc1.am_Name;
            param[4] = new SqlParameter("@am_FilePath", SqlDbType.NVarChar, 200);
            param[4].Value = uc1.am_FilePath;
            param[5] = new SqlParameter("@am_target", SqlDbType.NVarChar, 20);
            param[5].Value = uc1.am_target;
            param[6] = new SqlParameter("@am_ParentID", SqlDbType.NVarChar, 12);
            param[6].Value = uc1.am_ParentID;
            param[7] = new SqlParameter("@am_type", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.am_type;
            param[8] = new SqlParameter("@am_creatTime", SqlDbType.DateTime, 8);
            param[8].Value = uc1.am_creatTime;
            param[9] = new SqlParameter("@am_orderID", SqlDbType.Int, 4);
            param[9].Value = uc1.am_orderID;
            param[10] = new SqlParameter("@isSys", SqlDbType.TinyInt, 1);
            param[10].Value = uc1.isSys;
            param[11] = new SqlParameter("@siteID", SqlDbType.NVarChar, 12);
            param[11].Value = uc1.siteID;
            param[12] = new SqlParameter("@userNum", SqlDbType.NVarChar, 12);
            param[12].Value = uc1.userNum;
            param[13] = new SqlParameter("@am_ID", SqlDbType.Int, 4);
            param[13].Value = uc1.am_ID;
            param[14] = new SqlParameter("@popCode", SqlDbType.NVarChar, 50);
            param[14].Value = uc1.popCode;

            return param;
        }

        /// <summary>
        /// 获取UserInfo7构造1
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] InsertManageMenuParameters1(Foosun.Model.UserInfo7 uc1)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@Am_position", SqlDbType.NVarChar, 5);
            param[0].Value = uc1.Am_position;
            param[1] = new SqlParameter("@am_Name", SqlDbType.NVarChar, 20);
            param[1].Value = uc1.am_Name;
            param[2] = new SqlParameter("@am_FilePath", SqlDbType.NVarChar, 200);
            param[2].Value = uc1.am_FilePath;
            param[3] = new SqlParameter("@am_target", SqlDbType.NVarChar, 20);
            param[3].Value = uc1.am_target;
            param[4] = new SqlParameter("@am_ParentID", SqlDbType.NVarChar, 12);
            param[4].Value = uc1.am_ParentID;
            param[5] = new SqlParameter("@am_type", SqlDbType.TinyInt, 1);
            param[5].Value = uc1.am_type;
            param[6] = new SqlParameter("@am_orderID", SqlDbType.Int, 4);
            param[6].Value = uc1.am_orderID;
            param[7] = new SqlParameter("@isSys", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.isSys;
            param[8] = new SqlParameter("@am_ID", SqlDbType.Int, 4);
            param[8].Value = uc1.am_ID;
            param[9] = new SqlParameter("@popCode", SqlDbType.NVarChar, 50);
            param[9].Value = uc1.popCode;
            return param;
        }

        /// <summary>
        /// 获取UserInfo7构造2
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] InsertManageMenuParameters2(Foosun.Model.UserInfo7 uc1)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@am_Name", SqlDbType.NVarChar, 20);
            param[0].Value = uc1.am_Name;
            param[1] = new SqlParameter("@am_orderID", SqlDbType.Int, 4);
            param[1].Value = uc1.am_orderID;
            param[2] = new SqlParameter("@am_ID", SqlDbType.Int, 4);
            param[2].Value = uc1.am_ID;
            param[3] = new SqlParameter("@popCode", SqlDbType.NVarChar, 50);
            param[3].Value = uc1.popCode;
            return param;
        }

        public void Shortcutdel(int Qid)
        {
            SqlParameter param = new SqlParameter("@Qid", Qid);
            string str_sql = "delete From " + Pre + "API_Navi where am_id=@Qid and UserNum='" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }

        public void Shortcutde2(string ClassID)
        {
            SqlParameter param = new SqlParameter("@ClassID", ClassID);
            string str_sql = "delete From " + Pre + "API_Navi where am_ParentID=@ClassID and UserNum='" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }

        /// <summary>
        /// 显示菜单
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public DataTable navimenusub(string _str)
        {
            string Sql = "Select am_id,api_IdentID,am_ClassID,Am_position,am_Name,Am_Ename,am_FilePath,am_target,am_ParentID,am_type,am_orderID,siteid,isSys From " + Pre + "API_Navi where SiteID='" + Foosun.Global.Current.SiteID + "' " + _str + " order by am_orderID asc,am_ID desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 删除快揭菜单
        /// </summary>
        /// <param name="Qid"></param>
        public void QShortcutdel(int Qid, int _num)
        {
            string str_sql = "delete From " + Pre + "API_Qmenu where id=" + Qid + " and UserNum='" + Foosun.Global.Current.UserNum + "' and ismanage=" + _num + " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        /// <summary>
        /// 获取快捷菜单的列表(管理员)
        /// </summary>
        /// <returns></returns>
        public IDataReader QShortcutList(int _num)
        {
            string Sql = "Select id,QMID,qName,FilePath,usernum,siteid,orderid From " + Pre + "API_Qmenu where (UserNum='" + Foosun.Global.Current.UserNum + "' or UserNum='0') and ismanage=" + _num + " and SiteID='" + Foosun.Global.Current.SiteID + "' order by OrderID desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 获取快捷菜单记录数据
        /// </summary>
        /// <returns></returns>
        public DataTable QeditAction(int QID)
        {
            string Sql = "Select QmID,qName,FilePath,Ismanage,OrderID,usernum,siteID From " + Pre + "API_Qmenu Where ID=" + QID + " and UserNum = '" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }
        /// <summary>
        /// 检查数量
        /// </summary>
        /// <returns></returns>
        public DataTable QGetRecord(int num)
        {
            string Sql = "Select QmID From " + Pre + "API_Qmenu Where UserNum='" + Foosun.Global.Current.UserNum + "' and ismanage=" + num + " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 检查快捷菜单是否有重复ＩＤ
        /// </summary>
        /// <returns></returns>
        public DataTable QGetNumberRecord(string strNumber)
        {
            SqlParameter param = new SqlParameter("@strNumber", strNumber);
            string Sql = "Select Id From " + Pre + "API_Qmenu Where QmID=@strNumber";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        /// <summary>
        /// 插入快捷菜单新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void InsertQMenu(Foosun.Model.UserInfo8 uc2)
        {
            string Sql = "insert into " + Pre + "API_Qmenu (";
            Sql += "QmID,qName,FilePath,Ismanage,OrderID,usernum,SiteID";
            Sql += ") values (";
            Sql += "@QmID,@qName,@FilePath,@Ismanage,@OrderID,@usernum,'" + Foosun.Global.Current.SiteID + "')";
            SqlParameter[] parm = InsertQMenuParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新快捷菜单记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateQMenu(Foosun.Model.UserInfo8 uc2)
        {
            string Sql = "Update " + Pre + "API_Qmenu set qName=@qName,FilePath=@FilePath,OrderID=@OrderID where ID=" + uc2.Id + " and UserNum='" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            SqlParameter[] parm = InsertQMenuParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo8构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] InsertQMenuParameters(Foosun.Model.UserInfo8 uc1)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@QmID", SqlDbType.NVarChar, 12);
            param[0].Value = uc1.QmID;
            param[1] = new SqlParameter("@qName", SqlDbType.NVarChar, 50);
            param[1].Value = uc1.qName;
            param[2] = new SqlParameter("@FilePath", SqlDbType.NVarChar, 200);
            param[2].Value = uc1.FilePath;
            param[3] = new SqlParameter("@Ismanage", SqlDbType.TinyInt, 1);
            param[3].Value = uc1.Ismanage;
            param[4] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            param[4].Value = uc1.OrderID;
            param[5] = new SqlParameter("@usernum", SqlDbType.NVarChar, 15);
            param[5].Value = uc1.usernum;
            param[6] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[6].Value = uc1.SiteID;
            param[7] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[7].Value = uc1.Id;
            return param;
        }

        /// <summary>
        /// 获取UserInfo8构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] InsertQMenuParameters1(Foosun.Model.UserInfo8 uc1)
        {
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@qName", SqlDbType.NVarChar, 50);
            param[0].Value = uc1.qName;
            param[1] = new SqlParameter("@FilePath", SqlDbType.NVarChar, 200);
            param[1].Value = uc1.FilePath;
            param[2] = new SqlParameter("@OrderID", SqlDbType.Int, 4);
            param[2].Value = uc1.OrderID;
            param[3] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[3].Value = uc1.Id;
            return param;
        }
        #endregion 菜单部分

        #region 会员列表部分
        public DataTable getUserInfobase1(int Uid)
        {
            string Sql = "select UserGroupNumber,UserNum,NickName,RealName,birthday,Userinfo,UserFace,userFacesize,email from " + Pre + "sys_User where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getUserInfobase2(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserNum,Job,Nation,orgSch,character,UserFan,education,Lastschool,nativeplace from " + Pre + "sys_userfields where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable sexlist(int Uid)
        {
            string Sql = "select sex from " + Pre + "sys_User where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable marriagelist(int Uid)
        {
            string Sql = "select marriage from " + Pre + "sys_User where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable isopenlist(int Uid)
        {

            string Sql = "select isopen from " + Pre + "sys_User where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getUserInfoParam(int Uid)
        {
            string Sql = "select CharLenContent,CharHTML,CharTF from " + Pre + "user_group a," + Pre + "sys_user b  where b.id=" + Uid + " and b.UserGroupNumber=a.GroupNumber";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getUserInfoNum(int Uid)
        {
            string Sql = "select userNum from " + Pre + "sys_User where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getUserInfoRecord(string userNum)
        {
            SqlParameter param = new SqlParameter("@userNum", userNum);
            string Sql = "select id from " + Pre + "sys_userfields where UserNum=@userNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getPassWord(int Uid)
        {
            string Sql = "select PassQuestion,PassKey from " + Pre + "sys_User where ID=" + Uid + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getleves()
        {
            string Sql = "select LTitle,Lpicurl,iPoint from " + Pre + "sys_UserLevel order by id asc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        public void UpdateUserSafe(int Uid, string PassQuestion, string PassKey, string password)
        {
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PassQuestion", SqlDbType.NVarChar, 20);
            param[0].Value = PassQuestion;
            param[1] = new SqlParameter("@PassKey", SqlDbType.NVarChar, 20);
            param[1].Value = Foosun.Common.Input.MD5(PassKey);
            param[2] = new SqlParameter("@password", SqlDbType.NVarChar, 32);
            param[2].Value = Foosun.Common.Input.MD5(password);

            string str_sql = "Update " + Pre + "sys_User set PassQuestion=@PassQuestion,PassKey=@PassKey,UserPassword=@password where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }

        public void UpdateUserInfoIDCard(int Uid, string _temp)
        {
            string str_sql = "update " + Pre + "sys_user " + _temp + " where id=" + Uid + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }

        public DataTable userStatlist(int Uid)
        {
            string Sql = "select UserName,isIDcard,IDcardFiles from " + Pre + "sys_user where id=" + Uid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable idCardlist(int Uid)
        {
            string Sql = "select id,UserName,isIDcard,IDcardFiles from " + Pre + "sys_user where id=" + Uid + "" + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        public DataTable getUserContactRecord(string userNum)
        {
            SqlParameter param = new SqlParameter("@userNum", userNum);
            string Sql = "select id from " + Pre + "sys_userfields where UserNum=@userNum " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        public DataTable getUserInfoContact(string userNum)
        {
            SqlParameter param = new SqlParameter("@userNum", userNum);
            string Sql = "select province,City,Address,Postcode,FaTel,WorkTel,Fax,QQ,MSN from " + Pre + "sys_userfields where UserNum=@userNum " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getUserInfoBaseStat(int Uid)
        {
            string Sql = "select id,CertType,CertNumber,ipoint,gpoint,cpoint,epoint,apoint,RegTime,onlineTime,LoginNumber,LoginLimtNumber,lastIP,LastLoginTime,SiteID from " + Pre + "sys_user where id=" + Uid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getLockStat(int Uid)
        {
            string Sql = "select islock from " + Pre + "sys_user where id=" + Uid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到管理员状态
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns></returns>
        public DataTable getAdminsStat(int Uid)
        {
            string Sql = "select isadmin from " + Pre + "sys_user where id=" + Uid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        /// 得到是否是管理员
        /// </summary>
        /// <returns>1是，0否</returns>
        public int getisAdmin()
        {
            int intflg = 0;
            string Sql = "select isAdmin from " + Pre + "sys_User where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0) { intflg = int.Parse(dt.Rows[0]["isAdmin"].ToString()); }
                dt.Clear(); dt.Dispose();
            }
            return intflg;
        }

        public DataTable getGroupListStat(int Uid)
        {
            string Sql = "select UserGroupNumber from " + Pre + "sys_user where id=" + Uid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        public DataTable getCertsStat(int Uid)
        {
            string Sql = "select id,isIDcard from " + Pre + "sys_user where id=" + Uid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }



        /// <summary>
        /// 更新基本资料
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateUserInfoBase(Foosun.Model.UserInfo uc)
        {
            string str_sql = "Update " + Pre + "sys_User set NickName=@NickName,RealName=@RealName,sex=@sex,birthday=@birthday,Userinfo=@Userinfo,UserFace=@UserFace,userFacesize=@userFacesize,marriage=@marriage,isopen=@isopen,UserGroupNumber=@UserGroupNumber,email=@email where id=" + uc.Id + " " + Foosun.Common.Public.getSessionStr() + "";
            SqlParameter[] parm = GetUserInfoParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, parm);
        }

        /// <summary>
        /// 获取UserInfo构造
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private SqlParameter[] GetUserInfoParameters(Foosun.Model.UserInfo uc)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@NickName", SqlDbType.NVarChar, 12);
            param[0].Value = uc.NickName;
            param[1] = new SqlParameter("@RealName", SqlDbType.NVarChar, 20);
            param[1].Value = uc.RealName;
            param[2] = new SqlParameter("@sex", SqlDbType.TinyInt, 1);
            param[2].Value = uc.sex;
            param[3] = new SqlParameter("@birthday", SqlDbType.DateTime, 8);
            param[3].Value = uc.birthday;
            param[4] = new SqlParameter("@Userinfo", SqlDbType.NText);
            param[4].Value = uc.Userinfo;
            param[5] = new SqlParameter("@UserFace", SqlDbType.NVarChar, 220);
            param[5].Value = uc.UserFace;
            param[6] = new SqlParameter("@userFacesize", SqlDbType.NVarChar, 8);
            param[6].Value = uc.userFacesize;
            param[7] = new SqlParameter("@marriage", SqlDbType.TinyInt, 1);
            param[7].Value = uc.marriage;
            param[8] = new SqlParameter("@isopen", SqlDbType.TinyInt, 1);
            param[8].Value = uc.isopen;
            param[9] = new SqlParameter("@UserGroupNumber", SqlDbType.NVarChar, 12);
            param[9].Value = uc.UserGroupNumber;
            param[10] = new SqlParameter("@email", SqlDbType.NVarChar, 220);
            param[10].Value = uc.email;
            return param;
        }

        /// <summary>
        /// 更新基本资料第2表
        /// </summary>
        /// <param name="uc1"></param>
        public void UpdateUserInfoBase1(Foosun.Model.UserInfo1 uc1)
        {
            string str_sql = "Update " + Pre + "sys_userfields set Nation=@Nation,nativeplace=@nativeplace,character=@character,UserFan=@UserFan,orgSch=@orgSch,job=@job,education=@education,Lastschool=@Lastschool where UserNum=@UserNum";

            SqlParameter[] parm = GetUserInfoParameters1(uc1);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, parm);
        }

        /// <summary>
        /// 如果基本资料第2表，则插入新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateUserInfoBase2(Foosun.Model.UserInfo1 uc2)
        {
            string Sql = "insert into " + Pre + "sys_userfields (";
            Sql += "UserNum,Nation,nativeplace,character,UserFan,orgSch,job,education,Lastschool";
            Sql += ") values (";
            Sql += "@UserNum,@Nation,@nativeplace,@character,@UserFan,@orgSch,@job,@education,@Lastschool)";

            SqlParameter[] parm = GetUserInfoParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo1构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] GetUserInfoParameters1(Foosun.Model.UserInfo1 uc1)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@Nation", SqlDbType.NVarChar, 12);
            param[0].Value = uc1.Nation;
            param[1] = new SqlParameter("@nativeplace", SqlDbType.NVarChar, 20);
            param[1].Value = uc1.nativeplace;
            param[2] = new SqlParameter("@character", SqlDbType.NText);
            param[2].Value = uc1.character;
            param[3] = new SqlParameter("@UserFan", SqlDbType.NText);
            param[3].Value = uc1.UserFan;
            param[4] = new SqlParameter("@orgSch", SqlDbType.NVarChar, 10);
            param[4].Value = uc1.orgSch;
            param[5] = new SqlParameter("@job", SqlDbType.NVarChar, 30);
            param[5].Value = uc1.job;
            param[6] = new SqlParameter("@education", SqlDbType.NVarChar, 20);
            param[6].Value = uc1.education;
            param[7] = new SqlParameter("@Lastschool", SqlDbType.NVarChar, 80);
            param[7].Value = uc1.Lastschool;
            param[8] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 15);
            param[8].Value = uc1.UserNum;
            return param;
        }

        /// <summary>
        /// 更新基本资料第2表
        /// </summary>
        /// <param name="uc1"></param>
        public void UpdateUserInfoContact1(Foosun.Model.UserInfo2 uc1)
        {
            string str_sql = "Update " + Pre + "sys_userfields set province=@province,City=@City,Address=@Address,Postcode=@Postcode,FaTel=@FaTel,WorkTel=@WorkTel,Fax=@Fax,QQ=@QQ,MSN=@MSN where UserNum=@UserNum";
            SqlParameter[] parm = GetUserInfoContactParameters1(uc1);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, parm);
        }

        /// <summary>
        /// 如果基本资料第2表，则插入新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateUserInfoContact2(Foosun.Model.UserInfo2 uc2)
        {
            string Sql = "insert into " + Pre + "sys_userfields (";
            Sql += "UserNum,province,City,Address,Postcode,FaTel,WorkTel,Fax,QQ,MSN";
            Sql += ") values (";
            Sql += "@UserNum,@province,@City,@Address,@Postcode,@FaTel,@WorkTel,@Fax,@QQ,@MSN)";

            SqlParameter[] parm = GetUserInfoContactParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }


        private SqlParameter[] GetUserInfoContactParameters1(Foosun.Model.UserInfo2 uc1)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@province", SqlDbType.NVarChar, 20);
            param[0].Value = uc1.province;
            param[1] = new SqlParameter("@City", SqlDbType.NVarChar, 20);
            param[1].Value = uc1.City;
            param[2] = new SqlParameter("@Address", SqlDbType.NVarChar, 50);
            param[2].Value = uc1.Address;
            param[3] = new SqlParameter("@Postcode", SqlDbType.NVarChar, 10);
            param[3].Value = uc1.Postcode;
            param[4] = new SqlParameter("@FaTel", SqlDbType.NVarChar, 30);
            param[4].Value = uc1.FaTel;
            param[5] = new SqlParameter("@WorkTel", SqlDbType.NVarChar, 30);
            param[5].Value = uc1.WorkTel;
            param[6] = new SqlParameter("@Fax", SqlDbType.NVarChar, 30);
            param[6].Value = uc1.Fax;
            param[7] = new SqlParameter("@QQ", SqlDbType.NVarChar, 30);
            param[7].Value = uc1.QQ;
            param[8] = new SqlParameter("@MSN", SqlDbType.NVarChar, 150);
            param[8].Value = uc1.MSN;
            param[9] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 15);
            param[9].Value = uc1.UserNum;
            return param;
        }


        /// <summary>
        /// 如果基本资料状态表
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateUserInfoBaseStat(Foosun.Model.UserInfo3 uc)
        {
            string str_sql = "Update " + Pre + "sys_user set UserGroupNumber=@UserGroupNumber,islock=@islock,isadmin=@isadmin,CertType=@CertType,CertNumber=@CertNumber,ipoint=@ipoint,gpoint=@gpoint,cpoint=@cpoint,epoint=@epoint,apoint=@apoint,onlineTime=@onlineTime,RegTime=@RegTime,LastLoginTime=@LastLoginTime,LoginNumber=@LoginNumber,LoginLimtNumber=@LoginLimtNumber,lastIP=@lastIP,SiteID=@SiteID where Id=" + uc.Id + "";
            SqlParameter[] parm = GetUserInfoBaseStatParameters1(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, parm);
        }


        private SqlParameter[] GetUserInfoBaseStatParameters1(Foosun.Model.UserInfo3 uc1)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@UserGroupNumber", SqlDbType.NVarChar, 12);
            param[0].Value = uc1.UserGroupNumber;
            param[1] = new SqlParameter("@islock", SqlDbType.TinyInt, 1);
            param[1].Value = uc1.islock;
            param[2] = new SqlParameter("@isadmin", SqlDbType.TinyInt, 1);
            param[2].Value = uc1.isadmin;
            param[3] = new SqlParameter("@CertType", SqlDbType.NVarChar, 15);
            param[3].Value = uc1.CertType;
            param[4] = new SqlParameter("@CertNumber", SqlDbType.NVarChar, 20);
            param[4].Value = uc1.CertNumber;
            param[5] = new SqlParameter("@ipoint", SqlDbType.Int, 4);
            param[5].Value = uc1.ipoint;
            param[6] = new SqlParameter("@gpoint", SqlDbType.Int, 4);
            param[6].Value = uc1.gpoint;
            param[7] = new SqlParameter("@cpoint", SqlDbType.Int, 4);
            param[7].Value = uc1.cpoint;
            param[8] = new SqlParameter("@epoint", SqlDbType.Int, 4);
            param[8].Value = uc1.epoint;
            param[9] = new SqlParameter("@apoint", SqlDbType.Int, 4);
            param[9].Value = uc1.apoint;
            param[10] = new SqlParameter("@onlineTime", SqlDbType.Int, 4);
            param[10].Value = uc1.onlineTime;
            param[11] = new SqlParameter("@RegTime", SqlDbType.DateTime, 8);
            param[11].Value = uc1.RegTime;
            param[12] = new SqlParameter("@LastLoginTime", SqlDbType.DateTime, 8);
            param[12].Value = uc1.LastLoginTime;
            param[13] = new SqlParameter("@LoginNumber", SqlDbType.Int, 4);
            param[13].Value = uc1.LoginNumber;
            param[14] = new SqlParameter("@LoginLimtNumber", SqlDbType.Int, 4);
            param[14].Value = uc1.LoginLimtNumber;
            param[15] = new SqlParameter("@lastIP", SqlDbType.NVarChar, 16);
            param[15].Value = uc1.lastIP;
            param[16] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[16].Value = uc1.SiteID;
            return param;
        }
        /// <summary>
        /// 得到手机是否捆绑
        /// </summary>
        /// <returns></returns>
        public DataTable getMobileBindTF()
        {
            string Sql = "select BindTF from " + Pre + "sys_User where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        /// <summary>
        ///更新手机号码
        /// </summary>
        /// <param name="uc2"></param>
        public void updateMobile(string _MobileNumber, int BindTF)
        {
            SqlParameter param = new SqlParameter("@Mobile", _MobileNumber);
            string Sql = "Update " + Pre + "sys_User set mobile=@Mobile,BindTF=" + BindTF + " where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        #endregion 会员列表部分

        #region 会员组部分
        public void GroupDels(int Gid)
        {
            rootPublic pd = new rootPublic();
            //更新相应的会员数据会员组
            string SQL = "Update " + Pre + "sys_user set UserGroupNumber='0' where UserGroupNumber='" + pd.getGidGroupNumber(Gid) + "' " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, SQL, null);

            string str_sql = "Delete From  " + Pre + "user_Group where id=" + Gid + " " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, null);
        }


        public DataTable GroupListStr()
        {
            string Sql = "select id,GroupNumber,Discount,GroupName,iPoint,Gpoint,CreatTime,Rtime from " + Pre + "user_Group where 1=1 " + Foosun.Common.Public.getSessionStr() + " order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable GetGroupRecord(string UserGroupNumber)
        {
            SqlParameter param = new SqlParameter("@UserGroupNumber", UserGroupNumber);
            string Sql = "select id from " + Pre + "sys_user where UserGroupNumber=@UserGroupNumber";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable GetGroupNumber(string UserGroupNumber)
        {
            SqlParameter param = new SqlParameter("@UserGroupNumber", UserGroupNumber);
            string Sql = "select GroupNumber from " + Pre + "user_Group where GroupNumber=@UserGroupNumber";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        /// <summary>
        /// 插入新会员组
        /// </summary>
        /// <param name="uc2"></param>
        public void InsertGroup(Foosun.Model.UserInfo4 uc2)
        {
            string Sql = "Insert Into " + Pre + "user_Group(GroupNumber,GroupName,iPoint,Gpoint,Rtime,LenCommContent,CommCheckTF,PostCommTime,upfileType,upfileNum,upfileSize,DayUpfilenum,ContrNum,DicussTF,PostTitle,ReadUser,MessageNum,MessageGroupNum,IsCert,CharTF,CharHTML,CharLenContent,RegMinute,PostTitleHTML,DelSelfTitle,DelOTitle,EditSelfTitle,EditOtitle,ReadTitle,MoveSelfTitle,MoveOTitle,TopTitle,GoodTitle,LockUser,UserFlag,CheckTtile,IPTF,EncUser,OCTF,StyleTF,UpfaceSize,GIChange,GTChageRate,LoginPoint,RegPoint,GroupTF,GroupSize,GroupPerNum,GroupCreatNum,CreatTime,siteID,Discount";
            Sql += ") Values(";
            Sql += "@GroupNumber,@GroupName,@iPoint,@Gpoint,@Rtime,@LenCommContent,@CommCheckTF,@PostCommTime,@upfileType,@upfileNum,@upfileSize,@DayUpfilenum,@ContrNum,@DicussTF,@PostTitle,@ReadUser,@MessageNum,@MessageGroupNum,@IsCert,@CharTF,@CharHTML,@CharLenContent,@RegMinute,@PostTitleHTML,@DelSelfTitle,@DelOTitle,@EditSelfTitle,@EditOtitle,@ReadTitle,@MoveSelfTitle,@MoveOTitle,@TopTitle,@GoodTitle,@LockUser,@UserFlag,@CheckTtile,@IPTF,@EncUser,@OCTF,@StyleTF,@UpfaceSize,@GIChange,@GTChageRate,@LoginPoint,@RegPoint,@GroupTF,@GroupSize,@GroupPerNum,@GroupCreatNum,@CreatTime,@siteID,@Discount)";

            SqlParameter[] parm = InsertGroupParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo4构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] InsertGroupParameters(Foosun.Model.UserInfo4 uc1)
        {
            SqlParameter[] param = new SqlParameter[52];
            param[0] = new SqlParameter("@SiteID", SqlDbType.NVarChar, 12);
            param[0].Value = uc1.SiteID;
            param[1] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 12);
            param[1].Value = uc1.GroupNumber;
            param[2] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 50);
            param[2].Value = uc1.GroupName;
            param[3] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[3].Value = uc1.iPoint;
            param[4] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[4].Value = uc1.Gpoint;
            param[5] = new SqlParameter("@Rtime", SqlDbType.Int, 4);
            param[5].Value = uc1.Rtime;
            param[6] = new SqlParameter("@LenCommContent", SqlDbType.Int, 4);
            param[6].Value = uc1.LenCommContent;
            param[7] = new SqlParameter("@CommCheckTF", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.CommCheckTF;
            param[8] = new SqlParameter("@PostCommTime", SqlDbType.Int, 4);
            param[8].Value = uc1.PostCommTime;
            param[9] = new SqlParameter("@upfileType", SqlDbType.NVarChar, 200);
            param[9].Value = uc1.upfileType;
            param[10] = new SqlParameter("@upfileNum", SqlDbType.Int, 4);
            param[10].Value = uc1.upfileNum;
            param[11] = new SqlParameter("@upfileSize", SqlDbType.Int, 4);
            param[11].Value = uc1.upfileSize;
            param[12] = new SqlParameter("@DayUpfilenum", SqlDbType.Int, 4);
            param[12].Value = uc1.DayUpfilenum;
            param[13] = new SqlParameter("@ContrNum", SqlDbType.Int, 4);
            param[13].Value = uc1.ContrNum;
            param[14] = new SqlParameter("@DicussTF", SqlDbType.TinyInt, 1);
            param[14].Value = uc1.DicussTF;
            param[15] = new SqlParameter("@PostTitle", SqlDbType.TinyInt, 1);
            param[15].Value = uc1.PostTitle;
            param[16] = new SqlParameter("@ReadUser", SqlDbType.TinyInt, 1);
            param[16].Value = uc1.ReadUser;
            param[17] = new SqlParameter("@MessageNum", SqlDbType.Int, 4);
            param[17].Value = uc1.MessageNum;
            param[18] = new SqlParameter("@MessageGroupNum", SqlDbType.NVarChar, 15);
            param[18].Value = uc1.MessageGroupNum;
            param[19] = new SqlParameter("@IsCert", SqlDbType.TinyInt, 1);
            param[19].Value = uc1.IsCert;
            param[20] = new SqlParameter("@CharTF", SqlDbType.TinyInt, 1);
            param[20].Value = uc1.CharTF;
            param[21] = new SqlParameter("@CharHTML", SqlDbType.TinyInt, 1);
            param[21].Value = uc1.CharHTML;
            param[22] = new SqlParameter("@CharLenContent", SqlDbType.Int, 4);
            param[22].Value = uc1.CharLenContent;
            param[23] = new SqlParameter("@RegMinute", SqlDbType.Int, 4);
            param[23].Value = uc1.RegMinute;
            param[24] = new SqlParameter("@PostTitleHTML", SqlDbType.TinyInt, 1);
            param[24].Value = uc1.PostTitleHTML;
            param[25] = new SqlParameter("@DelSelfTitle", SqlDbType.TinyInt, 1);
            param[25].Value = uc1.DelSelfTitle;
            param[26] = new SqlParameter("@DelOTitle", SqlDbType.TinyInt, 1);
            param[26].Value = uc1.DelOTitle;
            param[27] = new SqlParameter("@EditSelfTitle", SqlDbType.TinyInt, 1);
            param[27].Value = uc1.EditSelfTitle;
            param[28] = new SqlParameter("@EditOtitle", SqlDbType.TinyInt, 1);
            param[28].Value = uc1.EditOtitle;
            param[29] = new SqlParameter("@ReadTitle", SqlDbType.TinyInt, 1);
            param[29].Value = uc1.ReadTitle;
            param[30] = new SqlParameter("@MoveSelfTitle", SqlDbType.TinyInt, 1);
            param[30].Value = uc1.MoveSelfTitle;
            param[31] = new SqlParameter("@MoveOTitle", SqlDbType.TinyInt, 1);
            param[31].Value = uc1.MoveOTitle;
            param[32] = new SqlParameter("@TopTitle", SqlDbType.TinyInt, 1);
            param[32].Value = uc1.TopTitle;
            param[33] = new SqlParameter("@GoodTitle", SqlDbType.TinyInt, 1);
            param[33].Value = uc1.GoodTitle;
            param[34] = new SqlParameter("@LockUser", SqlDbType.TinyInt, 1);
            param[34].Value = uc1.LockUser;

            param[35] = new SqlParameter("@UserFlag", SqlDbType.NVarChar, 100);
            param[35].Value = uc1.UserFlag;
            param[36] = new SqlParameter("@CheckTtile", SqlDbType.TinyInt, 1);
            param[36].Value = uc1.CheckTtile;
            param[37] = new SqlParameter("@IPTF", SqlDbType.TinyInt, 1);
            param[37].Value = uc1.IPTF;
            param[38] = new SqlParameter("@EncUser", SqlDbType.TinyInt, 1);
            param[38].Value = uc1.EncUser;
            param[39] = new SqlParameter("@OCTF", SqlDbType.TinyInt, 1);
            param[39].Value = uc1.OCTF;
            param[40] = new SqlParameter("@StyleTF", SqlDbType.TinyInt, 1);
            param[40].Value = uc1.StyleTF;
            param[41] = new SqlParameter("@UpfaceSize", SqlDbType.Int, 4);
            param[41].Value = uc1.UpfaceSize;


            param[42] = new SqlParameter("@GIChange", SqlDbType.NVarChar, 10);
            param[42].Value = uc1.GIChange;
            param[43] = new SqlParameter("@GTChageRate", SqlDbType.NVarChar, 30);
            param[43].Value = uc1.GTChageRate;
            param[44] = new SqlParameter("@LoginPoint", SqlDbType.NVarChar, 20);
            param[44].Value = uc1.LoginPoint;
            param[45] = new SqlParameter("@RegPoint", SqlDbType.NVarChar, 20);
            param[45].Value = uc1.RegPoint;
            param[46] = new SqlParameter("@GroupTF", SqlDbType.TinyInt, 1);
            param[46].Value = uc1.GroupTF;


            param[47] = new SqlParameter("@GroupSize", SqlDbType.Int, 4);
            param[47].Value = uc1.GroupSize;
            param[48] = new SqlParameter("@GroupPerNum", SqlDbType.Int, 4);
            param[48].Value = uc1.GroupPerNum;
            param[49] = new SqlParameter("@GroupCreatNum", SqlDbType.Int, 4);
            param[49].Value = uc1.GroupCreatNum;
            param[50] = new SqlParameter("@CreatTime", SqlDbType.DateTime, 8);
            param[50].Value = uc1.CreatTime;
            param[51] = new SqlParameter("@Discount", SqlDbType.Float, 8);
            param[51].Value = uc1.Discount;

            return param;
        }

        public DataTable getGroupEdit(int Gid)
        {
            string Sql = "select id,GroupNumber,GroupName,iPoint,Gpoint,Rtime,LenCommContent,CommCheckTF,PostCommTime,upfileType,upfileNum,upfileSize,DayUpfilenum,ContrNum,DicussTF,PostTitle,ReadUser,MessageNum,MessageGroupNum,IsCert,CharTF,CharHTML,CharLenContent,RegMinute,PostTitleHTML,DelSelfTitle,DelOTitle,EditSelfTitle,EditOtitle,ReadTitle,MoveSelfTitle,MoveOTitle,TopTitle,GoodTitle,LockUser,UserFlag,CheckTtile,IPTF,EncUser,OCTF,StyleTF,UpfaceSize,GIChange,GTChageRate,LoginPoint,RegPoint,GroupTF,GroupSize,GroupPerNum,GroupCreatNum,CreatTime,siteID,Discount from " + Pre + "user_Group where id=" + Gid + "" + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        /// <summary>
        /// 更新新会员组
        /// </summary>
        /// <param name="uc2"></param>
        public void updateGroupEdit(Foosun.Model.UserInfo4 uc2)
        {
            string Sql = "Update " + Pre + "user_Group set GroupName=@GroupName,iPoint=@iPoint,Gpoint=@Gpoint,Rtime=@Rtime,LenCommContent=@LenCommContent,CommCheckTF=@CommCheckTF,PostCommTime=@PostCommTime,upfileType=@upfileType,upfileNum=@upfileNum,upfileSize=@upfileSize,DayUpfilenum=@DayUpfilenum,ContrNum=@ContrNum,DicussTF=@DicussTF,PostTitle=@PostTitle,ReadUser=@ReadUser,MessageNum=@MessageNum,MessageGroupNum=@MessageGroupNum,IsCert=@IsCert,CharTF=@CharTF,CharHTML=@CharHTML,CharLenContent=@CharLenContent,RegMinute=@RegMinute,PostTitleHTML=@PostTitleHTML,DelSelfTitle=@DelSelfTitle,DelOTitle=@DelOTitle,EditSelfTitle=@EditSelfTitle,EditOtitle=@EditOtitle,ReadTitle=@ReadTitle,MoveSelfTitle=@MoveSelfTitle,MoveOTitle=@MoveOTitle,TopTitle=@TopTitle,GoodTitle=@GoodTitle,LockUser=@LockUser,UserFlag=@UserFlag,CheckTtile=@CheckTtile,IPTF=@IPTF,EncUser=@EncUser,OCTF=@OCTF,StyleTF=@StyleTF,UpfaceSize=@UpfaceSize,GIChange=@GIChange,GTChageRate=@GTChageRate,LoginPoint=@LoginPoint,RegPoint=@RegPoint,GroupTF=@GroupTF,GroupSize=@GroupSize,GroupPerNum=@GroupPerNum,GroupCreatNum=@GroupCreatNum,Discount=@Discount where id=" + uc2.gID + "";
            SqlParameter[] parm = updateGroupEditParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo4构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] updateGroupEditParameters(Foosun.Model.UserInfo4 uc1)
        {
            SqlParameter[] param = new SqlParameter[50];
            param[0] = new SqlParameter("@gID", SqlDbType.Int, 4);
            param[0].Value = uc1.gID;
            param[1] = new SqlParameter("@GroupCreatNum", SqlDbType.Int, 4);
            param[1].Value = uc1.GroupCreatNum;
            param[2] = new SqlParameter("@GroupName", SqlDbType.NVarChar, 50);
            param[2].Value = uc1.GroupName;
            param[3] = new SqlParameter("@iPoint", SqlDbType.Int, 4);
            param[3].Value = uc1.iPoint;
            param[4] = new SqlParameter("@Gpoint", SqlDbType.Int, 4);
            param[4].Value = uc1.Gpoint;
            param[5] = new SqlParameter("@Rtime", SqlDbType.Int, 4);
            param[5].Value = uc1.Rtime;
            param[6] = new SqlParameter("@LenCommContent", SqlDbType.Int, 4);
            param[6].Value = uc1.LenCommContent;
            param[7] = new SqlParameter("@CommCheckTF", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.CommCheckTF;
            param[8] = new SqlParameter("@PostCommTime", SqlDbType.Int, 4);
            param[8].Value = uc1.PostCommTime;
            param[9] = new SqlParameter("@upfileType", SqlDbType.NVarChar, 200);
            param[9].Value = uc1.upfileType;
            param[10] = new SqlParameter("@upfileNum", SqlDbType.Int, 4);
            param[10].Value = uc1.upfileNum;
            param[11] = new SqlParameter("@upfileSize", SqlDbType.Int, 4);
            param[11].Value = uc1.upfileSize;
            param[12] = new SqlParameter("@DayUpfilenum", SqlDbType.Int, 4);
            param[12].Value = uc1.DayUpfilenum;
            param[13] = new SqlParameter("@ContrNum", SqlDbType.Int, 4);
            param[13].Value = uc1.ContrNum;
            param[14] = new SqlParameter("@DicussTF", SqlDbType.TinyInt, 1);
            param[14].Value = uc1.DicussTF;
            param[15] = new SqlParameter("@PostTitle", SqlDbType.TinyInt, 1);
            param[15].Value = uc1.PostTitle;
            param[16] = new SqlParameter("@ReadUser", SqlDbType.TinyInt, 1);
            param[16].Value = uc1.ReadUser;
            param[17] = new SqlParameter("@MessageNum", SqlDbType.Int, 4);
            param[17].Value = uc1.MessageNum;
            param[18] = new SqlParameter("@MessageGroupNum", SqlDbType.NVarChar, 15);
            param[18].Value = uc1.MessageGroupNum;
            param[19] = new SqlParameter("@IsCert", SqlDbType.TinyInt, 1);
            param[19].Value = uc1.IsCert;
            param[20] = new SqlParameter("@CharTF", SqlDbType.TinyInt, 1);
            param[20].Value = uc1.CharTF;
            param[21] = new SqlParameter("@CharHTML", SqlDbType.TinyInt, 1);
            param[21].Value = uc1.CharHTML;
            param[22] = new SqlParameter("@CharLenContent", SqlDbType.Int, 4);
            param[22].Value = uc1.CharLenContent;
            param[23] = new SqlParameter("@RegMinute", SqlDbType.Int, 4);
            param[23].Value = uc1.RegMinute;
            param[24] = new SqlParameter("@PostTitleHTML", SqlDbType.TinyInt, 1);
            param[24].Value = uc1.PostTitleHTML;
            param[25] = new SqlParameter("@DelSelfTitle", SqlDbType.TinyInt, 1);
            param[25].Value = uc1.DelSelfTitle;
            param[26] = new SqlParameter("@DelOTitle", SqlDbType.TinyInt, 1);
            param[26].Value = uc1.DelOTitle;
            param[27] = new SqlParameter("@EditSelfTitle", SqlDbType.TinyInt, 1);
            param[27].Value = uc1.EditSelfTitle;
            param[28] = new SqlParameter("@EditOtitle", SqlDbType.TinyInt, 1);
            param[28].Value = uc1.EditOtitle;
            param[29] = new SqlParameter("@ReadTitle", SqlDbType.TinyInt, 1);
            param[29].Value = uc1.ReadTitle;
            param[30] = new SqlParameter("@MoveSelfTitle", SqlDbType.TinyInt, 1);
            param[30].Value = uc1.MoveSelfTitle;
            param[31] = new SqlParameter("@MoveOTitle", SqlDbType.TinyInt, 1);
            param[31].Value = uc1.MoveOTitle;
            param[32] = new SqlParameter("@TopTitle", SqlDbType.TinyInt, 1);
            param[32].Value = uc1.TopTitle;
            param[33] = new SqlParameter("@GoodTitle", SqlDbType.TinyInt, 1);
            param[33].Value = uc1.GoodTitle;
            param[34] = new SqlParameter("@LockUser", SqlDbType.TinyInt, 1);
            param[34].Value = uc1.LockUser;

            param[35] = new SqlParameter("@UserFlag", SqlDbType.NVarChar, 100);
            param[35].Value = uc1.UserFlag;
            param[36] = new SqlParameter("@CheckTtile", SqlDbType.TinyInt, 1);
            param[36].Value = uc1.CheckTtile;
            param[37] = new SqlParameter("@IPTF", SqlDbType.TinyInt, 1);
            param[37].Value = uc1.IPTF;
            param[38] = new SqlParameter("@EncUser", SqlDbType.TinyInt, 1);
            param[38].Value = uc1.EncUser;
            param[39] = new SqlParameter("@OCTF", SqlDbType.TinyInt, 1);
            param[39].Value = uc1.OCTF;
            param[40] = new SqlParameter("@StyleTF", SqlDbType.TinyInt, 1);
            param[40].Value = uc1.StyleTF;
            param[41] = new SqlParameter("@UpfaceSize", SqlDbType.Int, 4);
            param[41].Value = uc1.UpfaceSize;


            param[42] = new SqlParameter("@GIChange", SqlDbType.NVarChar, 10);
            param[42].Value = uc1.GIChange;
            param[43] = new SqlParameter("@GTChageRate", SqlDbType.NVarChar, 30);
            param[43].Value = uc1.GTChageRate;
            param[44] = new SqlParameter("@LoginPoint", SqlDbType.NVarChar, 20);
            param[44].Value = uc1.LoginPoint;
            param[45] = new SqlParameter("@RegPoint", SqlDbType.NVarChar, 20);
            param[45].Value = uc1.RegPoint;
            param[46] = new SqlParameter("@GroupTF", SqlDbType.TinyInt, 1);
            param[46].Value = uc1.GroupTF;


            param[47] = new SqlParameter("@GroupSize", SqlDbType.Int, 4);
            param[47].Value = uc1.GroupSize;
            param[48] = new SqlParameter("@GroupPerNum", SqlDbType.Int, 4);
            param[48].Value = uc1.GroupPerNum;
            param[49] = new SqlParameter("@Discount", SqlDbType.Float, 8);
            param[49].Value = uc1.Discount;
            return param;
        }

        #endregion 会员组部分

        #region 公告部分
        public void Announcedels(string Aid)
        {
            string Sql = "Delete From  " + Pre + "user_news where id in(" + Aid + ") " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void AnnounceLockAction(string Aid, string lockstr)
        {
            string Sql = "update " + Pre + "user_news " + lockstr + " where id in(" + Aid + ") " + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 则插入新记录公告
        /// </summary>
        /// <param name="uc2"></param>
        public void InsertAnnounce(Foosun.Model.UserInfo5 uc2)
        {
            string Sql = "insert into " + Pre + "user_news (";
            Sql += "newsID,Title,content,creatTime,GroupNumber,getPoint,SiteId,isLock";
            Sql += ") values (";
            Sql += "@newsID,@Title,@content,@creatTime,@GroupNumber,@getPoint,@SiteId,0)";

            SqlParameter[] parm = GetAnnounceParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo5构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] GetAnnounceParameters(Foosun.Model.UserInfo5 uc1)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@newsID", SqlDbType.NVarChar, 12);
            param[0].Value = uc1.newsID;
            param[1] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            param[1].Value = uc1.Title;
            param[2] = new SqlParameter("@content", SqlDbType.NText);
            param[2].Value = uc1.content;
            param[3] = new SqlParameter("@creatTime", SqlDbType.DateTime, 8);
            param[3].Value = uc1.creatTime;
            param[4] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 12);
            param[4].Value = uc1.GroupNumber;
            param[5] = new SqlParameter("@getPoint", SqlDbType.NVarChar, 50);
            param[5].Value = uc1.getPoint;
            param[6] = new SqlParameter("@SiteId", SqlDbType.NVarChar, 12);
            param[6].Value = uc1.SiteId;
            param[7] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.isLock;
            param[8] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[8].Value = uc1.Id;
            return param;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateAnnounce(Foosun.Model.UserInfo5 uc2)
        {
            string Sql = "update " + Pre + "user_news set Title=@Title,content=@content,GroupNumber=@GroupNumber,getPoint=@getPoint where Id=" + uc2.Id + " " + Foosun.Common.Public.getSessionStr() + "";

            SqlParameter[] parm = UpdateAnnounceParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo5构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateAnnounceParameters(Foosun.Model.UserInfo5 uc1)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Title", SqlDbType.NVarChar, 50);
            param[0].Value = uc1.Title;
            param[1] = new SqlParameter("@content", SqlDbType.NText);
            param[1].Value = uc1.content;
            param[2] = new SqlParameter("@GroupNumber", SqlDbType.NVarChar, 12);
            param[2].Value = uc1.GroupNumber;
            param[3] = new SqlParameter("@getPoint", SqlDbType.NVarChar, 50);
            param[3].Value = uc1.getPoint;
            param[4] = new SqlParameter("@Id", SqlDbType.Int, 4);
            param[4].Value = uc1.Id;
            return param;
        }

        public DataTable getAnnounceEdit(int aid)
        {
            string Sql = "select id,title,content,getpoint,GroupNumber from " + Pre + "user_news where id=" + aid + " " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        #endregion 公告部分

        #region 点卡

        public void ICarddels(string iId)
        {
            string Sql = "Delete From  " + Pre + "user_card where id in(" + iId + ")" + Foosun.Common.Public.getSessionStr() + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void ICardLockAction(string iId, string lockstr)
        {
            string Sql = "";
            if (lockstr == "000000000")
            {
                string _Tmpstr = "";
                _Tmpstr = " set TimeOutDate='1900-1-1'";
                Sql = "update " + Pre + "user_card " + _Tmpstr + " where id in(" + iId + ") " + Foosun.Common.Public.getSessionStr() + "";
            }
            else
            {
                Sql = "update " + Pre + "user_card " + lockstr + " where id in(" + iId + ") " + Foosun.Common.Public.getSessionStr() + "";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public DataTable GetPage(string _islock, string _isuse, string _isbuy, string _timeout, string _SiteID, string cardnumber, string cardpassword, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {

            string QSQL = "";
            if (cardnumber != "" && cardnumber != null)
            {
                QSQL += " and CardNumber = '" + cardnumber.ToString() + "'";
            }

            if (cardpassword != "" && cardpassword != null)
            {
                QSQL += " and CardPassWord = '" + cardpassword.ToString() + "'";
            }

            if (_islock != "" && _islock != null)
            {
                QSQL += " and isLock = " + int.Parse(_islock.ToString()) + "";
            }
            if (_isuse != "" && _isuse != null)
            {
                QSQL += " and isUse = " + int.Parse(_isuse.ToString()) + "";
            }
            if (_isbuy != "" && _isbuy != null)
            {
                QSQL += " and isBuy = " + int.Parse(_isbuy.ToString()) + "";
            }
            if (_timeout != "" && _timeout != null)
            {
                if (_timeout.ToString() == "1")
                {
                    QSQL += " and TimeOutDate <= '" + System.DateTime.Now + "'";
                }
                else
                {
                    QSQL += " and TimeOutDate > '" + System.DateTime.Now + "'";
                }
            }
            if (_SiteID != "" && _SiteID != null)
            {
                if (Foosun.Global.Current.SiteID == "0")
                {
                    QSQL += " and SiteID='" + _SiteID + "'";
                }
                else
                {
                    QSQL += " and SiteID='" + Foosun.Global.Current.SiteID + "'";
                }
            }
            else
            {
                QSQL += " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            }

            string AllFields = "id,CaID,CardNumber,CardPassWord,creatTime,Money,Point,isBuy,isUse,isLock,UserNum,SiteId,TimeOutDate";
            string Condition = "" + Pre + "user_Card where 1=1 " + QSQL + "";
            string IndexField = "ID";
            string OrderFields = "order by Id Desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
        /// <summary>
        /// 得到编号是否重复
        /// </summary>
        /// <param name="CardNumber"></param>
        /// <returns></returns>
        public DataTable getCardNumberTF(string CardNumber)
        {
            SqlParameter param = new SqlParameter("@CardNumber", CardNumber);
            string Sql = "select CardNumber from " + Pre + "user_card where CardNumber=@CardNumber";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        /// <summary>
        /// 点卡密码是否重复
        /// </summary>
        /// <param name="CardPass"></param>
        /// <returns></returns>
        public bool getCardPassTF(string CardPass)
        {
            SqlParameter param = new SqlParameter("@CardPass", CardPass);
            bool flg = false;
            string Sql = "select id from " + Pre + "user_card where CardPassWord=@CardPass";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (rdr != null)
            {
                if (rdr.Rows.Count > 0)
                {
                    flg = true;
                }
                rdr.Clear(); rdr.Dispose();
            }
            return flg;
        }

        public void insertCardR(Foosun.Model.IDCARD uc)
        {
            string Sql = "Insert Into " + Pre + "user_card(CaID,CardNumber,CardPassWord,creatTime,[Money],Point,isBuy,isUse,isLock,UserNum,siteID,TimeOutDate) Values(@CaID,@CardNumber,@CardPassWord,@creatTime,@Money,@Point,@isBuy,@isUse,@isLock,@UserNum,@siteID,@TimeOutDate)";
            SqlParameter[] parm = insertCardRParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        public void UpdateCardR(Foosun.Model.IDCARD uc)
        {
            string Sql = "Update " + Pre + "user_card set CardPassWord=@CardPassWord,[Money]=@Money,Point=@Point,isBuy=@isBuy,isUse=@isUse,isLock=@isLock,TimeOutDate=@TimeOutDate where Id=" + uc.Id + " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            SqlParameter[] parm = UpdateCardRParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取IDCARD构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] insertCardRParameters(Foosun.Model.IDCARD uc1)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@CaID", SqlDbType.NVarChar, 12);
            param[0].Value = uc1.CaID;
            param[1] = new SqlParameter("@CardNumber", SqlDbType.NVarChar, 30);
            param[1].Value = uc1.CardNumber;
            param[2] = new SqlParameter("@CardPassWord", SqlDbType.NVarChar, 150);
            param[2].Value = uc1.CardPassWord;
            param[3] = new SqlParameter("@creatTime", SqlDbType.DateTime, 8);
            param[3].Value = uc1.creatTime;
            param[4] = new SqlParameter("@Money", SqlDbType.Int, 4);
            param[4].Value = uc1.Money;

            param[5] = new SqlParameter("@Point", SqlDbType.Int, 4);
            param[5].Value = uc1.Point;
            param[6] = new SqlParameter("@isBuy", SqlDbType.TinyInt, 1);
            param[6].Value = uc1.isBuy;
            param[7] = new SqlParameter("@isUse", SqlDbType.TinyInt, 1);
            param[7].Value = uc1.isUse;
            param[8] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[8].Value = uc1.isLock;

            param[9] = new SqlParameter("@UserNum", SqlDbType.NVarChar, 15);
            param[9].Value = uc1.UserNum;
            param[10] = new SqlParameter("@siteID", SqlDbType.NVarChar, 12);
            param[10].Value = uc1.siteID;

            param[11] = new SqlParameter("@TimeOutDate", SqlDbType.DateTime, 8);
            param[11].Value = uc1.TimeOutDate;

            param[12] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[12].Value = uc1.Id;

            return param;
        }
        /// <summary>
        /// 获取IDCARD1构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateCardRParameters(Foosun.Model.IDCARD uc1)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CardPassWord", SqlDbType.NVarChar, 150);
            param[0].Value = uc1.CardPassWord;
            param[1] = new SqlParameter("@Money", SqlDbType.Int, 4);
            param[1].Value = uc1.Money;

            param[2] = new SqlParameter("@Point", SqlDbType.Int, 4);
            param[2].Value = uc1.Point;
            param[3] = new SqlParameter("@isBuy", SqlDbType.TinyInt, 1);
            param[3].Value = uc1.isBuy;
            param[4] = new SqlParameter("@isUse", SqlDbType.TinyInt, 1);
            param[4].Value = uc1.isUse;
            param[5] = new SqlParameter("@isLock", SqlDbType.TinyInt, 1);
            param[5].Value = uc1.isLock;
            param[6] = new SqlParameter("@TimeOutDate", SqlDbType.DateTime, 8);
            param[6].Value = uc1.TimeOutDate;

            param[7] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[7].Value = uc1.Id;

            return param;
        }
        /// <summary>
        /// 删除所有点卡
        /// </summary>
        public void delALLCARD()
        {
            string Sql = "Delete From  " + Pre + "user_card where SiteID = " + Foosun.Global.Current.SiteID + "";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable getmoneylist()
        {
            string Sql = "select DisTinct Money from " + Pre + "user_Card where isBuy=0 and isUse=0 and isLock=0 and SiteID='" + Foosun.Global.Current.SiteID + "' and TimeOutDate>'" + DateTime.Now + "' and Money>0 order by Money asc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getCardInfoID(int ID)
        {
            string Sql = "select id,CardNumber,CardPassWord,Money,Point,TimeOutDate,isLock,isUse,isBuy From " + Pre + "user_card where id=" + ID + " and SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }



        #endregion 点卡

        #region 在线支付开始

        public DataTable getOnlinePay()
        {
            string Sql = "select onpayType,O_userName,O_key,O_sendurl,O_returnurl,O_md5,O_other1,O_other2,O_other3 from " + Pre + "sys_PramUser where SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateOnlinePay(Foosun.Model.UserInfo6 uc2)
        {
            string Sql = "";
            string SQLTF = "select ID from " + Pre + "sys_PramUser where SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable dt = DbHelper.ExecuteTable(CommandType.Text, SQLTF, null);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Sql = "Update " + Pre + "sys_PramUser set onpayType=@onpayType,O_userName=@O_userName,o_key=@o_key,O_sendurl=@O_sendurl,O_returnurl=@O_returnurl,O_md5=@O_md5,O_other1=@O_other1,O_other2=@O_other2,O_other3=@O_other3 where SiteID='" + Foosun.Global.Current.SiteID + "'";
                }
                else
                {
                    Sql = "Insert Into " + Pre + "sys_PramUser(onpayType,O_userName,o_key,O_sendurl,O_returnurl,O_md5,O_other1,O_other2,O_other3,SiteID) Values(@onpayType,@O_userName,@o_key,@O_sendurl,@O_returnurl,@O_md5,@O_other1,@O_other2,@O_other3,'" + Foosun.Global.Current.SiteID + "')";
                }
            }
            SqlParameter[] parm = UpdateOnlinePayParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo6构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private SqlParameter[] UpdateOnlinePayParameters(Foosun.Model.UserInfo6 uc1)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@o_userName", SqlDbType.NVarChar, 100);
            param[0].Value = uc1.O_userName;
            param[1] = new SqlParameter("@o_key", SqlDbType.NVarChar, 128);
            param[1].Value = uc1.O_key;
            param[2] = new SqlParameter("@o_sendurl", SqlDbType.NVarChar, 220);
            param[2].Value = uc1.O_sendurl;
            param[3] = new SqlParameter("@o_returnurl", SqlDbType.NVarChar, 220);
            param[3].Value = uc1.O_returnurl;
            param[4] = new SqlParameter("@o_md5", SqlDbType.NVarChar, 128);
            param[4].Value = uc1.O_md5;
            param[5] = new SqlParameter("@o_other1", SqlDbType.NVarChar, 220);
            param[5].Value = uc1.O_other1;
            param[6] = new SqlParameter("@o_other2", SqlDbType.NVarChar, 220);
            param[6].Value = uc1.O_other2;
            param[7] = new SqlParameter("@o_other3", SqlDbType.NVarChar, 220);
            param[7].Value = uc1.O_other3;
            param[8] = new SqlParameter("@Id", SqlDbType.NVarChar, 128);
            param[8].Value = uc1.Id;
            param[8] = new SqlParameter("@onpayType", SqlDbType.TinyInt, 1);
            param[8].Value = uc1.onpayType;
            return param;
        }


        #endregion 在线支付结束

        #region 会员前台部分

        public DataTable getUserUserNumRecord(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "Select UserName,NickName,RealName,Sex,birthday,UserFace,userFacesize,marriage,Userinfo,UserGroupNumber,iPoint,gPoint,cPoint,ePoint,aPoint,RegTime,OnlineTime,OnlineTF,LoginNumber,Mobile,BindTF,PassQuestion,PassKey,CertType,CertNumber,Email,isOpen,isLock From " + Pre + "sys_User where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public string getUserGChange(string GroupNumber)
        {
            SqlParameter param = new SqlParameter("@GroupNumber", GroupNumber);
            string Sql = "Select GIChange From " + Pre + "user_Group  where GroupNumber=@GroupNumber";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }


        public DataTable getUserUserfields(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select * from " + Pre + "sys_userfields where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        public DataTable getUserInfobase1_user(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select UserNum,NickName,RealName,birthday,Userinfo,UserFace,userFacesize,email from " + Pre + "sys_User where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getUserInfobase2_user(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select Nation,nativeplace,character,orgSch,job,education,Lastschool,UserFan from " + Pre + "sys_userfields where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        public int getPasswordTF(string password)
        {
            string md5Pwd = Foosun.Common.Input.MD5(password, true);
            SqlParameter param = new SqlParameter("@password", md5Pwd);
            int flg = 1;
            string Sql = "select UserPassword from " + Pre + "sys_User where UserNum='" + Foosun.Global.Current.UserNum + "' and UserPassword=@password";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            if (rdr != null)
            {
                if (rdr.Rows.Count > 0)
                {
                    flg = 0;
                }
            }
            rdr.Clear();
            return flg;
        }

        public DataTable getICardTF()
        {
            string Sql = "select isIDcard,IDcardFiles,ID from " + Pre + "sys_User where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }


        /// <summary>
        /// 取消认证
        /// </summary>
        /// <param name="uc2"></param>
        public void ResetICard()
        {
            string Sql = "update  " + Pre + "sys_User set IDcardFiles='',isIDcard=0 where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 保存上传图片
        /// </summary>
        /// <param name="uc2"></param>
        public void SaveDataICard(string f_IDcardFiles)
        {
            SqlParameter param = new SqlParameter("@f_IDcardFiles", f_IDcardFiles);
            string Sql = "update " + Pre + "sys_User set IDcardFiles=@f_IDcardFiles,isIDcard=0 where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        #endregion 会员前台部分结束

        public string sel_pic(string PhotoalbumID)
        {
            SqlParameter param = new SqlParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "select top 1 PhotoUrl from " + Pre + "user_Photo where PhotoalbumID=@PhotoalbumID order by id desc";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int sel_picnum(string PhotoalbumID)
        {
            SqlParameter param = new SqlParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "select count(id) from " + Pre + "user_Photo where PhotoalbumID=@PhotoalbumID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        #region 投稿
        public DataTable getConstrClass(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select ID,Ccid,cName,Content from " + Pre + "user_ConstrClass where UserNum=@UserNum order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getConstrID(string ConID, string UserNum)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@ConID", ConID), new SqlParameter("@UserNum", UserNum) };
            string Sql = "select ID,Title,Content,creatTime,Source,Tags,Author,PicURL from " + Pre + "user_Constr where UserNum=@UserNum and ConID=@ConID and  isuserdel=0 and SiteID='" + Foosun.Global.Current.SiteID + "' order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        #endregion 投稿

        public string getAdminPopandSupper(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string strflg = "0|foosun";
            string Sql = "select isSuper,PopList from " + Pre + "sys_admin where UserNum=@UserNum";
            IDataReader rd = DbHelper.ExecuteReader(CommandType.Text, Sql, param);
            if (rd.Read())
            {
                strflg = rd.GetByte(0).ToString() + "|";
                if (!rd.IsDBNull(1))
                    strflg += rd.GetString(1);
                strflg += "foosun";
            }
            rd.Close();
            return strflg;
        }

        //URL
        public void updateURL(string URLName, string URL, string URLColor, string ClassID, string Content, int NUM, int ID)
        {
            SqlParameter[] param = new SqlParameter[] { new SqlParameter("@URLName", URLName), new SqlParameter("@URL", URL), new SqlParameter("@URLColor", URLColor), new SqlParameter("@ClassID", ClassID), new SqlParameter("@Content", Content) };
            string Sql = "";
            if (NUM == 0)
            {
                Sql = "Insert Into " + Pre + "user_URL(URLName,URL,URLColor,ClassID,Content,CreatTime,UserNum) Values(@URLName,@URL,@URLColor,@ClassID,@Content,'" + DateTime.Now + "','" + Foosun.Global.Current.UserNum + "')";
            }
            else
            {
                Sql = "update " + Pre + "user_URL set URLName=@URLName,URL=@URL,URLColor=@URLColor,ClassID=@ClassID,Content=@Content where id=" + ID + " and UserNum='" + Foosun.Global.Current.UserNum + "'";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public void updateClass(string ClassName, int NUM, int ID)
        {
            SqlParameter param = new SqlParameter("@ClassName", ClassName);
            string Sql = "";
            if (NUM == 0)
            {
                Sql = "Insert Into " + Pre + "user_URLClass(ClassName,ParentID,UserNum) Values(@ClassName,0,'" + Foosun.Global.Current.UserNum + "')";
            }
            else
            {
                Sql = "update " + Pre + "user_URLClass set ClassName=@ClassName where id=" + ID + " and UserNum='" + Foosun.Global.Current.UserNum + "'";
            }
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        public DataTable getURL(int ID)
        {
            string Sql = "select * from " + Pre + "user_URL where ID=" + ID + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public void delURL(int ID)
        {
            string Sql = "delete from " + Pre + "user_URL where ID =" + ID + " and UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void delclass(int ID)
        {
            string Sql = "delete from " + Pre + "user_URLClass where ID =" + ID + " and UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
            string Sql1 = "delete from " + Pre + "user_URL where ClassID =" + ID + " and UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql1, null);
        }

        public DataTable getClassList(string UserNum)
        {
            SqlParameter param = new SqlParameter("@UserNum", UserNum);
            string Sql = "select ID,ClassName from " + Pre + "user_URLClass where UserNum=@UserNum order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getClassURLList(int ClassID)
        {
            string Sql = "select * from " + Pre + "user_URL where ClassID=" + ClassID + " order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public DataTable getClassInfo(int ID)
        {
            string Sql = "select * from " + Pre + "user_URLClass where ID=" + ID + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, null);
            return rdr;
        }

        public string GetUserLogs(int ID)
        {
            SqlParameter param = new SqlParameter("@ID", ID);
            string sql = "select content from " + Pre + "user_userlogs where ID=@ID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }
        #region API
        public IDataReader GetUserAPiInfo(string UserName)
        {
            SqlParameter param = new SqlParameter("@UserName", UserName);
            string sql = "select UserNum,UserName,UserPassword,NickName,RealName,isAdmin,UserGroupNumber,PassQuestion,PassKey from " + Pre + "sys_user where UserName=@UserName";
            return DbHelper.ExecuteReader(CommandType.Text, sql, param);

        }
        #endregion API

        #region IUserMisc 成员

        /// <summary>
        /// 判断一个用户是否存在
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool ExistsUser(string username)
        {
            SqlParameter param = new SqlParameter("@UserName", username);
            string sql = "select count(Id) from " + Pre + "sys_user where UserName=@UserName";
            int count=Convert.ToInt32( DbHelper.ExecuteScalar(CommandType.Text, sql, param));
            if (count > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        public SysUserInfo GetUserInfo(string username)
        {
            SysUserInfo userInfo = null;
            SqlParameter param = new SqlParameter("@UserName", username);
            string sql = "select * from " + Pre + "sys_user where UserName=@UserName";
            DbDataReader reader = DbHelper.ExecuteReader(CommandType.Text, sql, param);
            if (reader.Read())
            {
                int i;
                userInfo = new SysUserInfo();

                i = reader.GetOrdinal("Addfriend");
                if (!reader.IsDBNull(i))
                    userInfo.Addfriend = reader.GetInt32(i);

                i = reader.GetOrdinal("Addfriendbs");
                if (!reader.IsDBNull(i))
                    userInfo.Addfriendbs = reader.GetByte(i);

                i = reader.GetOrdinal("aPoint");
                if (!reader.IsDBNull(i))
                    userInfo.aPoint = reader.GetInt32(i);

                i = reader.GetOrdinal("BindTF");
                if (!reader.IsDBNull(i))
                    userInfo.BindTF = reader.GetByte(i);

                i = reader.GetOrdinal("birthday");
                if (!reader.IsDBNull(i))
                    userInfo.birthday = reader.GetDateTime(i);

                i = reader.GetOrdinal("CertNumber");
                if (!reader.IsDBNull(i))
                    userInfo.CertNumber = reader.GetString(i);


                i = reader.GetOrdinal("CertType");
                if (!reader.IsDBNull(i))
                    userInfo.CertType = reader.GetString(i);

                i = reader.GetOrdinal("cPoint");
                if (!reader.IsDBNull(i))
                    userInfo.cPoint = reader.GetInt32(i);

                i = reader.GetOrdinal("Email");
                if (!reader.IsDBNull(i))
                    userInfo.Email = reader.GetString(i);

                i = reader.GetOrdinal("EmailATF");
                if (!reader.IsDBNull(i))
                    userInfo.EmailATF = reader.GetByte(i);

                i = reader.GetOrdinal("EmailCode");
                if (!reader.IsDBNull(i))
                    userInfo.EmailCode = reader.GetString(i);

                i = reader.GetOrdinal("ePoint");
                if (!reader.IsDBNull(i))
                    userInfo.ePoint = reader.GetInt32(i);

                i = reader.GetOrdinal("FriendClass");
                if (!reader.IsDBNull(i))
                    userInfo.FriendClass = reader.GetString(i);

                i = reader.GetOrdinal("gPoint");
                if (!reader.IsDBNull(i))
                    userInfo.gPoint = reader.GetInt32(i);

                i = reader.GetOrdinal("Id");
                if (!reader.IsDBNull(i))
                    userInfo.Id = reader.GetInt64(i);

                i = reader.GetOrdinal("IDcardFiles");
                if (!reader.IsDBNull(i))
                    userInfo.IDcardFiles = reader.GetString(i);

                i = reader.GetOrdinal("iPoint");
                if (!reader.IsDBNull(i))
                    userInfo.iPoint = reader.GetInt32(i);

                i = reader.GetOrdinal("isAdmin");
                if (!reader.IsDBNull(i))
                    userInfo.isAdmin = reader.GetByte(i);

                i = reader.GetOrdinal("isIDcard");
                if (!reader.IsDBNull(i))
                    userInfo.isIDcard = reader.GetByte(i);

                i = reader.GetOrdinal("isLock");
                if (!reader.IsDBNull(i))
                    userInfo.isLock = reader.GetByte(i);

                i = reader.GetOrdinal("isMobile");
                if (!reader.IsDBNull(i))
                    userInfo.isMobile = reader.GetByte(i);

                i = reader.GetOrdinal("isOpen");
                if (!reader.IsDBNull(i))
                    userInfo.isOpen = reader.GetByte(i);

                i = reader.GetOrdinal("LastIP");
                if (!reader.IsDBNull(i))
                    userInfo.LastIP = reader.GetString(i);

                i = reader.GetOrdinal("LastLoginTime");
                if (!reader.IsDBNull(i))
                    userInfo.LastLoginTime = reader.GetDateTime(i);

                i = reader.GetOrdinal("LoginLimtNumber");
                if (!reader.IsDBNull(i))
                    userInfo.LoginLimtNumber = reader.GetInt32(i);

                i = reader.GetOrdinal("LoginNumber");
                if (!reader.IsDBNull(i))
                    userInfo.LoginNumber = reader.GetInt32(i);

                i = reader.GetOrdinal("marriage");
                if (!reader.IsDBNull(i))
                    userInfo.marriage = reader.GetByte(i);

                i = reader.GetOrdinal("mobile");
                if (!reader.IsDBNull(i))
                    userInfo.mobile = reader.GetString(i);

                i = reader.GetOrdinal("MobileCode");
                if (!reader.IsDBNull(i))
                    userInfo.MobileCode = reader.GetString(i);

                i = reader.GetOrdinal("NickName");
                if (!reader.IsDBNull(i))
                    userInfo.NickName = reader.GetString(i);

                i = reader.GetOrdinal("OnlineTF");
                if (!reader.IsDBNull(i))
                    userInfo.OnlineTF = reader.GetInt32(i);

                i = reader.GetOrdinal("OnlineTime");
                if (!reader.IsDBNull(i))
                    userInfo.OnlineTime = reader.GetInt32(i);

                i = reader.GetOrdinal("ParmConstrNum");
                if (!reader.IsDBNull(i))
                    userInfo.ParmConstrNum = reader.GetInt32(i);

                i = reader.GetOrdinal("PassKey");
                if (!reader.IsDBNull(i))
                    userInfo.PassKey = reader.GetString(i);

                i = reader.GetOrdinal("PassQuestion");
                if (!reader.IsDBNull(i))
                    userInfo.PassQuestion = reader.GetString(i);

                i = reader.GetOrdinal("RealName");
                if (!reader.IsDBNull(i))
                    userInfo.RealName = reader.GetString(i);

                i = reader.GetOrdinal("RegTime");
                if (!reader.IsDBNull(i))
                    userInfo.RegTime = reader.GetDateTime(i);

                i = reader.GetOrdinal("Sex");
                if (!reader.IsDBNull(i))
                    userInfo.Sex = reader.GetByte(i);

                i = reader.GetOrdinal("SiteID");
                if (!reader.IsDBNull(i))
                    userInfo.SiteID = reader.GetString(i);

                i = reader.GetOrdinal("UserFace");
                if (!reader.IsDBNull(i))
                    userInfo.UserFace = reader.GetString(i);

                i = reader.GetOrdinal("userFacesize");
                if (!reader.IsDBNull(i))
                    userInfo.userFacesize = reader.GetString(i);

                i = reader.GetOrdinal("UserGroupNumber");
                if (!reader.IsDBNull(i))
                    userInfo.UserGroupNumber = reader.GetString(i);

                i = reader.GetOrdinal("Userinfo");
                if (!reader.IsDBNull(i))
                    userInfo.Userinfo = reader.GetString(i);

                i = reader.GetOrdinal("UserName");
                if (!reader.IsDBNull(i))
                    userInfo.UserName = reader.GetString(i);

                i = reader.GetOrdinal("UserNum");
                if (!reader.IsDBNull(i))
                    userInfo.UserNum = reader.GetString(i);

                i = reader.GetOrdinal("UserPassword");
                if (!reader.IsDBNull(i))
                    userInfo.UserPassword = reader.GetString(i);
            }
            reader.Close();

            if (userInfo != null)
            {
                sql = "select * from " + Pre + "sys_userfields  where UserNum=@UserNum";
                reader = DbHelper.ExecuteReader(CommandType.Text, sql, 
                    new SqlParameter("@UserNum",userInfo.UserNum));
                if (reader.Read())
                {
                    userInfo.Fields = new SysUserFields(userInfo.UserNum);
                    int i;

                    i = reader.GetOrdinal("id");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.id = reader.GetInt32(i);

                    i = reader.GetOrdinal("province");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.province = reader.GetString(i);

                    i = reader.GetOrdinal("City");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.City = reader.GetString(i);

                    i = reader.GetOrdinal("Address");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.Address = reader.GetString(i);

                    i = reader.GetOrdinal("Postcode");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.Postcode = reader.GetString(i);

                    i = reader.GetOrdinal("FaTel");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.FaTel = reader.GetString(i);

                    i = reader.GetOrdinal("WorkTel");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.WorkTel = reader.GetString(i);

                    i = reader.GetOrdinal("QQ");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.QQ = reader.GetString(i);

                    i = reader.GetOrdinal("MSN");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.MSN = reader.GetString(i);

                    i = reader.GetOrdinal("Fax");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.Fax = reader.GetString(i);

                    i = reader.GetOrdinal("character");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.character = reader.GetString(i);

                    i = reader.GetOrdinal("UserFan");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.UserFan = reader.GetString(i);

                    i = reader.GetOrdinal("Nation");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.Nation = reader.GetString(i);

                    i = reader.GetOrdinal("nativeplace");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.nativeplace = reader.GetString(i);

                    i = reader.GetOrdinal("Job");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.Job = reader.GetString(i);

                    i = reader.GetOrdinal("education");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.education = reader.GetString(i);

                    i = reader.GetOrdinal("Lastschool");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.Lastschool = reader.GetString(i);

                    i = reader.GetOrdinal("orgSch");
                    if (!reader.IsDBNull(i))
                        userInfo.Fields.orgSch = reader.GetString(i);
                }
                reader.Close();
            }
            return userInfo;
        }

        /// <summary>
        /// 创建一个新用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public bool CreateUserInfo(SysUserInfo userinfo)
        {
            SqlParameter param = new SqlParameter("@UserName", userinfo.UserName);
            string sql = "insert into  " + Pre + "sys_user(UserNum,UserName,UserPassword,NickName,RealName,isAdmin,UserGroupNumber,PassQuestion,PassKey,CertType,CertNumber,Email,mobile,Sex,birthday,Userinfo,UserFace,userFacesize,marriage,iPoint,gPoint,cPoint,ePoint,aPoint,isLock,RegTime,LastLoginTime,OnlineTime,OnlineTF,LoginNumber,FriendClass,LoginLimtNumber,LastIP,SiteID,Addfriend,isOpen,ParmConstrNum,isIDcard,IDcardFiles,Addfriendbs,EmailATF,EmailCode,isMobile,BindTF,MobileCode) " +
                " values(@UserNum,@UserName,@UserPassword,@NickName,@RealName,@isAdmin,@UserGroupNumber,@PassQuestion,@PassKey,@CertType,@CertNumber,@Email,@mobile,@Sex,@birthday,@Userinfo,@UserFace,@userFacesize,@marriage,@iPoint,@gPoint,@cPoint,@ePoint,@aPoint,@isLock,@RegTime,@LastLoginTime,@OnlineTime,@OnlineTF,@LoginNumber,@FriendClass,@LoginLimtNumber,@LastIP,@SiteID,@Addfriend,@isOpen,@ParmConstrNum,@isIDcard,@IDcardFiles,@Addfriendbs,@EmailATF,@EmailCode,@isMobile,@BindTF,@MobileCode)";


            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                new SqlParameter[]{
                    
                    new SqlParameter("@UserNum", userinfo.UserNum ),
                    new SqlParameter("@UserName", userinfo.UserName ),
                    new SqlParameter("@UserPassword", userinfo.UserPassword ),
                    new SqlParameter("@NickName", userinfo.NickName ),
                    new SqlParameter("@RealName", userinfo.RealName ),
                    new SqlParameter("@isAdmin", userinfo.isAdmin ),
                    new SqlParameter("@UserGroupNumber", userinfo.UserGroupNumber ),
                    new SqlParameter("@PassQuestion", userinfo.PassQuestion ),
                    new SqlParameter("@PassKey", userinfo.PassKey ),
                    new SqlParameter("@CertType", userinfo.CertType ),
                    new SqlParameter("@CertNumber", userinfo.CertNumber ),
                    new SqlParameter("@Email", userinfo.Email ),
                    new SqlParameter("@mobile", userinfo.mobile ),
                    new SqlParameter("@Sex", userinfo.Sex ),
                    new SqlParameter("@birthday", userinfo.birthday),
                    new SqlParameter("@Userinfo", userinfo.Userinfo ),
                    new SqlParameter("@UserFace", userinfo.UserFace ),
                    new SqlParameter("@userFacesize", userinfo.userFacesize ),
                    new SqlParameter("@marriage", userinfo.marriage ),
                    new SqlParameter("@iPoint", userinfo.iPoint ),
                    new SqlParameter("@gPoint", userinfo.gPoint ),
                    new SqlParameter("@cPoint", userinfo.cPoint ),
                    new SqlParameter("@ePoint", userinfo.ePoint ),
                    new SqlParameter("@aPoint", userinfo.aPoint ),
                    new SqlParameter("@isLock", userinfo.isLock ),
                    new SqlParameter("@RegTime", userinfo.RegTime ),
                    new SqlParameter("@LastLoginTime", userinfo.LastLoginTime ),
                    new SqlParameter("@OnlineTime", userinfo.OnlineTime ),
                    new SqlParameter("@OnlineTF", userinfo.OnlineTF ),
                    new SqlParameter("@LoginNumber", userinfo.LoginNumber ),
                    new SqlParameter("@FriendClass", userinfo.FriendClass ),
                    new SqlParameter("@LoginLimtNumber", userinfo.LoginLimtNumber ),
                    new SqlParameter("@LastIP", userinfo.LastIP ),
                    new SqlParameter("@SiteID", userinfo.SiteID ),
                    new SqlParameter("@Addfriend", userinfo.Addfriend ),
                    new SqlParameter("@isOpen", userinfo.isOpen ),
                    new SqlParameter("@ParmConstrNum", userinfo.ParmConstrNum ),
                    new SqlParameter("@isIDcard", userinfo.isIDcard ),
                    new SqlParameter("@IDcardFiles", userinfo.IDcardFiles ),
                    new SqlParameter("@Addfriendbs", userinfo.Addfriendbs ),
                    new SqlParameter("@EmailATF", userinfo.EmailATF ),
                    new SqlParameter("@EmailCode", userinfo.EmailCode ),
                    new SqlParameter("@isMobile", userinfo.isMobile ),
                    new SqlParameter("@BindTF", userinfo.BindTF ),
                    new SqlParameter("@MobileCode", userinfo.MobileCode)
                });
            sql = "if not exists(select top 1 * from {0}sys_userfields where userNum=@userNum) " +
                " insert into {0}sys_userfields (userNum,province,City,Address,Postcode,FaTel,WorkTel,QQ,MSN,Fax,character,UserFan,Nation,nativeplace,Job,education,Lastschool,orgSch) " +
                " values(@userNum,@province,@City,@Address,@Postcode,@FaTel,@WorkTel,@QQ,@MSN,@Fax,@character,@UserFan,@Nation,@nativeplace,@Job,@education,@Lastschool,@orgSch)" +
                " else " +
                " update  {0}sys_userfields set  " +
                "province=@province," +
                "City=@City," +
                "Address=@Address," +
                "Postcode=@Postcode," +
                "FaTel=@FaTel," +
                "WorkTel=@WorkTel," +
                "QQ=@QQ," +
                "MSN=@MSN," +
                "Fax=@Fax," +
                "character=@character," +
                "UserFan=@UserFan," +
                "Nation=@Nation," +
                "nativeplace=@nativeplace," +
                "Job=@Job," +
                "education=@education," +
                "Lastschool=@Lastschool," +
                "orgSch=@orgSch   where userNum=@userNum ";
            sql = string.Format(sql, Pre);


            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                new SqlParameter[]{
                    new SqlParameter("@userNum",userinfo.Fields.userNum),
	                new SqlParameter("@province",userinfo.Fields.province),
	                new SqlParameter("@City",userinfo.Fields.City),
	                new SqlParameter("@Address",userinfo.Fields.Address),
	                new SqlParameter("@Postcode",userinfo.Fields.Postcode),
	                new SqlParameter("@FaTel",userinfo.Fields.FaTel),
	                new SqlParameter("@WorkTel",userinfo.Fields.WorkTel),
	                new SqlParameter("@QQ",userinfo.Fields.QQ),
	                new SqlParameter("@MSN",userinfo.Fields.MSN),
	                new SqlParameter("@Fax",userinfo.Fields.Fax),
	                new SqlParameter("@character",userinfo.Fields.character),
	                new SqlParameter("@UserFan",userinfo.Fields.UserFan),
	                new SqlParameter("@Nation",userinfo.Fields.Nation),
	                new SqlParameter("@nativeplace",userinfo.Fields.nativeplace),
	                new SqlParameter("@Job",userinfo.Fields.Job),
	                new SqlParameter("@education",userinfo.Fields.education),
	                new SqlParameter("@Lastschool",userinfo.Fields.Lastschool),
	                new SqlParameter("@orgSch",userinfo.Fields.orgSch)

                });
            return true;

        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userinfo">用户实例</param>
        /// <returns>成功或失败</returns>
        public bool UpdateUserInfo(SysUserInfo userinfo)
        {
            SqlParameter param = new SqlParameter("@UserName", userinfo.UserName);
            string sql = "update  " + Pre + "sys_user set  " +
            " UserPassword=@UserPassword," +
            " NickName=@NickName," +
            " RealName=@RealName," +
            " isAdmin=@isAdmin," +
            " UserGroupNumber=@UserGroupNumber," +
            " PassQuestion=@PassQuestion," +
            " PassKey=@PassKey," +
            " CertType=@CertType," +
            " CertNumber=@CertNumber," +
            " Email=@Email," +
            " mobile=@mobile," +
            " Sex=@Sex," +
            " birthday=@birthday," +
            " Userinfo=@Userinfo," +
            " UserFace=@UserFace," +
            " userFacesize=@userFacesize," +
            " marriage=@marriage," +
            " iPoint=@iPoint," +
            " gPoint=@gPoint," +
            " cPoint=@cPoint," +
            " ePoint=@ePoint," +
            " aPoint=@aPoint," +
            " isLock=@isLock," +
            " RegTime=@RegTime," +
            " LastLoginTime=@LastLoginTime," +
            " OnlineTime=@OnlineTime," +
            " OnlineTF=@OnlineTF," +
            " LoginNumber=@LoginNumber," +
            " FriendClass=@FriendClass," +
            " LoginLimtNumber=@LoginLimtNumber," +
            " LastIP=@LastIP," +
            " SiteID=@SiteID," +
            " Addfriend=@Addfriend," +
            " isOpen=@isOpen," +
            " ParmConstrNum=@ParmConstrNum," +
            " isIDcard=@isIDcard," +
            " IDcardFiles=@IDcardFiles," +
            " Addfriendbs=@Addfriendbs," +
            " EmailATF=@EmailATF," +
            " EmailCode=@EmailCode," +
            " isMobile=@isMobile," +
            " BindTF=@BindTF," +
            " MobileCode=@MobileCode " +
            " where UserName=@UserName ";



            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                new SqlParameter[]{
                 
                    new SqlParameter("@UserName", userinfo.UserName ),
                    new SqlParameter("@UserPassword", userinfo.UserPassword ),
                    new SqlParameter("@NickName", userinfo.NickName ),
                    new SqlParameter("@RealName", userinfo.RealName ),
                    new SqlParameter("@isAdmin", userinfo.isAdmin ),
                    new SqlParameter("@UserGroupNumber", userinfo.UserGroupNumber ),
                    new SqlParameter("@PassQuestion", userinfo.PassQuestion ),
                    new SqlParameter("@PassKey", userinfo.PassKey ),
                    new SqlParameter("@CertType", userinfo.CertType ),
                    new SqlParameter("@CertNumber", userinfo.CertNumber ),
                    new SqlParameter("@Email", userinfo.Email ),
                    new SqlParameter("@mobile", userinfo.mobile ),
                    new SqlParameter("@Sex", userinfo.Sex ),
                    new SqlParameter("@birthday", userinfo.birthday),
                    new SqlParameter("@Userinfo", userinfo.Userinfo ),
                    new SqlParameter("@UserFace", userinfo.UserFace ),
                    new SqlParameter("@userFacesize", userinfo.userFacesize ),
                    new SqlParameter("@marriage", userinfo.marriage ),
                    new SqlParameter("@iPoint", userinfo.iPoint ),
                    new SqlParameter("@gPoint", userinfo.gPoint ),
                    new SqlParameter("@cPoint", userinfo.cPoint ),
                    new SqlParameter("@ePoint", userinfo.ePoint ),
                    new SqlParameter("@aPoint", userinfo.aPoint ),
                    new SqlParameter("@isLock", userinfo.isLock ),
                    new SqlParameter("@RegTime", userinfo.RegTime ),
                    new SqlParameter("@LastLoginTime", userinfo.LastLoginTime ),
                    new SqlParameter("@OnlineTime", userinfo.OnlineTime ),
                    new SqlParameter("@OnlineTF", userinfo.OnlineTF ),
                    new SqlParameter("@LoginNumber", userinfo.LoginNumber ),
                    new SqlParameter("@FriendClass", userinfo.FriendClass ),
                    new SqlParameter("@LoginLimtNumber", userinfo.LoginLimtNumber ),
                    new SqlParameter("@LastIP", userinfo.LastIP ),
                    new SqlParameter("@SiteID", userinfo.SiteID ),
                    new SqlParameter("@Addfriend", userinfo.Addfriend ),
                    new SqlParameter("@isOpen", userinfo.isOpen ),
                    new SqlParameter("@ParmConstrNum", userinfo.ParmConstrNum ),
                    new SqlParameter("@isIDcard", userinfo.isIDcard ),
                    new SqlParameter("@IDcardFiles", userinfo.IDcardFiles ),
                    new SqlParameter("@Addfriendbs", userinfo.Addfriendbs ),
                    new SqlParameter("@EmailATF", userinfo.EmailATF ),
                    new SqlParameter("@EmailCode", userinfo.EmailCode ),
                    new SqlParameter("@isMobile", userinfo.isMobile ),
                    new SqlParameter("@BindTF", userinfo.BindTF ),
                    new SqlParameter("@MobileCode", userinfo.MobileCode)
                });


            sql = "if not exists(select top 1 * from {0}sys_userfields where userNum=@userNum) "+
                " insert into {0}sys_userfields (userNum,province,City,Address,Postcode,FaTel,WorkTel,QQ,MSN,Fax,character,UserFan,Nation,nativeplace,Job,education,Lastschool,orgSch) " +
                " values(@userNum,@province,@City,@Address,@Postcode,@FaTel,@WorkTel,@QQ,@MSN,@Fax,@character,@UserFan,@Nation,@nativeplace,@Job,@education,@Lastschool,@orgSch)"+
                " else "+
                " update  {0}sys_userfields set  " +             
                "province=@province,"+
                "City=@City,"+
                "Address=@Address,"+
                "Postcode=@Postcode,"+
                "FaTel=@FaTel,"+
                "WorkTel=@WorkTel,"+
                "QQ=@QQ,"+
                "MSN=@MSN,"+
                "Fax=@Fax,"+
                "character=@character,"+
                "UserFan=@UserFan,"+
                "Nation=@Nation,"+
                "nativeplace=@nativeplace,"+
                "Job=@Job,"+
                "education=@education,"+
                "Lastschool=@Lastschool,"+
                "orgSch=@orgSch   where userNum=@userNum ";
            sql = string.Format(sql, Pre);


            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                new SqlParameter[]{
                    new SqlParameter("@userNum",userinfo.Fields.userNum),
	                new SqlParameter("@province",userinfo.Fields.province),
	                new SqlParameter("@City",userinfo.Fields.City),
	                new SqlParameter("@Address",userinfo.Fields.Address),
	                new SqlParameter("@Postcode",userinfo.Fields.Postcode),
	                new SqlParameter("@FaTel",userinfo.Fields.FaTel),
	                new SqlParameter("@WorkTel",userinfo.Fields.WorkTel),
	                new SqlParameter("@QQ",userinfo.Fields.QQ),
	                new SqlParameter("@MSN",userinfo.Fields.MSN),
	                new SqlParameter("@Fax",userinfo.Fields.Fax),
	                new SqlParameter("@character",userinfo.Fields.character),
	                new SqlParameter("@UserFan",userinfo.Fields.UserFan),
	                new SqlParameter("@Nation",userinfo.Fields.Nation),
	                new SqlParameter("@nativeplace",userinfo.Fields.nativeplace),
	                new SqlParameter("@Job",userinfo.Fields.Job),
	                new SqlParameter("@education",userinfo.Fields.education),
	                new SqlParameter("@Lastschool",userinfo.Fields.Lastschool),
	                new SqlParameter("@orgSch",userinfo.Fields.orgSch)

                });
            return true;
        }

        #endregion
    }
}
