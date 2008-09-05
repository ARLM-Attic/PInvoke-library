using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface ICustomForm
    {
        void Edit(CustomFormInfo info);
        CustomFormInfo GetInfo(int id);
        void DeleteForm(int id);
        string GetFormName(int id);
        CustomFormItemInfo GetFormItemInfo(int itemid);
        int GetItemCount(int formid);
        void EditFormItem(CustomFormItemInfo info);
        void DeleteFormItem(int itemid);
        IList<CustomFormItemInfo> GetAllInfo(int formid, out CustomFormInfo FormInfo);
        void AddRecord(int formid, SQLConditionInfo[] data);
        DataTable GetSubmitData(int formid, out string formname, out string tablename);
        void TruncateTable(int formid);
    }
    public sealed partial class DataAccess
    {
        public static ICustomForm CreateCustomForm()
        {
            string className = path + ".CustomForm";
            return (ICustomForm)Assembly.Load(path).CreateInstance(className);
        }
    }
}
