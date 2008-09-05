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

public partial class manage_news_class_site_ajax : Foosun.Web.UI.ManagePage
{
    ContentManage rd = new ContentManage();
    Site sd = new Site();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";
            Response.Expires = 0;
            newsList.InnerHtml = newsstr();
        }
    }

    /// <summary>
    /// 得到站点列表
    /// </summary>
    /// <returns></returns>
    protected string newsstr()
    {
        string liststr = "";
        string ParentId = Request.QueryString["ParentId"];
        if (ParentId == "" || ParentId == null) { ParentId = "0"; }
        int flag = 0;
        IDataReader sRd = sd.siteList();

        while (sRd.Read())
        {
            if (ParentId == "0")
            {
                liststr += "<div><img src=\"../../sysImages/normal/site.gif\" /><span style=\"cursor:pointer;\" title=\"此频道有" + sd.getsiteClassCount(sRd["ChannelID"].ToString()) + "个栏目\" onclick=\"hsite('gsite" + sRd["ChannelID"].ToString() + "');\">&nbsp;" + sRd["CName"].ToString() + "<span style=\"font-size:9px;\">(" + sd.getsiteClassCount(sRd["ChannelID"].ToString()) + ")</span></span></div>\r";
            }
            if (sRd["ChannelID"].ToString() != "0")
            {
                liststr += "<div id=\"gsite" + sRd["ChannelID"].ToString() + "\" style=\"display:none;\">";
            }
            else
            {
                liststr += "<div id=\"gsite" + sRd["ChannelID"].ToString() + "\">";
            }
            int i = 0;
            IDataReader dt = rd.GetClassSitenewsstr(ParentId.ToString(), sRd["ChannelID"].ToString());
            while (dt.Read())
            {
                this.ClassID = dt["classid"].ToString();
                if (this.CheckAuthority())
                {
                    string isdomainstr = dt["domain"].ToString();
                    string isdomain = "";
                    int lens = isdomainstr.Length;
                    if (lens > 5)
                    {
                        isdomain = "<img src=\"../../sysImages/normal/domain.gif\" border=\"0\" alt=\"有捆绑域名的栏目\">";
                    }
                    else
                    {
                        isdomain = "<img src=\"../../sysImages/normal/undomain.gif\" border=\"0\" alt=\"无捆绑域名的栏目\">";
                    }
                    if (int.Parse(dt["HasSub"].ToString()) > 0)
                    {

                        liststr += "<div style=\"padding-left:8px;\"><img src=\"../../sysImages/normal/b.gif\" style=\"cursor:pointer;\" alt=\"点击展开子栏目\"  border=\"0\" class=\"LableItem\" onClick=\"javascript:SwitchImg(this,'" + dt["ClassID"] + "');\" />" + isdomain + "&nbsp;<a href=\"news_list.aspx?ClassID=" + dt["ClassID"].ToString() + "\" target=\"sys_main\" class=\"menulist\">" + dt["ClassCName"] + "</a><span style=\"font-size:9px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(dt["ClassID"].ToString()) + ")</span><a href=\"news_add.aspx?ClassID=" + dt["ClassID"].ToString() + "&EditAction=add\" target=\"sys_main\" class=\"menulist\"><img src=\"../../sysImages/folder/addnew.gif\" border=\"0\" alt=\"添加新闻\"></a><div id=\"Parent" + dt["ClassID"] + "\" class=\"SubItem\" HasSub=\"True\" style=\"height:100%;display:none;\"></div></div>\r";
                    }
                    else
                    {
                        liststr += "<div style=\"padding-left:8px;\"><img src=\"../../sysImages/normal/s.gif\" style=\"cursor:pointer;\" alt=\"没有子栏目\"  border=\"0\" class=\"LableItem\" />" + isdomain + "&nbsp;<a href=\"news_list.aspx?ClassID=" + dt["ClassID"].ToString() + "\" target=\"sys_main\" class=\"menulist\">" + dt["ClassCName"] + "</a><span style=\"font-size:9px;color:red\" title=\"新闻数\">(" + rd.getClassNewsCount(dt["ClassID"].ToString()) + ")</span><a href=\"news_add.aspx?ClassID=" + dt["ClassID"].ToString() + "&EditAction=add\" target=\"sys_main\" class=\"menulist\"><img src=\"../../sysImages/folder/addnew.gif\" border=\"0\" alt=\"添加新闻\"></a></div>";
                    }
                }
                i++;
            }
            dt.Close();
            flag++;
            liststr += "</div>";
        }
        sRd.Close();
        liststr = "Succee|||" + ParentId + "|||" + liststr;
        if (flag == 0)
        {
            liststr += "Fail|||" + ParentId + "|||";
        }
        return liststr;
    }
}
