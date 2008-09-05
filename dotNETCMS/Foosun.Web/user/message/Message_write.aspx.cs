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
public partial class user_Message_write : Foosun.Web.UI.UserPage
{
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    Message mes = new Message();
    #region  初始化
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            Response.CacheControl = "no-cache";
            string UserNum = Foosun.Global.Current.UserNum;
            this.UserNameBox.Text = Request.QueryString["uid"];
            DataTable dts1 = mes.sel_9(UserNum);
            this.DropDownList1.DataSource = dts1;
            this.DropDownList1.DataTextField = "UserName";
            this.DropDownList1.DataValueField = "FriendUserNum";
            this.DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("请选择", ""));

            //查找会员所属会员组
            string u_meGroupNumber = mes.sel_10(UserNum);
            //是否可以群发

            DataTable u_MG = mes.sel_11(u_meGroupNumber);

            string[] MessageGroupNum = u_MG.Rows[0]["MessageGroupNum"].ToString().Split('|');
            int MessageGroupNum1 = int.Parse(MessageGroupNum[0].ToString());
            int MessageGroupNum2 = int.Parse(MessageGroupNum[1].ToString());

            int MessageNum = 1;
            if (u_MG.Rows[0]["MessageNum"].ToString() != "")
            {
                string MessageNumfg = u_MG.Rows[0]["MessageNum"].ToString();
                MessageNum = int.Parse(MessageNumfg);
            }

            //查询会员已经发送多少条短信
            int cut = mes.sel_12(UserNum);

            if (cut >= MessageNum)
            {
                PageError("您发送的邮件已经超过最大的发送数.<li>请将邮件彻底删除再发送.</li>", "Message_box.aspx?Id=1");
            }
            else
            {
                if (MessageGroupNum1 == 1)
                {
                    this.Label1.Text = "你所在的组支持群发多个会员之间用\",\"分割开，最多只能发" + MessageGroupNum2 + "个.";
                }
                else
                {
                    this.Label1.Text = "你所在的组不支持群发.";
                }
            }
        }
    }
    #endregion
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            add();
        }
    }
    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    if (Page.IsValid)
    //    {
    //        add(1);
    //    }
    //}
    protected void add()
    {
        string UserNum = Foosun.Global.Current.UserNum;
        string MfID = Foosun.Common.Rand.Number(12);//产生14位随机字符
        //查找会员所属会员组
        string u_meGroupNumber1 = mes.sel_10(UserNum);
        //是否可以群发
        DataTable u_MG1 = mes.sel_11(u_meGroupNumber1);
        string[] MessageGroupNums = u_MG1.Rows[0]["MessageGroupNum"].ToString().Split('|');
        int MessageGroupNums1 = int.Parse(MessageGroupNums[0].ToString());
        int MessageGroupNums2 = int.Parse(MessageGroupNums[1].ToString());
        int MB = 102400;
        if (u_MG1.Rows[0]["MessageNum"].ToString() != "")
        {
            string MessageNumo = u_MG1.Rows[0]["MessageNum"].ToString();
            MB = int.Parse(MessageNumo);
        }
        DateTime CreatTime = DateTime.Now;//邮件撰写时间
        DateTime Send_DateTime = DateTime.Now;//邮件发送时间
        string Mid = Foosun.Common.Rand.Number(12,true);//产生12位随机字符
        string Title = Foosun.Common.Input.Htmls(Request.Form["TitleBox"].ToString());
        string Content = Foosun.Common.Input.Htmls(ContentBox.Value);
        int LevelFlag = this.LevelFlagList.SelectedIndex;
        string UserNumfile = Foosun.Global.Current.UserNum;
        int SortType = 0;
        if(this.CheckBox1.Checked){SortType = 1;}
        if (!Directory.Exists("~/" + Userfiles + "/" + UserNumfile + "/")){CreateFolder(UserNumfile);}
        //bool fileOK = false;
        string path = Server.MapPath("~/" + Userfiles + "/" + UserNumfile + "/");
        //if (MessFilesUpload.PostedFile.ContentLength > MB)
        //{ 
        //    fileOK = true;
        //}
        string fileName = Foosun.Common.Input.MD5(MfID) + MessFilesUpload.FileName;
        string FileUrl = "~/" + Userfiles + "/" + UserNumfile + "/" + fileName;

        int FileTF = 0;

        if (this.MessFilesUpload.HasFile)
        {
            FileTF = 1;
            MessFilesUpload.PostedFile.SaveAs(path + fileName);
        }
        if (MessageGroupNums1 == 1)
        {
            bool sc = true;
            string[] i = this.UserNameBox.Text.Split(',');
            for (int s = 0; s < i.Length; s++)
            {
                string str_MID = Foosun.Common.Rand.Number(12, true);
                DataTable dts = mes.sel_15(i[s]);
                int cuts = dts.Rows.Count;
                if (cuts==0){continue;}
                string Rec_UserNum = dts.Rows[0]["UserNum"].ToString();
                Foosun.Model.message uc = new Foosun.Model.message();
                uc.Mid = str_MID;
                uc.UserNum = UserNum;
                uc.Title = Title;
                uc.Content = Content;
                uc.CreatTime = CreatTime;
                uc.Send_DateTime = Send_DateTime;
                uc.SortType = SortType;
                uc.Rec_UserNum = Rec_UserNum;
                uc.FileTF = FileTF;
                uc.LevelFlag = LevelFlag;
                mes.Add(uc);
                if (this.MessFilesUpload.HasFile)
                {
                    if (mes.Add_1(MfID, str_MID, UserNum, fileName, FileUrl, CreatTime) != 0)
                    { sc = true; }
                    else { sc = false; }
                }
            }
            if (!sc)
            {
                PageRight("消息发送成功。<li>但附件发生错误。并未发送成功。</li>", "Message_box.aspx?Id=1");
            }
            else
            {
                PageRight("消息发送成功。", "Message_box.aspx?Id=1");
            }
        }
        else
        {
            string Rec_UserNuma = this.UserNameBox.Text;
            DataTable dts = mes.sel_15(Rec_UserNuma);
            int cuts = dts.Rows.Count;
            if (cuts == 0){PageError("收件用户不存在。", "Message_box.aspx?Id=1");}
            string Rec_UserNum = dts.Rows[0]["UserNum"].ToString();
            Foosun.Model.message uc = new Foosun.Model.message();
            uc.Mid = Mid;
            uc.UserNum = UserNum;
            uc.Title = Title;
            uc.Content = Content;
            uc.CreatTime = CreatTime;
            uc.Send_DateTime = Send_DateTime;
            uc.SortType = SortType;
            uc.Rec_UserNum = Rec_UserNum;
            uc.FileTF = FileTF;
            uc.LevelFlag = LevelFlag;
            if (this.MessFilesUpload.HasFile)
            {
                mes.Add(uc);
                if (mes.Add_1(MfID, Mid, UserNum, fileName, FileUrl, CreatTime) != 0)
                {
                    PageRight("发送成功", "Message_box.aspx?Id=1");
                }
                else
                {
                    PageRight("发送失败", "Message_box.aspx?Id=1");
                }
            }
            else
            {
                mes.Add(uc);
                if (mes.Add_1(MfID, Mid, UserNum, fileName, FileUrl, CreatTime) != 0 )
                {
                    PageRight("保存成功", "Message_box.aspx?Id=1");
                }
                else
                {
                    PageRight("保存失败", "Message_box.aspx?Id=1");
                }
            }
        }
    }

    public void CreateFolder(string FolderPathName)
    {
        if (FolderPathName.Trim().Length > 0)
        {
            try
            {
                string CreatePath = System.Web.HttpContext.Current.Server.MapPath("~/" + Userfiles + "/" + FolderPathName).ToString();
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
}