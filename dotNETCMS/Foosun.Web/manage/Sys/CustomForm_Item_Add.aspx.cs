using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.Model;

namespace Foosun.Web.manage.Sys
{
    public partial class CustomForm_Item_Add : Foosun.Web.UI.ManagePage
    {
        Foosun.CMS.CustomForm customfm = new Foosun.CMS.CustomForm();
        static private readonly string FormTbPre = Config.DBConfig.TableNamePrefix + "Form_";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string[] ftype = System.Enum.GetNames(typeof(EnumCstmFrmItemType));
                if (ftype != null && ftype.Length > 0)
                {
                    foreach (string s in ftype)
                    {
                        ListItem it = new ListItem();
                        it.Value = s;
                        it.Text = CustomFormItemInfo.GetFieldTypeName((EnumCstmFrmItemType)Enum.Parse(typeof(EnumCstmFrmItemType), s));
                        if (s != string.Empty)
                            this.DdlItemType.Items.Add(it);
                    }
                }
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim() != string.Empty)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    this.HidItemID.Value = id.ToString();
                    this.TxtFieldName.Attributes.Add("readonly", "true");
                    this.LtrCaption.Text = "修改表单项";
                    CustomFormItemInfo info = customfm.GetFormItemInfo(id);
                    this.HlkManage.NavigateUrl = this.HlkBack.NavigateUrl = "CustomForm_Item.aspx?id=" + info.formid;
                    this.HidFormID.Value = info.formid.ToString();
                    this.LblFormName.Text = info.formname;
                    this.TxtName.Text = info.itemname;
                    this.TxtFieldName.Text = info.fieldname;
                    if (info.islock)
                        this.RadOpenNo.Checked = true;
                    else
                        this.RadOpenYes.Checked = true;
                    if (info.isnotnull)
                        this.RadNotNullYes.Checked = true;
                    else
                        this.RadNotNullNo.Checked = true;
                    this.DdlItemType.SelectedValue = info.itemtype.ToString();
                    if (info.itemtype == EnumCstmFrmItemType.RadioBox ||
                        info.itemtype == EnumCstmFrmItemType.CheckBox ||
                        info.itemtype == EnumCstmFrmItemType.DropList ||
                        info.itemtype == EnumCstmFrmItemType.List)
                    {
                        this.TxtSelectItem.Text = info.selectitem;
                    }
                    this.TxtDefault.Text = info.defaultvalue;
                    this.TxtMaxSize.Text = info.itemsize.ToString();
                    this.TxtPrompt.Text = info.prompt;
                    int n = customfm.GetItemCount(info.formid);
                    BindSnNumber(n);
                    this.DdlSN.SelectedValue = info.seriesnumber.ToString();
                    this.DdlItemType.Enabled = false;
                }
                else if (Request.QueryString["formid"] != null && Request.QueryString["formid"].Trim() != string.Empty)
                {
                    int frmid = int.Parse(Request.QueryString["formid"]);
                    this.HidFormID.Value = frmid.ToString();
                    this.HidItemID.Value = "0";
                    this.HlkManage.NavigateUrl = this.HlkBack.NavigateUrl = "CustomForm_Item.aspx?id=" + frmid;
                    this.LblFormName.Text = customfm.GetFormName(frmid);
                    int n = customfm.GetItemCount(frmid);
                    BindSnNumber(++n);
                    this.LtrCaption.Text = "新建表单项";
                }
                else
                {
                    PageError("参数不完整!", "CustomForm.aspx");
                }
            }
        }
        private void BindSnNumber(int count)
        {
            if (count < 1)
                return;
            for (int i = 1; i <= count; i++)
            {
                ListItem it = new ListItem(i.ToString(), i.ToString());
                if (i == count)
                    it.Selected = true;
                this.DdlSN.Items.Add(it);
            }
        }
        protected void BtnOK_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CustomFormItemInfo info = new CustomFormItemInfo();
                info.id = int.Parse(this.HidItemID.Value);
                info.formid = int.Parse(this.HidFormID.Value);
                info.itemname = this.TxtName.Text.Trim();
                info.fieldname = this.TxtFieldName.Text.Trim();
                if (this.RadOpenNo.Checked)
                    info.islock = true;
                else
                    info.islock = false;
                if (this.RadNotNullYes.Checked)
                    info.isnotnull = true;
                else
                    info.isnotnull = false;
                info.itemtype = (EnumCstmFrmItemType)Enum.Parse(typeof(EnumCstmFrmItemType), this.DdlItemType.SelectedValue);

                if (info.itemtype == EnumCstmFrmItemType.RadioBox ||
                        info.itemtype == EnumCstmFrmItemType.CheckBox ||
                        info.itemtype == EnumCstmFrmItemType.DropList ||
                        info.itemtype == EnumCstmFrmItemType.List)
                {
                    info.selectitem = this.TxtSelectItem.Text;
                }
                info.defaultvalue = this.TxtDefault.Text.Trim();
                if (info.defaultvalue != string.Empty)
                {
                    if (info.itemtype == EnumCstmFrmItemType.DateTime)
                    {
                        DateTime dt;
                        if (!DateTime.TryParse(info.defaultvalue, out dt))
                        {
                            PageError("默认值格式不是正确的日期型格式", "CustomForm_Item.aspx?id=" + info.formid);
                        }
                    }
                    else if (info.itemtype == EnumCstmFrmItemType.Numberic)
                    {
                        double db;
                        if (!double.TryParse(info.defaultvalue, out db))
                        {
                            PageError("默认值格式不是正确的数字格式", "CustomForm_Item.aspx?id=" + info.formid);
                        }
                    }
                }
                if (this.TxtMaxSize.Text.Trim() != string.Empty)
                    info.itemsize = int.Parse(this.TxtMaxSize.Text);
                info.prompt = this.TxtPrompt.Text;
                info.seriesnumber = int.Parse(this.DdlSN.SelectedValue);
                customfm.EditFormItem(info);
                if (info.id > 0)
                    PageRight("修改自定义表单成功!", "CustomForm_Item.aspx?id=" + info.formid);
                else
                    PageRight("新建自定义表单成功!", "CustomForm_Item.aspx?id=" + info.formid);
            }
        }
    }
}
