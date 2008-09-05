//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By ChenZhaoHui                        ==
//=====================================================================
using System;
using System.Data;
using System.Data.OleDb;
using Foosun.DALFactory;
using Foosun.Model;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;
using Foosun.Common;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class FrindLink : DbBase, IFrindLink
    {
        public DataTable GetClass()//取连接的分类
        {
            string Sql = "Select ClassID,ClassCName,ParentID From " + Pre + "friend_class where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable ParamStart()//取友情连接参数设置情况
        {
            string Str_StartSql = "Select * From " + Pre + "friend_pram";//从参数设置表中读出数据并初始化赋值
            return DbHelper.ExecuteTable(CommandType.Text, Str_StartSql, null);
        }
        public int Update_Pram(int open, int IsReg, int isLok, string Str_ArrSize, string Str_Content)//修改参数设置
        {
            OleDbParameter[] param = new OleDbParameter[2];
            param[0] = new OleDbParameter("@ArrSize", OleDbType.VarWChar, 10);
            param[0].Value = Str_ArrSize;
            param[1] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[1].Value = Str_Content;

            //经检查参数排列顺序一致 arjun
            string Str_InSql = "Update " + Pre + "friend_pram Set IsOpen=" + open + ",IsRegister=" + IsReg + ",isLock = " + isLok + ",ArrSize=@ArrSize,Content=@Content,SiteID=" + Foosun.Global.Current.SiteID + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, param);
        }
        //----------------分类分页-------------------
        //------递归---------------------------------
        public DataTable GetChildClassList(string classid)
        {
            OleDbParameter param = new OleDbParameter("@classid", classid);
            string Str = "Select id,ClassID,ClassCName,Content,ParentID From " + Pre + "friend_class Where ParentID=@classid and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Str, param);
        }
        //-------------------------------------------
        public int IsExitClassName(string Str_ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", Str_ClassID);
            string Str = " Select count(ClassID) From " + Pre + "friend_class where ClassID=@ClassID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Str, param);
        }
        public int ISExitNam(string name)
        {
            OleDbParameter param = new OleDbParameter("@ClassCName", name);
            string Str_CheckSql = "Select count(ClassCName) From " + Pre + "friend_class Where ClassCName=@ClassCName and SiteID='" + Foosun.Global.Current.SiteID + "'"; ;
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Str_CheckSql, param);
        }
        public int Insert_Class(string Str_ClassID, string Str_ClassCName, string Str_ClassEName, string Str_Description, string parentid)
        {
            OleDbParameter[] param = new OleDbParameter[5];
            param[0] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 10);
            param[0].Value = Str_ClassID;
            param[1] = new OleDbParameter("@ClassCName", OleDbType.VarWChar);
            param[1].Value = Str_ClassCName;
            param[2] = new OleDbParameter("@ClassEName", OleDbType.VarWChar);
            param[2].Value = Str_ClassEName;
            param[3] = new OleDbParameter("@Content", OleDbType.VarWChar);
            if (string.IsNullOrEmpty(Str_Description))
            {
                param[3].Value = "";
            }
            else
            {
                param[3].Value = Str_Description;
            }
           
            param[4] = new OleDbParameter("@ParentID", OleDbType.VarWChar);
            param[4].Value = parentid;

            string Str_InSql = "Insert into " + Pre + "friend_class ("+Database.getParam(param)+",SiteID) Values("+Database.getAParam(param)+",'" + Foosun.Global.Current.SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, param);

        }
        public int del_oneClass_1(string fid)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", fid);
            string Str_FriSql = "Delete From " + Pre + "friend_class where ClassID=@ClassID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_FriSql, param);
        }
        public int del_oneClass_2(string fid)
        {
            OleDbParameter param = new OleDbParameter("@ParentID", fid);
            string Str_Fri_child_Sql = "Delete From " + Pre + "friend_class where ParentID=@ParentID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_Fri_child_Sql, param);
        }
        public int del_onelink(int fid)
        {
            OleDbParameter param = new OleDbParameter("@ID", fid);
            string Str_FriSql = "Delete From " + Pre + "friend_link where ID=@ID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_FriSql, param);
        }
        public int suo_onelink(int fid)
        {
            OleDbParameter param = new OleDbParameter("@id", fid);
            string Str_FriSql = "Update " + Pre + "friend_link Set Lock = 1 where id=@id and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_FriSql, param);
        }
        public int unsuo_onelink(int fid)
        {
            OleDbParameter param = new OleDbParameter("@id", fid);
            string Str_FriSql = "Update " + Pre + "friend_link Set Lock = 0 where id=@@id and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_FriSql, param);
        }
        public DataTable EditClass(string classid)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", classid);
            string Str_FriSql = "Select id,ClassID,ClassCName,ClassEName,Content,ParentID From " + Pre + "friend_class where ClassID=@ClassID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Str_FriSql, param);
        }
        public int EditClick(string FID, string Str_ClassNameE, string Str_EnglishE, string Str_Descript)
        {
            OleDbParameter[] param = new OleDbParameter[4];
            param[0] = new OleDbParameter("@ClassCName", OleDbType.VarWChar, 12);
            param[0].Value = Str_ClassNameE;
            param[1] = new OleDbParameter("@ClassEName", OleDbType.VarWChar, 50);
            param[1].Value = Str_EnglishE;
            param[2] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[2].Value = Str_Descript;
            param[3] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[3].Value = FID;
            //经检查参数排列一致 arjun
            string Str_UpdateSql = "Update " + Pre + "friend_class Set ClassCName=@ClassCName,ClassEName=@ClassEName,Content=@Content where ClassID=@ClassID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_UpdateSql, param);
        }
        public int _DelPClass(string boxs)
        {
            string Fri_Sql = "Delete From " + Pre + "friend_class where ClassID in('" + boxs + "') and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Fri_Sql, null);
        }
        public int _DelPClass2(string boxs)
        {
            string Str_Fri_child_Sql = "Delete From " + Pre + "friend_class where ParentID in('" + boxs + "') and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_Fri_child_Sql, null);
        }
        public int _DelAllClass()
        {
            string Str_Fri_Sql = "Delete From " + Pre + "friend_class where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_Fri_Sql, null);
        }
        public int _LockP_Link(string boxs)
        {
            string F_Link_Sql = "Update " + Pre + "friend_link  Set Lock=1 where id in(" + boxs + ") and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, F_Link_Sql, null);
        }
        public int _unLockP_Link(string boxs)
        {
            string F_Link_Sql = "Update " + Pre + "friend_link  Set Lock=0 where id in(" + boxs + ") and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, F_Link_Sql, null);
        }
        public int _delP_Link(string boxs)
        {
            string F_Link_Sql = "Delete From " + Pre + "friend_link  where id in(" + boxs + ") and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, F_Link_Sql, null);
        }
        public int _delAll_Link()
        {
            string Str_Fri_Sql = "Delete From " + Pre + "friend_link where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_Fri_Sql, null);
        }
        public int ExistName_Link(string Str_Name)
        {
            OleDbParameter param = new OleDbParameter("@Name", Str_Name);
            string Str_CheckSql = "Select count(Name) From " + Pre + "friend_link Where Name=@Name and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Str_CheckSql, param);
        }
        public int _LinkSave(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, string Str_Author, string Str_Mail, string Str_ContentFor, string Str_LinkContent, string Str_Addtime, int Isuser, int isLok)
        {

            OleDbParameter[] param = new OleDbParameter[13];
            param[0] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[0].Value = Str_Class;
            param[1] = new OleDbParameter("@Name", OleDbType.VarWChar, 50);
            param[1].Value = Str_Name;
            param[2] = new OleDbParameter("@Type", OleDbType.Integer, 1);
            param[2].Value = Convert.ToInt32(Str_Type);
            param[3] = new OleDbParameter("@Url", OleDbType.VarWChar, 250);
            param[3].Value = Str_Url+"";
            param[4] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[4].Value = Str_Content+"";
            param[5] = new OleDbParameter("@PicUrl", OleDbType.VarWChar, 250);
            param[5].Value = Str_PicUrl+"";
            param[6] = new OleDbParameter("@Author", OleDbType.VarWChar, 50);
            param[6].Value = Str_Author+"";
            param[7] = new OleDbParameter("@Mail", OleDbType.VarWChar, 150);
            param[7].Value = Str_Mail+"";
            param[8] = new OleDbParameter("@ContentFor", OleDbType.VarWChar);
            param[8].Value = Str_ContentFor+"";
            param[9] = new OleDbParameter("@LinkContent", OleDbType.VarWChar);
            param[9].Value = Str_LinkContent+"";
            param[10] = new OleDbParameter("@Addtime", OleDbType.Date, 8);
            param[10].Value = DateTime.Now;
            param[11] = new OleDbParameter("@Isuser", OleDbType.Integer, 4);
            param[11].Value = Isuser;
            param[12] = new OleDbParameter("@Lock", OleDbType.Integer, 4);
            param[12].Value = isLok;
            string Str_InSql = "Insert into " + Pre + "friend_link ("+Database.getParam(param)+",SiteID) Values("+Database.getAParam(param)+",'" + Foosun.Global.Current.SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_InSql, param);
        }
        public DataTable Start_Link(int fid)
        {
            OleDbParameter param = new OleDbParameter("@ID", fid);
            string Str_FriSql = "Select id,Name,Type,Url,Content,PicUrl,Lock,IsUser,Author,Mail,ContentFor,LinkContent,Addtime,ClassID From " + Pre + "friend_link where ID=@ID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Str_FriSql, param);
        }
        public DataTable Edit_Link_Di()
        {
            string Sql = "Select ClassID,ClassCName,ParentID From " + Pre + "friend_class Where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public int Update_Link(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, int Isuser, int isLok, string Str_Author, string Str_Mail, string Str_ContentFor, string Str_LinkContent, string Str_Addtime, int FID)
        {
            OleDbParameter[] param = new OleDbParameter[13];
            param[0] = new OleDbParameter("@ClassID", OleDbType.VarWChar, 12);
            param[0].Value = Str_Class;
            param[1] = new OleDbParameter("@Name", OleDbType.VarWChar, 50);
            param[1].Value = Str_Name;
            param[2] = new OleDbParameter("@Type", OleDbType.Integer, 1);
            param[2].Value = Convert.ToInt32(Str_Type);
            param[3] = new OleDbParameter("@Url", OleDbType.VarWChar, 250);
            param[3].Value = Str_Url;
            param[4] = new OleDbParameter("@Content", OleDbType.VarWChar);
            param[4].Value = Convert.ToString(Str_Content);
            param[5] = new OleDbParameter("@PicUrl", OleDbType.VarWChar, 250);
            param[5].Value = Str_PicUrl;
            param[6] = new OleDbParameter("@Author", OleDbType.VarWChar, 50);
            param[6].Value = Str_Author;
            param[7] = new OleDbParameter("@Mail", OleDbType.VarWChar, 150);
            param[7].Value = Str_Mail;
            param[8] = new OleDbParameter("@ContentFor", OleDbType.VarWChar);
            param[8].Value = Str_ContentFor;
            param[9] = new OleDbParameter("@LinkContent", OleDbType.VarWChar);
            param[9].Value = Convert.ToString(Str_LinkContent);
            param[10] = new OleDbParameter("@Addtime", OleDbType.Date, 8);
            param[10].Value = Str_Addtime;
            param[11] = new OleDbParameter("@Isuser", OleDbType.Integer, 4);
            param[11].Value = Isuser;
            param[12] = new OleDbParameter("@Lock", OleDbType.Integer, 4);
            param[12].Value = isLok;
            //param[13] = new OleDbParameter("@FID", OleDbType.Integer, 4);
            //param[13].Value = FID;
            string Str_UpdateSql = "Update " + Pre + "friend_link  Set "+Database.getModifyParam(param)+" where ID="+FID+"";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Str_UpdateSql, param);
        }
        public DataTable UserNumm()
        {
            string Str_FriSql = "Select UserNum From " + Pre + "sys_User where UserNum='" + Foosun.Global.Current.UserNum + "' and isAdmin=1 and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Str_FriSql, null);
        }
        public DataTable CClas(string ClassID)
        {
            OleDbParameter param = new OleDbParameter("@ClassID", ClassID);
            string Class_Sql = "Select ClassCName From " + Pre + "friend_class where ClassID=@ClassID and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Class_Sql, param);
        }
        public DataTable USerSess(string Authorr)
        {
            OleDbParameter param = new OleDbParameter("@Authorr", Authorr);
            string Str_UserNum_Sql = "Select UserName,isAdmin From " + Pre + "sys_User where userNum=@Authorr and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Str_UserNum_Sql, param);
        }
    }
}
