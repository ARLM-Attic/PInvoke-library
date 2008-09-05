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
using System.IO;

public partial class user_discussManage_add : Foosun.Web.UI.ManagePage
{
    public user_discussManage_add()
    {
        Authority_Code = "U018";
    }
    //连接数据库
    Discuss dis = new Discuss();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.CacheControl = "no-cache";

            //-----------------绑定讨论组开始-------------------------------
            this.ClassIDList1.DataSource = dis.sel_49();
            this.ClassIDList1.DataTextField = "Cname";
            this.ClassIDList1.DataValueField = "DcID";
            this.ClassIDList1.DataBind();
            ClassIDList1.Items.Insert(0, new ListItem("请选择", "0"));
            ClassIDList2.Items.Insert(0, new ListItem("请选择", "0"));
            //-----------------绑定讨论组结束-------------------------------

        }
        if (Request.Form["provinces"] != null && !Request.Form["provinces"].Trim().Equals(""))
        {
            DataTable tb = dis.sel_50(Request.Form["provinces"].ToString());
            int cut = tb.Rows.Count;
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                if (i > 0)
                    Response.Write(";");

                Response.Write(tb.Rows[i]["DcID"] + "," + tb.Rows[i]["Cname"]);
            }
            Response.End();
        }
    }
    #endregion
    /// <summary>
    /// 添加讨论组
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    #region 添加讨论组
    protected void but1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            DateTime Creatime = DateTime.Now;//获取当前系统时间
            string DisID = Foosun.Common.Rand.Number(12);

            string Cname = Request.Form["CnameBox"].ToString();
            string D_Content = Request.Form["D_ContentBox"].ToString();
            string D_anno = Request.Form["D_annoBox"].ToString();
            string Authority = "";
            if (this.AuthorityList1.Items[0].Selected == true)
            {
                Authority += "1|";
            }
            else
            {
                Authority += "0|";
            }
            if (this.AuthorityList2.Items[0].Selected == true)
            {
                Authority += "1|";
            }
            else
            {
                Authority += "0|";
            }
            if (this.AuthorityList4.Items[0].Selected == true)
            {
                Authority += "1";
            }
            else
            {
                Authority += "0";
            }
            string isAuthority = null;
            if (this.Radio1.Checked)
            {
                isAuthority = "0";
            }
            else if (this.Radio2.Checked)
            {
                isAuthority = "1";
            }
            else if (this.Radio3.Checked)
            {
                isAuthority = "2";
            }
            string gPoint = Request.Form["gPointBox"].ToString();
            string iPoint = Request.Form["iPointBox"].ToString();
            string Authoritymoney = isAuthority + "|" + gPoint + "|" + iPoint;
            string classid1 = this.ClassIDList1.SelectedValue.ToString();
            string classid2 = this.ClassIDList2.SelectedValue.ToString();
            string ClassID = classid1 + "|" + classid2;
            //查询会员属于那个会员组
            string UserGroupNumber = pd.getUserGroupNumber(UserNum);
            string UName = pd.getUserName(UserNum);
            //查询允许最大建立数量
            DataTable dt7 = dis.sel_52(UserGroupNumber);
            int cut = dt7.Rows.Count;
            int GroupCreatNum = 5;
            int GroupSize = 0;
            int GroupPerNum = 0;
            if (cut != 0)
            {
                if (dt7.Rows[0]["GroupCreatNum"].ToString() != "")
                {
                    GroupCreatNum = int.Parse(dt7.Rows[0]["GroupCreatNum"].ToString());
                }
                if (dt7.Rows[0]["GroupSize"].ToString() != "")
                {
                    GroupSize = int.Parse(dt7.Rows[0]["GroupSize"].ToString());
                }
                if (dt7.Rows[0]["GroupPerNum"].ToString() != "")
                {
                    GroupPerNum = int.Parse(dt7.Rows[0]["GroupPerNum"].ToString());
                }
            }
            //获取我的用户名
            DataTable dt1 = dis.sel_53(UserNum);
            string um = dt1.Rows[0]["UserName"].ToString();
            string site = dt1.Rows[0]["SiteID"].ToString();
            //获取我已经建立的讨论组
            string Fundwarehouse = "0|0";
            int ct = dis.sel_54(um);
            //添加魅力值和活跃值
            DataTable dt4 = dis.sel_55();
            string[] cPointParam = dt4.Rows[0]["cPointParam"].ToString().Split('|');
            string[] aPointparam = dt4.Rows[0]["aPointparam"].ToString().Split('|');
            int aPoint = int.Parse(dt1.Rows[0]["aPoint"].ToString());
            int cPoint = int.Parse(dt1.Rows[0]["cPoint"].ToString());
            int cPoint1 = int.Parse(cPointParam[1]);
            int aPoint1 = int.Parse(aPointparam[1]);
            int cPoint2 = cPoint + cPoint1;
            int aPoint2 = aPoint + aPoint1;
            //创建讨论组  
            Foosun.Model.STDiscuss stcn;
            stcn.DisID = DisID;
            stcn.Cname = Cname;
            stcn.Authority = Authority;
            stcn.Authoritymoney = Authoritymoney;
            stcn.UserNames = UName;
            stcn.D_Content = D_Content;
            stcn.D_anno = D_anno;
            stcn.Creatimes = Creatime;
            stcn.ClassID = ClassID;
            stcn.Fundwarehouse = Fundwarehouse;
            stcn.GroupSize = GroupSize;
            stcn.GroupPerNum = GroupPerNum;
            stcn.SiteID = site;
            DataTable dt = dis.sel_56();
            int cutb = dt.Rows.Count;
            string DisIDs = "";
            if (cutb > 0)
            {
                DisIDs = dt.Rows[0]["DisID"].ToString();
            }
            if (DisIDs != DisID)
            {
                if (ct > GroupCreatNum)
                {
                    PageError("对不起你建立的讨论组已经超过自大数目不能在建立了", "userdiscuss_list.aspx");
                }
                else
                {
                    if (dis.Add_12(stcn) == 0 || dis.Update_9(cPoint2, aPoint2, UserNum) == 0)
                    {
                        PageError("添加错误", "userdiscuss_list.aspx");
                    }
                    else
                    {
                        CreateFolder(DisID);
                        PageRight("添加成功", "userdiscuss_list.aspx");
                    }
                }
            }
            else
            {
                PageError("对不起建立失败有可能是编号重复", "userdiscuss_list.aspx");
            }
        }
    }

    //创建文件夹
    public void CreateFolder(string discussDisID)
    {
        if (discussDisID.Trim().Length > 0)
        {
            try
            {
                string CreatePath = System.Web.HttpContext.Current.Server.MapPath
("~/" + Userfiles + "/discuss/" + discussDisID).ToString();
                if (!Directory.Exists(CreatePath))
                {
                    Directory.CreateDirectory(CreatePath);
                }
            }
            catch
            {
                throw;
            }
        }
    }
    #endregion
}
