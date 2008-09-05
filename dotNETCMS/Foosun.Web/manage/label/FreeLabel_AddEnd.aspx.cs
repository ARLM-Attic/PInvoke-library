//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By JiangDong                       == 
//===========================================================
using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class manage_label_FreeLabel_AddEnd : Foosun.Web.UI.ManagePage
{
    public manage_label_FreeLabel_AddEnd()
    {
        Authority_Code = "T009";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";

        this.LnkBtnSave.Attributes.Add("onclick", "javascript:var s = oUtil.obj.getHTMLBody().trim();if(s.length>4000){alert('标签内容不能长于4000个字符');return false;}if(s.length < 1){ alert('标签内容不能为空');return false;}");
        Foosun.CMS.FreeLabel fb = new Foosun.CMS.FreeLabel();
        this.TxtLabelName.Attributes.Add("readonly","true");
        this.PreSteps.Visible = false;
        if (!Page.IsPostBack)
        {
            if(Request.Form["TxtName"] == null || Request.Form["TxtName"].Trim().Equals(""))
                PageError("自由标签名称不能为空!","");
            if(Request.Form["TxtSql"] == null || Request.Form["TxtSql"].Trim().Equals(""))
                PageError("SQL语句不能为空!","");
            this.TxtDescrpt.Text = Request.Form["Descrpt"];
            this.EdtContent.Value = Request.Form["StyleCon"];
            string labelnm = Request.Form["TxtName"].Trim();
            int id = int.Parse(Request.Form["LID"]);
            if (id > 0)
                this.LblNavigt.Text = this.LblCaption.Text = "修改自由标签";
            this.HidName.Value = "{FS_FREE_" + labelnm + "}";
            this.HidID.Value = id.ToString();
            if(fb.IsNameRepeat(id,labelnm))
            {
                PageError("标签名称重复!", "FreeLabel_List.aspx");
            }
            this.TxtLabelName.Text = "{FS_FREE_" + labelnm + "}";
            string dbtb1 = Request.Form["SelPrin"].Trim();
            string dbtb2 = Request.Form["SelSub"].Trim();
            string StrSql = Request.Form["TxtSql"].Trim();
            this.HidSQL.Value = StrSql;
            string pattern = @"select(\s+top\s\d+)?\s+(?<fields>.+?)\s+from";

            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = reg.Match(StrSql);
            if (m.Success)
            {
                string Fields = m.Groups["fields"].Value.Trim();
                if (Fields.Equals(""))
                {
                    PageError("SQL语句无效,没有要显示的字段名!", "");
                    return;
                }
                if (Fields.IndexOf(",") > 0)
                {
                    bool single = true;
                    if (Fields.IndexOf(dbtb1) >= 0 && Fields.IndexOf(dbtb2) >= 0)
                        single = false;
                    string[] Fld = Fields.Split(',');
                    foreach (string _fld in Fld)
                    {
                        string fd = _fld.Trim();
                        ListItem it = new ListItem();
                        it.Value = "[*"+ fd +"*]";
                        it.Text = fd;
                        if (!single)
                        {
                            if (fd.IndexOf(dbtb1 +".") == 0)
                            {
                                this.DdlField1.Items.Add(it);
                            }
                            else if(fd.IndexOf(dbtb2 +".") == 0)
                            {
                                this.DdlField2.Items.Add(it);
                            }
                        }
                        else
                        {
                            this.DdlField1.Items.Add(it);
                        }
                    }
                    if (single)
                        this.DdlField2.Visible = false;
                    
                }
                else
                {
                    ListItem it = new ListItem();
                    it.Value = "[*"+ Fields +"*]";
                    it.Text = Fields;
                    this.DdlField1.Items.Add(it);
                    this.DdlField2.Visible = false;
                }
            }
            else
            {
                PageError("SQL语句非法或无效!", "");
            }
        }
    }
    protected void LnkBtnSave_Click(object sender, EventArgs e)
    {
        string sname = this.HidName.Value;
        int id = int.Parse(this.HidID.Value);
        string lblsql = this.HidSQL.Value;
        string content = this.EdtContent.Value.Trim();
        content = Foosun.Common.Input.HtmlDecode(content);
        if (content.Equals(""))
        {
            PageError("标签内容不能为空!", "");
        }
        if (content.Length > 4000)
        {
            PageError("标签内容不能大于4000个字符!", "");
        }
        Foosun.Model.FreeLabelInfo info = new Foosun.Model.FreeLabelInfo(id, sname, lblsql, content, this.TxtDescrpt.Text.Trim());
        Foosun.CMS.FreeLabel fb = new Foosun.CMS.FreeLabel();
        if (id > 0)
        {
            if (fb.Update(info))
                PageRight("自由标签修改成功!", "freelabel_list.aspx",true);
            else
                PageError("自由标签修改失败，已存在同名称的标签!", "", true);
        }
        else
        {
            if (fb.Add(info))
                PageRight("自由标签添加成功!", "freelabel_list.aspx", true);
            else
                PageError("自由标签添加失败，已存在同名称的标签!", "", true);
        }
    }

    protected void reviewBtn_Click(object sender, EventArgs e)
    {
        if (reviewBtn.Text == "预览")
        {
            reviewBtn.Text = "隐藏预览";
            review.Style.Value = "display:block;";
            string sname = this.HidName.Value;
            int id = int.Parse(this.HidID.Value);
            string lblsql = this.HidSQL.Value;
            string content = this.EdtContent.Value.Trim();
            content = Foosun.Common.Input.HtmlDecode(content);
            if (content.Equals(""))
            {
                PageError("标签内容不能为空!", "");
            }
            if (content.Length > 4000)
            {
                PageError("标签内容不能大于4000个字符!", "");
            }
            Foosun.Model.FreeLabelInfo info = new Foosun.Model.FreeLabelInfo(id, sname, lblsql, content, this.TxtDescrpt.Text.Trim());
            Foosun.CMS.FreeLabel fb = new Foosun.CMS.FreeLabel();
            Foosun.Publish.FreeLabel flb = new Foosun.Publish.FreeLabel(sname, Foosun.Publish.LabelType.Free);
            string htmlStr = "";
            if (id > 0)
            {
                if (fb.Update(info))
                {
                    flb.GetContentFromDB();
                    flb.MakeHtmlCode();
                    htmlStr = flb.FinalHtmlCode;
                    review.InnerHtml = htmlStr;
                }
                else
                {
                    PageError("自由标签修改失败，已存在同名称的标签!", "");
                }
            }
            else
            {
                if (fb.Add(info))
                {
                    flb.GetContentFromDB();
                    flb.MakeHtmlCode();
                    htmlStr = flb.FinalHtmlCode;
                    review.InnerHtml = htmlStr;
                }
                else
                {
                    PageError("自由标签添加失败，已存在同名称的标签!", "");
                }
            }
        }
        else
        {
            reviewBtn.Text = "预览";
            review.Style.Value = "display:none;";
        }
    }
}
