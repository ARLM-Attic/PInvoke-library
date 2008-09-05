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

public partial class user_Exchange : Foosun.Web.UI.UserPage
{
    Info inf = new Info();
    public string type = null;
    protected void Page_Init(object sernder, EventArgs e)
    {
        #region  初始化
        
        string UserNum = Foosun.Global.Current.UserNum;
        string UserGroupNumber = inf.sel_3(UserNum);
        string[] GIChange = inf.sel_4(UserGroupNumber).Split('|');
        string[] GTChageRate = inf.sel_6(UserGroupNumber).Split('|');
        Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
        string[] UserGI = pd.getGIPoint(UserNum).Split('|');
        string d1 = "";
        string dd1 = "";
        string d2 = "";
        string dd2 = "";
        if (int.Parse(UserGI[0]) < int.Parse(GTChageRate[0]))
        {
            d1 = "disabled";
            dd1 = "&nbsp;积分不够,不能兑换。";
        }
        if (int.Parse(UserGI[1])<1)
        {
            d2 = "disabled";
            dd1 = "&nbsp;" + pd.getgPointName() + "不够,不能兑换。";
        }

        if (GIChange[0] == "1" && GIChange[1] == "0")
        {
            type = "I";
            scs.InnerHtml = show_scs();
            string ctr = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\" id=\"I\"><tr class=\"TR_BG_list\"><td class=\"list_link\" width=\"30%\" style=\"text-align: right\">积分数(兑换比例：" + GTChageRate[0] + "积分兑换1" + pd.getgPointName() + ")</td><td class=\"list_link\" width=\"70%\"><asp:TextBox ID=\"iPointBox\" onclientclick=\"Change(0);\" runat=\"server\" Width=\"188px\" " + d1 + " CssClass=\"form\">" + UserGI[0] + "</asp:TextBox>　" + dd1 + "　<asp:RegularExpressionValidator ID=\"RegularExpressionValidator2\" runat=\"server\" ControlToValidate=\"iPointBox\" ErrorMessage=\"您输入的格式不对，请输入整数\" ValidationExpression=\"^[1-9]\\d*|0$\"></asp:RegularExpressionValidator></td></tr><tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right\"></td><td class=\"list_link\"><asp:Button ID=\"Iexchange\" runat=\"server\" Text=\"确定兑换\" " + d1 + " Width=\"94px\" CssClass=\"form\" OnClick=\"Iexchange_Click\" onclientclick=\"Change(0);\"/></td></tr></table> ";
            Control ctrl = Page.ParseControl(ctr);
            PlaceHolder1.Controls.Add(ctrl);
            Button bt2 = (Button)Page.FindControl("Iexchange");
            bt2.Command += new CommandEventHandler(this.Iexchange);
        }
        if (GIChange[1] == "1" && GIChange[0] == "0")
        {
            type = "G";
            sc.InnerHtml = show_sc();
            string ctr1 = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\" id=\"I\"><tr class=\"TR_BG_list\"><td class=\"list_link\" width=\"30%\" style=\"text-align: right\">" + pd.getgPointName() + "数(兑换比例：1" + pd.getgPointName() + "兑换" + GTChageRate[1] + "积分)</td><td class=\"list_link\" width=\"70%\"><asp:TextBox ID=\"gPointBox\" onclientclick=\"Change(1);\" runat=\"server\" Width=\"188px\" " + d2 + " MaxLength=\"8\" CssClass=\"form\">" + UserGI[1] + "</asp:TextBox>　" + dd2 + "　<asp:RegularExpressionValidator ID=\"RegularExpressionValidator1\" runat=\"server\" ControlToValidate=\"gPointBox\" ErrorMessage=\"您输入的格式不对，请输入整数\" ValidationExpression=\"^[1-9]\\d*|0$\"></asp:RegularExpressionValidator></td></tr><tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right\"></td><td class=\"list_link\"><asp:Button ID=\"Gexchange\" runat=\"server\" Text=\"确定兑换\" " + d2 + " Width=\"94px\" CssClass=\"form\" OnClick=\"Iexchange_Click\" onclientclick=\"Change(1);\"/></td></tr></table> ";
            Control ctrl1 = Page.ParseControl(ctr1);
            PlaceHolder2.Controls.Add(ctrl1);
            Button bt1 = (Button)Page.FindControl("Gexchange");
            bt1.Command += new CommandEventHandler(this.Gexchange);
        }
        if (GIChange[1] == "1" && GIChange[0] == "1")
        {
            sc.InnerHtml = show_sc();
            scs.InnerHtml = show_scs();
            string ctr3 = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\" id=\"I\"><tr class=\"TR_BG_list\"><td class=\"list_link\" width=\"30%\" style=\"text-align: right\">" + pd.getgPointName() + "数(兑换比例：1" + pd.getgPointName() + "兑换" + GTChageRate[1] + "积分)</td><td class=\"list_link\" width=\"70%\"><asp:TextBox onclientclick=\"Change(1);\" ID=\"gPointBox\" runat=\"server\" MaxLength=\"8\" " + d2 + " Width=\"188px\" CssClass=\"form\">" + UserGI[1] + "</asp:TextBox>　" + dd2 + "　<asp:RegularExpressionValidator ID=\"RegularExpressionValidator1\" runat=\"server\" ControlToValidate=\"gPointBox\" ErrorMessage=\"您输入的格式不对，请输入整数\" ValidationExpression=\"^[1-9]\\d*|0$\"></asp:RegularExpressionValidator></td></tr><tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right\"></td><td class=\"list_link\"><asp:Button ID=\"Gexchange\" runat=\"server\" Text=\"确定兑换\" " + d2 + " Width=\"94px\" CssClass=\"form\" OnClick=\"Iexchange_Click\" onclientclick=\"Change(1);\"/></td></tr></table> ";
            Control ctrl3 = Page.ParseControl(ctr3);
            PlaceHolder2.Controls.Add(ctrl3);
            string ctr2 = "<table width=\"98%\" border=\"0\" align=\"center\" cellpadding=\"5\" cellspacing=\"1\" class=\"table\" id=\"I\"><tr class=\"TR_BG_list\"><td class=\"list_link\" width=\"30%\" style=\"text-align: right\">积分数(兑换比例：" + GTChageRate[0] + "积分兑换1" + pd.getgPointName() + ")</td><td class=\"list_link\" width=\"70%\"><asp:TextBox onclientclick=\"Change(0);\" ID=\"iPointBox\" runat=\"server\" MaxLength=\"8\" " + d1 + " Width=\"188px\" CssClass=\"form\">" + UserGI[0] + "</asp:TextBox>　" + dd1 + "　<asp:RegularExpressionValidator ID=\"RegularExpressionValidator2\" runat=\"server\" ControlToValidate=\"iPointBox\" ErrorMessage=\"您输入的格式不对，请输入整数\" ValidationExpression=\"^[1-9]\\d*|0$\"></asp:RegularExpressionValidator></td></tr><tr class=\"TR_BG_list\"><td class=\"list_link\" style=\"text-align: right\"></td><td class=\"list_link\"><asp:Button ID=\"Iexchange\" runat=\"server\" Text=\"确定兑换\" " + d1 + " Width=\"94px\" CssClass=\"form\" OnClick=\"Iexchange_Click\" onclientclick=\"Change(0);\"/></td></tr></table> ";
            Control ctrl2 = Page.ParseControl(ctr2);
            PlaceHolder1.Controls.Add(ctrl2);
            Button bt2 = (Button)Page.FindControl("Iexchange");
            bt2.Command += new CommandEventHandler(this.Iexchange);
            Button bt1 = (Button)Page.FindControl("Gexchange");
            bt1.Command += new CommandEventHandler(this.Gexchange);
        }
        #endregion
    }
    #region  G币兑换积分
    protected void Gexchange(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        DataTable dt = inf.sel_5(UserNum);
        string[] GTChageRate = inf.sel_6(dt.Rows[0]["UserGroupNumber"].ToString()).Split('|');

        int gPoints = 232;
        try
        {
            gPoints = int.Parse(GTChageRate[1]);
        }
        catch
        {
        }
        if (Request.Form["gPointBox"].ToString() == "")
        {
            PageError("G币数不能为空", "Exchange.aspx");
        }
        int gPoint = int.Parse(Foosun.Common.Input.Filter(Request.Form["gPointBox"].ToString()));
        int ipoint1 = int.Parse(dt.Rows[0]["iPoint"].ToString());
        int gpoint1 = int.Parse(dt.Rows[0]["gPoint"].ToString());
        int i = gPoint * gPoints;
        if (gpoint1 < gPoint)
        {
            PageError("对不起你的G币不够不能兑换", "Exchange.aspx");
        }
        int ipoint2 = ipoint1 + i;
        int gpoint2 = gpoint1 - gPoint;
        string content3 = "积分兑换";
        int ghtype = 0;
        int ghtype1 = 1;
        Foosun.Model.STGhistory Gh = new Foosun.Model.STGhistory();
        Gh.Gpoint = gPoint;
        Gh.iPoint = i;
        if ((inf.Update1(ipoint2, gpoint2, UserNum) == 0) || (inf.Add(Gh, ghtype, UserNum, content3)==0) || (inf.Add(Gh, ghtype1, UserNum, content3)==0))
        {
            PageError("对不起兑换失败", "Exchange.aspx");
        }
        else
        {
            PageRight("兑换成功", "Exchange.aspx");
        }
    }
    #endregion
    #region  积分兑换G币
    protected void Iexchange(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        DataTable dt = inf.sel_5(UserNum);
        string[] GTChageRate = inf.sel_6(dt.Rows[0]["UserGroupNumber"].ToString()).Split('|');
        int iPoints = int.Parse(GTChageRate[0]);
        if (Request.Form["iPointBox"].ToString() == "")
        {
            PageError("积分数不能空", "Exchange.aspx");
        }
        int iPoint = int.Parse(Foosun.Common.Input.Filter(Request.Form["iPointBox"].ToString()));
        int ipoint1=int.Parse(dt.Rows[0]["iPoint"].ToString());
        int gpoint1=int.Parse(dt.Rows[0]["gPoint"].ToString());
        int g = (int)iPoint / iPoints;
        int i = g * iPoints;
        if (ipoint1 < i)
        {
            PageError("对不起你的积分不够不能兑换", "Exchange.aspx");
        }
        int ipoint2=ipoint1-i;
        int gpoint2=gpoint1+g;
        string content3="积分兑换";
        int ghtype = 0;
        int ghtype1 = 1;
        Foosun.Model.STGhistory Gh = new Foosun.Model.STGhistory();
        Gh.Gpoint = g;
        Gh.iPoint = i;
        if ((inf.Update1(ipoint2, gpoint2, UserNum) == 0) || (inf.Add(Gh, ghtype, UserNum, content3)==0) || (inf.Add(Gh, ghtype1, UserNum, content3)==0))
        {
            PageError("对不起兑换失败", "Exchange.aspx");
        }
        else 
        {
            PageRight("兑换成功", "Exchange.aspx");
        }
    }
    #endregion

    string show_sc()
    {
        string sc = "<a href=\"?types=G\" class=\"list_link\">G币兑换积分</a>";
        return sc;
    }
    string show_scs()
    {
        string scs = "<a href=\"?types=I\" class=\"list_link\">积分兑换G币</a>";
        return scs;
    }
}