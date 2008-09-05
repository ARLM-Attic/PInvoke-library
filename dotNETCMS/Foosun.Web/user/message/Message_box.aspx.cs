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
using System.IO;
using Foosun.CMS;
using Foosun.Model;

public partial class user_message_Message_box : Foosun.Web.UI.UserPage
{
    Message mes = new Message();
    #region 初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator1.OnPageChange += new PageChangeHandler(PageNavigator1_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Show_Mesages(1);
        }
    }
    protected void PageNavigator1_PageChange(object sender, int PageIndex)
    {
        Show_Mesages(PageIndex);
    }

    protected void Show_Mesages(int PageIndex)
    {
        DataTable dts = null;
        int i, j;
        string StrID = Request.QueryString["Id"];
        string xs = "1";
        if (StrID == "" && StrID == null)
        {
            PageError("错误的参数。", "");
        }
        else
        {
            xs = StrID.ToString();
        }
        if (xs == "1")
        {
            SQLConditionInfo st = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dts = Foosun.CMS.Pagination.GetPage("user_message_Message_box_1_aspx", PageIndex, 20, out i, out j, st);
        }
        else if (xs == "2")
        {
            SQLConditionInfo st2 = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dts = Foosun.CMS.Pagination.GetPage("user_message_Message_box_2_aspx", PageIndex, 20, out i, out j, st2);
        }
        else if (xs == "3")
        {
            SQLConditionInfo st3 = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dts = Foosun.CMS.Pagination.GetPage("user_message_Message_box_3_aspx", PageIndex, 20, out i, out j, st3);
        }
        else
        {
            SQLConditionInfo st4 = new SQLConditionInfo("@UserNum", Foosun.Global.Current.UserNum);
            dts = Foosun.CMS.Pagination.GetPage("user_message_Message_box_4_aspx", PageIndex, 20, out i, out j, st4);
        }
        this.PageNavigator1.PageCount = j;
        this.PageNavigator1.PageIndex = PageIndex;
        this.PageNavigator1.RecordCount = i;

        if (dts != null && dts.Rows.Count!=0)
        {
            dts.Columns.Add("LevelFlag1", typeof(string));
            dts.Columns.Add("isRead1", typeof(string));
            dts.Columns.Add("FileTF1", typeof(string));
            dts.Columns.Add("idc", typeof(string));
            dts.Columns.Add("titles", typeof(string));
            dts.Columns.Add("btf1", typeof(string));
            dts.Columns.Add("btf2", typeof(string));
            dts.Columns.Add("links", typeof(string));

            Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
            foreach (DataRow s in dts.Rows)
            {
                s["links"] = "Message_read.aspx?Mid=" + s["Mid"].ToString() + "";
                s["titles"] = "<a title=\"点击查看详细信息!&#13;发送者：" + pd.getUserName(s["UserNum"].ToString()) + "\" href=\"Message_read.aspx?Mid=" + s["Mid"].ToString() + "\" class=\"list_link\">" + s["Title"].ToString() + "</a>";
                int LevelFlag = int.Parse(s["LevelFlag"].ToString());
                if (LevelFlag == 0)
                {
                    s["LevelFlag1"] = "普通";
                }
                else if (LevelFlag == 1)
                {
                    s["LevelFlag1"] = "加急";
                }
                else
                {
                    s["LevelFlag1"] = "紧急";
                }
                int isRead = 0;
                if (s["isRead"].ToString() != "")
                {
                    isRead = int.Parse(s["isRead"].ToString());
                    if (isRead == 0)
                    {
                        s["isRead1"] = "<span style=\"color: #ff0000\">未查收</span>";
                        s["btf1"] = "<strong>";
                        s["btf2"] = "</strong>";
                    }
                    else
                    {
                        s["isRead1"] = "已查收";
                    }
                }
                else
                {
                    s["isRead1"] = "未查收";
                }
                int FileTF = int.Parse(s["FileTF"].ToString());
                if (FileTF == 0)
                {
                    s["FileTF1"] = "无附件";
                }
                else
                {
                    s["FileTF1"] = "有附件";
                }
                s["idc"] = "<input name=\"Checkbox1\" type=\"checkbox\" value=" + s["Mid"].ToString() + "  runat=\"server\" />";
            }
            DataList1.DataSource = dts;
            DataList1.DataBind();
        }
        else
        {
            no.InnerHtml = Show_no();
            this.PageNavigator1.Visible = false;
        }
       
    }
    #endregion

    #region 删除到废件箱
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
      int xs = int.Parse(Foosun.Common.Input.Filter(Request.QueryString["Id"].ToString()));

        string checkboxq = Request.Form["Checkbox1"];
        if (checkboxq == null || checkboxq == String.Empty)
        {
            PageError("请先选择要删除的信件!", "Message_box.aspx?Id=1");
        }
        else
        {
            String[] CheckboxArray = checkboxq.Split(',');
            for (int i = 0; i < CheckboxArray.Length; i++)
            {
                if (xs == 1)
                {
                    if (mes.sel(CheckboxArray[i]) == 0)
                    {
                        PageError("失败信息已经删除不能在操作", "Message_box.aspx?Id=1");
                    }
                    if (mes.Update(CheckboxArray[i]) == 0)
                    {
                        PageError("信息删除失败", "Message_box.aspx?Id=1");
                        break;
                    }
                }
                else if (xs == 2 || xs == 3)
                {
                    if (mes.sel_3(CheckboxArray[i]) == 0)
                    {
                        PageError("失败信息已经删除不能在操作", "Message_box.aspx?Id=1");
                    }
                    if (mes.Update_1(CheckboxArray[i]) == 0)
                    {
                        PageError("信息删除失败", "");
                        break;
                    }
                }
                else
                {
                    PageError("废件箱中的信件不能在删除到废件箱", "Message_box.aspx?Id=1");
                }
            }
            PageRight("消息删到废件箱成功!", "Message_box.aspx?Id=4");
        }
    }
    #endregion


    #region 删除短消息
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string UserNum = Foosun.Global.Current.UserNum;
        int xq = int.Parse(Foosun.Common.Input.Filter(Request.QueryString["Id"].ToString()));
        string checkbox = Request.Form["Checkbox1"];
        if (checkbox == null || checkbox == String.Empty)
        {
            PageError("请先选择要删除的信件!", "Message_box.aspx?Id=1");
        }
        else
        {
            String[] CheckboxArray1 = checkbox.Split(',');
            for (int i = 0; i < CheckboxArray1.Length; i++)
            {
                if (CheckboxArray1[i] != "on")
                {
                    DataTable sel_RDs = mes.sel_2(CheckboxArray1[i]);
                    int cut = sel_RDs.Rows.Count;
                    if (cut==0){continue;}
                    mes.Delete_1(CheckboxArray1[i]);
                }
            }
        }

        PageRight("信息删除成功!", "Message_box.aspx?Id=1");
    }
    #endregion
    string Show_no()
    {
        string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
        nos = nos + "<tr class='TR_BG_list'>";
        nos = nos + "<td class='navi_link'>没有数据</td>";
        nos = nos + "</tr>";
        nos = nos + "</table>";
        return nos;
    }
}
