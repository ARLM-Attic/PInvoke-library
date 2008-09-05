using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using Foosun.Config;
using Foosun.Model;
using Foosun.Common;

namespace Foosun.Publish
{
    public partial class LabelMass
    {
        /// <summary>
        /// 讨论组标签
        /// </summary>
        /// <returns></returns>
        public string Analyse_GroupMember()
        {
            string str_Group = string.Empty;
            int n_Cols;
            if (!int.TryParse(this.GetParamValue("FS:Cols"), out n_Cols))
                n_Cols = 1;
            if (n_Cols < 1)
                n_Cols = 1;

            string str_GroupType = this.GetParamValue("FS:GroupType");
            string str_isDiv = this.GetParamValue("FS:isDiv");
            if (str_isDiv == null)
            {
                str_isDiv = "true";
            }
            string str_CSS = this.GetParamValue("FS:CSS");
            string str_ulID = this.GetParamValue("FS:ulID");
            string str_ulClass = this.GetParamValue("FS:ulClass");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_ShowM = this.GetParamValue("FS:ShowM");
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_NaviPic = this.GetParamValue("FS:NaviPic");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string Fileds = "";
            switch (str_GroupType)
            {
                case "hot":
                    Fileds = "Cnt1";
                    break;
                case "click":
                    Fileds = "Browsenumber";
                    break;
                case "Mmore":
                    Fileds = "Cnt1";
                    break;
                case "Last":
                    Fileds = "Creatime";
                    break;
                default:
                    Fileds = "Creatime";
                    break;
            }

            if (str_CSS != null)
                str_CSS = " class=\"" + str_CSS + "\"";
            string dirUser =Foosun.Config.UIConfig.dirUser;
            int i = 0;
            IDataReader rd = CommonData.DalPublish.GetDiscussInfo(str_GroupType,this.Param_Loop);
            while (rd.Read())
            {
                string row = "";
                string str_Cname = rd["Cname"].ToString();
                if (str_TitleNumer != null)
                    str_Cname = Foosun.Common.Input.GetSubString(str_Cname, Convert.ToInt32(str_TitleNumer));

                if (str_ShowM == "true")
                    row = getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + "<a href=\"" + CommonData.SiteDomain + "/" + dirUser + "/index.aspx?urls=discuss/discussTopi_list.aspx?DisID=" + rd["DisID"].ToString() + "" + "\" " + str_CSS + ">" + str_Cname + "</a> " + rd[Fileds].ToString();
                else
                    row = getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + "<a href=\"" + CommonData.SiteDomain + "/" + dirUser + "/index.aspx?urls=discuss/discussTopi_list.aspx?DisID=" + rd["DisID"].ToString() + "" + "\" " + str_CSS + ">" + str_Cname + "</a>";

                if (str_isDiv == "true")
                {
                    str_Group += "<li>" + newLine;
                    str_Group += row;
                    str_Group += "</li>" + newLine;
                }
                else
                {
                    str_isDiv = "false";
                    if (n_Cols == 1)
                    {
                        str_Group += "<tr>" + newLine + "<td>" + newLine + row + newLine + "</td>" + newLine + "</tr>" + newLine;
                    }
                    else
                    {
                        row = "<td width=\"" + (100 / n_Cols) + "%\">" + newLine + row + newLine + "</td>" + newLine;
                        if ((i > 0) && ((i + 1) % n_Cols == 0))
                            row = "</tr>" + newLine + "<tr>" + newLine;
                        str_Group += row;
                    }
                }
                i++;
            }
            rd.Close();
            if (i == 0)
                return str_Group;
            if (str_isDiv == "fasle")
            {
                if (str_Group != string.Empty && n_Cols > 1)
                {
                    str_Group = "<tr>" + newLine + str_Group;
                    if (i % n_Cols != 0)
                    {
                        int n = n_Cols - i;
                        if (n < 0)
                        {
                            n = n_Cols - (i % n_Cols);
                        }
                        for (int j = 0; j < n; j++)
                        {
                            str_Group += "<td width=\"" + (100 / n_Cols) + "%\">" + newLine + " </td>" + newLine;
                        }
                    }
                    str_Group += "</tr>" + newLine;
                }
                str_Group = News_List_Head(str_isDiv, str_ulID, str_ulClass) + str_Group + News_List_End(str_isDiv);
            }
            return str_Group;
        }

        /// <summary>
        /// 投稿标签
        /// </summary>
        /// <returns></returns>
        public string Analyse_ConstrNews()
        {
            return Analyse_List(null, "1");
        }
        /// <summary>
        /// 最新注册排行
        /// </summary>
        /// <returns></returns>
        public string Analyse_NewUser()
        {
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_NaviPic = this.GetParamValue("FS:NaviPic");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_CSS = this.GetParamValue("FS:CSS");
            string str_ShowDate = this.GetParamValue("FS:ShowDate");
            string classStr = "";
            if (str_CSS != null)
            {
                classStr = " class=\"" + str_CSS + "\"";
            }
            string str_NewUser = "<ul>" + newLine;
            DataTable dt = CommonData.DalPublish.GetSysUser(this.Param_Loop);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string getNavistr = getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i);
                    if (str_ShowDate == "right")
                    {
                        str_NewUser += "<li style=\"list-style:none;\"><span style=\"float:left\">" + getNavistr + " <a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + dt.Rows[i]["UserName"].ToString() + ".aspx\" " + classStr + ">" + dt.Rows[i]["UserName"].ToString() + "</a></span><span style=\"float:right\">" + dt.Rows[i]["RegTime"].ToString() + "</a></span></li>" + newLine;
                    }
                    else if (str_ShowDate == "left")
                    {
                        str_NewUser += "<li style=\"list-style:none;\"><span style=\"float:left\">" + getNavistr + " <a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + dt.Rows[i]["UserName"].ToString() + ".aspx\" " + classStr + ">" + dt.Rows[i]["UserName"].ToString() + "</a></span> <span>" + dt.Rows[i]["RegTime"].ToString() + "</a></span></li>" + newLine;
                    }
                    else
                    {
                        str_NewUser += "<li style=\"list-style:none;\"><span>" + getNavistr + "<a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + dt.Rows[i]["UserName"].ToString() + ".aspx\" " + classStr + ">" + dt.Rows[i]["UserName"].ToString() + "</a></span></li>" + newLine;
                    }
                }
                dt.Clear(); dt.Dispose();
            }
            else
            {
                str_NewUser += "<div><!--找不到记录--></div>" + newLine;
            }
            str_NewUser += "</ul>" + newLine;
            return str_NewUser;
        }

        /// <summary>
        /// 用户排行
        /// </summary>
        /// <returns></returns>
        public string Analyse_TopUser()
        {
            string str_TopUserType = this.GetParamValue("FS:TopUserType");
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_NaviPic = this.GetParamValue("FS:NaviPic");
            string str_CSS = this.GetParamValue("FS:CSS");
            string str_PointParam = this.GetParamValue("FS:PointParam");

            string classStr = "";
            if (str_CSS != null)
            {
                classStr = " class=\"" + str_CSS + "\"";
            }
            string str_NewUser = "";
            string Fileds = "";
            switch (str_TopUserType)
            {
                case "inter":
                    Fileds = "iPoint";
                    break;
                case "gpoint":
                    Fileds = "gPoint";
                    break;
                case "click":
                    Fileds = "ePoint";
                    break;
                case "info":
                    Fileds = "Cnt";
                    break;
                default:
                    Fileds = "RegTime";
                    break;
            }
            IDataReader rd = CommonData.DalPublish.GetTopUser(this.Param_Loop, Fileds);
            int i = 0;
            while (rd.Read())
            {
                if (str_PointParam == "right")
                {
                    str_NewUser += "<li style=\"list-style:none;\"><span style=\"float:left\">" + getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + " <a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + rd["UserName"].ToString() + ".aspx\" " + classStr + " title=\"昵称：" + rd["NickName"].ToString() + "\" target=\"_blank\">" + rd["UserName"].ToString() + "</a></span><span style=\"float:right\">" + rd[Fileds].ToString() + "</span></li>" + newLine;
                }
                else if (str_PointParam == "left")
                {
                    str_NewUser += "<li style=\"list-style:none;\">" + getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + " <a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + rd["UserName"].ToString() + ".aspx\" title=\"昵称：" + rd["NickName"].ToString() + "\" " + classStr + " target=\"_blank\">" + rd["UserName"].ToString() + "</a> " + rd[Fileds].ToString() + "</li>" + newLine;
                }
                else
                {
                    str_NewUser += "<li style=\"list-style:none;\">" + getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + " <a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + rd["UserName"].ToString() + ".aspx\" title=\"昵称：" + rd["NickName"].ToString() + "\" " + classStr + " target=\"_blank\">" + rd["UserName"].ToString() + "</a></li>" + newLine;
                }
                i++;
            }
            if (i < 1)
            {
                str_NewUser += "<li>找不到记录!</li>" + newLine;
            }
            rd.Close();
            return str_NewUser;
        }


        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <returns></returns>
        public string Analyse_UserLogin()
        {
            #region  判断是否开启整合
            string path = ServerInfo.GetRootPath().TrimEnd('\\');
            path += "\\api\\dz\\Adapt.config";
            AdaptConfig adaptConfig = new AdaptConfig(path);   
            #endregion

            string str_LoginP = this.GetParamValue("FS:LoginP");
            string str_FormCSS = this.GetParamValue("FS:FormCSS");
            string str_LoginCSS = this.GetParamValue("FS:LoginCSS");
            string str_RegCSS = this.GetParamValue("FS:RegCSS");
            string str_PassCSS = this.GetParamValue("FS:PassCSS");
            string str_StyleID = this.GetParamValue("FS:StyleID");
            string str_RandNum = Foosun.Common.Rand.Number(5);
            string str_UserLogin = "<div id=\"Div_UserInfo" + str_RandNum + "\">正在加载中..." + newLine;
            str_UserLogin += "</div>" + newLine;

            if (str_LoginP == null)
                str_LoginP = "true";

            str_UserLogin += "<script language=\"javascript\" type=\"text/javascript\">" + newLine;
            str_UserLogin += "function getLoginForm()" + newLine;
            str_UserLogin += "{" + newLine;
            str_UserLogin += "      var Action='Type=getLoginForm&RandNum=" + str_RandNum + "&LoginP=" + str_LoginP + "&FormCSS=" + str_FormCSS + "&LoginCSS=" + str_LoginCSS + "&RegCSS=" + Foosun.Common.Input.URLEncode(str_RegCSS) + "&PassCSS=" + Foosun.Common.Input.URLEncode(str_PassCSS) + "&StyleID=" + Foosun.Common.Input.URLEncode(str_StyleID) + "';" + newLine;
            str_UserLogin += "      var options={ " + newLine;
            str_UserLogin += "                  method:'get', " + newLine;
            str_UserLogin += "                  parameters:Action, " + newLine;
            str_UserLogin += "                  onComplete:function(transport) " + newLine;
            str_UserLogin += "                  { " + newLine;
            str_UserLogin += "                      var returnvalue=transport.responseText; " + newLine;
            str_UserLogin += "                      if (returnvalue.indexOf(\"??\")>-1) " + newLine;
            str_UserLogin += "                          alert('未知错误!请联系系统管理员'); " + newLine;
            str_UserLogin += "                      else " + newLine;
            str_UserLogin += "                          document.getElementById(\"Div_UserInfo" + str_RandNum + "\").innerHTML=returnvalue; " + newLine;
            str_UserLogin += "                   } " + newLine;
            str_UserLogin += "                   }; " + newLine;
            str_UserLogin += "      new  Ajax.Request('" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/UserLoginajax.aspx?no-cache='+Math.random(),options);" + newLine;
            str_UserLogin += "}" + newLine;

            str_UserLogin += "function LoginSubmit(obj)" + newLine;
            str_UserLogin += "{" + newLine;
            str_UserLogin += "      if(obj.UserNum.value==\"\"){alert('帐号不能为空');return false;}" + newLine;
            str_UserLogin += "      if(obj.UserPwd.value==\"\"){alert('密码不能为空');return false;}" + newLine;

            #region 整合，同步登录
            if (adaptConfig.isAdapt)
            {
                str_UserLogin += "      var adaptAction='username='+obj.UserNum.value+'&password='+obj.UserPwd.value+'&tag=login&StyleID=" + Foosun.Common.Input.URLEncode(str_StyleID) + "';" + newLine;
                str_UserLogin += "      var adaptOptions={" + newLine;
                str_UserLogin += "                  method:'get'," + newLine;
                str_UserLogin += "                  parameters:adaptAction" + newLine;
                str_UserLogin += "                   }; " + newLine;
                str_UserLogin += "      new Ajax.Request('" + adaptConfig.adaptPath + "?',adaptOptions);" + newLine;
            }
            #endregion

            str_UserLogin += "      var Action='UserNum='+obj.UserNum.value+'&UserPwd='+obj.UserPwd.value+'&Type=Login&RandNum=" + str_RandNum + "&LoginP=" + str_LoginP + "&StyleID=" + Foosun.Common.Input.URLEncode(str_StyleID) + "';" + newLine;
            str_UserLogin += "      var options={ " + newLine;
            str_UserLogin += "                  method:'get', " + newLine;
            str_UserLogin += "                  parameters:Action, " + newLine;
            str_UserLogin += "                  onComplete:function(transport) " + newLine;
            str_UserLogin += "                  { " + newLine;
            str_UserLogin += "                      var returnvalue=transport.responseText; " + newLine;
            str_UserLogin += "                      var returnvaluearr=returnvalue.split('$$$'); " + newLine;

            str_UserLogin += "                      if (returnvaluearr[0]==\"ERR\") " + newLine;
            str_UserLogin += "                          alert(returnvaluearr[1]); " + newLine;
            str_UserLogin += "                      else " + newLine;
            str_UserLogin += "                          document.getElementById(\"Div_UserInfo" + str_RandNum + "\").innerHTML=returnvaluearr[1]; " + newLine;
            str_UserLogin += "                   } " + newLine;
            str_UserLogin += "                   }; " + newLine;
            str_UserLogin += "      new  Ajax.Request('" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/UserLoginajax.aspx?no-cache='+Math.random(),options);" + newLine;
            str_UserLogin += "}" + newLine;

            str_UserLogin += "function LoginOut()" + newLine;
            str_UserLogin += "{" + newLine;

            #region 整合，同步退出
            if (adaptConfig.isAdapt)
            {
                str_UserLogin += "      var adaptAction='tag=logout&StyleID=" + Foosun.Common.Input.URLEncode(str_StyleID) + "';" + newLine;
                str_UserLogin += "      var adaptOptions={" + newLine;
                str_UserLogin += "                  method:'get'," + newLine;
                str_UserLogin += "                  parameters:adaptAction" + newLine;
                str_UserLogin += "                   }; " + newLine;
                str_UserLogin += "      new Ajax.Request('" + adaptConfig.adaptPath + "?',adaptOptions);" + newLine;
            }
            #endregion

            str_UserLogin += "      var Action='Type=LoginOut&LoginP=" + str_LoginP + "&StyleID=" + Foosun.Common.Input.URLEncode(str_StyleID) + "';" + newLine;
            str_UserLogin += "      var options={ " + newLine;
            str_UserLogin += "                  method:'get', " + newLine;
            str_UserLogin += "                  parameters:Action, " + newLine;
            str_UserLogin += "                  onComplete:function(transport) " + newLine;
            str_UserLogin += "                  { " + newLine;
            str_UserLogin += "                      var returnvalue=transport.responseText; " + newLine;
            str_UserLogin += "                      if (returnvalue.indexOf(\"??\")>-1) " + newLine;
            str_UserLogin += "                          alert('未知错误!请联系管理员'); " + newLine;
            str_UserLogin += "                      else " + newLine;
            str_UserLogin += "                          document.getElementById(\"Div_UserInfo" + str_RandNum + "\").innerHTML=returnvalue; " + newLine;
            str_UserLogin += "                   } " + newLine;
            str_UserLogin += "                   }; " + newLine;
            str_UserLogin += "      new  Ajax.Request('" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/UserLoginajax.aspx?no-cache='+Math.random(),options);" + newLine;
            str_UserLogin += "}" + newLine;

            str_UserLogin += "getLoginForm();" + newLine;

            str_UserLogin += "</script>" + newLine;
            return str_UserLogin;
        }

        /// <summary>
        /// 最新评论
        /// </summary>
        /// <returns></returns>
        public string Analyse_LastComm()
        {
            string str_TopComm = "";
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_CSS = this.GetParamValue("FS:CSS");
            string str_ShowDate = this.GetParamValue("FS:ShowDate"); 
            string str_NaviPic = this.GetParamValue("FS:NaviPic");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            if (str_CSS != null)
                str_CSS += " class=\"" + str_CSS + "\"";

            int i_tnumber = 20;
            if (str_TitleNumer != null)
                i_tnumber = Convert.ToInt32(str_TitleNumer);
            DataTable dt = CommonData.DalPublish.GetApiComm(this.Param_Loop);
            str_TopComm = "<ul>" + newLine;
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str_Title = Foosun.Common.Input.GetSubString(dt.Rows[i]["Content"].ToString(), i_tnumber);
                    string GUrls = CommonData.SiteDomain + "/Comment.aspx?CommentType=getlist&id=" + dt.Rows[i]["InfoID"].ToString();
                    if (str_ShowDate == "right")
                    {
                        str_TopComm += "<li style=\"list-style:none;\"><span style=\"float:left\">" + getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + " <a href=\"" + GUrls + "\" " + str_CSS + ">" + str_Title + "</a></span> <span style=\"float:right\">" + dt.Rows[i]["creatTime"].ToString() + "</span></li>" + newLine;
                    }
                    else if (str_ShowDate == "left")
                    {
                        str_TopComm += "<li style=\"list-style:none;\"><span style=\"float:left\">" + getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + " <a href=\"" + GUrls + "\" " + str_CSS + ">" + str_Title + "</a></span> <span>" + dt.Rows[i]["creatTime"].ToString() + "</span></li>" + newLine;
                    }
                    else
                    {
                        str_TopComm += "<li style=\"list-style:none;\"><span style=\"float:left\">" + getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i) + " <a href=\"" + GUrls + "\" " + str_CSS + ">" + str_Title + "</a></span></li>" + newLine;
                    }
                }
            }
            str_TopComm += "</ul>" + newLine;
            return str_TopComm;
        }


        /// <summary>
        /// 新闻排行
        /// </summary>
        /// <returns></returns>
        public string Analyse_TopNews()
        {
            string mystyle = this.Mass_Inserted;
            string styleid = Regex.Match(mystyle, @"\[\#FS:StyleID=(?<sid>[^\]]+)]", RegexOptions.Compiled).Groups["sid"].Value.Trim();
            if (!styleid.Equals(string.Empty))
            {
                mystyle = LabelStyle.GetStyleByID(styleid);
            }
            if (mystyle.Trim().Equals(string.Empty))
                return string.Empty;

            string str_TopNewsType = this.GetParamValue("FS:TopNewsType");
            string str_SubNews = this.GetParamValue("FS:SubNews");
            string str_ClassID = this.GetParamValue("FS:ClassID");
            int n_Cols;
            if (!int.TryParse(this.GetParamValue("FS:Cols"), out n_Cols))
                n_Cols = 1;
            if (n_Cols < 1)
                n_Cols = 1;
            string str_isDiv = this.GetParamValue("FS:isDiv");
            string str_ulID = this.GetParamValue("FS:ulID");
            string str_ulClass = this.GetParamValue("FS:ulClass");
            string str_isPic = this.GetParamValue("FS:isPic");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_ContentNumber = this.GetParamValue("FS:ContentNumber");
            string str_NaviNumber = this.GetParamValue("FS:NaviNumber");
            string str_isSub = this.GetParamValue("FS:isSub");
            bool subTF = false;
            if (str_SubNews != null)
            {
                if (str_SubNews == "true")
                {
                    subTF = true;
                }
            }
            string SqlCondition = " Where [isRecyle]=0 And [isLock]=0 And [SiteID]='" + this.Param_SiteID + "'";
            //-------判断是否调用图片
            if (str_isPic == "true")
                SqlCondition += " And [NewsType]=1";
            else if (str_isPic == "false")
                SqlCondition += " And([NewsType]=0 Or [NewsType]=2) ";

            //-------排序
            string SqlOrderBy = string.Empty;
            switch (str_TopNewsType)
            {
                case "Hour":
                    if (Foosun.Config.UIConfig.WebDAL == "foosun.accessdal")
                    {
                        SqlOrderBy += " And DateDiff('d',[CreatTime] ,now())=0 Order By [CreatTime]";
                    }
                    else
                    {
                        SqlOrderBy += " And DateDiff(Day,[CreatTime] ,Getdate())=0 Order By [CreatTime]";
                    }
                    break;
                case "YesDay":
                    if (Foosun.Config.UIConfig.WebDAL == "foosun.accessdal")
                    {
                        SqlOrderBy += " And DateDiff('d',[CreatTime] ,now())=1 Order By [CreatTime]";
                    }
                    else
                    {
                        SqlOrderBy += " And DateDiff(Day,[CreatTime] ,Getdate())=1 Order By [CreatTime]";
                    }
                    break;
                case "Week":

                    if (Foosun.Config.UIConfig.WebDAL == "foosun.accessdal")
                    {
                        SqlOrderBy += " And DateDiff('ww',[CreatTime] ,now())=0 Order By [CreatTime]";
                    }
                    else
                    {
                        SqlOrderBy += " And DateDiff(Week,[CreatTime] ,Getdate())=0 Order By [CreatTime]";
                    }
                    break;
                case "Month":

                    if (Foosun.Config.UIConfig.WebDAL == "foosun.accessdal")
                    {
                        SqlOrderBy += " And DateDiff('m',[CreatTime] ,now())=0 Order By [CreatTime]";
                    }
                    else
                    {
                        SqlOrderBy += " And DateDiff(Month,[CreatTime] ,Getdate())=0 Order By [CreatTime]";
                    }
                    break;
                case "Comm":
                    SqlOrderBy += " Order By (Select Count(ID) From [" + DBConfig.TableNamePrefix + "api_commentary] Where [" + DBConfig.TableNamePrefix + "api_commentary].[InfoID]=[" + DBConfig.TableNamePrefix + "News].[NewsID])";
                    break;
                case "disc":
                    SqlOrderBy += " Order By [CreatTime]";
                    break;
                default:
                    SqlOrderBy += " Order By [CreatTime]";
                    break;
            }
            SqlOrderBy += " Desc,[ID] Desc";
            #region 对栏目进行判断
            string Sql = string.Empty;
            if (str_ClassID == null || str_ClassID == "0")
            {
                if (this._TemplateType == TempType.Class)
                {
                    if (str_isSub == "true")
                        SqlCondition += " And [ClassID] In(" + getChildClassID(this.Param_CurrentClassID) + ")";
                    else
                        SqlCondition += " And [ClassID] In('" + this.Param_CurrentClassID + "')";
                    Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + SqlOrderBy;
                }
                else
                {
                    Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + SqlOrderBy;
                }
            }
            else if (str_ClassID == "-1")
            {
                Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + SqlOrderBy;
            }
            else
            {
                //-------判断是否调用子类
                if (str_isSub == "true")
                    SqlCondition += " And [ClassID] In(" + getChildClassID(str_ClassID) + ")";
                else
                    SqlCondition += " And [ClassID] In('" + str_ClassID + "')";
                Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + SqlOrderBy;
            }

            #endregion 对栏目进行判断

            int nTitleNum = 30, nContentNum = 200, nNaviNumber = 200;
            if (str_TitleNumer != null && Foosun.Common.Input.IsInteger(str_TitleNumer))
                nTitleNum = int.Parse(str_TitleNumer);
            if (str_ContentNumber != null && Foosun.Common.Input.IsInteger(str_ContentNumber))
                nContentNum = int.Parse(str_ContentNumber);
            if (str_NaviNumber != null && Foosun.Common.Input.IsInteger(str_NaviNumber))
                nNaviNumber = int.Parse(str_NaviNumber);

            DataTable dt = null;
            try
            {
                dt = CommonData.DalPublish.ExecuteSql(Sql);
            }
            catch
            {
                Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + " Order By [NewsID] Desc,[ID] Desc";
                dt = CommonData.DalPublish.ExecuteSql(Sql);
            }

            if (dt == null || dt.Rows.Count < 1) return string.Empty;
            string str_newslist = string.Empty;
            int i;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                if (str_isDiv == "true")
                {
                    str_newslist += Analyse_ReadNews((int)dt.Rows[i][0], nTitleNum, nContentNum, nNaviNumber, mystyle, styleid, 1, 1, 0);
                    //开始调用副新闻
                    if (subTF)
                    {
                        Foosun.Model.NewsContent sNCI = new Foosun.Model.NewsContent();
                        sNCI = this.getNewsInfo((int)dt.Rows[i][0], null);
                        str_newslist += getSubSTR(sNCI.NewsID,string.Empty);
                    }
                }
                else
                {
                    str_isDiv = "false";
                    string row = Analyse_ReadNews((int)dt.Rows[i][0], nTitleNum, nContentNum, nNaviNumber, mystyle, styleid, 1, 1, 0);
                    //开始调用副新闻
                    if (subTF)
                    {
                        Foosun.Model.NewsContent sNCI = new Foosun.Model.NewsContent();
                        sNCI = this.getNewsInfo((int)dt.Rows[i][0], null);
                        row += getSubSTR(sNCI.NewsID,string.Empty);
                    }
                    if (n_Cols == 1)
                        str_newslist += "<tr>" + newLine + "<td>" + newLine + row + newLine + "</td>" + newLine + "</tr>" + newLine;
                    else
                    {
                        row = "<td width=\"" + (100 / n_Cols) + "%\">" + newLine + row + newLine + "</td>" + newLine;
                        if ((i > 0) && ((i + 1) % n_Cols == 0))
                            row = "</tr>" + newLine + "<tr>" + newLine;
                        str_newslist += row;
                    }
                }
            }
            dt.Clear();
            dt.Dispose();
            if (str_newslist != string.Empty && n_Cols > 1)
            {
                str_newslist = "<tr>" + newLine + str_newslist;
                if (i % n_Cols != 0)
                {
                    int n = n_Cols - i;
                    if (n < 0)
                    {
                        n = n_Cols - (i % n_Cols);
                    }
                    for (int j = 0; j < n; j++)
                    {
                        str_newslist += "<td width=\"" + (100 / n_Cols) + "%\">" + newLine + " </td>" + newLine;
                    }
                }
                str_newslist += "</tr>" + newLine;
            }
            str_newslist = News_List_Head(str_isDiv, str_ulID, str_ulClass) + str_newslist + News_List_End(str_isDiv);
            return str_newslist;
        }

        /// <summary>
        /// RSS 
        /// </summary>
        /// <returns></returns>
        public string Analyse_RSS()
        {
            string str_RSS = "";
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string getClassID = "";
            bool booltf = false;
            if (str_ClassID != null)
            {
                if (str_ClassID == "0")
                {
                    booltf = true;
                }
                else if (str_ClassID == "-1")
                {
                    getClassID = this.Param_CurrentClassID;
                }
                else
                {
                    getClassID = str_ClassID;
                }
            }
            else
            {
                if (this.Param_CurrentClassID == null)
                {
                    booltf = true;
                }
                else
                {
                    getClassID = this.Param_CurrentClassID;
                }
            }

            if (booltf == true)
            {
                str_RSS += "<a title=\"订阅本站所有信息\" href=\"" + CommonData.SiteDomain + "/xml/content/all/news.xml\" target=\"blank\"><img src=\"" + CommonData.SiteDomain + "/sysImages/Label/preview/RSS.gif\" border=\"0\" alt=\"RSS图片\"></a>";
            }
            else
            {
                PubClassInfo info = CommonData.GetClassById(getClassID);
                if (info != null && info.IsURL == 0 && info.SiteID == this.Param_SiteID)
                {
                    str_RSS += "<a title=\"订阅" + info.ClassEName + "信息\" href=\"" + CommonData.SiteDomain + "/xml/content/" + info.ClassEName + ".xml\" target=\"blank\"><img src=\"" + CommonData.SiteDomain + "/sysImages/Label/preview/RSS.gif\" border=\"0\" alt=\"RSS图片\"></a>";
                }
            }
            return str_RSS;
        }

        /// <summary>
        /// 专题导读
        /// </summary>
        /// <returns></returns>
        public string Analyse_SpeicalNaviRead()
        {
            string str_NaviRead = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            string str_SpecialID = this.GetParamValue("FS:SpecialID");
            string str_SpecialTitleNumber = this.GetParamValue("FS:SpecialTitleNumber");
            string str_SpecialNaviTitleNumber = this.GetParamValue("FS:SpecialNaviTitleNumber");

            string specialid = "";
            if (str_SpecialID != null)
            {
                specialid = str_SpecialID;
            }
            else if (this.Param_CurrentSpecialID != null)
            {
                specialid = this.Param_CurrentSpecialID;
            }
            if (specialid != "")
            {
                PubSpecialInfo info = CommonData.GetSpecial(specialid);
                //IDataReader rd = CommonData.DalPublish.GetSpecialSavePath(specialid);
                if (info != null)
                {
                    string str_url = getSpeacilURL(info.isDelPoint.ToString(),info.SpecialID, info.SavePath, info.saveDirPath, info.FileName, info.FileEXName);

                    str_NaviRead += "<div>" + newLine;
                    if (str_SpecialTitleNumber != null)
                    {
                        str_NaviRead += "   <a href=\"" + str_url + "\" style=\"font-weight:bold;\">" + Foosun.Common.Input.GetSubString(info.SpecialCName, int.Parse(str_SpecialTitleNumber));
                    }
                    else
                    {
                        str_NaviRead += "   <a href=\"" + str_url + "\" style=\"font-weight:bold;\">" + info.SpecialCName;
                    }
                    str_NaviRead += "</div>" + newLine;

                    str_NaviRead += "<div>" + newLine;
                    if (str_SpecialNaviTitleNumber != null)
                    {
                        str_NaviRead += "   " + Foosun.Common.Input.GetSubString(info.NaviContent, int.Parse(str_SpecialNaviTitleNumber)) + "...<a href=\"" + str_url + "\">[详情]</a>";
                    }
                    else
                    {
                        str_NaviRead += "   " + info.NaviContent + "...<a href=\"" + str_url + "\">[详情]</a>";
                    }
                    str_NaviRead += "</div>" + newLine;
                }
                else
                {
                    str_NaviRead += "<tr><td><!--未找到此专题--></td></tr>" + newLine;
                }
            }
            else
                str_NaviRead += "<tr><td><!--未找到此专题--></td></tr>" + newLine;
            str_NaviRead += "</table>";
            return str_NaviRead;
        }

        ///// <summary>
        ///// 专题导航
        ///// </summary>
        ///// <returns></returns>
        //public string Analyse_SpecialNavi()
        //{
        //    string str_SpecialID = this.GetParamValue("FS:SpecialID");
        //    string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
        //    string str_NaviChar = this.GetParamValue("FS:NaviChar");
        //    string str_Mapp = this.GetParamValue("FS:Mapp");

        //    if (str_NaviCSS != null)
        //        str_NaviCSS = "class=\"" + str_NaviCSS + "\"";
        //    string Specialid = string.Empty;
        //    if (str_SpecialID != null)
        //    {
        //        Specialid = str_SpecialID;
        //    }
        //    else if (this.Param_CurrentSpecialID != null)
        //    {
        //        Specialid = this.Param_CurrentSpecialID;
        //    }
        //    string str_Navi = "";
        //    if (Specialid != string.Empty)
        //    {
        //        IList<PubSpecialInfo> list = CommonData.NewsSpecial;
        //        if (list != null)
        //        {
        //            str_Navi = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" + newLine;
        //            if (str_Mapp == "true")
        //                str_Navi += "<tr>" + newLine;
        //            foreach (PubSpecialInfo info in list)
        //            {
        //                if (info.ParentID == Specialid && info.SiteID == this.Param_SiteID)
        //                {
        //                    string str_url = getSpeacilURL(info.isDelPoint.ToString(),info.SpecialID, info.SavePath, info.saveDirPath, info.FileName, info.FileEXName);
        //                    if (str_Mapp == "true")
        //                    {
        //                        str_Navi += "   <td>" + str_NaviChar + " <a href=\"" + str_url + "\">" + newLine;
        //                        str_Navi += "   " + info.SpecialCName + "</a>";
        //                        str_Navi += "   </td>" + newLine;
        //                    }
        //                    else
        //                    {
        //                        str_Navi += "<tr>" + newLine;
        //                        str_Navi += "   <td>" + str_NaviChar + " <a href=\"" + str_url + "\">" + newLine;
        //                        str_Navi += "   " + info.SpecialCName + "</a>";
        //                        str_Navi += "   </td>" + newLine;
        //                        str_Navi += "</tr>" + newLine;
        //                    }
        //                }
        //            }
        //            if (str_Mapp.Equals("true"))
        //                str_Navi += "</tr>" + newLine;
        //            str_Navi += "</table>" + newLine;
        //        }
        //    }
        //    return str_Navi;
        //}

        

        /// <summary>
        /// 专题导航(wxh 2008-7-24)
        /// </summary>
        /// <returns></returns>
        public string Analyse_SpecialNavi()
        {
            string str_SpecialID = this.GetParamValue("FS:SpecialID");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_NaviChar = this.GetParamValue("FS:NaviChar");
            //string str_Mapp = this.GetParamValue("FS:Mapp");

            if (str_NaviCSS != null)
                str_NaviCSS = "class=\"" + str_NaviCSS + "\"";
            string Specialid = "0";
            if (str_SpecialID == "0")//自适应
            {
                if (!string.IsNullOrEmpty(Param_CurrentSpecialID))
                {
                    Specialid = Param_CurrentSpecialID;
                }
                else
                {
                    Specialid = "0";
                }
            }
            else if (str_SpecialID == "-1")//所有
            {
                Specialid = "-1";
            }
            else if (!string.IsNullOrEmpty(str_SpecialID)) //指定固定专题
            {
                Specialid = str_SpecialID;
            }
            else //防止出错
            {
                Specialid = "0";
            }
            string str_Navi = "";
            IList<PubSpecialInfo> list = CommonData.NewsSpecial;
            if (list != null)
            {
                foreach (PubSpecialInfo info in list)
                {
                    if (Specialid == "-1" && info.SiteID == this.Param_SiteID)//显示所有
                    {
                        string str_url = getSpeacilURL(info.isDelPoint.ToString(), info.SpecialID, info.SavePath, info.saveDirPath, info.FileName, info.FileEXName);
                        str_Navi += "   <li>" + str_NaviChar + " <a href=\"" + str_url + "\" " + str_NaviCSS + ">" + newLine;
                        str_Navi += "   " + info.SpecialCName + "</a>";
                        str_Navi += "   </li>" + newLine;
                    }
                    else if (info.ParentID == Specialid && info.SiteID == this.Param_SiteID)
                    {
                        string str_url = getSpeacilURL(info.isDelPoint.ToString(), info.SpecialID, info.SavePath, info.saveDirPath, info.FileName, info.FileEXName);
                        str_Navi += "   <li>" + str_NaviChar + " <a href=\"" + str_url + "\" " + str_NaviCSS + ">" + newLine;
                        str_Navi += "   " + info.SpecialCName + "</a>";
                        str_Navi += "   </li>" + newLine;
                    }
                }
            }
            return str_Navi;           
        }
        
        /// <summary>
        /// 栏目导读
        /// </summary>
        /// <returns></returns>
        public string Analyse_ClassNaviRead()
        {
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_ClassTitleNumber = this.GetParamValue("FS:ClassTitleNumber");
            string str_ClassNaviTitleNumber = this.GetParamValue("FS:ClassNaviTitleNumber");

            string classid = "";
            if (str_ClassID != null)
            {
                classid = str_ClassID;
            }
            else if (this.Param_CurrentClassID != null)
            {
                classid = this.Param_CurrentClassID;
            }
            string str_NaviRead = "";
            if (classid != "")
            {
                PubClassInfo info = CommonData.GetClassById(classid);
                if (info != null)
                {
                    string str_url = getClassURL(info.Domain,info.isDelPoint, info.ClassID, info.SavePath, info.SaveClassframe, info.ClassSaveRule);

                    str_NaviRead += "<div>" + newLine;
                    if (str_ClassTitleNumber != null)
                        str_NaviRead += "   <a href=\"" + str_url + "\" style=\"font-weight:bold;\">" + Foosun.Common.Input.GetSubString(info.ClassCName, int.Parse(str_ClassTitleNumber)) + "</a>";
                    else
                        str_NaviRead += "   <a href=\"" + str_url + "\" style=\"font-weight:bold;\">" + info.ClassCName + "</a>";
                    str_NaviRead += "</div>" + newLine;

                    str_NaviRead += "<div>" + newLine;

                    if (str_ClassNaviTitleNumber != null)
                        str_NaviRead += "   " + Foosun.Common.Input.GetSubString(info.NaviContent, int.Parse(str_ClassNaviTitleNumber)) + "...<a href=\"" + str_url + "\">[详情]</a>";
                    else
                        str_NaviRead += "   " + info.NaviContent + "...<a href=\"" + str_url + "\">[详情]</a>";
                    str_NaviRead += "</div>" + newLine;
                }
            }
            return str_NaviRead;
        }

        ///// <summary>
        ///// 栏目导航
        ///// </summary>
        ///// <returns></returns>
        //public string Analyse_ClassNavi()
        //{
        //    string str_ClassID = this.GetParamValue("FS:ClassID");
        //    string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
        //    string str_NaviChar = this.GetParamValue("FS:NaviChar");
        //    string str_Mapp = this.GetParamValue("FS:Mapp");

        //    if (str_NaviCSS != null)
        //    {
        //        str_NaviCSS = "class=\"" + str_NaviCSS + "\"";
        //    }
        //    string pcid = "";
        //    int P = 0;
        //    if (str_ClassID != null && str_ClassID != "-1" && str_ClassID!="0")
        //    {
        //        pcid = str_ClassID;
        //        P = 1;
        //    }
        //    else if (this.Param_CurrentClassID != null)
        //    {
        //        if (str_ClassID == "0")
        //        {
        //            pcid = this.Param_CurrentClassID;
        //            P = 1;
        //        }
        //    }
        //    string str_Navi = string.Empty;
        //    string str_gNaviChar = string.Empty;
        //    IList<PubClassInfo> list = CommonData.NewsClass;
        //    if (list != null && list.Count > 0)
        //    {
        //        int jm = 0;
        //        foreach (PubClassInfo info in list)
        //        {
        //            if (jm == 0)
        //            {
        //                str_gNaviChar = string.Empty;
        //            }
        //            else
        //            {
        //                str_gNaviChar = str_NaviChar;
        //            }
        //            if (P == 0)
        //            {
        //                string str_url = "";
        //                if (info.IsURL == 1)
        //                {
        //                    str_url = info.URLaddress;
        //                }
        //                else
        //                {
        //                    str_url = getClassURL(info.Domain,info.isDelPoint, info.ClassID, info.SavePath, info.SaveClassframe, info.ClassSaveRule);
        //                }
        //                str_Navi += "   <li>" + str_gNaviChar + " <a href=\"" + str_url + "\" " + str_NaviCSS + ">" + newLine;
        //                str_Navi += "   " + info.ClassCName + "</a>";
        //                str_Navi += "   </li>" + newLine;
        //            }
        //            else
        //            {
        //                if (info.ParentID.ToLower().Trim() == pcid.ToLower().Trim() && info.SiteID == Param_SiteID)
        //                {
        //                    string str_url = "";
        //                    if (info.IsURL == 1)
        //                    {
        //                        str_url = info.URLaddress;
        //                    }
        //                    else
        //                    {
        //                        str_url = getClassURL(info.Domain,info.isDelPoint, info.ClassID, info.SavePath, info.SaveClassframe, info.ClassSaveRule);
        //                    }
        //                    str_Navi += "   <li>" + str_gNaviChar + " <a href=\"" + str_url + "\" " + str_NaviCSS + ">" + newLine;
        //                    str_Navi += "   " + info.ClassCName + "</a>";
        //                    str_Navi += "   </li>" + newLine;
        //                }
        //            }
        //            jm++;
        //        }
        //    }
        //    return str_Navi;
        //}

        /// <summary>
        /// 栏目导航
        /// </summary>
        /// <returns></returns>         
        public string Analyse_ClassNavi()
        {
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_gNaviChar = this.GetParamValue("FS:NaviChar");
            string str_Mapp = this.GetParamValue("FS:Mapp");

            if (str_NaviCSS != null)
            {
                str_NaviCSS = "class=\"" + str_NaviCSS + "\"";
            }

            string ClassId = "0";
            if (str_ClassID == "0")//自适应
            {
                if (!string.IsNullOrEmpty(Param_CurrentClassID))
                {
                    ClassId = Param_CurrentClassID;
                }
                else
                {
                    ClassId = "0";
                }
            }
            else if (str_ClassID == "-1")//所有
            {
                ClassId = "-1";
            }
            else if (!string.IsNullOrEmpty(str_ClassID)) //指定固定专题
            {
                ClassId = str_ClassID;
            }
            else //防止出错
            {
                ClassId = "0";
            }
            string str_Navi = "";
            IList<PubClassInfo> list = CommonData.NewsClass;
            if (list != null)
            {
                foreach (PubClassInfo info in list)
                {
                    if (ClassId == "-1" && info.SiteID == this.Param_SiteID)//显示所有
                    {
                        string str_url = getClassURL(info.Domain, info.isDelPoint, info.ClassID, info.SavePath, info.SaveClassframe, info.ClassSaveRule);
                        str_Navi += "   <li>" + str_gNaviChar + " <a href=\"" + str_url + "\" " + str_NaviCSS + ">" + newLine;
                        str_Navi += "   " + info.ClassCName + "</a>";
                        str_Navi += "   </li>" + newLine;
                    }
                    else if (info.ParentID == ClassId && info.SiteID == this.Param_SiteID)
                    {
                        string str_url = getClassURL(info.Domain, info.isDelPoint, info.ClassID, info.SavePath, info.SaveClassframe, info.ClassSaveRule);
                        str_Navi += "   <li>" + str_gNaviChar + " <a href=\"" + str_url + "\" " + str_NaviCSS + ">" + newLine;
                        str_Navi += "   " + info.ClassCName + "</a>";
                        str_Navi += "   </li>" + newLine;
                    }
                }
            }
            return str_Navi;
        }

        /// <summary>
        /// 总站导航
        /// </summary>
        /// <returns></returns>
        public string Analyse_SiteNavi()
        {
            string str_SiteNavi = "";
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_NaviChar = this.GetParamValue("FS:NaviChar");
            string str_isDiv = this.GetParamValue("FS:isDiv");
            string nvchar2 = str_NaviChar;
            int ni = 0;
            if (str_isDiv == null)
            {
                str_isDiv = "true";
            }
            if (str_NaviCSS != null && str_NaviCSS != string.Empty)
            {
                str_NaviCSS = " class=\"" + str_NaviCSS + "\"";
            }
            IList<PubClassInfo> list = CommonData.NewsClass;
            foreach (PubClassInfo info in list)
            {
                if (info.ParentID == "0")
                {
                    string str_ClassUrl = "";
                    if (info.ClassCName != string.Empty && info.SavePath != string.Empty)
                    {
                        str_ClassUrl = getClassURL(info.Domain,info.isDelPoint, info.ClassID, info.SavePath, info.SaveClassframe, info.ClassSaveRule);
                    }
                    else
                    {
                        if (info.ClassSaveRule != string.Empty)
                        {
                            if (info.isPage == 1)
                            {
                                str_ClassUrl = CommonData.SiteDomain + "/" + info.SavePath;
                            }
                        }
                    }
                    if (ni == 0)
                    {
                        nvchar2 = "";
                    }
                    else
                    {
                        nvchar2 = str_NaviChar;
                    }
                    if (info.NaviShowtf == 1)
                    {
                        if (str_isDiv == "true")
                        {
                            str_SiteNavi += "<li " + str_NaviCSS + ">" + nvchar2 + " <a href=\"" + str_ClassUrl + "\">" + info.ClassCName + "</a></li>";
                        }
                        else
                        {
                            str_SiteNavi += nvchar2 + " <a href=\"" + str_ClassUrl + "\" " + str_NaviCSS + ">" + info.ClassCName + "</a> ";
                        }
                    }
                }
                ni++;
            }
            return str_SiteNavi;
        }
    }
}
