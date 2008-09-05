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
using System.IO;
using System.Xml;

public partial class manage_Sys_skinChange : Foosun.Web.UI.ManagePage
{
    public manage_Sys_skinChange()
    {
        Authority_Code = "Q036";
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            string _style = Foosun.Config.UIConfig.CssPath();
            skinlist.InnerHtml = getskinlist(_style);
        }
    }

    protected string getskinlist(string _style)
    {
        string _Str = "<select name=\"styleDir\" onchange=\"javascript:lsrc(this.value);\">\r";
        try
        {
            string _dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (_dirdumm.Trim() != "")
            { _dirdumm = "/" + _dirdumm; }
            string xmlPath = Server.MapPath(_dirdumm + "/xml/skin/skin.xml");
            if (!File.Exists(xmlPath)) { PageError("找不到配置文件(/xml/skin/skin.xml).<li>请与系统管理员联系。</li>", ""); }
            FileInfo finfo = new FileInfo(xmlPath);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("skinname");
            XmlNodeList elemList1 = root.GetElementsByTagName("skindir");
            string selectTF = "";
            for (int i = 0; i < elemList.Count; i++)
            {
                if (_style.Trim() == elemList1[i].InnerXml.ToString()) { selectTF = " selected"; }
                else { selectTF = ""; }
                _Str += "<option value=\"" + elemList1[i].InnerXml + "\"" + selectTF + ">" + (i + 1) + "." + elemList[i].InnerXml + "</option>\r";
            }
            _Str += "</select>\r";
        }
        catch
        {
            _Str = "配置文件有问题。/xml/skin/skin.xml";
            buttons.Enabled = false;
        }
        return _Str;
    }

    /// <summary>
    /// buttonsave使用说明
    /// 把皮肤目录写如web.config文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void buttonsave(object sender, EventArgs e)
    {
        string StrChar = Request.Form["styleDir"];
        //if (Foosun.Common.Public.constReadOnly(0))
        //    Foosun.Common.Public.constReadOnly(2);
        if (Foosun.Common.Public.constReadOnly(0, "xml/sys/foosun.config"))
            Foosun.Common.Public.constReadOnly(2, "xml/sys/foosun.config");
        //Foosun.Common.Public.SaveXmlElementValue("manner", StrChar);
        Foosun.Common.Public.SaveXmlConfig("manner", StrChar, "xml/sys/foosun.config");
        //Foosun.Common.Public.constReadOnly(1);
        Foosun.Common.Public.constReadOnly(1, "xml/sys/foosun.config");
        Response.Write("<script language=\"JavaScript\" type=\"text/javascript\">top.location.href=\"../index.aspx\"</script>");
    }
}
