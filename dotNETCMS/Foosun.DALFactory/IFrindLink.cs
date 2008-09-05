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
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IFrindLink
    {
        DataTable GetClass();//连接分类
        DataTable ParamStart();//参数设置
        int Update_Pram(int open, int IsReg, int isLok, string Str_ArrSize, string Str_Content);//修改参数设置
        DataTable GetChildClassList(string classid);//分类页递归
        int IsExitClassName(string Str_ClassID);//类ID是否重复
        int ISExitNam(string name);//是否存在类名
        int Insert_Class(string Str_ClassID, string Str_ClassCName, string Str_ClassEName, string Str_Description, string parentid);
        int del_oneClass_1(string fid);
        int del_oneClass_2(string fid);
        int del_onelink(int fid);
        int suo_onelink(int fid);
        int unsuo_onelink(int fid);
        DataTable EditClass(string classid);
        int EditClick(string FID, string Str_ClassNameE, string Str_EnglishE, string Str_Descript);
        int _DelPClass(string boxs);
        int _DelPClass2(string boxs);
        int _DelAllClass();
        int _LockP_Link(string boxs);
        int _unLockP_Link(string boxs);
        int _delP_Link(string boxs);
        int _delAll_Link();
        int ExistName_Link(string Str_Name);
        int _LinkSave(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, string Str_Author, string Str_Mail, string Str_ContentFor, string Str_LinkContent, string Str_Addtime, int Isuser, int isLok);
        DataTable Start_Link(int fid);
        DataTable Edit_Link_Di();
        int Update_Link(string Str_Class, string Str_Name, string Str_Type, string Str_Url, string Str_Content, string Str_PicUrl, int Isuser, int isLok, string Str_Author, string Str_Mail, string Str_ContentFor, string Str_LinkContent, string Str_Addtime, int FID);
        DataTable UserNumm();
        DataTable CClas(string ClassID);
        DataTable USerSess(string Authorr);
    }
    public sealed partial class DataAccess
    {
        public static IFrindLink CreateFrindLink()
        {
            string className = path + ".FrindLink";
            return (IFrindLink)Assembly.Load(path).CreateInstance(className);
        }
    }
}
