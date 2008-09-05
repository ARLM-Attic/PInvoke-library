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

public partial class user_info_shortcut : Foosun.Web.UI.UserPage
{
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;
        }
        string action=Request.QueryString["action"];
        if (action == "edit")
        {
            string action_edit = Request.QueryString["action"];
            string action_id = Request.QueryString["id"];
            this.action_edit.Value = action_edit.ToString();
            this.action_id.Value = action_id.ToString();
            string qID = Request.QueryString["id"];
             if (qID != null)
                {
                    int qIDs = int.Parse(Foosun.Common.Input.Filter(qID.ToString()));
                    editAction(qIDs);
                }
        }
    }

    protected void editAction(int qIDs)
    {
        DataTable dt = rd.QeditAction(qIDs);
        if(dt.Rows.Count>0)
        {
            qName.Text=dt.Rows[0]["qName"].ToString();
            FilePath.Text=dt.Rows[0]["FilePath"].ToString();
            orderID.Text = dt.Rows[0]["OrderID"].ToString();
        }
    }

    protected void shortCutsubmit(object sender, EventArgs e)
    {
        if (Page.IsValid == true)                       //判断是否验证成功
        {
            //------------------获取表单值-----------------------------------------
            string Str_qName = Foosun.Common.Input.Htmls(Request.Form["qName"]);
            string Str_FilePath = Foosun.Common.Input.Htmls(Request.Form["FilePath"]);
            string Str_orderID = Request.Form["orderID"];
            string Str_action = Request.Form["action_edit"];
            string str_ID =  Request.Form["action_id"];
            string Str_QmID;
            Str_QmID = Foosun.Common.Rand.Number(12);//产生12位随机字符
            DataTable dt = rd.QGetRecord(0);
            if(dt.Rows.Count>20)
            {
                PageError("操作错误：您最多允许输入20个", "");
            }
            else
            {
                if (Str_action == "edit")
                {
                    Foosun.Model.UserInfo8 uc = new Foosun.Model.UserInfo8();
                    uc.qName = Str_qName;
                    uc.Id = int.Parse(str_ID);
                    uc.FilePath = Str_FilePath;
                    uc.OrderID = int.Parse(Str_orderID);
                    rd.UpdateQMenu(uc); 
                    PageRight("快捷方式修改成功。", "shortcut_list.aspx");
                }
                else
                {
                    DataTable dts = rd.QGetNumberRecord(Str_QmID);
                    if (dts != null)
                    {
                        if (dts.Rows.Count > 0)
                        {
                            PageError("意外错误：有可能是系统编号重复，请重新添加", "");
                        }
                        else
                        {
                            Foosun.Model.UserInfo8 uc = new Foosun.Model.UserInfo8();
                            uc.QmID = Str_QmID;
                            uc.qName = Str_qName;
                            uc.FilePath = Str_FilePath;
                            uc.Ismanage = 0;
                            uc.OrderID = int.Parse(Str_orderID);
                            uc.usernum = Foosun.Global.Current.UserNum;
                            uc.SiteID = Foosun.Global.Current.SiteID;
                            rd.InsertQMenu(uc);
                            PageRight("添加快捷方式成功。", "shortcut_list.aspx");
                        }
                        dts.Clear(); dts.Dispose();
                    }
                    else
                    {
                        PageError("意外错误!", "");
                    }
                }
            }
        }
    }
}
