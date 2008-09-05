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

public partial class user_photo_Photoalbumlist : Foosun.Web.UI.UserPage
{

    Photo pho = new Photo();
    UserMisc rd = new UserMisc();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        this.PageNavigator3.OnPageChange += new PageChangeHandler(PageNavigator3_PageChange);
        Response.CacheControl = "no-cache";
        if (!IsPostBack)
        {
            Show_cjlist(1);
        }
        string Type = Request.QueryString["Type"];  //取得操作类型
        string ID = "";
        if (Request.QueryString["ID"] != "" && Request.QueryString["ID"] != null)
        {
            ID = Foosun.Common.Input.Filter(Request.QueryString["ID"]);  //取得需要操作的稿件ID
        }

        switch (Type)
        {
            case "del":          //删除
                del(ID);
                break;
            case "PDel":            //批量删除
                PDel();
                break;
            default:
                break;
        }
    }

        protected void PageNavigator3_PageChange(object sender, int PageIndex2)
        {
            Show_cjlist(PageIndex2);
        }
        protected void Show_cjlist(int PageIndex2)
        {
            string ClassID = "";
            if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != "")
            {
                ClassID = Request.QueryString["ClassID"].ToString();
            }
            string UserNum=Foosun.Global.Current.UserNum;
            int ib, jb;
            DataTable cjlistdts = pho.GetPage(UserNum, ClassID, PageIndex2, 20, out ib, out jb, null);
            this.PageNavigator3.PageCount = jb;
            this.PageNavigator3.PageIndex = PageIndex2;
            this.PageNavigator3.RecordCount = ib;
            if (cjlistdts.Rows.Count > 0)
            {
                cjlistdts.Columns.Add("idc2", typeof(string));
                cjlistdts.Columns.Add("PhotoalbumNames", typeof(string));
                cjlistdts.Columns.Add("UserNames", typeof(string));
                cjlistdts.Columns.Add("picnum", typeof(string));
                cjlistdts.Columns.Add("Pwd", typeof(string));
                string _dirDumm = Foosun.Config.UIConfig.dirDumm;
                if (_dirDumm.Trim() != "")
                {
                    _dirDumm = "/" + _dirDumm;
                }
                foreach (DataRow h in cjlistdts.Rows)
                {
                    int sel_picnumber = rd.sel_picnum(h["PhotoalbumID"].ToString());
                    if (sel_picnumber != 0)
                    {
                        h["picnum"] = sel_picnumber ;
                    }
                    else
                    {
                        h["picnum"] = "0";
                    } 
                    
                    if (h["pwd"].ToString() == "")
                    {
                        h["Pwd"] = "";
                    }
                    else 
                    {
                        h["Pwd"] = "<img src=\"../../sysImages/folder/pw.gif\" alt=\"访问需要密码\" />";
                    }
                    h["idc2"] = "<a href=\"Photoalbum_up.aspx?PhotoalbumID=" + h["PhotoalbumID"].ToString() + "\"><img src=\"../../sysImages/folder/re.gif\" alt=\"修改\" border=\"0\" /></a>&nbsp;<a href=\"#\" onclick=\"javascript:del('" + h["PhotoalbumID"].ToString() + "');\"><img src=\"../../sysImages/folder/dels.gif\" alt=\"删除\" border=\"0\" /></a>&nbsp;<input name=\"Checkbox1\" type=\"checkbox\" value=" + h["PhotoalbumID"].ToString() + "  runat=\"server\" />";
                    h["PhotoalbumNames"] = "<a href=\"photo.aspx?PhotoalbumID=" + h["PhotoalbumID"].ToString() + "\" class=\"list_link\">" + h["PhotoalbumName"].ToString() + "</a>";
             
                    h["UserNames"] = pho.sel_12(h["UserName"].ToString());

                }
                sc.InnerHtml = Show_sc();
                Repeater1.DataSource = cjlistdts;
                Repeater1.DataBind();
            }
            else
            {
                no.InnerHtml = Show_no();
                this.PageNavigator3.Visible = false;
            }
        }
        string Show_no()
        {

            string nos = "<table border=0 width='98%' align=center cellpadding=5 cellspacing=1 class='table'>";
            nos = nos + "<tr class='TR_BG_list'>";
            nos = nos + "<td class='navi_link'>没有数据</td>";
            nos = nos + "</tr>";
            nos = nos + "</table>";
            return nos;
        }
    string Show_sc()
    {
        string sc = "<a href=\"javascript:PDel();\" class=\"topnavichar\">批量删除</a>";
        return sc;
    }
            protected void PDel()
        {
            string checkboxq = Request.Form["Checkbox1"];


            if (checkboxq == null || checkboxq == String.Empty)
            {
                PageError("请先选择要删除的相册!", "Photoalbumlist.aspx");
            }
            else
            {
                string[] chSplit = checkboxq.Split(',');
                for (int i = 0; i < chSplit.Length; i++)
                {
                    if (chSplit[i] != "on")
                    {
                        DeleParentFolder(pho.sel_11(chSplit[i]));
                        if (pho.sel_19(chSplit[i]) != 0)
                        {
                            if (pho.Delete_1(chSplit[i]) == 0 || pho.Delete_2(chSplit[i]) == 0)
                            {
                                PageError("批量删除失败", "Photoalbumlist.aspx");
                                break;
                            }
                        }
                        else
                        {
                            if (pho.Delete_1(chSplit[i]) == 0)
                            {
                                PageError("批量删除失败", "Photoalbumlist.aspx");
                                break;
                            }
                        }
                         
                    }
                }
                PageRight("批量删除成功", "Photoalbumlist.aspx");
            }

        }
    protected void del(string ID)
    {
            DeleParentFolder(pho.sel_11(ID));
            if (pho.sel_19(ID) != 0)
            {
                if (pho.Delete_1(ID) == 0 || pho.Delete_2(ID) == 0)
                {
                    PageError("删除失败", "Photoalbumlist.aspx");
                }
                else
                {
                    PageRight("删除成功!", "Photoalbumlist.aspx");
                }
            }
            else 
            {
                if (pho.Delete_1(ID) == 0)
                {
                    PageError("删除失败", "Photoalbumlist.aspx");
                }
                else
                {
                    PageRight("删除成功!", "Photoalbumlist.aspx");
                }
            }
        }
    public void DeleParentFolder(string Url)
    {
        try
        {
            DirectoryInfo DelFolder = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(Url).ToString());
            if (DelFolder.Exists)
            {
                DelFolder.Delete();
            }
        }
        catch
        {

        }
    }
}
