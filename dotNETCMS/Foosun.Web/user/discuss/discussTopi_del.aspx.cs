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

public partial class user_discussTopi_del : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            string ID = Foosun.Common.Input.Filter(Request.QueryString["DtID"].ToString());
            int vote=dis.sel_59(ID);
            if (vote == 0)
            {
                if (dis.Delete_13(ID) == 0 || dis.Delete_14(ID) == 0)
                {
                    PageError("主题删除成功", "");
                }
                else
                {
                    PageRight("主题删除失败", "");
                }
            }
            else
            {
                if (dis.Delete_13(ID) == 0 || dis.Delete_14(ID) == 0 || dis.Delete_15(ID)==0)
                {
                    PageError("投票删除成功", "");
                }
                else
                {
                    PageRight("投票删除失败", "");
                }
            }
        } 
    }    
 }