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
    public partial class CustomForm_Add : Foosun.Web.UI.ManagePage
    {
        static private readonly string FormTbPre = Config.DBConfig.TableNamePrefix + "Form_";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.TxtFolder.Attributes.Add("readonly", "true");
                this.TxtStartTm.Attributes.Add("readonly", "true");
                this.TxtEndTm.Attributes.Add("readonly", "true");
                this.LblTablePre.Text = FormTbPre;
                if (Request.QueryString["id"] != null && Request.QueryString["id"].Trim() != string.Empty)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    this.HidID.Value = id.ToString();
                    this.TxtTableName.Attributes.Add("readonly", "true");
                    this.LtrCaption.Text = "修改表单";
                    Foosun.CMS.CustomForm customfm = new Foosun.CMS.CustomForm();
                    CustomFormInfo info = customfm.GetInfo(id);
                    this.TxtName.Text = info.formname;
                    this.TxtTableName.Text = Regex.Replace(info.formtablename, "^" + Regex.Escape(FormTbPre), "", RegexOptions.Compiled);
                    this.TxtFolder.Text = info.accessorypath;
                    if (info.accessorypath != string.Empty && info.accessorysize > 0)
                        this.TxtMaxSize.Text = info.accessorysize.ToString();
                    this.RadLock.Checked = info.islock;
                    this.RadNormal.Checked = !info.islock;
                    this.RadTimeLimited.Checked = info.timelimited;
                    this.RadTimeNotLmt.Checked = !info.timelimited;
                    if (info.timelimited)
                    {
                        this.TxtStartTm.Text = info.starttime.ToShortDateString();
                        this.TxtEndTm.Text = info.endtime.ToShortDateString();
                    }
                    this.UserPop1.AuthorityType = (int)info.isdelpoint;
                    this.UserPop1.Gold = info.gpoint;
                    this.UserPop1.Point = info.gpoint;
                    if (info.groupnumber != null && info.groupnumber != string.Empty)
                        this.UserPop1.MemberGroup = info.groupnumber.Split(',');
                    this.ChbOnce.Checked = info.submitonce;
                    this.ChbShowValidate.Checked = info.showvalidatecode;
                    this.TxtMemo.Text = info.memo;
                }
                else
                {
                    this.HidID.Value = "0";
                    this.LtrCaption.Text = "新建表单";
                }
            }
        }

        protected void BtnOK_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (this.RadTimeLimited.Checked && (this.TxtStartTm.Text.Trim() == string.Empty || this.TxtEndTm.Text.Trim() == string.Empty))
                {
                    PageError("开启时间限制,必须填写开始时间和结束时间!", "CustomForm.aspx");
                }
                CustomFormInfo cf = new CustomFormInfo();
                cf.id = int.Parse(this.HidID.Value);
                cf.formname = this.TxtName.Text.Trim();
                cf.formtablename = FormTbPre + this.TxtTableName.Text.Trim();
                cf.accessorypath = this.TxtFolder.Text.Trim();
                if (this.TxtMaxSize.Text.Trim() != string.Empty)
                    cf.accessorysize = int.Parse(this.TxtMaxSize.Text);
                cf.islock = this.RadLock.Checked;
                cf.timelimited = this.RadTimeLimited.Checked;
                if (cf.timelimited)
                {
                    cf.starttime = DateTime.Parse(this.TxtStartTm.Text);
                    cf.endtime = DateTime.Parse(this.TxtEndTm.Text);
                }
                #region 获得权限开始
                cf.isdelpoint = Convert.ToByte(this.UserPop1.AuthorityType);
                cf.gpoint = this.UserPop1.Gold;
                cf.gpoint = this.UserPop1.Point;
                string[] _GroupNumber = this.UserPop1.MemberGroup;
                string GroupNumber = string.Empty;
                foreach (string gnum in _GroupNumber)
                {
                    if (GroupNumber != string.Empty)
                        GroupNumber += ",";
                    GroupNumber += gnum;
                }
                cf.groupnumber = GroupNumber;
                cf.submitonce = this.ChbOnce.Checked;
                cf.showvalidatecode = this.ChbShowValidate.Checked;
                cf.memo = this.TxtMemo.Text.Trim();
                Foosun.CMS.CustomForm customfm = new Foosun.CMS.CustomForm();
                customfm.Edit(cf);
                if (cf.id > 0)
                    PageRight("修改自定义表单成功!", "CustomForm.aspx");
                else
                    PageRight("新建自定义表单成功!", "CustomForm.aspx");
                #endregion 获得权限结束
            }
        }
    }
}
