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

public partial class user_show_showcontent : Foosun.Web.UI.BasePage
{
    UserMisc rd = new UserMisc();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            copyright.InnerHtml = CopyRight;
            string u_name = Request.QueryString["uid"];
            string uID = pd.getUserNameUserNum(Foosun.Common.Input.Filter(u_name.ToString()));
            string ConID = Request.QueryString["ConID"];
            contentClass.InnerHtml = cclass(uID);
            if (ConID != null && ConID != "")
            {
                if (u_name != null && u_name != "")
                {
                    DataTable dt = rd.getConstrID(Foosun.Common.Input.Filter(ConID.ToString()), uID);
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            div_title.InnerHtml = dt.Rows[0]["Title"].ToString();
                            div_other.InnerHtml = "作者：" + dt.Rows[0]["Author"].ToString() + "&nbsp;&nbsp;&nbsp;日期：" + dt.Rows[0]["creatTime"].ToString() + "&nbsp;&nbsp;&nbsp;来源：" + dt.Rows[0]["Source"].ToString() + "";
                            div_Content.InnerHtml = dt.Rows[0]["Content"].ToString();
                            string _TmpTags =dt.Rows[0]["tags"].ToString();
                            string getTags = "<span class=\"tbie\">tag：</span>";
                            if (_TmpTags != null && _TmpTags != "")
                            {
                                if (_TmpTags.IndexOf('|') > 0)
                                {
                                    string[] _TmpTagsARR = _TmpTags.Split('|');
                                    for (int j = 0; j < _TmpTagsARR.Length; j++)
                                    {
                                        getTags += "<a class=\"list_link\" target=\"_blank\" href=\"../../tag.aspx?type=constr&tag=" + _TmpTagsARR[j] + "\">" + _TmpTagsARR[j] + "</a>&nbsp;&nbsp;";
                                    }
                                }
                                else
                                {
                                    getTags += "<a class=\"list_link\" target=\"_blank\" href=\"../../tag.aspx?type=constr&tag=" + _TmpTags + "\">" + _TmpTags + "</a>&nbsp;&nbsp;";
                                }

                            }
                            else
                            {
                                getTags += "无";
                            }
                            div_tags.InnerHtml = getTags;
                        }
                        else
                        {
                            PageError("找不到记录", "info.aspx?s=content&uid=" + u_name.ToString() + "");
                        }
                    }
                    else
                    {
                        PageError("错误的参数", "");
                    }
                }
                else
                {
                    PageError("错误的参数", "");
                }
            }
            else
            {
                PageError("错误的参数", "info.aspx?s=content&uid=" + u_name.ToString() + "");
            }
        }
    }

    protected string cclass(string UserNum)
    {
        string _tmpchar = "&nbsp;│&nbsp;";
        string _STR = "文章分类：<a class=\"list_link\" href=\"info.aspx?s=content&uid=" + Request.QueryString["uid"] + "\">全部</a>&nbsp;│&nbsp;";
        DataTable dt = rd.getConstrClass(UserNum);
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _STR += "<a class=\"list_link\" href=\"info.aspx?s=content&uid=" + Request.QueryString["uid"] + "&ClassID=" + dt.Rows[i]["Ccid"].ToString() + "\" title=\"" + dt.Rows[i]["Content"].ToString() + "\">" + dt.Rows[i]["cName"].ToString() + "</a>" + _tmpchar;
            }
            dt.Clear(); dt.Dispose();
        }
        return _STR;
    }
}
