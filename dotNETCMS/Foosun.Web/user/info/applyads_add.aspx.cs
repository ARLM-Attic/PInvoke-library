///************************************************************************************************************
///**********添加广告Code By DengXi****************************************************************************
///************************************************************************************************************
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
using Foosun.CMS.Common;

public partial class user_info_applyads_add : Foosun.Web.UI.UserPage
{
    public DataTable TbClass;
    rootPublic rd = new rootPublic();
    public string str_dirMana = Foosun.Config.UIConfig.dirDumm;//获取用户虚拟路径
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.CacheControl = "no-cache";                        //设置页面无缓存
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;        //获取版权信息
            ClassID.Items.Clear();
            getClassInfo();
            getAdsList();
        }
        string Type = Request.QueryString["Type"];
        if (Type == "Add")
        {
            AdsAdd();
        }
    }

    /// <summary>
    /// 调用获得分类信息
    /// </summary>
    /// <returns>调用获得分类信息</returns>
    /// Code By DengXi

    protected void getClassInfo()
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        TbClass = ac.getAdsClassList();
        if (TbClass != null)
            ClassRender("0", 0);
        TbClass.Clear();
        TbClass.Dispose();
    }

    /// <summary>
    /// 取得广告列表
    /// </summary>
    /// <returns>取得广告列表</returns>
    /// Code By DengXi

    protected void getAdsList()
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        CycID.Items.Clear();
        TbClass = ac.getAdsList(null);
        if (TbClass != null)
        {
            for (int i = 0; i < TbClass.Rows.Count; i++)
            {
                ListItem it = new ListItem();
                it.Text = TbClass.Rows[i][1].ToString();
                it.Value = TbClass.Rows[i][0].ToString();
                CycID.Items.Add(it);
            }
        }
        TbClass.Clear();
        TbClass.Dispose();
    }

    /// <summary>
    /// 获得分类信息开始(递归)
    /// </summary>
    /// <param name="PID">父类编号</param>
    /// <param name="Layer">第几层</param>
    /// <returns>取得广告列表</returns>
    /// Code By DengXi


    private void ClassRender(string PID, int Layer)
    {
        DataRow[] row = TbClass.Select("ParentID='" + PID + "'");
        if (row.Length < 1)
            return;
        else
        {
            foreach (DataRow r in row)
            {
                ListItem it = new ListItem();
                it.Value = r["AcID"].ToString();
                string stxt = "┝";
                for (int i = 0; i < Layer; i++)
                {
                    stxt += "┉";
                }
                it.Text = stxt + r["Cname"].ToString() + "[需要" + rd.getgPointName() +":" + r["Adprice"].ToString() + "]";
                this.ClassID.Items.Add(it);
                ClassRender(r["AcID"].ToString(), Layer + 1);
            }
        }
    }

    /// <summary>
    /// 添加广告信息
    /// </summary>
    /// <returns>添加广告信息</returns>
    /// Code By DengXi

    protected void AdsAdd()
    {
        Foosun.Model.AdsInfo ai = new Foosun.Model.AdsInfo();
        ai.AdID = "";
        ai.adName = Request.Form["adName"];

        ai.ClassID = Request.Form["ClassID"];

        int int_classadsprice = classAdprice(ai.ClassID);
        int int_UserG = userG();
        if (int_UserG < int_classadsprice)
            PageError("您当前的金币数不足,请冲值后申请此广告.", "");

        ai.adType = int.Parse(Request.Form["adType"]);
        ai.leftPic = Request.Form["leftPic"];
        ai.leftSize = Request.Form["leftSize"];
        ai.rightPic = Request.Form["rightPic"];
        ai.rightSize = Request.Form["rightSize"];
        ai.LinkURL = Request.Form["LinkURL"];

        if (int.Parse(Request.QueryString["CycTF"]) == 1)
        {
            ai.CycTF = 1;
            ai.CycAdID = Request.Form["CycID"];
            ai.CycSpeed = int.Parse(Request.Form["CycSpeed"]);
            ai.CycDic = int.Parse(Request.Form["CycDic"]);
        }
        else
        {
            ai.CycTF = 0;
            ai.CycAdID = "-1";
            ai.CycSpeed = 0;
            ai.CycDic = 0;
        }

        ai.CusID = Foosun.Global.Current.UserNum;
        ai.AdTxtContent = Request.Form["AdTxtContent"];
        ai.AdTxtCss = Request.Form["AdTxtCss"];
        ai.AdTxtLink = Request.Form["AdTxtLink"];
        if (Request.Form["AdTxtColNum"] != null && Request.Form["AdTxtColNum"] != "" && Request.Form["AdTxtColNum"] != string.Empty)
            ai.AdTxtNum = int.Parse(Request.Form["AdTxtColNum"]);
        else
            ai.AdTxtNum = 0;

        ai.isLock = 1;
        ai.CondiTF = 0;
        ai.maxShowClick = 0;
        ai.TimeOutDay = DateTime.Parse("3000-1-1");
        ai.maxClick = 0;
        ai.creatTime = DateTime.Now;
        ai.SiteID = Foosun.Global.Current.SiteID;

        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        int resutl = ac.adsAdd(ai);
        if (resutl == 1)
        {
            DelUserG(int_classadsprice);
            PageRight("申请广告成功!", "applyads.aspx");
        }
        else
            PageError("申请广告失败!", "");
        
    }


    /// <summary>
    /// 获取广告栏目金币数
    /// </summary>
    /// <param name="classID"></param>
    /// Code By DengXi

    protected int classAdprice(string classID)
    {
        int int_Adprice = 0;
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getClassAdprice(classID);
        if (dt != null)
        {
            int_Adprice = int.Parse(dt.Rows[0][0].ToString());
            dt.Clear();
            dt.Dispose();
        }
        return int_Adprice;
    }

    /// <summary>
    /// 获取用户金币数
    /// </summary>
    /// <returns></returns>
    /// Code By DengXi

    protected int userG()
    {
        int int_G = 0;
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        DataTable dt = ac.getuserG();
        if (dt != null)
        {
            int_G = int.Parse(dt.Rows[0][0].ToString());
            dt.Clear();
            dt.Dispose();
        }
        return int_G;
    }

    /// <summary>
    /// 删除用户金币
    /// <param name="Gnum">金币数量</param>
    /// </summary>

    protected void DelUserG(int Gnum)
    {
        Foosun.CMS.Ads.Ads ac = new Foosun.CMS.Ads.Ads();
        ac.DelUserG(Gnum);
    }
}
