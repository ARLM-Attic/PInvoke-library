using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface ILabel
    {
        int LabelAdd(LabelInfo lbc);
        int LabelEdit(LabelInfo lbc);
        void LabelDel(string id);
        void LabelDels(string id);
        void LabelBackUp(string id);
        DataTable GetLabelInfo(string id);
        int LabelClassAdd(LabelClassInfo lbcc);
        int LabelClassEdit(LabelClassInfo lbcc);
        void LabelClassDel(string id);
        void LabelClassDels(string id);
        DataTable GetLabelClassInfo(string id);
        DataTable GetLabelClassList();
        DataTable GetLabelinClassList();
        void LabelToResume(string id);
        DataTable getRuleID();
        DataTable getTodayPicID();
        DataTable getfreeJSInfo();
        DataTable getsysJSInfo();
        DataTable getadsJsInfo();
        DataTable getsurveyJSInfo();
        DataTable getstatJSInfo();
        DataTable getDiscussInfo();
        DataTable getLableList(string SiteID, int intsys);
        DataTable getfreeLableList();
        DataTable getFreeLabelInfo();
        DataTable outLabelALL(int Num);
        DataTable outLabelmutile(string LabelID);
        void inserLabelLocal(string LabelID,string Classid, string Label_Name, string Label_Content, string Description,string isSystem);
        DataTable getLableListM(int Num, string ParentID);
        int getClassLabelCount(string ClassID, int num);
        IDataReader GetStyleList(string SiteID);
    }

    public sealed partial class DataAccess
    {
        public static ILabel CreateLabel()
        {
            string className = path + ".Label";
            return (ILabel)Assembly.Load(path).CreateInstance(className);
        }
    }
}
