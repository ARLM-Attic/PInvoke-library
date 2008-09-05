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

public partial class manage_label_sysLabel_inlabel : Foosun.Web.UI.ManagePage
{
    public manage_label_sysLabel_inlabel()
    {
        Authority_Code = "T015";
    }
    Foosun.CMS.Label rd = new Foosun.CMS.Label();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            showInfo();
            string _xmlPath = Request.QueryString["xmlPath"];
            string _ATserverTF = Request.QueryString["ATserverTF"];
            if ((_xmlPath != null && _xmlPath != "") || (_ATserverTF != null && _ATserverTF != ""))
            {
                this.xmlPath.Value = _xmlPath.ToString();
                this.ATserverTF.Value = _ATserverTF.ToString();
            }
            else
            {
                PageError("参数传递失败","");
            }
        }
    }

    /// <summary>
    /// 在前台显示分类列表
    /// </summary>
    /// <returns>在前台显示分类列表</returns>
    /// 编写时间2007-04-24   Code By DengXi

    protected void showInfo()
    {
        Foosun.CMS.Label lbc = new Foosun.CMS.Label();
        DataTable dt = lbc.GetLabelinClassList();
        if (dt != null)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListItem itm = new ListItem();
                itm.Value = dt.Rows[i]["ClassID"].ToString();
                itm.Text = dt.Rows[i]["ClassName"].ToString();
                LabelClass.Items.Add(itm);
                itm = null;
            }
            dt.Clear(); dt.Dispose();
        }
    }

    /// <summary>
    /// 导入本地标签
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// code by simplet.Xie
    protected void In_click(object sender, EventArgs e)
    {
        string xmlPath = this.xmlPath.Value;
        FileInfo finfo = new FileInfo(xmlPath);
        if ((finfo.Extension.ToString()).ToUpper() != ".xml".ToUpper()){PageError("不是正确的xml格式。", "");}
        if (this.ATserverTF.Value == "1"){xmlPath = Server.MapPath(this.xmlPath.Value);}
        string Classid = this.LabelClass.SelectedValue;
        System.Xml.XmlDocument xdoc = new XmlDocument();
        xdoc.Load(xmlPath);
        XmlElement root = xdoc.DocumentElement;
        XmlNodeList elemList = root.GetElementsByTagName("labelname");
        XmlNodeList elemList1 = root.GetElementsByTagName("labelcontent");
        XmlNodeList elemList2 = root.GetElementsByTagName("labeldescription");
        XmlNodeList elemList3 = root.GetElementsByTagName("labelid");
        XmlNodeList elemList4 = root.GetElementsByTagName("labelissys");
        string Label_Name = "";
        string Label_Content = "";
        string Description = "";
        string LabelID = "";
        string isSystem = "";
        string _Classid = "";
        for (int i = 0; i < elemList.Count; i++)
        {
            Label_Name = elemList[i].InnerXml;
            Label_Content = elemList1[i].InnerXml;
            if (Label_Content.Trim() != string.Empty && Label_Content != null)
            {
                Label_Content = Label_Content.Replace("<![CDATA[", "").Replace("]]>", "");
            }
            Description = elemList2[i].InnerXml;
            LabelID = elemList3[i].InnerXml;
            isSystem = elemList4[i].InnerXml;
            _Classid = Classid;
            rd.inserLabelLocal(LabelID, _Classid, Label_Name, Label_Content, Description, isSystem);
        }
        PageRight("导入成功", "SysLabel_List.aspx?ClassID=" + Classid + "");
    }


}
