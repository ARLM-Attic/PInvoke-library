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

public partial class manage_label_syslabel_outLocal : Foosun.Web.UI.ManagePage
{
    public manage_label_syslabel_outLocal()
    {
        Authority_Code = "T015";
    }
    Foosun.CMS.Label rd = new Foosun.CMS.Label();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            string _classID = Request.QueryString["classID"];
            string _outtype = Request.QueryString["outtype"];
            int _tmptype = 0;
            if (_classID != null && _classID != "")
            {
                if (_outtype != "" && _outtype != null)
                {
                    if (_outtype.ToString() == "trues") { _tmptype = 2; }
                    else if (_outtype.ToString() == "falses") { _tmptype = 0; }
                }
                if (_classID.ToString() == "alllabel")
                { saveXML(_tmptype, ""); }
                else { saveXML(_tmptype, _classID.ToString()); }
                string FileName = Server.MapPath("~/xml/label/" + SiteID + "/label.xml");
                FileInfo finfo = new FileInfo(FileName);
                if (finfo.Exists)
                {
                    Response.Clear();
                    Response.Charset = "utf-8";
                    Response.Buffer = true;
                    this.EnableViewState = false;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + Server.UrlEncode(FileName) + "\"");
                    Response.ContentType = "application/unknown";

                    Response.WriteFile(FileName);
                    Response.Flush();
                    Response.Close();
                    Response.End();
                }
            }
        }
    }


    protected void saveXML(int Num, string labelID)
    {
        StreamWriter sw = null;
        string xmlFileName = HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID + "/label.xml");
        if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID)))
        {
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/label/" + SiteID));
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
