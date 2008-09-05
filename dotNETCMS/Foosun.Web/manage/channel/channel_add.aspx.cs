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
using Foosun.CMS.Common;
using System.Xml;

public partial class manage_channel_channel_add : Foosun.Web.UI.ManagePage
{
    public manage_channel_channel_add()
    {
        Authority_Code = "C035";
    }
    Channel rd = new Channel();
    rootPublic pd = new rootPublic();
    public string dable = "";
    public static string dirdumm = Foosun.Config.UIConfig.dirDumm;
    public static string dirTemplet = Foosun.Config.UIConfig.dirTemplet;
    public static string dirHTML = Foosun.Config.UIConfig.dirHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../Publish/psf.aspx");
    }

    /// <summary>
    /// 提交数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int ChID = 0;
            int channelType = 0;
            if (Request.QueryString["ChID"] != null && Request.QueryString["ChID"] != string.Empty)
            {
                this.Authority_Code = "C036";
                this.CheckAdminAuthority();
                ChID = int.Parse(Request.QueryString["ChID"].ToString());
            }
            else
            {
                channelType = int.Parse(Request.Form["channelType"]);
            }
            string channelName = this.channelName.Text;
            string channelItem = this.channelItem.Text;
            string channelEItem = this.channelEItem.Text;
            //判断英文是否存在
            if (rd.getItemCount(channelEItem.ToString(), ChID) != 0)
            {
                PageError("频道标志已经存在", "javascript:history.back()", true);
            }
            string channelDescript = this.channelDescript.Text;
            string DataLib = Foosun.Config.UIConfig.dataRe + "channel_" + this.DataLib.Text;
            //判断英文是否存在
            if (rd.getDbCount(DataLib.ToString(), ChID) != 0)
            {
                PageError("数据库表已经存在", "javascript:history.back()", true);
            }
            string channelunit = this.channelunit.Text;
            string htmldir = this.htmldir.Text;
            string indexFileName = this.indexFileName.Text;
            string ClassSave = this.ClassSave.Text;
            string ClassFileName = this.ClassFileName.Text;
            string SavePath = this.SavePath.Text;
            string FileName = this.FileName.Text;
            string binddomain = this.binddomain.Text;
            string TempletPath = this.TempletPath.Text;
            int isconstr = 0;
            if (this.isconstr.Checked)
            {
                isconstr = 1;
            }
            int isHTML = 0;
            if (this.isHTML.Checked) { isHTML = 1; }
            int upfilessize = 0;
            if (this.upfilessize.Text != null && this.upfilessize.Text != string.Empty)
            {
                upfilessize = int.Parse(this.upfilessize.Text);
            }
            string upfiletype = "jpg,gif,jpeg,png,swf,rar,zip,doc,txt,pdf";
            if (this.upfiletype.Text != null && this.upfiletype.Text != string.Empty)
            {
                upfiletype = this.upfiletype.Text;
            }
            int ischeck = 0;
            if (this.ischeck.Checked) { ischeck = 1; }
            int issys = int.Parse(this.issys.SelectedValue.ToString());
            string indextemplet = this.indextemplet.Text;
            string classtemplet = this.classtemplet.Text;
            string newstemplet = this.newstemplet.Text;
            string specialtemplet = this.specialtemplet.Text;
            int isDelPoint = this.UserPop1.AuthorityType;
            int Gpoint = this.UserPop1.Gold;
            int iPoint = this.UserPop1.Point;
            string[] _GroupNumber = this.UserPop1.MemberGroup;
            string GroupNumber = "";
            foreach (string gnum in _GroupNumber)
            {
                if (GroupNumber != "")
                    GroupNumber += ",";
                GroupNumber += gnum;
            }

            //开始创建表
            //插入数据
            DateTime _Temp_date = DateTime.Now;
            Foosun.Model.ChannelInfo uc = new Foosun.Model.ChannelInfo();
            uc.Id = ChID;
            uc.channelType = channelType;
            uc.channelName = channelName;
            uc.channelItem = channelItem;
            uc.channelEItem = channelEItem;
            uc.isConstr = isconstr;
            uc.ClassSave = pd.getResultPage(ClassSave, _Temp_date, "0", channelEItem);
            uc.ClassFileName = pd.getResultPage(ClassFileName, _Temp_date, "0", channelEItem);
            uc.binddomain = binddomain;
            uc.TempletPath = TempletPath;
            uc.SavePath = SavePath;
            uc.FileName = FileName;
            uc.channelDescript = channelDescript;
            uc.DataLib = DataLib;
            uc.channelunit = channelunit;
            uc.htmldir = htmldir;
            uc.indexFileName = indexFileName;
            uc.isHTML = isHTML;
            uc.isDelPoint = isDelPoint;
            uc.Gpoint = Gpoint;
            uc.iPoint = iPoint;
            uc.GroupNumber = GroupNumber;
            uc.issys = issys;
            uc.upfilessize = upfilessize;
            uc.upfiletype = upfiletype;
            uc.ischeck = ischeck;
            uc.indextemplet = indextemplet;
            uc.classtemplet = classtemplet;
            uc.newstemplet = newstemplet;
            uc.specialtemplet = specialtemplet;
            //创建模板目录
            if (TempletPath.Trim() != string.Empty)
            {
                if (dirdumm.Trim() != string.Empty){dirdumm = "/" + dirdumm;}
                string TempletDir = dirdumm + "/" + dirTemplet + "/" + TempletPath;
                TempletDir = TempletDir.Replace("//", "/");
                string dimmTempletDir = Server.MapPath(TempletDir);
                if (!Directory.Exists(dimmTempletDir))
                {
                    Directory.CreateDirectory(dimmTempletDir);
                }
            }
            if (Request.QueryString["ChID"] != null && Request.QueryString["ChID"] != string.Empty)
            {
                rd.updateDate1(uc);
            }
            else
            {
                try
                {
                    rd.creatModeltable(DataLib, channelType, isconstr);
                }
                catch (Exception es)
                {
                    PageError("创建频道数据表失败.原因：<li>"+es.ToString()+"</li>", "javascript:history.back();", true);
                }
                rd.updateDate(uc);
            }
            //开始写配置文件
            StreamWriter sw = null;
            int gChID = 0;
            if (ChID != 0)
            {
                gChID = ChID;
            }
            else
            {
                gChID = rd.GetTopChID(channelEItem);
            }
            string ConfigFileName = HttpContext.Current.Server.MapPath("~/xml/sys/Channel/ChParams/CH_" + gChID + ".config");
            if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/xml/sys/Channel/ChParams")))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/xml/sys/Channel/ChParams"));
            }
            sw = File.CreateText(ConfigFileName);
            sw.WriteLine("<?xml version=\"1.0\" ?>\r");
            sw.WriteLine("<rss version=\"2.0\">\r");
            sw.WriteLine("<channel>\r");
            sw.WriteLine("  <channelname>" + channelName + "</channelname>\r");
            sw.WriteLine("  <channeltemplet>" + TempletPath + "/" + indextemplet + "</channeltemplet>\r");
            sw.WriteLine("  <channelindexname>" + indexFileName + "</channelindexname>\r");
            sw.WriteLine("  <channelindexpath>" + htmldir + "/" + indexFileName + "</channelindexpath>\r");
            sw.WriteLine("  <isHTML>" + isHTML + "</isHTML>\r");
            sw.WriteLine("  <bdomain>" + binddomain + "</bdomain>\r");
            sw.WriteLine("  <htmldir>" + htmldir + "</htmldir>\r");
            sw.WriteLine("</channel>\r");
            sw.WriteLine("</rss>\r");
            sw.Flush();
            sw.Close(); sw.Dispose();

            Response.Write("<script>alert('频道操作成功');window.top.location.href=\"../index.aspx?urls=channel/list.aspx\"</script>");
            Response.End();
            //PageRight("操作成功", "index.aspx?url=model/list.aspx",true);
        } 
    }
}
