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

public partial class configuration_system_iframe : Foosun.Web.UI.DialogPage
{
    public configuration_system_iframe()
    {
        BrowserAuthor = EnumDialogAuthority.ForAdmin | EnumDialogAuthority.ForPerson; 
    }
    public string Str_dirMana = Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";
        }
        string sh = Request.QueryString["heights"];
        select_iframe.InnerHtml = select_iframelist(sh);
    }
    string select_iframelist(string sh)
    {
        if (Str_dirMana.Trim() != "")
        {
            Str_dirMana = "/" + Str_dirMana;
        }
        string liststr = "";
        string srcstr = "";
        string rq = Request.QueryString["FileType"];
        string arrrq = rq.Split('|')[0];
        switch (arrrq)
        {
            case "file":
                srcstr = Str_dirMana + "/configuration/system/selectFiles.aspx?FileType=file";
                break;
            case "pic":
                srcstr = Str_dirMana + "/configuration/system/selectFiles.aspx?FileType=pic";
                break;
            case "picEdit":
                srcstr = Str_dirMana + "/configuration/system/selectFiles.aspx?FileType=pic&Edit=1";
                break;
            case "templet":
                srcstr = Str_dirMana + "/configuration/system/selectFiles.aspx?FileType=templet";
                break;
            case "date":
                srcstr = Str_dirMana + "/configuration/system/DateTime.aspx";
                break;
            case "path":
                srcstr = Str_dirMana + "/configuration/system/selectPath.aspx?Path=" + rq.Replace("path|", "");
                break;
            case "newsclass":
                srcstr = Str_dirMana + "/configuration/system/selectNewsClass.aspx";
                break;
            case "special":
                srcstr = Str_dirMana + "/configuration/system/selectNewsspecial.aspx";
                break;
            case "newsspecial":
                srcstr = Str_dirMana + "/configuration/system/selectspecial.aspx";
                break;
            case "user_file":
                srcstr = Str_dirMana + "/configuration/system/selectuserpic.aspx?FileType=user_file";
                break;
            case "user_pic":
                srcstr = Str_dirMana + "/configuration/system/selectuserpic.aspx?FileType=user_pic";
                break;
            case "user_Edit":
                srcstr = Str_dirMana + "/configuration/system/selectuserpic.aspx?FileType=user_pic&Edit=1";
                break;
            case "user_Hpic":
                srcstr = Str_dirMana + "/configuration/system/selectuserpic.aspx?FileType=user_Hpic";
                break;
            case "rulePram":
                srcstr = Str_dirMana + "/configuration/system/selectrulePram.aspx?FileType=rulePram";
                break;
            case "rulesmallPramo":
                srcstr = Str_dirMana + "/configuration/system/selectrulePram.aspx?FileType=rulesmallPramo";
                break;
            case "rulesmallPram":
                srcstr = Str_dirMana + "/configuration/system/selectrulePram.aspx?FileType=rulesmallPram";
                break;
            case "discuss_file":
                srcstr = Str_dirMana + "/configuration/system/selectuserdiscuss.aspx?FileType=discuss_file";
                break;
            case "discuss_pic":
                srcstr = Str_dirMana + "/configuration/system/selectuserdiscuss.aspx?FileType=discuss_pic";
                break;
            case "newsLink":
                srcstr = Str_dirMana + "/configuration/system/selectnewsLink.aspx";
                break;
            case "style":
                srcstr = Str_dirMana + "/configuration/system/selectLabelStyle.aspx";
                break;
            case "Channel":
                srcstr = Str_dirMana + "/configuration/system/selectChannel.aspx";
                break;
            case "Souce":
                srcstr = Str_dirMana + "/configuration/system/Genlist.aspx?type=Souce";
                break;
            case "Author":
                srcstr = Str_dirMana + "/configuration/system/Genlist.aspx?type=Author";
                break;
            case "Tag":
                srcstr = Str_dirMana + "/configuration/system/Genlist.aspx?type=Tag";
                break;
            case "xml":
                srcstr = Str_dirMana + "/configuration/system/xml.aspx";
                break;
            default:
                break;
        }
        liststr += "<iframe src=\"" + srcstr + "\" frameborder=\"0\" id=\"select_main\" scrolling=\"yes\" name=\"select_main\" width=\"100%\" height=\"" + sh + "px\" />";
        return liststr;
    }
}
