using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Foosun.Config;
using Foosun.Model;

namespace Foosun.Publish
{
    public partial class LabelMass
    {

        public static string dimmDir = Foosun.Config.UIConfig.dirDumm;

        /// <summary>
        /// 友情连接
        /// </summary>
        /// <returns></returns>
        public string Analyse_FrindList()
        {
            int int_Number = this.Param_Loop;
            string str_Cols = this.GetParamValue("FS:Cols");
            string str_FType = this.GetParamValue("FS:FType");
            string str_isAdmin = this.GetParamValue("FS:isAdmin");
            string str_isDiv = this.GetParamValue("FS:isDiv");
            string str_classID = this.GetParamValue("FS:TypeClassID");
            if (str_Cols == null || !Foosun.Common.Input.IsInteger(str_Cols))
            {
                str_Cols = "10";
            }
            if (str_FType == null || !Foosun.Common.Input.IsInteger(str_FType))
            {
                str_FType = "0";
            }
            if (str_isDiv == null)
            {
                str_isDiv = "true";
            }
            if (str_isAdmin == null || !Foosun.Common.Input.IsInteger(str_isAdmin))
            {
                str_isAdmin = "3";
            }
            string list = string.Empty;
            if (str_isDiv != "true")
            {
                list += "<table style=\"width:100%\" border=\"0\"><tr>";
            }
            int i = 0;
            IDataReader dr = CommonData.DalPublish.GetFriend(int.Parse(str_FType), int_Number, int.Parse(str_isAdmin));

            //bug修改  周峻平  为图片大小添加宽和高
            //查询友情链接数据表中图片的大小
            Foosun.DALFactory.IFrindLink frindLinks = Foosun.DALFactory.DataAccess.CreateFrindLink();
            DataTable dt = frindLinks.ParamStart();
            string size = dt.Rows[0]["ArrSize"].ToString();
            //判断是否有值
            if (size == null || size.Equals(""))//没有值,则使用默认大小80,30
            {
                size = "80,30";
            }

            string[] strSize = size.Split(',');
            //图片的宽
            string image_width = strSize[0];
            //图片的高
            string image_height = strSize[1];

            while (dr.Read())
            {
                if (dr["ClassID"].ToString() != null && dr["ClassID"].ToString().Equals(str_classID))
                {
                    if (str_isDiv == "true")
                    {
                        if (str_FType == "0")
                        {
                            //时间：2008-07-15  修改者：吴静岚
                            //增加友情连接是否打开新窗口参数设置 开始
                            list += "<li><a border='0' href=\"" + dr["Url"] + "\" target=\"" + Foosun.Config.UIConfig.Linktagertimg + "\"><img border='0' width=\"" + image_width + "\" height=\"" + image_height + "\" src=\"" + (dr["PicURL"].ToString().ToLower()).Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile) + "\" alt=\"" + dr["Name"].ToString() + "\" /></a></li>";
                        }
                        else
                        {
                            list += "<li><a border='0' href=\"" + dr["Url"] + "\" alt=\"" + dr["Name"].ToString() + "\" target=\"" + Foosun.Config.UIConfig.Linktagert + "\">" + dr["Name"].ToString() + "</a></li>";
                        }
                    }
                    else
                    {
                        if (str_FType == "0")
                        {
                            list += "<td><a border='0' href=\"" + dr["Url"] + "\" title=\"" + dr["Content"].ToString() + "\" target=\"" + Foosun.Config.UIConfig.Linktagertimg + "\"><img border='0' width=\"" + image_width + "\" height=\"" + image_height + "\" src=\"" + (dr["PicURL"].ToString().ToLower()).Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile) + "\" alt=\"" + dr["Name"].ToString() + "\" /></a></td>";
                        }
                        else
                        {
                            list += "<td><a border='0' href=\"" + dr["Url"] + "\" title=\"" + dr["Content"].ToString() + "\" target=\"" + Foosun.Config.UIConfig.Linktagert + "\">" + dr["Name"].ToString() + "</a></td>";
                            //结束 by wjl
                        }
                        if ((i + 1) % int.Parse(str_Cols) == 0)
                        {
                            list += "</tr><tr>";
                        }
                    }
                    i++;
                }
            }
            dr.Close();
            if (str_isDiv != "true")
            {
                list += "</tr></table>";
            }
            return list;
        }


        /// <summary>
        /// 归档
        /// </summary>
        /// <returns>返回列表</returns>
        public string Analyse_History()
        {
            ///返回一个日期选择器
            /////[FS:unLoop,FS:SiteID=0,FS:LabelType=History,FS:IsDate=true,FS:ShowDate=true][/FS:unLoop]
            string s_IsDate = this.GetParamValue("FS:IsDate");
            string s_ShowDate = this.GetParamValue("FS:ShowDate");
            string SaveIndexPage = "";
            IDataReader rd = CommonData.DalPublish.GetSysParam();
            if (rd.Read())
            {
                if (rd["SaveIndexPage"] != DBNull.Value)
                    SaveIndexPage = rd["SaveIndexPage"].ToString();
            }
            rd.Close();
            string hlist = "";
            if (s_ShowDate == "true") { hlist = getDateForm(SaveIndexPage); }
            else { hlist = getDateJs(SaveIndexPage); }
            return hlist;
        }

        /// <summary>
        /// 热点关键字
        /// </summary>
        /// <returns></returns>
        public string Analyse_HotTag()
        {
            return string.Empty;
        }

        /// <summary>
        /// 版权信息
        /// </summary>
        /// <returns>返回列表</returns>
        public string Analyse_CopyRight()
        {
            string CopyRight = string.Empty;
            IDataReader rd = CommonData.DalPublish.GetSysParam();
            if (rd.Read())
            {
                CopyRight = rd["CopyRight"].ToString();
            }
            rd.Close();
            return CopyRight;
        }
        /// <summary>
        /// 历史首页查询
        /// </summary>
        /// <returns></returns>
        public string Analyse_HistoryIndex()
        {
            string hstr = string.Empty;
            string h_year = string.Empty;
            string h_month = string.Empty;
            string h_day = string.Empty;
            for (int i = 2005; i <= DateTime.Now.Year; i++)
            {
                if (i == DateTime.Now.Year)
                {
                    h_year += "<option selected value=\"" + i + "\">" + i + "</option>\r";
                }
                else
                {
                    h_year += "<option value=\"" + i + "\">" + i + "</option>\r";
                }
            }
            for (int i1 = 1; i1 <= 12; i1++)
            {
                if (i1 == DateTime.Now.Month)
                {
                    h_month += "<option selected value=\"" + i1 + "\">" + i1 + "</option>\r";
                }
                else
                {
                    h_month += "<option value=\"" + i1 + "\">" + i1 + "</option>\r";
                }
            }
            for (int i2 = 1; i2 <= 31; i2++)
            {
                if (i2 == (DateTime.Now.Day - 1))
                {
                    h_day += "<option selected value=\"" + i2 + "\">" + i2 + "</option>\r";
                }
                else
                {
                    h_day += "<option value=\"" + i2 + "\">" + i2 + "</option>\r";
                }
            }
            hstr += "<div id=\"index_historyindexdiv\"><form method=\"POST\" id=\"index_historyindex1\"><select name=\"h_year\" id=\"h_year1\">" + h_year + "</select>年&nbsp;";
            hstr += "<select name=\"h_month\" id=\"h_month1\">" + h_month + "</select>月&nbsp;";
            hstr += "<select name=\"h_day\" id=\"h_day1\">" + h_day + "</select>日&nbsp;";
            hstr += "<input type=\"image\" name=\"imageFields\" src=\"" + CommonData.SiteDomain + "/sysimages/folder/buttonreview.gif\" onclick=\"s_getHistoryindex();return false;\" /></form></div>" + newLine;
            //hstr += "<input type=\"button\" name=\"Submit\" onclick=\"s_getHistoryindex();return false;\" value=\"查询\" /></form></div>" + newLine;
            hstr += "<script language=\"javascript\">" + newLine;
            hstr += "function s_getHistoryindex()" + newLine;
            hstr += "{" + newLine;
            hstr += "   var syear = index_historyindex1.h_year.options[index_historyindex1.h_year.selectedIndex].value;;" + newLine;
            hstr += "   var smonth = index_historyindex1.h_month.options[index_historyindex1.h_month.selectedIndex].value;" + newLine;
            hstr += "   var sday = index_historyindex1.h_day.options[index_historyindex1.h_day.selectedIndex].value;" + newLine;
            //hstr += "   window.open('" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirPige + "/index/\'+syear+smonth+sday+\'.shtml\','_blank');return false;" + newLine;
            hstr += "   window.open('" + CommonData.SiteDomain + "/history.aspx?year='+syear+'&month=' + smonth + '&day=' + sday +'','_blank');return false;" + newLine;
            hstr += "}" + newLine;
            hstr += "</script>";
            return hstr;
        }

        /// <summary>
        /// 得到归档类型，JS日期列表
        /// </summary>
        /// <returns>得到显示样式</returns>
        public string getDateJs(string SaveIndexPage)
        {
            return "<iframe src=\"" + CommonData.SiteDomain + "/configuration/historyjs.html?startDate=" + Foosun.Config.UIConfig.dirPigeDate + "&param=history/" + SaveIndexPage + "\" width=\"143px\" height=\"165px\" frameborder=\"0\" scrolling=\"no\"></iframe>";
        }

        /// <summary>
        /// 得到日期查询,表单类
        /// </summary>
        /// <returns>得到显示样式</returns>
        public string getDateForm(string SaveIndexPage)
        {
            string hstr = "";
            string h_year = "";
            string h_month = "";
            string h_day = "";
            for (int i = 2002; i <= DateTime.Now.Year; i++)
            {
                if (i == DateTime.Now.Year)
                {
                    h_year += "<option selected value=\"" + i + "\">" + i + "</option>\r";
                }
                else
                {
                    h_year += "<option value=\"" + i + "\">" + i + "</option>\r";
                }
            }
            for (int i1 = 1; i1 <= 12; i1++)
            {
                if (i1 == DateTime.Now.Month)
                {
                    h_month += "<option selected value=\"" + i1 + "\">" + i1 + "</option>\r";
                }
                else
                {
                    h_month += "<option value=\"" + i1 + "\">" + i1 + "</option>\r";
                }
            }
            for (int i2 = 1; i2 <= 31; i2++)
            {
                if (i2 == DateTime.Now.Day)
                {
                    h_day += "<option selected value=\"" + i2 + "\">" + i2 + "</option>\r";
                }
                else
                {
                    h_day += "<option value=\"" + i2 + "\">" + i2 + "</option>\r";
                }
            }
            hstr += "<div id=\"index_historydiv\"><form method=\"POST\" id=\"index_history1\"><select name=\"h_year\" id=\"h_year1\">" + h_year + "</select>年&nbsp;";
            hstr += "<select name=\"h_month\" id=\"h_month1\">" + h_month + "</select>月&nbsp;";
            hstr += "<select name=\"h_day\" id=\"h_day1\">" + h_day + "</select>日&nbsp;";
            hstr += "<input type=\"image\" name=\"imageFields\" src=\"" + CommonData.SiteDomain + "/sysimages/folder/buttonreview.gif\" onclick=\"s_getHistory();return false;\" /></form></div>" + newLine;
            hstr += "<script language=\"javascript\">" + newLine;
            hstr += "function s_getHistory()" + newLine;
            hstr += "{" + newLine;
            hstr += "   var syear = index_history1.h_year.options[index_history1.h_year.selectedIndex].value;;" + newLine;
            hstr += "   var smonth = index_history1.h_month.options[index_history1.h_month.selectedIndex].value;" + newLine;
            hstr += "   var sday = index_history1.h_day.options[index_history1.h_day.selectedIndex].value;" + newLine;
            hstr += "" + newLine;
            hstr += "   var sgetParam=\"" + SaveIndexPage + "\";" + newLine;
            hstr += "   var content=sgetParam;" + newLine;
            hstr += "   content=content.replace(\"{@year04}\",syear);" + newLine;
            hstr += "   content=content.replace(\"{@year02}\",syear.substring(2,4));" + newLine;
            hstr += "   content=content.replace(\"{@month}\",smonth);" + newLine;
            hstr += "   content=content.replace(\"{@day}\",sday);" + newLine;
            //hstr += "   window.open('" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirPige + "/\'+content+\'/index.html\','_blank');return false;" + newLine;
            hstr += "   window.open('" + CommonData.SiteDomain + "/history.aspx?year='+syear+'&month=' + smonth + '&day=' + sday +'','_blank');return false;" + newLine;
            hstr += "}" + newLine;
            hstr += "</script>";
            return hstr;
        }

        /// <summary>
        /// 相关新闻
        /// </summary>
        /// <returns></returns>
        public string Analyse_CorrNews()
        {
            string str_Tags = null;
            if (Param_CurrentNewsID != null && Param_CurrentNewsID != string.Empty)
            {
                str_Tags = CommonData.DalPublish.GetNewsTag(Param_CurrentNewsID);
                if (str_Tags != null && str_Tags != "")
                    return this.Analyse_List(str_Tags, null);
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 站点地图
        /// </summary>
        /// <returns></returns>
        public string Analyse_Sitemap()
        {
            //子类每行显示数量
            string dirfile = Foosun.Config.UIConfig.dirFile;
            string dimm = Foosun.Config.UIConfig.dirDumm;
            if (dimm.Trim() != string.Empty)
            {
                dimm = "/" + dimm;
            }
            int n_isSubCols = Convert.ToInt32(this.GetParamValue("FS:isSubCols"));
            if (n_isSubCols < 1)
                n_isSubCols = 100;
            //主类CSS
            string s_MapTitleCSS = this.GetParamValue("FS:MapTitleCSS");
            if (s_MapTitleCSS != null)
            {
                s_MapTitleCSS = " class=\"" + s_MapTitleCSS + "\"";
            }
            //子类CSS
            string s_SubCSS = this.GetParamValue("FS:SubCSS");
            if (s_SubCSS != null)
            {
                s_SubCSS = " class=\"" + s_SubCSS + "\"";
            }
            //true 为显示方式为横排，false为竖排
            string s_Mapp = this.GetParamValue("FS:Mapp");
            if (s_Mapp == null)
            {
                s_Mapp = "true";
            }
            string brStr = "";
            if (s_Mapp == "true")
            {
                brStr = "&nbsp;&nbsp;";
            }
            else
            {
                brStr = "<br />";
            }
            //标题导航图片(文字)
            string s_MapNavi = this.GetParamValue("FS:MapNavi");
            string s_MapNaviPic = this.GetParamValue("FS:MapNaviPic");
            string s_MapsubNavi = this.GetParamValue("FS:MapsubNavi");
            string s_MapsubNaviText = this.GetParamValue("FS:MapsubNaviText");
            string s_MapsubNaviPic = this.GetParamValue("FS:MapsubNaviPic");
            if (s_MapNaviPic == "true")
            {
                if (s_MapNaviPic != null)
                {
                    s_MapNaviPic = "<img src=\"" + s_MapNaviPic + "\" border=\"0\" />".Replace("{@dirfile}", dimm + dirfile);
                }
            }
            string s_MapNaviText = this.GetParamValue("FS:MapNaviText");
            if (s_MapsubNavi == "true")
            {
                if (s_MapsubNaviPic != null)
                {
                    s_MapsubNaviPic = "<img src=\"" + s_MapsubNaviPic + "\" border=\"0\" />".Replace("{@dirfile}", dimm + dirfile);
                }
            }
            string r = "";
            IDataReader rd = CommonData.DalPublish.GetNaviShowClass("0");
            while (rd.Read())
            {
                r += s_MapNaviPic + s_MapNaviText + "<a " + s_MapTitleCSS + " href=\"" + getClassURL(rd["Domain"].ToString(), Convert.ToInt32(rd["isDelPoint"]), rd["ClassID"].ToString(), rd["SavePath"].ToString(), rd["SaveClassFrame"].ToString(), rd["ClassSaveRule"].ToString()) + "\">" + rd["ClassCName"].ToString() + "</a>" + brStr;
                r += Recursion_Sitemap(rd["ClassID"].ToString(), brStr, s_SubCSS, s_MapsubNaviText, s_MapsubNaviPic);
            }
            rd.Close();
            return r;
        }

        /// <summary>
        /// 站点地图递归调用
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="PID"></param>
        /// <param name="Layer"></param>
        /// <returns></returns>
        protected string Recursion_Sitemap(string ParentID, string brStr, string s_SubCSS, string s_MapsubNaviText, string s_MapsubNavi)
        {
            string r = "";
            IDataReader rd = CommonData.DalPublish.GetNaviShowClass(ParentID);
            while (rd.Read())
            {
                r += s_MapsubNaviText + s_MapsubNavi + "<a " + s_SubCSS + " href=\"" + getClassURL(rd["Domain"].ToString(), Convert.ToInt32(rd["isDelPoint"]), rd["ClassID"].ToString(), rd["SavePath"].ToString(), rd["SaveClassFrame"].ToString(), rd["ClassSaveRule"].ToString()) + "\">" + rd["ClassCName"].ToString() + "</a>" + brStr;
            }
            rd.Close();
            return r;
        }

        /// <summary>
        /// 轮换幻灯片
        /// </summary>
        /// <returns></returns>
        public string Analyse_NorFilt()
        {
            string str_NorFilt = "";
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_isSub = this.GetParamValue("FS:isSub");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_WCSS = this.GetParamValue("FS:WCSS");
            string str_ShowTitle = this.GetParamValue("FS:ShowTitle");
            string str_FlashSize = this.GetParamValue("FS:FlashSize");
            string str_Target = this.GetParamValue("FS:Target");

            //string SqlFields = " [NewsID],[NewsTitle],[TitleColor],[TitleITF],[TitleBTF],[PicURL],[SPicURL],[ClassID],[SavePath],[FileName],[FileEXName],[DataLib] ";
            string SqlCondition = " Where [isRecyle]=0 And [isLock]=0 And [SiteID]='" + this.Param_SiteID + "' And [NewsType]=1 And SubString([NewsProperty],7,1)='1'";
            if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
            {
                SqlCondition = " Where [isRecyle]=0 And [isLock]=0 And [SiteID]='" + this.Param_SiteID + "' And [NewsType]=1 And mid([NewsProperty],7,1)='1'";
            }
            string SqlOrderBy = " Order By [CreatTime] Desc";

            #region 对栏目进行判断
            /*
            0表示：调用所有的新闻，不分classID
            -1表示：在哪个栏目调用哪个栏目的新闻，如果在首页或者在新闻页，则调用所有。
            如果CLASSID为空，则默认为-1 
            */
            DataTable dt = null;
            string Sql = string.Empty;
            if (str_ClassID == null || str_ClassID == "-1")
            {
                if (this._TemplateType == TempType.Class)
                {
                    if (str_isSub == "true")
                        SqlCondition += " And [ClassID] In (" + getChildClassID(this.Param_CurrentClassID) + ")";
                    Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + " And ClassID='" + this.Param_CurrentClassID + "' " + SqlOrderBy;
                }
                else
                {
                    Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + SqlOrderBy;
                }
            }
            else if (str_ClassID == "0")
            {
                Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
            }
            else
            {
                if (str_isSub == "true")
                    SqlCondition += " And [ClassID] In (" + getChildClassID(str_ClassID) + ")";
                Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
            }
            dt = CommonData.DalPublish.ExecuteSql(Sql);
            #endregion 对栏目进行判断

            if (dt != null)
            {
                if (dt.Rows.Count < 2)
                {
                    str_NorFilt = "至少需要两条幻灯新闻才能正确显示幻灯效果";
                    return str_NorFilt;
                }
                string str_FlashWidth = " width='200'";
                string str_FlashHeight = " height='100'";

                if (str_FlashSize != null)
                {
                    string[] arr_FlashSize = str_FlashSize.Split('|');
                    str_FlashWidth = " width='" + arr_FlashSize[0].ToString() + "'";
                    str_FlashHeight = " height='" + arr_FlashSize[1].ToString() + "'";
                }
                if (str_WCSS != null)
                    str_WCSS = " class='" + str_WCSS + "'";
                if (str_Target != null)
                    str_Target = " target='" + str_Target + "'";

                string str_Imgstr = "";
                string str_Linkstr = "";
                string str_Txtstr = "";
                string str_FirstTxt = "";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    PubClassInfo ci = CommonData.GetClassById(dt.Rows[i]["ClassID"].ToString());

                    string str_PicUrl = RelpacePicPath(dt.Rows[i]["PicURL"].ToString());
                    string str_Link = getNewsURL(dt.Rows[i]["isDelPoint"].ToString(), dt.Rows[i]["NewsID"].ToString(), dt.Rows[i]["SavePath"].ToString(), ci.SavePath + "/" + ci.SaveClassframe, dt.Rows[i]["FileName"].ToString(), dt.Rows[i]["FileEXName"].ToString());
                    string str_Txt = dt.Rows[i]["NewsTitle"].ToString();

                    if (str_TitleNumer != null)
                        str_Txt = Foosun.Common.Input.GetSubString(str_Txt, Convert.ToInt32(str_TitleNumer));

                    if (str_PicUrl != "" && str_PicUrl != null)
                    {
                        if (str_Imgstr == "")
                        {
                            str_Imgstr += str_PicUrl;
                            str_Linkstr += str_Link;
                            str_Txtstr += "<a href='" + str_Link + "' " + str_Target + " " + str_WCSS + ">" + str_Txt + "</a>";
                            str_FirstTxt = str_Txtstr;
                        }
                        else
                        {
                            str_Imgstr += "," + str_PicUrl;
                            str_Linkstr += "," + str_Link;
                            str_Txtstr += ",<a href='" + str_Link + "' " + str_Target + " " + str_WCSS + ">" + str_Txt + "</a>";
                        }
                    }
                }
                str_NorFilt += "<script language=\"vbscript\">" + newLine;
                str_NorFilt += "Dim FileList,FileListArr,TxtList,TxtListArr,LinkList,LinkArr" + newLine;
                str_NorFilt += "FileList = \"" + str_Imgstr + "\"" + newLine;
                str_NorFilt += "LinkList = \"" + str_Linkstr + "\"" + newLine;
                str_NorFilt += "TxtList = \"" + str_Txtstr + "\"" + newLine;
                str_NorFilt += "FileListArr = Split(FileList,\",\")" + newLine;
                str_NorFilt += "LinkArr = Split(LinkList,\",\")" + newLine;
                str_NorFilt += "TxtListArr = Split(TxtList,\",\")" + newLine;
                str_NorFilt += "Dim CanPlay" + newLine;
                str_NorFilt += "CanPlay = CInt(Split(Split(navigator.appVersion,\";\")(1),\" \")(2))>5" + newLine;
                str_NorFilt += "Dim FilterStr" + newLine;
                str_NorFilt += "FilterStr = \"RevealTrans(duration=2,transition=23)\"" + newLine;
                str_NorFilt += "FilterStr = FilterStr + \";BlendTrans(duration=2)\"" + newLine;
                str_NorFilt += "If CanPlay Then" + newLine;
                str_NorFilt += "FilterStr = FilterStr + \";progid:DXImageTransform.Microsoft.Fade(duration=2,overlap=0)\"" + newLine;
                str_NorFilt += "FilterStr = FilterStr + \";progid:DXImageTransform.Microsoft.Wipe(duration=3,gradientsize=0.25,motion=reverse)\"" + newLine;
                str_NorFilt += "Else" + newLine;
                str_NorFilt += "Msgbox \"幻灯片播放具有多种动态图片切换效果，但此功能需要您的浏览器为IE5.5或以上版本，否则您将只能看到部分的切换效果。\",64" + newLine;
                str_NorFilt += "End If" + newLine;
                str_NorFilt += "Dim FilterArr" + newLine;
                str_NorFilt += "FilterArr = Split(FilterStr,\";\")" + newLine;
                str_NorFilt += "Dim PlayImg_M" + newLine;
                str_NorFilt += "PlayImg_M = 5 * 1000  " + newLine;
                str_NorFilt += "Dim I" + newLine;

                str_NorFilt += "I = 1" + newLine;
                str_NorFilt += "Sub ChangeImg" + newLine;
                str_NorFilt += "Do While FileListArr(I)=\"\"" + newLine;
                str_NorFilt += "I = I + 1" + newLine;
                str_NorFilt += "If I>UBound(FileListArr) Then I = 0" + newLine;
                str_NorFilt += "Loop" + newLine;
                str_NorFilt += "Dim J" + newLine;
                str_NorFilt += "If I>UBound(FileListArr) Then I = 0" + newLine;
                str_NorFilt += "Randomize" + newLine;
                str_NorFilt += "J = Int(Rnd * (UBound(FilterArr)+1))" + newLine;
                str_NorFilt += "Img.style.filter = FilterArr(J)" + newLine;
                str_NorFilt += "Img.filters(0).Apply" + newLine;
                str_NorFilt += "Img.Src = FileListArr(I)" + newLine;
                str_NorFilt += "Img.filters(0).play" + newLine;
                str_NorFilt += "Link.Href = LinkArr(I)" + newLine;
                if (str_ShowTitle == "true")
                {
                    str_NorFilt += "Txt.filters(0).Apply" + newLine;
                    str_NorFilt += "Txt.innerHTML = TxtListArr(I)" + newLine;
                    str_NorFilt += "Txt.filters(0).play" + newLine;
                }
                str_NorFilt += "I = I + 1" + newLine;
                str_NorFilt += "If I>UBound(FileListArr) Then I = 0" + newLine;
                str_NorFilt += "TempImg.Src = FileListArr(I)" + newLine;
                str_NorFilt += "TempLink.Href = LinkArr(I)" + newLine;
                str_NorFilt += "SetTimeout \"ChangeImg\", PlayImg_M,\"VBScript\"" + newLine;
                str_NorFilt += "End Sub" + newLine;
                str_NorFilt += "</SCRIPT>" + newLine;
                str_NorFilt += "<TABLE WIDTH=\"100%\" height=\"100%\" BORDER=\"0\" CELLSPACING=\"0\" CELLPADDING=\"0\">" + newLine;
                str_NorFilt += "<TR ID=\"NoScript\">" + newLine;
                str_NorFilt += "<TD Align=\"Center\" Style=\"Color:White\">对不起，图片浏览功能需脚本支持，但您的浏览器已经设置了禁止脚本运行。请您在浏览器设置中调整有关安全选项。</TD>" + newLine;
                str_NorFilt += "</TR>" + newLine;
                str_NorFilt += "<TR Style=\"Display:none\" ID=\"CanRunScript\"><TD HEIGHT=\"100%\" Align=\"Center\" vAlign=\"Center\"><a id=\"Link\" " + str_Target + "><Img ID=\"Img\" " + str_FlashWidth + " " + str_FlashHeight + " Border=\"0\" ></a>" + newLine;
                str_NorFilt += "</TD></TR><TR Style=\"Display:none\"><TD><a id=TempLink ><Img ID=\"TempImg\" Border=\"0\"></a></TD></TR>" + newLine;
                if (str_ShowTitle == "true")
                {
                    str_NorFilt += "<TR><TD HEIGHT=\"100%\" Align=\"Center\" vAlign=\"Top\">" + newLine;
                    str_NorFilt += "<div ID=\"Txt\" style=\"PADDING-LEFT: 5px; Z-INDEX: 1; FILTER: progid:DXImageTransform.Microsoft.Fade(duration=1,overlap=0); POSITION:\">" + str_FirstTxt + "</div>" + newLine;
                    str_NorFilt += "</TD></TR>" + newLine;
                }
                str_NorFilt += "</TABLE>" + newLine;
                str_NorFilt += "<Script Language=\"VBScript\">" + newLine;
                str_NorFilt += "NoScript.Style.Display = \"none\"" + newLine;
                str_NorFilt += "CanRunScript.Style.Display = \"\"" + newLine;
                str_NorFilt += "Img.Src = FileListArr(0)" + newLine;
                str_NorFilt += "Link.Href = LinkArr(0)" + newLine;
                str_NorFilt += "SetTimeout \"ChangeImg\", PlayImg_M,\"VBScript\"" + newLine;
                str_NorFilt += "</Script>" + newLine;

                dt.Clear(); dt.Dispose();
            }
            else
            {
                str_NorFilt = "没有幻灯片";
            }
            return str_NorFilt;
        }

        /// <summary>
        /// Flash幻灯片
        /// </summary>
        /// <returns></returns>
        public string Analyse_FlashFilt()
        {
            string str_FlashFilt = "暂无幻灯新闻";

            string str_FlashType = this.GetParamValue("FS:FlashType");
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_Flashweight = this.GetParamValue("FS:Flashweight");
            string str_Flashheight = this.GetParamValue("FS:Flashheight");
            string str_FlashBG = this.GetParamValue("FS:FlashBG");
            string str_ShowTitle = this.GetParamValue("FS:ShowTitle");
            string str_isSub = this.GetParamValue("FS:isSub");

            if (str_Flashweight == null)
                str_Flashweight = "200";
            if (str_Flashheight == null)
                str_Flashheight = "150";
            if (str_FlashBG == null)
                str_FlashBG = "FFF";

            string SqlCondition = " Where [isRecyle]=0 And [isLock]=0 And [SiteID]='" + this.Param_SiteID + "' And [NewsType]=1 And SubString([NewsProperty],7,1)='1'";
            if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
            {
                SqlCondition = " Where [isRecyle]=0 And [isLock]=0 And [SiteID]='" + this.Param_SiteID + "' And [NewsType]=1 And mid([NewsProperty],7,1)='1'";
            }
            string SqlOrderBy = " Order By [CreatTime] Desc";

            #region 对栏目进行判断
            /*
            0表示：调用所有的新闻，不分classID
            -1表示：在哪个栏目调用哪个栏目的新闻，如果在首页或者在新闻页，则调用所有。
            如果CLASSID为空，则默认为-1 
            */
            DataTable dt = null;
            string Sql = string.Empty;
            if (str_ClassID != null)
            {
                switch (str_ClassID)
                {
                    case "0":
                        if (str_isSub == "true")
                        {
                            SqlCondition += " And [ClassID] In (" + getChildClassID(this.Param_CurrentClassID) + ")";
                        }
                        Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News] " + SqlCondition + " And ClassID='" + this.Param_CurrentClassID + "' " + SqlOrderBy;
                        break;
                    case "-1":
                        Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
                        break;
                    default:
                        if (str_isSub == "true")
                        {
                            SqlCondition += " And [ClassID] In(" + getChildClassID(str_ClassID) + ")";
                        }
                        else
                        {
                            SqlCondition += " And [ClassID] ='" + str_ClassID + "'";
                        }
                        Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
                        break;
                }
            }
            else
            {
                Sql = "select top " + Param_Loop + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
            }
            dt = CommonData.DalPublish.ExecuteSql(Sql);
            #endregion 对栏目进行判断

            string Pics_Path = "";
            string Link_Str = "";
            string Title_Str = "";

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        PubClassInfo ci = CommonData.GetClassById(dt.Rows[i]["ClassID"].ToString());
                        Pics_Path += dt.Rows[i]["PicURL"].ToString() + "|";
                        Link_Str += getNewsURL(dt.Rows[i]["isDelPoint"].ToString(), dt.Rows[i]["newsID"].ToString(), dt.Rows[i]["SavePath"].ToString(), ci.SavePath + "/" + ci.SaveClassframe, dt.Rows[i]["FileName"].ToString(), dt.Rows[i]["FileEXName"].ToString()) + "|";
                        Title_Str += dt.Rows[i]["NewsTitle"].ToString() + "|";
                    }
                }
            }
            dt.Clear(); dt.Dispose();
            Pics_Path = Foosun.Common.Input.CutComma(Pics_Path, "|");
            Pics_Path = RelpacePicPath(Pics_Path);
            Link_Str = Foosun.Common.Input.CutComma(Link_Str, "|");
            Title_Str = Foosun.Common.Input.CutComma(Title_Str, "|");

            if (Pics_Path != string.Empty && Link_Str != string.Empty && Title_Str != string.Empty)
            {
                string[] P_Arr = Pics_Path.Split('|');
                string[] L_Arr = Link_Str.Split('|');
                string[] T_Arr = Title_Str.Split('|');
                if (P_Arr.Length == L_Arr.Length && P_Arr.Length == T_Arr.Length)
                {
                    if (P_Arr.Length < 2)
                    {
                        str_FlashFilt = "flash幻灯至少要两条以上才可以显示";
                    }
                    else
                    {
                        string SwfFilePath = CommonData.SiteDomain + "/Flash.swf";

                        str_FlashFilt = "<script language=\"javascript\" type=\"text/javascript\">" + newLine;
                        str_FlashFilt += "<!--" + newLine;
                        str_FlashFilt += "var Flash_Width = " + str_Flashweight + ";" + newLine;
                        str_FlashFilt += "var Flash_Height = " + str_Flashheight + ";" + newLine;
                        if (str_ShowTitle == "true")
                            str_FlashFilt += "var Txt_Height = 20;" + newLine;
                        else
                            str_FlashFilt += "var Txt_Height = 0;" + newLine;
                        str_FlashFilt += "var Swf_Height = parseInt(Flash_Height + Txt_Height);" + newLine;
                        str_FlashFilt += "var Pics_ = '" + Pics_Path + "';" + newLine;
                        str_FlashFilt += "var Links_ = '" + Link_Str + "';" + newLine;
                        str_FlashFilt += "var Texts_ = '" + Title_Str + "';" + newLine;
                        str_FlashFilt += "document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"'+ Flash_Width +'\" height=\"'+ Swf_Height +'\">');" + newLine;
                        str_FlashFilt += "document.write('<param name=\"allowScriptAccess\" value=\"sameDomain\"><param name=\"movie\" value=\"" + SwfFilePath + "\"><param name=\"quality\" value=\"high\"><param name=\"bgcolor\" value=\"#" + str_FlashBG + "\">');" + newLine;
                        str_FlashFilt += "document.write('<param name=\"menu\" value=\"false\"><param name=\"wmode\" value=\"opaque\">');" + newLine;
                        str_FlashFilt += "document.write('<param name=\"FlashVars\" value=\"pics='+Pics_+'&links='+Links_+'&texts='+Texts_+'&borderwidth='+Flash_Width+'&borderheight='+Flash_Height+'&textheight='+Txt_Height+'\">');" + newLine;
                        str_FlashFilt += "document.write('<embed src=\"" + SwfFilePath + "\" wmode=\"opaque\" FlashVars=\"pics='+Pics_+'&links='+Links_+'&texts='+Texts_+'&borderwidth='+Flash_Width+'&borderheight='+Flash_Height+'&textheight='+Txt_Height+'\" menu=\"false\" bgcolor=\"#" + str_FlashBG + "\" quality=\"high\" width=\"'+ Flash_Width +'\" height=\"'+ Swf_Height +'\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');" + newLine;
                        str_FlashFilt += "document.write('</object>');" + newLine;
                        str_FlashFilt += "//-->" + newLine;
                        str_FlashFilt += "</script>" + newLine;
                    }
                }
            }
            return str_FlashFilt;
        }


        /// <summary>
        /// 信息统计标签
        /// </summary>
        /// <returns></returns>
        public string Analyse_Stat()
        {
            string Statstr = "";

            return Statstr;
        }
        /// <summary>
        /// 搜索
        /// </summary>
        /// <returns></returns>
        public string Analyse_Search()
        {
            string str_Search = "";
            string str_RnadNum = Foosun.Common.Rand.Number(5);
            string str_SearchType = this.GetParamValue("FS:SearchType");
            string str_ShowDate = this.GetParamValue("FS:ShowDate");
            string str_ShowClass = this.GetParamValue("FS:ShowClass");

            str_Search += "<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">" + newLine;
            str_Search += "<form id=\"Search_Form\" name=\"Search_Form\" method=\"get\" action=\"search.html\">" + newLine;
            str_Search += "<tr>" + newLine;
            str_Search += "<td>" + newLine;
            str_Search += "<input name=\"tags\" type=\"text\"  size=\"10\" maxlength=\"20\" onkeydown=\"javascript:if(event.keyCode==13){SearchGo" + str_RnadNum + "(this.form);}\" />";
            if (str_SearchType == "true")
            {
                if (str_ShowDate == "true")
                {
                    str_ShowDate = " <select name=\"Date\">" + newLine;
                    str_ShowDate += "<option value=\"0\">不限制</otpion>" + newLine;
                    str_ShowDate += "<option value=\"1\">最近一天</otpion>" + newLine;
                    str_ShowDate += "<option value=\"3\">最近三天</otpion>" + newLine;
                    str_ShowDate += "<option value=\"7\">最近一周</otpion>" + newLine;
                    str_ShowDate += "<option value=\"30\">最近一月</otpion>" + newLine;
                    str_ShowDate += "<option value=\"180\">最近半年</otpion>" + newLine;
                    str_ShowDate += "</select>" + newLine;
                }
                else
                    str_ShowDate = "";
                if (str_ShowClass == "true")
                {
                    IList<PubClassInfo> list = CommonData.NewsClass;
                    if (list != null)
                    {
                        str_ShowClass = " <select name=\"ClassID\"><option vlaue=\"\">请选择</option>" + newLine + ChildList(list, "0", "├") + "</select>";
                    }
                    else
                        str_ShowClass = " <select name=\"ClassID\"><option vlaue=\"\">当前没有栏目</option></select>";
                }
                else
                    str_ShowClass = "";
                str_Search += str_ShowDate + str_ShowClass;
            }
            str_Search += " <input name=\"buttongo\" type=\"button\" value=\"搜索\" onclick=\"javascript:SearchGo" + str_RnadNum + "(this.form);\">";
            str_Search += "</td>" + newLine;
            str_Search += "</tr>" + newLine;
            str_Search += "</form>" + newLine;
            str_Search += "</table>" + newLine;

            str_Search += "<script language=\"javascript\" type=\"text/javascript\">" + newLine;
            str_Search += "function SearchGo" + str_RnadNum + "(obj)" + newLine;
            str_Search += "{" + newLine;
            int minlen = 0;
            int maxlen = 20;
            string LenSearch = Foosun.Common.Public.readparamConfig("LenSearch");
            minlen = int.Parse(LenSearch.Split('|')[0]);
            maxlen = int.Parse(LenSearch.Split('|')[1]);
            str_Search += "if(obj.tags.value.length<" + minlen + "||obj.tags.value.length>" + maxlen + ")" + newLine;
            str_Search += "{" + newLine;
            str_Search += " alert('搜索最小长度" + minlen + "字符，最大长度" + maxlen + "字符。');return false;" + newLine;
            str_Search += "}" + newLine;
            str_Search += "if(obj.tags.value=='')" + newLine;
            str_Search += "{" + newLine;
            str_Search += " alert('请填写关键字');return false;" + newLine;
            str_Search += "}" + newLine;
            if (str_SearchType == "true")
            {
                str_Search += "window.location.href='" + CommonData.SiteDomain + "/Search.html?type=news";
                if (str_ShowDate == "true")
                    str_Search += "&Date='+obj.Date.value+'";
                if (str_ShowClass == "true")
                    str_Search += "&ClassID='+obj.ClassID.value+'";
                str_Search += "&tags='+escape(obj.tags.value)+'';" + newLine;
            }
            else
                str_Search += "window.location.href='" + CommonData.SiteDomain + "/Search.html?type=news&tags='+obj.tags.value+'';" + newLine;
            str_Search += "}" + newLine;
            str_Search += "</script>" + newLine;
            return str_Search;
        }

        /// <summary>
        /// 位置导航
        /// </summary>
        /// <returns></returns>
        public string Analyse_Position(int ChID)
        {
            string str_Position = string.Empty;
            string str_DynChar = this.GetParamValue("FS:DynChar");
            if (str_DynChar == null)
            {
                str_DynChar = " >> ";
            }
            string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
            switch (this.TemplateType.ToString())
            {
                #region 新闻频道
                case "Index":
                    str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>";
                    break;
                case "News":
                    string ClassID = CommonData.DalPublish.GetClassIDByNewsID(this.Param_CurrentNewsID);
                    PubClassInfo ci = CommonData.GetClassById(ClassID);
                    if (ci != null)
                    {
                        if (ci.isDelPoint != 0 || ReadType == "1")
                        {
                            str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + getPositionSTR(str_DynChar, ClassID, 0) + "正文";
                        }
                        else
                        {
                            str_Position = ci.NewsPosition;
                        }
                    }
                    else
                    {
                        str_Position = string.Empty;
                    }
                    break;
                case "Class":
                    PubClassInfo ci1 = CommonData.GetClassById(this.Param_CurrentClassID);
                    if (ci1.isDelPoint != 0 || ReadType == "1")
                    {
                        str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + getPositionSTR(str_DynChar, this.Param_CurrentClassID, 0) + "列表";
                    }
                    else
                    {
                        str_Position = ci1.NaviPosition;
                    }
                    break;
                case "Special":
                    PubSpecialInfo si = CommonData.GetSpecial(this.Param_CurrentSpecialID);
                    if (si.isDelPoint != 0 || ReadType == "1")
                    {
                        str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + getPositionSTR(str_DynChar, this.Param_CurrentSpecialID, 1) + "列表";
                    }
                    else
                    {
                        //根据父节点得到数据
                        Foosun.DALFactory.ISpecial DalPublish = Foosun.DALFactory.DataAccess.CreateSpecial();
                        string parentIDs = si.ParentID;
                        DataTable thisData = null;
                        DataRow thisDr = null;
                        string NPosions = null;//"<a href=\"/\" target=\"_blank\">首页</a>"
                        while (!parentIDs.Equals("0"))
                        {
                            thisData = DalPublish.getSpeacilInfo(parentIDs);
                            thisDr = thisData.Rows[0];
                            //设置父节点的ID
                            parentIDs = thisDr["ParentID"].ToString();
                            NPosions = " >> <a href=\"" + thisDr["SavePath"] + "/" + thisDr["saveDirPath"] + "/" + thisDr["FileName"] + thisDr["FileEXName"] + "\">" + thisDr["SpecialCName"] + "</a>" + NPosions;
                        }
                        str_Position = "<a href=\"/\" target=\"_blank\">首页</a>" + NPosions + " >> 专题列表";
                    }
                    break;
                #endregion 新闻频道的
                case "ChIndex":
                    IDataReader dr = CommonData.DalPublish.GetPositionNavi(0, "ChIndex", ChID);
                    if (dr.Read())
                    {
                        if (ReadType == "1")
                        {
                            str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + "<a href=\"" + CommonData.SiteDomain + "/default.aspx?ChID=" + ChID + "" + "\">" + dr["channelName"].ToString() + "</a>";
                        }
                        else
                        {
                            string iPath = "/" + dr["htmldir"].ToString() + "/" + dr["indexFileName"].ToString() + "";
                            iPath = iPath.Replace("//", "/").Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
                            str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + "<a href=\"" + CommonData.SiteDomain + iPath + "\">" + dr["channelName"].ToString() + "</a>";
                        }
                    }
                    dr.Close();
                    break;
                case "ChNews":
                    str_Position = GetIndexPath(ReadType, ChID, str_DynChar) + str_DynChar + getCHPositionSTR(str_DynChar, this.Param_CurrentCHNewsID, "ChNews", ChID);
                    break;
                case "ChClass":
                    str_Position = GetIndexPath(ReadType, ChID, str_DynChar) + str_DynChar + getCHPositionSTR(str_DynChar, this.Param_CurrentCHClassID, "ChClass", ChID) + "列表";
                    break;
                case "ChSpecial":
                    str_Position = GetIndexPath(ReadType, ChID, str_DynChar) + str_DynChar + getCHPositionSTR(str_DynChar, this.Param_CurrentCHSpecialID, "ChSpecial", ChID);
                    break;
                default:
                    break;
            }
            return str_Position;
        }

        public string GetIndexPath(string ReadType, int ChID, string str_DynChar)
        {
            string str_Position = string.Empty;
            IDataReader dr = CommonData.DalPublish.GetPositionNavi(0, "ChIndex", ChID);
            if (dr.Read())
            {
                if (ReadType == "1")
                {
                    str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + "<a href=\"" + CommonData.SiteDomain + "/default.aspx?ChID=" + ChID + "" + "\">" + dr["channelName"].ToString() + "</a>";
                }
                else
                {
                    string iPath = "/" + dr["htmldir"].ToString() + "/" + dr["indexFileName"].ToString() + "";
                    iPath = iPath.Replace("//", "/").Replace("{@dirHTML}", Foosun.Config.UIConfig.dirHtml);
                    str_Position = "<a href=\"" + CommonData.SiteDomain + "\">首页</a>" + str_DynChar + "<a href=\"" + CommonData.SiteDomain + iPath + "\">" + dr["channelName"].ToString() + "</a>";
                }
            }
            dr.Close();
            return str_Position;
        }

        /// <summary>
        /// 得到META类
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public string Analyse_Meta(int Num, int ChID)
        {
            string str_Metas = "";
            string str_MetaContent = this.GetParamValue("FS:MetaContent");
            switch (this.TemplateType.ToString())
            {
                case "Index":
                    str_Metas = "首页,新闻,CMS";
                    break;
                case "News":
                    str_Metas = getMetaContent(this.Param_CurrentNewsID, "News", Num) + "," + str_MetaContent;
                    break;
                case "Class":
                    str_Metas = getMetaContent(this.Param_CurrentClassID, "Class", Num) + "," + str_MetaContent;
                    break;
                case "Special":
                    str_Metas = getMetaContent(this.Param_CurrentSpecialID, "Special", Num) + "," + str_MetaContent;
                    break;
                case "ChIndex":
                    str_Metas = CommonData.DalPublish.GetCHMeta(0, Num, ChID, "ChIndex");
                    break;
                case "ChClass":
                    str_Metas = CommonData.DalPublish.GetCHMeta(this.Param_CurrentCHClassID, Num, ChID, "ChClass");
                    break;
                case "ChNews":
                    str_Metas = CommonData.DalPublish.GetCHMeta(this.Param_CurrentCHNewsID, Num, ChID, "ChNews");
                    break;
                case "ChSpecial":
                    str_Metas = CommonData.DalPublish.GetCHMeta(this.Param_CurrentCHSpecialID, Num, ChID, "ChSpecial");
                    break;
                default:
                    break;
            }
            return str_Metas;

        }

        /// <summary>
        /// 页面标题
        /// </summary>
        /// <returns></returns>
        public string Analyse_PageTitle(int ChID)
        {
            string str_PageTitle = "";
            string str_prefix = this.GetParamValue("FS:prefix");
            string str_prefix_1 = "0";
            string str_prefix_2 = "";
            if (str_prefix.IndexOf("$") > -1)
            {
                string[] str_prefixARR = str_prefix.Split('$');
                str_prefix_1 = str_prefixARR[0];
                str_prefix_2 = str_prefixARR[1];
            }
            switch (this.TemplateType.ToString())
            {
                case "Index":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getSiteName();
                    }
                    else
                    {
                        str_PageTitle = getSiteName() + str_prefix_2;
                    }
                    break;
                case "News":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle(this.Param_CurrentNewsID, "News", 0);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle(this.Param_CurrentNewsID, "News", 0) + str_prefix_2;
                    }
                    break;
                case "Class":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle(this.Param_CurrentClassID, "Class", 0);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle(this.Param_CurrentClassID, "Class", 0) + str_prefix_2;
                    }
                    break;
                case "Special":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle(this.Param_CurrentSpecialID, "Special", 0);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle(this.Param_CurrentSpecialID, "Special", 0) + str_prefix_2;
                    }
                    break;
                case "ChIndex":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle("0", "ChIndex", ChID);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle("0", "ChIndex", ChID) + str_prefix_2;
                    }
                    break;
                case "ChNews":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle(this.Param_CurrentCHNewsID.ToString(), "ChNews", ChID);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle(this.Param_CurrentCHNewsID.ToString(), "ChNews", ChID) + str_prefix_2;
                    }
                    break;
                case "ChClass":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle(this.Param_CurrentCHClassID.ToString(), "ChClass", ChID);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle(this.Param_CurrentCHClassID.ToString(), "ChClass", ChID) + str_prefix_2;
                    }
                    break;
                case "ChSpecial":
                    if (str_prefix_1 == "0")
                    {
                        str_PageTitle = str_prefix_2 + getPageTitle(this.Param_CurrentCHSpecialID.ToString(), "ChSpecial", ChID);
                    }
                    else
                    {
                        str_PageTitle = getPageTitle(this.Param_CurrentCHSpecialID.ToString(), "ChSpecial", ChID) + str_prefix_2;
                    }
                    break;

            }
            return str_PageTitle;
        }

        /// <summary>
        /// 自定义不规则新闻
        /// </summary>
        /// <returns></returns>
        public string Analyse_unRule()
        {
            string str_unRule = string.Empty;
            string str_RuleID = this.GetParamValue("FS:RuleID");
            string str_STitle = this.GetParamValue("FS:STitle");
            string str_unNavi1 = this.GetParamValue("FS:unNavi");
            string str_unNavi = string.Empty;
            string gstr_RuleID = "0";
            if (str_unNavi1 != null)
            {
                str_unNavi = Foosun.Common.Input.isPicStr(str_unNavi1);
            }
            if (str_RuleID != null)
            {
                gstr_RuleID = str_RuleID;
            }
            DataTable dt = CommonData.DalPublish.GetUnRule(gstr_RuleID, Param_SiteID);
            if (dt != null && dt.Rows.Count > 0)
            {
                string str_titleCss = "";
                int int_rows = 0;
                int int_rows1 = 1;
                if (str_STitle == "true")
                {
                    str_unRule += "<div>" + newLine;
                    str_unRule += "<span " + str_titleCss + ">" + dt.Rows[0]["unName"].ToString() + "</span>";
                    str_unRule += "</div>" + newLine;
                }
                str_unRule += "<div>" + newLine;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str_titleCss = dt.Rows[i]["TitleCSS"].ToString();
                    if (str_titleCss != null && str_titleCss != "")
                    {
                        str_titleCss = " class=\"" + str_titleCss + "\"";
                    }
                    string str_SubClass = dt.Rows[i]["SubCSS"].ToString();
                    if (str_SubClass != null && str_SubClass != "")
                    {
                        str_SubClass = " class=\"" + str_SubClass + "\"";
                    }
                    int_rows = int.Parse(dt.Rows[i]["Rows"].ToString());
                    IDataReader rd = CommonData.DalPublish.GetNewsSavePath(dt.Rows[i]["ONewsID"].ToString());
                    string str_NewsUrl = "";
                    if (rd.Read())
                    {
                        PubClassInfo ci = CommonData.GetClassById(rd["ClassID"].ToString());
                        if (ci != null && rd["SavePath"] != DBNull.Value && rd["FileName"] != DBNull.Value && rd["FileEXName"] != DBNull.Value)
                        {
                            str_NewsUrl = getNewsURL(rd["isDelPoint"].ToString(), rd["NewsID"].ToString(), rd["SavePath"].ToString(), ci.SavePath + "/" + ci.SaveClassframe, rd["FileName"].ToString(), rd["FileEXName"].ToString());
                        }
                        else
                        {
                            str_NewsUrl = "javascript:void(0);";
                        }
                        if (int_rows == int_rows1)
                        {
                            if (i == 0)
                            {
                                str_unRule += str_unNavi + "<a target=\"_blank\" href=\"" + str_NewsUrl + "\" " + str_SubClass + ">" + dt.Rows[i]["unTitle"].ToString() + "</a>&nbsp;";
                            }
                            else
                            {
                                str_unRule += "<a target=\"_blank\" href=\"" + str_NewsUrl + "\" " + str_SubClass + ">" + dt.Rows[i]["unTitle"].ToString() + "</a>&nbsp;";
                            }
                        }
                        else
                        {
                            int_rows1 = int_rows1 + 1;
                            str_unRule += "<br />" + str_unNavi + "<a target=\"_blank\" href=\"" + str_NewsUrl + "\" " + str_SubClass + ">" + dt.Rows[i]["unTitle"].ToString() + "</a>&nbsp;";
                        }
                    }
                    rd.Close();
                }

                str_unRule += "</div>" + newLine;
                dt.Clear(); dt.Dispose();
            }
            return str_unRule;
        }

        public string Analyse_ReadNews(int id /*DataRow dr*/, int TitleNumer, int ContentNumber, int NaviNumber, string str_Style, string styleid, int currentPageNum, int EndPageNum, int NewsTF)
        {
            Foosun.Model.NewsContent Nci = new Foosun.Model.NewsContent();
            Nci = this.getNewsInfo(id, this.Param_CurrentNewsID);

            string TmpdimmDir = "";
            if (dimmDir.Trim() != string.Empty)
            {
                TmpdimmDir = "/" + dimmDir;
            }
            if (NewsTF == 1)
            {
                str_Style = this.Mass_Inserted;
                string styleids = Regex.Match(str_Style, @"\[\#FS:StyleID=(?<sid>[^\]]+)]", RegexOptions.Compiled).Groups["sid"].Value.Trim();
                if (styleids != string.Empty)
                {
                    str_Style = LabelStyle.GetStyleByID(styleids);
                }
                if (str_Style.Trim() == string.Empty)
                    return string.Empty;
            }
            if (Nci != null)
            {
                if (TitleNumer <= 0)
                    TitleNumer = 15;
                if (ContentNumber <= 0)
                    ContentNumber = 15;
                if (NaviNumber <= 0)
                    NaviNumber = 15;

                PubClassInfo ci = CommonData.GetClassById(Nci.ClassID);
                if (ci == null)
                    ci = new PubClassInfo();
                PubSpecialInfo si = new PubSpecialInfo();
                if (Nci.SpecialID != string.Empty)
                    si = CommonData.GetSpecial(Nci.SpecialID);
                //string NewsPathstr = "";

                //if (Nci.SavePath != "" && Nci.FileName != "")
                //{
                //    if (ci != null)
                //    {
                //        NewsPathstr = ci.SavePath + "/" + ci.SaveClassframe + "/" + Nci.SavePath + "/" + Nci.FileName + Nci.FileEXName;
                //        NewsPathstr = CommonData.SiteDomain + NewsPathstr.Replace("//", "/");
                //    }
                //}
                #region 基本
                //标题--------------------------------------------------------------------------------------------------------

                if (str_Style.IndexOf("{#Title}") > -1)
                {
                    string str_title = Nci.NewsTitle;
                    string CommStr = "";
                    if (NewsTF == 0)
                    {
                        if (Nci.CommLinkTF == 1)
                        {
                            if (Nci.CommTF == 1)
                            {
                                CommStr = " <a href=\"" + getNewsURL(Nci.isDelPoint.ToString(), Nci.NewsID, Nci.SavePath, ci.SavePath + "/" + ci.SaveClassframe, Nci.FileName, Nci.FileEXName) + "#commList\">[评]</a>";
                            }
                        }

                        str_title = getStyle(Foosun.Common.Input.GetSubString(str_title, TitleNumer), Nci.TitleColor, Nci.TitleITF, Nci.TitleBTF);
                    }
                    str_Style = str_Style.Replace("{#Title}", str_title + CommStr);
                }
                //标题(不可截断)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#uTitle}") > -1)
                    if (NewsTF == 1)
                    {
                        str_Style = str_Style.Replace("{#uTitle}", Nci.NewsTitle);
                    }
                    else
                    {
                        //<--2008-07-22 wjl 标题全部显示问题
                        str_Style = str_Style.Replace("{#uTitle}", getStyle(Nci.NewsTitle, Nci.TitleColor, Nci.TitleITF, Nci.TitleBTF));
                        //--wjl>
                    }
                //副标题--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#sTitle}") > -1)
                {
                    if (Nci.sNewsTitle != "")
                        str_Style = str_Style.Replace("{#sTitle}", Nci.sNewsTitle);
                    else
                        str_Style = str_Style.Replace("{#sTitle}", "");
                }
                //连接地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#URL}") > -1)
                {
                    string URLS = "";
                    if (Nci.NewsType == 2)
                    {
                        URLS = Nci.URLaddress;
                    }
                    else
                    {
                        if (Nci.FileEXName != "")
                        {
                            URLS = getNewsURL(Nci.isDelPoint.ToString(), Nci.NewsID, Nci.SavePath, ci.SavePath + "/" + ci.SaveClassframe, Nci.FileName, Nci.FileEXName);
                        }
                    }
                    str_Style = str_Style.Replace("{#URL}", URLS);
                }
                //新闻内容--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Content}") > -1)
                {
                    string str_content = Nci.Content;
                    string tmpcontent = str_content;
                    if (NewsTF == 0)
                    {
                        string LostResultStr = Foosun.Common.Input.LostHTML(tmpcontent);
                        if (LostResultStr.IndexOf("[FS:PAGE") > -1 && LostResultStr.IndexOf("$]") > -1)
                        {
                            LostResultStr = Foosun.Common.Input.LostPage(LostResultStr);
                        }
                        if (LostResultStr.IndexOf("[FS:unLoop") > -1 && LostResultStr.IndexOf("[/FS:unLoop]") > -1)
                        {
                            LostResultStr = Foosun.Common.Input.LostVoteStr(LostResultStr);
                        }
                        str_content = Foosun.Common.Input.GetSubString(LostResultStr, ContentNumber) + "...";
                        str_Style = str_Style.Replace("{#Content}", str_content.Replace("[FS:PAGE]", "").Replace("[FS:PAGE", "").Replace("$]", ""));
                    }
                    else if (NewsTF == 1)
                    {
                        string str_tailAdStr = "";
                        if (Nci.ContentPicTF == 1)
                        {
                            string ContentPicURL = Nci.ContentPicURL;
                            string ContentPicSize = Nci.ContentPicSize;
                            string[] arrContentPicSize = ContentPicSize.Split('|');
                            string heighSTR = arrContentPicSize[1].ToString();
                            string widthSTR = arrContentPicSize[0].ToString();
                            string dirFile = Foosun.Config.UIConfig.dirFile;
                            string getPostAlign = Foosun.Common.Public.readparamConfig("InsertPicPosition");
                            int picLen = 200;
                            string postAlign = "left";
                            if (getPostAlign.IndexOf("|") > -1)
                            {
                                postAlign = getPostAlign.Split('|')[1];
                                picLen = Convert.ToInt32(getPostAlign.Split('|')[0]);
                            }
                            int getType = 0;
                            string getContentFileName = ContentPicURL.Substring(ContentPicURL.Length - 4).ToLower();
                            switch (getContentFileName)
                            {
                                case ".jpg":
                                    getType = 0;
                                    break;
                                case ".gif":
                                    getType = 0;
                                    break;
                                case ".jpeg":
                                    getType = 0;
                                    break;
                                case ".png":
                                    getType = 0;
                                    break;
                                case ".bmp":
                                    getType = 0;
                                    break;
                                case ".swf":
                                    getType = 1;
                                    break;
                                case ".flv":
                                    getType = 2;
                                    break;
                                default:
                                    getType = 3;
                                    break;
                            }
                            if (getType != 3)
                            {
                                if (ContentPicURL.IndexOf("http://") == -1)
                                {
                                    ContentPicURL = CommonData.SiteDomain + ContentPicURL;
                                }
                            }
                            ContentPicURL = ContentPicURL.ToLower().Replace("{@dirfile}", dirFile);
                            switch (getType)
                            {
                                case 0:
                                    str_tailAdStr += "<table border=\"0\" cellspacing=\"2\" cellpadding=\"2\" align=\"" + postAlign + "\">" + newLine;
                                    str_tailAdStr += "<tr>" + newLine;
                                    str_tailAdStr += "<td>" + newLine;
                                    str_tailAdStr += "<img border=\"0\" src=\"" + ContentPicURL + "\" height=\"" + heighSTR + "\" width=\"" + widthSTR + "\">" + newLine;
                                    str_tailAdStr += "</td>" + newLine;
                                    str_tailAdStr += "</tr>" + newLine;
                                    str_tailAdStr += "</table>" + newLine;
                                    break;
                                case 1:
                                    str_tailAdStr += "<table border=\"0\" cellspacing=\"2\" cellpadding=\"2\" align=\"" + postAlign + "\">" + newLine;
                                    str_tailAdStr += "<tr>" + newLine;
                                    str_tailAdStr += "<td>" + newLine;
                                    str_tailAdStr += "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=";
                                    str_tailAdStr += "\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" ";
                                    str_tailAdStr += "width=\"" + widthSTR + "\" ";
                                    str_tailAdStr += "height=\"" + heighSTR + "\" >" + newLine;
                                    str_tailAdStr += "<param name=\"movie\" value=\"" + ContentPicURL + "\">" + newLine;
                                    str_tailAdStr += "<param name=\"quality\" value=\"high\">" + newLine;
                                    str_tailAdStr += "<embed src=\"" + ContentPicURL + "\" quality=\"high\" ";
                                    str_tailAdStr += "pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" ";
                                    str_tailAdStr += "width=\"" + widthSTR + "\" ";
                                    str_tailAdStr += "height=\"" + heighSTR + "\" >";
                                    str_tailAdStr += "</embed></object>" + newLine;
                                    str_tailAdStr += "</td>" + newLine;
                                    str_tailAdStr += "</tr>" + newLine;
                                    str_tailAdStr += "</table>" + newLine;
                                    break;
                                case 2:
                                    str_tailAdStr += "<table border=\"0\" cellspacing=\"2\" cellpadding=\"2\" align=\"" + postAlign + "\">" + newLine;
                                    str_tailAdStr += "<tr>" + newLine;
                                    str_tailAdStr += "<td>" + newLine;
                                    str_tailAdStr += "<embed src=\"" + CommonData.SiteDomain + "/FlvPlayer.swf?id=" + ContentPicURL + "\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" quality=\"high\" height=\"" + heighSTR + "\" width=\"" + widthSTR + "\" autostart=\"true\"></embed>" + newLine;
                                    str_tailAdStr += "</td>" + newLine;
                                    str_tailAdStr += "</tr>" + newLine;
                                    str_tailAdStr += "</table>" + newLine;
                                    break;
                                default:
                                    str_tailAdStr += "<table border=\"0\" cellspacing=\"2\" height=\"" + heighSTR + "\" width=\"" + widthSTR + "\" cellpadding=\"2\" align=\"" + postAlign + "\">" + newLine;
                                    str_tailAdStr += "<tr>" + newLine;
                                    str_tailAdStr += "<td>" + newLine;
                                    str_tailAdStr += ContentPicURL + newLine;
                                    str_tailAdStr += "</td>" + newLine;
                                    str_tailAdStr += "</tr>" + newLine;
                                    str_tailAdStr += "</table>" + newLine;
                                    break;

                            }
                            if (str_content.Length < picLen)
                            {
                                tmpcontent = tmpcontent + str_tailAdStr;
                            }
                            else
                            {
                                tmpcontent = str_content.Substring(0, (picLen - 1)) + str_tailAdStr + str_content.Substring(picLen);
                            }
                        }
                        else
                        {
                            tmpcontent = str_content;
                        }
                        if (NewsTF == 1)
                        {
                            if (str_Style.IndexOf("{#PageTitle_select}") > -1 || str_Style.IndexOf("{#PageTitle_textdouble}") > -1 || str_Style.IndexOf("{#PageTitle_textsinge}") > -1 || str_Style.IndexOf("{#PageTitle_textcols}") > -1)
                            {
                                string GetPagecontent = tmpcontent;
                                string Re_Content = string.Empty;
                                string Pagetitstr = string.Empty;
                                if (GetPagecontent.IndexOf("[FS:PAGE=") > -1 && GetPagecontent.IndexOf("$]") > -1)
                                {
                                    string pattern = @"\[FS:PAGE=(?<p>[\s\S]+?)\$\]";
                                    Regex reg = new Regex(pattern, RegexOptions.Compiled);
                                    Match m = reg.Match(GetPagecontent);
                                    while (m.Success)
                                    {
                                        Pagetitstr += m.Groups["p"].Value + "###";
                                        m = m.NextMatch();
                                    }
                                    tmpcontent = reg.Replace(tmpcontent, "[FS:PAGE]");
                                    if (str_Style.IndexOf("{#PageTitle_select}") > -1)
                                    {
                                        Re_Content = getPageTitleStyle(Nci.NewsID, Nci.FileName, Nci.FileEXName, Pagetitstr, 0, Nci.isDelPoint, 0);
                                        str_Style = str_Style.Replace("{#PageTitle_select}", Re_Content);
                                    }
                                    if (str_Style.IndexOf("{#PageTitle_textdouble}") > -1)
                                    {
                                        Re_Content = getPageTitleStyle(Nci.NewsID, Nci.FileName, Nci.FileEXName, Pagetitstr, 1, Nci.isDelPoint, 0);
                                        str_Style = str_Style.Replace("{#PageTitle_textdouble}", Re_Content);
                                    }
                                    if (str_Style.IndexOf("{#PageTitle_textsinge}") > -1)
                                    {
                                        Re_Content = getPageTitleStyle(Nci.NewsID, Nci.FileName, Nci.FileEXName, Pagetitstr, 2, Nci.isDelPoint, 0);
                                        str_Style = str_Style.Replace("{#PageTitle_textsinge}", Re_Content);
                                    }
                                    if (str_Style.IndexOf("{#PageTitle_textcols}") > -1)
                                    {
                                        Re_Content = getPageTitleStyle(Nci.NewsID, Nci.FileName, Nci.FileEXName, Pagetitstr, 3, Nci.isDelPoint, 0);
                                        str_Style = str_Style.Replace("{#PageTitle_textcols}", Re_Content);
                                    }
                                }
                                else
                                {
                                    str_Style = str_Style.Replace("{#PageTitle_select}", "");
                                    str_Style = str_Style.Replace("{#PageTitle_textdouble}", "");
                                    str_Style = str_Style.Replace("{#PageTitle_textsinge}", "");
                                    str_Style = str_Style.Replace("{#PageTitle_textcols}", "");
                                }
                            }
                            else
                            {
                                str_Style = str_Style.Replace("{#PageTitle_select}", "");
                                str_Style = str_Style.Replace("{#PageTitle_textdouble}", "");
                                str_Style = str_Style.Replace("{#PageTitle_textsinge}", "");
                                str_Style = str_Style.Replace("{#PageTitle_textcols}", "");
                            }
                        }
                        //str_Style = str_Style.Replace("{#Content}", "<!-FS:STAR=" + tmpcontent.Replace("[FS:PAGE]", "{Foosun:NewsLIST}") + "FS:END->");
                        if (Foosun.Common.Public.readparamConfig("collectTF") == "1")
                        {
                            tmpcontent = tmpcontent.Replace("<div", "<!--source from " + Foosun.Common.Public.readparamConfig("siteDomain") + "--><div");
                        }
                        str_Style = str_Style.Replace("{#Content}", "<!-FS:STAR=" + tmpcontent + "FS:END->");
                    }
                }
                #endregion 基本
                #region 日期
                //录入时间：完整
                if (str_Style.IndexOf("{#Date}") > -1)
                    str_Style = str_Style.Replace("{#Date}", Nci.CreatTime.ToString() + "");
                //录入时间：年-月-日
                if (str_Style.IndexOf("{#DateShort}") > -1)
                    str_Style = str_Style.Replace("{#DateShort}", Nci.CreatTime.ToShortDateString().ToString() + "");
                //录入日期:二位年份--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Year02}") > -1)
                    str_Style = str_Style.Replace("{#Date:Year02}", Nci.CreatTime.Year.ToString().Remove(0, 2));
                //录入日期:四位年份--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Year04}") > -1)
                    str_Style = str_Style.Replace("{#Date:Year04}", Nci.CreatTime.Year.ToString());
                //录入日期:月份--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Month}") > -1)
                    str_Style = str_Style.Replace("{#Date:Month}", Nci.CreatTime.Month.ToString());
                //录入日期:日--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Day}") > -1)
                    str_Style = str_Style.Replace("{#Date:Day}", Nci.CreatTime.Day.ToString());
                //录入日期:时--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Hour}") > -1)
                    str_Style = str_Style.Replace("{#Date:Hour}", Nci.CreatTime.Hour.ToString());
                //录入日期:分--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Minute}") > -1)
                    str_Style = str_Style.Replace("{#Date:Minute}", Nci.CreatTime.Minute.ToString());
                //录入日期:秒--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Date:Second}") > -1)
                    str_Style = str_Style.Replace("{#Date:Second}", Nci.CreatTime.Second.ToString());
                //点击--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Click}") > -1)
                {
                    if (NewsTF == 0)
                    {
                        str_Style = str_Style.Replace("{#Click}", Nci.Click.ToString());
                    }
                    else
                    {
                        string str_Click = "<span id=\"click_" + Nci.NewsID + "\"></span><script language=\"javascript\" type=\"text/javascript\">";
                        str_Click += "pubajax('" + CommonData.SiteDomain + "/click.aspx','id=" + Nci.NewsID + "','click_" + Nci.NewsID + "');";
                        str_Click += "</script>";
                        str_Style = str_Style.Replace("{#Click}", str_Click);
                    }
                }
                #endregion 日期
                #region 其他
                //来源--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Source}") > -1)
                {
                    if (Nci.Souce != "")
                        str_Style = str_Style.Replace("{#Source}", Nci.Souce);
                    else
                        str_Style = str_Style.Replace("{#Source}", "");
                }
                //编辑--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Editor}") > -1)
                {
                    if (Nci.Editor != "")
                        str_Style = str_Style.Replace("{#Editor}", "<a href=\"" + CommonData.SiteDomain + "/search.html?type=edit&tags=" + Foosun.Common.Input.URLEncode(Nci.Editor) + "\" title=\"查看此编辑的所有新闻\" target=\"_blank\">" + Nci.Editor + "</a>");
                    else
                        str_Style = str_Style.Replace("{#Editor}", "");
                }

                //作者--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Author}") > -1)
                {
                    if (Nci.Author != "")
                    {
                        if (Nci.isConstr == 1)
                        {
                            str_Style = str_Style.Replace("{#Author}", "<a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + Nci.Author + ".aspx\" title=\"查看他的资料\">" + Nci.Author + "</a> <a href=\"" + CommonData.SiteDomain + "/search.html?type=author&tags=" + Nci.Author + "\" title=\"此看此作者所有的文章\" target=\"_blank\">发表的文章</a>");
                        }
                        else
                        {
                            str_Style = str_Style.Replace("{#Author}", "<a href=\"" + CommonData.SiteDomain + "/search.html?type=author&tags=" + Nci.Author + "\" title=\"此看此作者所有的文章\" target=\"_blank\">" + Nci.Author + "</a>");
                        }
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{#Author}", "");
                    }
                }


                //Meta关键字--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#MetaKeywords}") > -1)
                {
                    if (Nci.Metakeywords != "")
                    {
                        str_Style = str_Style.Replace("{#MetaKeywords}", Nci.Metakeywords);
                    }
                    else
                        str_Style = str_Style.Replace("{#MetaKeywords}", "");
                }
                //Meta描述--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Metadesc}") > -1)
                {
                    if (Nci.Metadesc != "")
                        str_Style = str_Style.Replace("{#Metadesc}", Nci.Metadesc);
                    else
                        str_Style = str_Style.Replace("{#Metadesc}", "");
                }
                //图片--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Picture}") > -1)
                {
                    if (Nci.PicURL != "")
                        str_Style = str_Style.Replace("{#Picture}", RelpacePicPath(Nci.PicURL));
                    else
                        str_Style = str_Style.Replace("{#Picture}", "");
                }
                //小图（缩图）--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#sPicture}") > -1)
                {
                    if (Nci.SPicURL != "")
                        str_Style = str_Style.Replace("{#sPicture}", RelpacePicPath(Nci.SPicURL));
                    else
                        str_Style = str_Style.Replace("{#sPicture}", "");
                }


                if (str_Style.IndexOf("{$NaviContent[") > -1 && str_Style.IndexOf("]}") > -1)
                {
                    int sPosion = str_Style.IndexOf("{$NaviContent[");
                    int ePosion = str_Style.IndexOf("]}");
                    int setplen = (ePosion - (sPosion + 14));
                    string MideStr = str_Style.Substring((sPosion + 14), setplen);
                    string GResult = string.Empty;
                    if (MideStr.IndexOf("{#NaviContent}") > -1)
                    {
                        if (NewsTF == 1)
                        {
                            GResult = MideStr.Replace("{#NaviContent}", Nci.naviContent);
                        }
                        else
                        {
                            if (Nci.naviContent != "")
                            {
                                GResult = MideStr.Replace("{#NaviContent}", Foosun.Common.Input.GetSubString(Nci.naviContent, NaviNumber));
                            }
                            else
                            {
                                GResult = MideStr.Replace("{#NaviContent}", "");
                            }
                        }
                    }
                    if (Nci.naviContent != string.Empty)
                    {
                        str_Style = str_Style.Replace(MideStr, GResult).Replace("{$NaviContent[", "").Replace("]}", "");
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{#NaviContent}", "").Replace("{$NaviContent[" + GResult + "]}", "");
                    }
                }

                //导读
                if (str_Style.IndexOf("{$#NaviContent}") > -1)
                {
                    if (NewsTF == 1)
                    {
                        str_Style = str_Style.Replace("{$#NaviContent}", Nci.naviContent);
                    }
                    else
                    {
                        if (Nci.naviContent != string.Empty)
                        {
                            str_Style = str_Style.Replace("{$#NaviContent}", Foosun.Common.Input.GetSubString(Nci.naviContent, NaviNumber));
                        }
                        else
                        {
                            str_Style = str_Style.Replace("{$#NaviContent}", "");
                        }
                    }
                }

                if (str_Style.IndexOf("{#vote}") > -1)
                {
                    if (Nci.VoteTF == 1)
                        str_Style = str_Style.Replace("{#vote}", getVoteItem(Nci.NewsID, NewsTF));
                    else
                        str_Style = str_Style.Replace("{#vote}", "");
                }
                #endregion 其他
                #region 关键字
                //TAG(关键字)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Tags}") > -1)
                {
                    if (Nci.Tags != "")
                    {
                        string tagdef = Nci.Tags;
                        string tagstr = "";
                        if (tagdef.IndexOf("|") > -1)
                        {
                            string[] tagARR = tagdef.Split('|');
                            for (int im = 0; im < tagARR.Length; im++)
                            {
                                tagstr += "<a href=\"" + CommonData.SiteDomain + "/Search.html?type=tag&tags=" + tagARR[im] + "\">" + tagARR[im] + "</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            tagstr = "<a href=\"" + CommonData.SiteDomain + "/Search.html?type=tag&tags=" + tagdef + "\">" + tagdef + "</a>";
                        }
                        str_Style = str_Style.Replace("{#Tags}", tagstr);
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{#Tags}", "");
                    }
                }
                #endregion 关键字
                #region 互动
                //评论表单--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#CommForm}") > -1)
                    str_Style = str_Style.Replace("{#CommForm}", getCommForm(Nci.NewsID, NewsTF, 0));
                //总评论数--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#CommCount}") > -1)
                    str_Style = str_Style.Replace("{#CommCount}", getCommCount(Nci.NewsID, NewsTF, 0, 0));
                //最新评论数(今日)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#LastCommCount}") > -1)
                    str_Style = str_Style.Replace("{#LastCommCount}", getCommCount(Nci.NewsID, NewsTF, 1, 0));
                //最新评论列表--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#LastComm}") > -1)
                    str_Style = str_Style.Replace("{#LastComm}", getLastComm(Nci.NewsID, NewsTF, 0));
                //总讨论数--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#GroupCount}") > -1)
                    str_Style = str_Style.Replace("{#GroupCount}", "");
                //发送给好友连接地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#SendInfo}") > -1)
                    str_Style = str_Style.Replace("{#SendInfo}", getSendInfo(Nci.NewsID, 0));
                //收藏连接地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#Collection}") > -1)
                    str_Style = str_Style.Replace("{#Collection}", getCollection(Nci.NewsID, 0));

                //上一篇--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#PrePage}") > -1)
                    str_Style = str_Style.Replace("{#PrePage}", getPrePage(Nci.ID.ToString(), Nci.DataLib, Nci.ClassID, 1, 0, 0));
                //下一篇--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#NextPage}") > -1)
                    str_Style = str_Style.Replace("{#NextPage}", getPrePage(Nci.ID.ToString(), Nci.DataLib, Nci.ClassID, 0, 0, 0));
                //上一篇标题--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#PrePageTitle}") > -1)
                    str_Style = str_Style.Replace("{#PrePageTitle}", getPrePage(Nci.ID.ToString(), Nci.DataLib, Nci.ClassID, 1, 0, 1));
                //下一篇标题--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#NextPageTitle}") > -1)
                    str_Style = str_Style.Replace("{#NextPageTitle}", getPrePage(Nci.ID.ToString(), Nci.DataLib, Nci.ClassID, 0, 0, 1));
                //Digg(数量)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#TopNum}") > -1)
                {
                    str_Style = str_Style.Replace("{#TopNum}", getTopNum(Nci.NewsID, NewsTF, Nci.TopNum.ToString(), Nci.FileName + Foosun.Common.Rand.Number(5)));
                }
                //Digg(连接地址)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#TopURL}") > -1)
                {
                    str_Style = str_Style.Replace("{#TopURL}", getTopURL(Nci.NewsID, NewsTF, Nci.FileName + Foosun.Common.Rand.Number(5)));
                }
                #endregion 互动
                #region 视频附件
                //附件--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#NewsFiles}") > -1)
                {
                    str_Style = str_Style.Replace("{#NewsFiles}", getNewsFiles(Nci.NewsID, NewsTF));
                }
                //视频播放地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#NewsvURL") > -1)
                {
                    Regex reg1 = new Regex(@"\{\#NewsvURL,(?<x>[^,]+),(?<y>[^\}]+)\}", RegexOptions.Compiled);
                    Match m1 = reg1.Match(str_Style);
                    string heightstr = "400";
                    string widthstr = "400";
                    string allstr = "";
                    if (m1.Success)
                    {
                        allstr = m1.Value;
                        heightstr = m1.Groups["x"].Value;
                        widthstr = m1.Groups["y"].Value;
                        if (Nci.vURL.Length > 5)
                        {
                            str_Style = str_Style.Replace(allstr, getNewsvURL(Nci.NewsID, NewsTF, Nci.vURL, heightstr, widthstr));
                        }
                        else
                        {
                            str_Style = str_Style.Replace(allstr, "");
                        }
                    }
                }
                #endregion 视频附件
                #region 栏目2
                //栏目中文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_Name}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{#class_Name}", ci.ClassCName);
                    else
                        str_Style = str_Style.Replace("{#class_Name}", "");
                }
                //栏目英文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_EName}") > -1)
                {
                    if (ci != null)
                    {
                        str_Style = str_Style.Replace("{#class_EName}", ci.ClassEName);
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{#class_EName}", "");
                    }
                }
                //栏目访问路径--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_Path}") > -1)
                {
                    if (ci != null)
                    {
                        str_Style = str_Style.Replace("{#class_Path}", getClassURL(ci.Domain, ci.isDelPoint, ci.ClassID, ci.SavePath, ci.SaveClassframe, ci.ClassSaveRule));
                    }
                    else
                        str_Style = str_Style.Replace("{#class_Path}", "");
                }
                //栏目信息:导读--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_Navi}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{#class_Navi}", ci.NaviContent);
                    else
                        str_Style = str_Style.Replace("{#class_Navi}", "");
                }
                //栏目信息:导读图片地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_NaviPic}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{#class_NaviPic}", ci.NaviPIC);
                    else
                        str_Style = str_Style.Replace("{#class_NaviPic}", "");
                }
                //栏目信息:meta关键字--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_Keywords}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{#class_Keywords}", ci.MetaKeywords);
                    else
                        str_Style = str_Style.Replace("{#class_Keywords}", "");
                }
                //栏目信息:meta描述--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#class_Descript}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{#class_Descript}", ci.MetaDescript);
                    else
                        str_Style = str_Style.Replace("{#class_Descript}", "");
                }
                #endregion --

                #region 专题
                //专题中文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#special_Name}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{#special_Name}", si.SpecialCName);
                    else
                        str_Style = str_Style.Replace("{#special_Name}", "");
                }
                //专题英文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#special_Ename}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{#special_Ename}", si.specialEName);
                    else
                        str_Style = str_Style.Replace("{#special_Ename}", "");
                }
                //专题连接路径--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#special_Path}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{#special_Path}", getSpeacilURL(si.isDelPoint.ToString(), si.SpecialID, si.SavePath, si.saveDirPath, si.FileName, si.FileEXName));
                    else
                        str_Style = str_Style.Replace("{#special_Path}", "");
                }
                //专题导航文字--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#special_NaviWords}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{#special_NaviWords}", si.NaviContent);
                    else
                        str_Style = str_Style.Replace("{#special_NaviWords}", "");
                }
                //专题导航图片地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{#special_NaviPic}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{#special_NaviPic}", si.NaviPicURL);
                    else
                        str_Style = str_Style.Replace("{#special_NaviPic}", "");
                }

                #endregion 专题
                #region 自定义字段
                //自定义字段--------------------------------------------------------------------------------------------------------
                string pattern_define = @"\{\#FS:define=(?<dname>[^\}]+)}";
                Regex regPage = new Regex(pattern_define, RegexOptions.Compiled);
                Match mPage = regPage.Match(str_Style);
                while (mPage.Success)
                {
                    string definename = mPage.Groups["dname"].Value;
                    string str_definedvalue = CommonData.DalPublish.GetDefinedValue(Nci.NewsID, definename);
                    str_Style = str_Style.Replace("{#FS:define=" + definename + "}", str_definedvalue);
                    mPage = mPage.NextMatch();
                }

                if (styleid.Equals(string.Empty))
                    return str_Style;
                else
                //return Mass_Inserted.Replace("[#FS:StyleID=" + styleid + "#]", str_Style);
                {
                    return Mass_Inserted.Replace("[#FS:StyleID=" + styleid + "]", str_Style);
                }
                #endregion 自定义
            }
            else
                return "";
        }



        /// <summary>
        /// 图片头条
        /// </summary>
        /// <returns></returns>
        public string Analyse_TodayPic()
        {
            string tpStr = string.Empty;
            string str_TodayPicID = this.GetParamValue("FS:TodayPicID");
            string str_TCHECK = this.GetParamValue("FS:TCHECK");
            string str_TNUM = this.GetParamValue("FS:TNUM");
            int number = 3;
            if (str_TNUM != null)
            {
                number = int.Parse(str_TNUM);
            }

            string str_TSCHAR = this.GetParamValue("FS:TSCHAR");
            string str_TECHAR = this.GetParamValue("FS:TECHAR");
            //开始得到图片

            DataTable dt = CommonData.DalPublish.GetTopLine(str_TodayPicID);
            if (dt != null && dt.Rows.Count > 0)
            {
                IDataReader rd = CommonData.DalPublish.GetNewsSavePath(dt.Rows[0]["NewsID"].ToString());
                if (rd.Read())
                {
                    string s = getNewsURL1(rd["ClassID"].ToString(), rd["isDelPoint"].ToString(), rd["NewsID"].ToString(), rd["SavePath"].ToString(), rd["FileName"].ToString(), rd["FileEXName"].ToString());
                    tpStr += "<div style=\"text-align:center;width:100%\"><a href=\"" + s + "\"><img src=\"" + CommonData.SiteDomain + RelpacePicPath(dt.Rows[0]["tl_SavePath"].ToString()) + "\" style=\"border:0px;\" /></a></div>";
                }
                rd.Close();
                dt.Clear(); dt.Dispose();
            }
            //如果有文字副新闻，开始获取
            if (str_TCHECK == "true")
            {
                DataTable dts = CommonData.DalPublish.GetTextSubNews(number);
                if (dts != null)
                {
                    if (dts.Rows.Count > 0)
                    {
                        DataRow r = dts.Rows[0];
                        tpStr += str_TSCHAR + "<a href=\"" + getNewsURL1(r) + "\">" + getNewstitleStyle(r, 1, "") + "</a>" + str_TECHAR + "&nbsp;";

                    }
                    dts.Clear();
                    dts.Dispose();
                }
            }
            return tpStr;
        }

        /// <summary>
        /// 文字头条
        /// </summary>
        /// <returns></returns>
        public string Analyse_TodayWord()
        {
            string twstr = "";
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_isBIGT = this.GetParamValue("FS:isBIGT");
            string str_BigCSS = this.GetParamValue("FS:BigCSS");
            string str_TSCHAR = this.GetParamValue("FS:TSCHAR");
            string str_TECHAR = this.GetParamValue("FS:TECHAR");
            string str_bigTitleNumber = this.GetParamValue("FS:bigTitleNumber");
            if (str_TSCHAR != null)
            {
                str_TSCHAR = str_TSCHAR.Replace("$#", "[").Replace("#$", "]");
            }
            if (str_TECHAR != null)
            {
                str_TECHAR = str_TECHAR.Replace("$#", "[").Replace("#$", "]");
            }
            string str_isSub = this.GetParamValue("FS:isSub");
            string str_Cols = this.GetParamValue("FS:Cols");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_ContentNumber = this.GetParamValue("FS:ContentNumber");
            string str_WNum = this.GetParamValue("FS:WNum");
            int TNum = 5;
            if (str_WNum != null && Foosun.Common.Input.IsInteger(str_WNum))
            {
                TNum = int.Parse(str_WNum);
            }
            string str_WCSS = this.GetParamValue("FS:WCSS");

            string SqlCondition = " Where substring(NewsProperty,9,1)='1' and islock=0 and isRecyle=0";
            if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
            {
                SqlCondition = " Where mid(NewsProperty,9,1)='1' and islock=0 and isRecyle=0";
            }
            string SqlOrderBy = " order by EditTime desc,id desc";

            #region 对栏目进行判断
            DataTable dt = null;
            string Sql = string.Empty;
            if (str_ClassID == null || str_ClassID == "-1")
            {
                if (this._TemplateType == TempType.Class)
                {
                    if (str_isSub == "true")
                        SqlCondition += " And [ClassID] In('" + getChildClassID(this.Param_CurrentClassID) + "')";
                    Sql = "select top " + TNum + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + " And ClassID='" + this.Param_CurrentClassID + "' " + SqlOrderBy;
                }
                else
                {
                    Sql = "select top " + TNum + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
                }
            }
            else if (str_ClassID == "0")
            {
                Sql = "select top " + TNum + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
            }
            else
            {
                if (str_isSub == "true")
                    SqlCondition += " And [ClassID] In(" + getChildClassID(str_ClassID) + ")";
                Sql = "select top " + TNum + " * from [" + DBConfig.TableNamePrefix + "News]" + SqlCondition + SqlOrderBy;
            }

            dt = CommonData.DalPublish.ExecuteSql(Sql);

            #endregion 对栏目进行判断


            int str_WNum_1 = 11;
            if (str_WNum != null)
                str_WNum_1 = int.Parse(str_WNum);

            string str_TitleNumer_1 = "30";
            if (str_TitleNumer != null)
                str_TitleNumer_1 = str_TitleNumer;

            bool isBIGTTF = false;
            if (str_isBIGT == "true")
                isBIGTTF = true;
            string classbigCssstr = "";
            string WCSSstr = "";
            int str_Colsint = 1;

            if (str_Cols != null)
                str_Colsint = int.Parse(str_Cols);

            int dtCount = dt.Rows.Count;
            if (dt != null && dtCount > 0)
            {
                for (int i = 0; i < dtCount; i++)
                {
                    if (isBIGTTF)
                    {
                        if (i == 0)
                        {
                            if (isBIGTTF)
                            {
                                string gstr_TitleNumer_1 = "20";
                                if (Foosun.Common.Input.IsInteger(str_bigTitleNumber))
                                {
                                    gstr_TitleNumer_1 = str_bigTitleNumber;
                                }
                                if (str_BigCSS != null)
                                    classbigCssstr = " class=\"" + str_BigCSS + "\"";
                                twstr += "<div style=\"width:100%;text-align:center;\"><a href=\"" + getNewsURL1(dt.Rows[i]) + "\"" + classbigCssstr + ">" + getNewstitleStyle(dt.Rows[i], 1, gstr_TitleNumer_1) + "</a></div>";
                            }
                        }
                        else
                        {

                            if (str_WCSS != null)
                            {
                                WCSSstr = " class=\"" + str_WCSS + "\"";
                            }
                            twstr += "<a href=\"" + getNewsURL1(dt.Rows[i]) + "\"" + WCSSstr + ">" + str_TSCHAR + getNewstitleStyle(dt.Rows[i], 1, str_TitleNumer_1) + str_TECHAR + "</a>&nbsp;";
                            if (i % str_Colsint == 0)
                            {
                                if (i != dtCount)
                                {
                                    twstr += "<br />";
                                }
                            }
                        }
                    }
                    else
                    {
                        if (str_WCSS != null)
                        {
                            WCSSstr = " class=\"" + str_WCSS + "\"";
                        }
                        twstr += "<a href=\"" + getNewsURL1(dt.Rows[i]) + "\"" + WCSSstr + ">" + str_TSCHAR + getNewstitleStyle(dt.Rows[i], 1, str_TitleNumer_1) + str_TECHAR + "</a>&nbsp;";
                        if ((i + 1) % str_Colsint == 0)
                        {
                            if (i != dtCount)
                            {
                                twstr += "<br />";
                            }
                        }
                    }

                }
                dt.Clear(); dt.Dispose();
            }
            else
            {
                twstr = "无文字头条";
            }
            return twstr;
        }


        /// <summary>
        /// 终极类：包括新闻终极,专题终极
        /// </summary>
        /// <returns></returns>
        public string Analyse_ClassList()
        {
            string mystyle = this.Mass_Inserted;
            string styleid = Regex.Match(mystyle, @"\[\#FS:StyleID=(?<sid>[^\]]+)]", RegexOptions.Compiled).Groups["sid"].Value.Trim();
            if (!styleid.Equals(string.Empty))
            {
                mystyle = LabelStyle.GetStyleByID(styleid);
            }
            if (mystyle.Trim().Equals(string.Empty))
                return string.Empty;

            string str_NewsType = this.GetParamValue("FS:LabelType");
            string str_ListType = this.GetParamValue("FS:ListType");
            string str_isSub = this.GetParamValue("FS:isSub");
            string str_SubNews = this.GetParamValue("FS:SubNews");
            int n_Cols;
            if (!int.TryParse(this.GetParamValue("FS:Cols"), out n_Cols))
                n_Cols = 1;
            if (n_Cols < 1)
                n_Cols = 1;
            string str_Desc = this.GetParamValue("FS:Desc");
            string str_DescType = this.GetParamValue("FS:DescType");
            string str_isDiv = this.GetParamValue("FS:isDiv");
            string str_ulID = this.GetParamValue("FS:ulID");
            string str_ulClass = this.GetParamValue("FS:ulClass");
            string str_bfStr = this.GetParamValue("FS:bfStr");
            string str_isPic = this.GetParamValue("FS:isPic");
            string str_NaviNumber = this.GetParamValue("FS:NaviNumber");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_ContentNumber = this.GetParamValue("FS:ContentNumber");
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_ColbgCSS = this.GetParamValue("FS:ColbgCSS");
            string str_NaviPic = this.GetParamValue("FS:NaviPic");
            string str_PageStyle = this.GetParamValue("FS:PageStyle");
            int PageTF = 0;
            int PageLineNum = 100;
            string PageLineContent = "";
            string PageLineContent1 = "";
            if (str_bfStr != string.Empty && str_bfStr != null)
            {
                if (str_bfStr.IndexOf("|") > -1)
                {
                    string[] BFARR = str_bfStr.Split('|');
                    PageTF = int.Parse(BFARR[0].ToString());
                    PageLineNum = int.Parse(BFARR[1].ToString());
                    PageLineContent = BFARR[2].ToString();
                    switch (PageTF)
                    {
                        case 0:
                            PageLineContent1 = "<span class=\"" + PageLineContent + "\" style=\"width:100%\"></span>";
                            break;
                        case 1:
                            PageLineContent1 = "<img src=\"" + PageLineContent + "\" border=\"0\" />";
                            break;
                        case 2:
                            PageLineContent1 = PageLineContent;
                            break;
                    }
                }
            }
            bool subTF = false;
            if (str_SubNews != null)
            {
                if (str_SubNews == "true")
                {
                    subTF = true;
                }
            }
            string getpublicType = Foosun.Config.verConfig.PublicType;//得到版本号
            string getWhere = "";
            if (getpublicType == "1")
            {
                getWhere += " And datediff(day,CreatTime ,getdate())=0";

            }
            if (str_ListType == "News")
            {
                if (str_isSub == "true")
                {
                    getWhere += " and ClassID in (" + getChildClassID(this.Param_CurrentClassID) + ")";
                }
                else
                {
                    getWhere += " and ClassID='" + this.Param_CurrentClassID + "'";
                }
            }
            else
            {

                if (str_isSub == "true")
                {
                    getWhere += " And NewsID In (Select NewsID From " + DBConfig.TableNamePrefix + "special_news Where SpecialID In (" + getChildSpecialID(this.Param_CurrentSpecialID) + "))";
                }
                else
                {
                    getWhere += " And NewsID In (Select NewsID From " + DBConfig.TableNamePrefix + "special_news Where SpecialID='" + this.Param_CurrentSpecialID + "')";
                }
            }

            string SqlFields = " [id] ";
            string SqlCondition = DBConfig.TableNamePrefix + "News Where [isRecyle]=0 And [isLock]=0 And [SiteID]='" + this.Param_SiteID + "'" + getWhere + "";
            //-------判断是否调用图片
            if (str_isPic == "true")
            {
                SqlCondition += " And [NewsType]=1";
            }
            else if (str_isPic == "false")
            {
                SqlCondition += " And [NewsType]=0 and [NewsType]=2 ";
            }

            string getPageTF = "{$FS:P0}{Page:0$$}";
            int PerPageNum = 30;
            if (getpublicType == "0")
            {
                PerPageNum = 30;
                string PageStyle = "0";
                string PageTitleColor = "";
                string PageCSS = "";
                if (str_PageStyle != string.Empty && str_PageStyle != null)
                {
                    //
                    string[] PageARR = str_PageStyle.Split('$');
                    PageStyle = PageARR[0].ToString();
                    PerPageNum = int.Parse(PageARR[2].ToString());
                    PageTitleColor = PageARR[1].ToString();
                    PageCSS = PageARR[3].ToString();
                }
                getPageTF = "{$FS:P0}{Page:" + PageStyle + "$" + PageTitleColor + "$" + PageCSS + "}";
            }
            #region 排序
            string SqlOrderBy = string.Empty;
            //-------排序
            if (str_NewsType == "Last")
            {
                SqlOrderBy += " order by ID Desc";
            }
            else
            {
                if (str_Desc != null && str_Desc.ToLower() == "asc")
                {
                    SqlOrderBy += " asc";
                }
                else
                {
                    SqlOrderBy += " Desc";
                }
                switch (str_DescType)
                {
                    case "id":
                        SqlOrderBy = " Order By id " + SqlOrderBy + "";
                        break;
                    case "date":
                        SqlOrderBy = " Order By [CreatTime] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    case "click":
                        SqlOrderBy = " Order By [Click] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    case "pop":
                        SqlOrderBy = " Order By [OrderID]" + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    case "digg":
                        SqlOrderBy = " Order By [TopNum]" + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    default:
                        if (str_NewsType == "Hot")
                        {
                            SqlOrderBy = " Order By [Click] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                        }
                        else
                        {
                            SqlOrderBy = " Order By [CreatTime] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                        }
                        break;
                }
            }
            #endregion 排序
            string topParam = "";
            //得到刷新新闻的数量//refresh.config里获得参数
            if (str_ListType == "News")
            {
                if (Foosun.Common.Public.readparamConfig("classlistNumber", "refresh") != "0")
                {
                    topParam = " top " + Foosun.Common.Public.readparamConfig("classlistNumber", "refresh") + " ";
                }
            }
            else
            {
                if (Foosun.Common.Public.readparamConfig("specialNumber", "refresh") != "0")
                {
                    topParam = " top " + Foosun.Common.Public.readparamConfig("specialNumber", "refresh") + " ";
                }
            }
            //bool Perpagebool = false;
            string Sql = "select " + topParam + SqlFields + " from " + SqlCondition + SqlOrderBy;
            DataTable dt = CommonData.DalPublish.ExecuteSql(Sql);
            if (dt == null || dt.Rows.Count < 1) return string.Empty;
            string str_newslist = string.Empty;
            int i;
            int nTitleNum = 30, nContentNum = 200, nNaviNumber = 200;
            if (str_TitleNumer != null && Foosun.Common.Input.IsInteger(str_TitleNumer))
            {
                nTitleNum = int.Parse(str_TitleNumer);
            }
            if (str_ContentNumber != null && Foosun.Common.Input.IsInteger(str_ContentNumber))
            {
                nContentNum = int.Parse(str_ContentNumber);
            }
            if (str_NaviNumber != null && Foosun.Common.Input.IsInteger(str_NaviNumber))
            {
                nNaviNumber = int.Parse(str_NaviNumber);
            }
            int dtcount = dt.Rows.Count;
            str_newslist = "{Foosun:NewsLIST}" + News_List_Head(str_isDiv, str_ulID, str_ulClass);
            string tmpPageLineContent1 = "";


            if (str_isDiv != "true")
            {
                str_newslist += News_List_Head(str_isDiv, str_ulID, str_ulClass);
            }
            for (i = 0; i < dtcount; i++)
            {
                //奇偶行样式
                string __showColBgCSS = string.Empty;
                if (!string.IsNullOrEmpty(str_ColbgCSS))
                {
                    string[] strClass = str_ColbgCSS.Split('|');
                    if (i % 2 == 0)//偶数行
                    {
                        __showColBgCSS = "class=\"" + strClass[0] + "\"";
                    }
                    else//奇数行
                    {
                        __showColBgCSS = "class=\"" + (strClass.Length == 2 ? strClass[1] : "") + "\"";
                    }
                }
                if (n_Cols != 1 && i == 0 && str_isDiv != "true")
                {
                    str_newslist += "<tr " + __showColBgCSS + ">";
                }

                if ((i + 1) % PageLineNum == 0)
                {
                    tmpPageLineContent1 = PageLineContent1;
                }
                else
                {
                    tmpPageLineContent1 = "";
                }
                if (str_isDiv == "true")
                {
                    str_newslist += getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i);
                    if (n_Cols == 1)
                    {
                        str_newslist += "<div " + __showColBgCSS + ">" + Analyse_ReadNews((int)dt.Rows[i][0], nTitleNum, nContentNum, nNaviNumber, mystyle, styleid, 1, 1, 0) + tmpPageLineContent1 + "</div>";
                    }
                    else
                    {
                        if (i == 0)
                            str_newslist += "<div " + __showColBgCSS + ">";
                        str_newslist += Analyse_ReadNews((int)dt.Rows[i][0], nTitleNum, nContentNum, nNaviNumber, mystyle, styleid, 1, 1, 0) + tmpPageLineContent1;
                        if (((i + 1) % n_Cols == 0))
                        {
                            if ((i + 1) < dtcount)
                            {
                                str_newslist += "</div>" + newLine + "<div " + __showColBgCSS + ">" + newLine;
                            }
                        }
                    }


                    //开始调用副新闻
                    if (subTF)
                    {
                        Foosun.Model.NewsContent sNCI = new Foosun.Model.NewsContent();
                        sNCI = this.getNewsInfo((int)dt.Rows[i][0], null);
                        str_newslist += getSubSTR(sNCI.NewsID, string.Empty);
                    }
                }
                else
                {
                    str_isDiv = "false";
                    string row = getNavi(str_ShowNavi, str_NaviCSS, str_NaviPic, i);
                    row += Analyse_ReadNews((int)dt.Rows[i][0], nTitleNum, nContentNum, nNaviNumber, mystyle, styleid, 1, 1, 0);
                    //开始调用副新闻
                    if (subTF)
                    {
                        Foosun.Model.NewsContent sNCI = new Foosun.Model.NewsContent();
                        sNCI = this.getNewsInfo((int)dt.Rows[i][0], null);
                        row += getSubSTR(sNCI.NewsID, string.Empty);
                    }
                    if (n_Cols == 1)
                    {
                        row += tmpPageLineContent1;
                    }


                    if (n_Cols == 1)
                    {
                        str_newslist += "<tr " + __showColBgCSS + ">";
                        str_newslist += newLine + "<td>" + newLine + row + newLine + "</td>" + newLine + "</tr>" + newLine;
                    }
                    else
                    {
                        row = "<td width=\"" + (100 / n_Cols) + "%\">" + newLine + row + newLine + "</td>" + newLine;
                        if (((i + 1) % n_Cols == 0))
                        {
                            if ((i + 1) < dtcount)
                            {
                                row += "</tr>" + newLine + "<tr " + __showColBgCSS + ">" + newLine;
                            }
                        }
                        str_newslist += row;
                    }
                }

                if (getpublicType == "0")
                {
                    if ((i + 1) % (PerPageNum) == 0)
                    {
                        if ((i + 1) < dtcount)
                        {
                            str_newslist += News_List_End(str_isDiv) + getPageTF + News_List_Head(str_isDiv, str_ulID, str_ulClass);
                        }
                    }
                }
                if ((i + 1) == dtcount)
                {
                    if (n_Cols != 1)
                    {
                        str_newslist += "</tr>" + newLine;
                    }
                    str_newslist += News_List_End(str_isDiv) + newLine;
                }
            }
            if (getpublicType == "1")
            {
                str_newslist += "{$FS:P1}";
            }
            dt.Clear(); dt.Dispose();
            return str_newslist + "{/Foosun:NewsLIST}";
        }

        protected string ChildList(IList<PubClassInfo> list, string Classid, string sign)
        {
            string str_Temp = "";
            sign += "─";
            foreach (PubClassInfo info in list)
            {
                if (info.ParentID == Classid && info.SiteID == this.Param_SiteID)
                {
                    string ClassID = info.ClassID;
                    string Classname = info.ClassCName;
                    str_Temp += "<option value=\"" + ClassID + "\">" + sign + Classname + "</option>" + newLine;
                    str_Temp += ChildList(list, ClassID, sign);
                }
            }
            return str_Temp;
        }

        /// <summary>
        /// 得到本身及所有子类编号字符串
        /// </summary>
        /// <param name="ClassID">父类编号</param>
        /// <returns></returns>
        protected string getChildClassID(string ClassID)
        {
            string RetVal = "'" + ClassID + "'" + GetChildClass(ClassID);
            return RetVal;
        }
        /// <summary>
        /// 得到子类编号字符串
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        protected string GetChildClass(string ParentID)
        {
            string RetVal = string.Empty;
            IList<PubClassInfo> list = CommonData.NewsClass;
            if (list != null && list.Count > 0)
            {
                foreach (PubClassInfo info in list)
                {
                    if (info.IsURL == 0 && info.SiteID == Param_SiteID)
                    {
                        if (info.ParentID != null)
                        {
                            if (info.ParentID == ParentID)
                            {
                                RetVal += ",'" + info.ClassID + "'" + GetChildClass(info.ClassID);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return RetVal;
        }
        /// <summary>
        /// 得到专题子类
        /// </summary>
        /// <param name="SpecialID"></param>
        /// <returns></returns>
        protected string getChildSpecialID(string SpecialID)
        {
            string RetVal = "'" + SpecialID + "'" + GetChildSpecial(SpecialID);
            return RetVal;
        }
        /// <summary>
        /// 得到子类编号字符串
        /// </summary>
        /// <param name="ParentID"></param>
        /// <returns></returns>
        protected string GetChildSpecial(string ParentID)
        {
            string RetVal = string.Empty;
            IList<PubSpecialInfo> list = CommonData.NewsSpecial;
            if (list != null && list.Count > 0)
            {
                foreach (PubSpecialInfo info in list)
                {
                    if (info.SiteID.ToLower().Trim() == Param_SiteID.ToLower().Trim())
                    {
                        if (info.ParentID.ToLower().Trim() == ParentID.ToLower().Trim())
                        {
                            RetVal += ",'" + info.SpecialID + "'" + GetChildSpecial(info.SpecialID);
                        }
                    }
                }
            }
            return RetVal;
        }

        protected Foosun.Model.NewsContent getNewsInfo(int ID, string NewsID)
        {

            Foosun.Model.NewsContent Nci = new Foosun.Model.NewsContent();
            IDataReader rd = CommonData.DalPublish.GetNewsDetail(ID, NewsID);
            if (rd.Read())
            {
                Nci.ID = Convert.ToInt32(rd["ID"]);
                Nci.NewsID = Convert.ToString(rd["NewsID"]);
                Nci.NewsType = Convert.ToByte(rd["NewsType"]);
                Nci.OrderID = Convert.ToByte(rd["OrderID"]);
                Nci.NewsTitle = Convert.ToString(rd["NewsTitle"]);
                if (rd["sNewsTitle"] == DBNull.Value) { Nci.sNewsTitle = ""; } else { Nci.sNewsTitle = Convert.ToString(rd["sNewsTitle"]); }
                if (rd["TitleColor"] == DBNull.Value) { Nci.TitleColor = ""; } else { Nci.TitleColor = Convert.ToString(rd["TitleColor"]); }
                Nci.TitleITF = Convert.ToByte(rd["TitleITF"]);
                if (rd["TitleBTF"] == DBNull.Value) { Nci.TitleBTF = 0; } else { Nci.TitleBTF = Convert.ToByte(rd["TitleBTF"]); }
                if (rd["CommLinkTF"] == DBNull.Value) { Nci.CommLinkTF = 0; } else { Nci.CommLinkTF = Convert.ToByte(rd["CommLinkTF"]); }
                if (rd["SubNewsTF"] == DBNull.Value) { Nci.SubNewsTF = 0; } else { Nci.SubNewsTF = Convert.ToByte(rd["SubNewsTF"]); }
                if (rd["URLaddress"] == DBNull.Value) { Nci.URLaddress = ""; } else { Nci.URLaddress = Convert.ToString(rd["URLaddress"]); }
                if (rd["PicURL"] == DBNull.Value) { Nci.PicURL = ""; } else { Nci.PicURL = Convert.ToString(rd["PicURL"]); }
                if (rd["SPicURL"] == DBNull.Value) { Nci.SPicURL = ""; } else { Nci.SPicURL = Convert.ToString(rd["SPicURL"]); }
                Nci.ClassID = Convert.ToString(rd["ClassID"]);
                if (rd["SpecialID"] == DBNull.Value) { Nci.SpecialID = ""; } else { Nci.SpecialID = Convert.ToString(rd["SpecialID"]); }
                if (rd["Author"] == DBNull.Value) { Nci.Author = ""; } else { Nci.Author = Convert.ToString(rd["Author"]); }
                if (rd["Souce"] == DBNull.Value) { Nci.Souce = ""; } else { Nci.Souce = Convert.ToString(rd["Souce"]); }
                if (rd["Tags"] == DBNull.Value) { Nci.Tags = ""; } else { Nci.Tags = Convert.ToString(rd["Tags"]); }
                Nci.NewsProperty = Convert.ToString(rd["NewsProperty"]);
                Nci.NewsPicTopline = Convert.ToByte(rd["NewsPicTopline"]);
                if (rd["Templet"] == DBNull.Value) { Nci.Templet = ""; } else { Nci.Templet = Convert.ToString(rd["Templet"]); }
                if (rd["Content"] == DBNull.Value) { Nci.Content = ""; } else { Nci.Content = Convert.ToString(rd["Content"]); }
                if (rd["Metakeywords"] == DBNull.Value) { Nci.Metakeywords = ""; } else { Nci.Metakeywords = Convert.ToString(rd["Metakeywords"]); }
                if (rd["Metadesc"] == DBNull.Value) { Nci.Metadesc = ""; } else { Nci.Metadesc = Convert.ToString(rd["Metadesc"]); }
                if (rd["naviContent"] == DBNull.Value) { Nci.naviContent = ""; } else { Nci.naviContent = Convert.ToString(rd["naviContent"]); }
                Nci.Click = Convert.ToInt32(rd["Click"]);
                Nci.CreatTime = Convert.ToDateTime(rd["CreatTime"]);
                if (rd["EditTime"] == DBNull.Value) { Nci.EditTime = Convert.ToDateTime(rd["CreatTime"]); } else { Nci.EditTime = Convert.ToDateTime(rd["EditTime"]); }
                if (rd["SavePath"] == DBNull.Value) { Nci.SavePath = ""; } else { Nci.SavePath = Convert.ToString(rd["SavePath"]); }
                Nci.FileName = Convert.ToString(rd["FileName"]);
                Nci.FileEXName = Convert.ToString(rd["FileEXName"]);
                Nci.isDelPoint = Convert.ToByte(rd["isDelPoint"]);
                Nci.Gpoint = Convert.ToInt32(rd["Gpoint"]);
                Nci.iPoint = Convert.ToInt32(rd["iPoint"]);
                if (rd["GroupNumber"] == DBNull.Value) { Nci.GroupNumber = ""; } else { Nci.GroupNumber = Convert.ToString(rd["GroupNumber"]); }
                Nci.ContentPicTF = Convert.ToByte(rd["ContentPicTF"]);
                if (rd["ContentPicURL"] == DBNull.Value) { Nci.ContentPicURL = ""; } else { Nci.ContentPicURL = Convert.ToString(rd["ContentPicURL"]); }
                if (rd["ContentPicSize"] == DBNull.Value) { Nci.ContentPicSize = ""; } else { Nci.ContentPicSize = Convert.ToString(rd["ContentPicSize"]); }
                Nci.CommTF = Convert.ToByte(rd["CommTF"]);
                Nci.DiscussTF = Convert.ToByte(rd["DiscussTF"]);
                Nci.TopNum = Convert.ToInt32(rd["TopNum"]);
                Nci.VoteTF = Convert.ToByte(rd["VoteTF"]);
                if (rd["CheckStat"] == DBNull.Value) { Nci.CheckStat = ""; } else { Nci.CheckStat = Convert.ToString(rd["CheckStat"]); }
                Nci.isLock = Convert.ToByte(rd["isLock"]);
                Nci.isRecyle = Convert.ToByte(rd["isRecyle"]);
                Nci.SiteID = Convert.ToString(rd["SiteID"]);
                Nci.DataLib = Convert.ToString(rd["DataLib"]);
                if (rd["DefineID"] == DBNull.Value) { Nci.DefineID = 0; } else { Nci.DefineID = Convert.ToByte(rd["DefineID"]); }
                Nci.isVoteTF = Convert.ToByte(rd["isVoteTF"]);
                if (rd["Editor"] == DBNull.Value) { Nci.Editor = ""; } else { Nci.Editor = Convert.ToString(rd["Editor"]); }
                Nci.isHtml = Convert.ToByte(rd["isHtml"]);
                Nci.isConstr = Convert.ToByte(rd["isConstr"]);
                if (rd["isFiles"] == DBNull.Value) { Nci.isFiles = 0; } else { Nci.isFiles = Convert.ToByte(rd["isFiles"]); }
                if (rd["vURL"] == DBNull.Value) { Nci.vURL = ""; } else { Nci.vURL = Convert.ToString(rd["vURL"]); }
            }
            rd.Close();
            return Nci;
        }
    }
}
