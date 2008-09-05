using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class CustomForm
    {
        private ICustomForm dal;
        public CustomForm()
        {
            dal = DataAccess.CreateCustomForm();
        }
        /// <summary>
        /// 新增或修改一个自定义表单
        /// </summary>
        /// <param name="info"></param>
        public void Edit(CustomFormInfo info)
        {
            dal.Edit(info);
        }
        /// <summary>
        /// 获取表单的信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CustomFormInfo GetInfo(int id)
        {
            return dal.GetInfo(id);
        }
        /// <summary>
        /// 删除一个表单
        /// </summary>
        /// <param name="id"></param>
        public void DeleteForm(int id)
        {
            dal.DeleteForm(id);
        }
        /// <summary>
        /// 取得表单名称，如果ID不存在则抛出异常
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFormName(int id)
        {
            return dal.GetFormName(id);
        }
        /// <summary>
        /// 取得表单项信息
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public CustomFormItemInfo GetFormItemInfo(int itemid)
        {
            return dal.GetFormItemInfo(itemid);
        }
        /// <summary>
        /// 取得已有表单项的总数
        /// </summary>
        /// <param name="formid"></param>
        /// <returns></returns>
        public int GetItemCount(int formid)
        {
            return dal.GetItemCount(formid);
        }
        /// <summary>
        /// 新增或修改表单项
        /// </summary>
        /// <param name="info"></param>
        public void EditFormItem(CustomFormItemInfo info)
        {
            dal.EditFormItem(info);
        }
        /// <summary>
        /// 删除一个表单项
        /// </summary>
        /// <param name="itemid"></param>
        public void DeleteFormItem(int itemid)
        {
            dal.DeleteFormItem(itemid);
        }
        /// <summary>
        /// 获取表单的所有信息
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="FormInfo"></param>
        /// <returns></returns>
        public IList<CustomFormItemInfo> GetAllInfo(int formid,out CustomFormInfo FormInfo)
        {
            return dal.GetAllInfo(formid, out FormInfo);
        }
        /// <summary>
        /// 取得提交的数
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="formname"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public DataTable GetSubmitData(int formid, out string formname, out string tablename)
        {
            return dal.GetSubmitData(formid, out  formname, out  tablename);
        }
        /// <summary>
        /// 清空所有提交记录
        /// </summary>
        /// <param name="formid"></param>
        public void TruncateTable(int formid)
        {
            dal.TruncateTable(formid);
        }
        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="formid"></param>
        /// <param name="data"></param>
        public void AddRecord(int formid, SQLConditionInfo[] data)
        {
            dal.AddRecord(formid, data);
        }
        public string GetHtmlCode(int formid)
        {
            string s = string.Empty;
            CustomFormInfo FormInfo;
            bool bupfile = false;
            IList<CustomFormItemInfo> list = GetAllInfo(formid, out FormInfo);

            s += "<input id=\"CustomFormID\" name=\"CustomFormID\" type=\"hidden\" value=\"" + FormInfo.id + "\" />\r\n";
            s += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\" id=\"tablist\" class=\"table\">\r\n";
            foreach (CustomFormItemInfo ItInfo in list)
            {
                if (ItInfo.itemtype == EnumCstmFrmItemType.UploadFile)
                    bupfile = true;
                s += " <tr class=\"TR_BG_list\">\r\n";
                s += "<td width=\"20%\" align=\"right\" class=\"list_link\">" + ItInfo.itemname + "：</td>\r\n";
                s += "<td width=\"80%\" class=\"list_link\" align=\"left\">";
                s += GetHtmlControl(ItInfo);
                if (ItInfo.prompt.Trim() != string.Empty)
                    s += ItInfo.prompt;
                s += "</td>\r\n</tr>\r\n";
            }
            if (FormInfo.showvalidatecode)
            {
                s += " <tr class=\"TR_BG_list\">\r\n";
                s += "<td width=\"20%\" align=\"right\" class=\"list_link\">效验码：</td>\r\n";
                s += "<td width=\"80%\" class=\"list_link\" align=\"left\"><input type=\"text\" id=\"CFValidate\" name=\"CFValidate\" />\r\n";
                s += "<img id=\"IMGValidCode\" width=\"70\" onClick=\"this.src+=Math.random()\" alt=\"图片看不清？点击重新得到验证码\" style=\"cursor:pointer;\" height=\"23\" hspace=\"4\" />";
                s += "<script type=\"text/javascript\" language=\"JavaScript\">\r\n";
                s += "document.getElementById('IMGValidCode').src='/comm/Image.aspx?k='+ Math.random();\r\n";
                s += "</script>\r\n";
                s += "</td>\r\n</tr>\r\n";
            }
            s += "<tr class=\"TR_BG_list\">\r\n";
            s += "<td align=\"center\" colspan=\"2\" class=\"list_link\">";
            s += "<input type=\"submit\" value=\" 提交 \" />  <input type=\"reset\" value=\" 重写 \" />";
            s += "</td>\r\n</tr>\r\n";
            s += "</table>\r\n";
            s += "</form>";
            string _s = "<form name=\"form1\" method=\"post\" action=\"/customform/CustomFormSubmit.aspx\" id=\"form1\"";
            if (bupfile)
                _s += " enctype=\"multipart/form-data\"";
            _s += ">\r\n";
            s = _s + s;
            return s;
        }
        protected string GetHtmlControl(CustomFormItemInfo info)
        {
            string s = string.Empty;
            if (info.itemtype == EnumCstmFrmItemType.SingleLineText ||
                info.itemtype == EnumCstmFrmItemType.PassWordText ||
                info.itemtype == EnumCstmFrmItemType.Numberic ||
                info.itemtype == EnumCstmFrmItemType.DateTime ||
                info.itemtype == EnumCstmFrmItemType.UploadFile)
            {
                s = "<input type=\"";
                if (info.itemtype == EnumCstmFrmItemType.PassWordText)
                    s += "password";
                else if (info.itemtype == EnumCstmFrmItemType.UploadFile)
                    s += "file";
                else
                    s += "text";
                s += "\" name=\"CF_" + info.fieldname + "\" id=\"CF_" + info.fieldname + "\"";
                if (info.itemsize > 0)
                    s += " size=\"" + info.itemsize + "\"";
                if (info.defaultvalue != string.Empty && info.itemtype != EnumCstmFrmItemType.PassWordText)
                    s += " value=\"" + info.defaultvalue + "\"";
                s += " />";
            }
            else if (info.itemtype == EnumCstmFrmItemType.MultiLineText)
            {
                s = "<textarea name=\"CF_" + info.fieldname + "\" id=\"CF_" + info.fieldname + "\"";
                if (info.itemsize > 0)
                    s += " cols=\"" + info.itemsize + "\"";
                s += ">";
                if (info.defaultvalue != string.Empty)
                    s += info.defaultvalue;
                s += "</textarea>";
            }
            else if (info.itemtype == EnumCstmFrmItemType.CheckBox ||
                     info.itemtype == EnumCstmFrmItemType.RadioBox)
            {
                string[] selitm = info.selectitem.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string _itm in selitm)
                {
                    s += "<input name=\"CF_" + info.fieldname + "\" id=\"CF_" + info.fieldname + "_" + _itm + "\" type=\"";
                    if (info.itemtype == EnumCstmFrmItemType.CheckBox)
                        s += "checkbox";
                    else
                        s += "radio";
                    s += "\" value=\"" + _itm + "\"";
                    if (_itm == info.defaultvalue)
                        s += " checked=\"checked\"";
                    s += "/>" + _itm;
                }
            }
            else if (info.itemtype == EnumCstmFrmItemType.DropList ||
                        info.itemtype == EnumCstmFrmItemType.List)
            {
                s = "<select name=\"CF_" + info.fieldname + "\" id=\"CF_" + info.fieldname + "\">";
                string[] selitm = info.selectitem.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string _itm in selitm)
                {
                    s += "<option value=\"" + _itm + "\"";
                    if (_itm == info.defaultvalue)
                        s += " selected=\"selected\"";
                    s += ">" + _itm;
                    s += "</option>\r\n";
                }
                s += "</select>";
            }
            return s;
        }
    }
}
