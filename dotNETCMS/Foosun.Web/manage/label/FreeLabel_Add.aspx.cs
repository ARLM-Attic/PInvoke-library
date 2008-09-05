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
using System.Web.Security;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.Model;

public partial class manage_label_FreeLabel_Add : Foosun.Web.UI.ManagePage
{
    public manage_label_FreeLabel_Add()
    {
        Authority_Code = "T009";
    }
    protected string TabList1 = "";
    protected string TabList2 = "";
    protected string JoinFlds1 = "";
    protected string JoinFlds2 = "";
    protected int id = 0;
    protected string stylecon = "";
    protected string descrpt = "";
    protected string lblname = "";
    protected string List1 = "";
    protected string List2 = "";
    protected string TopNum = "";
    protected string lblsql = "";
    protected string Caption = "添加自由标签";
    protected void Page_Load(object sender, EventArgs e)
    {
        #region AJAX
        if (Request.Form["Option"] != null && Request.Form["Option"].Equals("GetFields") && Request.Form["TableName"] != null && !Request.Form["TableName"].Equals(""))
        {
            Foosun.CMS.FreeLabel freelbl = new Foosun.CMS.FreeLabel();
            IList<FreeLablelDBInfo> flds = freelbl.GetFields(Request.Form["TableName"]);
            int i = 0;
            foreach (FreeLablelDBInfo flinfo in flds)
            {
                if (i > 0)
                    Response.Write(";");
                Response.Write(flinfo.Name +","+ flinfo.TypeName);
                i++;
            }
            Response.End();
        }
        #endregion AJAX
        if (!Page.IsPostBack)
        {
            string tab1 = "", tab2 = "";
            Foosun.CMS.FreeLabel fb = new Foosun.CMS.FreeLabel();
            if (Request.QueryString["id"] != null && !Request.QueryString["id"].Trim().Equals(""))
            {
                id = int.Parse(Request.QueryString["id"]);
                FreeLabelInfo ln = fb.GetSingle(id);
                stylecon = ln.StyleContent;
                descrpt = ln.Description;
                lblsql = ln.LabelSQL;
                lblname = ln.LabelName.Replace("{FS_FREE_", "").Replace("}", "");
                string pattern = @"select\s(top\s(?<tp>\d+)\s)?(?<flds>.+?)\sfrom\s(?<tb1>[^\s,]+)(,(?<tb2>\S+))?(\s?where\s(?<con>.+))?";
                if (lblsql.ToLower().IndexOf(" order by ") > 0)
                    pattern += @"\sorder\sby\s(?<odr>.+)";
                Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match m = reg.Match(lblsql);
                if (m.Success)
                {
                    string con = "", ord = "";
                    string flds = m.Groups["flds"].Value.Trim();
                    tab1 = m.Groups["tb1"].Value.Trim();
                    if (m.Groups["tp"] != null) TopNum = m.Groups["tp"].Value.Trim();
                    if (m.Groups["tb2"] != null) tab2 = m.Groups["tb2"].Value.Trim();
                    if (m.Groups["con"] != null) con = m.Groups["con"].Value.Trim();
                    if (m.Groups["odr"] != null) ord = m.Groups["odr"].Value.Trim();
                    bool flagdbl = false;
                    string jnfld1 = "", jnfld2 = "";
                    if (!tab1.Equals("") && !tab2.Equals(""))
                    {
                        flagdbl = true;
                        string profile = tab1 + @"\.(?<jn1>[^=]+)\s?=\s?" + tab2 + @"\.(?<jn2>\S+)";
                        Regex regex = new Regex(profile, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        Match match = regex.Match(con);
                        if (match.Success)
                        {
                            jnfld1 = match.Groups["jn1"].Value.Trim();
                            jnfld2 = match.Groups["jn2"].Value.Trim();
                            con = regex.Replace(con, "");
                        }
                    }
                    if (!tab1.Equals(""))
                    {
                        string option = "";
                        List1 = GetFields(fb, tab1, flds, con, ord, flagdbl, jnfld1, out option);
                        JoinFlds1 = option;
                    }
                    if (!tab2.Equals(""))
                    {
                        string option = "";
                        List2 = GetFields(fb, tab2, flds, con, ord, flagdbl, jnfld2, out option);
                        JoinFlds2 = option;
                    }
                    Caption = "修改自由标签";
                }
            }
            else
            {
                TopNum = "10";
            }
            IList<FreeLablelDBInfo> tbs = fb.GetTables();
            foreach (FreeLablelDBInfo info in tbs)
            {
                TabList1 += "<option";
                if (info.Name.Equals(tab1))
                    TabList1 += " selected=\"selected\"";
                TabList1 += " value=\"" + info.Name + "\">" + info.Description + "</option>\r";
                TabList2 += "<option";
                if (info.Name.Equals(tab2))
                    TabList2 += " selected=\"selected\"";
                TabList2 += " value=\"" + info.Name + "\">" + info.Description + "</option>\r";

            }
            DataBind();
        }
    }

    private string GetFields(Foosun.CMS.FreeLabel fb, string tabnm, string fdlst, string cond, string ordr, bool dbflag, string jnfld, out string opt)
    {
        string ret = "";
        opt = "";
        IList<FreeLablelDBInfo> fds = fb.GetFields(tabnm);
        foreach (FreeLablelDBInfo info in fds)
        {
            opt += "<option";
            if (info.Name == jnfld)
                opt += " selected=\"selected\"";
            opt += " value=\"" + info.Name + "\">" + info.Name + "</option>\r";
            string fld = info.Name;
            if (dbflag)
                fld = tabnm + "." + info.Name;
            ret += "<tr style=\"cursor:hand\" onclick=\"RowClick(this)\" height=\"22\">\r";
            ret += "<td>" + info.Name + "</td>\r";
            ret += "<td>" + info.TypeName + "</td>\r<td align=\"center\">";
            if (fdlst.IndexOf(fld) >= 0)
                ret += "√";
            ret += "</td>\r<td align=\"center\">";
            if (cond.IndexOf(fld) >= 0)
            {
                string pattern = fld + @"\s?(?<con>\S+\s*\S+)";
                Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Match m = reg.Match(cond);
                if (m.Success)
                {
                    string con = m.Groups["con"].Value.Trim();
                    ret += con;
                }
            }
            ret += "</td>\r<td align=\"center\">";
            if (ordr.IndexOf(fld + " ASC") >= 0)
                ret += "升序";
            else if (ordr.IndexOf(fld + " DESC") >= 0)
                ret += "降序";
            ret += "</td>\r</tr>";
        }
        return ret;
    }
}
