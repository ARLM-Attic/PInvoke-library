//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By ChenZhaoHui                        ==
//=====================================================================
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

public partial class sys_Param : Foosun.Web.UI.ManagePage
{
    public sys_Param()
    {
        Authority_Code = "Q001";
    }
    sys sys = new sys();//初始化实例
    Foosun.Model.STsys_param su;
    Foosun.CMS.Common.rootPublic rp = new Foosun.CMS.Common.rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        #region 页面载入时版权信息,加载函数
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";//设置页面无缓存

            if (SiteID != "0") { PageError("您没有参数管理的权限", ""); }
            
            //LoginInfo.CheckPop("权限代码", "0", "1", "9");//权限代码
            copyright.InnerHtml = CopyRight;
            ParamStartLoad();//参数设置页面载入初始化数据
            UserStartLoad();//会员设置页面载入初始化数据
            WaterStartLoad();//水印设置页面载入初始化数据
            FtpRssStartLoad();//FtpRss设置页面载入初始数据
            Model_Templet();

            JsModel1.Items.Insert(0, new ListItem("选择模型", "0"));
            JsModel2.Items.Insert(0, new ListItem("选择模型", "0"));
            JsModel3.Items.Insert(0, new ListItem("选择模型", "0"));
            JsModel4.Items.Insert(0, new ListItem("选择模型", "0"));
            JsModel5.Items.Insert(0, new ListItem("选择模型", "0"));
            #region 选择会员组
            try
            {
                DataTable ugroup = sys.UserGroup();
                this.RegGroupNumber.DataTextField = "GroupName";
                this.RegGroupNumber.DataValueField = "GroupNumber";
                this.RegGroupNumber.DataSource = ugroup;
                this.RegGroupNumber.DataBind();
                //RegGroupNumber.Items.Insert(0, new ListItem("--请选择会员组--", "0"));
            }
            catch { }
            #endregion

            #region 会员注册初始
            DataTable dt = sys.UserReg();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    this.regitemContent.Text = dt.Rows[0]["regItem"].ToString();
                    int n = int.Parse(dt.Rows[0]["returnemail"].ToString());
                    if (n == 0)
                        this.returnemail.Items[0].Selected = true;
                    else
                        this.returnemail.Items[1].Selected = true;
                    int n1 = int.Parse(dt.Rows[0]["returnmobile"].ToString());
                    if (n1 == 0)
                        this.returnmobile.Items[0].Selected = true;
                    else
                        this.returnmobile.Items[1].Selected = true;
                }
                dt.Clear();
                dt.Dispose();
            }
            #endregion

            #region 设置发布类型
            string publishStates = Foosun.Common.Public.readparamConfig("publishState");
            if (string.IsNullOrEmpty(publishStates))
            {
                publishStates = "true";
            }
            else
            {
                if (bool.Parse(publishStates))
                {
                    this.radPublishOne.Checked = true;
                }
                else
                {
                    this.radPublishTwo.Checked = true;
                }
            }
            #endregion

            #region 设置分页样式
            string PageStyles = Foosun.Common.Public.readparamConfig("PageStyle");
            string PageLinkCounts = Foosun.Common.Public.readparamConfig("PageLinkCount");
            if (string.IsNullOrEmpty(PageStyles))
            {
                PageStyles = "0";
            }
            if (string.IsNullOrEmpty(PageLinkCounts))
                PageLinkCounts = "10";
            this.NewsPageStyle.SelectedIndex = Convert.ToInt32(PageStyles);
            this.PageLinkCount.Text = PageLinkCounts;

            //设置是否开启分页  默认开启
            string isAutoPageSplit = Foosun.Config.UIConfig.enableAutoPage;
            if (string.IsNullOrEmpty(isAutoPageSplit))
            {
                this.RadioButton_autoPageSplit1.Checked = true;
            }
            else
            {
                isAutoPageSplit = isAutoPageSplit.ToLower();
                if (isAutoPageSplit.Equals("true"))
                {
                    this.RadioButton_autoPageSplit1.Checked = true;
                }
                else
                {
                    this.RadioButton_autoPageSplit2.Checked = true;
                }
            }

            //设置分页字数
            this.txt_pageSplitCount.Text = Foosun.Config.UIConfig.splitPageCount;
            #endregion
        }
        #endregion
    }
        
    /// <summary>
    /// 基本信息
    /// </summary>
    /// <returns>取得基本信息</returns>
    /// Code By ChenZhaohui

    void ParamStartLoad()
    {
        #region 从参数设置表中读出数据并初始化赋值
        DataTable dt = sys.BasePramStart();
        if (dt.Rows.Count > 0)
        {
            #region 基本参数设置部分

            SiteName.Text = dt.Rows[0]["siteName"].ToString();
            SiteDomain.Text = dt.Rows[0]["SiteDomain"].ToString();
            IndexTemplet.Text = dt.Rows[0]["IndexTemplet"].ToString();
            IndexFileName.Text = dt.Rows[0]["IndexFileName"].ToString();
            FileEXName.Text = dt.Rows[0]["FileEXName"].ToString();
            ReadNewsTemplet.Text = dt.Rows[0]["ReadNewsTemplet"].ToString();
            ClassListTemplet.Text = dt.Rows[0]["ClassListTemplet"].ToString();
            SpecialTemplet.Text = dt.Rows[0]["SpecialTemplet"].ToString();

            #region 前台浏览方式
            string readtypee = dt.Rows[0]["ReadType"].ToString();
            if (readtypee == "1")
            {
                DongTai.Checked = true;
                JingTai.Checked = false;
            }
            else
            {
                DongTai.Checked = false;
                JingTai.Checked = true;
            }
            #endregion

            LoginTimeOut.Text = dt.Rows[0]["LoginTimeOut"].ToString();
            Email.Text = dt.Rows[0]["Email"].ToString();

            #region 前台路径
            string linktypee = dt.Rows[0]["LinkType"].ToString();
            if (linktypee == "1")
            {
                JueDui.Checked = true;
                XiangDui.Checked = false;
            }
            else
            {
                JueDui.Checked = false;
                XiangDui.Checked = true;
            }
            #endregion
            BaseCopyRight.Value = Foosun.Common.Input.ToTxt(dt.Rows[0]["CopyRight"].ToString());
            CheckInt.Text = dt.Rows[0]["CheckInt"].ToString();

            #region 图片盗链
            string pic = dt.Rows[0]["UnLinkTF"].ToString();
            if (pic == "1")
            {
                Yes.Checked = true;
                No.Checked = false;
            }
            else
            {
                Yes.Checked = false;
                No.Checked = true;
            }
            #endregion
            LenSearch.Text = dt.Rows[0]["LenSearch"].ToString();
            InsertPicPosition.Text = dt.Rows[0]["InsertPicPosition"].ToString();
            HistoryNum.Text = dt.Rows[0]["HistoryNum"].ToString();
            #region checktitle
            string checktitlee = dt.Rows[0]["CheckNewsTitle"].ToString();
            if (checktitlee == "1")
            {
                checktitle.Checked = true;
                nochecktitle.Checked = false;
            }
            else
            {
                checktitle.Checked = false;
                nochecktitle.Checked = true;
            }
            #endregion

            #region check collect
            string check = dt.Rows[0]["CollectTF"].ToString();
            if (check == "1")
            {
                Ischeck.Checked = true;
                nocheck.Checked = false;
            }
            else
            {
                Ischeck.Checked = false;
                nocheck.Checked = true;
            }
            #endregion

            SaveClassFilePath.Text = dt.Rows[0]["SaveClassFilePath"].ToString();
            SaveIndexPage.Text = dt.Rows[0]["SaveIndexPage"].ToString();
            SaveNewsFilePath.Text = dt.Rows[0]["SaveNewsFilePath"].ToString();
            SaveNewsDirPath.Text = dt.Rows[0]["SaveNewsDirPath"].ToString();
            Pram_Index.Text = dt.Rows[0]["Pram_Index"].ToString();//每页显示控制

            #endregion

            #region 上传参数设置部分

            #region 图片是否作为单独域名
            string piccc = dt.Rows[0]["PicServerTF"].ToString();

            if (piccc == "1")
            {
                picy.Checked = true;
                picn.Checked = false;
            }
            if (piccc == "0")
            {
                picy.Checked = false;
                picn.Checked = true;
            }
            #endregion

            PicServerDomain.Text = dt.Rows[0]["PicServerDomain"].ToString();
            PicUpload.Text = dt.Rows[0]["PicUpload"].ToString();
            UpfilesType.Text = dt.Rows[0]["UpfilesType"].ToString();
            UpFilesSize.Text = dt.Rows[0]["UpFilesSize"].ToString();

            #region 远程域名启用
            string domain = dt.Rows[0]["ReMoteDomainTF"].ToString();
            if (domain == "1")
            {
                domainr.Checked = true;
                domainn.Checked = false;
            }
            else
            {
                domainr.Checked = false;
                domainn.Checked = true;
            }
            #endregion

            RemoteDomain.Text = dt.Rows[0]["RemoteDomain"].ToString();
            RemoteSavePath.Text = dt.Rows[0]["RemoteSavePath"].ToString();
            ClassListNum.Text = dt.Rows[0]["ClassListNum"].ToString();
            NewsNum.Text = dt.Rows[0]["NewsNum"].ToString();
            BatDelNum.Text = dt.Rows[0]["BatDelNum"].ToString();
            SpecialNum.Text = dt.Rows[0]["SpecialNum"].ToString();
            #endregion

            #region JS,FTP参数设置部分

            #region 分离出数据分别显示在相应的TextBox中--热点JS
            string[] HotJS = dt.Rows[0]["HotNewsJs"].ToString().Split('|');
            JsNews1.Text = HotJS[0];
            JsTitle1.Text = HotJS[1];
            JsModel1.Text = HotJS[2];
            #endregion

            #region 分离出数据分别显示在相应的TextBox中--最新JS
            string[] LastJS = dt.Rows[0]["LastNewsJs"].ToString().Split('|');
            JsNews2.Text = LastJS[0];
            JsTitle2.Text = LastJS[1];
            JsModel2.Text = LastJS[2];
            #endregion

            #region 分离出数据分别显示在相应的TextBox中--推荐JS
            string[] RecJS = dt.Rows[0]["RecNewsJS"].ToString().Split('|');
            JsNews3.Text = RecJS[0];
            JsTitle3.Text = RecJS[1];
            JsModel3.Text = RecJS[2];
            #endregion

            #region 分离出数据分别显示在相应的TextBox中--评论JS
            string[] ComJS = dt.Rows[0]["HotCommJS"].ToString().Split('|');
            JsNews4.Text = ComJS[0];
            JsTitle4.Text = ComJS[1];
            JsModel4.Text = ComJS[2];
            #endregion

            #region 分离出数据分别显示在相应的TextBox中--头条JS
            string[] TJS = dt.Rows[0]["TNewsJS"].ToString().Split('|');
            JsNews5.Text = TJS[0];
            JsTitle5.Text = TJS[1];
            JsModel5.Text = TJS[2];
            #endregion

            #endregion
        }
        #endregion
    }

    /// <summary>
    /// FTP/RSS
    /// </summary>
    /// <returns>取得FTP/RSS信息</returns>
    /// Code By ChenZhaohui

    void FtpRssStartLoad()
    {
        #region 从其他设置表中读出数据并初始化赋值
        DataTable dt = sys.FtpRss();
        if (dt.Rows.Count > 0)
        {
            #region  FTP
            #region ftp
            string ftp = dt.Rows[0]["FtpTF"].ToString();
            if (ftp == "1")
            {
                ftpy.Checked = true;
                ftpn.Checked = false;
            }
            else
            {
                ftpy.Checked = false;
                ftpn.Checked = true;
            }
            #endregion
            FTPIP.Text = dt.Rows[0]["FTPIP"].ToString();
            Ftpport.Text = dt.Rows[0]["Ftpport"].ToString();
            FtpUserName.Text = dt.Rows[0]["FtpUserName"].ToString();
            FTPPASSword.Text = Foosun.Common.Input.NcyString(dt.Rows[0]["FTPPASSword"].ToString());//字符串解密显示
            #endregion

            #region RSS参数设置部分

            RssNum.Text = dt.Rows[0]["RssNum"].ToString();
            RssContentNum.Text = dt.Rows[0]["RssContentNum"].ToString();
            RssTitle.Text = dt.Rows[0]["RssTitle"].ToString();
            RssPicURL.Text = dt.Rows[0]["RssPicURL"].ToString();

            #region 是否添加到wap中
            string str_wap = dt.Rows[0]["WapTF"].ToString();
            if (str_wap == "1")
            {
                wapy.Checked = true;
                wapn.Checked = false;
            }
            else
            {
                wapy.Checked = false;
                wapn.Checked = true;
            }
            #endregion

            WapPath.Text = dt.Rows[0]["WapPath"].ToString();
            WapDomain.Text = dt.Rows[0]["WapDomain"].ToString();
            WapLastNum.Text = dt.Rows[0]["WapLastNum"].ToString();
            #endregion
        }
        #endregion
    }

    /// <summary>
    /// 会员部分
    /// </summary>
    /// <returns>取得会员部分信息</returns>
    /// Code By ChenZhaohui

    void UserStartLoad()
    {
        #region 从会员设置表中读出数据并初始化赋值
        DataTable dt = sys.UserPram();
        if (dt.Rows.Count > 0)
        {
            #region 基本参数设置部分
            RegGroupNumber.Text = dt.Rows[0]["RegGroupNumber"].ToString();//会员组

            #region 投稿状态
            string constr = dt.Rows[0]["ConstrTF"].ToString();
            if (constr == "1")
            {
                IsConstrTF.Checked = true;
                NoConstrTF.Checked = false;
            }
            else
            {
                IsConstrTF.Checked = false;
                NoConstrTF.Checked = true;
            }
            #endregion

            #region 注册
            string regg = dt.Rows[0]["RegTF"].ToString();
            if (regg == "1")
            {
                RegYes.Checked = true;
                RegNo.Checked = false;
            }
            else
            {
                RegYes.Checked = false;
                RegNo.Checked = true;
            }
            #endregion
            #region 登陆验证
            string logincode = dt.Rows[0]["UserLoginCodeTF"].ToString();
            if (logincode == "1")
            {
                codeyes.Checked = true;
                codeno.Checked = false;
            }
            else
            {
                codeyes.Checked = false;
                codeno.Checked = true;
            }

            #endregion

            #region 评论验证
            string disv = dt.Rows[0]["CommCodeTF"].ToString();
            if (disv == "1")
            {
                dis.Checked = true;
                nodis.Checked = false;
            }
            else
            {
                dis.Checked = false;
                nodis.Checked = true;
            }

            string CommCheck = dt.Rows[0]["CommCheck"].ToString();
            if (CommCheck == "1")
            {
                CommCheck1.Checked = true;
                CommCheck0.Checked = false;
            }
            else
            {
                CommCheck1.Checked = false;
                CommCheck0.Checked = true;
            }
            #endregion

            #region 群发
            string send = dt.Rows[0]["SendMessageTF"].ToString();
            if (send == "1")
            {
                sendall.Checked = true;
                sendone.Checked = false;
            }
            else
            {
                sendall.Checked = false;
                sendone.Checked = true;
            }
            #endregion

            #region 匿名
            string nn = dt.Rows[0]["UnRegCommTF"].ToString();
            if (nn == "1")
            {
                yun.Checked = true;
                noyun.Checked = false;
            }
            else
            {
                yun.Checked = false;
                noyun.Checked = true;
            }
            #endregion

            #region HTML
            string htmll = dt.Rows[0]["CommHTMLLoad"].ToString();
            if (htmll == "1")
            {
                html.Checked = true;
                nohtml.Checked = false;
            }
            else
            {
                html.Checked = false;
                nohtml.Checked = true;
            }
            #endregion

            Commfiltrchar.Value = dt.Rows[0]["Commfiltrchar"].ToString();//过滤
            IPLimt.Value = dt.Rows[0]["IPLimt"].ToString();//IP
            GpointName.Text = dt.Rows[0]["GpointName"].ToString();//金币
            LoginLock.Text = dt.Rows[0]["LoginLock"].ToString();//锁定时间
            //LevelID.Text = dt.Rows[0]["LevelID"].ToString();//等级ID
            setPoint.Text = dt.Rows[0]["setPoint"].ToString();//注册获得的积分金币
            cPointParam.Text = dt.Rows[0]["cPointParam"].ToString();//魅力值增加
            aPointparam.Text = dt.Rows[0]["aPointparam"].ToString();//活跃值增加
            RegContent.Value = dt.Rows[0]["RegContent"].ToString();//注册协议
            #endregion

            #region 冲值类型
            string ghclassc = dt.Rows[0]["GhClass"].ToString();
            if (ghclassc == "1")
            {
                JiFen.Checked = true;
                GB.Checked = false;
            }
            else
            {
                GB.Checked = true;
                JiFen.Checked = false;
            }
            #endregion
        }
        #endregion

        #region 等级表中读数据并初始化赋值
        DataTable dtL = sys.UserLeavel();
        if (dtL.Rows.Count > 0)
        {
            #region 等级设置部分//读出每个设置的数出来
            LTitle_TextBox1.Text = dtL.Rows[0]["Ltitle"].ToString();//名称
            Lpicurl_TextBox1.Text = dtL.Rows[0]["Lpicurl"].ToString();//头像
            iPoint_TextBox1.Text = dtL.Rows[0]["iPoint"].ToString();//金币

            LTitle_TextBox2.Text = dtL.Rows[1]["Ltitle"].ToString();
            Lpicurl_TextBox2.Text = dtL.Rows[1]["Lpicurl"].ToString();
            iPoint_TextBox2.Text = dtL.Rows[1]["iPoint"].ToString();

            LTitle_TextBox3.Text = dtL.Rows[2]["Ltitle"].ToString();
            Lpicurl_TextBox3.Text = dtL.Rows[2]["Lpicurl"].ToString();
            iPoint_TextBox3.Text = dtL.Rows[2]["iPoint"].ToString();

            LTitle_TextBox4.Text = dtL.Rows[3]["Ltitle"].ToString();
            Lpicurl_TextBox4.Text = dtL.Rows[3]["Lpicurl"].ToString();
            iPoint_TextBox4.Text = dtL.Rows[3]["iPoint"].ToString();

            LTitle_TextBox5.Text = dtL.Rows[4]["Ltitle"].ToString();
            Lpicurl_TextBox5.Text = dtL.Rows[4]["Lpicurl"].ToString();
            iPoint_TextBox5.Text = dtL.Rows[4]["iPoint"].ToString();

            LTitle_TextBox6.Text = dtL.Rows[5]["Ltitle"].ToString();
            Lpicurl_TextBox6.Text = dtL.Rows[5]["Lpicurl"].ToString();
            iPoint_TextBox6.Text = dtL.Rows[5]["iPoint"].ToString();

            LTitle_TextBox7.Text = dtL.Rows[6]["Ltitle"].ToString();
            Lpicurl_TextBox7.Text = dtL.Rows[6]["Lpicurl"].ToString();
            iPoint_TextBox7.Text = dtL.Rows[6]["iPoint"].ToString();

            LTitle_TextBox8.Text = dtL.Rows[7]["Ltitle"].ToString();
            Lpicurl_TextBox8.Text = dtL.Rows[7]["Lpicurl"].ToString();
            iPoint_TextBox8.Text = dtL.Rows[7]["iPoint"].ToString();

            LTitle_TextBox9.Text = dtL.Rows[8]["Ltitle"].ToString();
            Lpicurl_TextBox9.Text = dtL.Rows[8]["Lpicurl"].ToString();
            iPoint_TextBox9.Text = dtL.Rows[8]["iPoint"].ToString();

            LTitle_TextBox10.Text = dtL.Rows[9]["Ltitle"].ToString();
            Lpicurl_TextBox10.Text = dtL.Rows[9]["Lpicurl"].ToString();
            iPoint_TextBox10.Text = dtL.Rows[9]["iPoint"].ToString();
            #endregion
        }
        #endregion
    }

    /// <summary>
    /// 水印部分
    /// </summary>
    /// <returns>取得水印部分信息</returns>
    /// Code By ChenZhaohui

    void WaterStartLoad()
    {
        #region 从水印设置表中读出数据并初始化赋值
        DataTable dt = sys.WaterStart();
        if (dt.Rows.Count > 0)
        {
            #region 是否开启水印/缩图
            string iswater = dt.Rows[0]["PrintTF"].ToString();
            if (iswater == "1")
            {
                WaterY.Checked = true;
                WaterN.Checked = false;
            }
            else
            {
                WaterY.Checked = false;
                WaterN.Checked = true;
            }
            #endregion

            PrintPicTF.Text = dt.Rows[0]["PrintPicTF"].ToString();
            PrintWord.Text = dt.Rows[0]["PrintWord"].ToString();
            Printfontsize.Text = dt.Rows[0]["Printfontsize"].ToString();
            Printfontfamily.Text = dt.Rows[0]["Printfontfamily"].ToString();
            Printfontcolor.Text = dt.Rows[0]["Printfontcolor"].ToString();
            PrintBTF.Text = dt.Rows[0]["PrintBTF"].ToString();
            PintPicURL.Text = dt.Rows[0]["PintPicURL"].ToString();
            if (!dt.Rows[0]["PrintPicsize"].ToString().Equals("100|100"))
            {
                PrintPicsize.Text = dt.Rows[0]["PrintPicsize"].ToString();
            }
            PintPictrans.Text = dt.Rows[0]["PintPictrans"].ToString();
            PrintPosition.Text = dt.Rows[0]["PrintPosition"].ToString();
            PrintSmallTF.Text = dt.Rows[0]["PrintSmallTF"].ToString();
            PrintSmallSizeStyle.Text = dt.Rows[0]["PrintSmallSizeStyle"].ToString();
            PrintSmallSize.Text = dt.Rows[0]["PrintSmallSize"].ToString();
            PrintSmallinv.Text = dt.Rows[0]["PrintSmallinv"].ToString();
        }
        #endregion
    }

    /// <summary>
    /// 基本属性保存事件
    /// </summary>
    /// <returns>基本属性保存事件</returns>
    /// Code By ChenZhaohui


    protected void SaveBaseInfo_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "Q002";
        this.CheckAdminAuthority(); 
        if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得基本参数设置添加中的表单信息
            su.Str_SiteName = Foosun.Common.Input.Filter(this.SiteName.Text.Trim());//站点名称
            su.Str_SiteDomain = Foosun.Common.Input.Filter(this.SiteDomain.Text.Trim());//站点域名
            su.Str_IndexTemplet = Foosun.Common.Input.Filter(this.IndexTemplet.Text.Trim());//首页摸板路径
            su.Str_Txt_IndexFileName = Foosun.Common.Input.Filter(this.IndexFileName.Text.Trim());//首页生成文件名
            su.Str_FileEXName = Foosun.Common.Input.Filter(this.FileEXName.Text.Trim());//默认的扩展名（主站）
            su.Str_ReadNewsTemplet = Foosun.Common.Input.Filter(this.ReadNewsTemplet.Text.Trim());//新闻浏览页摸板
            su.Str_ClassListTemplet = Foosun.Common.Input.Filter(this.ClassListTemplet.Text.Trim());//新闻栏目页摸板
            su.Str_SpecialTemplet = Foosun.Common.Input.Filter(this.SpecialTemplet.Text.Trim());//新闻专题页摸板

            #region 前台浏览方式
            su.readtypef = 0;
            int ReviewType = 0;
            if (DongTai.Checked)
            {
                su.readtypef = 1;
                ReviewType = 1;
            }
            if (JingTai.Checked)
            {
                su.readtypef = 0;
                ReviewType = 0;
            }
            #endregion

            su.Str_LoginTimeOut = Foosun.Common.Input.Filter(this.LoginTimeOut.Text.Trim());//后台登陆过期时间
            su.Str_Email = Foosun.Common.Input.Filter(this.Email.Text.Trim());//管理员信箱

            #region 站点采用路径方式
            int linkTypeConfig = 0;
            su.linktypef = 0;
            if (JueDui.Checked)
            {
                su.linktypef = 1;
                linkTypeConfig = 1;
            }
            if (XiangDui.Checked)
            {
                su.linktypef = 0;
                linkTypeConfig = 0;
            }
            #endregion

            su.Str_BaseCopyRight = Foosun.Common.Input.Filter(this.BaseCopyRight.Value.Trim());//版权信息
            su.Str_CheckInt = Foosun.Common.Input.Filter(this.CheckInt.Text.Trim());//新闻后台审核机制

            #region 图片防盗
            su.picc = 0;
            if (Yes.Checked)
            {
                su.picc = 1;
            }
            if (No.Checked)
            {
                su.picc = 0;
            }
            #endregion
            su.Str_LenSearch = Foosun.Common.Input.Filter(this.LenSearch.Text.Trim());//关键字长度
            if (Foosun.Common.Input.IsInteger(this.HistoryNum.Text) == false)
            {
                PageError("生成归档索引生成多少天前索引请填写数字", "sys_Param.aspx");
            }
            else
            {
                su.HistoryNum = int.Parse(this.HistoryNum.Text);
            }
            if (this.InsertPicPosition.Text.Length < 20)
            {
                if (this.InsertPicPosition.Text.IndexOf("|") > -1)
                {
                    su.InsertPicPosition = this.InsertPicPosition.Text;
                }
                else
                {
                    PageError("插入画中画位置格式不正确，格式：插入的位置|right或left", "sys_Param.aspx");
                }
            }
            else
            {
                PageError("插入画中画位置，长度太长，格式：插入的位置|right或left", "sys_Param.aspx");
            }
            #region 检测栏目标题
            su.chetitle = 0;
            if (checktitle.Checked)
            {
                su.chetitle = 1;
            }
            if (nochecktitle.Checked)
            {
                su.chetitle = 0;
            }
            #endregion

            #region 防采集
            su.collect = 0;
            int collectTF = 0;
            if (Ischeck.Checked)
            {
                su.collect = 1;
                collectTF = 1;
            }
            if (nocheck.Checked)
            {
                su.collect = 0;
                collectTF = 0;
            }
            #endregion

            su.Str_SaveClassFilePath = Foosun.Common.Input.Filter(this.SaveClassFilePath.Text.Trim());//生成栏目文件保存路径
            string TmpIndexPage = this.SaveIndexPage.Text.Trim();
            if (TmpIndexPage.ToLower().IndexOf("{@year") == -1 || TmpIndexPage.ToLower().IndexOf("{@month}") == -1 || TmpIndexPage.ToLower().IndexOf("{@day}") == -1)
            {
                PageError("索引页面规则必须包含{@month}、{@day}及{@year04}或{@year02}", "sys_Param.aspx");
            }
            su.Str_SaveIndexPage = Foosun.Common.Input.Filter(this.SaveIndexPage.Text.Trim());//生成索引页规则
            su.Str_SaveNewsFilePath = Foosun.Common.Input.Filter(this.SaveNewsFilePath.Text.Trim());//新闻文件命名规则
            su.Str_SaveNewsDirPath = Foosun.Common.Input.Filter(this.SaveNewsDirPath.Text.Trim());//新闻文件保存路径
            su.Str_Pram_Index = Foosun.Common.Input.Filter(this.Pram_Index.Text.Trim());//每页显示数量
            #endregion

            #region 判断
            if (su.Str_SiteName == null || su.Str_SiteName == "" || su.Str_SiteName == string.Empty)
            {
                PageError("抱歉，当前基本属性设置中站点名称为空，请填写名称!", "sys_Param.aspx");
            }
            if (su.Str_SiteDomain == null || su.Str_SiteDomain == "" || su.Str_SiteDomain == string.Empty)
            {
                PageError("抱歉，当前基本属性设置中站点域名为空，请填写域名!", "sys_Param.aspx");
            }
            #endregion

            #region 发布方式
            bool publishState = true;
            if (this.radPublishOne.Checked != true)
            {
                publishState = false;
            }
            #endregion

            #region 设置内容分页样式

            string _PageStyle = this.NewsPageStyle.Value;//新闻内容分页样式
            string _PageLinkCount = this.PageLinkCount.Text;//分页显示链接个数

            #endregion

            #region 设置是否开启内容分页
            string isAutoPageSplit = "true";
            if (!this.RadioButton_autoPageSplit1.Checked)
            {
                isAutoPageSplit = "false";
            }
            Foosun.Common.Public.SaveXmlConfig("enableAutoPage", isAutoPageSplit, "xml/sys/foosun.config");

            //设置字数
            string txt_pageSplitCount = this.txt_pageSplitCount.Text;
            Foosun.Common.Public.SaveXmlConfig("splitPageCount", txt_pageSplitCount, "xml/sys/foosun.config");
            //刷新
            Foosun.Config.UIConfig.RefurbishCatch();
            #endregion 

            #region 载入数据-刷新页面
            if (sys.Update_BaseInfo(su) != 0)
            {
                if (Foosun.Common.Public.saveparamconfig(this.SiteName.Text.Trim(), this.SiteDomain.Text.Trim(), linkTypeConfig, ReviewType, this.IndexTemplet.Text.Trim(), this.IndexFileName.Text.Trim(), this.LenSearch.Text.Trim(), this.SaveIndexPage.Text.Trim(), this.InsertPicPosition.Text, collectTF.ToString(), this.HistoryNum.Text, publishState, _PageStyle, _PageLinkCount) == false)
                {
                    PageError("参数保存成功。<li>但更新缓存失败，请检查您的/xml/sys/base.config是否具有可写权限。</li>", "sys_Param.aspx");
                }
                rp.SaveUserAdminLogs(1, 1, UserNum, "保存基本参数设置", "基本参数设置操作成功！");
                PageRight("基本参数设置成功", "sys_Param.aspx");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "sys_Param.aspx");
            }
            #endregion

            
        }
    }

    /// <summary>
    /// 会员部分保存事件
    /// </summary>
    /// <returns>会员部分保存事件</returns>
    /// Code By ChenZhaohui

    protected void SaveUser_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "Q003";
        this.CheckAdminAuthority();
        if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得会员参数设置添加中的表单信息
            su.Str_RegGroupNumber = this.RegGroupNumber.SelectedValue;//默认会员组

            #region 投稿状态
            su.constrr = 0;
            if (IsConstrTF.Checked)
            {
                su.constrr = 1;
            }
            if (NoConstrTF.Checked)
            {
                su.constrr = 0;
            }
            #endregion

            #region 注册
            su.reg = 0;
            if (RegYes.Checked)
            {
                su.reg = 1;
            }
            if (RegNo.Checked)
            {
                su.reg = 0;
            }
            #endregion

            #region 验证码
            su.code = 0;
            if (codeyes.Checked)
            {
                su.code = 1;
            }
            if (codeno.Checked)
            {
                su.code = 0;
            }
            #endregion

            #region 评论验证
            su.diss = 0;
            if (dis.Checked)
            {
                su.diss = 1;
            }
            if (nodis.Checked)
            {
                su.diss = 0;
            }
            #endregion
            su.CommCheck = 0;
            if (CommCheck1.Checked)
            {
                su.CommCheck = 1;
            }
            if (CommCheck0.Checked)
            {
                su.CommCheck = 0;
            }

            #region 群发
            su.senemessage = 0;
            if (sendall.Checked)
            {
                su.senemessage = 1;
            }
            if (sendone.Checked)
            {
                su.senemessage = 0;
            }
            #endregion

            #region 匿名
            su.n = 0;
            if (yun.Checked)
            {
                su.n = 1;
            }
            if (noyun.Checked)
            {
                su.n = 0;
            }
            #endregion

            #region html编辑器
            su.htmls = 0;
            if (html.Checked)
            {
                su.htmls = 1;
            }
            if (nohtml.Checked)
            {
                su.htmls = 0;
            }
            #endregion

            su.Str_Commfiltrchar = this.Commfiltrchar.Value.Trim() + "";//评论过滤
            //string Str_IPLimt = Foosun.Common.Input.Filter(this.IPLimt.Value.Trim());//IP
            su.Str_IPLimt = Request.Form["IPLimt"]+"";
            su.Str_GpointName = Foosun.Common.Input.Filter(this.GpointName.Text.Trim());//G币
            su.Str_LoginLock = Foosun.Common.Input.Filter(this.LoginLock.Text.Trim());//锁定
            su.Str_setPoint = Foosun.Common.Input.Filter(this.setPoint.Text.Trim());//注册获得的积分金币
            su.Str_cPointParam = Foosun.Common.Input.Filter(this.cPointParam.Text.Trim());//魅力值增加
            su.Str_aPointparam = Foosun.Common.Input.Filter(this.aPointparam.Text.Trim());//活跃值增加
            su.Str_RegContent = Foosun.Common.Input.Filter(this.RegContent.Value.Trim());//注册协议

            #region 会员注册参数
            string _tmpSTR = this.regitemContent.Text;//,UserPassword
            if (_tmpSTR.IndexOf("UserName") == -1 || _tmpSTR.IndexOf("UserPassword") == -1 || _tmpSTR.IndexOf("email") == -1)
            {
                PageError("注册项目中，用户名、密码及电子邮件必须选择!", "");
            }
            if (this.returnmobile.SelectedValue == "1" && this.returnemail.SelectedValue == "1")
            {
                PageError("电子邮件认证和手机认证只能选择一个。", "");
            }

            if (this.returnmobile.SelectedValue == "1")
            {
                if (_tmpSTR.IndexOf("Mobile") == -1)
                {
                    PageError("开启了手机验证.注册项目中的手机必须选择!", "");
                }
            }
            su.strContent = _tmpSTR;
            su.returnemail = int.Parse(this.returnemail.SelectedValue);
            su.returnmobile = int.Parse(this.returnmobile.SelectedValue);
            #endregion

            #region 冲值类型
            su.ghclass = 0;
            if (JiFen.Checked)
            {
                su.ghclass = 1;
            }
            if (GB.Checked)
            {
                su.ghclass = 0;
            }
            #endregion

            #endregion

            #region 等级设置

            su.Str_LtitleArr = new string[11];//存取名称数组
            su.Str_LpicurlArr = new string[11];//存取头像数组
            su.Str_iPointArr = new string[11];//存取金币数组

            #endregion

            for (int k = 1; k <= 10; k++)
            {
                su.Str_LtitleArr[k] = Request.Form["LTitle_TextBox" + k];//取得表单中的名称信息
                su.Str_LpicurlArr[k] = Request.Form["Lpicurl_TextBox" + k];//取得表单中的头像信息
                su.Str_iPointArr[k] = Request.Form["iPoint_TextBox" + k];//取得表单中的金币信息
                sys.Update_Leavel(su, k);
            }
            #region 向数据库中写入添加的会员参数设置信息
            int userpram = sys.Update_UserInfo(su);
            #endregion

            #region 会员注册参数SQL语句
            int ureg = sys.Update_UserRegInfo(su);
            #endregion

            #region 载入数据-刷新页面
            if (userpram != 0 && ureg != 0)
            {
                rp.SaveUserAdminLogs(1, 1, UserNum, "保存会员参数设置", "会员参数设置操作成功！");
                PageRight("会员参数设置成功", "sys_Param.aspx");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }
            #endregion
        }
    }

    /// <summary>
    /// 上传，分组刷新保存事件
    /// </summary>
    /// <returns>上传，分组刷新保存事件</returns>
    /// Code By ChenZhaohui

    protected void SaveUpload_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "Q004";
        this.CheckAdminAuthority();
        if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得上传设置添加中的表单信息
            #region 图片路径域名
            su.picsa = 0;
            if (picy.Checked)
            {
                su.picsa = 1;
            }
            if (picn.Checked)
            {
                su.picsa = 0;
            }
            #endregion

            su.Str_PicServerDomain = Foosun.Common.Input.Filter(this.PicServerDomain.Text.Trim());//域名
            su.Str_PicUpLoad = Foosun.Common.Input.Filter(this.PicUpload.Text.Trim());//图片附件目录
            su.Str_UpfilesType = Foosun.Common.Input.Filter(this.UpfilesType.Text.Trim());//上传格式
            su.Str_UpFilesSize = Foosun.Common.Input.Filter(this.UpFilesSize.Text.Trim());//上传大小

            #region 域名
            su.domainnn = 0;
            if (domainr.Checked)
            {
                su.domainnn = 1;
            }
            if (domainn.Checked)
            {
                su.domainnn = 0;
            }
            #endregion

            su.Str_RemoteDomain = Foosun.Common.Input.Filter(this.RemoteDomain.Text.Trim());//远程图片域名
            su.Str_RemoteSavePath = Foosun.Common.Input.Filter(this.RemoteSavePath.Text.Trim());//远程图片保存路径
            #endregion

            #region 分组刷新
            su.Str_ClassListNum = Foosun.Common.Input.Filter(this.ClassListNum.Text.Trim());//列表每次刷新数
            su.Str_NewsNum = Foosun.Common.Input.Filter(this.NewsNum.Text.Trim());//信息每次刷新数
            su.Str_BatDelNum = Foosun.Common.Input.Filter(this.BatDelNum.Text.Trim());//批量删除数
            su.Str_SpecialNum = Foosun.Common.Input.Filter(this.SpecialNum.Text.Trim());//专题每次刷新数
            #endregion

            #region 向数据库中写入添加的基本参数设置信息

            int ftpf = sys.Update_FtpInfo(su);
            #endregion

            #region 载入数据-刷新页面
            if (ftpf != 0)
            {
                if (Foosun.Common.Public.saveRefreshConfig(this.ClassListNum.Text.Trim(), this.NewsNum.Text.Trim(), this.BatDelNum.Text.Trim(), this.SpecialNum.Text.Trim()) == false)
                {
                    PageError("上传、分组刷新参数设置成功。<li>但更新缓存失败，请检查您的/xml/sys/refresh.config是否具有可写权限。</li>", "sys_Param.aspx");
                }
                rp.SaveUserAdminLogs(1, 1, UserNum, "保存上传，分组刷新设置", "上传，分组刷新设置操作成功！");
                PageRight("上传、分组刷新参数设置成功", "sys_Param.aspx");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }
            #endregion
        }
    }

    /// <summary>
    /// JS,FTP保存事件
    /// </summary>
    /// <returns>JS,FTP保存事件</returns>
    /// Code By ChenZhaohui

    protected void SaveJs_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "Q005";
        this.CheckAdminAuthority();
        #region 取得JS,FTP设置添加中的表单信息
        string Str_JsNews1 = Foosun.Common.Input.Filter(this.JsNews1.Text.Trim());
        string Str_JsTitle1 = Foosun.Common.Input.Filter(this.JsTitle1.Text.Trim());
        string Str_JsModel1 = this.JsModel1.SelectedValue;
        su.Str_HotJS = "" + Str_JsNews1 + "|" + Str_JsTitle1 + "|" + Str_JsModel1 + "";

        string Str_JsNews2 = Foosun.Common.Input.Filter(this.JsNews2.Text.Trim());
        string Str_JsTitle2 = Foosun.Common.Input.Filter(this.JsTitle2.Text.Trim());
        string Str_JsModel2 = this.JsModel2.SelectedValue;
        su.Str_LastJS = "" + Str_JsNews2 + "|" + Str_JsTitle2 + "|" + Str_JsModel2 + "";

        string Str_JsNews3 = Foosun.Common.Input.Filter(this.JsNews3.Text.Trim());
        string Str_JsTitle3 = Foosun.Common.Input.Filter(this.JsTitle3.Text.Trim());
        string Str_JsModel3 = this.JsModel3.SelectedValue;
        su.Str_RecJS = "" + Str_JsNews3 + "|" + Str_JsTitle3 + "|" + Str_JsModel3 + "";

        string Str_JsNews4 = Foosun.Common.Input.Filter(this.JsNews4.Text.Trim());
        string Str_JsTitle4 = Foosun.Common.Input.Filter(this.JsTitle4.Text.Trim());
        string Str_JsModel4 = this.JsModel4.SelectedValue;
        su.Str_HoMJS = "" + Str_JsNews4 + "|" + Str_JsTitle4 + "|" + Str_JsModel4 + "";

        string Str_JsNews5 = Foosun.Common.Input.Filter(this.JsNews5.Text.Trim());
        string Str_JsTitle5 = Foosun.Common.Input.Filter(this.JsTitle5.Text.Trim());
        string Str_JsModel5 = this.JsModel5.SelectedValue;
        su.Str_TMJS = "" + Str_JsNews5 + "|" + Str_JsTitle5 + "|" + Str_JsModel5 + "";

        #region FTp
        #region ftp
        su.ftpp = 0;
        if (ftpy.Checked)
        {
            su.ftpp = 1;
        }
        if (ftpn.Checked)
        {
            su.ftpp = 0;
        }
        #endregion
        su.Str_FTPIP = Foosun.Common.Input.Filter(this.FTPIP.Text.Trim());
        su.Str_Ftpport = Foosun.Common.Input.Filter(this.Ftpport.Text.Trim());
        su.Str_FtpUserName = Foosun.Common.Input.Filter(this.FtpUserName.Text.Trim());
        su.Str_FTPPASSword = Foosun.Common.Input.Filter(Foosun.Common.Input.EncryptString(this.FTPPASSword.Text.Trim()));//字符串加密方式写入数据库
        #endregion
        #endregion

        #region 执行数据-js
        int jsj = sys.Update_JS(su);
        #endregion

        #region 执行数据-ftp
        int ftpp = sys.Update_JFtP(su);
        #endregion


        #region 载入数据-刷新页面
        if (jsj != 0 && ftpp != 0)
        {
            rp.SaveUserAdminLogs(1, 1, UserNum, "FTP，JS参数设置", "FTP，JS设置操作成功！");
            PageRight("JS，FTP参数设置成功", "sys_Param.aspx");
        }
        else
        {
            PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
        }
        #endregion
    }

    /// <summary>
    /// 水印保存事件
    /// </summary>
    /// <returns>水印保存事件</returns>
    /// Code By ChenZhaohui

    protected void Savewater_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "Q006";
        this.CheckAdminAuthority();
        if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得水印设置添加中的表单信息
            #region 是否开启水印/缩图
            su.water = 0;
            if (WaterY.Checked)
            {
                su.water = 1;
            }
            if (WaterN.Checked)
            {
                su.water = 0;
            }
            #endregion

            su.Str_PrintPicTF = Foosun.Common.Input.Filter(this.PrintPicTF.Text.Trim());//类型
            su.Str_PrintWord = Foosun.Common.Input.Filter(this.PrintWord.Text.Trim());//文字水印
            su.Str_Printfontsize = Foosun.Common.Input.Filter(this.Printfontsize.Text.Trim());//字体大小
            su.Str_Printfontfamily = Foosun.Common.Input.Filter(this.Printfontfamily.Text.Trim());//字体
            su.Str_Printfontcolor = Foosun.Common.Input.Filter(this.Printfontcolor.Text.Trim());//水印颜色
            su.Str_PrintBTF = Foosun.Common.Input.Filter(this.PrintBTF.Text.Trim());//文字是否加粗
            su.Str_PintPicURL = Foosun.Common.Input.Filter(this.PintPicURL.Text.Trim());//图片水印路径
            su.Str_PrintPicsize = Foosun.Common.Input.Filter(this.PrintPicsize.Text.Trim());//图片水印大小
            su.Str_PintPictrans = Foosun.Common.Input.Filter(this.PintPictrans.Text.Trim());//透明度
            su.Str_PrintPosition = Foosun.Common.Input.Filter(this.PrintPosition.Text.Trim());//位置
            su.Str_PrintSmallTF = Foosun.Common.Input.Filter(this.PrintSmallTF.Text.Trim());//是否开启缩图
            su.Str_PrintSmallSizeStyle = Foosun.Common.Input.Filter(this.PrintSmallSizeStyle.Text.Trim());//缩图方式
            su.Str_PrintSmallSize = Foosun.Common.Input.Filter(this.PrintSmallSize.Text.Trim());//缩图大小
            su.Str_PrintSmallinv = Foosun.Common.Input.Filter(this.PrintSmallinv.Text.Trim());//缩图比例

            #endregion

            #region 向数据库中写入添加的水印参数设置信息

            int watr = sys.Update_Water(su);
            #endregion

            #region 载入数据-刷新页面
            if (watr != 0)
            {
                rp.SaveUserAdminLogs(1, 1, UserNum, "水印/缩图参数设置", "水印/缩图设置操作成功！");
                PageRight("水印/缩图参数设置成功", "sys_Param.aspx");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }
            #endregion
        }
    }

    /// <summary>
    /// RSS保存事件
    /// </summary>
    /// <returns>RSS保存事件</returns>
    /// Code By ChenZhaohui

    protected void Saverss_ServerClick(object sender, EventArgs e)
    {
        this.Authority_Code = "Q007";
        this.CheckAdminAuthority();
        if (Page.IsValid)//判断页面是否通过验证
        {
            #region 取得设置添加中的表单信息
            string wapdomains = "无";
            if (this.WapDomain.Text.Trim() != "")
            {
                wapdomains = this.WapDomain.Text.Trim();
            }
            string WapPaths = "/xml/wap";
            if (this.WapPath.Text.Trim() != "")
            {
                WapPaths = this.WapPath.Text.Trim();
            }
            string WapLastNums = "10";
            if (this.WapLastNum.Text.Trim() != "")
            {
                WapLastNums = this.WapLastNum.Text.Trim();
            }
            su.Str_RssNum = Foosun.Common.Input.Filter(this.RssNum.Text.Trim());//显示范围
            su.Str_RssContentNum = Foosun.Common.Input.Filter(this.RssContentNum.Text.Trim());//截取数
            su.Str_RssTitle = Foosun.Common.Input.Filter(this.RssTitle.Text.Trim());//标题
            su.Str_RssPicURL = Foosun.Common.Input.Filter(this.RssPicURL.Text.Trim());//地址

            #region 加入WAP
            su.wapp = 0;
            if (wapy.Checked)
            {
                su.wapp = 1;
            }
            if (wapn.Checked)
            {
                su.wapp = 0;
            }
            #endregion

            su.Str_WapPath = Foosun.Common.Input.Filter(WapPaths);//WAP路径
            su.Str_WapDomain = Foosun.Common.Input.Filter(wapdomains);//WAP域名
            su.Str_WapLastNum = Foosun.Common.Input.Filter(WapLastNums);//WAP数

            #endregion
            #region 向数据库中写入添加的RSS参数设置信息

            int rswa = sys.Update_RssWap(su);
            #endregion

            #region 载入数据-刷新页面
            if (rswa != 0)
            {
                rp.SaveUserAdminLogs(1, 1, UserNum, "RSS/XML/WAP参数设置", "RSS/XML/WAP设置操作成功！");
                PageRight("RSS/XML/WAP参数设置成功", "sys_Param.aspx");
            }
            else
            {
                PageError("意外错误：未知错误<br />", "shortcut_list.aspx");
            }
            #endregion
        }
    }

    /// <summary>
    /// 调用函数控制选择按钮选则是的时候其相应的框架是否显示出来
    /// </summary>
    /// <returns>RSS保存事件</returns>
    /// Code By ChenZhaohui

    #region 调用函数控制选择按钮选则是的时候其相应的框架是否显示出来
    protected void isshow()
    {
        DataTable dt = sys.ShowJS1();
        DataTable dt1 = sys.ShoeJs2();
        DataTable dt2 = sys.showJs3();
        #region 传递值
        if (dt.Rows.Count > 0 || dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
        {
            Response.Write("<script language=\"javascript\">SelectOpPic0('" + dt.Rows[0]["PicServerTF"].ToString() + "');</script>");
            Response.Write("<script language=\"javascript\">SelectOpPic1('" + dt.Rows[0]["ReMoteDomainTF"].ToString() + "');</script>");

            Response.Write("<script language=\"javascript\">SelectOpPic2('" + dt1.Rows[0]["FtpTF"].ToString() + "');</script>");
            Response.Write("<script language=\"javascript\">SelectOpPic3('" + dt1.Rows[0]["WapTF"].ToString() + "');</script>");

            Response.Write("<script language=\"javascript\">SelectOpPic('" + dt2.Rows[0]["PrintPicTF"].ToString() + "');</script>");
            Response.Write("<script language=\"javascript\">SelectOpPic('" + dt2.Rows[0]["PrintSmallTF"].ToString() + "');</script>");
            Response.Write("<script language=\"javascript\">SelectOpPic('" + dt2.Rows[0]["PrintSmallSizeStyle"].ToString() + "');</script>");
        }
        #endregion
    }
    #endregion

    void Model_Templet()
    {
        #region 模型
        try
        {
            DataTable dt1 = sys.JsTemplet1();
            this.JsModel1.DataTextField = "jsTName";
            this.JsModel1.DataValueField = "JsID";
            this.JsModel1.DataSource = dt1;
            this.JsModel1.DataBind();
        }
        catch { }
        try
        {
            DataTable dt2 = sys.JsTemplet2();
            this.JsModel2.DataTextField = "jsTName";
            this.JsModel2.DataValueField = "JsID";
            this.JsModel2.DataSource = dt2;
            this.JsModel2.DataBind();
        }
        catch { }
        try
        {
            DataTable dt3 = sys.JsTemplet3();
            this.JsModel3.DataTextField = "jsTName";
            this.JsModel3.DataValueField = "JsID";
            this.JsModel3.DataSource = dt3;
            this.JsModel3.DataBind();
        }
        catch { }

        try
        {
            DataTable dt4 = sys.JsTemplet3();
            this.JsModel4.DataTextField = "jsTName";
            this.JsModel4.DataValueField = "JsID";
            this.JsModel4.DataSource = dt4;
            this.JsModel4.DataBind();
        }
        catch { }

        try
        {
            DataTable dt5 = sys.JsTemplet5();
            this.JsModel5.DataTextField = "jsTName";
            this.JsModel5.DataValueField = "JsID";
            this.JsModel5.DataSource = dt5;
            this.JsModel5.DataBind();
        }
        catch { }
    }
        #endregion
}
