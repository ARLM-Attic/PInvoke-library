//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Y.xiaoBin                       ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class DefineTable
    {
        private IDefineTable dal;
        public DefineTable()
        {
            dal = Foosun.DALFactory.DataAccess.CreateDefineTable();
        }
        #region DefineTable.aspx
        public DataTable Sel_DefineInfoId()
        {
            return dal.Sel_DefineInfoId();
        }
        public DataTable Sel_ParentInfoId(string PID)
        {
            return dal.Sel_ParentInfoId(PID);
        }
        public int sel_defCname(string defCname)
        {
            return dal.sel_defCname(defCname);
        }

        public int sel_defEname(string defEname)
        {
            return dal.sel_defEname(defEname);
        }
        public int Add(string Str_ColumnsType, string defCname, string defEname, int definSelected, int Isnull, string defColumns, string defExp, string definedvalue)
        {
            return dal.Add(Str_ColumnsType, defCname, defEname, definSelected, Isnull, defColumns, defExp, definedvalue);
        }
        #endregion

        #region DefineTable_Edit_List.aspx
        public DataTable Str_Start_Sql(int ID)
        {
            return dal.Str_Start_Sql(ID);
        }
        public int Update(string Str_ColumnsType, string Str_DefName, string Str_DefEname, string Str_DefType, int Str_DefIsNull, string Str_DefColumns, string Str_DefExpr, int DefID, string definedvalue)
        {
            return dal.Update(Str_ColumnsType, Str_DefName, Str_DefEname, Str_DefType, Str_DefIsNull, Str_DefColumns, Str_DefExpr, DefID, definedvalue);
        }
        #endregion

        #region DefineTable_Edit_Manage.aspx
        public DataTable Str_DefID(string DefID)
        {
            return dal.Str_DefID(DefID);
        }
        public int Update1(string Str_NewText, string DefID)
        {
            return dal.Update1(Str_NewText, DefID);
        }
        #endregion

        #region DefineTable_List.aspx
        public DataTable GetPage(string defid, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            return dal.GetPage(defid, PageIndex,PageSize, out RecordCount, out PageCount, SqlCondition);
        }
        public int Str_Del_Data(string pr)
        {
            return dal.Str_Del_Data(pr);
        }
        public void Delete(string CheckboxArray)
        {
            dal.Delete(CheckboxArray);
        }
        public int Delete1(int DefID)
        {
            return dal.Delete1(DefID);
        }
        #endregion


        public DataTable sel_Str(string Classid)
        {
            return dal.sel_Str(Classid);
        }
        public int sel_1(string _NewText)
        {
            return dal.sel_1(_NewText);
        }
        public int sel_2(string rand)
        {
            return dal.sel_2(rand);
        }
        public int Add2(string rand, string _NewText, string _PraText)
        {
            return dal.Add2(rand, _NewText, _PraText);
        }
        public void Delete3(string DefID)
        {
            dal.Delete3(DefID);
        }
        public void Delete4(string DefID)
        {
            dal.Delete4(DefID);
        }
        public void Delete5(string DefID)
        {
            dal.Delete5(DefID);
        }
        public int Delete6()
        {
            return dal.Delete6();
        }
        public int Delete7()
        {
            return dal.Delete7();
        }
        public void Delete8(string CheckboxArray)
        {
            dal.Delete8(CheckboxArray);
        }
        public void Delete9(string CheckboxArray)
        {
            dal.Delete9(CheckboxArray);
        }
    }
}