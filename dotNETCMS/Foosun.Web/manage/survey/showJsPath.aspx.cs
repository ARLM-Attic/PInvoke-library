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
using Foosun.CMS.Common;

public partial class manage_survey_showJsPath : Foosun.Web.UI.ManagePage
{
    #region 取得服务器变量集合
    System.Collections.Specialized.NameValueCollection ServerVariables = System.Web.HttpContext.Current.Request.ServerVariables;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache"; //清除缓存
        if (!IsPostBack)  //判断页面是否重载
        {
              //判断用户是否登录
            copyright.InnerHtml = CopyRight;//获取版权信息
        }
        startContent(); //初始内容
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// code by chenzhaohui

    void startContent()
    {
        rootPublic rd = new rootPublic();
        int jsid = int.Parse(Request.QueryString["jsid"]);
        if (jsid <=0)//如果ID小于0，则参数错误
        {
            PageError("参数传递错误", "setTitle.aspx");
        }
        else
        {
            string Path = null;
            string span = null;
            string scripts = null;
            string scripte = null;
            string strtheurl = null;
            string Str_dirMana = Foosun.Config.UIConfig.dirDumm;
            if (Str_dirMana != "" && Str_dirMana != null && Str_dirMana != string.Empty)//判断虚拟路径是否为空,如果不是则加上//
            {
                Str_dirMana = "/" + Str_dirMana;
            }
            else
            {
                Str_dirMana = "";
            }

            string Cookie_Domain = rd.sitedomain();
            if (Cookie_Domain == "")
            {
                Cookie_Domain = "localhost";
            }
          
            ///jsid为传递的相应主题ID
            ///InfoID为HTML页面ID
            ///PicW为查看页面图片的宽度
            scripts = "<script src=\"";
            Path = "/survey/VoteJs.aspx?TID=" + jsid + "&PicW=60&ajaxid=Vote_HTML_ID_" + jsid + "_" + Foosun.Common.Rand.Number(5) + "\"";
            scripte = "  language=\"JavaScript\"></script>";
            span = "<div style=\"display:inline\" id=\"Vote_HTML_ID_" + jsid + "_"+Foosun.Common.Rand.Number(5)+"\">正在加载...</div>";
            //取得js调用地址
            strtheurl = "" + scripts + "http://" + Cookie_Domain + Str_dirMana  + Path + scripte + span;
            
            this.CodePath.Value = strtheurl;//将值传递给textarea文本区域内
        }
    }
}
