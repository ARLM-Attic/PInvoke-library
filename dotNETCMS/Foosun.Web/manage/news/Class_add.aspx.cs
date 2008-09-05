//***Code by Simplt.Xie (C)2007 Foosun INC.
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

public partial class manage_news_Class_add : Foosun.Web.UI.ManagePage
{
 /// <summary>
 /// 权限设置
 /// </summary>
    public manage_news_Class_add()
    {
        Authority_Code = "C021";
    }
    ContentManage rd = new ContentManage();
    rootPublic pd = new rootPublic();
    public string dirm = Foosun.Config.UIConfig.dirDumm;
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            string getSiteID = Request.QueryString["SiteID"];
            if (getSiteID == null)
            {
                PageError("错误的参数,请先选择频道", "javascript:history.back();");
            }
            if (dirm.Trim() != string.Empty) { dirm = "/" + dirm; }
            SiteCopyRight.InnerHtml = CopyRight;
            string action = Request.QueryString["Acation"];
            string cname = Request.QueryString["Cname"];
            String Pram = Request.QueryString["Number"];    //获取查看是否父类
            if (action != null&&action!="")
            {
                string[] StrNum = action.Split(',');
                if (StrNum[0] == "Add")
                {
                    //权限管理
                    this.Authority_Code = "C022";
                    this.CheckAdminAuthority();
                    ChangeStatic(StrNum[1]);
                    DefineRows_div.InnerHtml = DefineRowslist(StrNum[1]);
                }
                else
                {
                    PageError("参数不正确,请返回正确操作!","class_list.aspx");
                }
            }
            else
            {
                if (Request.QueryString["Number"] != string.Empty)
                {
                    if (Request.QueryString["SiteID"] == string.Empty||Request.QueryString["SiteID"]==null)
                    {
                        if (SiteID != "0")
                        {
                            sitelabel.InnerHtml = pd.getChName(SiteID);
                        }
                        else
                        {
                            sitelabel.InnerHtml = pd.getChName("0");
                        }
                    }
                    else
                    {
                        sitelabel.InnerHtml = pd.getChName(Request.QueryString["SiteID"].ToString()); ;
                    }
                }
                if (Pram == null || Pram == "")
                    Pram = "foosun";
                SatratData(Pram);
                TParentId.Enabled = false;
                DefineRows_div.InnerHtml = DefineRowslist("");
            }
        }
    }

    /// <summary>
    /// 获得已经选择的自定义
    /// </summary>
    /// <param name="ClassID"></param>
    /// <returns></returns>
    protected string DefineRowslist(string ClassID)
    {
        string _STR = "<select disabled style=\"height:129px;width:131px;\" class=\"form\" name=\"DefineRows\"  multiple=\"multiple\">";
        if (ClassID != "")
        {
            DataTable dte = rd.getdefineEditTable(ClassID);
            if (dte != null)
            {
                string TmpDefineworkey = dte.Rows[0]["Defineworkey"].ToString();
                if (TmpDefineworkey.Trim() != string.Empty)
                {
                    string[] TmpDefineworkeyARR = TmpDefineworkey.Split(',');
                    for (int m = 0; m < TmpDefineworkeyARR.Length; m++)
                    {
                        DataTable dt = rd.getdefineEditTablevalue(int.Parse(TmpDefineworkeyARR[m]));
                        if (dt != null)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                _STR += "<option value=" + dt.Rows[0]["id"].ToString() + ">" + dt.Rows[0]["defineCname"].ToString() + "</option>\r";
                            }
                            dt.Clear(); dt.Dispose();
                        }
                    }
                }
                dte.Clear(); dte.Dispose();
            }
        }
        _STR += "</select>";
        return _STR;
    }
    /// <summary>
    /// 绑定用户选择,保存新闻数据表
    /// </summary>
    protected void UserNewsTable(string Str)
    {
        string _Str = Str;
        if (SiteID != "0")
        {
            DataTable DtSite = rd.getSiteParam(SiteID);
            if (DtSite != null)
            {
                if (DtSite.Rows.Count > 0)
                {
                    _Str = DtSite.Rows[0]["DataLib"].ToString().ToUpper();
                }
                DtSite.Clear(); DtSite.Dispose();
            }
        }
    }

    //修改初始化
    protected void ChangeStatic(string Num)
    {
        //检查是否数据表里有数据
        DataTable dt = rd.getClassContent(Num);
        if (dt!=null&&dt.Rows.Count > 0)
        {
            TCname.Text = dt.Rows[0]["ClassCName"].ToString();
            TEname.Text = dt.Rows[0]["ClassEName"].ToString();
            //栏目英文不可写
            //TEname.Enabled = false;
            modifynote.InnerHtml = "&nbsp;<span class=\"reshow\">修改后可能产生垃圾文件</span>";
            TParentId.Text = dt.Rows[0]["ParentID"].ToString();
            //父编号不可写
            TParentId.Enabled = false;
            ProjectStatic(dt.Rows[0]["IsURL"].ToString());
            TOrder.Text = dt.Rows[0]["OrderID"].ToString();
            TAddress.Text = dt.Rows[0]["Urladdress"].ToString();
            THoustAddress.Text = dt.Rows[0]["Domain"].ToString();
            THoustAddress.Enabled = false;

            FProjTemplets.Text = dt.Rows[0]["ClassTemplet"].ToString();
            FListTemplets.Text = dt.Rows[0]["ReadNewsTemplet"].ToString();
            TPath.Text = dt.Rows[0]["SavePath"].ToString();
            DirData1.Text = dt.Rows[0]["SaveClassframe"].ToString();
            DirData2.Text = dt.Rows[0]["ClassSaveRule"].ToString();
            DirData3.Text = dt.Rows[0]["ClassIndexRule"].ToString();
            NewsSave.Text = dt.Rows[0]["NewsSavePath"].ToString();
            NewsDisplay.Text = dt.Rows[0]["NewsFileRule"].ToString();
            ImageUpload.Text = dt.Rows[0]["PicDirPath"].ToString();
            sitelabel.InnerHtml = pd.getChName(dt.Rows[0]["SiteID"].ToString());
            ClassID.Value = dt.Rows[0]["ClassID"].ToString();
            this.UserPop1.AuthorityType = int.Parse(dt.Rows[0]["isDelPoint"].ToString());
            this.UserPop1.Point = int.Parse(dt.Rows[0]["iPoint"].ToString());
            this.UserPop1.Gold = int.Parse(dt.Rows[0]["Gpoint"].ToString());
            this.UserPop1.MemberGroup = dt.Rows[0]["GroupNumber"].ToString().Split(',');
            if (dt.Rows[0]["FileName"].ToString() == ".html")
                ExDropDownList.Items[0].Selected = true;
            if (dt.Rows[0]["FileName"].ToString() == ".htm")
                ExDropDownList.Items[1].Selected = true;
            if (dt.Rows[0]["FileName"].ToString() == ".shtml")
                ExDropDownList.Items[2].Selected = true;
            if (dt.Rows[0]["FileName"].ToString() == ".shtm")
                ExDropDownList.Items[3].Selected = true;
            if (dt.Rows[0]["FileName"].ToString() == ".aspx")
                ExDropDownList.Items[4].Selected = true;
            
            
            if (dt.Rows[0]["Checkint"].ToString() == "0")
                Auditing.Items[0].Selected = true;
            if (dt.Rows[0]["Checkint"].ToString() == "1")
                Auditing.Items[1].Selected = true;
            if (dt.Rows[0]["Checkint"].ToString() == "2")
                Auditing.Items[2].Selected = true;
            if (dt.Rows[0]["Checkint"].ToString() == "3")
                Auditing.Items[3].Selected = true;

            //检测是否允许画中画
            if (dt.Rows[0]["ContentPicTF"].ToString() == "1" && dt.Rows[0]["IsURL"].ToString() != "1")
            {
                draw.Checked = true;
                Page.RegisterStartupScript("", "<Script>document.getElementById(\"ClssStyle_21\").style.display = \"\";document.getElementById(\"ClssStyle_22\").style.display = \"\";</script>");
                //画中画地址
                drawUrl.Text = dt.Rows[0]["ContentPICurl"].ToString();
                //检测参数设置是否有值
                if (dt.Rows[0]["ContentPicSize"].ToString() != null && dt.Rows[0]["ContentPicSize"].ToString() != String.Empty)
                {
                    string[] wh = dt.Rows[0]["ContentPicSize"].ToString().Split('|');
                    drawWith.Text = wh[0].ToString();
                    drawHeight.Text = wh[1].ToString();
                }
            }
            else
            {
                draw.Checked = false;
                Page.RegisterStartupScript("", "<script>document.getElementById(\"ClssStyle_21\").style.display = \"none\";document.getElementById(\"ClssStyle_22\").style.display = \"none\";</script>");
            }
            ClassIDNum.Value = dt.Rows[0]["ClassID"].ToString();
            Pigeonhole.Text = dt.Rows[0]["InHitoryDay"].ToString();

            UserNewsTable(dt.Rows[0]["DataLib"].ToString());
            //SiteID.Text = dt.Rows[0]["SiteID"].ToString();
            //SiteID.Enabled = false;
            //是否在导航中显示
            if (dt.Rows[0]["NaviShowtf"].ToString() == "1")
            {
                NaviShowtf.Checked = true;
            }
            else
            {
                NaviShowtf.Checked = false;
            }

            //导航文字/图片
            fontText.Text = dt.Rows[0]["NaviContent"].ToString();
            fileLoad.Text = dt.Rows[0]["NaviPIC"].ToString();
            KeyMeata.Text = dt.Rows[0]["MetaKeywords"].ToString();
            BeWrite.Text = dt.Rows[0]["MetaDescript"].ToString();

            //是否允许评论
            if (dt.Rows[0]["isComm"].ToString() == "1")
            {
                Saying.Checked = true;
            }
            else
                Saying.Checked = false;
            HtmlPhrasing.Text = Foosun.Common.Input.ToTxt(dt.Rows[0]["NaviPosition"].ToString());
            NewsHtmlPhrasing.Text = Foosun.Common.Input.ToTxt(dt.Rows[0]["NewsPosition"].ToString());
            Hidden.Value = "Add";
            this.HiddenDefine.Value = dt.Rows[0]["Defineworkey"].ToString(); 

            //处理提交信息
            btnClick.Text = "保存数据";
            #region 输出自定义自段
            DataTable dts = rd.getdefineTable();
            if (dts != null)
            {
                DefineColumns.DataTextField = "defineCname";
                DefineColumns.DataValueField = "Id";
                DefineColumns.DataSource = dts;
                DefineColumns.DataBind();
                dts.Clear();
                dts.Dispose();
            }
            #endregion
 
        }
        else 
        {
            PageError("参数不正确,请正确操作!","class_list.aspx");
        }

    }

    /// <summary>
    /// 检测是否外部栏目
    /// </summary>
    /// <param name="Str"></param>
    protected void ProjectStatic(string Str)
    {
        if (Str == "1")
            CProject.Checked = true;
        else
        {
            CProject.Checked = false;
        }
        csHiden.Value = "1";
    }

    /// <summary>
    /// 数据初始化
    /// </summary>
    /// <param name="Pram"></param>
    protected void SatratData(string Pram)
    {
        UserNewsTable("0");
        //检查参数是父类ID是否有效
        if (Pram != "foosun")
        {
            DataTable dt = rd.getParentClass(Pram);
            if (dt!=null)
            {
                if (dt.Rows.Count > 0){TParentId.Text = Pram;}
                else{PageError("传入的参数不正确!", "");}
            }
            else{PageError("传入的参数不正确!", "");}
        }
        else{TParentId.Text = "0";}

        #region 输出自定义自段
        DataTable dts = rd.getdefineTable();
        if (dts != null)
        {
            DefineColumns.DataTextField = "defineCname";
            DefineColumns.DataValueField = "Id";
            DefineColumns.DataSource = dts;
            DefineColumns.DataBind();
            dts.Clear();
            dts.Dispose();
        }
        #endregion
        //继承参数设置
        DirData2.Text = "{@EName}/index.html";
        DirData3.Text = pd.SaveIndexPage().ToString();
        TOrder.Text = "10";
        Pigeonhole.Text = "180";
        string tmSite = "0";
        string gSiteID = Request.QueryString["SiteID"];
        if (SiteID == "0")
        {
            if (gSiteID!=null)
            {
                if (gSiteID.Trim() != string.Empty)
                {
                    tmSite = gSiteID.ToString();
                }
            }
            TPath.Text = pd.SaveClassFilePath(tmSite); //栏目保存路径
            ClassID.Value = "";
            NewsDisplay.Text = pd.SaveNewsFilePath().ToString();
            NewsSave.Text = pd.SaveNewsDirPath().ToString();
            if (pd.CheckInt().ToString() == "0")
                Auditing.Items[0].Selected = true;
            if (pd.CheckInt().ToString() == "1")
                Auditing.Items[1].Selected = true;
            if (pd.CheckInt().ToString() == "2")
                Auditing.Items[2].Selected = true;
            if (pd.CheckInt().ToString() == "3")
                Auditing.Items[3].Selected = true;

            if (pd.PicServerTF().ToString() == "1")
            {
                ImageUpload.Text = pd.PicServerDomain().ToString();
            }
            else
            {
                ImageUpload.Text = "/{@dirFile}";
            }

        }
        else
        {
            tmSite = SiteID;
        }
        this.FProjTemplets.Text = rd.GetParamBase("ClasslistTemplet");
        this.FListTemplets.Text = rd.GetParamBase("ReadNewsTemplet");
        this.TPath.Text = pd.SaveClassFilePath(tmSite);//DtSite.Rows[0]["SaveDirPath"].ToString();
        this.NewsDisplay.Text = rd.GetParamBase("SaveNewsFilePath");
        this.NewsSave.Text = rd.GetParamBase("SaveNewsDirPath");
        //this.ImageUpload.Text = DtSite.Rows[0]["PicSavePath"].ToString();
        //UserNewsTable(DtSite.Rows[0]["DataLib"].ToString());
    }

    protected void btnClick_Click(object sender, EventArgs e)
    {
        #region
        if (Page.IsValid)
        {
            //取得隐藏控件取
            String VclassId = Hidden.Value.ToString().Trim();
            switch (VclassId)
            {
                case "0":
                    SaveData(0);
                    break;
                case "Add":
                    SaveData(1);    //修改数据
                    break;
                default:
                    PageError("参数不正确,请重新正确操作!", "");
                    break;
            }
        }
        #endregion
    }

   /// <summary>
   /// 保存数据
   /// </summary>
   /// <param name="InterChar"></param>
    protected void SaveData(int InterChar)
    {
        if (Page.IsValid)                       //判断是否验证成功
        {
            #region 得到表单值

            string ClassID = "";
            if (InterChar == 0)
            {
                ClassID = Foosun.Common.Rand.Number(12);
            }
            else
            {
                ClassID = this.ClassID.Value;
                rd.updateClassStat(0, ClassID);
            }
            string ClassCName = this.TCname.Text;
            string ClassEName = this.TEname.Text;
            if (InterChar == 0)
            {
                DataTable edt = rd.getClassEname(ClassEName);
                if (edt != null)
                {
                    if (edt.Rows.Count > 0)
                    {
                        PageError("栏目的英文名称在站点中已经存在!", "class_list.aspx");
                    }
                    edt.Clear(); edt.Dispose();
                }
            }
            string ParentID = this.TParentId.Text;
            int IsURL = 0;
            if (this.CProject.Checked)
            {
                IsURL = 1;
            }
            int OrderID = int.Parse(this.TOrder.Text);
            string URLaddress = this.TAddress.Text;
            if (IsURL == 1)
            {
                if (URLaddress.Length < 5)
                {
                    PageError("请正确外部连接地址!", "class_list.aspx");
                }
            }
            string Domain = this.THoustAddress.Text;
            if (Domain != null && Domain != "")
            {
                if (Domain.Length < 5)
                {
                    PageError("请正确填写域名地址!", "class_list.aspx");
                }
            }
            DateTime _Temp_date = System.DateTime.Now;
            string ClassTemplet = this.FProjTemplets.Text;
            string ReadNewsTemplet = this.FListTemplets.Text;
            string SavePath = this.TPath.Text;
            string SaveClassframe = this.DirData1.Text;
            int Checkint = int.Parse(this.Auditing.SelectedValue);
            string ClassSaveRule = this.DirData2.Text;
            if (ClassSaveRule.IndexOf(".") == -1)
            {
                PageError("文件名规则必须填写文件名!", "class_list.aspx");
            }
            string ClassIndexRule = this.DirData3.Text;
            string NewsSavePath = this.NewsSave.Text;
            string NewsFileRule = this.NewsDisplay.Text;
            string PicDirPath = this.ImageUpload.Text;
            int ContentPicTF = 0;
            if (this.draw.Checked == true)
            {
                ContentPicTF = 1;
                if (((this.drawUrl.Text).ToString()).Length<5)
                {
                    PageError("请正确填写画中画内容!", "class_list.aspx");
                }
                if (((this.drawWith.Text).ToString()).Length<1)
                {
                    PageError("请正确填写画中画图片(Flash)，代码的宽度!", "class_list.aspx");
                }
                if (((this.drawHeight.Text).ToString()).Length<1)
                {
                    PageError("请正确填写画中画图片(Flash),代码的高度!", "class_list.aspx");
                }
            }
            string ContentPICurl = this.drawUrl.Text;
            string ContentPicSize = this.drawHeight.Text + "|" + this.drawWith.Text;
            int InHitoryDay = int.Parse(this.Pigeonhole.Text);
            string site = "0";
            if (SiteID == "0")
            {
                if (Request.QueryString["SiteID"] != string.Empty&&Request.QueryString["SiteID"] !=null)
                {
                    site = Request.QueryString["SiteID"];
                }
            }
            else { site = SiteID; }
            int NaviShowtf = 0;
            if (this.NaviShowtf.Checked) { NaviShowtf = 1; }
            string NaviPIC = this.fileLoad.Text;
            string NaviContent = this.fontText.Text;
            string MetaKeywords = this.KeyMeata.Text;
            string MetaDescript = this.BeWrite.Text;
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
            string FileName = Request.Form["ExDropDownList"];
            int isLock = 0;
            int isRecyle = 0;
            string NaviPosition = this.HtmlPhrasing.Text;
            string NewsPosition = this.NewsHtmlPhrasing.Text;
            int isComm = 0;
            if (this.Saying.Checked) { isComm = 1; }
            string Defineworkey = this.HiddenDefine.Value;

            Foosun.Model.ClassContent uc = new Foosun.Model.ClassContent();
            uc.ClassCName = ClassCName;
            uc.ClassEName = ClassEName;
            uc.ParentID = ParentID;
            uc.IsURL = IsURL;
            uc.OrderID = OrderID;
            uc.URLaddress = URLaddress;
            uc.Domain = Domain;
            uc.ClassTemplet = ClassTemplet;
            uc.ReadNewsTemplet = ReadNewsTemplet;

            uc.SavePath = SavePath;
            uc.Checkint = Checkint;
            uc.SaveClassframe = pd.getResultPage(SaveClassframe, _Temp_date, "0", ClassEName);
            uc.ClassSaveRule = pd.getResultPage(ClassSaveRule, _Temp_date, "0", ClassEName);
            uc.ClassIndexRule = ClassIndexRule;//pd.getResultPage(ClassIndexRule, _Temp_date, "0", ClassEName);
            uc.NewsSavePath = NewsSavePath;
            uc.NewsFileRule = NewsFileRule;
            uc.PicDirPath = PicDirPath;
            uc.ContentPicTF = ContentPicTF;
            uc.ContentPICurl = ContentPICurl;
            uc.ContentPicSize = ContentPicSize;
            uc.InHitoryDay = InHitoryDay;
            uc.SiteID = site;
            uc.NaviShowtf = NaviShowtf;
            uc.NaviPIC = NaviPIC;
            uc.NaviContent = NaviContent;
            uc.MetaKeywords = MetaKeywords;
            uc.MetaDescript = MetaDescript;
            uc.isDelPoint = isDelPoint;
            uc.Gpoint = Gpoint;
            uc.iPoint = iPoint;
            uc.GroupNumber = GroupNumber;

            uc.FileName = FileName;
            uc.isLock = isLock;
            uc.isRecyle = isRecyle;
            uc.NaviPosition = ReplaceNavi(NaviPosition,ParentID).Replace("//","/");
            uc.NewsPosition = ReplaceNavi(NewsPosition, ParentID).Replace("//", "/");
            uc.isComm = isComm;
            uc.Defineworkey = Defineworkey;
            uc.ClassID = ClassID;
            uc.CreatTime = _Temp_date;
            Foosun.Common.Public.saveClassXML(ClassEName);
            if (InterChar == 0)
            {
                rd.insertClassContent(uc);
                rd.updateReplaceNavi(ClassID);
                //更新导航
                PageRight("添加栏目成功!<li><a class=\"list_link\" href=\"Class_Add.aspx?Acation=Add," + ClassID + "&SiteID=" + Request.QueryString["SiteID"] + "\"><font color=\"red\">修改此栏目</font></a></li>", "class_list.aspx");
            }
            else
            {
                rd.UpdateClassContent(uc);
                //更新导航
                rd.updateReplaceNavi(ClassID);
                PageRight("修改栏目成功!<li><a class=\"list_link\" href=\"Class_Add.aspx?Acation=Add," + ClassID + "&SiteID="+Request.QueryString["SiteID"]+"\"><font color=\"red\">继续修改此栏目</font></a></li>", "class_list.aspx");
            }
            #endregion 得到表单值

        }
    }

    /// <summary>
    /// 替换导航
    /// </summary>
    /// <param name="Content"></param>
    /// <param name="ClassID"></param>
    /// <returns></returns>
    protected string ReplaceNavi(string Contents, string ClassID)
    {
        string getResult = "";
        getResult = Contents.Replace("{@ParentClassStr}", getNaviClassName(ClassID));
        return getResult;
    }

    protected string getNaviClassName(string ClassID)
    {
        string _Str = "";
        if (dirm.Trim() != string.Empty)
        {
            dirm = "/" + dirm;
        }
        string urls = "";
        IDataReader dr = rd.getNaviClass(ClassID);
        if (dr.Read())
        {
            string tmsavebstr = dirm + "/" + dr["SavePath"].ToString() + "/" + dr["SaveClassframe"].ToString() + "/" + dr["ClassSaveRule"].ToString();
            _Str += "<a href=\"" + tmsavebstr.Replace("//", "/") + "\">" + dr["ClassCName"] + "</a> >> ";
            if (dr["ParentID"] != DBNull.Value && dr["ParentID"].ToString() != "0")
            {
                IDataReader dr2 = rd.getNaviClass(dr["ParentID"].ToString());
                while (dr2.Read())
                {
                    urls = dirm + "/" + dr["SavePath"].ToString() + "/" + dr2["SaveClassframe"].ToString() + "/" + dr2["ClassSaveRule"].ToString();
                    _Str = "<a href=\"" + urls.Replace("//", "/") + "\">" + dr2["ClassCName"] + "</a> >> " + _Str;
                    _Str = getNaviClassName(dr2["ParentID"].ToString()) + _Str;
                }
                dr2.Close();
            }
        }
        dr.Close();
        return _Str;
    }

    protected string getClassSavePath()
    {
        string str_classSavePath = "";
        string str_SiteID = Request.QueryString["SiteID"];
        bool tf = false;
        if (SiteID != "0")
        {
            str_SiteID = SiteID;
            tf = true;
        }
        else
        {
            if (str_SiteID == "0")
            {
                tf = false;
            }
            else
            {
                tf = true;
            }
        }
        if (tf)
        {
            str_classSavePath = pd.SaveClassFilePath(str_SiteID); //栏目保存路径
        }
        else
        {
            str_classSavePath = Foosun.Config.UIConfig.dirHtml;
        }
        return str_classSavePath;
    }

    protected void TOrder_TextChanged(object sender, EventArgs e)
    {

    }

}
