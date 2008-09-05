///************************************************************************************************************
///**********修改专题Code By DengXi****************************************************************************
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
using System.Text.RegularExpressions;

public partial class manage_news_Special_edit : Foosun.Web.UI.ManagePage
{
    public manage_news_Special_edit()
    {
        Authority_Code = "C039";
    }
    public string fileexname = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;            //获取版权信息
            GetSpeacilInfo();
        }
    }

    /// <summary>
    /// 取得此条记录并在前台呈现出来
    /// </summary>
    /// <returns>取得此条记录并在前台呈现出来</returns>
    /// Code By DengXi

    protected void GetSpeacilInfo()
    {
        string ID = Foosun.Common.Input.checkID(Request.QueryString["ID"]);

        Foosun.CMS.Special scinfo = new Foosun.CMS.Special();
        DataTable dt = scinfo.getSpeacilInfo(ID);

        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                SpaecilID.Value = dt.Rows[0]["SpecialID"].ToString();
                S_Cname.Text = dt.Rows[0]["SpecialCName"].ToString();
                S_Ename.Text = dt.Rows[0]["specialEName"].ToString();
                S_Parent.Text = dt.Rows[0]["ParentID"].ToString();
                if (dt.Rows[0]["ParentID"].ToString() != "0")
                {
                    Foosun.CMS.Special parentSpecial = new Foosun.CMS.Special();
                    DataTable psc = parentSpecial.getSpeacilInfo(dt.Rows[0]["ParentID"].ToString());
                    this.S_ParentName.Text = psc.Rows[0]["SpecialCName"].ToString();
                }
                else
                {
                    this.S_ParentName.Text = "根专题";
                }
                S_Domain.Text = dt.Rows[0]["Domain"].ToString();
                S_FileExname.Text = dt.Rows[0]["FileEXName"].ToString();

                string str_UesrGroup = dt.Rows[0]["GroupNumber"].ToString();
                string [] arr_UserGroup = str_UesrGroup.Split(',');

                Foosun.CMS.Common.rootPublic rpc = new Foosun.CMS.Common.rootPublic();
                IDataReader rd = rpc.GetGroupList();
                ListItem defaultitm = new ListItem();
                defaultitm.Value = "0";
                defaultitm.Text = "请选择会员组";
                S_UserGroup.Items.Add(defaultitm);
                while (rd.Read())
                {
                    ListItem itm = new ListItem();
                    itm.Text = rd["GroupName"].ToString();
                    itm.Value = rd["GroupNumber"].ToString();
                    for (int j = 0; j < arr_UserGroup.Length; j++)
                    {
                        if (arr_UserGroup[j].ToString() == rd["GroupNumber"].ToString())
                        {
                            itm.Selected = true;
                        }
                    }
                    S_UserGroup.Items.Add(itm);
                }
                rd.Close();
                fileexname = dt.Rows[0]["FileEXName"].ToString();

                S_IsDel.Text = dt.Rows[0]["isDelPoint"].ToString();
                S_Point.Text = dt.Rows[0]["iPoint"].ToString();
                S_Money.Text = dt.Rows[0]["Gpoint"].ToString();
                S_DirRule.Text = dt.Rows[0]["saveDirPath"].ToString();
                S_FileRule.Text = dt.Rows[0]["FileName"].ToString();
                S_SavePath.Text = dt.Rows[0]["SavePath"].ToString();

                S_Pic.Text = dt.Rows[0]["NaviPicURL"].ToString();
                S_Text.Text = dt.Rows[0]["NaviContent"].ToString();
                S_Templet.Text = dt.Rows[0]["Templet"].ToString();

                S_Page.Text = dt.Rows[0]["NaviPosition"].ToString();
            }
            else
            {
                PageError("参数传递错误", "");
            }
            dt.Clear();
            dt.Dispose();
        }
        else
        {
            PageError("参数传递错误", "");
        }
    }

    /// <summary>
    /// 修改专题信息开始
    /// </summary>
    /// <returns>修改专题信息开始</returns>
    /// Code By DengXi

    protected void Button1_Click(object sender, EventArgs e)
    {
        //---------------------------取得表单值

        Foosun.Model.Special sci = new Foosun.Model.Special();
        sci.SpecialID = Request.Form["SpaecilID"];            //专题编号
        sci.SpecialCName = Request.Form["S_Cname"];           //中文名
        sci.specialEName = "";
        sci.Domain = Request.Form["S_Domain"];                //域名
        sci.FileEXName = Request.Form["S_FileExname"];        //生成文件的扩展名

        if (Request.Form["isTrue"] == "1")                    //是否启用浏览权限控制
        {
            sci.GroupNumber = Request.Form["S_UserGroup"]+"";    //可浏览用户组
            sci.isDelPoint = int.Parse(Request.Form["S_IsDel"]);  //是否启用扣取与所需
            sci.iPoint = int.Parse(Request.Form["S_Point"]);      //点数
            sci.Gpoint = int.Parse(Request.Form["S_Money"]);      //金币
        }
        else
        {
            sci.GroupNumber = "0";
            sci.isDelPoint = 0;  //是否启用扣取与所需
            sci.iPoint = 0;      //点数
            sci.Gpoint = 0;      //金币
        }
        //英文名
        string enNames = this.S_Ename.Text;

        string fileNames = Foosun.CMS.Common.CommStr.FileRandName(Request.Form["S_FileRule"]);      //文件名生成规则
        string f_name = Regex.Replace(fileNames, "{@EName}", enNames);

        sci.saveDirPath = Foosun.CMS.Common.CommStr.FileRandName(Request.Form["S_DirRule"]);    //目录生成规则
        sci.FileName = f_name;      //文件名生成规则
        sci.SavePath = Request.Form["S_SavePath"];            //保存路径

        sci.NaviPicURL = Request.Form["S_Pic"];               //导航图片路径
        sci.NaviContent = Request.Form["S_Text"];             //导航文字
        sci.Templet = Request.Form["S_Templet"];              //专题模板路径
        sci.NaviPosition = Request.Form["S_Page"];            //页面导航条

        sci.SiteID = SiteID;
        sci.CreatTime = DateTime.Now;
        sci.isRecyle = 0;
        sci.ParentID = "";

        int result = 0;
        Foosun.CMS.Special sc = new Foosun.CMS.Special();
        result = sc.Edit(sci);

        if (result == 1)
        {
            Foosun.Publish.General PG = new Foosun.Publish.General();
            if (PG.publishSingleSpecial(sci.SpecialID))
                PageRight("修改专题信息成功!生成专题成功!", "special_list.aspx");
            else
                PageRight("修改专题信息成功!生成专题失败!", "special_list.aspx");
        }
        else
            PageRight("修改专题信息失败!", "");
    }

    /// <summary>
    /// 显示前台JS效果,如果是.aspx后缀名就显示浏览权限选项.
    /// </summary>
    /// <returns>显示前台JS效果</returns>
    /// Code By DengXi

    protected void Show()
    {
        if (fileexname == ".aspx")
        {
            Response.Write("<script language=\"javascript\">Hide('.aspx');</script>");
        }
    }
}
