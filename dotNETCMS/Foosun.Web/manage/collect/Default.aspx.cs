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
using System.Text.RegularExpressions;

public partial class manage_collect_Default : Foosun.Web.UI.ManagePage
{
    public manage_collect_Default()
    {
        Authority_Code = "S008";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string AppPath = "";
         string UrlAuthority  = Request.Url.GetLeftPart(UriPartial.Authority);
        if(HttpContext.Current.Request.ApplicationPath == "/")  
          //直接安装在   Web   站点   
            AppPath = UrlAuthority;
        else  
          //安装在虚拟子目录下   
            AppPath = UrlAuthority + HttpContext.Current.Request.ApplicationPath;

        Response.Write(AppPath);
        //Response.Write(this.GetType().FullName);
        //FSCollect.PageRes pr = new FSCollect.PageRes("", "");
        //pr.saveremotepic("http://image.mm113.com/54/61/mini_1165902952429.jpg", @"c:\a.jpg");
        //return;
        //string s = "http://www.sohu.com/imges/index.aps";
        ////string p = "herf\\s?=\\s?(\"[^\"]*\"|[^\"][^\\s]*)";
        //string p = "ab|de";
        //Regex r = new Regex(p);
        //Match m = r.Match(p);
        //while (m.Success)
        //{
        //    this.TextBox1.Text += m.Value + "\r\n"; 
        //    m=m.NextMatch();
        //}



    }
}
