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

public partial class user_up_discussManage : Foosun.Web.UI.UserPage
{
    Discuss dis = new Discuss();
    protected void Page_Load(object sender, EventArgs e)
    {
       if (!Page.IsPostBack)
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

            string DID = Foosun.Common.Input.Filter(Request["DisID"].ToString());
            HiddenField1.Value = DID;
            string UserNum = Foosun.Global.Current.UserNum;
            string UserName = dis.sel_57(UserNum);
            DataTable u_upDis = dis.sel_58(DID, UserName);
            this.CnameBox.Text = u_upDis.Rows[0]["Cname"].ToString();
            string[] Aut = u_upDis.Rows[0]["Authority"].ToString().Split('|');

            if (int.Parse(Aut[0].ToString()) == 0)
            {
                this.AuthorityList1.Items[1].Selected = true;
            }
            else
            {
                this.AuthorityList1.Items[0].Selected=true;
            }
            if (int.Parse(Aut[1].ToString()) == 0)
            {
                this.AuthorityList2.Items[1].Selected = true;
            }
            else
            {
                this.AuthorityList2.Items[0].Selected = true;
            }
            if (int.Parse(Aut[2].ToString()) == 0)
            {
                this.AuthorityList4.Items[1].Selected = true;
            }
            else
            {
                this.AuthorityList4.Items[0].Selected = true;
            }
            string[] Autm = u_upDis.Rows[0]["Authoritymoney"].ToString().Split('|');
            if (int.Parse(Autm[0].ToString()) == 0)
            {
                this.Radio1.Checked = true;
            }
            else if (int.Parse(Autm[0].ToString()) == 1)
            {
                this.Radio2.Checked = true;
            }
            else 
            {
                this.Radio3.Checked = true;
            }
            this.gPointBox.Text = Autm[1].ToString();
            this.iPointBox.Text = Autm[2].ToString();
            this.D_annoBox.Text = u_upDis.Rows[0]["D_anno"].ToString();
            this.D_ContentBox.Text = u_upDis.Rows[0]["D_Content"].ToString();
            string[] ClaID = u_upDis.Rows[0]["ClassID"].ToString().Split('|');
            for (int q = 0; q < this.ClassIDList1.Items.Count - 1; q++)
            {
                if (this.ClassIDList1.Items[q].Text == ClaID[0].ToString())
                {
                    this.ClassIDList1.Items[q].Selected = true;
                }
            }
            for (int s = 0; s < this.ClassIDList2.Items.Count - 1; s++)
            {
                if (this.ClassIDList2.Items[s].Text == ClaID[1].ToString())
                {
                    this.ClassIDList2.Items[s].Selected = true;
                }
            }
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
    protected void but1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string Did = Foosun.Common.Input.Filter(Request.QueryString["DisID"].ToString());
            string Cname = Foosun.Common.Input.Htmls(Request.Form["CnameBox"].ToString());
            DateTime Creatime = DateTime.Now;

            string D_Content = Foosun.Common.Input.Htmls(Request.Form["D_ContentBox"].ToString());
            string D_anno = Foosun.Common.Input.Filter(Request.Form["D_annoBox"].ToString());
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
            string gPoint = Foosun.Common.Input.Filter(Request.Form["gPointBox"].ToString());
            string iPoint = Foosun.Common.Input.Filter(Request.Form["iPointBox"].ToString());
            string Authoritymoney = isAuthority + "|" + gPoint + "|" + iPoint;
            string classid1 = this.ClassIDList1.SelectedValue.ToString();
            string classid2 = this.ClassIDList2.SelectedValue.ToString();
            string ClassID = classid1 + "|" + classid2;
            string UserNum = Foosun.Global.Current.UserNum;
            string UserName1 = dis.sel_57(UserNum);
            if (dis.Update_10(Cname, Authority, Authoritymoney, D_Content, D_anno, Creatime, ClassID, Did, UserName1) == 0)
            {
                PageError("修改错误", "discussManage_list.aspx");
            }
            else
            {
                PageRight("修改成功", "discussManage_list.aspx");
            }

        }
    }

    protected void but2_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("discussManage_list.aspx");
    }
}
