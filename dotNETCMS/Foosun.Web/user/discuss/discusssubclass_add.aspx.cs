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

public partial class user_discusssubclass_add : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.CacheControl = "no-cache";
            
            DataTable dts1 = dis.sel_33();
            this.ClassIDList1.DataSource = dts1;
            this.ClassIDList1.DataTextField = "Cname";
            this.ClassIDList1.DataValueField = "DcID";
            this.ClassIDList1.DataBind();
        }
    }
    protected void but1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string Cname = Foosun.Common.Input.Filter(Request.Form["Cname"].ToString());
            string Content = Foosun.Common.Input.ToHtml(Request.Form["Content"].ToString());

            string DcID = Foosun.Common.Rand.Number(12);
            string indexnumber = this.ClassIDList1.SelectedValue;

            //创建讨论组
            
            DataTable dt = dis.sel_4();
            int cutb = dt.Rows.Count;
            string DcIDs = "";
            if (cutb > 0)
            {
                DcIDs = dt.Rows[0]["DcID"].ToString();
            }

            if (DcIDs != DcID)
            {
                if (dis.Add_5(DcID, Cname, Content, indexnumber) == 0)
                    {
                        PageError("添加错误", "");
                    }
                    else
                    {
                        PageRight("添加成功", "");
                    }
            }
            else
            {
                PageError("对不起建立失败有可能是编号重复", "");
            }

        }
    }

}

