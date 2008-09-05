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

public partial class vplay : Foosun.Web.UI.BasePage
{
    protected string newLine = "\r\n";
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!Page.IsPostBack)
            getVideoInfo();
    }

    /// <summary>
    /// 取得视频播放地址
    /// </summary>
    protected void getVideoInfo()
    {
        string NewsID = Foosun.Common.Input.Filter(Request.QueryString["NewsID"]);
        string vtype = Request.QueryString["vtype"];
        string widthstr = "500";
        string heightstr = "500";
        if (Request.QueryString["width"] != string.Empty && Request.QueryString["width"] != null && Request.QueryString["height"] != string.Empty && Request.QueryString["height"] != null)
        {
            widthstr = Request.QueryString["width"].ToString();
            heightstr = Request.QueryString["height"].ToString();
        }
        int i_type=0;
        string str_VUrl = "";
        if (NewsID != "" && NewsID != null)
        {
            try
            {
                i_type = int.Parse(vtype);
            }
            catch { i_type = 0; }

            Foosun.CMS.ContentManage news = new Foosun.CMS.ContentManage();
            IDataReader dr = news.getNewsID(NewsID);
            if (dr.Read())
            {
                str_VUrl = dr["vURL"].ToString();
                dr.Close();
                getPlay(i_type, str_VUrl, heightstr, widthstr);
            }
            else
            {
                dr.Close();
                Err();
            }
        }
        else
        {
            Err();
        }
    }

    protected void Err()
    {
        Response.Write("<script language=\"javascript\">alert('参数传递错误!');history.back();</script>");
        Response.End();
    }


    protected void getPlay(int vtype, string vURL, string heightstr, string widthstr)
    {
        string str = "";
        vURL = vURL.Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
        if (vtype == 0)
        {
            str = "<object id=\"nstv\" classid=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\" width=\""+widthstr+"\" height=\""+heightstr+"\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#\" Version=\"5,1,52,701standby=Loading Microsoft? Windows Media? Player components...\" type=\"application/x-oleobject\">" + newLine;
            str += "<param name=\"URL\" value=\"" + vURL + "\">" + newLine;
            str += "<PARAM NAME=\"UIMode\" value=\"full\">" + newLine;
            str += "<PARAM NAME=\"AutoStart\" value=\"true\">" + newLine;
            str += "<PARAM NAME=\"Enabled\" value=\"true\">" + newLine;
            str += "<PARAM NAME=\"enableContextMenu\" value=\"false\">" + newLine;
            str += "<param name=\"WindowlessVideo\" value=\"true\">" + newLine;
            str += "</object>" + newLine;
        }
        else if (vtype == 1)
        {
            str = "<object id=\"player\" name=\"player\" classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" width=\""+widthstr+"\" height=\""+heightstr+"\">" + newLine;
            str += "<param name=_ExtentX value=18415>" + newLine;
            str += "<param name=_ExtentY value=9102>" + newLine;
            str += "<param name=AUTOSTART value=-1>" + newLine;
            str += "<param name=SHUFFLE value=0>" + newLine;
            str += "<param name=PREFETCH value=0>" + newLine;
            str += "<param name=NOLABELS value=-1>" + newLine;
            str += "<param name=SRC value=" + vURL + ">" + newLine;
            str += "<param name=CONTROLS value=Imagewindow>" + newLine;
            str += "<param name=CONSOLE value=clip1>" + newLine;
            str += "<param name=LOOP value=0>" + newLine;
            str += "<param name=NUMLOOP value=0>" + newLine;
            str += "<param name=CENTER value=0>" + newLine;
            str += "<param name=MAINTAINASPECT value=0>" + newLine;
            str += "<param name=BACKGROUNDCOLOR value=#000000>" + newLine;
            str += "</object><br>" + newLine;
            str += "<object ID=RP2 CLASSID=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA WIDTH=400 HEIGHT=50>" + newLine;
            str += "<param name=_ExtentX value=18415>" + newLine;
            str += "<param name=_ExtentY value=1005>" + newLine;
            str += "<param name=AUTOSTART value=-1>" + newLine;
            str += "<param name=SHUFFLE value=0>" + newLine;
            str += "<param name=PREFETCH value=0>" + newLine;
            str += "<param name=NOLABELS value=-1>" + newLine;
            str += "<param name=SRC value=" + vURL + ">" + newLine;
            str += "<PARAM NAME=CONTROLS VALUE=ControlPanel,StatusBar>" + newLine;
            str += "<param name=CONSOLE value=clip1>" + newLine;
            str += "<param name=LOOP value=0>" + newLine;
            str += "<param name=NUMLOOP value=0>" + newLine;
            str += "<param name=CENTER value=0>" + newLine;
            str += "<param name=MAINTAINASPECT value=0>" + newLine;
            str += "<param name=BACKGROUNDCOLOR value=#000000>" + newLine;
            str += "</object>" + newLine;
        }
        else if (vtype == 2)
        {
            str = "<embed src=\"" + Foosun.Publish.CommonData.getUrl() + "/FlvPlayer.swf?id=" + vURL + "\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" quality=\"high\" width=\""+widthstr+"\" height=\""+heightstr+"\" autostart=\"true\"></embed>" + newLine;
        }
        else if (vtype == 3)
        {
            str = "<embed src=\"" + vURL + "?bgcolor=000000\" quality=\"high\" pluginspage=\"http://www.adobe.com/support/documentation/zh-CN/flashplayer/help/settings_manager04a.html\" type=\"application/x-shockwave-flash\" width=\""+widthstr+"\" height=\""+heightstr+"\" id=\"cfplay\"></embed>";
        }

        Response.Write(str);
        Response.End();
    }
}
