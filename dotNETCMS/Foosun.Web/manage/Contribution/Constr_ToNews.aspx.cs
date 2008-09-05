//=====================================================================
//==                  (C)2007 Foosun Inc.By doNetCMS1.0              ==
//==                     Forum:bbs.foosun.net                        ==
//==                     WebSite:www.foosun.net                      ==
//==                 Address:No.109 HuiMin ST,.ChengDu,China         ==
//==                   Tel:86-28-85098980/66026180                   ==
//==                   QQ:655071,MSN:ikoolls@gmail.com               ==
//==                   Email:Service@foosun.cn                       ==
//==                      Code By WangZhenjiang                      ==
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
using Foosun.CMS.Common;

public partial class manage_Contribution_Constr_ToNews : Foosun.Web.UI.ManagePage
{
    Constr con = new Constr();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            string ConIDs = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
            DataTable dtx = con.sel11(ConIDs);
            int isCheckss = int.Parse(dtx.Rows[0]["isCheck"].ToString());
            if (isCheckss == 1)
            {
                PageError("对不起此稿件已经审核不能再审核", "");
            }
            this.Title.Text = dtx.Rows[0]["Title"].ToString();
            DataTable dts1 = con.sel12();
            this.ParmConstr.DataSource = dts1;
            this.ParmConstr.DataTextField = "ConstrPayName";
            this.ParmConstr.DataValueField = "PCId";
            this.ParmConstr.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        rootPublic rd = new rootPublic();
        string ConIDp = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
        DataTable dt = con.sel13(ConIDp);
        //获得原始稿件数据
        string Title = dt.Rows[0]["Title"].ToString();
        string Contents = dt.Rows[0]["Content"].ToString();
        string Source = dt.Rows[0]["Source"].ToString();
        string Author = dt.Rows[0]["Author"].ToString();
        string PicURL = dt.Rows[0]["PicURL"].ToString();
        string site = dt.Rows[0]["SiteID"].ToString();
        string UNum = dt.Rows[0]["UserNum"].ToString();
        string creatTime = dt.Rows[0]["creatTime"].ToString();
        string Tags = dt.Rows[0]["Tags"].ToString();
        //获得选择的栏目
        //string ClassID = Foosun.Common.Input.Filter(Request.Form["ClassCName"].ToString());
        string ClassID = Foosun.Common.Input.Filter(Request.Form["ClassCName"].Split(',')[0]);
        //获得栏目的数据表
        string DataLib = con.sel18(ClassID);
        string NewsTemplet = "/{@dirTemplet}/Content/news.html";
        string strSavePath = "{@year04}-{@month}-{@day}";
        string strfileName = "constr-" + Foosun.Common.Rand.Number(5) + "";
        string strfileexName = ".html";
        string strCheckInt = "0|0|0|0";
        DataTable dts = con.getClassInfo(ClassID);
        if (dts != null)
        {
            if (dts.Rows.Count > 0)
            {
                NewsTemplet = dts.Rows[0]["ReadNewsTemplet"].ToString();
                strSavePath = dts.Rows[0]["NewsSavePath"].ToString();
                strfileName = dts.Rows[0]["NewsFileRule"].ToString();
                strfileexName = dts.Rows[0]["FileName"].ToString();
                if (dts.Rows[0]["CheckInt"].ToString() == "1") { strCheckInt = "1|1|0|0"; }
                else if (dts.Rows[0]["CheckInt"].ToString() == "2") { strCheckInt = "2|1|1|0"; }
                else if (dts.Rows[0]["CheckInt"].ToString() == "3") { strCheckInt = "3|1|1|1"; }
            }
            dts.Clear(); dts.Dispose();
        }
        DateTime getDateTime = DateTime.Now;
        strSavePath = rd.getResultPage(strSavePath, getDateTime, ClassID, "");
        strfileName = rd.getResultPage(strfileName, getDateTime, ClassID, "");
        //获得新闻表
        string NewsID = Foosun.Common.Rand.Number(12);
        DateTime CreatTime1 = DateTime.Now;
        //获得稿酬编号
        string PCIdsa = this.ParmConstr.SelectedValue;
        if (PCIdsa.Trim() == "" && PCIdsa.Trim() == null)
        {
            PageError("选择稿酬分类", "");
        }
        DataTable dt_ParmConstr = con.sel14(PCIdsa);
        int gPoint = 0;
        int iPoint = 0;
        int Money1 = 0;
        if (dt_ParmConstr != null)
        {
            if (dt_ParmConstr.Rows.Count > 0)
            {
                gPoint = int.Parse(dt_ParmConstr.Rows[0]["gPoint"].ToString());
                iPoint = int.Parse(dt_ParmConstr.Rows[0]["iPoint"].ToString());
                Money1 = int.Parse(dt_ParmConstr.Rows[0]["money"].ToString());
            }
            dt_ParmConstr.Clear(); dt_ParmConstr.Dispose();
        }

        string content4 = "稿酬";
        //获得用户的积分，点数，G币

        DataTable dt_User = con.sel15(UNum);
        int gPoint1 = int.Parse(dt_User.Rows[0]["gPoint"].ToString());
        int iPoint1 = int.Parse(dt_User.Rows[0]["iPoint"].ToString());
        int cPoint = int.Parse(dt_User.Rows[0]["cPoint"].ToString());
        int aPoint = int.Parse(dt_User.Rows[0]["aPoint"].ToString());
        int Money2 = int.Parse(dt_User.Rows[0]["ParmConstrNum"].ToString());

        DataTable dt4 = con.sel16();
        string[] cPointParam = dt4.Rows[0]["cPointParam"].ToString().Split('|');
        string[] aPointparam = dt4.Rows[0]["aPointparam"].ToString().Split('|');
        int cPoint1 = int.Parse(cPointParam[1]);
        int aPoint1 = int.Parse(aPointparam[1]);
        int cPoint2 = cPoint + cPoint1;
        int aPoint2 = aPoint + aPoint1;
        int gPoint2 = gPoint + gPoint1;
        int iPoint2 = iPoint + iPoint1;


        int Money3 = Money1 + Money2;
        string GetUName = pd.getUserName(UNum);
        if (con.Add3(NewsID, Title, PicURL, ClassID, Author, GetUName, Source, Contents, creatTime, site, Tags, DataLib, NewsTemplet, strSavePath, strfileName, strfileexName, strCheckInt) == 0 || con.Update4(ConIDp) == 0 || con.Update5(iPoint2, gPoint2, Money3, cPoint2, aPoint2, UserNum) == 0 || con.Add4(NewsID, gPoint, iPoint, Money1, CreatTime1, UserNum, content4) == 0)
        {
            rd.SaveUserAdminLogs(1, 1, UserNum, "直接审核", "审核错误");
            PageError("审核错误", "");
        }
        else
        {
            //更改稿件状态
            con.updateConstrStrat(ConIDp);
            rd.SaveUserAdminLogs(1, 1, UserNum, "直接审核", "审核成功");
            PageRight("审核成功", "Constr_List.aspx");
        }

    }
}

