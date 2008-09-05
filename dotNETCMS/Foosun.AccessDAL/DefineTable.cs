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
    public class DefineTable : DbBase, IDefineTable
    {
        #region DefineTable.aspx
        public DataTable Sel_DefineInfoId()
        {
            string Sql = "Select DefineInfoId,DefineName,ParentInfoId From " + Pre + "define_class where SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, null);
        }
        public DataTable Sel_ParentInfoId(string PID)
        {
            OleDbParameter param = new OleDbParameter("@ParentInfoId",PID);
            string Sql = "Select DefineInfoId,DefineName,ParentInfoId From " + Pre + "define_class where ParentInfoId=@ParentInfoId and SiteID='" + Foosun.Global.Current.SiteID + "'";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel_defCname(string defCname)
        {
            OleDbParameter param = new OleDbParameter("@DefineCname", defCname);
            string Sql = "Select count(id) From " + Pre + "Define_Data  Where DefineCname=@DefineCname";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int sel_defEname(string defEname)
        {
            OleDbParameter param = new OleDbParameter("@defineColumns", defEname);
            string Sql = "Select count(id) From " + Pre + "Define_Data  Where defineColumns=@defineColumns";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }

        public int Add(string Str_ColumnsType, string defCname, string defEname, int definSelected, int Isnull, string defColumns, string defExp, string definedvalue)
        {
            OleDbParameter[] param = new OleDbParameter[8];
            param[0] = new OleDbParameter("@ColumnsType",OleDbType.VarWChar,12);
            param[0].Value= Str_ColumnsType;
            param[1] = new OleDbParameter("@defCname",OleDbType.VarWChar,50);
            param[1].Value= defCname;
            if (defEname == null)
                defEname = "";
            param[2] = new OleDbParameter("@defEname",OleDbType.VarWChar,50);
            param[2].Value= defEname;
            param[3] = new OleDbParameter("@defineType",OleDbType.Integer,4);
            param[3].Value= definSelected;
            param[4] = new OleDbParameter("@Is_null",OleDbType.Integer,4);
            param[4].Value= Isnull;
            if (defColumns == null)
                defColumns = "";
            param[5] = new OleDbParameter("@defColumns",OleDbType.VarWChar);
            param[5].Value= defColumns;
            if (defExp == null)
                defExp = "";
            param[6] = new OleDbParameter("@defExp",OleDbType.VarWChar,200);
            param[6].Value= defExp;
            if (definedvalue == null)
                definedvalue = "";
            param[7] = new OleDbParameter("@definedvalue", OleDbType.VarWChar, 200);
            param[7].Value= definedvalue;
            //已检查参数顺序正确
            string Sql = "Insert Into " + Pre + "Define_Data(defineInfoId,DefineCname,DefineColumns,defineType,IsNull,defineValue," +
                         "defineExpr,SiteID,definedvalue) Values(@ColumnsType,@defCname,@defEname,@defineType,@Is_null,@defColumns," +
                         "@defExp,'" + Foosun.Global.Current.SiteID + "',@definedvalue)";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region DefineTable_Edit_List.aspx
        public DataTable Str_Start_Sql(int ID)
        {
            OleDbParameter param = new OleDbParameter("@ID", ID);
            string Sql = "Select id,defineInfoId,defineCname,defineColumns,defineType,IsNull,defineValue,defineExpr,definedvalue From " + Pre + "define_data where Id=@ID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update(string Str_ColumnsType, string Str_DefName, string Str_DefEname, string Str_DefType, int Str_DefIsNull, string Str_DefColumns, string Str_DefExpr, int DefID, string definedvalue)
        {
            OleDbParameter[] param = new OleDbParameter[9];
            param[0] = new OleDbParameter("@ColumnsType", OleDbType.VarWChar, 12);
            param[0].Value = Str_ColumnsType;
            param[1] = new OleDbParameter("@defCname", OleDbType.VarWChar, 50);
            param[1].Value = Str_DefName;
            if (Str_DefEname == null)
                Str_DefEname = "";
            param[2] = new OleDbParameter("@defEname", OleDbType.VarWChar, 50);
            param[2].Value = Str_DefEname;
            param[3] = new OleDbParameter("@defineType", OleDbType.Integer, 4);
            param[3].Value = Convert.ToInt32(Str_DefType);
            param[4] = new OleDbParameter("@Is_null", OleDbType.Integer, 4);
            param[4].Value = Convert.ToInt32(Str_DefIsNull);
            param[5] = new OleDbParameter("@defColumns", OleDbType.VarWChar);
            if (Str_DefColumns == null)
                Str_DefColumns = "";
            param[5].Value = Str_DefColumns;
            param[6] = new OleDbParameter("@defExp", OleDbType.VarWChar, 200);
            if (Str_DefExpr == null)
                Str_DefExpr = "";
            param[6].Value = Str_DefExpr;
            param[7] = new OleDbParameter("@definedvalue", OleDbType.VarWChar, 200);
            if (definedvalue == null)
                definedvalue = "";
            param[7].Value = definedvalue;
            param[8] = new OleDbParameter("@DefID", OleDbType.Integer, 4);
            param[8].Value = DefID;
            //已检查参数顺序正确
            string Sql = "Update " + Pre + "define_data Set defineInfoId=@ColumnsType,defineCname=@defCname," +
                         "defineColumns=@defEname,defineType=@defineType,IsNull=@Is_null,defineValue=@defColumns," +
                         "defineExpr=@defExp,definedvalue=@definedvalue where id=@DefID";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region DefineTable_Edit_Manage.aspx
        public DataTable Str_DefID(string DefID)
        {
            OleDbParameter param = new OleDbParameter("@DefineInfoId", DefID);
            string Sql = "Select DefineInfoId,DefineName,ParentInfoId From " + Pre + "define_class where DefineInfoId=@DefineInfoId";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int Update1(string Str_NewText, string DefID)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@DefineName", Str_NewText), new OleDbParameter("@DefineInfoId", DefID) };
            string Sql = "Update " + Pre + "define_class Set DefineName=@DefineName where DefineInfoId=@DefineInfoId";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        #endregion

        #region DefineTable_List.aspx

        public DataTable GetPage(string defid, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string where = "";
            if (defid == null && defid == string.Empty)
            {
                where = "";
            }
            else
            {
                where = "  where defineInfoId=@defineInfoId";
            }
            OleDbParameter param = new OleDbParameter("@defineInfoId", defid);
            string AllFields = "id,defineInfoId,defineCname,defineType,[IsNull]";
            string Condition = "" + Pre + "Define_Data " + where + "";
            string IndexField = "id";
            string OrderFields = "order by id Desc";
            return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, param);
        }
        public int Str_Del_Data(string pr)
        {
            OleDbParameter param = new OleDbParameter("@defineInfoId", pr);
            string Sql = "Delete From " + Pre + "define_data where defineInfoId=@defineInfoId";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void Delete(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "define_data where Id in(" + CheckboxArray + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public int Delete1(int DefID)
        {
            string Sql = "Delete From " + Pre + "define_data where Id=" + DefID + "";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        #endregion


        public DataTable sel_Str(string Classid)
        {
            OleDbParameter param = new OleDbParameter("@ParentInfoID", Classid);
            string Sql = "Select DefineID,DefineInfoId,DefineName,ParentInfoID From " + Pre + "define_class where ParentInfoID=@ParentInfoID";
            return DbHelper.ExecuteTable(CommandType.Text, Sql, param);
        }
        public int sel_1(string _NewText)
        {
            OleDbParameter param = new OleDbParameter("@DefineName", _NewText);
            string Sql = "Select count(DefineId) From " + Pre + "Define_Class Where DefineName=@DefineName";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int sel_2(string rand)
        {
            OleDbParameter param = new OleDbParameter("@DefineInfoId", rand);
            string Sql = "Select count(DefineId) From " + Pre + "Define_Class Where DefineInfoId=@DefineInfoId";
            return (int)DbHelper.ExecuteScalar(CommandType.Text, Sql, param);
        }
        public int Add2(string rand, string _NewText, string _PraText)
        {
            OleDbParameter[] param = new OleDbParameter[] { new OleDbParameter("@DefineInfoId", rand), new OleDbParameter("@DefineName", _NewText), new OleDbParameter("@ParentInfoId", _PraText) };
            string Sql = "Insert Into " + Pre + "Define_Class(DefineInfoId,DefineName,ParentInfoId,SiteID) values(@DefineInfoId,@DefineName,@ParentInfoId,'" + Foosun.Global.Current.SiteID + "')";
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void Delete3(string DefID)
        {
            OleDbParameter param = new OleDbParameter("@DefineInfoId", DefID);
            string Sql = "Delete From " + Pre + "define_class where DefineInfoId=@DefineInfoId";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void Delete4(string DefID)
        {
            OleDbParameter param = new OleDbParameter("@ParentInfoId", DefID);
            string Sql = "Delete From " + Pre + "define_class where ParentInfoId=@ParentInfoId";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public void Delete5(string DefID)
        {
            OleDbParameter param = new OleDbParameter("@defineInfoId", DefID);
            string Sql = "Delete From " + Pre + "define_data where defineInfoId=@defineInfoId";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, param);
        }
        public int Delete6()
        {
            string Sql = "";
            if (Foosun.Global.Current.SiteID == "0") { Sql = "Delete From " + Pre + "define_class"; }
            else { Sql = "Delete From " + Pre + "define_class where SiteID='" + Foosun.Global.Current.SiteID + "'"; }
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public int Delete7()
        {
            string Sql = "";
            if (Foosun.Global.Current.SiteID == "0")
            { Sql = "Delete From " + Pre + "define_data"; }
            else { Sql = "Delete From " + Pre + "define_data where SiteID='" + Foosun.Global.Current.SiteID + "'"; }
            return DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }

        public void Delete8(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "define_class where DefineInfoId in(" + CheckboxArray + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
        public void Delete9(string CheckboxArray)
        {
            string Sql = "Delete From " + Pre + "define_class where ParentInfoId in(" + CheckboxArray + ")";
            DbHelper.ExecuteNonQuery(CommandType.Text, Sql, null);
        }
    }
}