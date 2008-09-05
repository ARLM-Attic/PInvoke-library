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
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class FrindLink
    {
        private IFrindLink dal;
        public FrindLink()
        {
            dal = Foosun.DALFactory.DataAccess.CreateFrindLink();
        }
        public DataTable GetClass()//分类选择
        {
            return dal.GetClass();
        }
        public DataTable ParamStart()//参数设置
        {
            return dal.ParamStart();
        }
        public int Update_Pram(int open, int IsReg, int isLok, string Str_ArrSize, string Str_Content)//修改参数设置
        {
            return dal.Update_Pram(open, IsReg, isLok, Str_ArrSize, Str_Content);
        }
        //---------------------分类页--------------------
        //--------------递归----------------------
        public DataTable GetChildClassList(string classid)
        {
            return dal.GetChildClassList(classid);
        }
        //-----------------------------------------------
        public int IsExitClassName(string Str_ClassID)
        {
            return dal.IsExitClassName(Str_ClassID);
        }
        public int ISExitNam(string name)
        {
            return dal.ISExitNam(name);
        }
        public int Insert_Class(string Str_ClassID, string Str_ClassCName, string Str_ClassEName, string Str_Description, string parentid)
        {
            return dal.Insert_Class(Str_ClassID,Str_ClassCName, Str_ClassEName, Str_Description,parentid);
        }
        public int del_oneClass_1(string fid)
        {
            return dal.del_oneClass_1(fid);
        }
        public int del_oneClass_2(string fid)
        {
            return dal.del_oneClass_2(fid);
        }
        public int del_onelink(int fid)
        {
            return dal.del_onelink(fid);
        }
        public int suo_onelink(int fid)
        {
            return dal.suo_onelink(fid);
        }
        public int unsuo_onelink(int fid)
        {
            return dal.unsuo_onelink(fid);
        }
        public DataTable EditClass(string classid)
        { 
            return dal.EditClass(classid);
        }
        public int EditClick(string FID, string Str_ClassNameE, string Str_EnglishE, string Str_Descript)
        {
            return dal.EditClick(FID,Str_ClassNameE,Str_EnglishE,Str_Descript);
        }
        public int _DelPClass(string boxs)
        {
            return dal._DelPClass(boxs);
        }
        public int _DelPClass2(string boxs)
        {
            return dal._DelPClass2(boxs);
        }
        public int _DelAllClass()
        {
            return dal._DelAllClass();
        }
        public int _LockP_Link(string boxs)
        {
            return dal._LockP_Link(boxs);
        }
        public int _unLockP_Link(string boxs)
        {
            return dal._unLockP_Link(boxs);
        }
        public int _delP_Link(string boxs)
        {
            return dal._delP_Link(boxs);
        }
        public int _delAll_Link()
        {
            return dal._delAll_Link();
        }
        public int ExistName_Link(string Str_Name)
        {
            return dal.ExistName_Link(Str_Name);
        }
        public int _LinkSave(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, string Str_Author, string Str_Mail, string Str_ContentFor, string Str_LinkContent, string Str_Addtime, int Isuser, int isLok)
        {
            return dal._LinkSave(Str_Class,Str_Name,Str_Type,Str_Url,Str_Content, Str_PicUrl,Str_Author,Str_Mail,Str_ContentFor,Str_LinkContent, Str_Addtime, Isuser, isLok);
        }
        public DataTable Start_Link(int fid)
        {
            return dal.Start_Link(fid);
        }
        public DataTable Edit_Link_Di()
        {
            return dal.Edit_Link_Di();
        }
        public int Update_Link(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, int Isuser, int isLok, string Str_Author, string Str_Mail, string Str_ContentFor, string Str_LinkContent, string Str_Addtime, int FID)
        {
            return dal.Update_Link(Str_Class,Str_Name,Str_Type, Str_Url,Str_Content,Str_PicUrl,Isuser,isLok, Str_Author, Str_Mail, Str_ContentFor, Str_LinkContent, Str_Addtime,FID);
        }
        public DataTable UserNumm()
        {
            return dal.UserNumm();
        }
        public DataTable CClas(string ClassID)
        {
            return dal.CClas(ClassID);
        }
        public DataTable USerSess(string Authorr)
        {
            return dal.USerSess(Authorr);
        }
    }
}
