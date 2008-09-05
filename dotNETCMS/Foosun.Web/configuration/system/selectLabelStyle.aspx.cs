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
using Foosun.CMS;
using Foosun.CMS.Common;
using System.Text.RegularExpressions;

public partial class configuration_system_selectLabelStyle : Foosun.Web.UI.DialogPage
{
    public configuration_system_selectLabelStyle()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Response.CacheControl = "no-cache";
            Response.Expires = 0;
            styleList.InnerHtml = newsstr();
        }
    }

    string newsstr()
    {
        string liststr = "";
        DataTable dt = pd.GetselectLabelList();
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count;i++ )
            {
                string imgstr = "";
                if (int.Parse(dt.Rows[i]["HasSub"].ToString()) > 0)
                {
                    imgstr = "<img src=\"../../sysImages/normal/s.gif\" id=item$pval[CatID]) style=\"CURSOR: hand\" onmouseup=\"opencat(mid" + dt.Rows[i]["id"] + ");\" language=javascript alt=\"点击展开栏目下样式\" onClick=\"javascript:SwitchImg(this);\" border=\"0\"  class=\"LableItem\" />";
                }
                else
                {
                    imgstr = "<img src=\"../../sysImages/normal/b.gif\" id=item$pval[CatID]) style=\"CURSOR: hand\" onmouseup=\"opencat(mid" + dt.Rows[i]["id"] + ");\" language=javascript alt=\"点击展开栏目下样式\" onClick=\"javascript:SwitchImg(this);\" border=\"0\"  class=\"LableItem\" />";
                }
                liststr += imgstr + dt.Rows[i]["Sname"] + "<br />";
                string ClassID = "";
                if (dt.Rows[i]["ClassID"].ToString() != null && dt.Rows[i]["ClassID"].ToString() != "")
                {
                    ClassID = dt.Rows[i]["ClassID"].ToString();
                }
                DataTable ldt = pd.GetselectLabelList1(ClassID);
                liststr += "<label style=\"display:;\" id=\"mid" + dt.Rows[i]["id"] + "\">";
                if (ldt != null)
                {
                    for (int k = 0; k < ldt.Rows.Count;k++)
                    {
                        string tmContent = ldt.Rows[k]["Content"].ToString();
                        tmContent = Regex.Replace(tmContent, @"<img(.+?){(.+?)}(.+?)>", "<img src=\"../../SysImages/folder/spic.png\" border=\"0\" title=\"样式中的标签\" />", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        liststr += "<img src=\"../../sysImages/folder/folderup.gif\" border=\"0\" class=\"SubItems\" /><a href=\"javascript:void(0);\" onclick=\"getReview1('s" + ldt.Rows[k]["styleID"].ToString() + "')\"><img src=\"../../SysImages/" + Foosun.Config.UIConfig.CssPath() + "/sysico/review.gif\" title=\"预览\" border=\"0\"></a> <span id=\"" + ldt.Rows[k]["styleID"] + "\" class=\"LableItem\" ondblclick=\"ReturnValue(document.form1.styleID.value);\" onClick=\"SelectLable(this);sFiles('" + ldt.Rows[k]["styleID"] + "');\" title=\"双击选择||描述：" + ldt.Rows[k]["Description"] + "\">" + ldt.Rows[k]["StyleName"] + "</span><div style=\"background-color:#FFFAE2;\" id=\"s" + ldt.Rows[k]["styleID"].ToString() + "\" style=\"display:none;\">" + tmContent + "</div><br />";
                    }
                }
                ldt.Clear();
                ldt.Dispose();
                liststr += "</label>";
            }
            dt.Clear();
            dt.Dispose();
        }
        return liststr;
    }
}
