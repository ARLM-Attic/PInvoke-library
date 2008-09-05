using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Foosun.Model;
using Foosun.Config;
using Foosun.CMS.Common;

public partial class manage_Sys_checkFiles : Foosun.Web.UI.ManagePage
{
    private bool showdiff = false;
    public string ReloadURL = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BtnCompare.Attributes.Add("onclick", "return CheckFile();");
            this.BtnOnline.Attributes.Add("onclick", "RemoveFile();");
            Response.CacheControl = "no-cache";                        //设置页面无缓存
            this.PnlResult.Visible = false;
            this.PnlStart.Visible = true;
            copyright.InnerHtml = CopyRight;            //获取版权信息
            ReloadURL = "http://passport.foosun.net/libary/dotnetcms/loadcheck/down.aspx?type=download";
        }
    }
    /// <summary>
    /// 开始对比文件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void BtnCompare_Click(object sender, EventArgs e)
    {
        FileUpload fu = this.localfile;
        if (fu.HasFile)
        {
            using (Stream ms = fu.FileContent)
            {
                long n = ms.Length;
                byte[] buffer = new byte[n];
                int count = ms.Read(buffer, 0, 20);
                while (count < n)
                {
                    buffer[count++] = Convert.ToByte(ms.ReadByte());
                }
                this.HidStandard.Value = System.Text.Encoding.Default.GetString(buffer);
            }
        }
        else
        {
            PageError("请选择作为对比标准的本地文件!", "checkFiles.aspx");
        }
        Display();
    }
    protected void Display()
    {
        string standardstr = this.HidStandard.Value;

        FileCompare fc = new FileCompare(Server.MapPath("~"), standardstr);
        fc.GetFileList();
        List<FileComprInfo> list = fc.FileList;
        list.Sort(new FileNameComparer());
        BindList(list);
        this.PnlResult.Visible = true;
        this.PnlStart.Visible = false;
    }
    protected void BindList(List<FileComprInfo> files)
    {
        int ntot = 0;
        int nsame = 0;
        foreach (FileComprInfo info in files)
        {
            int flag = 0;
            if (info.StModifyTime == DateTime.MinValue)
                flag = 1;
            else if (info.FaModifyTime == DateTime.MinValue)
                flag = 2;
            else if (info.StFileSize != info.FaFileSize)
                flag = 3;
            else if (!(info.FaModifyTime.Year == info.StModifyTime.Year &&
           info.FaModifyTime.Month == info.StModifyTime.Month &&
           info.FaModifyTime.Day == info.StModifyTime.Day &&
           info.FaModifyTime.Hour == info.StModifyTime.Hour &&
           info.FaModifyTime.Minute == info.StModifyTime.Minute &&
           info.FaModifyTime.Second == info.StModifyTime.Second
                ))
                flag = 4;
            if (showdiff && flag == 0)
            {
                nsame++;
                ntot++;
                continue;
            }

            HtmlTableRow tr = new HtmlTableRow();
            tr.Attributes.Add("class", "TR_BG_list");
            for (int i = 0; i < 6; i++)
            {

                HtmlTableCell tc = new HtmlTableCell();
                tc.Attributes.Add("class", "list_link");
                switch (flag)
                {
                    case 1:
                        tc.Attributes.Add("style", "color:blue");
                        break;
                    case 2:
                        tc.Attributes.Add("style", "color:gray");
                        break;
                    case 3:
                        tc.Attributes.Add("style", "color:red");
                        break;
                    case 4:
                        tc.Attributes.Add("style", "color:sienna");
                        break;
                }
                switch (i)
                {
                    case 0:
                        string s = "√";
                        switch (flag)
                        {
                            case 1:
                                s = "<font style=\"border-left:inherit; text-decoration: line-through;\">×</font>";
                                break;
                            case 2:
                                s = "×";
                                break;
                            case 3:
                                s = "≠";
                                break;
                            case 4:
                                s = "≈";
                                break;
                        }
                        tc.Align = "center";
                        tc.InnerHtml = s;
                        break;
                    case 1:
                        tc.InnerHtml = "<a title=\"" + info.FileName + "\">" + CutStr(info.FileName, 50) + "</a>";
                        break;
                    case 2:
                        if (flag == 1)
                        {
                            tc.Align = "center";
                            tc.InnerHtml = "-";
                        }
                        else
                        {
                            tc.Align = "right";
                            tc.InnerHtml = info.StFileSize.ToString("###,###");
                        }
                        break;
                    case 3:
                        tc.Align = "center";
                        if (flag == 1)
                        {
                            tc.InnerHtml = "-";
                        }
                        else
                        {
                            tc.InnerHtml = info.StModifyTime.ToString("yy-MM-dd HH:mm:ss");
                        }
                        break;
                    case 4:
                        if (flag == 2)
                        {
                            tc.Align = "center";
                            tc.InnerHtml = "-";
                        }
                        else
                        {
                            tc.Align = "right";
                            tc.InnerHtml = info.FaFileSize.ToString("###,###");
                        }

                        break;
                    case 5:
                        tc.Align = "center";
                        if (flag == 2)
                        {
                            tc.InnerHtml = "-";
                        }
                        else
                        {
                            tc.InnerHtml = info.FaModifyTime.ToString("yy-MM-dd HH:mm:ss");
                        }
                        break;
                }
                tr.Cells.Add(tc);
            }
            if (flag == 0)
                nsame++;
            this.TabResult.Rows.Add(tr);
            ntot++;
        }
        this.LblStat.Text = "共对比了:" + ntot + "个文件,有:" + nsame + "个文件相同,有:" + (ntot - nsame) + "个文件有差异";
    }
    protected string CutStr(string input, int len)
    {
        if (input == null || input == string.Empty)
            return string.Empty;
        int n = input.Length;
        int m = n - len;
        if (m <= 3)
            return input;
        else
            return input.Substring(0, 10) + "..." + input.Substring(m + 10);
    }

    protected void LnkAll_Click(object sender, EventArgs e)
    {
        Display();
    }

    protected void LnkDiff_Click(object sender, EventArgs e)
    {
        showdiff = true;
        Display();
    }

    protected void BtnOnline_Click(object sender, EventArgs e)
    {
        string fsurl = OfficialConfig.CompareFileUrl;
        if (fsurl.Trim() != string.Empty)
        {
            string s = Foosun.CMS.Collect.Utility.GetPageContent(new System.Uri(fsurl, true), System.Text.Encoding.Default);
            this.HidStandard.Value = s;
            Display();
        }
    }

    protected void LnkDownload_Click(object sender, EventArgs e)
    {
        DateTime Now = DateTime.Now;
        string xmlfile = DateTime.Now.ToString("yyyyMMddHHmmss") + ".xml";
        FileCompare fc = new FileCompare(Server.MapPath("~"), string.Empty);
        fc.GetFileList();
        List<FileComprInfo> list = fc.FileList;
        list.Sort(new FileNameComparer());
        string s = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n";
        s += "<rss version=\"2.0\">\r\n";
        s += "<foosun>\r\n";
        foreach (FileComprInfo info in list)
        {
            if (info.FaModifyTime != DateTime.MinValue)
                s += "<file name=\"" + info.FileName + "\" size=\"" + info.FaFileSize + "\" modifytime=\"" + info.FaModifyTime + "\" />\r\n";
        }
        s += "</foosun>\r\n";
        s += "</rss>\r\n";
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + xmlfile);
        byte[] buffer = System.Text.Encoding.Default.GetBytes(s);
        Response.BinaryWrite(buffer);
        Response.End();
    }
}
