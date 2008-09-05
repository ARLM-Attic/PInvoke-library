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

public partial class manage_Contribution_Constr_Edit : Foosun.Web.UI.ManagePage
{
    Constr con = new Constr();
    protected void Page_Load(object sender, EventArgs e)
    {

        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            this.Authority_Code = "C043";
            this.CheckAdminAuthority();
            string ConIDs = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
            DataTable dtx = con.sel11(ConIDs);
            ContentBox.Value = dtx.Rows[0]["Content"].ToString();
            this.Title.Text = dtx.Rows[0]["Title"].ToString();
            this.Author.Text = dtx.Rows[0]["Author"].ToString();
            this.Tags.Text = dtx.Rows[0]["Tags"].ToString();
            int isCheckss = int.Parse(dtx.Rows[0]["isCheck"].ToString());
            if (isCheckss == 1)
            {
                PageError("对不起此搞已经审核不能在审核!", "");
            }
            DataTable dts1 = con.sel12();
            this.ParmConstr.DataSource = dts1;
            this.ParmConstr.DataTextField = "ConstrPayName";
            this.ParmConstr.DataValueField = "PCId";
            this.ParmConstr.DataBind();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            rootPublic rd = new rootPublic();
            string Contents = ContentBox.Value;
            string Title = Foosun.Common.Input.Filter(Request.Form["Title"].ToString());
            string Author = this.Author.Text;
            string ConIDp = Foosun.Common.Input.Filter(Request.QueryString["ConID"].ToString());
            DataTable dt = con.sel13(ConIDp);
            string Source = dt.Rows[0]["Source"].ToString();
            string PicURL = dt.Rows[0]["PicURL"].ToString();
            string site = dt.Rows[0]["SiteID"].ToString();
            string sUNum = dt.Rows[0]["UserNum"].ToString();
            string Tags = this.Tags.Text;
            string creatTime = dt.Rows[0]["creatTime"].ToString();
            string ClassID1 = Request.Form["ClassCName"].ToString();
            string ClassID = Request.Form["ClassCName"].Split(',')[0];
            string DataLib = con.sel18(ClassID);
            string NewsTemplet = "/{@dirTemplet}/Content/news.html";
            string strSavePath = "{@year04}-{@month}-{@day}";
            string strfileName = "constr-" + Foosun.Common.Rand.Number(5) + "";
            string strfileexName = ".html";
            string strCheckInt = "0|0|0|0";
            int NewsType = 0;
            if (!string.IsNullOrEmpty(PicURL))
            {
                NewsType = 1;
            }
            string NewsID = Foosun.Common.Rand.Number(12);//产生12位随机字符
            DataTable dts = con.getClassInfo(ClassID);
            if (dts != null)
            {
                if (dts.Rows.Count > 0)
                {
                    NewsTemplet = dts.Rows[0]["ReadNewsTemplet"].ToString();
                    strSavePath = dts.Rows[0]["NewsSavePath"].ToString();
                    if (strSavePath.IndexOf("{@year04}") >= 0)
                    {
                        strSavePath = strSavePath.Replace("{@year04}", DateTime.Now.ToString("yyyy"));
                    }
                    if (strSavePath.IndexOf("{@year02}") >= 0)
                    {
                    strSavePath = strSavePath.Replace("{@year02}", DateTime.Now.ToString("yy"));
                    }
                    if (strSavePath.IndexOf("{@month}") >= 0)
                    {
                        strSavePath = strSavePath.Replace("{@month}", DateTime.Now.ToString("MM"));
                    }
                    if (strSavePath.IndexOf("{@day}") >= 0)
                    {
                        strSavePath = strSavePath.Replace("{@day}", DateTime.Now.ToString("dd"));
                    }
                    strfileName = dts.Rows[0]["NewsFileRule"].ToString();
                    if (strfileName.IndexOf("{@自动编号ID}") >= 0)
                    {
                        strfileName = strfileName.Replace("{@自动编号ID}", NewsID);
                    }
                    strfileexName = dts.Rows[0]["FileName"].ToString();
                    if (dts.Rows[0]["CheckInt"].ToString() == "1") { strCheckInt = "1|1|0|0"; }
                    else if (dts.Rows[0]["CheckInt"].ToString() == "2") { strCheckInt = "2|1|1|0"; }
                    else if (dts.Rows[0]["CheckInt"].ToString() == "3") { strCheckInt = "3|1|1|1"; }
                }
                dts.Clear(); dts.Dispose();
            }            
            DateTime CreatTime1 = DateTime.Now;
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
            DataTable dt_User = con.sel15(sUNum);
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
            string _UserID = rd.getUserName(sUNum);
            //if (con.Add3(NewsID, Title, PicURL, ClassID, Author, _UserID, Source, Contents, creatTime, site, Tags, DataLib, NewsTemplet, strSavePath, strfileName, strfileexName, strCheckInt) == 0 || con.Update4(ConIDp) == 0 || con.Update5(iPoint2, gPoint2, Money3, cPoint2, aPoint2, UserNum) == 0 || con.Add4(NewsID, gPoint, iPoint, Money1, CreatTime1, UserNum, content4) == 0)
            if (con.Add3(NewsID, NewsType, Title, PicURL, ClassID, Author, _UserID, Source, Contents, creatTime, site, Tags, DataLib, NewsTemplet, strSavePath, strfileName, strfileexName, strCheckInt) == 0 || con.Update4(ConIDp) == 0 || con.Update5(iPoint2, gPoint2, Money3, cPoint2, aPoint2, UserNum) == 0 || con.Add4(NewsID, gPoint, iPoint, Money1, CreatTime1, UserNum, content4) == 0)
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "编辑审核投稿", "编辑审核失败");
                PageError("审核错误", "");
            }
            else
            {
                rd.SaveUserAdminLogs(1, 1, UserNum, "编辑审核投稿", "编辑审核成功");
                PageRight("审核成功", "Constr_List.aspx");
            }
        }
    }
}