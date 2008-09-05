//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
//=====================================================================
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

public partial class mycom_up : Foosun.Web.UI.UserPage
{
    Info inf = new Info();
    sys sd = new sys();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        string Commid = Foosun.Common.Input.Filter(Request.QueryString["Commid"]);
        DataTable dt = inf.sel_19(Commid);
        this.TitleBox.Text=dt.Rows[0]["Title"].ToString();
        this.ContentBox.Text=dt.Rows[0]["Content"].ToString();
    }   
    protected void shortCutsubmit(object sender, EventArgs e)
    {
        if (Page.IsValid)//判断是否通过验证
        {
            string Title=Foosun.Common.Input.Filter(Request.Form["TitleBox"]);
            string Contents=Foosun.Common.Input.Filter(Request.Form["ContentBox"]);
            if (Contents.Length > 200)
            {
                PageError("评论内容最多200个字符。", "mycom.aspx");
            }
            DateTime CreatTime = DateTime.Now;
            string Commid = Foosun.Common.Input.Filter(Request.QueryString["Commid"]);
            int islocks = 0;
            DataTable isl = sd.UserPram();
            if (isl != null && isl.Rows.Count > 0)
            {
                if (Foosun.Common.Input.IsInteger(isl.Rows[0]["CommCheck"].ToString()))
                {
                    islocks = int.Parse(isl.Rows[0]["CommCheck"].ToString());
                }
                isl.Clear(); isl.Dispose();
            }
            if (islocks != 1)
            {
                islocks = 2;
            }

            if (inf.Update6(Title, Contents, CreatTime, Commid, islocks) == 0)
            {
                PageError("修改错误", "mycom.aspx");
            }
            else
            {
                PageRight("修改成功,可能需要重新审核！", "mycom.aspx");
            }
        }
    }
}
