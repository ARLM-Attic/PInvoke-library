///************************************************************************************************************
///**********添加专题Code By DengXi****************************************************************************
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
using Foosun.CMS;
using Foosun.CMS.Common;

public partial class manage_news_Special_add : Foosun.Web.UI.ManagePage
{
    public manage_news_Special_add()
    {
        Authority_Code = "C0";
    }
    rootPublic pd = new rootPublic();
    public string DirHtml = Foosun.Config.UIConfig.dirHtml;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            copyright.InnerHtml = CopyRight;    //获取版权信息
            if (SiteID != "0"){DirHtml = Foosun.Config.UIConfig.dirSite;}
            string parentID = Request.QueryString["parentID"];
            //-----------------获取会员组信息
            Foosun.CMS.Common.rootPublic rpc = new Foosun.CMS.Common.rootPublic();
            IDataReader dr = rpc.GetGroupList();
            while (dr.Read())
            {
                ListItem it = new ListItem();
                it.Value = dr["GroupNumber"].ToString();
                it.Text = dr["GroupName"].ToString();
                S_UserGroup.Items.Add(it);
            }
            dr.Close();
            ListItem itm = new ListItem();
            itm.Value = "0";
            itm.Text = "请选择会员组";
            //itm.Selected = true;
            S_UserGroup.Items.Insert(0, itm);
            itm = null; 
            if (parentID != null && parentID != "")
            {
                this.S_Parent.Text = parentID.ToString();
                Foosun.CMS.Special parentSpecial = new Foosun.CMS.Special();
                DataTable psc = parentSpecial.getSpeacilInfo(parentID);
                this.S_ParentName.Text=psc.Rows[0]["SpecialCName"].ToString();
            }
            else
            {
                this.S_Parent.Text = "0";
                this.S_ParentName.Text = "根专题";
            }
            this.S_Templet.Text = pd.allTemplet().Split(new Char[] { '|' })[2];
            this.S_DirRule.Text = "{@year04}-{@month}";
            this.S_FileRule.Text = "{@EName}/index";
            this.S_SavePath.Text = "/" + Foosun.Config.UIConfig.dirHtml + "/special";
        }
    }


    /// <summary>
    /// 添加专题进数据库
    /// </summary>
    /// <returns>返回域名字符串</returns>
    /// Code By DengXi

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid == true)
        {
            //-----------------取得表单内容
            Foosun.Model.Special sci = new Foosun.Model.Special();
            sci.SpecialCName = Request.Form["S_Cname"];             //中文名
            sci.specialEName = Request.Form["S_Ename"];             //英文名
            sci.ParentID = Request.Form["S_Parent"];                //父栏目

            sci.Domain = Request.Form["S_Domain"];                  //域名
            sci.FileEXName = Request.Form["S_FileExname"];          //生成文件的扩展名

            if (Request.Form["isTrue"] == "1")                         //是否启用浏览权限控制
            {
                sci.GroupNumber = Request.Form["S_UserGroup"]+"";         //可浏览用户组
                sci.isDelPoint = int.Parse(Request.Form["S_IsDel"]);   //是否启用扣取与所需
                sci.iPoint = int.Parse(Request.Form["S_Point"]);       //点数
                sci.Gpoint = int.Parse(Request.Form["S_Money"]);       //金币
            }
            else
            {
                sci.GroupNumber = "0";                               
                sci.isDelPoint = 0;    
                sci.iPoint = 0;        
                sci.Gpoint = 0;
            }
            string DirPath =Foosun.CMS.Common.CommStr.FileRandName(Request.Form["S_DirRule"]);
            string SaveTP=Request.Form["S_SavePath"].Replace("{@dirHtml}",Foosun.Config.UIConfig.dirHtml);
            string fName=Foosun.CMS.Common.CommStr.FileRandName(Request.Form["S_FileRule"]).Replace("{@EName}", Request.Form["S_Ename"]);
            sci.saveDirPath = DirPath;    //目录生成规则
            sci.FileName = fName;      //文件名生成规则
            sci.SavePath = SaveTP;                                             //保存路径

            sci.NaviPicURL = Request.Form["S_Pic"];                 //导航图片路径
            sci.NaviContent = Request.Form["S_Text"];               //导航文字
            sci.Templet = Request.Form["S_Templet"];                //专题模板路径
            string NPosion =Request.Form["S_Page"];
            //替换导航
            NPosion = (NPosion.Replace("{#URL}", SaveTP + "/" + DirPath + "/" + fName + Request.Form["S_FileExname"])).Replace("//", "/");
            sci.NaviPosition = NPosion;              //页面导航条
            sci.isLock = int.Parse(Request.Form["S_Lock"]);         //是否锁定

            sci.SpecialID = Foosun.Common.Rand.Number(12);
            sci.SiteID = SiteID;
            sci.CreatTime = DateTime.Now;
            sci.isRecyle = 0;

            Foosun.CMS.Special sc = new Foosun.CMS.Special();
            string result = sc.Add(sci);
            string[] arr_result = result.Split('|');
            if (arr_result[0].ToString() == "1")
            {
                Foosun.Publish.General PG = new Foosun.Publish.General();
                if (PG.publishSingleSpecial(arr_result[1].ToString()))
                    PageRight("添加专题成功!生成专题成功!", "Special_List.aspx");
                else
                    PageRight("添加专题成功!但是生成专题失败!", "Special_List.aspx");
            }
            else
                PageError("添加专题失败!", "");
        }
    }
}
