//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Dong.Jiang                      ==
//===========================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;

namespace Foosun.CMS
{
   public class JSTemplet
    {
       private IJSTemplet dal;
       public JSTemplet()
       {
           dal = DataAccess.CreateJSTemplet();
       }
       public DataTable List()
       {
           return dal.List();
       }
       public DataTable ClassList()
       {
           return dal.ClassList();
       }
       public DataTable GetCustom()
       {
           return dal.GetCustom();
       }

       public DataTable reviewTempletContent(string tid)
       {
           return dal.reviewTempletContent(tid);
       }

       public DataTable GetPage(int PageIndex, int PageSize, out int RecordCount, out int PageCount, string ParentID)
       {
           return dal.GetPage(PageIndex, PageSize, out RecordCount, out PageCount, ParentID);
       }
       public void Add(string CName,string JSClassid,int JSTType,string JSTContent)
       {
           dal.Add(CName, JSClassid, JSTType, JSTContent);
       }
       public void Update(int id, string CName, string JSClassid, string JSTContent)
       {
           dal.Update(id, CName, JSClassid, JSTContent);
       }
       public DataTable GetSingle(int id)
       {
           return dal.GetSingle(id);
       }
       public DataTable GetClass(int id)
       {
           return dal.GetClass(id);
       }
       public void ClassAdd(string CName, string ParentID, string Description)
       {
           dal.ClassAdd(CName, ParentID, Description);
       }
       public void ClassUpdate(int id, string CName, string ParentID, string Description)
       {
           dal.ClassUpdate(id, CName, ParentID, Description);
       }
       public void Delete(int id)
       {
           dal.Delete(id);
       }
       public void ClassDelete(string id)
       {
           dal.ClassDelete(id);
       }
    }
}
