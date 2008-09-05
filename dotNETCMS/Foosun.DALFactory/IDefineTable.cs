using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IDefineTable
    {
        #region DefineTable.aspx
        DataTable Sel_DefineInfoId();
        DataTable Sel_ParentInfoId(string PID);
        int sel_defCname(string defCname);
        int sel_defEname(string defEname);
        int Add(string Str_ColumnsType, string defCname, string defEname, int definSelected, int Isnull, string defColumns, string defExp, string definedvalue);
        #endregion

        #region DefineTable_Edit_List.aspx
        DataTable Str_Start_Sql(int ID);
        int Update(string Str_ColumnsType, string Str_DefName, string Str_DefEname, string Str_DefType, int Str_DefIsNull, string Str_DefColumns, string Str_DefExpr, int DefID, string definedvalue);

        #endregion

        #region DefineTable_Edit_Manage.aspx
        DataTable Str_DefID(string DefID);
        int Update1(string Str_NewText, string DefID);
        #endregion

        #region DefineTable_List.aspx
        DataTable GetPage(string defid, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition);
        int Str_Del_Data(string pr);
        void Delete(string CheckboxArray);
        int Delete1(int DefID);
        #endregion


        DataTable sel_Str(string Classid);
        int sel_1(string _NewText);
        int sel_2(string rand);
        int Add2(string rand, string _NewText, string _PraText);
        void Delete3(string DefID);
        void Delete4(string DefID);
        void Delete5(string DefID);
        int Delete6();
        int Delete7();
        void Delete8(string CheckboxArray);
        void Delete9(string CheckboxArray);
    }
    public sealed partial class DataAccess
    {
        public static IDefineTable CreateDefineTable()
        {
            string className = path + ".DefineTable";
            return (IDefineTable)Assembly.Load(path).CreateInstance(className);
        }
    }
}