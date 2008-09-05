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
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;

public partial class manage_collect_Collect_RuleAdd : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        this.BtnOK.Attributes.Add("onclick", "javascript:if(!window.confirm('您确定要修改规则吗？所有选中的采集站点将应用新的规则取代原来的规则！'))return false;");
        int n = 0;
        if (Request["RID"] == null || Request["RID"].Trim().Equals(""))
        {
            this.RID.Value = "";
            this.LblTitle.Text = "新建规则";
            this.ChbCase.Checked = true;
        }
        else
        {
            this.LblTitle.Text = "修改规则";
            try
            {
                n = int.Parse(Request["RID"]);
                this.RID.Value = n.ToString();
            }
            catch
            {
                PageError("输入的参数无效!", "");
                return;
            }
        }
        //this.EdtOldStr.SetTag = new string[] { "[过滤字符串]", "[变量]" };
        this.EdtOldStr.SetTag = new string[] { "[变量]" };
        Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
        DataTable tb = cl.SiteList();
        if (tb != null)
        {
            foreach (DataRow r in tb.Rows)
            {
                TableRow tr = new TableRow();
                TableCell tc = new TableCell();
                CheckBox chb = new CheckBox();
                if (!r.IsNull("RuleID"))
                {
                    chb.ID = "ChbON_" + r["id"].ToString();
                    if (!r["RuleID"].ToString().Equals(n.ToString()))
                        tc.Style.Add("color", "Gray");
                }
                else
                {
                    chb.ID = "ChbOF_" + r["id"].ToString();
                }
                if (!r.IsNull("RuleID") && r["RuleID"].ToString().Equals(n.ToString()))
                    chb.Checked = true;
                chb.Text = r["SiteName"].ToString();
                tc.Controls.Add(chb);
                tr.Cells.Add(tc);
                this.TabRuleApply.Rows.Add(tr);
            }
            tb.Dispose();
        }
        if (!Page.IsPostBack)
        {       
            if(n>0)
            {
                tb = cl.GetRule(n);
                if (tb != null && tb.Rows.Count > 0)
                {
                    this.TxtRuleName.Text = tb.Rows[0]["RuleName"].ToString();
                    this.EdtOldStr.Text = tb.Rows[0]["OldContent"].ToString();
                    if(!tb.Rows[0].IsNull("ReContent"))this.TxtNewStr.Text = tb.Rows[0]["ReContent"].ToString();
                    if(bool.Parse(tb.Rows[0]["IgnoreCase"].ToString())) this.ChbCase.Checked = true; else this.ChbCase.Checked=false;
                }
                else
                {
                    PageError("没有找到相关的规则记录!", "");
                }
            }
            this.DataBind();
        }
    }
    protected void BtnOK_Click(object sender, EventArgs e)
    {       
        if (Page.IsValid)
        {
            if (TxtRuleName.Text.Trim().Equals(""))
            {
                PageError("规则名称请必须填写!", "");
            }
            if (EdtOldStr.Text.Trim().Equals(""))
            {
                PageError("过滤字符串请必须填写!", "");
            }
            if (TxtNewStr.Text.Trim().Equals(""))
            {
                PageError("请必须填写!", "");
            }
            Foosun.CMS.Collect.Collect cl = new Foosun.CMS.Collect.Collect();
            if (this.RID.Value.Trim().Equals("") || this.RID.Value.Trim().Equals("0"))
            {
                int nid = cl.RuleAdd(TxtRuleName.Text.Trim(), EdtOldStr.Text.Trim(), TxtNewStr.Text.Trim(), GetSelectedSite(), ChbCase.Checked);
                this.RID.Value = nid.ToString();
                PageRight("新增规则成功!", "");
            }
            else
            {
                int id = int.Parse(RID.Value);
                cl.RuleUpdate(id, TxtRuleName.Text.Trim(), EdtOldStr.Text.Trim(), TxtNewStr.Text.Trim(), GetSelectedSite(), ChbCase.Checked);
                PageRight("修改规则成功!", "");
            }
        }
    }

    private int[] GetSelectedSite()
    {
        ArrayList lstsite = new ArrayList();
        lstsite.Clear();
        foreach (TableRow tr in this.TabRuleApply.Rows)
        {
            TableCell tc = tr.Cells[0];
            if (tc != null)
            {
                foreach (Control cl in tc.Controls)
                {
                    if (cl.GetType().Equals(typeof(CheckBox)))
                    {
                        string cid = cl.ID;
                        if (((CheckBox)cl).Checked)
                        {
                            int n = int.Parse(cid.Substring(6));
                            lstsite.Add(n);
                        }
                        break;
                    }
                }
            }
        }
        return (int[])lstsite.ToArray(typeof(int));
    }
}
