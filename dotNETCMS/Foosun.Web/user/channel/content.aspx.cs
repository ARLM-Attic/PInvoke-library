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
using Foosun.Model;

public partial class user_channel_content : Foosun.Web.UI.UserPage
{
    public string UDir = string.Empty;
    Channel rd = new Channel();
    rootPublic pd = new rootPublic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string ChID = Request.QueryString["ChID"];
            if (ChID == null && ChID == string.Empty)
            {
                PageError("错误的频道参数", "javascript:history.back()", true);
            }
            string DTable = rd.getChannelTable(int.Parse(ChID.ToString()));
            string id = Request.QueryString["id"];
            IDataReader gCdr = rd.ChannelInfo(int.Parse(ChID.ToString()));
            if (gCdr.Read())
            {
                if (id != null && id != string.Empty)
                {
                    //修改
                    channelName.InnerHtml = "<a class=\"list_link\" href=\"list.aspx?ChID=" + ChID + "\">" + gCdr["channelName"].ToString() + "</a> >> 修改信息";
                    IDataReader dr = rd.getContentAll(int.Parse(ChID.ToString()), int.Parse(id.ToString()));
                    if (dr.Read())
                    {
                        if (dr["author"].ToString() != Foosun.Global.Current.UserName)
                        {
                            PageError("您不能修改别人的信息", "javascript:history.back()", true);
                        }
                        GetValue(int.Parse(dr["id"].ToString()), int.Parse(ChID.ToString()), DTable);
                        GetClassList((this.ClassID), 0, 0, int.Parse(dr["ClassID"].ToString()));
                        this.title.Text = dr["title"].ToString();
                        this.PicURL.Text = dr["PicURL"].ToString();
                        this.naviContent.Text = dr["NaviContent"].ToString();
                        this.Content.Value = dr["Content"].ToString();
                        this.Souce.Text = dr["Souce"].ToString();
                        this.CTime.Value = dr["CreatTime"].ToString();
                        this.Tags.Text = dr["Tags"].ToString();
                    }
                    dr.Close(); 
                    
                }
                else
                {
                    //增加
                    int gClassID = 0;
                    if (Request.QueryString["ClassID"] != null && Request.QueryString["ClassID"] != string.Empty)
                    {
                        gClassID = int.Parse(Request.QueryString["ClassID"].ToString());
                    }
                    channelName.InnerHtml = "<a class=\"list_link\" href=\"list.aspx?ChID=" + ChID + "\">" + gCdr["channelName"].ToString() + "</a> >> 添加信息";
                    GetClassList((this.ClassID), 0, 0, gClassID);
                    GetValue(0, int.Parse(ChID.ToString()), DTable);
                }
            }
            gCdr.Close();
        }
    }


    protected void GetValue(int ID, int ChID, string DTable)
    {
        string list = "<table width=\"98%\" border=\"0\" cellpadding=\"3\" align=\"center\" cellspacing=\"1\" class=\"table\">";
        DataTable dt = rd.GetChannelUserValueFormInfo(ChID, DTable, ID);
        if (dt != null && dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                list += "<tr class=\"TR_BG_list\">\r";
                list += "<td style=\"width:100px;text-align:right;\">\r";
                list += dt.Rows[i]["CName"].ToString();
                list += "</td>\r";
                list += "<td>\r";
                list += GetValueType(dt.Rows[i], ID);
                list += "</td>\r";
                list += "</tr>\r";
            }
        }
        list += "</table>";
        dt.Clear(); dt.Dispose();
        definfo.InnerHtml = list;
    }

    protected string GetValueType(DataRow dr, int ID)
    {
        string list = string.Empty;
        string Nullstr = "";
        if (dr["isNulls"].ToString() == "0")
        {
            Nullstr = "<span class=\"reshow\" title=\"不能为空\">*</span>";
        }
        int vType = int.Parse(dr["vType"].ToString());
        string defalutValue = string.Empty;
        if (ID != 0) { defalutValue = dr[dr["EName"].ToString()].ToString(); }
        else { defalutValue = dr["vValue"].ToString(); }
        switch (vType)
        {
            case 0:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"20\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(20个字符)";
                break;
            case 1:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"50\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(50个字符)";
                break;
            case 2:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"100\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(100个字符)";
                break;
            case 3:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"180\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(180个字符)";
                break;
            case 4:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"225\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(225个字符)";
                break;
            case 5:
                list += "<textarea name=\"d_" + dr["EName"].ToString() + "\" style=\"height:" + dr["vHeight"].ToString() + "px;width:" + dr["vLength"].ToString() + "px;\">" + defalutValue + "</textarea>" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 6:
                list += "<select name=\"d_" + dr["EName"].ToString() + "\"  style=\"width:" + dr["vLength"].ToString() + "px;\">" + GetSelectValue("", defalutValue, dr["vitem"].ToString(), 0, 0) + "</select>" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 7:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"8\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(8位整数)";
                break;
            case 8:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"2\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(0~56的正整数)";
                break;
            case 9:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"8\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(货币类型)";
                break;
            case 10:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(长日期类型,格式：2007-12-20 15:10:10)";
                break;
            case 11:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"12\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(短日期类型,格式：2007-12-20)";
                break;
            case 12:
                list += GetSelectValue("d_" + dr["EName"].ToString(), defalutValue, dr["vitem"].ToString(), 1, 0) + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 13:
                list += GetSelectValue("d_" + dr["EName"].ToString(), defalutValue, dr["vitem"].ToString(), 1, 0) + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 14:
                list += GetSelectValue("d_" + dr["EName"].ToString(), defalutValue, dr["vitem"].ToString(), 2, 0) + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 15:
                list += GetSelectValue("d_" + dr["EName"].ToString(), defalutValue, dr["vitem"].ToString(), 2, 0) + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 16:
                list += "<select name=\"d_" + dr["EName"].ToString() + "\"  style=\"width:" + dr["vLength"].ToString() + "px;height:" + dr["vHeight"].ToString() + "px;\" multiple=\"multiple\">" + GetSelectValue("", defalutValue.Replace(",", "|") + "|", dr["vitem"].ToString(), 0, 1) + "</select>" + Nullstr + "&nbsp;" + dr["vDescript"].ToString() + "(按CTRL或者shift多选)";
                break;
            case 17:
                list += GETHTMLedit(dr["EName"].ToString(), dr["vHeight"].ToString(), dr["vLength"].ToString(), int.Parse(dr["HTMLedit"].ToString()), defalutValue) + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 30:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" /><img src=\"../../sysImages/folder/s.gif\" alt=\"选择附件\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('file',document.form1.d_" + dr["EName"].ToString() + ",380,500);document.form1.d_" + dr["EName"].ToString() + ".focus();\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 31:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" /><img src=\"../../sysImages/folder/s.gif\" alt=\"选择图片\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('pic',document.form1.d_" + dr["EName"].ToString() + ",380,500);document.form1.d_" + dr["EName"].ToString() + ".focus();\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 32:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" /><img src=\"../../sysImages/folder/s.gif\" alt=\"选择模板\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('templet',document.form1.d_" + dr["EName"].ToString() + ",380,500);document.form1.d_" + dr["EName"].ToString() + ".focus();\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 33:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" /><img src=\"../../sysImages/folder/s.gif\" alt=\"选择作者\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('Author',document.form1.d_" + dr["EName"].ToString() + ",250,400);document.form1.d_" + dr["EName"].ToString() + ".focus();\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 34:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" /><img src=\"../../sysImages/folder/s.gif\" alt=\"选择来源\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('Souce',document.form1.d_" + dr["EName"].ToString() + ",250,400);document.form1.d_" + dr["EName"].ToString() + ".focus();\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            case 35:
                list += "<input class=\"form\" type=\"text\" name=\"d_" + dr["EName"].ToString() + "\" maxlength=\"22\" value=\"" + defalutValue + "\" style=\"width:" + dr["vLength"].ToString() + "px;\" /><img src=\"../../sysImages/folder/s.gif\" alt=\"选择TAG\" border=\"0\" style=\"cursor:pointer;\" onclick=\"selectFile('Tag',document.form1.d_" + dr["EName"].ToString() + ",250,400);document.form1.d_" + dr["EName"].ToString() + ".focus();\" />" + Nullstr + "&nbsp;" + dr["vDescript"].ToString();
                break;
            default:
                list += "无自定义内容";
                break;
        }
        return list;
    }

    //得到编辑器
    protected string GETHTMLedit(string EName, string Height, string Width, int Num, string vValue)
    {
        string list = string.Empty;
        string EidtBase = "Foosun_User";
        list += "<textarea rows=\"1\" cols=\"1\" name=\"d_" + EName + "\" id=\"d_" + EName + "\" style=\"display:none;\">" + vValue + "</textarea>";
        list += "<script type=\"text/javascript\" language=\"JavaScript\">\r";
        list += "{\r";
        list += "var sBasePath = \"../../editor/\"\r";
        list += "var oFCKeditor = new FCKeditor('d_" + EName + "');\r";
        list += "oFCKeditor.BasePath = sBasePath;\r";
        list += "oFCKeditor.ToolbarSet = '" + EidtBase + "';\r";
        list += "oFCKeditor.Width = '" + Width + "px';\r";
        list += "oFCKeditor.Height = '" + Height + "px' ;\r";
        list += "oFCKeditor.ReplaceTextarea() ;\r";
        list += "}\r";
        list += "</script>\r";
        return list;
    }
    /// <summary>
    /// 得到复选，多选，列表框
    /// </summary>
    /// <param name="EName"></param>
    /// <param name="vValue"></param>
    /// <param name="vitem"></param>
    /// <param name="Num"></param>
    /// <param name="MLine"></param>
    /// <returns></returns>
    protected string GetSelectValue(string EName, string vValue, string vitem, int Num, int MLine)
    {
        string list = string.Empty;
        if (vitem.Trim() != string.Empty)
        {
            if (vitem.IndexOf("\r\n") > -1)
            {
                string[] Items = vitem.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < Items.Length; i++)
                {
                    switch (Num)
                    {
                        case 0:
                            if (MLine == 1)
                            {
                                if (vValue.IndexOf("|") > -1)
                                {
                                    if (vValue.IndexOf(Items[i] + "|".Trim()) > -1)
                                    {
                                        list += "<option selected=\"selected\" value=\"" + Items[i] + "\">" + Items[i] + "</option>\r";
                                    }
                                    else
                                    {
                                        list += "<option value=\"" + Items[i] + "\">" + Items[i] + "</option>\r";
                                    }
                                }
                                else
                                {
                                    if (vValue.Trim() != Items[i].Trim())
                                    {
                                        list += "<option  value=\"" + Items[i] + "\">" + Items[i] + "</option>\r";
                                    }
                                    else
                                    {
                                        list += "<option selected=\"selected\" value=\"" + Items[i] + "\">" + Items[i] + "</option>\r";
                                    }
                                }
                            }
                            else
                            {
                                if (vValue.Trim() != Items[i].Trim())
                                {
                                    list += "<option value=\"" + Items[i] + "\">" + Items[i] + "</option>\r";
                                }
                                else
                                {
                                    list += "<option selected=\"selected\" value=\"" + Items[i] + "\">" + Items[i] + "</option>\r";
                                }
                            }
                            break;
                        case 1:
                            if (vValue.Trim() != Items[i].Trim())
                            {
                                list += "<input type=\"radio\" name=\"" + EName + "\" value=\"" + Items[i] + "\" />" + Items[i] + "&nbsp;\r";
                            }
                            else
                            {
                                list += "<input type=\"radio\" name=\"" + EName + "\" checked=\"checked\" value=\"" + Items[i] + "\" />" + Items[i] + "&nbsp;\r";
                            }
                            break;
                        case 2:
                            if (vValue.IndexOf("|") > -1)
                            {
                                if (vValue.IndexOf(Items[i] + "|".Trim()) > -1)
                                {
                                    list += "<input type=\"checkbox\" name=\"" + EName + "\" checked=\"checked\" value=\"" + Items[i] + "\" />" + Items[i] + "&nbsp;\r";
                                }
                                else
                                {
                                    list += "<input type=\"checkbox\" name=\"" + EName + "\" value=\"" + Items[i] + "\" />" + Items[i] + "&nbsp;\r";
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                switch (Num)
                {
                    case 0:
                        list += "<option value=\"" + vitem + "\">" + vitem + "</option>\r";
                        break;
                    case 1:
                        if (vValue.Trim() != vitem.Trim())
                        {
                            list += "<input type=\"radio\" name=\"" + EName + "\" value=\"" + vitem + "\" />" + vitem + "&nbsp;\r";
                        }
                        else
                        {
                            list += "<input type=\"radio\" checked=\"checked\" name=\"" + EName + "\" value=\"" + vitem + "\" />" + vitem + "&nbsp;\r";
                        }
                        break;
                    case 2:
                        if (vValue.Trim() != vitem.Trim())
                        {
                            list += "<input type=\"checkbox\" name=\"" + EName + "\" value=\"" + vitem + "\" />" + vitem + "&nbsp;\r";
                        }
                        else
                        {
                            list += "<input type=\"checkbox\" checked=\"checked\" name=\"" + EName + "\" value=\"" + vitem + "\" />" + vitem + "&nbsp;\r";
                        }
                        break;
                }
            }
        }
        return list;
    }


    protected void GetClassList(DropDownList lst, int ParentID, int Layer, int gID)
    {
        IDataReader dr = rd.getClassList(ParentID, int.Parse(Request.QueryString["ChID"].ToString()));
        while (dr.Read())
        {
            ListItem it = new ListItem();
            string stxt = "";
            it.Value = dr["ID"].ToString();
            if (Layer > 0)
                stxt = "┝";
            for (int i = 1; i < Layer; i++)
            {
                stxt += " ┉ ";
            }
            it.Text = stxt + dr["ClassCName"].ToString();
            if (dr["ID"].ToString() == gID.ToString())
            {
                it.Selected = true;
            }
            lst.Items.Add(it);
            GetClassList(lst, int.Parse(dr["ID"].ToString()), (Layer + 1), gID);
        }
        dr.Close();
    }

    /// <summary>
    /// 保存数据
    /// </summary>
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int ChID = int.Parse(Request.QueryString["ChID"].ToString());
            string DTable = rd.getChannelTable(ChID);
            int ID = 0;
            if (Request.QueryString["id"] != string.Empty && Request.QueryString["id"] != null)
            {
                ID = int.Parse(Request.QueryString["id"]);
            }
            if (this.ClassID.SelectedValue == string.Empty)
            {
                PageError("请选择栏目", "javascript:history.back();", true);
            }
            IDataReader drtf = rd.GetDefineUserStyle(ChID);
            while (drtf.Read())
            {
                string FContent = Request.Form["d_" + drtf["EName"].ToString() + ""];
                if (drtf["isNulls"].ToString() != "1")
                {
                    if (FContent == string.Empty || FContent == null)
                    {
                        PageError("[" + drtf["CName"].ToString() + "] 必须填写!", "javascript:history.back();", true);
                    }
                }
            }
            drtf.Close();

            int ClassID = int.Parse(this.ClassID.SelectedValue);
            string SpecialID = string.Empty;
            string title = Foosun.Common.Input.Htmls(this.title.Text);
            if (title == string.Empty || title == null)
            {
                PageError("请填写信息标题", "javascript:history.back();", true);
            }
            int TitleITF = 0;
            int TitleBTF = 0;
            string PicURL = Foosun.Common.Input.Htmls(this.PicURL.Text);
            string NaviContent = Foosun.Common.Input.Htmls(this.naviContent.Text);
            string Content = Foosun.Common.Input.Htmls(this.Content.Value);
            string Author = Foosun.Global.Current.UserName;
            string Souce = Foosun.Common.Input.Htmls(this.Souce.Text);
            int OrderID = 0;
            string Tags = Foosun.Common.Input.Htmls(this.Tags.Text);
            int Click = 0;
            int isHTML = 0;
            int isConstr = 1;
            int islock = 0;
            IDataReader cr = rd.getModelinfo(ChID);
            if(cr.Read())
            {
                islock = int.Parse(cr["isCheck"].ToString());
            }
            cr.Close();
            string Templet = string.Empty;
            string SavePath = string.Empty;
            string FileName = string.Empty;
            if (ID == 0)
            {
                IDataReader dr = rd.GetClassInfo(ClassID);
                if (dr.Read())
                {
                    Templet = dr["ContentTemplet"].ToString();
                    SavePath = dr["ContentSavePath"].ToString();
                    FileName = dr["ContentFileNameRule"].ToString();
                }
                dr.Close();
            }
            string ContentProperty = "0|0|0|0|0";
            DateTime getTime = DateTime.Now;
            if (ID != 0)
            {
                getTime = DateTime.Parse(this.CTime.Value);
            }
            ChInfoContent uc = new ChInfoContent();
            uc.Id = ID;
            uc.ChID = ChID;
            uc.ClassID = ClassID;
            if (ID == 0)
            {
                uc.SpecialID = SpecialID;
                uc.TitleColor = string.Empty;
                uc.TitleITF = TitleITF;
                uc.TitleBTF = TitleBTF;
                uc.OrderID = OrderID;
                uc.Templet = Templet;
                uc.SavePath = pd.getResultPage(SavePath, getTime, ClassID.ToString(), "");
                uc.FileName = pd.getResultPage(FileName, getTime, ClassID.ToString(), "");
                uc.isDelPoint = 0;
                uc.Gpoint = 0;
                uc.iPoint = 0;
                uc.GroupNumber = string.Empty;
                uc.Metakeywords = string.Empty;
                uc.Metadesc = string.Empty;
                uc.Click = Click;
                uc.isHTML = isHTML;
                uc.ContentProperty = ContentProperty;
                uc.Editor = Foosun.Global.Current.UserName;
            }
            uc.title = title;
            uc.PicURL = PicURL;
            uc.NaviContent = NaviContent;
            uc.Content = Content;
            uc.Author = Author;
            uc.Souce = Souce;
            uc.Tags = Tags;
            uc.isConstr = isConstr;
            uc.islock = islock;
            if (ID != 0)
            {
                rd.updateUserContentInfo(uc, DTable);
                IDataReader drup = rd.GetDefineUserStyle(ChID);
                while (drup.Read())
                {
                    string FEName = drup["EName"].ToString();
                    string FContent = Request.Form["d_" + FEName + ""];
                    rd.updatePreContentInfo(ID, FEName, FContent, DTable);
                }
                drup.Close();
                PageRight("修改信息成功。<li><a href=\"Content.aspx?ChID=" + ChID.ToString() + "&id=" + ID.ToString() + "&ClassID=" + ClassID.ToString() + "\" class=\"reshow\">继续修改</a></li>", "list.aspx?ChID=" + ChID.ToString() + "&ClassID=" + ClassID + "", true);
            }
            else
            {
                int gID = rd.inserContentInfo(uc, DTable);
                IDataReader dr = rd.GetDefineUserStyle(ChID);
                while (dr.Read())
                {
                    string FEName = dr["EName"].ToString();
                    string FContent = Request.Form["d_" + FEName + ""];
                    rd.updatePreContentInfo(gID, FEName, FContent, DTable);
                }
                dr.Close();
                PageRight("添加信息成功。<li><a href=\"Content.aspx?ChID=" + ChID.ToString() + "&ClassID=" + ClassID.ToString() + "\" class=\"reshow\">继续添加</a></li><li><a href=\"Content.aspx?ChID=" + ChID.ToString() + "&id=" + gID.ToString() + "&ClassID=" + ClassID.ToString() + "\" class=\"list_link\">修改此条信息</a></li>", "list.aspx?ChID=" + ChID.ToString() + "&ClassID=" + ClassID + "", true);
            }
        }
    }
}
