using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
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
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select id,logID,Title,Content,userNum,LogDateTime From " + Pre + "user_userlogs Where (DATEDIFF('d', LogDateTime, Now())<=datenum) and Usernum=@UserNum and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable messageChar(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select id,Rec_UserNum From " + Pre + "user_message Where Rec_UserNum=@UserNum and isRead=0 and isRdel=0 and isRecyle=0 order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public IDataReader ShortcutList(string UserNum, int _num)
        {
            OleDbParameter[] param = new OleDbParameter[] 
{
        new OleDbParameter("@_num", _num),
new OleDbParameter("@UserNum", UserNum) 
};
            string Sql = "Select id,QMID,qName,FilePath,usernum,siteid From " + Pre + "API_Qmenu where ismanage=@_num and (UserNum=@UserNum or UserNum='0') order by OrderID desc,id desc";
            return DbHelper.ExecuteReader(CommandType.Text, Sql, param);
        }

        public IDataReader menuNavilist(string stype, string UserNum)
        {
            OleDbParameter[] param = new OleDbParameter[]{
            new OleDbParameter("@stype", stype),
new OleDbParameter("@UserNum", UserNum)
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
            OleDbParameter param = new OleDbParameter("@pID", pID);
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
            OleDbParameter param = new OleDbParameter("@am_ClassID", am_ClassID);
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
            OleDbParameter param = new OleDbParameter("@nID", nID);
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
            OleDbParameter param = new OleDbParameter("@pID", pID);
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
            OleDbParameter[] parm = InsertManageMenuParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        ///更新菜单
        /// </summary>
        /// <param name="uc2"></param>
        public void EditManageMenu(Foosun.Model.UserInfo7 uc2)
        {
            string Sql = "Update " + Pre + "API_Navi set am_ParentID=@am_ParentID,Am_position=@Am_position,am_Name=@am_Name,am_FilePath=@am_FilePath,am_target=@am_target,am_type=@am_type,am_orderID=@am_orderID,isSys=@isSys,popCode=@popCode where am_ID=" + uc2.am_ID + " " + Foosun.Common.Public.getSessionStr() + "";
            OleDbParameter[] parm = InsertManageMenuParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        ///更新菜单
        /// </summary>
        /// <param name="uc2"></param>
        public void EditManageMenu1(Foosun.Model.UserInfo7 uc2)
        {
            string Sql = "Update " + Pre + "API_Navi set am_Name=@am_Name,am_orderID=@am_orderID,popCode=@popCode where am_ID=" + uc2.am_ID + " " + Foosun.Common.Public.getSessionStr() + "";
            OleDbParameter[] parm = InsertManageMenuParameters2(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo7构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertManageMenuParameters(Foosun.Model.UserInfo7 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[15];
            param[0] = new OleDbParameter("@api_IdentID", OleDbType.VarWChar, 30);
            param[0].Value = uc1.api_IdentID;
            param[1] = new OleDbParameter("@am_ClassID", OleDbType.VarWChar, 12);
            param[1].Value = uc1.am_ClassID;
            param[2] = new OleDbParameter("@Am_position", OleDbType.VarWChar, 5);
            param[2].Value = uc1.Am_position;
            param[3] = new OleDbParameter("@am_Name", OleDbType.VarWChar, 20);
            param[3].Value = uc1.am_Name;
            param[4] = new OleDbParameter("@am_FilePath", OleDbType.VarWChar, 200);
            param[4].Value = uc1.am_FilePath;
            param[5] = new OleDbParameter("@am_target", OleDbType.VarWChar, 20);
            param[5].Value = uc1.am_target;
            param[6] = new OleDbParameter("@am_ParentID", OleDbType.VarWChar, 12);
            param[6].Value = uc1.am_ParentID;
            param[7] = new OleDbParameter("@am_type", OleDbType.Integer, 1);
            param[7].Value = uc1.am_type;
            param[8] = new OleDbParameter("@am_creatTime", OleDbType.Date, 8);
            param[8].Value = uc1.am_creatTime;
            param[9] = new OleDbParameter("@am_orderID", OleDbType.Integer, 4);
            param[9].Value = uc1.am_orderID;
            param[10] = new OleDbParameter("@isSys", OleDbType.Integer, 1);
            param[10].Value = uc1.isSys;
            param[11] = new OleDbParameter("@siteID", OleDbType.VarWChar, 12);
            param[11].Value = uc1.siteID;
            param[12] = new OleDbParameter("@userNum", OleDbType.VarWChar, 12);
            param[12].Value = uc1.userNum;
            param[13] = new OleDbParameter("@am_ID", OleDbType.Integer, 4);
            param[13].Value = uc1.am_ID;
            param[14] = new OleDbParameter("@popCode", OleDbType.VarWChar, 50);
            param[14].Value = uc1.popCode;

            return param;
        }

        /// <summary>
        /// 获取UserInfo7构造1
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertManageMenuParameters1(Foosun.Model.UserInfo7 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@Am_position", OleDbType.VarWChar, 5);
            param[0].Value = uc1.Am_position;
            param[1] = new OleDbParameter("@am_Name", OleDbType.VarWChar, 20);
            param[1].Value = uc1.am_Name;
            param[2] = new OleDbParameter("@am_FilePath", OleDbType.VarWChar, 200);
            param[2].Value = uc1.am_FilePath;
            param[3] = new OleDbParameter("@am_target", OleDbType.VarWChar, 20);
            param[3].Value = uc1.am_target;
            param[4] = new OleDbParameter("@am_ParentID", OleDbType.VarWChar, 12);
            param[4].Value = uc1.am_ParentID;
            param[5] = new OleDbParameter("@am_type", OleDbType.Integer, 1);
            param[5].Value = uc1.am_type;
            param[6] = new OleDbParameter("@am_orderID", OleDbType.Integer, 4);
            param[6].Value = uc1.am_orderID;
            param[7] = new OleDbParameter("@isSys", OleDbType.Integer, 1);
            param[7].Value = uc1.isSys;
            param[8] = new OleDbParameter("@am_ID", OleDbType.Integer, 4);
            param[8].Value = uc1.am_ID;
            param[9] = new OleDbParameter("@popCode", OleDbType.VarWChar, 50);
            param[9].Value = uc1.popCode;
            return param;
        }

        /// <summary>
        /// 获取UserInfo7构造2
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertManageMenuParameters2(Foosun.Model.UserInfo7 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[4];
            param[0] = new OleDbParameter("@am_Name", OleDbType.VarWChar, 20);
            param[0].Value = uc1.am_Name;
            param[1] = new OleDbParameter("@am_orderID", OleDbType.Integer, 4);
            param[1].Value = uc1.am_orderID;
            param[2] = new OleDbParameter("@am_ID", OleDbType.Integer, 4);
            param[2].Value = uc1.am_ID;
            param[3] = new OleDbParameter("@popCode", OleDbType.VarWChar, 50);
            param[3].Value = uc1.popCode;
            return param;
        }

        public void Shortcutdel(int Qid)
        {
            OleDbParameter param = new OleDbParameter("@Qid", Qid);
            string str_sql = "delete From " + Pre + "API_Navi where am_id=@Qid and UserNum='" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, param);
        }

        public void Shortcutde2(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
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
            OleDbParameter param = new OleDbParameter("@strNumber", strNumber);
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
            OleDbParameter[] parm = InsertQMenuParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 更新快捷菜单记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateQMenu(Foosun.Model.UserInfo8 uc2)
        {
            string Sql = "Update " + Pre + "API_Qmenu set qName=@qName,FilePath=@FilePath,OrderID=@OrderID where ID=" + uc2.Id + " and UserNum='" + Foosun.Global.Current.UserNum + "' and SiteID='" + Foosun.Global.Current.SiteID + "'";
            OleDbParameter[] parm = InsertQMenuParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo8构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertQMenuParameters(Foosun.Model.UserInfo8 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[8];
            param[0] = new OleDbParameter("@QmID", OleDbType.VarWChar, 12);
            param[0].Value = uc1.QmID;
            param[1] = new OleDbParameter("@qName", OleDbType.VarWChar, 50);
            param[1].Value = uc1.qName;
            param[2] = new OleDbParameter("@FilePath", OleDbType.VarWChar, 200);
            param[2].Value = uc1.FilePath;
            param[3] = new OleDbParameter("@Ismanage", OleDbType.Integer, 1);
            param[3].Value = uc1.Ismanage;
            param[4] = new OleDbParameter("@OrderID", OleDbType.Integer, 4);
            param[4].Value = uc1.OrderID;
            param[5] = new OleDbParameter("@usernum", OleDbType.VarWChar, 15);
            param[5].Value = uc1.usernum;
            param[6] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[6].Value = uc1.SiteID;
            param[7] = new OleDbParameter("@Id", OleDbType.Integer, 4);
            param[7].Value = uc1.Id;
            return param;
        }

        /// <summary>
        /// 获取UserInfo8构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertQMenuParameters1(Foosun.Model.UserInfo8 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[4];
            param[0] = new OleDbParameter("@qName", OleDbType.VarWChar, 50);
            param[0].Value = uc1.qName;
            param[1] = new OleDbParameter("@FilePath", OleDbType.VarWChar, 200);
            param[1].Value = uc1.FilePath;
            param[2] = new OleDbParameter("@OrderID", OleDbType.Integer, 4);
            param[2].Value = uc1.OrderID;
            param[3] = new OleDbParameter("@Id", OleDbType.Integer, 4);
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
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
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
            OleDbParameter param = new OleDbParameter("@userNum", userNum);
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
            OleDbParameter[] param = new OleDbParameter[3];
            param[0] = new OleDbParameter("@PassQuestion", OleDbType.VarWChar, 20);
            param[0].Value = PassQuestion;
            param[1] = new OleDbParameter("@PassKey", OleDbType.VarWChar, 20);
            param[1].Value = Foosun.Common.Input.MD5(PassKey);
            param[2] = new OleDbParameter("@password", OleDbType.VarWChar, 32);
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
            OleDbParameter param = new OleDbParameter("@userNum", userNum);
            string Sql = "select id from " + Pre + "sys_userfields where UserNum=@userNum " + Foosun.Common.Public.getSessionStr() + "";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        public DataTable getUserInfoContact(string userNum)
        {
            OleDbParameter param = new OleDbParameter("@userNum", userNum);
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
            OleDbParameter[] parm = GetUserInfoParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, Database.getNewParam(parm, Database.getSqlParam(str_sql)));
        }

        /// <summary>
        /// 获取UserInfo构造
        /// </summary>
        /// <param name="uc"></param>
        /// <returns></returns>
        private OleDbParameter[] GetUserInfoParameters(Foosun.Model.UserInfo uc)
        {
            OleDbParameter[] param = new OleDbParameter[11];
            param[0] = new OleDbParameter("@NickName", OleDbType.VarWChar, 12);
            param[0].Value = uc.NickName;
            param[1] = new OleDbParameter("@RealName", OleDbType.VarWChar, 20);
            param[1].Value = uc.RealName;
            param[2] = new OleDbParameter("@sex", OleDbType.Integer, 1);
            param[2].Value = uc.sex;
            param[3] = new OleDbParameter("@birthday", OleDbType.Date, 8);
            param[3].Value = uc.birthday;
            param[4] = new OleDbParameter("@Userinfo", OleDbType.VarWChar);
            param[4].Value = uc.Userinfo;
            param[5] = new OleDbParameter("@UserFace", OleDbType.VarWChar, 220);
            param[5].Value = uc.UserFace;
            param[6] = new OleDbParameter("@userFacesize", OleDbType.VarWChar, 8);
            param[6].Value = uc.userFacesize;
            param[7] = new OleDbParameter("@marriage", OleDbType.Integer, 1);
            param[7].Value = uc.marriage;
            param[8] = new OleDbParameter("@isopen", OleDbType.Integer, 1);
            param[8].Value = uc.isopen;
            param[9] = new OleDbParameter("@UserGroupNumber", OleDbType.VarWChar, 12);
            param[9].Value = uc.UserGroupNumber;
            param[10] = new OleDbParameter("@email", OleDbType.VarWChar, 220);
            param[10].Value = uc.email;
            return param;
        }

        /// <summary>
        /// 更新基本资料第2表
        /// </summary>
        /// <param name="uc1"></param>
        public void UpdateUserInfoBase1(Foosun.Model.UserInfo1 uc1)
        {
            string str_sql = "Update " + Pre + "sys_userfields set Nation=@Nation,nativeplace=@nativeplace,[character]=@character,UserFan=@UserFan,orgSch=@orgSch,job=@job,education=@education,Lastschool=@Lastschool where UserNum=@UserNum";

            OleDbParameter[] parm = GetUserInfoParameters1(uc1);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, Database.getNewParam(parm,Database.getSqlParam(str_sql)));
        }

        /// <summary>
        /// 如果基本资料第2表，则插入新记录
        /// </summary>
        /// <param name="uc2"></param>
        public void UpdateUserInfoBase2(Foosun.Model.UserInfo1 uc2)
        {
            //<--修改者：吴静岚 时间：2008-06-24 解决会员资料修改错误
            string Sql = "insert into [" + Pre + "sys_userfields ] (";
            Sql += "[UserNum],[Nation],[nativeplace],[character],[UserFan],[orgSch],[job],[education],[Lastschool]";
            //wjl-->
            Sql += ") values (";
            Sql += "'" + uc2.UserNum + "','" + uc2.Nation + "','" + uc2.nativeplace + "','" + uc2.character + "','" + uc2.UserFan + "','" + uc2.orgSch + "','" + uc2.job + "','" + uc2.education + "','" + uc2.Lastschool + "')";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 获取UserInfo1构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] GetUserInfoParameters1(Foosun.Model.UserInfo1 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[9];   
            param[0] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[0].Value = uc1.UserNum;
            param[1] = new OleDbParameter("@Nation", OleDbType.VarWChar, 12);
            param[1].Value = uc1.Nation;
            param[2] = new OleDbParameter("@nativeplace", OleDbType.VarWChar, 20);
            param[2].Value = uc1.nativeplace;
            param[3] = new OleDbParameter("@character", OleDbType.VarWChar);
            param[3].Value = uc1.character;
            param[4] = new OleDbParameter("@UserFan", OleDbType.VarWChar);
            param[4].Value = uc1.UserFan;
            param[5] = new OleDbParameter("@orgSch", OleDbType.VarWChar, 10);
            param[5].Value = uc1.orgSch;
            param[6] = new OleDbParameter("@job", OleDbType.VarWChar, 30);
            param[6].Value = uc1.job;
            param[7] = new OleDbParameter("@education", OleDbType.VarWChar, 20);
            param[7].Value = uc1.education;
            param[8] = new OleDbParameter("@Lastschool", OleDbType.VarWChar, 80);
            param[8].Value = uc1.Lastschool;

            return param;
        }

        /// <summary>
        /// 更新基本资料第2表
        /// </summary>
        /// <param name="uc1"></param>
        public void UpdateUserInfoContact1(Foosun.Model.UserInfo2 uc1)
        {
            string str_sql = "Update " + Pre + "sys_userfields set province=@province,City=@City,Address=@Address,Postcode=@Postcode,FaTel=@FaTel,WorkTel=@WorkTel,Fax=@Fax,QQ=@QQ,MSN=@MSN where UserNum=@UserNum";
            OleDbParameter[] parm = GetUserInfoContactParameters1(uc1);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, Database.getNewParam(parm, Database.getSqlParam(str_sql)));
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

            OleDbParameter[] parm = GetUserInfoContactParameters1(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(parm, Database.getSqlParam(Sql)));
        }


        private OleDbParameter[] GetUserInfoContactParameters1(Foosun.Model.UserInfo2 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[10];
            param[0] = new OleDbParameter("@province", OleDbType.VarWChar, 20);
            param[0].Value = uc1.province;
            param[1] = new OleDbParameter("@City", OleDbType.VarWChar, 20);
            param[1].Value = uc1.City;
            param[2] = new OleDbParameter("@Address", OleDbType.VarWChar, 50);
            param[2].Value = uc1.Address;
            param[3] = new OleDbParameter("@Postcode", OleDbType.VarWChar, 10);
            param[3].Value = uc1.Postcode;
            param[4] = new OleDbParameter("@FaTel", OleDbType.VarWChar, 30);
            param[4].Value = uc1.FaTel;
            param[5] = new OleDbParameter("@WorkTel", OleDbType.VarWChar, 30);
            param[5].Value = uc1.WorkTel;
            param[6] = new OleDbParameter("@Fax", OleDbType.VarWChar, 30);
            param[6].Value = uc1.Fax;
            param[7] = new OleDbParameter("@QQ", OleDbType.VarWChar, 30);
            param[7].Value = uc1.QQ;
            param[8] = new OleDbParameter("@MSN", OleDbType.VarWChar, 150);
            param[8].Value = uc1.MSN;
            param[9] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
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
            OleDbParameter[] parm = GetUserInfoBaseStatParameters1(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, str_sql, Database.getNewParam(parm, Database.getSqlParam(str_sql)));
        }


        private OleDbParameter[] GetUserInfoBaseStatParameters1(Foosun.Model.UserInfo3 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[17];
            param[0] = new OleDbParameter("@UserGroupNumber", OleDbType.VarWChar, 12);
            param[0].Value = uc1.UserGroupNumber;
            param[1] = new OleDbParameter("@islock", OleDbType.Integer, 1);
            param[1].Value = uc1.islock;
            param[2] = new OleDbParameter("@isadmin", OleDbType.Integer, 1);
            param[2].Value = uc1.isadmin;
            param[3] = new OleDbParameter("@CertType", OleDbType.VarWChar, 15);
            param[3].Value = uc1.CertType;
            param[4] = new OleDbParameter("@CertNumber", OleDbType.VarWChar, 20);
            param[4].Value = uc1.CertNumber;
            param[5] = new OleDbParameter("@ipoint", OleDbType.Integer, 4);
            param[5].Value = uc1.ipoint;
            param[6] = new OleDbParameter("@gpoint", OleDbType.Integer, 4);
            param[6].Value = uc1.gpoint;
            param[7] = new OleDbParameter("@cpoint", OleDbType.Integer, 4);
            param[7].Value = uc1.cpoint;
            param[8] = new OleDbParameter("@epoint", OleDbType.Integer, 4);
            param[8].Value = uc1.epoint;
            param[9] = new OleDbParameter("@apoint", OleDbType.Integer, 4);
            param[9].Value = uc1.apoint;
            param[10] = new OleDbParameter("@onlineTime", OleDbType.Integer, 4);
            param[10].Value = uc1.onlineTime;
            param[11] = new OleDbParameter("@RegTime", OleDbType.Date, 8);
            param[11].Value = uc1.RegTime;
            param[12] = new OleDbParameter("@LastLoginTime", OleDbType.Date, 8);
            param[12].Value = uc1.LastLoginTime;
            param[13] = new OleDbParameter("@LoginNumber", OleDbType.Integer, 4);
            param[13].Value = uc1.LoginNumber;
            param[14] = new OleDbParameter("@LoginLimtNumber", OleDbType.Integer, 4);
            param[14].Value = uc1.LoginLimtNumber;
            param[15] = new OleDbParameter("@lastIP", OleDbType.VarWChar, 16);
            param[15].Value = uc1.lastIP;
            param[16] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
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
            OleDbParameter param = new OleDbParameter("@Mobile", _MobileNumber);
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
            OleDbParameter param = new OleDbParameter("@UserGroupNumber", UserGroupNumber);
            string Sql = "select id from " + Pre + "sys_user where UserGroupNumber=@UserGroupNumber";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable GetGroupNumber(string UserGroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@UserGroupNumber", UserGroupNumber);
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

            OleDbParameter[] parm = InsertGroupParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(parm, "GroupNumber,GroupName,iPoint,Gpoint,Rtime,LenCommContent,CommCheckTF,PostCommTime,upfileType,upfileNum,upfileSize,DayUpfilenum,ContrNum,DicussTF,PostTitle,ReadUser,MessageNum,MessageGroupNum,IsCert,CharTF,CharHTML,CharLenContent,RegMinute,PostTitleHTML,DelSelfTitle,DelOTitle,EditSelfTitle,EditOtitle,ReadTitle,MoveSelfTitle,MoveOTitle,TopTitle,GoodTitle,LockUser,UserFlag,CheckTtile,IPTF,EncUser,OCTF,StyleTF,UpfaceSize,GIChange,GTChageRate,LoginPoint,RegPoint,GroupTF,GroupSize,GroupPerNum,GroupCreatNum,CreatTime,siteID,Discount"));
        }

        /// <summary>
        /// 获取UserInfo4构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] InsertGroupParameters(Foosun.Model.UserInfo4 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[52];
            param[0] = new OleDbParameter("@SiteID", OleDbType.VarWChar, 12);
            param[0].Value = uc1.SiteID;
            param[1] = new OleDbParameter("@GroupNumber", OleDbType.VarWChar, 12);
            param[1].Value = uc1.GroupNumber;
            param[2] = new OleDbParameter("@GroupName", OleDbType.VarWChar, 50);
            param[2].Value = uc1.GroupName;
            param[3] = new OleDbParameter("@iPoint", OleDbType.Integer, 4);
            param[3].Value = uc1.iPoint;
            param[4] = new OleDbParameter("@Gpoint", OleDbType.Integer, 4);
            param[4].Value = uc1.Gpoint;
            param[5] = new OleDbParameter("@Rtime", OleDbType.Integer, 4);
            param[5].Value = uc1.Rtime;
            param[6] = new OleDbParameter("@LenCommContent", OleDbType.Integer, 4);
            param[6].Value = uc1.LenCommContent;
            param[7] = new OleDbParameter("@CommCheckTF", OleDbType.Integer, 1);
            param[7].Value = uc1.CommCheckTF;
            param[8] = new OleDbParameter("@PostCommTime", OleDbType.Integer, 4);
            param[8].Value = uc1.PostCommTime;
            param[9] = new OleDbParameter("@upfileType", OleDbType.VarWChar, 200);
            param[9].Value = uc1.upfileType;
            param[10] = new OleDbParameter("@upfileNum", OleDbType.Integer, 4);
            param[10].Value = uc1.upfileNum;
            param[11] = new OleDbParameter("@upfileSize", OleDbType.Integer, 4);
            param[11].Value = uc1.upfileSize;
            param[12] = new OleDbParameter("@DayUpfilenum", OleDbType.Integer, 4);
            param[12].Value = uc1.DayUpfilenum;
            param[13] = new OleDbParameter("@ContrNum", OleDbType.Integer, 4);
            param[13].Value = uc1.ContrNum;
            param[14] = new OleDbParameter("@DicussTF", OleDbType.Integer, 1);
            param[14].Value = uc1.DicussTF;
            param[15] = new OleDbParameter("@PostTitle", OleDbType.Integer, 1);
            param[15].Value = uc1.PostTitle;
            param[16] = new OleDbParameter("@ReadUser", OleDbType.Integer, 1);
            param[16].Value = uc1.ReadUser;
            param[17] = new OleDbParameter("@MessageNum", OleDbType.Integer, 4);
            param[17].Value = uc1.MessageNum;
            param[18] = new OleDbParameter("@MessageGroupNum", OleDbType.VarWChar, 15);
            param[18].Value = uc1.MessageGroupNum;
            param[19] = new OleDbParameter("@IsCert", OleDbType.Integer, 1);
            param[19].Value = uc1.IsCert;
            param[20] = new OleDbParameter("@CharTF", OleDbType.Integer, 1);
            param[20].Value = uc1.CharTF;
            param[21] = new OleDbParameter("@CharHTML", OleDbType.Integer, 1);
            param[21].Value = uc1.CharHTML;
            param[22] = new OleDbParameter("@CharLenContent", OleDbType.Integer, 4);
            param[22].Value = uc1.CharLenContent;
            param[23] = new OleDbParameter("@RegMinute", OleDbType.Integer, 4);
            param[23].Value = uc1.RegMinute;
            param[24] = new OleDbParameter("@PostTitleHTML", OleDbType.Integer, 1);
            param[24].Value = uc1.PostTitleHTML;
            param[25] = new OleDbParameter("@DelSelfTitle", OleDbType.Integer, 1);
            param[25].Value = uc1.DelSelfTitle;
            param[26] = new OleDbParameter("@DelOTitle", OleDbType.Integer, 1);
            param[26].Value = uc1.DelOTitle;
            param[27] = new OleDbParameter("@EditSelfTitle", OleDbType.Integer, 1);
            param[27].Value = uc1.EditSelfTitle;
            param[28] = new OleDbParameter("@EditOtitle", OleDbType.Integer, 1);
            param[28].Value = uc1.EditOtitle;
            param[29] = new OleDbParameter("@ReadTitle", OleDbType.Integer, 1);
            param[29].Value = uc1.ReadTitle;
            param[30] = new OleDbParameter("@MoveSelfTitle", OleDbType.Integer, 1);
            param[30].Value = uc1.MoveSelfTitle;
            param[31] = new OleDbParameter("@MoveOTitle", OleDbType.Integer, 1);
            param[31].Value = uc1.MoveOTitle;
            param[32] = new OleDbParameter("@TopTitle", OleDbType.Integer, 1);
            param[32].Value = uc1.TopTitle;
            param[33] = new OleDbParameter("@GoodTitle", OleDbType.Integer, 1);
            param[33].Value = uc1.GoodTitle;
            param[34] = new OleDbParameter("@LockUser", OleDbType.Integer, 1);
            param[34].Value = uc1.LockUser;

            param[35] = new OleDbParameter("@UserFlag", OleDbType.VarWChar, 100);
            param[35].Value = uc1.UserFlag;
            param[36] = new OleDbParameter("@CheckTtile", OleDbType.Integer, 1);
            param[36].Value = uc1.CheckTtile;
            param[37] = new OleDbParameter("@IPTF", OleDbType.Integer, 1);
            param[37].Value = uc1.IPTF;
            param[38] = new OleDbParameter("@EncUser", OleDbType.Integer, 1);
            param[38].Value = uc1.EncUser;
            param[39] = new OleDbParameter("@OCTF", OleDbType.Integer, 1);
            param[39].Value = uc1.OCTF;
            param[40] = new OleDbParameter("@StyleTF", OleDbType.Integer, 1);
            param[40].Value = uc1.StyleTF;
            param[41] = new OleDbParameter("@UpfaceSize", OleDbType.Integer, 4);
            param[41].Value = uc1.UpfaceSize;


            param[42] = new OleDbParameter("@GIChange", OleDbType.VarWChar, 10);
            param[42].Value = uc1.GIChange;
            param[43] = new OleDbParameter("@GTChageRate", OleDbType.VarWChar, 30);
            param[43].Value = uc1.GTChageRate;
            param[44] = new OleDbParameter("@LoginPoint", OleDbType.VarWChar, 20);
            param[44].Value = uc1.LoginPoint;
            param[45] = new OleDbParameter("@RegPoint", OleDbType.VarWChar, 20);
            param[45].Value = uc1.RegPoint;
            param[46] = new OleDbParameter("@GroupTF", OleDbType.Integer, 1);
            param[46].Value = uc1.GroupTF;


            param[47] = new OleDbParameter("@GroupSize", OleDbType.Integer, 4);
            param[47].Value = uc1.GroupSize;
            param[48] = new OleDbParameter("@GroupPerNum", OleDbType.Integer, 4);
            param[48].Value = uc1.GroupPerNum;
            param[49] = new OleDbParameter("@GroupCreatNum", OleDbType.Integer, 4);
            param[49].Value = uc1.GroupCreatNum;
            param[50] = new OleDbParameter("@CreatTime", OleDbType.Date, 8);
            param[50].Value = uc1.CreatTime;
            param[51] = new OleDbParameter("@Discount", OleDbType.Double, 8);
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
            OleDbParameter[] parm = updateGroupEditParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, Database.getNewParam(parm, "GroupName,iPoint,Gpoint,Rtime,LenCommContent,CommCheckTF,PostCommTime,upfileType,upfileNum,upfileSize,DayUpfilenum,ContrNum,DicussTF,PostTitle,ReadUser,MessageNum,MessageGroupNum,IsCert,CharTF,CharHTML,CharLenContent,RegMinute,PostTitleHTML,DelSelfTitle,DelOTitle,EditSelfTitle,EditOtitle,ReadTitle,MoveSelfTitle,MoveOTitle,TopTitle,GoodTitle,LockUser,UserFlag,CheckTtile,IPTF,EncUser,OCTF,StyleTF,UpfaceSize,GIChange,GTChageRate,LoginPoint,RegPoint,GroupTF,GroupSize,GroupPerNum,GroupCreatNum,Discount"));
        }

        /// <summary>
        /// 获取UserInfo4构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] updateGroupEditParameters(Foosun.Model.UserInfo4 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[50];
            param[0] = new OleDbParameter("@gID", OleDbType.Integer, 4);
            param[0].Value = uc1.gID;
            param[1] = new OleDbParameter("@GroupCreatNum", OleDbType.Integer, 4);
            param[1].Value = uc1.GroupCreatNum;
            param[2] = new OleDbParameter("@GroupName", OleDbType.VarWChar, 50);
            param[2].Value = uc1.GroupName;
            param[3] = new OleDbParameter("@iPoint", OleDbType.Integer, 4);
            param[3].Value = uc1.iPoint;
            param[4] = new OleDbParameter("@Gpoint", OleDbType.Integer, 4);
            param[4].Value = uc1.Gpoint;
            param[5] = new OleDbParameter("@Rtime", OleDbType.Integer, 4);
            param[5].Value = uc1.Rtime;
            param[6] = new OleDbParameter("@LenCommContent", OleDbType.Integer, 4);
            param[6].Value = uc1.LenCommContent;
            param[7] = new OleDbParameter("@CommCheckTF", OleDbType.Integer, 1);
            param[7].Value = uc1.CommCheckTF;
            param[8] = new OleDbParameter("@PostCommTime", OleDbType.Integer, 4);
            param[8].Value = uc1.PostCommTime;
            param[9] = new OleDbParameter("@upfileType", OleDbType.VarWChar, 200);
            param[9].Value = uc1.upfileType;
            param[10] = new OleDbParameter("@upfileNum", OleDbType.Integer, 4);
            param[10].Value = uc1.upfileNum;
            param[11] = new OleDbParameter("@upfileSize", OleDbType.Integer, 4);
            param[11].Value = uc1.upfileSize;
            param[12] = new OleDbParameter("@DayUpfilenum", OleDbType.Integer, 4);
            param[12].Value = uc1.DayUpfilenum;
            param[13] = new OleDbParameter("@ContrNum", OleDbType.Integer, 4);
            param[13].Value = uc1.ContrNum;
            param[14] = new OleDbParameter("@DicussTF", OleDbType.Integer, 1);
            param[14].Value = uc1.DicussTF;
            param[15] = new OleDbParameter("@PostTitle", OleDbType.Integer, 1);
            param[15].Value = uc1.PostTitle;
            param[16] = new OleDbParameter("@ReadUser", OleDbType.Integer, 1);
            param[16].Value = uc1.ReadUser;
            param[17] = new OleDbParameter("@MessageNum", OleDbType.Integer, 4);
            param[17].Value = uc1.MessageNum;
            param[18] = new OleDbParameter("@MessageGroupNum", OleDbType.VarWChar, 15);
            param[18].Value = uc1.MessageGroupNum;
            param[19] = new OleDbParameter("@IsCert", OleDbType.Integer, 1);
            param[19].Value = uc1.IsCert;
            param[20] = new OleDbParameter("@CharTF", OleDbType.Integer, 1);
            param[20].Value = uc1.CharTF;
            param[21] = new OleDbParameter("@CharHTML", OleDbType.Integer, 1);
            param[21].Value = uc1.CharHTML;
            param[22] = new OleDbParameter("@CharLenContent", OleDbType.Integer, 4);
            param[22].Value = uc1.CharLenContent;
            param[23] = new OleDbParameter("@RegMinute", OleDbType.Integer, 4);
            param[23].Value = uc1.RegMinute;
            param[24] = new OleDbParameter("@PostTitleHTML", OleDbType.Integer, 1);
            param[24].Value = uc1.PostTitleHTML;
            param[25] = new OleDbParameter("@DelSelfTitle", OleDbType.Integer, 1);
            param[25].Value = uc1.DelSelfTitle;
            param[26] = new OleDbParameter("@DelOTitle", OleDbType.Integer, 1);
            param[26].Value = uc1.DelOTitle;
            param[27] = new OleDbParameter("@EditSelfTitle", OleDbType.Integer, 1);
            param[27].Value = uc1.EditSelfTitle;
            param[28] = new OleDbParameter("@EditOtitle", OleDbType.Integer, 1);
            param[28].Value = uc1.EditOtitle;
            param[29] = new OleDbParameter("@ReadTitle", OleDbType.Integer, 1);
            param[29].Value = uc1.ReadTitle;
            param[30] = new OleDbParameter("@MoveSelfTitle", OleDbType.Integer, 1);
            param[30].Value = uc1.MoveSelfTitle;
            param[31] = new OleDbParameter("@MoveOTitle", OleDbType.Integer, 1);
            param[31].Value = uc1.MoveOTitle;
            param[32] = new OleDbParameter("@TopTitle", OleDbType.Integer, 1);
            param[32].Value = uc1.TopTitle;
            param[33] = new OleDbParameter("@GoodTitle", OleDbType.Integer, 1);
            param[33].Value = uc1.GoodTitle;
            param[34] = new OleDbParameter("@LockUser", OleDbType.Integer, 1);
            param[34].Value = uc1.LockUser;

            param[35] = new OleDbParameter("@UserFlag", OleDbType.VarWChar, 100);
            param[35].Value = uc1.UserFlag;
            param[36] = new OleDbParameter("@CheckTtile", OleDbType.Integer, 1);
            param[36].Value = uc1.CheckTtile;
            param[37] = new OleDbParameter("@IPTF", OleDbType.Integer, 1);
            param[37].Value = uc1.IPTF;
            param[38] = new OleDbParameter("@EncUser", OleDbType.Integer, 1);
            param[38].Value = uc1.EncUser;
            param[39] = new OleDbParameter("@OCTF", OleDbType.Integer, 1);
            param[39].Value = uc1.OCTF;
            param[40] = new OleDbParameter("@StyleTF", OleDbType.Integer, 1);
            param[40].Value = uc1.StyleTF;
            param[41] = new OleDbParameter("@UpfaceSize", OleDbType.Integer, 4);
            param[41].Value = uc1.UpfaceSize;


            param[42] = new OleDbParameter("@GIChange", OleDbType.VarWChar, 10);
            param[42].Value = uc1.GIChange;
            param[43] = new OleDbParameter("@GTChageRate", OleDbType.VarWChar, 30);
            param[43].Value = uc1.GTChageRate;
            param[44] = new OleDbParameter("@LoginPoint", OleDbType.VarWChar, 20);
            param[44].Value = uc1.LoginPoint;
            param[45] = new OleDbParameter("@RegPoint", OleDbType.VarWChar, 20);
            param[45].Value = uc1.RegPoint;
            param[46] = new OleDbParameter("@GroupTF", OleDbType.Integer, 1);
            param[46].Value = uc1.GroupTF;


            param[47] = new OleDbParameter("@GroupSize", OleDbType.Integer, 4);
            param[47].Value = uc1.GroupSize;
            param[48] = new OleDbParameter("@GroupPerNum", OleDbType.Integer, 4);
            param[48].Value = uc1.GroupPerNum;
            param[49] = new OleDbParameter("@Discount", OleDbType.Double, 8);
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

            OleDbParameter[] parm = GetAnnounceParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo5构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] GetAnnounceParameters(Foosun.Model.UserInfo5 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@newsID", OleDbType.VarWChar, 12);
            param[0].Value = uc1.newsID;
            param[1] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
            param[1].Value = uc1.Title;
            param[2] = new OleDbParameter("@content", OleDbType.VarWChar);
            param[2].Value = uc1.content;
            param[3] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[3].Value = uc1.creatTime;
            param[4] = new OleDbParameter("@GroupNumber", OleDbType.VarWChar, 12);
            param[4].Value = uc1.GroupNumber;
            param[5] = new OleDbParameter("@getPoint", OleDbType.VarWChar, 50);
            param[5].Value = uc1.getPoint;
            param[6] = new OleDbParameter("@SiteId", OleDbType.VarWChar, 12);
            param[6].Value = uc1.SiteId;
            param[7] = new OleDbParameter("@isLock", OleDbType.Integer, 1);
            param[7].Value = uc1.isLock;
            param[8] = new OleDbParameter("@Id", OleDbType.Integer, 4);
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

            OleDbParameter[] parm = UpdateAnnounceParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取UserInfo5构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] UpdateAnnounceParameters(Foosun.Model.UserInfo5 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@Title", OleDbType.VarWChar, 50);
            param[0].Value = uc1.Title;
            param[1] = new OleDbParameter("@content", OleDbType.VarWChar);
            param[1].Value = uc1.content;
            param[2] = new OleDbParameter("@GroupNumber", OleDbType.VarWChar, 12);
            param[2].Value = uc1.GroupNumber;
            param[3] = new OleDbParameter("@getPoint", OleDbType.VarWChar, 50);
            param[3].Value = uc1.getPoint;
            param[4] = new OleDbParameter("@Id", OleDbType.Integer, 4);
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
                    QSQL += " and TimeOutDate <= #" + System.DateTime.Now + "#";
                }
                else
                {
                    QSQL += " and TimeOutDate > #" + System.DateTime.Now + "#";
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
            OleDbParameter param = new OleDbParameter("@CardNumber", CardNumber);
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
            OleDbParameter param = new OleDbParameter("@CardPass", CardPass);
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
            OleDbParameter[] parm = insertCardRParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        public void UpdateCardR(Foosun.Model.IDCARD uc)
        {
            string Sql = "Update " + Pre + "user_card set CardPassWord=@CardPassWord,[Money]=@Money,Point=@Point,isBuy=@isBuy,isUse=@isUse,isLock=@isLock,TimeOutDate=@TimeOutDate where Id=" + uc.Id + " and SiteID='" + Foosun.Global.Current.SiteID + "'";
            OleDbParameter[] parm = UpdateCardRParameters(uc);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, parm);
        }

        /// <summary>
        /// 获取IDCARD构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] insertCardRParameters(Foosun.Model.IDCARD uc1)
        {
            OleDbParameter[] param = new OleDbParameter[13];
            param[0] = new OleDbParameter("@CaID", OleDbType.VarWChar, 12);
            param[0].Value = uc1.CaID;
            param[1] = new OleDbParameter("@CardNumber", OleDbType.VarWChar, 30);
            param[1].Value = uc1.CardNumber;
            param[2] = new OleDbParameter("@CardPassWord", OleDbType.VarWChar, 150);
            param[2].Value = uc1.CardPassWord;
            param[3] = new OleDbParameter("@creatTime", OleDbType.Date, 8);
            param[3].Value = uc1.creatTime;
            param[4] = new OleDbParameter("@Money", OleDbType.Integer, 4);
            param[4].Value = uc1.Money;

            param[5] = new OleDbParameter("@Point", OleDbType.Integer, 4);
            param[5].Value = uc1.Point;
            param[6] = new OleDbParameter("@isBuy", OleDbType.Integer, 1);
            param[6].Value = uc1.isBuy;
            param[7] = new OleDbParameter("@isUse", OleDbType.Integer, 1);
            param[7].Value = uc1.isUse;
            param[8] = new OleDbParameter("@isLock", OleDbType.Integer, 1);
            param[8].Value = uc1.isLock;

            param[9] = new OleDbParameter("@UserNum", OleDbType.VarWChar, 15);
            param[9].Value = uc1.UserNum;
            param[10] = new OleDbParameter("@siteID", OleDbType.VarWChar, 12);
            param[10].Value = uc1.siteID;

            param[11] = new OleDbParameter("@TimeOutDate", OleDbType.Date, 8);
            param[11].Value = uc1.TimeOutDate;

            param[12] = new OleDbParameter("@ID", OleDbType.Integer, 4);
            param[12].Value = uc1.Id;

            return param;
        }
        /// <summary>
        /// 获取IDCARD1构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] UpdateCardRParameters(Foosun.Model.IDCARD uc1)
        {
            OleDbParameter[] param = new OleDbParameter[8];
            param[0] = new OleDbParameter("@CardPassWord", OleDbType.VarWChar, 150);
            param[0].Value = uc1.CardPassWord;
            param[1] = new OleDbParameter("@Money", OleDbType.Integer, 4);
            param[1].Value = uc1.Money;

            param[2] = new OleDbParameter("@Point", OleDbType.Integer, 4);
            param[2].Value = uc1.Point;
            param[3] = new OleDbParameter("@isBuy", OleDbType.Integer, 1);
            param[3].Value = uc1.isBuy;
            param[4] = new OleDbParameter("@isUse", OleDbType.Integer, 1);
            param[4].Value = uc1.isUse;
            param[5] = new OleDbParameter("@isLock", OleDbType.Integer, 1);
            param[5].Value = uc1.isLock;
            param[6] = new OleDbParameter("@TimeOutDate", OleDbType.Date, 8);
            param[6].Value = uc1.TimeOutDate;

            param[7] = new OleDbParameter("@ID", OleDbType.Integer, 4);
            param[7].Value = uc1.Id;

            return param;
        }
        /// <summary>
        /// 删除所有点卡
        /// </summary>
        public void delALLCARD()
        {
            string Sql = "Delete From  " + Pre + "user_card where SiteID = '" + Foosun.Global.Current.SiteID + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public DataTable getmoneylist()
        {
            string Sql = "select DisTinct Money from " + Pre + "user_Card where isBuy=0 and isUse=0 and isLock=0 and SiteID='" + Foosun.Global.Current.SiteID + "' and TimeOutDate>#" + DateTime.Now + "# and Money>0 order by Money asc";
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
                    Sql = "Update " + Pre + "sys_PramUser set onpayType=" + uc2.onpayType + ",O_userName='" + uc2.O_userName + "',o_key='" + uc2.O_key + "',O_sendurl='" + uc2.O_sendurl + "',O_returnurl='" + uc2.O_returnurl + "',O_md5='" + uc2.O_md5 + "',O_other1='" + uc2.O_other1 + "',O_other2='" + uc2.O_other2 + "',O_other3='" + uc2.O_other3 + "' where SiteID='" + Foosun.Global.Current.SiteID + "'";
                }
                else
                {
                    Sql = "Insert Into " + Pre + "sys_PramUser(onpayType,O_userName,o_key,O_sendurl,O_returnurl,O_md5,O_other1,O_other2,O_other3,SiteID) Values(" + uc2.onpayType + ",'" + uc2.O_userName + "','" + uc2.O_key + "','" + uc2.O_sendurl + "','" + uc2.O_returnurl + "','" + uc2.O_md5 + "','" + uc2.O_other1 + "','" + uc2.O_other2 + "','" + uc2.O_other3 + "','" + Foosun.Global.Current.SiteID + "')";
                }
            }
            //OleDbParameter[] parm = UpdateOnlinePayParameters(uc2);
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        /// <summary>
        /// 获取UserInfo6构造
        /// </summary>
        /// <param name="uc1"></param>
        /// <returns></returns>
        private OleDbParameter[] UpdateOnlinePayParameters(Foosun.Model.UserInfo6 uc1)
        {
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@o_userName", OleDbType.VarWChar, 100);
            param[0].Value = uc1.O_userName;
            param[1] = new OleDbParameter("@o_key", OleDbType.VarWChar, 128);
            param[1].Value = uc1.O_key;
            param[2] = new OleDbParameter("@o_sendurl", OleDbType.VarWChar, 220);
            param[2].Value = uc1.O_sendurl;
            param[3] = new OleDbParameter("@o_returnurl", OleDbType.VarWChar, 220);
            param[3].Value = uc1.O_returnurl;
            param[4] = new OleDbParameter("@o_md5", OleDbType.VarWChar, 128);
            param[4].Value = uc1.O_md5;
            param[5] = new OleDbParameter("@o_other1", OleDbType.VarWChar, 220);
            param[5].Value = uc1.O_other1;
            param[6] = new OleDbParameter("@o_other2", OleDbType.VarWChar, 220);
            param[6].Value = uc1.O_other2;
            param[7] = new OleDbParameter("@o_other3", OleDbType.VarWChar, 220);
            param[7].Value = uc1.O_other3;
            param[8] = new OleDbParameter("@Id", OleDbType.VarWChar, 128);
            param[8].Value = uc1.Id;
            param[8] = new OleDbParameter("@onpayType", OleDbType.Integer, 1);
            param[8].Value = uc1.onpayType;
            return param;
        }


        #endregion 在线支付结束

        #region 会员前台部分

        public DataTable getUserUserNumRecord(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "Select UserName,NickName,RealName,Sex,birthday,UserFace,userFacesize,marriage,Userinfo,UserGroupNumber,iPoint,gPoint,cPoint,ePoint,aPoint,RegTime,OnlineTime,OnlineTF,LoginNumber,Mobile,BindTF,PassQuestion,PassKey,CertType,CertNumber,Email,isOpen,isLock From " + Pre + "sys_User where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public string getUserGChange(string GroupNumber)
        {
            OleDbParameter param = new OleDbParameter("@GroupNumber", GroupNumber);
            string Sql = "Select GIChange From " + Pre + "user_Group  where GroupNumber=@GroupNumber";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }


        public DataTable getUserUserfields(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select * from " + Pre + "sys_userfields where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        public DataTable getUserInfobase1_user(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select UserNum,NickName,RealName,birthday,Userinfo,UserFace,userFacesize,email from " + Pre + "sys_User where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getUserInfobase2_user(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select Nation,nativeplace,character,orgSch,job,education,Lastschool,UserFan from " + Pre + "sys_userfields where UserNum=@UserNum";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }


        public int getPasswordTF(string password)
        {
            string md5Pwd = Foosun.Common.Input.MD5(password, true);
            OleDbParameter param = new OleDbParameter("@password", md5Pwd);
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
            OleDbParameter param = new OleDbParameter("@f_IDcardFiles", f_IDcardFiles);
            string Sql = "update " + Pre + "sys_User set IDcardFiles=@f_IDcardFiles,isIDcard=0 where UserNum='" + Foosun.Global.Current.UserNum + "'";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }

        #endregion 会员前台部分结束

        public string sel_pic(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "select top 1 PhotoUrl from " + Pre + "user_Photo where PhotoalbumID=@PhotoalbumID order by id desc";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }
        public int sel_picnum(string PhotoalbumID)
        {
            OleDbParameter param = new OleDbParameter("@PhotoalbumID", PhotoalbumID);
            string Sql = "select count(id) from " + Pre + "user_Photo where PhotoalbumID=@PhotoalbumID";
            return Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, Sql, param));
        }

        #region 投稿
        public DataTable getConstrClass(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
            string Sql = "select ID,Ccid,cName,Content from " + Pre + "user_ConstrClass where UserNum=@UserNum order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        public DataTable getConstrID(string ConID, string UserNum)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@ConID", ConID), new OleDbParameter("@UserNum", UserNum) };
            string Sql = "select ID,Title,Content,creatTime,Source,Tags,Author,PicURL from " + Pre + "user_Constr where UserNum=@UserNum and ConID=@ConID and  isuserdel=0 and SiteID='" + Foosun.Global.Current.SiteID + "' order by id desc";
            DataTable rdr = DbHelper.ExecuteTable(CommandType.Text, Sql, param);
            return rdr;
        }

        #endregion 投稿

        public string getAdminPopandSupper(string UserNum)
        {
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
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
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@URLName", URLName), new OleDbParameter("@URL", URL), new OleDbParameter("@URLColor", URLColor), new OleDbParameter("@ClassID", ClassID), new OleDbParameter("@Content", Content) };
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
            OleDbParameter param = new OleDbParameter("@ClassName", ClassName);
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
            OleDbParameter param = new OleDbParameter("@UserNum", UserNum);
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
            OleDbParameter param = new OleDbParameter("@ID", ID);
            string sql = "select content from " + Pre + "user_userlogs where ID=@ID";
            return Convert.ToString(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
        }
        #region API
        public IDataReader GetUserAPiInfo(string UserName)
        {
            OleDbParameter param = new OleDbParameter("@UserName", UserName);
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
            OleDbParameter param = new OleDbParameter("@UserName", username);
            string sql = "select count(Id) from " + Pre + "sys_user where UserName=@UserName";
            int count = Convert.ToInt32(DbHelper.ExecuteScalar(CommandType.Text, sql, param));
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
            OleDbParameter param = new OleDbParameter("@UserName", username);
            string sql = "select * from " + Pre + "sys_user where UserName=@UserName";
            IDataReader reader = DbHelper.ExecuteReader(CommandType.Text, sql, param);
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
                    new OleDbParameter("@UserNum", userInfo.UserNum));
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
            OleDbParameter param = new OleDbParameter("@UserName", userinfo.UserName);
            string sql = "insert into  " + Pre + "sys_user(UserNum,UserName,UserPassword,NickName,RealName,isAdmin,UserGroupNumber,PassQuestion,PassKey,CertType,CertNumber,Email,mobile,Sex,birthday,Userinfo,UserFace,userFacesize,marriage,iPoint,gPoint,cPoint,ePoint,aPoint,isLock,RegTime,LastLoginTime,OnlineTime,OnlineTF,LoginNumber,FriendClass,LoginLimtNumber,LastIP,SiteID,Addfriend,isOpen,ParmConstrNum,isIDcard,IDcardFiles,Addfriendbs,EmailATF,EmailCode,isMobile,BindTF,MobileCode) " +
                " values(@UserNum,@UserName,@UserPassword,@NickName,@RealName,@isAdmin,@UserGroupNumber,@PassQuestion,@PassKey,@CertType,@CertNumber,@Email,@mobile,@Sex,@birthday,@Userinfo,@UserFace,@userFacesize,@marriage,@iPoint,@gPoint,@cPoint,@ePoint,@aPoint,@isLock,@RegTime,@LastLoginTime,@OnlineTime,@OnlineTF,@LoginNumber,@FriendClass,@LoginLimtNumber,@LastIP,@SiteID,@Addfriend,@isOpen,@ParmConstrNum,@isIDcard,@IDcardFiles,@Addfriendbs,@EmailATF,@EmailCode,@isMobile,@BindTF,@MobileCode)";


            DbHelper.ExecuteNonQuery(CommandType.Text, sql,
                new OleDbParameter[]{
                    
                    new OleDbParameter("@UserNum", userinfo.UserNum ),
                    new OleDbParameter("@UserName", userinfo.UserName ),
                    new OleDbParameter("@UserPassword", userinfo.UserPassword ),
                    new OleDbParameter("@NickName", userinfo.NickName ),
                    new OleDbParameter("@RealName", userinfo.RealName ),
                    new OleDbParameter("@isAdmin", userinfo.isAdmin ),
                    new OleDbParameter("@UserGroupNumber", userinfo.UserGroupNumber ),
                    new OleDbParameter("@PassQuestion", userinfo.PassQuestion ),
                    new OleDbParameter("@PassKey", userinfo.PassKey ),
                    new OleDbParameter("@CertType", userinfo.CertType ),
                    new OleDbParameter("@CertNumber", userinfo.CertNumber ),
                    new OleDbParameter("@Email", userinfo.Email ),
                    new OleDbParameter("@mobile", userinfo.mobile ),
                    new OleDbParameter("@Sex", userinfo.Sex ),
                    new OleDbParameter("@birthday", userinfo.birthday),
                    new OleDbParameter("@Userinfo", userinfo.Userinfo ),
                    new OleDbParameter("@UserFace", userinfo.UserFace ),
                    new OleDbParameter("@userFacesize", userinfo.userFacesize ),
                    new OleDbParameter("@marriage", userinfo.marriage ),
                    new OleDbParameter("@iPoint", userinfo.iPoint ),
                    new OleDbParameter("@gPoint", userinfo.gPoint ),
                    new OleDbParameter("@cPoint", userinfo.cPoint ),
                    new OleDbParameter("@ePoint", userinfo.ePoint ),
                    new OleDbParameter("@aPoint", userinfo.aPoint ),
                    new OleDbParameter("@isLock", userinfo.isLock ),
                    new OleDbParameter("@RegTime", userinfo.RegTime ),
                    new OleDbParameter("@LastLoginTime", userinfo.LastLoginTime ),
                    new OleDbParameter("@OnlineTime", userinfo.OnlineTime ),
                    new OleDbParameter("@OnlineTF", userinfo.OnlineTF ),
                    new OleDbParameter("@LoginNumber", userinfo.LoginNumber ),
                    new OleDbParameter("@FriendClass", userinfo.FriendClass ),
                    new OleDbParameter("@LoginLimtNumber", userinfo.LoginLimtNumber ),
                    new OleDbParameter("@LastIP", userinfo.LastIP ),
                    new OleDbParameter("@SiteID", userinfo.SiteID ),
                    new OleDbParameter("@Addfriend", userinfo.Addfriend ),
                    new OleDbParameter("@isOpen", userinfo.isOpen ),
                    new OleDbParameter("@ParmConstrNum", userinfo.ParmConstrNum ),
                    new OleDbParameter("@isIDcard", userinfo.isIDcard ),
                    new OleDbParameter("@IDcardFiles", userinfo.IDcardFiles ),
                    new OleDbParameter("@Addfriendbs", userinfo.Addfriendbs ),
                    new OleDbParameter("@EmailATF", userinfo.EmailATF ),
                    new OleDbParameter("@EmailCode", userinfo.EmailCode ),
                    new OleDbParameter("@isMobile", userinfo.isMobile ),
                    new OleDbParameter("@BindTF", userinfo.BindTF ),
                    new OleDbParameter("@MobileCode", userinfo.MobileCode)
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
                new OleDbParameter[]{
                    new OleDbParameter("@userNum",userinfo.Fields.userNum),
	                new OleDbParameter("@province",userinfo.Fields.province),
	                new OleDbParameter("@City",userinfo.Fields.City),
	                new OleDbParameter("@Address",userinfo.Fields.Address),
	                new OleDbParameter("@Postcode",userinfo.Fields.Postcode),
	                new OleDbParameter("@FaTel",userinfo.Fields.FaTel),
	                new OleDbParameter("@WorkTel",userinfo.Fields.WorkTel),
	                new OleDbParameter("@QQ",userinfo.Fields.QQ),
	                new OleDbParameter("@MSN",userinfo.Fields.MSN),
	                new OleDbParameter("@Fax",userinfo.Fields.Fax),
	                new OleDbParameter("@character",userinfo.Fields.character),
	                new OleDbParameter("@UserFan",userinfo.Fields.UserFan),
	                new OleDbParameter("@Nation",userinfo.Fields.Nation),
	                new OleDbParameter("@nativeplace",userinfo.Fields.nativeplace),
	                new OleDbParameter("@Job",userinfo.Fields.Job),
	                new OleDbParameter("@education",userinfo.Fields.education),
	                new OleDbParameter("@Lastschool",userinfo.Fields.Lastschool),
	                new OleDbParameter("@orgSch",userinfo.Fields.orgSch)

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
            OleDbParameter param = new OleDbParameter("@UserName", userinfo.UserName);
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
                new OleDbParameter[]{
                 
                    new OleDbParameter("@UserName", userinfo.UserName ),
                    new OleDbParameter("@UserPassword", userinfo.UserPassword ),
                    new OleDbParameter("@NickName", userinfo.NickName ),
                    new OleDbParameter("@RealName", userinfo.RealName ),
                    new OleDbParameter("@isAdmin", userinfo.isAdmin ),
                    new OleDbParameter("@UserGroupNumber", userinfo.UserGroupNumber ),
                    new OleDbParameter("@PassQuestion", userinfo.PassQuestion ),
                    new OleDbParameter("@PassKey", userinfo.PassKey ),
                    new OleDbParameter("@CertType", userinfo.CertType ),
                    new OleDbParameter("@CertNumber", userinfo.CertNumber ),
                    new OleDbParameter("@Email", userinfo.Email ),
                    new OleDbParameter("@mobile", userinfo.mobile ),
                    new OleDbParameter("@Sex", userinfo.Sex ),
                    new OleDbParameter("@birthday", userinfo.birthday),
                    new OleDbParameter("@Userinfo", userinfo.Userinfo ),
                    new OleDbParameter("@UserFace", userinfo.UserFace ),
                    new OleDbParameter("@userFacesize", userinfo.userFacesize ),
                    new OleDbParameter("@marriage", userinfo.marriage ),
                    new OleDbParameter("@iPoint", userinfo.iPoint ),
                    new OleDbParameter("@gPoint", userinfo.gPoint ),
                    new OleDbParameter("@cPoint", userinfo.cPoint ),
                    new OleDbParameter("@ePoint", userinfo.ePoint ),
                    new OleDbParameter("@aPoint", userinfo.aPoint ),
                    new OleDbParameter("@isLock", userinfo.isLock ),
                    new OleDbParameter("@RegTime", userinfo.RegTime ),
                    new OleDbParameter("@LastLoginTime", userinfo.LastLoginTime ),
                    new OleDbParameter("@OnlineTime", userinfo.OnlineTime ),
                    new OleDbParameter("@OnlineTF", userinfo.OnlineTF ),
                    new OleDbParameter("@LoginNumber", userinfo.LoginNumber ),
                    new OleDbParameter("@FriendClass", userinfo.FriendClass ),
                    new OleDbParameter("@LoginLimtNumber", userinfo.LoginLimtNumber ),
                    new OleDbParameter("@LastIP", userinfo.LastIP ),
                    new OleDbParameter("@SiteID", userinfo.SiteID ),
                    new OleDbParameter("@Addfriend", userinfo.Addfriend ),
                    new OleDbParameter("@isOpen", userinfo.isOpen ),
                    new OleDbParameter("@ParmConstrNum", userinfo.ParmConstrNum ),
                    new OleDbParameter("@isIDcard", userinfo.isIDcard ),
                    new OleDbParameter("@IDcardFiles", userinfo.IDcardFiles ),
                    new OleDbParameter("@Addfriendbs", userinfo.Addfriendbs ),
                    new OleDbParameter("@EmailATF", userinfo.EmailATF ),
                    new OleDbParameter("@EmailCode", userinfo.EmailCode ),
                    new OleDbParameter("@isMobile", userinfo.isMobile ),
                    new OleDbParameter("@BindTF", userinfo.BindTF ),
                    new OleDbParameter("@MobileCode", userinfo.MobileCode)
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
                new OleDbParameter[]{
                    new OleDbParameter("@userNum",userinfo.Fields.userNum),
	                new OleDbParameter("@province",userinfo.Fields.province),
	                new OleDbParameter("@City",userinfo.Fields.City),
	                new OleDbParameter("@Address",userinfo.Fields.Address),
	                new OleDbParameter("@Postcode",userinfo.Fields.Postcode),
	                new OleDbParameter("@FaTel",userinfo.Fields.FaTel),
	                new OleDbParameter("@WorkTel",userinfo.Fields.WorkTel),
	                new OleDbParameter("@QQ",userinfo.Fields.QQ),
	                new OleDbParameter("@MSN",userinfo.Fields.MSN),
	                new OleDbParameter("@Fax",userinfo.Fields.Fax),
	                new OleDbParameter("@character",userinfo.Fields.character),
	                new OleDbParameter("@UserFan",userinfo.Fields.UserFan),
	                new OleDbParameter("@Nation",userinfo.Fields.Nation),
	                new OleDbParameter("@nativeplace",userinfo.Fields.nativeplace),
	                new OleDbParameter("@Job",userinfo.Fields.Job),
	                new OleDbParameter("@education",userinfo.Fields.education),
	                new OleDbParameter("@Lastschool",userinfo.Fields.Lastschool),
	                new OleDbParameter("@orgSch",userinfo.Fields.orgSch)

                });
            return true;
        }

        #endregion
    }
}
