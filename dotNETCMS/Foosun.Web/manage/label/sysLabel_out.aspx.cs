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
using System.Xml;
using System.IO;

public partial class manage_label_sysLabel_out : Foosun.Web.UI.ManagePage
{
    public manage_label_sysLabel_out()
    {
        Authority_Code = "T015";
    }
    Foosun.CMS.Label rd = new Foosun.CMS.Label();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
            string _OutType = Request.QueryString["type"];
            if (_OutType != null && _OutType != "")
            {
                if (_OutType.ToString() == "out")
                {
                    in_table.Visible = false;
                    outlabel_type.InnerHtml = "导出标签";
                    if (Request.QueryString["LabelName"] != null && Request.QueryString["LabelName"] != "")
                    {
                        classShow.InnerHtml = "您要导出的标签为：" + Request.QueryString["LabelName"].ToString() + "";
                        this.classID.Value = Request.QueryString["LabelID"].ToString();
                        this.outSystem.Visible = false;
                    }
                    else
                    {
                        classShow.InnerHtml = "您要导出的标签为：导出所有标签";
                        this.classID.Value = "alllabel";
                    }
                }
                else if (_OutType.ToString() == "in")
                {
                    out_table.Visible = false;
                    outlabel_type.InnerHtml = "导入标签";
                }
                else
                    PageError("错误的参数", "");
            }
            else
            {
                PageError("错误的参数", "");
            }
        }
    }


    protected void label_out_Click(object sender, EventArgs e)
    {
            ////开始生成xml
            string RandNumber = Foosun.Common.Rand.Number(5);
            string _tmpDir = DateTime.Now.Year + "-" + DateTime.Now.Month;
            string fileName = DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + RandNumber;
            if (this.classID.Value == "alllabel")
            {
                //生成非系统标签xml
                saveXML(_tmpDir, fileName, 0, "");
                //生成系统标签xml
                saveXML(_tmpDir, fileName, 1, "");
                PageRight("成功导出2个标签!<li>路径：/xml/label/"+SiteID+"/" + _tmpDir + "/Sys-*-" + fileName + ".xml</li>", "sysLabel_list.aspx");
            }
            else
            {
                string LabelID = this.classID.Value;
                saveXML(_tmpDir, fileName, 2, LabelID);
                PageRight("批量导出标签成功!<li>路径：/xml/label/" + SiteID + "/" + _tmpDir + "/LabelID-" + LabelID + "-" + fileName + ".xml</li>", "sysLabel_list.aspx");
            }
    }


    protected void label_clear_Click(object sender, EventArgs e)
    {
        if (Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + "")))
        {
            Directory.Delete(HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + ""), true);
        }
        PageRight("操作成功，以前的标签已经被清除", "sysLabel_list.aspx");

    }

    protected void label_in_Click(object sender, EventArgs e)
    {
        string _xmlPath = this.xmlPath_put.Value;
        string ATserverTF = "0";
        if (_xmlPath == "")
        {
            PageError("请选择需要导入的xml", "");
        }
        if (this.ATserverTF.Checked){ATserverTF = "1";}
        Response.Redirect("sysLabel_inlabel.aspx?ATserverTF=" + ATserverTF + "&xmlPath=" + _xmlPath);
    }

    protected void saveXML(string strDir,string FileName, int Num,string labelID)
    {
        StreamWriter sw = null;
        string xmlFileName = "";
        if (Num == 2)
        {
            xmlFileName = HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + "/" + strDir + "/LabelID-" + labelID + "-" + FileName + ".xml");
        }
        else
        {
            xmlFileName = HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + "/" + strDir + "/Sys-" + Num + "-" + FileName + ".xml");
        }
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + "/" + strDir)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + "/" + strDir));
        }
        sw = File.CreateText(xmlFileName);
        sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r");
        sw.WriteLine("<rss version=\"2.0\">\r");
        sw.WriteLine("<foosunlabel>\r");
        DataTable dt = null;
        if (labelID == "") { dt = rd.outLabelALL(Num); }
        else { dt = rd.outLabelmutile(labelID); }
        if (dt != null)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sw.WriteLine("  <labelitem>\r");
                sw.WriteLine("      <labelname>" + dt.Rows[i]["Label_Name"].ToString() + "</labelname>\r");
                sw.WriteLine("      <labelid>" + dt.Rows[i]["LabelID"].ToString() + "</labelid>\r");
                sw.WriteLine("      <labelclassid>" + dt.Rows[i]["ClassID"].ToString() + "</labelclassid>\r");
                sw.WriteLine("      <labelcontent><![CDATA[" + dt.Rows[i]["Label_Content"].ToString() + "]]></labelcontent>\r");
                sw.WriteLine("      <labeldescription><![CDATA[" + dt.Rows[i]["Description"].ToString() + "]]></labeldescription>\r");
                sw.WriteLine("      <labelcreattime>" + dt.Rows[i]["CreatTime"].ToString() + "</labelcreattime>\r");
                sw.WriteLine("      <labelissys>" + dt.Rows[i]["isSys"].ToString() + "</labelissys>\r");
                sw.WriteLine("  </labelitem>\r");
            }
            dt.Clear(); dt.Dispose();
        }
        sw.WriteLine("</foosunlabel>\r");
        sw.WriteLine("</rss>\r");
        sw.Flush();
        sw.Close(); sw.Dispose();
    }
}
