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
using Foosun.CMS.Common;
using System.IO;
using System.Xml;

public partial class manage_news_news_page : Foosun.Web.UI.ManagePage
{
    //<--时间：2008-07-04 修改者：吴静岚 页面添加图片上传 图片选择功能 开始
    public string UDir = "\\Content";
    //结束-->
    ContentManage rd = new ContentManage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            #region 分页参数获得
            //<--时间：2008-07-17 修改者：吴静岚 单页分页功能 开始
            try
            {
                this.CheckBox1.Checked = bool.Parse(Foosun.Config.UIConfig.enableAutoPage);
            }
            catch
            {
                this.CheckBox1.Checked = false;
            }
            try
            {
                int i = Int32.Parse(Foosun.Config.UIConfig.splitPageCount);
                this.TxtPageCount.Text = i.ToString();
            }
            catch
            {
                this.TxtPageCount.Text = "20";
            }
            //结束 wjl-->

            #endregion 分页参数获得

            Response.CacheControl = "no-cache";
            Response.Expires = 0;

            SiteCopyRight.InnerHtml = CopyRight;
            string ClassID = Request.QueryString["Number"];
            string Action = Request.QueryString["Action"];

            if (Action != null && Action != string.Empty)
            {
                //<--时间：2008-07-17 修改者：吴静岚 单页分页功能 开始
                //wjl-->
                this.acc.Value = "Edit";
                string sClassID = Request.QueryString["ClassID"];
                if (ClassID == null && ClassID == string.Empty)
                {
                    PageError("错误的参数！", "");
                }
                DataTable dt = rd.getPageContent(sClassID.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.TCname.Text = dt.Rows[0]["ClassCName"].ToString();
                    if (dt.Rows[0]["NaviShowtf"].ToString() == "1")
                    {
                        this.NaviShowtf.Checked = true;
                    }
                    this.TParentId.Text = dt.Rows[0]["ParentID"].ToString();
                    this.TOrder.Text = dt.Rows[0]["OrderID"].ToString();
                    this.KeyMeata.Text = dt.Rows[0]["MetaKeywords"].ToString();
                    this.BeWrite.Text = dt.Rows[0]["MetaDescript"].ToString();
                    this.FProjTemplets.Text = dt.Rows[0]["ClassTemplet"].ToString();
                    this.TPath.Text = dt.Rows[0]["SavePath"].ToString();
                    this.Content.Value = dt.Rows[0]["PageContent"].ToString();
                    this.tContent.Text = dt.Rows[0]["PageContent"].ToString();
                    #region 浏览权限
                    if (dt.Rows[0]["isDelPoint"].ToString() != "0")
                    {
                        this.UserPop1.AuthorityType = int.Parse(dt.Rows[0]["isDelPoint"].ToString());
                        this.UserPop1.Gold = int.Parse(dt.Rows[0]["Gpoint"].ToString());
                        this.UserPop1.Point = int.Parse(dt.Rows[0]["iPoint"].ToString());
                        this.UserPop1.MemberGroup = dt.Rows[0]["GroupNumber"].ToString().Split(',');
                    }
                    this.gClassID.Value = sClassID;
                    if (dt.Rows[0]["ClassTemplet"].ToString().Trim() == "")
                    {
                        rows_key.Style.Value = "display:none";
                        rows_meta.Style.Value = "display:none";
                        div_Templet.Style.Value = "display:none";
                        editorcontent.Style.Value = "display:none";
                        textcontent.Style.Value = "display:block";
                        contentTag.InnerHtml = "网页源码";
                        zt.Checked = true;

                    }
                    else
                    {
                        rows_key.Style.Value = "display:block";
                        rows_meta.Style.Value = "display:block";
                        div_Templet.Style.Value = "display:block";
                        editorcontent.Style.Value = "display:block";
                        textcontent.Style.Value = "display:none";
                        contentTag.InnerHtml = "内容";
                        zt.Checked = false;
                    }
                    #endregion 浏览权限
                }
            }
            else
            {  //<--时间：2008-07-17 修改者：吴静岚 单页分页功能 开始
                //wjl-->
                this.NaviShowtf.Checked = true;
                if (ClassID != null && ClassID != string.Empty)
                {
                    TParentId.Text = ClassID.ToString();
                }
                else
                {
                    TParentId.Text = "0";
                }
                if (SiteID == "0")
                {
                    FProjTemplets.Text = "/{@dirTemplet}/Content/page.html";
                }
                else
                {
                    FProjTemplets.Text = "/{@dirTemplet}/siteTemplets/" + SiteID + "/Content/page.html";
                }
                TPath.Text = "/" + Foosun.Config.UIConfig.dirHtml + "/" + Foosun.Common.Rand.Str_char(5).ToLower() + ".html";
                this.TOrder.Text = "0";
            }
        }
    }


    protected void Buttonsave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string Action = this.acc.Value;
            string ClassID = "";
            if (Action == "Edit")
            {
                ClassID = this.gClassID.Value;
                rd.updateClassStat(0, ClassID);
            }
            else
            {
                ClassID = Foosun.Common.Rand.Number(12);
            }
            string ClassCName = this.TCname.Text;
            string ClassEName = Foosun.Common.Rand.Str_char(10).ToLower();
            string ParentId = this.TParentId.Text;
            if (ParentId.Trim() != "0")
            {
                string s = rd.getDataLib(ParentId);
                if (s == null || s.Trim() == string.Empty)
                {
                    PageError("找不到栏目数据！", "");
                }
            }
            int OrderID = 0;
            if (Foosun.Common.Input.IsInteger(this.TOrder.Text))
            {
                OrderID = int.Parse(this.TOrder.Text);
            }
            else
            {
                PageError("权重必须为数字", "");
            }
            DateTime CreatTime = DateTime.Now;
            string ClassTemplet = this.FProjTemplets.Text;
            string SavePath = this.TPath.Text;
            int NaviShowtf = 0;
            if (this.NaviShowtf.Checked)
            {
                NaviShowtf = 1;
            }
            string MetaKeywords = this.KeyMeata.Text;
            string MetaDescript = this.BeWrite.Text;
            if (MetaKeywords.Length > 200 || MetaDescript.Length > 200)
            {
                PageError("关键字或者描述最多200个字符", "");
            }
            int isDelPoint = this.UserPop1.AuthorityType;
            int Gpoint = this.UserPop1.Point;
            int iPoint = this.UserPop1.Gold;
            string[] _GroupNumber = this.UserPop1.MemberGroup;
            string GroupNumber = "";
            foreach (string gnum in _GroupNumber)
            {
                if (GroupNumber != "")
                    GroupNumber += ",";
                GroupNumber += gnum;
            }
            int isPage = 1;
            string Content=string.Empty;
            if (!this.zt.Checked)
            {
                Content = this.Content.Value;
                if (Content.Trim() == "")
                {
                    PageError("输入内容", "");
                }
            }
            else
            {
                Content = this.tContent.Text;
                ClassTemplet = "";
            }
            #region 分页设置
            //<--时间：2008-07-17 修改者：吴静岚 单页分页功能 开始
            if (!Action.Equals("Edit"))
            {
                
                bool enableAutoPage;
                try
                {
                    enableAutoPage = this.CheckBox1.Checked;
                }
                catch
                {
                    enableAutoPage = false;
                }

                //如果是直帖代码,则不进行分页
                if (this.zt.Checked)
                {
                    enableAutoPage = false;
                }

                if (enableAutoPage)
                {
                    try
                    {
                        Content = Foosun.Common.Input.AutoSplitPage(Content, int.Parse(this.TxtPageCount.Text));
                    }
                    catch
                    {
                        Content = Foosun.Common.Input.AutoSplitPage(Content, 20);
                    }
                }
            }
            //结束 wjl-->
            #endregion

            Foosun.Model.PageContent uc = new Foosun.Model.PageContent();
            uc.ClassID = ClassID;
            uc.ClassCName = ClassCName;
            uc.ClassEName = ClassEName;
            uc.ParentID = ParentId;
            uc.IsURL = 0;
            uc.OrderID = OrderID;
            uc.CreatTime = CreatTime;
            uc.ClassTemplet = ClassTemplet;
            uc.SavePath = SavePath;
            uc.SiteID = SiteID;
            uc.NaviShowtf = NaviShowtf;
            uc.MetaKeywords = MetaKeywords;
            uc.MetaDescript = MetaDescript;
            uc.isDelPoint = isDelPoint;
            uc.Gpoint = Gpoint;
            uc.iPoint = iPoint;
            uc.GroupNumber = GroupNumber;
            uc.isPage = isPage;
            uc.Content = Content;
            if (Action == "Edit")
            {
                //开始生成静态
                rd.updatePage(uc);
            }
            else
            {
                //开始生成静态
                rd.insertPage(uc);
            }
            string ishtml = "";
            if (Foosun.Publish.General.publishPage(ClassID))
            {
                ishtml = "<li>同时生成了静态文件。</li>";
            }
            else
            {
                ishtml = "<li>生成静态文件发生了错误。请查看日志。</li>";
            }
            PageRight("保存数据成功" + ishtml + "", "class_list.aspx");
        }
    }

}
