//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Xi.Deng                         ==
//===========================================================
using System;
using System.Collections.Generic;
using System.Data;
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class Label
    {
        private ILabel labelclass;
        public Label()
        {
            labelclass = DataAccess.CreateLabel();
        }
        public int LabelAdd(LabelInfo lbc)
        {
            int result = labelclass.LabelAdd(lbc);
            return result;
        }
        public int LabelEdit(LabelInfo lbc)
        {
            int result = labelclass.LabelEdit(lbc);
            return result;
        }
        public void LabelDel(string id)
        {
            labelclass.LabelDel(id);
        }
        public void LabelDels(string id)
        {
            labelclass.LabelDels(id);
        }
        public void LabelBackUp(string id)
        {
            labelclass.LabelBackUp(id);
        }
        public DataTable GetLabelInfo(string id)
        {
            DataTable dt = labelclass.GetLabelInfo(id);
            return dt;
        }
        public int LabelClassAdd(LabelClassInfo lbcc)
        {
            int result = labelclass.LabelClassAdd(lbcc);
            return result;
        }
        public int LabelClassEdit(LabelClassInfo lbcc)
        {
            int result = labelclass.LabelClassEdit(lbcc);
            return result;
        }
        public void LabelClassDel(string id)
        {
            labelclass.LabelClassDel(id);
        }
        public void LabelClassDels(string id)
        {
            labelclass.LabelClassDels(id);
        }
        public DataTable GetLabelClassInfo(string id)
        {
            DataTable dt = labelclass.GetLabelClassInfo(id);
            return dt;
        }
        public DataTable GetLabelClassList()
        {
            DataTable dt = labelclass.GetLabelClassList();
            return dt;
        }
        public DataTable GetLabelinClassList()
        {
            DataTable dt = labelclass.GetLabelinClassList();
            return dt;
        }
        public void LabelToResume(string id)
        {
            labelclass.LabelToResume(id);
        }
        public DataTable getRuleID()
        {
            DataTable dt = labelclass.getRuleID();
            return dt;
        }
        public DataTable getTodayPicID()
        {
            DataTable dt = labelclass.getTodayPicID();
            return dt;
        }
        public DataTable getfreeJSInfo()
        {
            DataTable dt = labelclass.getfreeJSInfo();
            return dt;
        }
        public DataTable getsysJSInfo()
        {
            DataTable dt = labelclass.getsysJSInfo();
            return dt;
        }
        public DataTable getadsJsInfo()
        {
            DataTable dt = labelclass.getadsJsInfo();
            return dt;
        }
        public DataTable getsurveyJSInfo()
        {
            DataTable dt = labelclass.getsurveyJSInfo();
            return dt;
        }
        public DataTable getstatJSInfo()
        {
            DataTable dt = labelclass.getstatJSInfo();
            return dt;
        }
        public DataTable getDiscussInfo()
        {
            DataTable dt = labelclass.getDiscussInfo();
            return dt;
        }
        public DataTable getLableList(string SiteID, int intsys)
        {
            DataTable dt = labelclass.getLableList(SiteID, intsys);
            return dt;
        }
        public DataTable getfreeLableList()
        {
            DataTable dt = labelclass.getfreeLableList();
            return dt;
        }
        public DataTable getFreeLabelInfo()
        {
            DataTable dt = labelclass.getFreeLabelInfo();
            return dt;
        }

        public DataTable outLabelALL(int Num)
        {
            DataTable dt = labelclass.outLabelALL(Num);
            return dt;
        }

        public DataTable outLabelmutile(string LabelID)
        {
            DataTable dt = labelclass.outLabelmutile(LabelID);
            return dt;
        }

        //string Classid = this.LabelClass.SelectedValue;
        //string Label_Name = Foosun.Common.Public.getXmlValue(xmlPath, "labelname");
        //string Label_Content = Foosun.Common.Public.getXmlValue(xmlPath, "labelcontent");
        //string Description = Foosun.Common.Public.getXmlValue(xmlPath, "labeldescription");
        //string CreatTime = Foosun.Common.Public.getXmlValue(xmlPath, "labelcreattime");
        //string isSys = Foosun.Common.Public.getXmlValue(xmlPath, "labelissys");
        //string SiteID = Foosun.Global.Current.SiteID;

        public void inserLabelLocal(string LabelID,string Classid,string Label_Name,string Label_Content,string Description,string isSystem)
        {
            labelclass.inserLabelLocal(LabelID, Classid, Label_Name, Label_Content, Description, isSystem);
        }

        /// <summary>
        /// 得到栏目/专题动态标签
        /// </summary>
        /// <param name="Num">0栏目，1为专题</param>
        /// <returns></returns>
        public DataTable getLableListM(int Num, string ParentID)
        {
            DataTable dt = labelclass.getLableListM(Num, ParentID);
            return dt;
        }

        /// <summary>
        /// 栏目下标签数
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public int getClassLabelCount(string ClassID,int num)
        {
            return labelclass.getClassLabelCount(ClassID, num);
        }

        public IDataReader GetStyleList(string SiteID)
        {
            return labelclass.GetStyleList(SiteID);
        }
    }
}
