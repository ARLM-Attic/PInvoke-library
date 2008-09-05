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

public partial class manage_ver_about : Foosun.Web.UI.ManagePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
        linkus1.InnerHtml = linkus();
    }
    string linkus()
    { 
       string  linkstr = "公司全称： 四川风讯科技发展有限公司 <br />" ;
             linkstr = linkstr + " 地　　址： 四川.成都.武侯区惠民街109号双楠嘉苑大厦A座2层<br /> ";
             linkstr = linkstr + " 邮政编码： 610041 <BR>  ";
             linkstr = linkstr + " 售前咨询：028-66026180 / 85098980转：601,606,607,609 <br /> " ;
             linkstr = linkstr + " 客户服务：028-66026180/85098980转608 <br />";
             linkstr = linkstr + " 传　　真：028-66026180/85098980转603 <br />";
             linkstr = linkstr + " 站　　点：www.foosun.net<br />";
             linkstr = linkstr + " 论　　坛：bbs.foosun.net<br />";
             linkstr = linkstr + " 电子邮件：<a class=\"list_link\" href=\"mailto:service@foosun.cn\">service@foosun.cn</a>, <a class=\"list_link\" href=\"mailto:office@foosun.cn\">office@foosun.cn</a> <BR> ";
             linkstr = linkstr + " 项目MSN：ikoolls@gmail.com<br /> ";
             linkstr = linkstr +  "项目ＱＱ：655071" ;
             return linkstr;
    }
}
