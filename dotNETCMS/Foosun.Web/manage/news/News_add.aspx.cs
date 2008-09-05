//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Simplt.Xie                      ==
//===========================================================
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
using System.Drawing;
using Foosun.CMS;
using Foosun.CMS.Common;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;

public partial class manage_news_News_add : Foosun.Web.UI.ManagePage
{
    /// <summary>
    /// 权限设置
    /// </summary>
    public manage_news_News_add()
    {
        Authority_Code = "C001";
    }
    #region 实例化
    ContentManage rd = new ContentManage();
    rootPublic pd = new rootPublic();
    protected static string getSiteRoot = "";
    private string dimmdir = Foosun.Config.UIConfig.dirDumm;
    private string localSavedir = Foosun.Config.UIConfig.dirFile;
    public string UDir = "\\Content";
    public int _SetTime = 180;
    public string loadTime = "";
    private DateTime getDateTime = System.DateTime.Now;
    //子新闻
    protected String UnNewsJsArray = "";
    //以下为以后预留
    protected String TopLineArray = "new Array()";
    protected String unNewsid = "";
    protected String FamilyArray = "['Agency FB','Arial','仿宋_GB2312','华文中宋','华文仿宋','华文彩云','华文新魏','华文细黑','华文行楷','宋体','宋体-方正超大字符集','幼圆','新宋体','方正姚体','方正舒体','楷体_GB2312','隶书','黑体']";
    protected String FontStyleArray = "{Regular:0,Bold:1,Italic:2,Underline:4,Strikeout:8}";
    protected String fs_PicInfo = "";
    protected string siteDomain = Foosun.Common.Public.readparamConfig("siteDomain");
    //预留结束
    //子新闻
    #endregion 实例化
    #region 页面初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["NewsID"] != null) { unNewsid = Request.QueryString["NewsID"]; }
        if (unNewsid == null) { unNewsid = ""; }

        if (!IsPostBack)
        {
            
            this.ClassName.Attributes.Add("readonly", "true");
            this.SpecialName.Attributes.Add("readonly", "true");
            this.Button2.Attributes.Add("onclick", "javascript:return checkNews();");

            if (dimmdir.Trim() != string.Empty) { getSiteRoot = siteDomain + dimmdir; }
            else { getSiteRoot = siteDomain; }
            if (getSiteRoot.IndexOf("http://") == -1) { getSiteRoot = "http://" + getSiteRoot; }
            #region 获得相关参数
            //string _Tmpdimmdir = "";
            //if (dimmdir.Trim() != "") { _Tmpdimmdir = "/" + dimmdir; }
            //if (SiteID != "0") { UDir = _Tmpdimmdir + "/" + localSavedir + "/siteFiles/" + SiteID + "/" + UDir; }
            //else { UDir = _Tmpdimmdir + "/" + localSavedir + "/" + UDir; }
            //UDir = Server.MapPath(UDir).Replace("\\", "\\\\") + "\\\\";
            lastTags.InnerHtml = tagslist();
            #endregion 获得相关参数
            #region 自动存稿
            //if (Foosun.Config.UIConfig.saveContent.Split('|')[0] == "1")
            //{
            //    int SetTime = 3;
            //    if (Foosun.Common.Input.IsInteger(Foosun.Config.UIConfig.saveContent.Split('|')[1])) { SetTime = int.Parse(Foosun.Config.UIConfig.saveContent.Split('|')[1]); }
            //    _SetTime = SetTime * 60;
            //    loadTime = "setTimeout('saveContentPage()', 1000)";
            //    divsaveContent.InnerHtml = "<label class=\"reshow\" id=\"div_time\">" + _SetTime + "</label>秒后将自动存稿。";
            //}
            //else { divsaveContent.InnerHtml = "自动存稿功能未开启"; }
            #endregion 自动存搞

            #region 加载服务上所有字体
            FontFamily[] ff = FontFamily.Families;
            foreach (FontFamily family in ff)
            { this.PageFontFamily.Items.Add(new ListItem(family.Name.ToString())); }
            this.PageFontFamily.DataBind();
            #endregion

            #region 如何获得系统字体样式
            ArrayList list = new ArrayList();
            foreach (int i in Enum.GetValues(typeof(System.Drawing.FontStyle)))
            {
                ListItem listitem = new ListItem(Enum.GetName(typeof(System.Drawing.FontStyle), i), i.ToString());
                list.Add(listitem);
            }
            PageFontStyle.Items.Clear();
            PageFontStyle.DataSource = list;
            PageFontStyle.DataValueField = "value";
            PageFontStyle.DataTextField = "text";
            PageFontStyle.DataBind();
            list.Clear();
            #endregion

            #region 得到是添加内容还是修改内容

            if (Request.QueryString["EditAction"] != null & Request.QueryString["EditAction"] != "")
            {
                if (Request.QueryString["EditAction"].ToString() == "Edit")
                {
                    this.EditAction.Value = "Edit";
                    this.tr_editorTime.Visible = true;
                    this.txtEditorTime.Text = getDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                else
                {
                    this.txtCreateTimes.Text = getDateTime.ToString("yyyy-MM-dd HH:mm");
                    this.EditAction.Value = "Add";
                }
            }
            else 
            {
                this.txtCreateTimes.Text = getDateTime.ToString("yyyy-MM-dd HH:mm");
                this.EditAction.Value = "Add"; 
            }
            #endregion 判断结束
            #region 判断导航
            if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"].Trim() != string.Empty)
            {
                string cid = Request.QueryString["ClassID"];
                naviClassName.InnerHtml = getNaviClassName(cid);
                string cnm = rd.getClassCName(cid);
                if (cnm != null && cnm.Trim() != string.Empty)
                {
                    this.ClassID.Value = cid;
                    this.ClassName.Text = cnm;
                }
            }
            else
            {
                naviClassName.InnerHtml = "<img src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />全部内容";
            }
            #endregion 判断导航
            #region 分页参数获得
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
            #endregion 分页参数获得
            #region 获得参数
            string _ClassID = Request.QueryString["ClassID"];
            string _EditAction = Request.QueryString["EditAction"];
            if (_EditAction != null & _EditAction != "")
            {
                this.Authority_Code = "C002";
                this.CheckAdminAuthority();
                if (_EditAction.ToString() == "Edit")
                {
                    m_NewsChar.InnerText = "修改内容";
                    this.style_hidden.Checked = true;
                    string NewsID = Request.QueryString["NewsID"].ToString();
                    #region 得到栏目数据表
                    string _DataLib = Foosun.Config.UIConfig.dataRe + "news";
                    #endregion 得到数据表结束
                    this.NewsID.Value = NewsID;
                    getNewsInfo(NewsID, _DataLib);
                    getdefined.InnerHtml = definelist(_ClassID.ToString(), 1, NewsID, _DataLib);
                }
                else
                {
                    m_NewsChar.InnerText = "添加内容";
                    if (_ClassID != null && _ClassID != "")
                    {
                        getNewsInfo_1(_ClassID.ToString(), 1);
                        getdefined.InnerHtml = definelist(_ClassID.ToString(), 0, "", "");
                    }
                    else
                    {
                        getNewsInfo_1("", 0);
                        getdefined.InnerHtml = definelist("", 0, "", "");
                    }
                    dlFileURL.InnerHtml = "<div id=\"default\" style=\"margin-bottom:1px;\">名称：<input name=\"URLName\" type=\"text\" style=\"width:100px;\" maxlength=\"50\" value=\"\" class=\"form\" id=\"URLName\"/>&nbsp;地址：<input name=\"FileUrl\" type=\"text\" style=\"width:250px;\" maxlength=\"220\" value=\"\" class=\"form\" id=\"FileUrl1\"/>&nbsp;<img src=\"../../sysImages/folder/s.gif\" alt=\"选择附件\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('file',document.Form1.FileUrl1,280,500);document.Form1.FileUrl1.focus();\" />&nbsp; 排序 <input name=\"FileOrderID\" type=\"text\" id=\"FileOrderID\" value=\"0\" style=\"width:50px;\" maxlength=\"1\" class=\"form\" />&nbsp;<font color=\"red\"><a href=\"javascript:Url_add()\" class=\"list_link\"><span class=\"reshow\"><strong>添加附件</strong></span></a></font>&nbsp;<a href='javascript:void(0);' onclick='URL_delete(this.parentNode)' class='list_link'>删除</a></div><div id=\"temp\"></div>";
                }
            }
            else
            {
                m_NewsChar.InnerText = "添加内容";
                getNewsInfo_1("", 0);
                getdefined.InnerHtml = definelist("", 0, "", "");
                dlFileURL.InnerHtml = "<div id=\"default\" style=\"margin-bottom:1px;\">名称：<input name=\"URLName\" type=\"text\" style=\"width:100px;\" maxlength=\"50\" value=\"\" class=\"form\" id=\"URLName\"/>&nbsp;地址：<input name=\"FileUrl\" type=\"text\" style=\"width:250px;\" maxlength=\"220\" value=\"\" class=\"form\" id=\"FileUrl1\"/>&nbsp;<img src=\"../../sysImages/folder/s.gif\" alt=\"选择附件\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('file',document.Form1.FileUrl1,280,500);document.Form1.FileUrl1.focus();\" />&nbsp; 排序 <input name=\"FileOrderID\" type=\"text\" id=\"FileOrderID\" value=\"0\" style=\"width:50px;\" maxlength=\"1\" class=\"form\" />&nbsp;<font color=\"red\"><a href=\"javascript:Url_add()\" class=\"list_link\"><span class=\"reshow\"><strong>添加附件</strong></span></a></font>&nbsp;<a href='javascript:void(0);' onclick='URL_delete(this.parentNode)' class='list_link'>删除</a></div><div id=\"temp\"></div>";
            }
            #endregion 获得参数

            GetunNewsData();
            if (!UnNewsJsArray.Equals("") && !UnNewsJsArray.Equals("new Array()"))
            {
                this.Button1.Visible = true;
            }
            getsurveyJSInfo();
            if (at1RandButton.Checked)
            {
                Page.ClientScript.RegisterStartupScript(ClientScript.GetType(), "picExcute", "<script>ShowLink('pic')</script>");
            }
        }
    }
    #endregion 页面初始化

    protected void GetunNewsData()
    {
        String For_string;
        int For_number;
        if (unNewsid != "")
        {
            #region 编辑不规则新闻
            unNewsid = Foosun.Common.Input.Filter(unNewsid);
            DataTable DT = rd.getUNews(unNewsid);
            if (DT != null && DT.Rows.Count > 0)
            {
                DataTable DTNews = null;
                for (For_number = 0; For_number < DT.Rows.Count; For_number++)
                {
                    {
                        DTNews = rd.sel_DTNews(DT.Rows[For_number]["DataLib"].ToString(), DT.Rows[For_number]["getNewsID"].ToString());
                        if (DTNews != null && DTNews.Rows.Count > 0)
                        {
                            For_string = "'" + DT.Rows[For_number]["getNewsID"] + "','" + DTNews.Rows[0][0] + "','" + DT.Rows[For_number]["NewsTitle"] + "'," + DT.Rows[For_number]["colsNum"] + ",'" + DT.Rows[For_number]["DataLib"] + "','" + DT.Rows[For_number]["titleCSS"] + "'";
                            For_string = "[" + For_string + "]";
                            if (UnNewsJsArray == "")
                            {
                                UnNewsJsArray = For_string;
                            }
                            else
                            {
                                UnNewsJsArray += "," + For_string;
                            }
                        }
                    }

                    if (DTNews != null)
                        DTNews.Dispose();
                    DT.Dispose();
                }
            }
            UnNewsJsArray = "[" + UnNewsJsArray + "]";
            #endregion 编辑不规则新闻
        }
        else
        {
            unNewsid = "";
            UnNewsJsArray = "new Array()";
        }
    }

    /// <summary>
    /// 自定义字段
    /// </summary>
    /// <param name="ClassID">栏目ID</param>
    /// <param name="intNum"></param>
    /// <param name="NewsID">新闻ID</param>
    /// <param name="DataLib">数据库表</param>
    /// <returns></returns>
    protected string definelist(string ClassID, int intNum, string NewsID, string DataLib)
    {
        string _STR = "";
        if (ClassID == "")
        {
            _STR = "<li>没有自定义项目</li>\r<li>如果需要自定义内容，请必须选择有自定义字段的栏目后添加新闻.</li>";
        }
        else
        {
            #region 自定义字段开始
            DataTable dt = rd.getdefineEditTable(ClassID);
            if (dt != null && dt.Rows.Count > 0)
            {
                if ((dt.Rows[0]["Defineworkey"].ToString()) != string.Empty)
                {
                    showClassTF.Visible = false;
                    _STR += "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"4\" cellspacing=\"1\">\r";
                    string dk = dt.Rows[0]["Defineworkey"].ToString();
                    string[] dkARR = dk.Split(',');
                    for (int i = 0; i < dkARR.Length; i++)
                    {
                        _STR += "<tr>";
                        DataTable dts = rd.getdefineEditTablevalue(int.Parse(dkARR[i]));
                        if (dts != null && dts.Rows.Count > 0)
                        {
                            string _dValue = dts.Rows[0]["definevalue"].ToString();
                            string typeFlg = dts.Rows[0]["defineType"].ToString();
                            string _defineColumns = dts.Rows[0]["defineColumns"].ToString();
                            if (NewsID.Trim() != "")
                            {
                                string _modifyDefine = rd.modifyNewsDefineValue(_defineColumns, NewsID, DataLib, "0");
                                string[] _modifyDefineARR = _modifyDefine.Split('|');
                                _dValue = _modifyDefineARR[1];
                            }
                            string inputSTR = "";
                            string isNullStr = "";
                            if (dts.Rows[0]["IsNull"].ToString() != "1")
                            {
                                isNullStr = "<span class=\"reshow\">(*)</span>";
                            }
                            string dvalue = dts.Rows[0]["definedValue"].ToString();
                            string[] dvalueARR = dvalue.Split('\n');
                            switch (typeFlg)
                            {
                                case "1":
                                    inputSTR = "<input style=\"width:200px;\" name=\"" + _defineColumns + "\" value=\"" + _dValue + "\" type=\"text\" />&nbsp;" + isNullStr + "&nbsp;" + dts.Rows[0]["defineExpr"].ToString() + "";
                                    break;
                                case "2":
                                    inputSTR = "<select style=\"width:210px;\" name=\"" + _defineColumns + "\">";
                                    for (int m = 0; m < dvalueARR.Length; m++)
                                    {
                                        if (dvalueARR[m].Trim().ToUpper() == _dValue.Trim().ToUpper())
                                        {
                                            inputSTR += "<option selected value=\"" + dvalueARR[m] + "\">" + dvalueARR[m] + "</option>\r";
                                        }
                                        else
                                        {
                                            inputSTR += "<option value=\"" + dvalueARR[m] + "\">" + dvalueARR[m] + "</option>\r";
                                        }
                                    }
                                    inputSTR += "</select>\r";
                                    break;
                                case "3":
                                    for (int j = 0; j < dvalueARR.Length; j++)
                                    {
                                        if (dvalueARR[j].Trim().ToUpper() == _dValue.Trim().ToUpper())
                                        {
                                            inputSTR += "<input type=\"radio\" name=\"" + _defineColumns + "\" checked value=\"" + dvalueARR[j] + "\">" + dvalueARR[j];
                                        }
                                        else
                                        {
                                            inputSTR += "<input type=\"radio\" name=\"" + _defineColumns + "\" value=\"" + dvalueARR[j] + "\">" + dvalueARR[j];
                                        }
                                    }
                                    break;
                                case "4":
                                    for (int p = 0; p < dvalueARR.Length; p++)
                                    {
                                        if (dvalueARR[p].Trim().ToUpper() == _dValue.Trim().ToUpper())
                                        {
                                            inputSTR += "<input type=\"checkbox\" name=\"" + _defineColumns + "\" checked value=\"" + dvalueARR[p] + "\">" + dvalueARR[p];
                                        }
                                        else
                                        {
                                            inputSTR += "<input type=\"checkbox\" name=\"" + _defineColumns + "\" value=\"" + dvalueARR[p] + "\">" + dvalueARR[p];
                                        }
                                    }
                                    break;
                                case "6":
                                    inputSTR = "<input style=\"width:200px;\" name=\"" + _defineColumns + "\" value=\"" + _dValue + "\" type=\"text\" />&nbsp;<img src=\"../../sysImages/folder/s.gif\" alt=\"选择已有图片\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('pic',document.Form1." + dts.Rows[0]["defineColumns"].ToString() + ",280,500);document.Form1." + dts.Rows[0]["defineColumns"].ToString() + ".focus();\" />&nbsp;" + isNullStr + "&nbsp;" + dts.Rows[0]["defineExpr"].ToString() + "";
                                    break;
                                case "7":
                                    inputSTR = "<input style=\"width:200px;\" name=\"" + _defineColumns + "\" value=\"" + _dValue + "\" type=\"text\" />&nbsp;<img src=\"../../sysImages/folder/s.gif\" alt=\"选择已有文件\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('file',document.Form1." + dts.Rows[0]["defineColumns"].ToString() + ",280,500);document.Form1." + dts.Rows[0]["defineColumns"].ToString() + ".focus();\" />&nbsp;" + isNullStr + "&nbsp;" + dts.Rows[0]["defineExpr"].ToString() + "";
                                    break;
                                case "8":
                                    inputSTR = "<textarea style=\"width:72%;\" name=\"" + _defineColumns + "\" rows=\"5\">" + _dValue + "</textarea>&nbsp;&nbsp;" + isNullStr + "&nbsp;" + dts.Rows[0]["defineExpr"].ToString() + "";
                                    break;
                                case "9":
                                    inputSTR = "<input style=\"width:200px;\" name=\"" + _defineColumns + "\" value=\"" + _dValue + "\" type=\"password\" />&nbsp;&nbsp;" + isNullStr + "&nbsp;" + dts.Rows[0]["defineExpr"].ToString() + "";
                                    break;

                            }
                            _STR += "<td style=\"width:90px;text-align:right;\">" + dts.Rows[0]["defineCname"] + "：</td><td>" + inputSTR + "</td>";
                            dts.Clear(); dts.Dispose();
                        }
                        _STR += "</tr>\r";
                    }
                    _STR += "</table>\r";
                }
                else
                {
                    showClassTF.Visible = true;
                    _STR = "<li>没有自定义项目</li>\r<li>如果需要自定义内容，请必须选择有自定义字段的栏目后添加新闻.</li>";
                }
                dt.Clear(); dt.Dispose();
            }
            #endregion 自定义字段结束
        }
        return _STR;
    }

    /// <summary>
    /// 得到最新的Tags.
    /// </summary>
    /// <returns></returns>
    protected string tagslist()
    {
        string _STR = "<span class=\"reshow\">最近使用过的Tags:</span>";
        DataTable dt = rd.getTagsList();
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                _STR += "<a href=\"javascript:addTags('" + dt.Rows[i]["Cname"].ToString() + "');AddMetaTags('" + dt.Rows[i]["Cname"].ToString() + "');\" class=\"helpstyle\">" + dt.Rows[i]["Cname"].ToString() + "</a>&nbsp;&nbsp;";
            }
            dt.Clear(); dt.Dispose();
        }
        return _STR;
    }


    /// <summary>
    /// 添加内容获得参数
    /// </summary>
    /// <param name="ClassID"></param>
    /// <param name="DataLib"></param>
    /// <param name="_num"></param>
    protected void getNewsInfo_1(string ClassID, int _num)
    {
        #region 数据初始化
        this.ClassID.Value = ClassID;
        this.Templet.Text = "/{@dirTemplet}/Content/news.html";
        this.SavePath.Text = "{@year04}/{@month}{@day}";
        this.FileName.Text = "{@自动编号ID}";
        #endregion 数据初始化
        if (_num == 1)
        {
            #region 继承栏目设置
            DataTable dt = rd.getClassParam(ClassID);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Templet.Text = dt.Rows[0]["ReadNewsTemplet"].ToString();
                this.SavePath.Text = dt.Rows[0]["NewsSavePath"].ToString();
                this.FileName.Text = dt.Rows[0]["NewsFileRule"].ToString();
                if (dt.Rows[0]["FileName"].ToString() == ".html")
                    this.FileEXName.Items[0].Selected = true;
                if (dt.Rows[0]["FileName"].ToString() == ".htm")
                    this.FileEXName.Items[1].Selected = true;
                if (dt.Rows[0]["FileName"].ToString() == ".shtml")
                    this.FileEXName.Items[2].Selected = true;
                if (dt.Rows[0]["FileName"].ToString() == ".shtm")
                    this.FileEXName.Items[3].Selected = true;
                if (dt.Rows[0]["FileName"].ToString() == ".aspx")
                    this.FileEXName.Items[4].Selected = true;
                if (dt.Rows[0]["isComm"].ToString() == "1")
                {
                    this.NewsProperty_CommTF1.Checked = true;
                }
                else
                {
                    this.NewsProperty_CommTF1.Checked = false;
                }
                if (dt.Rows[0]["Checkint"].ToString() == "0")
                    this.CheckStat.Items[0].Selected = true;
                if (dt.Rows[0]["Checkint"].ToString() == "1")
                    this.CheckStat.Items[1].Selected = true;
                if (dt.Rows[0]["Checkint"].ToString() == "2")
                    this.CheckStat.Items[2].Selected = true;
                if (dt.Rows[0]["Checkint"].ToString() == "3")
                    this.CheckStat.Items[3].Selected = true;
                //此处判断时候有更改审核权限的可写权限
                // this.CheckStat.Enabled = false;
                if (dt.Rows[0]["ContentPicTF"].ToString() == "1")
                {
                    this.ContentPicTF.Checked = true;
                    this.ContentPicURL.Text = dt.Rows[0]["ContentPICurl"].ToString();
                    string _ContentPicSize = dt.Rows[0]["ContentPicSize"].ToString();
                    string[] _ContentPicSizeArr = _ContentPicSize.Split('|');
                    this.tHight.Text = _ContentPicSizeArr[0];
                    this.tWidth.Text = _ContentPicSizeArr[1];
                }
                dt.Clear(); dt.Dispose();
            }
            #endregion 继承栏目设置
        }
        else
        {
            #region 继承系统参数
            DataTable dts = rd.getSysParam();
            if (dts != null && dts.Rows.Count > 0)
            {
                this.Templet.Text = dts.Rows[0]["ReadNewsTemplet"].ToString();
                this.SavePath.Text = dts.Rows[0]["SaveNewsDirPath"].ToString();
                this.FileName.Text = dts.Rows[0]["SaveNewsFilePath"].ToString();
                string _fileEX = dts.Rows[0]["FileEXName"].ToString();
                string[] fileEXARR = _fileEX.Split(',');
                if (fileEXARR[0] == "html")
                    this.FileEXName.Items[0].Selected = true;
                if (fileEXARR[0] == "htm")
                    this.FileEXName.Items[1].Selected = true;
                if (fileEXARR[0] == "shtml")
                    this.FileEXName.Items[2].Selected = true;
                if (fileEXARR[0] == "shtm")
                    this.FileEXName.Items[3].Selected = true;
                if (fileEXARR[0] == "aspx")
                    this.FileEXName.Items[4].Selected = true;
                if (dts.Rows[0]["CheckInt"].ToString() == "0")
                    this.CheckStat.Items[0].Selected = true;
                if (dts.Rows[0]["CheckInt"].ToString() == "1")
                    this.CheckStat.Items[1].Selected = true;
                if (dts.Rows[0]["CheckInt"].ToString() == "2")
                    this.CheckStat.Items[2].Selected = true;
                if (dts.Rows[0]["CheckInt"].ToString() == "3")
                    this.CheckStat.Items[3].Selected = true;
                //此处判断时候有更改审核权限的可写权限
                // this.CheckStat.Enabled = false;
                dts.Clear(); dts.Dispose();
            }
            #endregion 继承系统参数
        }
        this.isTimeOutTime.Text = (getDateTime).ToString();
    }

    /// <summary>
    /// 修改内容得到NEWSID的参数
    /// </summary>
    /// <param name="NewsID">传入的新闻ID</param>
    protected void getNewsInfo(string NewsID, string DataLib)
    {
        IDataReader dr = rd.getNewsID(NewsID);
        if (dr.Read())
        {//this.HiddenField_editTime.Value
            #region 基本参数
            if (dr["CreatTime"] != null)
                this.txtCreateTimes.Text = dr["CreatTime"].ToString();
            else
                this.txtCreateTimes.Text = getDateTime.ToString("yyyy-MM-dd HH:mm");
            if (dr["edittime"] != null)
            {
                this.txtEditorTime.Text = dr["edittime"].ToString();
            }
            else
            {
                this.txtEditorTime.Text = getDateTime.ToString("yyyy-MM-dd HH:mm");
            }
            if (dr["NewsType"].ToString() == "0")
                this.atRadioButton.Checked = true;
            if (dr["NewsType"].ToString() == "1")
                this.at1RandButton.Checked = true;
            if (dr["NewsType"].ToString() == "2")
                this.at2RandButton.Checked = true;
            this.NewsTitle.Text = dr["NewsTitle"].ToString();
            this.TitleColor.Value = dr["TitleColor"].ToString();
            if (dr["TitleITF"].ToString() == "1")
                this.TitleITF.Checked = true;
            if (dr["TitleBTF"].ToString() == "1")
                this.TitleBTF.Checked = true;
            if (dr["CommLinkTF"].ToString() == "1")
                this.CommLinkTF.Checked = true;
            if (dr["SubNewsTF"].ToString() == "1")
                this.SubTF.Checked = true;
            //int OrderID = int.Parse(dr["OrderID"].ToString());
            this.OrderIDText.Text = dr["OrderID"].ToString();
            this.sNewsTitle.Text = dr["sNewsTitle"].ToString();
            this.ClassID.Value = dr["ClassID"].ToString();
            this.ClassName.Text = dr["ClassCName"].ToString();

            ///取得专题信息
            DataTable dt = rd.getSpecialNews(NewsID);
            this.SpecialID.Value = "";
            this.SpecialName.Text = "";
            if (dt != null)
            {
                string s_tempsid = "";
                string s_tempsname = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    s_tempsid += dt.Rows[i]["SpecialID"].ToString() + ",";
                    s_tempsname += dt.Rows[i]["SpecialCName"].ToString() + ",";
                }
                if (s_tempsid.Length > 0)
                    s_tempsid = s_tempsid.Substring(0, s_tempsid.Length - 1);
                if (s_tempsname.Length > 0)
                    s_tempsname = s_tempsname.Substring(0, s_tempsname.Length - 1);
                this.SpecialID.Value = s_tempsid;
                this.SpecialName.Text = s_tempsname;
                dt.Clear(); dt.Dispose();
            }
            
            this.URLaddress.Text = dr["URLaddress"].ToString();
            this.PicURL.Text = dr["PicURL"].ToString();
            this.SPicURL.Value = dr["SPicURL"].ToString();
            if ((dr["SPicURL"].ToString()).Trim() != "")
            {
                this.SPicURLTF.Checked = true;
            }
            this.naviContent.Text = dr["naviContent"].ToString();
            this.naviContent.Text = dr["naviContent"].ToString();
            //this.Content.Content = dr["Content"].ToString();
            this.FileContent.Value= dr["Content"].ToString();
            this.vURL.Text = dr["vURL"].ToString();
            this.Templet.Text = dr["Templet"].ToString();
            if (dr["CommTF"].ToString() == "1") { this.NewsProperty_CommTF1.Checked = true; }
            else { this.NewsProperty_CommTF1.Checked = false; }
            if (dr["DiscussTF"].ToString() == "1") { this.NewsProperty_DiscussTF1.Checked = true; }
            else { this.NewsProperty_DiscussTF1.Checked = false; }
            string NewsProperty = dr["NewsProperty"].ToString();
            string[] NewsPropertyArr = NewsProperty.Split(',');
            if (NewsPropertyArr[0] == "1")
                this.NewsProperty_RECTF1.Checked = true;
            if (NewsPropertyArr[1] == "1")
                this.NewsProperty_MARTF1.Checked = true;
            if (NewsPropertyArr[2] == "1")
                this.NewsProperty_HOTTF1.Checked = true;
            if (NewsPropertyArr[3] == "1")
                this.NewsProperty_FILTTF1.Checked = true;
            if (NewsPropertyArr[4] == "1")
                this.NewsProperty_TTTF1.Checked = true;
            if (NewsPropertyArr[5] == "1")
                this.NewsProperty_ANNTF1.Checked = true;
            if (NewsPropertyArr[6] == "1")
                this.NewsProperty_WAPTF1.Checked = true;
            if (NewsPropertyArr[7] == "1")
                this.NewsProperty_JCTF1.Checked = true;
            #endregion 基本参数

            #region 头条参数

            if (dr["NewsPicTopline"].ToString() == "1")
            {
                this.PicTTTF.Checked = true;
                DataTable td = rd.getTopline(NewsID, DataLib, 0);
                if (td != null)
                {
                    if (td.Rows.Count > 0)
                    {
                        string tl_font = td.Rows[0]["tl_font"].ToString();
                        for (int m = 0; m < this.PageFontFamily.Items.Count; m++)
                        {
                            if (this.PageFontFamily.Items[m].Value == tl_font)
                            {
                                this.PageFontFamily.Items[m].Selected = true;
                            }
                        }
                        string tl_style = td.Rows[0]["tl_style"].ToString();
                        for (int n = 0; n < this.PageFontStyle.Items.Count; n++)
                        {
                            if (this.PageFontStyle.Items[n].Value == tl_style)
                            {
                                this.PageFontStyle.Items[n].Selected = true;
                            }
                        }
                        this.fontColor.Value = td.Rows[0]["tl_color"].ToString();
                        this.fontCellpadding.Text = td.Rows[0]["tl_space"].ToString();
                        this.PagefontSize.Text = td.Rows[0]["tl_size"].ToString();
                        this.PagePicwidth.Text = td.Rows[0]["tl_Width"].ToString();
                        this.Imagesbgcolor.Value = td.Rows[0]["tl_PicColor"].ToString();
                        this.topFontInfo.Text = td.Rows[0]["tl_Title"].ToString();
                        this.tl_SavePath.Value = td.Rows[0]["tl_SavePath"].ToString();
                    }
                    td.Clear(); td.Dispose();
                }
            }
            #endregion 头条参数

            #region 其他参数
            this.Souce.Text = dr["Souce"].ToString();
            this.Author.Text = dr["Author"].ToString();
            this.Tags.Text = dr["Tags"].ToString();
            this.Click.Text = dr["Click"].ToString();
            this.Metakeywords.Text = dr["Metakeywords"].ToString();
            this.Metadesc.Text = dr["Metadesc"].ToString();
            this.SavePath.Text = dr["SavePath"].ToString();
            this.FileName.Text = dr["FileName"].ToString();
            this.FileEXName.Text = dr["FileEXName"].ToString();
            if (dr["FileEXName"].ToString() == ".html")
                this.FileEXName.Items[0].Selected = true;
            if (dr["FileEXName"].ToString() == ".htm")
                this.FileEXName.Items[1].Selected = true;
            if (dr["FileEXName"].ToString() == ".shtml")
                this.FileEXName.Items[2].Selected = true;
            if (dr["FileEXName"].ToString() == ".shtm")
                this.FileEXName.Items[3].Selected = true;
            if (dr["FileEXName"].ToString() == ".aspx")
                this.FileEXName.Items[4].Selected = true;
            this.SavePath.Enabled = false;
            this.FileName.Enabled = false;
            this.FileEXName.Enabled = false;
            if (dr["isFiles"].ToString() == "1") { this.isFiles.Checked = true; }
            string _checkStat = dr["CheckStat"].ToString();
            string[] checkStatarr = _checkStat.Split('|');
            if (checkStatarr[0] == "0") { this.CheckStat.Items[0].Selected = true; }
            if (checkStatarr[0] == "1") { this.CheckStat.Items[1].Selected = true; this.CheckStat.Enabled = false; }
            if (checkStatarr[0] == "2") { this.CheckStat.Items[2].Selected = true; this.CheckStat.Enabled = false; }
            if (checkStatarr[0] == "3") { this.CheckStat.Items[3].Selected = true; this.CheckStat.Enabled = false; }
            #endregion 其他参数

            #region 投票
            if (dr["VoteTF"].ToString() == "1")
            {
                this.VoteTF.Checked = true;
                DataTable vt = rd.getVoteID(NewsID, DataLib);
                if (vt != null && vt.Rows.Count > 0)
                {
                    if (vt.Rows[0]["ismTF"].ToString() == "1")
                    {
                        this.ismTF.Checked = true;
                    }
                    if (vt.Rows[0]["isMember"].ToString() == "1")
                    {
                        this.isMember.Checked = true;
                    }
                    this.VoteContent.Text = vt.Rows[0]["voteContent"].ToString();
                    this.isTimeOutTime.Text = vt.Rows[0]["isTimeOutTime"].ToString();
                }
                vt.Clear(); vt.Dispose();
            }
            #endregion 投票

            #region 画中画
            if (dr["ContentPicTF"].ToString() == "1")
            {
                this.ContentPicTF.Checked = true;
                try
                {
                    this.ContentPicURL.Text = dr["ContentPicURL"].ToString();
                    string _PicSize = dr["ContentPicSize"].ToString();
                    string[] PicSizeArr = _PicSize.Split('|');
                    this.tHight.Text = PicSizeArr[0];
                    this.tWidth.Text = PicSizeArr[1];
                }
                catch
                {
                    this.tHight.Text = "200";
                    this.tWidth.Text = "200";
                }
            }
            #endregion 画中画

            #region 浏览权限
            if (dr["isDelPoint"].ToString() != "0")
            {
                this.UserPop1.AuthorityType = int.Parse(dr["isDelPoint"].ToString());
                this.UserPop1.Gold = int.Parse(dr["Gpoint"].ToString());
                this.UserPop1.Point = int.Parse(dr["iPoint"].ToString());
                this.UserPop1.MemberGroup = dr["GroupNumber"].ToString().Split(',');
            }
            #endregion 浏览权限
            #region 得到附件列表
            string TmpList = "";
            DataTable fdt = rd.getFileList(NewsID, DataLib);
            if (fdt != null)
            {
                if (fdt.Rows.Count > 0)
                {
                    for (int fi = 0; fi < fdt.Rows.Count; fi++)
                    {
                        string TmpA = "";
                        if (fi == 0) { TmpA = "&nbsp;<font color=\"red\"><a href=\"javascript:Url_add()\" class=\"list_link\"><span class=\"reshow\"><strong>添加附件</strong></span></a></font>"; }
                        else { TmpA = ""; }
                        TmpList += " 名称：<input name=\"URLName\" type=\"text\" style=\"width:100px;\" maxlength=\"50\" value=\"" + fdt.Rows[fi]["URLName"].ToString() + "\" class=\"form\" id=\"URLName\"/>&nbsp;地址：<input name=\"FileUrl\" type=\"text\" style=\"width:250px;\" maxlength=\"220\" value=\"" + fdt.Rows[fi]["FileURL"].ToString() + "\" class=\"form\" id=\"FileUrl1" + fdt.Rows[fi]["id"].ToString() + "\"/>&nbsp;<img src=\"../../sysImages/folder/s.gif\" alt=\"选择附件\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('file',document.Form1.FileUrl1" + fdt.Rows[fi]["id"].ToString() + ",280,500);document.Form1.FileUrl1" + fdt.Rows[fi]["id"].ToString() + ".focus();\" />&nbsp; 排序 <input name=\"FileOrderID\" type=\"text\" id=\"FileOrderID\" value=\"" + fdt.Rows[fi]["OrderID"].ToString() + "\" style=\"width:50px;\" maxlength=\"1\" class=\"form\" /><input name=\"fIDs\" type=\"hidden\" id=\"fIDs\" value=\"" + fdt.Rows[fi]["id"].ToString() + "\" style=\"width:50px;\" class=\"form\" />&nbsp;" + TmpA + "&nbsp;<a href='javascript:void(0);' onclick='URL_delete(this.parentNode)' class='list_link'>删除</a><div id=\"temp\">";
                    }
                }
                else
                {
                    TmpList += "<div id=\"default\" style=\"margin-bottom:1px;\"> 名称：<input name=\"URLName\" type=\"text\" style=\"width:100px;\" maxlength=\"50\" value=\"\" class=\"form\" id=\"URLName\"/>&nbsp;地址：<input name=\"FileUrl\" type=\"text\" style=\"width:250px;\" maxlength=\"220\" value=\"\" class=\"form\" id=\"FileUrl1\"/>&nbsp;<img src=\"../../sysImages/folder/s.gif\" alt=\"选择附件\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('file',document.Form1.FileUrl1,280,500);document.Form1.FileUrl1.focus();\" />&nbsp; 排序 <input name=\"FileOrderID\" type=\"text\" id=\"FileOrderID\" value=\"0\" style=\"width:50px;\" maxlength=\"100\" class=\"form\" />&nbsp;<font color=\"red\"><a href=\"javascript:Url_add()\" class=\"list_link\"><span class=\"reshow\"><strong>添加附件</strong></span></a></font>&nbsp;<a href='javascript:void(0);' onclick='URL_delete(this.parentNode)' class='list_link'>删除</a></div><div id=\"temp\"></div>";
                }
                fdt.Clear(); fdt.Dispose();
            }
            dlFileURL.InnerHtml = TmpList;
            #endregion 得到附件列表结束
            dr.Close();
        }
        else
        {
            dr.Close();
            PageError("找不到内容记录.输入的参数错误！", "");
        }
    }

    /// <summary>
    /// 得到导航位置
    /// </summary>
    /// <param name="ClassID"></param>
    /// <returns></returns>
    string getNaviClassName(string ClassID)
    {
        string _Str = "";
        IDataReader dr = rd.getNaviClass(ClassID);
        if (dr.Read())
        {
            _Str += "<img src=\"../../sysImages/folder/navidot.gif\" border=\"0\" /><a href=\"News_list.aspx?ClassID=" + dr["ClassID"].ToString() + "\" class=\"topnavichar\">" + dr["ClassCName"] + "</a>(<a href=\"news_add.aspx?ClassID=" + dr["ClassID"].ToString() + "&EditAction=Add\" title=\"添加此栏目下的内容\" class=\"list_link\"><img src=\"../../sysImages/folder/addnews.gif\" border=\"0\" /></a>)";
            if (dr["ParentID"] != DBNull.Value && dr["ParentID"].ToString() != "0")
            {
                IDataReader dr2 = rd.getNaviClass(dr["ParentID"].ToString());
                while (dr2.Read())
                {
                    _Str = "<a href=\"News_list.aspx?ClassID=" + dr2["ClassID"].ToString() + "\" class=\"topnavichar\">" + dr2["ClassCName"] + "</a>(<a href=\"news_add.aspx?ClassID=" + dr2["ClassID"].ToString() + "&EditAction=Add\" title=\"添加此栏目下的内容\" class=\"list_link\"><img src=\"../../sysImages/folder/addnews.gif\" border=\"0\" /></a>)" + _Str;
                    _Str = getNaviClassName(dr2["ParentID"].ToString()) + "<img src=\"../../sysImages/folder/navidot.gif\" border=\"0\" />" + _Str;
                }
                dr2.Close();
            }
        }
        dr.Close();
        return _Str;
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    protected void Buttonsave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            string NewsID = "";
            string EditAction = this.EditAction.Value;
            string ClassID = this.ClassID.Value;
            string SpecialID = this.SpecialID.Value;
            #region 获取栏目数据库表
            string getDataLibStr = "";
            getDataLibStr = rd.getDataLib(ClassID);
            if (getDataLibStr == null || getDataLibStr.Trim() == string.Empty)
            {
                PageError("找不到栏目数据！", "");
            }
            #endregion 获取栏目数据库表
            if (EditAction == "Edit")
            {
                NewsID = this.NewsID.Value;
            }
            else
            {
                NewsID = Foosun.Common.Rand.Number(12);
                #region 检查编号是否存在
            RandFirst: string RandInt = NewsID;
                DataTable rTF = rd.getNewsIDTF(RandInt, getDataLibStr);
                if (rTF.Rows.Count > 0)
                    goto RandFirst;
                rTF.Clear(); rTF.Dispose();
                #endregion 检查编号是否存在结束
            }
            string gFileURL = Request.Form["FileUrl"];
            string gFileOrderID = Request.Form["FileOrderID"];
            string gURLName = Request.Form["URLName"];
            string _NewsType = Request.Form["NewsType"];
            #region 获取表单值
            int NewsType = 0;
            if (_NewsType.ToString() == "at1RandButton") { NewsType = 1; }
            if (_NewsType.ToString() == "at2RandButton") { NewsType = 2; }
            string URLaddress = "";
            if (NewsType != 2)
            {
                //if (((this.Content.Content.Trim())).Length < 3) { PageError("请正确填写内容.长度不能小于3字符!", ""); }
                if (((this.FileContent.Value.Trim())).Length < 3) { PageError("请正确填写内容.长度不能小于3字符!", ""); }
                if (((this.Templet.Text).Trim()).Length < 2) { PageError("模板路径错误!", ""); }
                //if (((this.SavePath.Text).Trim()).Length < 2) { PageError("保存路径参数错误!", ""); }
                if (((this.FileName.Text).Trim()).Length < 1) { PageError("文件名参数错误!", ""); }
                if (((this.FileEXName.Text).Trim()).Length < 4) { PageError("扩展名参数错误!", ""); }
            }
            else
            {
                URLaddress = this.URLaddress.Text;
                if ((URLaddress.Trim()).Length < 5) { PageError("请正确填写外部连接地址!", ""); }
            }
            if (NewsType == 1)
            {
                if (((this.PicURL.Text).Trim()).Length < 5) { PageError("图片请填写图片地址!", ""); }
            }
            string NewsTitle = this.NewsTitle.Text;
            if ((NewsTitle.Trim()).Length < 3) { PageError("请正确填写标题!", ""); }

            #region 得到SiteID
            string site = pd.getSiteIDFromClass(ClassID);
            #endregion 得到SiteID
            if (pd.CheckNewsTitle() == 1)
            {
                if (rd.newsTitletf(NewsTitle, getDataLibStr, EditAction, NewsID) == 1)
                {
                    PageError("新闻标题[<span style=\"color:red\">" + NewsTitle + "</span>]重复！<li>如果需要重复，请在控制面版的参数中设置。</li>", "");
                }
            }
            string TitleColor = this.TitleColor.Value;
            int TitleITF = 0;
            if (this.TitleITF.Checked) { TitleITF = 1; }
            int TitleBTF = 0;
            if (this.TitleBTF.Checked) { TitleBTF = 1; }
            int CommLinkTF = 0;
            if (this.CommLinkTF.Checked) { CommLinkTF = 1; }
            int OrderID = int.Parse(this.OrderIDText.Text);
            string sNewsTitle = this.sNewsTitle.Text;
            int SubNewsTF = 0;
            if (this.SubTF.Checked) { SubNewsTF = 1; }
            string PicURL = this.PicURL.Text;
            int SPicURLTF = 0;
            if (this.SPicURLTF.Checked) { SPicURLTF = 1; }
            string naviContent = this.naviContent.Text;
            if (naviContent.Length > 255)
            {
                PageError("导读最大长度为255个中文字符或者英文字符", "javascript:history.back()", true);
            }
            if (this.sNaviContentFromContent.Checked)
            {
                string LostResultStr = Foosun.Common.Input.LostHTML(naviContent);
                LostResultStr = Foosun.Common.Input.LostPage(LostResultStr);
                LostResultStr = Foosun.Common.Input.LostVoteStr(LostResultStr);
                naviContent = Foosun.Common.Input.GetSubString(LostResultStr, 200);
            }
            string Templet = this.Templet.Text;
            int CommTF = 0;
            if (this.NewsProperty_CommTF1.Checked) { CommTF = 1; }
            int DiscussTF = 0;
            if (this.NewsProperty_DiscussTF1.Checked) { DiscussTF = 1; }
            string NewsProperty_RECTF1 = "0";
            if (this.NewsProperty_RECTF1.Checked) { NewsProperty_RECTF1 = "1"; }
            string NewsProperty_MARTF1 = "0";
            if (this.NewsProperty_MARTF1.Checked) { NewsProperty_MARTF1 = "1"; }
            string NewsProperty_HOTTF1 = "0";
            if (this.NewsProperty_HOTTF1.Checked) { NewsProperty_HOTTF1 = "1"; }
            string NewsProperty_FILTTF1 = "0";
            if (this.NewsProperty_FILTTF1.Checked) { NewsProperty_FILTTF1 = "1"; }
            string NewsProperty_TTTF1 = "0";
            if (this.NewsProperty_TTTF1.Checked) { NewsProperty_TTTF1 = "1"; }
            string NewsProperty_ANNTF1 = "0";
            if (this.NewsProperty_ANNTF1.Checked) { NewsProperty_ANNTF1 = "1"; }
            string NewsProperty_JCTF1 = "0";
            if (this.NewsProperty_JCTF1.Checked) { NewsProperty_JCTF1 = "1"; }
            string NewsProperty_WAPTF1 = "0";
            if (this.NewsProperty_WAPTF1.Checked) { NewsProperty_WAPTF1 = "1"; }
            //推荐,滚动,热点,幻灯,头条,公告,WAP,精彩
            string NewsProperty = NewsProperty_RECTF1 + "," + NewsProperty_MARTF1 + "," + NewsProperty_HOTTF1 + "," + NewsProperty_FILTTF1 + "," + NewsProperty_TTTF1 + "," + NewsProperty_ANNTF1 + "," + NewsProperty_WAPTF1 + "," + NewsProperty_JCTF1;
            string Souce = this.Souce.Text;
            //插入常规表，来源
            if (this.SouceTF.Checked)
            {
                if (Souce.Trim() != string.Empty)
                {
                    rd.iGen(Souce, "", "", 1);
                }
            }
            string Author = this.Author.Text;
            //插入常规表，作者
            if (this.AuthorTF.Checked)
            {
                if (Author.Trim() != string.Empty)
                {
                    rd.iGen(Author, "", "", 2);
                }
            }
            string Tags = this.Tags.Text;
            //插入常规表，tags
            if (this.TagsTF.Checked)
            {
                if (Tags.IndexOf("|") > -1)
                {
                    string[] TagsARR = Tags.Split('|');
                    for (int mt = 0; mt < TagsARR.Length; mt++)
                    {
                        rd.iGen(TagsARR[mt], "", "", 0);
                    }
                }
                else
                {
                    if (Tags.Trim() != string.Empty)
                    {
                        rd.iGen(Tags, "", "", 0);
                    }
                }
            }
            int Click = int.Parse(this.Click.Text);
            string Metakeywords = this.Metakeywords.Text;
            string Metadesc = this.Metadesc.Text;
            string SavePath = this.SavePath.Text;
            string FileName = this.FileName.Text;
            string FileEXName = this.FileEXName.SelectedValue;
            string vURL = this.vURL.Text;
            if (vURL != string.Empty)
            {
                if (vURL.Length < 5) { PageError("请正确填写视频文件!", ""); }
            }
            string _CheckStat = this.CheckStat.SelectedValue;
            string CheckStat = "0|0|0|0";
            int isLock = 0;
            if (_CheckStat == "0") { CheckStat = "0|0|0|0"; isLock = 0; }
            if (_CheckStat == "1") { CheckStat = "1|1|0|0"; isLock = 1; }
            if (_CheckStat == "2") { CheckStat = "2|1|1|0"; isLock = 1; }
            if (_CheckStat == "3") { CheckStat = "3|1|1|1"; isLock = 1; }
            int VoteTF = 0;
            string VoteContent = "";
            if (this.VoteTF.Checked)
            {
                if (NewsType != 2)
                {
                    VoteTF = 1;
                    if ((this.VoteContent.Text).Trim() != null && (this.VoteContent.Text).Trim() != "") { VoteContent = this.VoteContent.Text; }
                    else { PageError("请填写投票项目!", ""); }
                }
            }
            int ContentPicTF = 0;
            string ContentPicURL = "";
            string ContentPicSize = "300|300";
            if (this.ContentPicTF.Checked)
            {
                if (NewsType != 2)
                {
                    ContentPicTF = 1;
                    //插入画中画记录
                    ContentPicURL = this.ContentPicURL.Text;
                    if ((ContentPicURL).Length < 5 || (ContentPicURL).Length > 200) { PageError("画中画内容请正确填写!长度为5-200个字符", ""); }
                    ContentPicSize = this.tHight.Text + "|" + this.tWidth.Text;
                }
            }

            #region 获得权限开始
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
            #endregion 获得权限结束
            //string _Content = this.Content.Content;
            string _Content = this.FileContent.Value;
            string __Content = "";
            if (this.RemoteTF.Checked)//远程保存图片!
            {
                string _dimmdir = "";
                if (dimmdir != null && dimmdir.Trim() != "") { _dimmdir = "/" + dimmdir; }
                string _localSavedir = _dimmdir + "/" + localSavedir + "/content/" + getDateTime.Year + "-" + getDateTime.Month + "";
                string _PhylocalSavedir = Server.MapPath(_localSavedir);
                __Content = getRemoteContent(_Content, _localSavedir, _PhylocalSavedir, "", true);
            }
            else
            {
                //__Content = this.Content.Content;
                __Content = this.FileContent.Value;
            }
            //此处开始内部连接替换！
            string Content = __Content;
             
            //分页
            bool enableAutoPage;
            try
            {
                enableAutoPage = this.CheckBox1.Checked;
            }
            catch
            {
                enableAutoPage = false;
            }

            if (enableAutoPage)
            {
                try
                { 
                    Content = Foosun.Common.Input.AutoSplitPage(Content,int.Parse(this.TxtPageCount.Text));
                }
                catch
                {
                    Content = Foosun.Common.Input.AutoSplitPage(Content,20);
                }
            }
            if (EditAction != "Edit")
            {
                if (Foosun.Config.UIConfig.isLinkTF == "1")
                {
                    DataTable gD = rd.getGenContent();
                    if (gD != null && gD.Rows.Count > 0)
                    {
                        for (int gi = 0; gi < gD.Rows.Count; gi++)
                        {
                            Content = Content.Replace(gD.Rows[gi]["Cname"].ToString(), "<a href=\"" + gD.Rows[gi]["URL"].ToString() + "\" target=\"_blank\">" + gD.Rows[gi]["Cname"].ToString() + "</a>");
                        }
                        gD.Clear(); gD.Dispose();
                    }
                }
            }

            #endregion 获取表单值
            //===================================================================================================
            #region 开始插入值

            Foosun.Model.NewsContent uc = new Foosun.Model.NewsContent();
            uc.NewsType = NewsType;
            uc.OrderID = OrderID;
            uc.NewsTitle = NewsTitle;
            uc.sNewsTitle = sNewsTitle;
            uc.TitleColor = TitleColor;
            uc.TitleITF = TitleITF;
            uc.TitleBTF = TitleBTF;
            uc.CommLinkTF = CommLinkTF;
            uc.SubNewsTF = SubNewsTF;
            uc.URLaddress = URLaddress;
            uc.PicURL = PicURL;
            if (this.sPicFromContent.Checked)
            {
                if (!Foosun.Common.Input.IsInteger(this.btngetContentNum.Text))
                {
                    PageError("提取图片的第几张，请填写数字", "");
                }
                int intPicNum = int.Parse(this.btngetContentNum.Text);
                string pattern = "\\<img\\ [\\s\\S]*?src=['\"]?(?<f>[^'\"\\>\\ ]+)['\"\\>\\ ]";
                Regex reg = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
               // Match match = reg.Match(this.Content.Content);
                Match match = reg.Match(this.FileContent.Value);
                string gPicURL = "";
                int Picnumj = 1;
                while (match.Success)
                {
                    gPicURL = match.Groups["f"].Value;
                    if (Picnumj == intPicNum)
                    {
                        break;
                    }
                    Picnumj++;
                    match = match.NextMatch();
                }
                if (RemoteTF.Checked)
                {
                    string _dimmdir = "";
                    if (dimmdir != null && dimmdir.Trim() != "") { _dimmdir = "/" + dimmdir; }
                    string _localSavedir = _dimmdir + "/" + localSavedir + "/content/" + getDateTime.Year + "-" + getDateTime.Month + "-" + getDateTime.Day;
                    string _PhylocalSavedir = Server.MapPath(_localSavedir);
                    gPicURL = getRemoteContent(gPicURL, _localSavedir, _PhylocalSavedir, "", false);
                }
                uc.PicURL = gPicURL;
                uc.NewsType = 1;
            }
            uc.SPicURL = "";
            #region 生成小图
            if (SPicURLTF == 1)
            {
                string _dimmdirPic = "";
                string fileEX = System.IO.Path.GetExtension(PicURL);
                if (dimmdir != null && dimmdir.Trim() != "") { _dimmdirPic = "/" + dimmdir; }
                string _tmpPic = "/" + localSavedir + "/shortPictures/" + getDateTime.Year + "-" + getDateTime.Month + "/" + getDateTime.Year + getDateTime.Month + getDateTime.Day + Foosun.Common.Rand.Number(5) + fileEX;
                string _localSavePicfile = _dimmdirPic + _tmpPic;
                string _tmpPicURL = _dimmdirPic + PicURL;
                int _sWidth = int.Parse(Foosun.Config.UIConfig.sWidth);
                int _sHeight = int.Parse(Foosun.Config.UIConfig.sHeight);
                if (this.stWidth.Text != null && this.stWidth.Text != "") { _sWidth = int.Parse(this.stWidth.Text); }
                if (this.stHeight.Text != null && this.stHeight.Text != "") { _sHeight = int.Parse(this.stHeight.Text); }
                string _PicURL = Server.MapPath(_tmpPicURL.Replace("{@dirfile}", localSavedir));
                string __PicURL = Server.MapPath(_localSavePicfile.Replace("{@dirfile}", localSavedir));
                string getActionPicUrl = "";
                bool Have_Spic = false;
                if (EditAction == "Edit")
                {
                    getActionPicUrl = __PicURL;
                    if (this.SPicURL.Value != null)
                    {
                        if (this.SPicURL.Value != string.Empty)
                        {
                            Have_Spic = true;
                            getActionPicUrl = Server.MapPath((_dimmdirPic + this.SPicURL.Value).Replace("{@dirfile}", localSavedir));
                        }
                    }
                }
                else
                {
                    getActionPicUrl = __PicURL;
                }
                FSImage FSI = new FSImage(_sWidth, _sHeight, _PicURL);
                //设置比例
                string _FSISmallin = null;
                sys syss = new sys();
                DataTable dt = syss.WaterStart();
                if (dt.Rows.Count > 0)
                    _FSISmallin = dt.Rows[0]["PrintSmallinv"].ToString();
                if (string.IsNullOrEmpty(_FSISmallin))
                    _FSISmallin = "0.5";//默认值
                FSI.Smallin = _FSISmallin;
                FSI.Thumbnail(getActionPicUrl);
                if (EditAction == "Edit")
                {
                    if (Have_Spic == true) { uc.SPicURL = this.SPicURL.Value; }
                    else { uc.SPicURL = _tmpPic.Replace(localSavedir, "{@dirfile}"); }
                }
                else
                {
                    uc.SPicURL = _tmpPic.Replace(localSavedir, "{@dirfile}");
                }
            }
            #endregion 生成小图
            uc.ClassID = ClassID;
            uc.SpecialID = SpecialID;
            uc.Author = Author;
            uc.Souce = Souce;
            uc.Tags = Tags;
            uc.NewsProperty = NewsProperty;
            uc.Templet = Templet;
            uc.Content = Content;
            uc.vURL = vURL;
            uc.naviContent = naviContent;
            uc.Click = Click;
            uc.Metakeywords = Metakeywords;
            uc.Metadesc = Metadesc;
            #region 得到当前新闻的上一条记录自动编号ID
            int _IDStr = 0;
            DataTable dts = rd.getTopNewsId(getDataLibStr);
            if (dts != null && dts.Rows.Count > 0)
            {
                _IDStr = int.Parse(dts.Rows[0]["Id"].ToString());
                dts.Clear(); dts.Dispose();
            }
            else
            {
                _IDStr = int.Parse(Foosun.Common.Rand.Number(8));
            }
            #endregion 结束
            uc.ContentPicTF = ContentPicTF;
            uc.ContentPicURL = ContentPicURL;
            uc.ContentPicSize = ContentPicSize;
            uc.CommTF = CommTF;
            uc.DiscussTF = DiscussTF;
            uc.TopNum = 0;
            uc.VoteTF = VoteTF;
            uc.isDelPoint = isDelPoint;
            uc.iPoint = iPoint;
            uc.Gpoint = Gpoint;
            uc.GroupNumber = GroupNumber;

            if (VoteTF == 1)
            {
                //此处插入投票数据
                int ismTF = 0;
                if (this.ismTF.Checked) { ismTF = 1; }
                int isMember = 0;
                if (this.isMember.Checked) { isMember = 1; }
                Foosun.Model.VoteContent uc2 = new Foosun.Model.VoteContent();
                uc2.NewsID = NewsID;
                uc2.voteTitle = NewsTitle;
                uc2.creattime = getDateTime;
                uc2.ismTF = ismTF;
                uc2.isMember = isMember;
                uc2.DataLib = getDataLibStr;
                uc2.isTimeOutTime = DateTime.Parse(this.isTimeOutTime.Text);
                uc2.voteContent = VoteContent;
                if (VoteContent.IndexOf("|") == -1)
                {
                    PageError("输入的投票参数错误，请仔细查看帮助说明！<li>可能原因：你输入的投票选项没有输入初始的投票数量。</li><li>格式：投票项|投票数</li>", "");
                }
                if (EditAction == "Edit")
                {
                    //更新投票
                    rd.UpdateVote(uc2);
                }
                else
                {
                    //插入投票
                    string _VoteNum = getDateTime.Year + "" + getDateTime.Month + "" + getDateTime.Day + "" + getDateTime.Hour + "" + Foosun.Common.Rand.Number(8);
                    uc2.voteNum = _VoteNum;
                    uc2.SiteID = site;
                    rd.intsertVote(uc2);
                }
            }
            uc.isLock = isLock;
            uc.Editor = pd.getUserName(UserNum);
            uc.isVoteTF = 0;
            #region 插入头条
            if (NewsProperty_TTTF1 == "1")
            {
                if (this.PicTTTF.Checked)
                {
                    uc.NewsPicTopline = 1;
                    Foosun.Model.NewsContentTT uc1 = new Foosun.Model.NewsContentTT();
                    uc1.NewsTF = 0;
                    uc1.NewsID = NewsID;
                    uc1.DataLib = getDataLibStr;
                    uc1.Creattime = DateTime.Parse((getDateTime).ToString());
                    uc1.tl_font = this.PageFontFamily.SelectedValue;
                    uc1.tl_style = int.Parse((this.PageFontStyle.SelectedValue).ToString());
                    uc1.tl_size = int.Parse((this.PagefontSize.Text).ToString());
                    uc1.tl_color = this.fontColor.Value;
                    uc1.tl_space = int.Parse((this.fontCellpadding.Text).ToString());
                    uc1.tl_PicColor = this.Imagesbgcolor.Value;
                    #region 动作
                    string _tl_Title = "";
                    if (this.topFontInfo.Text != null & this.topFontInfo.Text != "") { _tl_Title = this.topFontInfo.Text; }
                    else { _tl_Title = NewsTitle; }
                    uc1.tl_Title = _tl_Title;
                    uc1.tl_Width = int.Parse((this.PagePicwidth.Text).ToString());
                    uc1.SiteID = site;
                    string _tl_SavePath = "";
                    #region 更新头条
                    if (EditAction == "Edit")
                    {
                        if (this.tl_SavePath.Value != null && this.tl_SavePath.Value != "")
                        {
                            _tl_SavePath = this.tl_SavePath.Value;
                        }
                        else
                        {
                            _tl_SavePath = "/{@dirFile}/topline/" + getDateTime.Year + "-" + getDateTime.Month + "/" + Foosun.Common.Rand.Number(15) + ".jpg";
                        }
                        uc1.tl_SavePath = _tl_SavePath;
                        rd.UpdateTT(uc1);
                    }
                    else
                    {
                        _tl_SavePath = "/{@dirFile}/topline/" + getDateTime.Year + "-" + getDateTime.Month + "/" + Foosun.Common.Rand.Number(15) + ".jpg";
                        uc1.tl_SavePath = _tl_SavePath;
                        rd.intsertTT(uc1);
                    }
                    #endregion 结束
                    #endregion 动作
                    #region 生成头条图片

                    string _dimmdirTT = "";
                    if (dimmdir != null && dimmdir.Trim() != "") { _dimmdirTT = "/" + dimmdir; }
                    string _localSaveTT = _dimmdirTT + _tl_SavePath;
                    string _Tmp_SavePath = Server.MapPath(_localSaveTT.Replace("{@dirFile}", localSavedir));
                    FSImage FSI = new FSImage(int.Parse((this.PagePicwidth.Text).ToString()), 0, _Tmp_SavePath);
                    FSI.FontFamilyName = this.PageFontFamily.SelectedValue;
                    switch (int.Parse((this.PageFontStyle.SelectedValue).ToString()))
                    {
                        case 0:
                            FSI.StrStyle = FontStyle.Regular;
                            break;
                        case 1:
                            FSI.StrStyle = FontStyle.Bold;
                            break;
                        case 2:
                            FSI.StrStyle = FontStyle.Italic;
                            break;
                        case 3:
                            FSI.StrStyle = FontStyle.Underline;
                            break;
                        case 4:
                            FSI.StrStyle = FontStyle.Strikeout;
                            break;
                    }
                    string FTColor = this.fontColor.Value;
                    string BGColor = this.Imagesbgcolor.Value;
                    FSI.FontSize = int.Parse((this.PagefontSize.Text).ToString());
                    FSI.FontColor = Color.FromArgb(Convert.ToInt32(FTColor.Substring(0, 2), 16), Convert.ToInt32(FTColor.Substring(2, 2), 16), Convert.ToInt32(FTColor.Substring(4, 2), 16));
                    FSI.BackGroudColor = Color.FromArgb(Convert.ToInt32(BGColor.Substring(0, 2), 16), Convert.ToInt32(BGColor.Substring(2, 2), 16), Convert.ToInt32(BGColor.Substring(4, 2), 16));
                    FSI.Title = _tl_Title;
                    FSI.TextPos = new PointF(0, 0);
                    FSI.GenerateTextPic();
                    #endregion 生成头条图片
                }
            }
            else
            {
                uc.NewsPicTopline = 0;
            }
            uc.DataLib = getDataLibStr;
            #endregion 插入头条

            #endregion 插入值
            //============================================================================================
            #region 自定义字段
            int _DefineID = 0;
            uc.DefineID = 0;
            string _dClassID = Request.QueryString["ClassID"];
            if (_dClassID == ClassID)
            {
                if (_dClassID != string.Empty && _dClassID != null)
                {
                    DataTable ddt = rd.getdefineEditTable(_dClassID);
                    if (ddt != null && ddt.Rows.Count > 0)
                    {
                        if (ddt.Rows[0]["Defineworkey"].ToString().Trim() != "")
                        {
                            string[] DefineworkeyARR = (ddt.Rows[0]["Defineworkey"].ToString()).Split(',');
                            for (int ddi = 0; ddi < DefineworkeyARR.Length; ddi++)
                            {
                                DataTable ddiv = rd.getdefineEditTablevalue(int.Parse(DefineworkeyARR[ddi]));
                                if (ddiv != null)
                                {
                                    if (ddiv.Rows.Count > 0)
                                    {
                                        string dsContent = Request.Form["" + ddiv.Rows[0]["defineColumns"].ToString() + ""];
                                        //if (ddiv.Rows[0]["IsNull"].ToString() == "0")
                                        //{
                                        //    if (dsContent.Trim() == string.Empty) { PageError("自定义字段有必填项目，而您并未填写", ""); }
                                        //}
                                        if (EditAction == "Edit") { rd.UpdateDefineSign(NewsID, ddiv.Rows[0]["defineColumns"].ToString(), getDataLibStr, 0, dsContent, "0"); }
                                        else { rd.insertDefineSign(NewsID, ddiv.Rows[0]["defineColumns"].ToString(), getDataLibStr, 0, dsContent, "0"); }
                                        _DefineID = 1;
                                    }
                                    ddiv.Clear(); ddiv.Dispose();
                                }
                            }
                        }
                        ddt.Clear(); ddt.Dispose();
                        uc.DefineID = _DefineID;
                    }
                }
            }
            #endregion 自定义字段
            uc.SiteID = site;
            //判断是否需要生成百度开放式搜索协议
            if (SearchEngine.IsBaidu() == "1") { SearchEngine.RefreshBaidu(); }
            uc.isFiles = 0;
            if (this.isFiles.Checked)
            {
                uc.isFiles = 1;
                string fIDs = "";
                if (EditAction == "Edit")
                {
                    fIDs = Request.Form["fIDs"];
                }
                if (gFileURL.IndexOf(',') != -1)
                {
                    string[] FileURL = gFileURL.Split(',');
                    string[] FileOrderID = gFileOrderID.Split(',');
                    string[] URLName = gURLName.Split(',');
                    string[] ids = null;
                    if (fIDs != "" && fIDs != null)
                    {
                        ids = fIDs.Split(',');
                        rd.deleteFileUrl(fIDs, NewsID);
                    }
                    for (int fj = 0; fj < FileURL.Length; fj++)
                    {
                        if (EditAction == "Edit")
                        {
                            try
                            {
                                if (rd.getFileIDTF(int.Parse(ids[fj])) == 1)
                                {
                                    rd.updateFileURL(URLName[fj], getDataLibStr, FileURL[fj], int.Parse(FileOrderID[fj]), int.Parse(ids[fj]));
                                }
                                else
                                {
                                    rd.insertFileURL(URLName[fj], NewsID, getDataLibStr, FileURL[fj], int.Parse(FileOrderID[fj]));
                                }
                            }
                            catch
                            {
                                rd.insertFileURL(URLName[fj], NewsID, getDataLibStr, FileURL[fj], int.Parse(FileOrderID[fj]));
                            }
                        }
                        else
                        {
                            rd.insertFileURL(URLName[fj], NewsID, getDataLibStr, FileURL[fj], int.Parse(FileOrderID[fj]));
                        }
                    }
                }
                else
                {
                    if (EditAction == "Edit")
                    {
                        try
                        {
                            if (rd.getFileIDTF(int.Parse(fIDs)) == 1)
                            {
                                rd.updateFileURL(gURLName, getDataLibStr, gFileURL, int.Parse(gFileOrderID), int.Parse(fIDs));
                            }
                            else
                            {
                                rd.insertFileURL(gURLName, NewsID, getDataLibStr, gFileURL, int.Parse(gFileOrderID));
                            }
                        }
                        catch
                        {
                            rd.insertFileURL(gURLName, NewsID, getDataLibStr, gFileURL, int.Parse(gFileOrderID));
                        }
                    }
                    else
                    {
                        rd.insertFileURL(gURLName, NewsID, getDataLibStr, gFileURL, int.Parse(gFileOrderID));
                    }
                    //rd.insertFileURL(gURLName, NewsID, getDataLibStr, gFileURL, int.Parse(gFileOrderID));
                }
            }
            //插入子新闻
            string iNewsTF = "";
            uc.SubNewsTF = 0;
            if (SubTF.Checked)
            {
                String OldNewsId = Foosun.Common.Input.Filter(Request.Form["NewsIDs"]);
                String[] Arr_OldNewsId;
                String getNewsTitle, NewsRow, NewsTable, titleCSS;
                #region 判断数据是否合法
                if (OldNewsId == null)
                {
                    PageError("不规则新闻为空", "");
                }
                #endregion 判断数据是否合法

                #region 获取普通新闻数据
                if (OldNewsId != null)
                {
                    OldNewsId = OldNewsId.Replace(" ", "");
                    Arr_OldNewsId = OldNewsId.Split(',');
                }
                else
                {
                    OldNewsId = "";
                    Arr_OldNewsId = OldNewsId.Split(new char[] { ',' });
                }
                string unNewsids = NewsID;
                if (EditAction == "Edit")
                {
                    rd.delSubID(unNewsids);
                }
                iNewsTF = "<li>同时添加子新闻成功</li>";
                uc.SubNewsTF = 1;
                for (int For_Num = 0; For_Num < Arr_OldNewsId.Length; For_Num++)
                {
                    getNewsTitle = Request.Form["getNewsTitle" + Arr_OldNewsId[For_Num]];
                    NewsRow = Request.Form["Row" + Arr_OldNewsId[For_Num]];
                    NewsTable = Request.Form["NewsTable" + Arr_OldNewsId[For_Num]];
                    titleCSS = Request.Form["titleCSS" + Arr_OldNewsId[For_Num]];
                    if (rd.Add_SubNews(unNewsids, Arr_OldNewsId[For_Num], NewsRow, getNewsTitle, NewsTable, SiteID, titleCSS) == 0)
                    {
                        iNewsTF = "<li>子新闻添加因为某个原因操作失败</li>";
                        uc.SubNewsTF = 0;
                    }
                }
                #endregion 获取普通新闻数据结束
            }
            uc.SavePath = pd.getResultPage(SavePath, getDateTime, ClassID, "");
            string tmID = "{@自动编号ID}";
            uc.FileName = pd.getResultPage(FileName.Replace(tmID, (_IDStr + 1).ToString()), getDateTime, ClassID, "");
            uc.FileEXName = FileEXName;
            uc.CheckStat = CheckStat;            
            uc.isRecyle = 0;
            //创建时间
            uc.CreatTime = DateTime.Parse(this.txtCreateTimes.Text);
            //编辑时间
            if (string.IsNullOrEmpty(this.txtEditorTime.Text))
            {
                uc.EditTime = DateTime.Now;
            }
            else
            {
                uc.EditTime = DateTime.Parse(this.txtEditorTime.Text);
            }
            //更新栏目状态
            rd.updateClassStat(0, ClassID);
            uc.isHtml = 0;
            string resultstr = string.Empty;
            if (EditAction == "Edit")
            {
                uc.NewsID = NewsID;
                rd.UpdateNewsContent(uc);
                resultstr = "新闻：<font color=\"red\">[" + NewsTitle + "]</font>&nbsp;&nbsp;修改成功!<li><a href=\"News_Add.aspx?ClassID=" + ClassID + "&EditAction=Edit&NewsID=" + NewsID + "\" class=\"list_link\"><b><font color=\"red\">继续修改</font></b></a>&nbsp;┊&nbsp;<a href=\"News_add.aspx?ClassID=" + ClassID + "&EditAction=Add\" class=\"list_link\"><b><font color=\"red\">添加新闻</font></b></a>&nbsp;┊&nbsp;<a href=\"News_list.aspx?ClassID=" + ClassID + "\" class=\"list_link\"><b><font color=\"blue\">返回本栏目列表</font></b></a></li>" + iNewsTF + "";
            }
            else
            {
                uc.NewsID = NewsID;
                rd.insertNewsContent(uc);
                resultstr = "新闻：<font color=\"red\">[" + NewsTitle + "]</font>&nbsp;&nbsp;添加成功!<li><a href=\"News_Add.aspx?ClassID=" + ClassID + "&EditAction=Add\" class=\"list_link\"><b><font color=\"red\">继续添加</font></b></a>&nbsp;┊&nbsp;<a href=\"News_add.aspx?ClassID=" + ClassID + "&NewsID=" + NewsID + "&EditAction=Edit\" class=\"list_link\"><b><font color=\"red\">修改本条新闻</font></b></a>&nbsp;┊&nbsp;<a href=\"News_list.aspx?ClassID=" + ClassID + "\" class=\"list_link\"><b><font color=\"blue\">返回本栏目列表</font></b></a></li>" + iNewsTF + "";
            }
            string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
            if (isDelPoint == 0)
            {
                if (this.isHTML.Checked)
                {
                    if (ReadType == "0")
                    {
                        //<--wjl  2008-07-22 解决是否允许评论问题
                        bool isCom = false;
                        if (uc.CommTF.ToString().Equals("1"))
                        {
                            isCom = true;
                        }
                        if (Foosun.Publish.General.publishSingleNews(NewsID, ClassID, isCom))
                        {
                            //--wjl>
                            rd.updateNewsHTML(1, NewsID);
                            resultstr += "<li>此页已经自动生成了静态页面，<a href=\"News_review.aspx?ID=" + NewsID + "\" target=\"_blank\">浏览本页</a></li>";
                        }
                        else
                        {
                            resultstr += "<li><span class=\"reshow\">此页生成静态页面失败</span> <a href=\"../Publish/error/GetError.aspx\">查看日志</a></li>";
                        }
                    }
                }
            }
            string[] publicfreshinfoARR = Foosun.Config.UIConfig.publicfreshinfo.Split('|');
            string publicclass = publicfreshinfoARR[0].ToString();
            string publicspecial = publicfreshinfoARR[1].ToString();
            if (publicclass == "1")
            {
                Foosun.Publish.General pn = new Foosun.Publish.General();
                if (pn.publishSingleClass(ClassID))
                {
                    resultstr += "<li>本新闻所在的栏目同时也生成了静态页面</li>";
                    rd.updateClassStat(1, ClassID);
                }
            }
            if (SpecialID.Trim() != null && SpecialID != "")
            {
                if (publicspecial == "1")
                {
                    string[] arr_specialID = uc.SpecialID.Split(',');
                    for (int i = 0; i < arr_specialID.Length; i++)
                    {
                        Foosun.Publish.General Pn = new Foosun.Publish.General();
                        Pn.publishSingleSpecial(arr_specialID[i].ToString());
                    }
                    resultstr += "<li>本新闻所属的专题已经生成了静态页面</li>";
                }
            }
            PageRight(resultstr, "News_list.aspx?ClassID=" + ClassID + "");
        }

    }

    /// <summary>
    /// 远程存图
    /// </summary>
    /// <param name="_Content"></param>
    /// <param name="_localSavedir"></param>
    /// <param name="_PhylocalSavedir"></param>
    /// <param name="o1"></param>
    /// <param name="ReminTF"></param>
    /// <returns></returns>
    public string getRemoteContent(string _Content, string _localSavedir, string _PhylocalSavedir, string o1, bool ReminTF)
    {
        Foosun.CMS.Collect.RemoteResource red = new Foosun.CMS.Collect.RemoteResource(_Content, _localSavedir, _PhylocalSavedir, "", ReminTF);
        red.FileExtends = new string[] { "gif", "jpg", "bmp", "ico", "png", "jpeg", "swf", "rar", "zip", "cab", "doc", "rm", "ram", "wav", "mid", "mp3", "avi", "wmv" };
        red.FetchResource();
        return red.Content;
    }

    /// <summary>
    /// 调查JS
    /// </summary>
    /// <returns>调查JS</returns>
    /// 编写时间2007-04-28   Code By DengXi

    protected void getsurveyJSInfo()
    {
        Foosun.CMS.Label lc = new Foosun.CMS.Label();
        DataTable dt = lc.getsurveyJSInfo();
        if (dt != null)
        {
            surveyJSID.DataTextField = "Title";
            surveyJSID.DataValueField = "TID";
            surveyJSID.DataSource = dt;
            surveyJSID.DataBind();
            dt.Clear();
            dt.Dispose();
        }
        ListItem itm = new ListItem();
        itm.Selected = true;
        itm.Text = "请选择";
        itm.Value = "";
        surveyJSID.Items.Insert(0, itm);
        itm = null;
    }
}
