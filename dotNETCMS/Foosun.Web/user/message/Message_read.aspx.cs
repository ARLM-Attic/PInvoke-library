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
public partial class user_Message_read : Foosun.Web.UI.UserPage
{
    public string Userfiles = Foosun.Config.UIConfig.UserdirFile;
    Message mes = new Message();
    Foosun.CMS.Common.rootPublic pd = new Foosun.CMS.Common.rootPublic();
     protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string Mids = Request.QueryString["Mid"].ToString();
            MidID.Value = Mids;
            
            Response.CacheControl = "no-cache";
            string UserNum = Foosun.Global.Current.UserNum;
            DataTable dts1 = mes.sel_9(UserNum);
            this.DropDownList1.DataSource = dts1;
            this.DropDownList1.DataTextField = "UserName";
            this.DropDownList1.DataValueField = "FriendUserNum";
            this.DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("请选择", "0"));

            //查找会员所属会员组
            string u_meGroupNumber = mes.sel_10(UserNum);
            //是否可以群发
            DataTable u_MG = mes.sel_11(u_meGroupNumber);

            string[] MessageGroupNum = u_MG.Rows[0]["MessageGroupNum"].ToString().Split('|');
            int MessageGroupNum1 = int.Parse(MessageGroupNum[0].ToString());
            int MessageGroupNum2 = int.Parse(MessageGroupNum[1].ToString());

                if (MessageGroupNum1 == 1)
                {
                    this.FileTFLabel.Text = "本站支持群发多个会员之间用逗号分割开，最多只能发" + MessageGroupNum2 + "个";
                }
                else
                {
                    this.FileTFLabel.Text = "本站不支持群发";
                }

            string Mid = Foosun.Common.Input.Filter(Request.QueryString["Mid"].ToString());
            if (mes.sel_16(Mid)==0)
            {
                PageError("参数错误", "Message_box.aspx?Id=1");
            }
            mes.Update_7(Mid);
            DataTable read = mes.sel_17(Mid);
            this.TitleBox.Text = read.Rows[0]["Title"].ToString();
            this.ContentBox.Value = read.Rows[0]["Content"].ToString();
            int sp = int.Parse(read.Rows[0]["FileTF"].ToString());
            if (sp == 1)
            {
                //this.FileTFLabelp.Text = "下载附件";
                FileTFLabelp.InnerHtml = "<a href=\"Message_file.aspx?Mid=" + Mids + "\"  class=\"\" target=\"_self\">下载附件</a>";
            }
            else
            {
                //this.FileTFLabelp.Enabled = false;
                //this.FileTFLabelp.Text = "无附件";
                FileTFLabelp.InnerHtml = "无附件";

            }
            int lf = int.Parse(read.Rows[0]["LevelFlag"].ToString());
            if (lf == 0)
            {
                this.LevelFlagLabel.Text = "普通";
            }
            else if (lf == 1)
            {
                this.LevelFlagLabel.Text = "加急";
            }
            else
            {
                this.LevelFlagLabel.Text = "紧急";
            }
            this.Rec_UserNumBox.Text = mes.sel_18(read.Rows[0]["Rec_UserNum"].ToString());

        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string UserNum = Foosun.Global.Current.UserNum;
            string u_meGroupNumbero = mes.sel_10(UserNum);
            //可以发送多少条
            DataTable u_MGo = mes.sel_11(u_meGroupNumbero);
            int MessageNumo = 1;
            if (u_MGo.Rows[0]["MessageNum"].ToString() != "")
            {
                string[] MessageNumfgo = u_MGo.Rows[0]["MessageNum"].ToString().Split('|');
                MessageNumo = int.Parse(MessageNumfgo[0].ToString());
            }
            //查询会员已经发送多少条短信
            int cuto = mes.sel_12(UserNum);
            if (cuto >= MessageNumo)
            {
                PageError("您发送的邮件已经超过最大的发送数<br>", "Message_write.aspx");
            }
            else
            {
                string Midw = Request.QueryString["Mid"].ToString();
                int FileTF = int.Parse(mes.sel_20(Midw));
                string fileName = "";
                string FileUrl = "";
                if (FileTF == 1)
                {
                    DataTable reada = mes.sel_19(Midw);
                    fileName = reada.Rows[0]["FileName"].ToString();
                    FileUrl = reada.Rows[0]["FileUrl"].ToString();
                }
                string copyfilelist = FileUrl.Substring(FileUrl.LastIndexOf(".") + 1).ToUpper();
                string filenum = Foosun.Common.Rand.Number(12);//产生12位随机字符
                string UserNumfile = Foosun.Global.Current.UserNum;
                string Dir = System.Web.HttpContext.Current.Server.MapPath("~/" + Userfiles + "/" + UserNumfile + "/").ToString();
                string copyFileUrl = Dir + filenum + "." + copyfilelist;
                //查找会员所属会员组
                string u_meGroupNumber1 = mes.sel_10(UserNum);
                //是否可以群发
                DataTable u_MG1 = mes.sel_11(u_meGroupNumber1);
                string[] MessageGroupNums = u_MG1.Rows[0]["MessageGroupNum"].ToString().Split('|');
                int MessageGroupNums1 = int.Parse(MessageGroupNums[0].ToString());
                int MessageGroupNums2 = int.Parse(MessageGroupNums[1].ToString());
                DateTime CreatTime = DateTime.Now;//邮件撰写时间
                DateTime Send_DateTime = DateTime.Now;//邮件发送时间

                string Midp = Foosun.Common.Rand.Number(12,true);//产生12位随机字符
                int SortType = 0;
                string Title = Request.Form["TitleBox"].ToString();
                string Content = Foosun.Common.Input.Htmls(this.ContentBox.Value);
                int LevelFlagp = 0;
                if (this.LevelFlagLabel.Text == "加急"){LevelFlagp = 1;}
                else if (this.LevelFlagLabel.Text == "紧急"){LevelFlagp = 2;}
                if (MessageGroupNums1 == 1)
                {
                    bool sc = true;
                    string[] i = this.Rec_UserNumBox.Text.Split(',');
                    for (int s = 0; s < i.Length; s++)
                    {
                        DataTable dts = mes.sel_15(i[s]);
                        int cuts = dts.Rows.Count;
                        if (cuts == 0){continue;}
                        string Rec_UserNum = dts.Rows[0]["UserNum"].ToString();
                        Foosun.Model.message uc = new Foosun.Model.message();
                        uc.Mid = Midp;
                        uc.UserNum = UserNum;
                        uc.Title = Title;
                        uc.Content = Content;
                        uc.CreatTime = CreatTime;
                        uc.Send_DateTime = Send_DateTime;
                        uc.SortType = SortType;
                        uc.Rec_UserNum = Rec_UserNum;
                        uc.FileTF = FileTF;
                        uc.LevelFlag = LevelFlagp;
                        mes.Add(uc);
                    }
                    if (FileTF == 1)
                    {
                        string MfID = Foosun.Common.Rand.Number(12,true);//产生12位随机字符
                        if (mes.Add_1(MfID, Midp, UserNum, fileName, FileUrl, CreatTime) != 0){sc = true;}
                        else{sc = false;}
                    }
                    if (sc){PageRight("发送成功", "Message_box.aspx?Id=1");}
                    else{PageRight("发送成功.但附件未发送成功", "Message_box.aspx?Id=1");}
                }
                else
                {
                    string Rec_UserNuma = this.Rec_UserNumBox.Text;
                    if (Rec_UserNuma == Foosun.Global.Current.UserName)
                    {
                        PageError("不能给自己转发信息!", "Message_box.aspx?Id=1");
                    }
                    DataTable dts = mes.sel_15(Rec_UserNuma);
                    int cuts = dts.Rows.Count;
                    if (cuts == 0){PageError("收件用户不存在", "Message_box.aspx?Id=1");}
                    string Rec_UserNum = dts.Rows[0]["UserNum"].ToString();
                    string MfID = Foosun.Common.Rand.Number(12,true);//产生12位随机字符
                    Foosun.Model.message uc = new Foosun.Model.message();
                    uc.Mid = Midp;
                    uc.UserNum = UserNum;
                    uc.Title = Title;
                    uc.Content = Content;
                    uc.CreatTime = CreatTime;
                    uc.Send_DateTime = Send_DateTime;
                    uc.SortType = SortType;
                    uc.Rec_UserNum = Rec_UserNum;
                    uc.FileTF = FileTF;
                    uc.LevelFlag = LevelFlagp;
                    if (FileTF == 1)
                    {
                        if (!File.Exists(FileUrl))
                        {
                            FileTF = 0;
                        }
                        mes.Add(uc);
                        if (mes.Add_1(MfID, Midp, UserNum, filenum, copyFileUrl, CreatTime) != 0)
                        {
                            if (!File.Exists(FileUrl))
                            {
                                PageRight("转发送成功。<li>但因为附件已经被删除或者不存在，附件未转发成功!</li>", "Message_box.aspx?Id=1");
                            }
                            else
                            {
                                System.IO.File.Copy(FileUrl, copyFileUrl, false);
                                PageRight("转发送成功", "Message_box.aspx?Id=1");
                            }
                        }
                        else
                        {
                            PageRight("转发送失败", "Message_box.aspx?Id=1");
                        }                   
                    }
                    else
                    {
                        mes.Add(uc);
                        PageRight("转发送成功", "Message_box.aspx?Id=1");
                    }
                }
            }
        } 
    }
}