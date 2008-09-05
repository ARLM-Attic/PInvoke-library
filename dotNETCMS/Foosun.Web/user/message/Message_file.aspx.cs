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
public partial class user_message_Message_file : Foosun.Web.UI.UserPage
{
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    Message mes = new Message();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Mids = Request.QueryString["Mid"].ToString();
            DataTable files = mes.sel_19(Mids);
            if (files != null && files.Rows.Count > 0)
            {
                string FileName = files.Rows[0]["FileName"].ToString();
                string FileUrl = files.Rows[0]["FileUrl"].ToString();
                FileUrl = FileUrl.Replace("/", "\\");
                FileUrl = FileUrl.Replace("~\\", Foosun.Common.ServerInfo.GetRootPath() + "\\");
                FileInfo finfo = new FileInfo(FileUrl);
                if (finfo.Exists)
                {
                    Response.Clear();
                    Response.Charset = "utf-8";
                    Response.Buffer = true;
                    this.EnableViewState = false;
                    Response.ContentEncoding = System.Text.Encoding.UTF8;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + Server.UrlEncode(FileName) + "\"");
                    Response.ContentType = "application/unknown";

                    Response.WriteFile(FileUrl);
                    Response.Flush();
                    Response.Close();
                    Response.End();
                }
                else
                {
                    PageError("附件可能被删除不能下载", "Message_box.aspx?Id=1");
                }
            }
            else
            {
                PageError("附件可能被删除不能下载", "Message_box.aspx?Id=1");
            }
        }
    }
}
