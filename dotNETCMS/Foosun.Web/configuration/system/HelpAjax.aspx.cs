///************************************************************************************************************
///**********返回帮助信息请求,Code By DengXi*******************************************************************
///************************************************************************************************************
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

public partial class HelpAjax : Foosun.Web.UI.DialogPage
{
    public HelpAjax()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin;
    }
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";             //设置页面无缓存
        string Type = Request.QueryString["Type"];      //取得AJAX参数值Type
        string HelpID = Request.QueryString["HelpID"];  //取得AJAX参数值HelpID
        if (Type == "ShowHelp")                         //判断参数是否为指定值,如果是则调用显示帮助过程
        {
            Response.Write(HelpAjax1(HelpID));          //调用过程
            Response.End();
        }
    }
//----------------------------------------------------------------------------------------------------
//-------------------------调用帮助信息---------------------------------------------------------------

    string HelpAjax1(string HelpID)                     //调用帮助信息,HelpID为帮助编号
    {
        string Str = "";
        try
        {
            DataTable Ds = pd.GetHelpId(HelpID);    //执行SQL语句,返回数据表
            int Cnt = Ds.Rows.Count;                    //取得数据表记录数
            if (Cnt > 0)                                //判断是否查询到记录
            {
                string Title = Ds.Rows[0]["TitleCN"].ToString();                //取得帮助标题
                string Content = Ds.Rows[0]["ContentCN"].ToString();            //取得帮助内容
                Str = "<div><font color=000><strong>" + Title + "</strong></font><span style=\"Font-size:10px;font-family:Arial, Helvetica, sans-serif;color:red\" onclick=\"javascript:getHelpCode('" + HelpID + "');\">(" + HelpID + ")</span></div>" + Content;     //标题样式控制
            }
            else
            {
                Str = "编号<span style=\"Font-size:10px;font-family:Arial, Helvetica, sans-serif;color:red\" onclick=\"javascript:getHelpCode('" + HelpID + "');\">" + HelpID + "</span>的帮助文档没找到！";                          //没有查到记录
            }
            return Str;                                 //返回结果
        }
        catch(Exception EX) 
        {
            return EX.ToString();                          //出现异常返回错误信息
        }
//        return Str;
    }
//----------------------------------------------------------------------------------------------------
    
    
}
