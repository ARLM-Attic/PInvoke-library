using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using Foosun.Config;
using Foosun.Model;
//模型，频道生成
namespace Foosun.Publish
{
    public partial class LabelMass
    {
        /// <summary>
        /// 生成列表类信息，频道
        /// </summary>
        /// <param name="ChID">频道ID</param>
        /// <returns>返回值</returns>
        public string Analyse_ChannellList(string Tags,int ChID)
        {
            //判断数据库是否存在
            string ChTable = CommonData.DalPublish.GetCHDatable(ChID);
            if (ChTable == "#")
            {
                return "频道数据库找不到!";
            }
            string mystyle = this.Mass_Inserted;
            string styleid = Regex.Match(mystyle, @"\[\#FS:StyleID=(?<sid>[^\]]+)]", RegexOptions.Compiled).Groups["sid"].Value.Trim();
            if (!styleid.Equals(string.Empty))
            {
                mystyle = LabelStyle.GetCHStyleByID(int.Parse(styleid), ChID);
            }
            if (mystyle.Trim().Equals(string.Empty))
                return string.Empty;

            string str_NewsType = this.GetParamValue("FS:Type");
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_SpecialID = this.GetParamValue("FS:SpecialID");
            int n_Cols;
            if (!int.TryParse(this.GetParamValue("FS:Cols"), out n_Cols))
                n_Cols = 1;
            if (n_Cols < 1)
                n_Cols = 1;
            string str_Desc = this.GetParamValue("FS:Desc");
            string str_DescType = this.GetParamValue("FS:OrderBy");
            string str_isDiv = this.GetParamValue("FS:isDiv");
            string str_isPic = this.GetParamValue("FS:isPic");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_ClickNumber = this.GetParamValue("FS:ClickNumber");
            string str_ShowDateNumer = this.GetParamValue("FS:ShowDateNumer");
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_ColbgCSS = this.GetParamValue("FS:ColbgCSS");
            string SqlFields = " [ID] ";
            string SqlCondition = ChTable + " Where [islock]=0";
            //-------判断是否调用图片
            if (str_isPic == "true")
            {
                SqlCondition += " And [PicURL]<>''";
            }
            else if (str_isPic == "false")
            {
                SqlCondition += "And [PicURL]=''";
            }
            //-------判断是否显示点击率大于多少
            if (str_ClickNumber != null && str_ClickNumber != "")
            {
                SqlCondition += " And [Click] > " + int.Parse(str_ClickNumber);
            }
            //-------判断显示最近多少天内信息
            if (str_ShowDateNumer != null && str_ShowDateNumer != "")
            {
                if (Foosun.Config.UIConfig.WebDAL.ToLower() == "foosun.accessdal")
                {
                    SqlCondition += " And DateDiff('d',[CreatTime] ,now()) < " + int.Parse(str_ShowDateNumer);
                }
                else
                {
                    SqlCondition += " And DateDiff(Day,[CreatTime] ,Getdate()) < " + int.Parse(str_ShowDateNumer);
                }
            }
            //判断是否相关新闻
            if (Tags != null && Tags != string.Empty)
            {
                SqlCondition += " And ([Tags] Like '%" + Tags + "%' or title Like '%" + Tags + "%')";
            }

            ///判断新闻类型  推荐|热点|幻灯|滚动|头条
            switch (str_NewsType)
            {
                case "last":
                    break;
                case "rec":
                    SqlCondition += " And ContentProperty like '1%'";
                    break;
                case "mar":
                    SqlCondition += " And ContentProperty like '______1%'";
                    break;
                case "hot":
                    SqlCondition += " And ContentProperty like '__1%'";
                    break;
                case "filt":
                    SqlCondition += " And ContentProperty like '____1%'";
                    break;
                case "tnews":
                    SqlCondition += " And ContentProperty like '________1%'";
                    break;
                case "special":
                    if (str_SpecialID != null)
                    {
                        SqlCondition += " And SpecialID='" + str_SpecialID + "'";
                    }
                    else if (this.Param_CurrentSpecialID != null)
                    {
                        SqlCondition += " And SpecialID='" + this.Param_CurrentSpecialID + "'";
                    }
                    else
                    {
                        return string.Empty;
                    }
                    break;
                case "constr":
                    SqlCondition += " And [isConstr]=1";
                    break;
                default:
                    break;
            }
            string SqlOrderBy = string.Empty;
            //-------排序
            if (str_NewsType == "last")
            {
                SqlOrderBy += " order by CreatTime desc,ID Desc";
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
                    case "CreatTime":
                        SqlOrderBy = " Order By [CreatTime] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    case "click":
                        SqlOrderBy = " Order By [Click] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    case "orderid":
                        SqlOrderBy = " Order By [OrderID]" + SqlOrderBy + ",id " + SqlOrderBy + "";
                        break;
                    default:
                        if (str_NewsType == "hot")
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
            #region 对栏目进行判断
            string Sql = string.Empty;
            #region 子类
            if (str_ClassID == null || str_ClassID == "0")
            {
                if (this._TemplateType == TempType.ChClass)
                {
                    SqlCondition += " And [ClassID]=" + this.Param_CurrentCHClassID + "";
                    Sql = "select top " + Param_Loop + " " + SqlFields + " from " + SqlCondition + " " + SqlOrderBy;
                }
                else
                {
                    Sql = "select top " + Param_Loop + " " + SqlFields + " from " + SqlCondition + SqlOrderBy;
                }
            }
            else if (str_ClassID == "-1")
            {
                Sql = "select top " + Param_Loop + " " + SqlFields + " from " + SqlCondition + SqlOrderBy;
            }
            else
            {
                SqlCondition += " And [ClassID] =" + int.Parse(str_ClassID) + "";
                Sql = "select top " + Param_Loop + " " + SqlFields + " from " + SqlCondition + SqlOrderBy;
            }
            #endregion
            #endregion 对栏目进行判断
            DataTable dt = CommonData.DalPublish.ExecuteSql(Sql);
            if (dt == null || dt.Rows.Count < 1) return string.Empty;
            string str_newslist = string.Empty;
            int i;
            int nTitleNum = 30;
            if (str_TitleNumer != null && Foosun.Common.Input.IsInteger(str_TitleNumer))
            {
                nTitleNum = int.Parse(str_TitleNumer);
            }
            int dtcount = dt.Rows.Count;
            string[] arr_ColbgCSS = null;
            bool b_ColbgCss = false;
            if (str_ColbgCSS != null)
            {
                arr_ColbgCSS = str_ColbgCSS.Split('|');
                b_ColbgCss = true;
            }

            string row = string.Empty;

            for (i = 0; i < dtcount; i++)
            {
                str_ColbgCSS = "";
                if (b_ColbgCss)
                {
                    if (i % 2 == 0)
                        str_ColbgCSS = " class=\"" + arr_ColbgCSS[0].ToString() + "\"";
                    else
                        str_ColbgCSS = " class=\"" + arr_ColbgCSS[1].ToString() + "\"";
                }

                if (str_isDiv == "false")
                {
                    row = getNavi(str_ShowNavi, str_NaviCSS, "", i) + " ";
                    row += Analyse_ChRead((int)dt.Rows[i][0], nTitleNum, mystyle, styleid, 0, ChTable, ChID);
                    if (n_Cols == 1)
                    {
                        str_newslist += "<tr>" + newLine + "<td" + str_ColbgCSS + ">" + newLine + row + newLine + "</td>" + newLine + "</tr>" + newLine;
                    }
                    else
                    {
                        row = "<td width=\"" + (100 / n_Cols) + "%\"" + str_ColbgCSS + ">" + newLine + row + newLine + "</td>" + newLine;
                        if (i > 0 && ((i + 1) % n_Cols == 0))
                            row += "</tr>" + newLine + "<tr>" + newLine;
                        str_newslist += row;
                    }
                }
                else
                {
                    str_isDiv = "true";
                    str_newslist += getNavi(str_ShowNavi, str_NaviCSS, "", i);
                    str_newslist += Analyse_ChRead((int)dt.Rows[i][0], nTitleNum, mystyle, styleid, 0, ChTable, ChID);
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
            str_newslist = News_List_Head(str_isDiv, "", "") + str_newslist + News_List_End(str_isDiv);
            return str_newslist;
        }

        /// <summary>
        /// 生成终极类信息，频道
        /// </summary>
        /// <param name="ChID">频道ID</param>
        /// <returns>返回值</returns>
        public string Analyse_ChannelClassList(int ChID)
        {
            //判断数据库是否存在
            string ChTable = CommonData.DalPublish.GetCHDatable(ChID);
            if (ChTable == "#")
            {
                return "频道数据库找不到";
            }
            string mystyle = this.Mass_Inserted;
            string styleid = Regex.Match(mystyle, @"\[\#FS:StyleID=(?<sid>[^\]]+)]", RegexOptions.Compiled).Groups["sid"].Value.Trim();
            if (!styleid.Equals(string.Empty))
            {
                mystyle = LabelStyle.GetCHStyleByID(int.Parse(styleid), ChID);
            }
            if (mystyle.Trim().Equals(string.Empty))
                return string.Empty;
            string str_NewsType = this.GetParamValue("FS:Type");
            int n_Cols;
            if (!int.TryParse(this.GetParamValue("FS:Cols"), out n_Cols))
                n_Cols = 1;
            if (n_Cols < 1)
                n_Cols = 1;
            string str_Desc = this.GetParamValue("FS:Desc");
            string str_DescType = this.GetParamValue("FS:OrderBy");
            if (str_DescType == null)
            {
                str_DescType = "id";
            }
            string str_isDiv = this.GetParamValue("FS:isDiv");
            if (str_isDiv == null)
            {
                str_isDiv = "true";
            }
            string str_bfStr = this.GetParamValue("FS:bfStr");
            string str_isPic = this.GetParamValue("FS:isPic");
            string str_TitleNumer = this.GetParamValue("FS:TitleNumer");
            string str_ShowNavi = this.GetParamValue("FS:ShowNavi");
            string str_NaviCSS = this.GetParamValue("FS:NaviCSS");
            string str_ColbgCSS = this.GetParamValue("FS:ColbgCSS");
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
            string getWhere = "";
            if (str_NewsType == "info")
            {
                getWhere += " and ClassID=" + this.Param_CurrentCHClassID + "";
            }
            else
            {
                getWhere += " And SpecialID='" + this.Param_CurrentCHSpecialID + "'";
            }

            string SqlFields = " [id] ";
            string SqlCondition = ChTable + " Where [isLock]=0 " + getWhere + "";
            //-------判断是否调用图片
            if (str_isPic == "true")
            {
                SqlCondition += " And [PicURL]<>''";
            }
            else if (str_isPic == "false")
            {
                SqlCondition += " And [PicURL]='' ";
            }

            string getPageTF = "{$FS:P0}{Page:0$$}";
            int PerPageNum = 30;
            string PageStyle = "0";
            string PageTitleColor = "";
            string PageCSS = "";
            if (str_PageStyle != string.Empty && str_PageStyle != null)
            {
                string[] PageARR = str_PageStyle.Split('$');
                PageStyle = PageARR[0].ToString();
                PerPageNum = int.Parse(PageARR[2].ToString());
                PageTitleColor = PageARR[1].ToString();
                PageCSS = PageARR[3].ToString();
            }
            getPageTF = "{$FS:P0}{Page:" + PageStyle + "$" + PageTitleColor + "$" + PageCSS + "}";
            #region 排序
            string SqlOrderBy = string.Empty;
            //-------排序
            if (str_Desc != null && str_Desc.ToLower() == "asc")
            {
                SqlOrderBy += " asc";
            }
            else
            {
                SqlOrderBy += " Desc";
            }
            switch (str_DescType.ToLower())
            {
                case "id":
                    SqlOrderBy = " Order By id " + SqlOrderBy + "";
                    break;
                case "creattime":
                    SqlOrderBy = " Order By [CreatTime] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                    break;
                case "click":
                    SqlOrderBy = " Order By [Click] " + SqlOrderBy + ",id " + SqlOrderBy + "";
                    break;
                case "orderid":
                    SqlOrderBy = " Order By [OrderID]" + SqlOrderBy + ",id " + SqlOrderBy + "";
                    break;
                default:
                    SqlOrderBy = " Order By [CreatTime]" + SqlOrderBy + ",id " + SqlOrderBy + "";
                    break;
            }
            #endregion 排序
            string Sql = "select " + SqlFields + " from " + SqlCondition + SqlOrderBy;
            DataTable dt = CommonData.DalPublish.ExecuteSql(Sql);
            if (dt == null || dt.Rows.Count < 1) return string.Empty;
            string str_newslist = string.Empty;
            int i;
            int nTitleNum = 30;
            if (str_TitleNumer != null && Foosun.Common.Input.IsInteger(str_TitleNumer))
            {
                nTitleNum = int.Parse(str_TitleNumer);
            }
            int dtcount = dt.Rows.Count;
            str_newslist = "{Foosun:NewsLIST}" + News_List_Head(str_isDiv, "", "");
            string tmpPageLineContent1 = "";
            if (str_isDiv != "true")
            {
                str_newslist += News_List_Head(str_isDiv, "", "");
                if (n_Cols != 1)
                {
                    str_newslist += "<tr>";
                }
            }
            for (i = 0; i < dtcount; i++)
            {
                if ((i + 1) % PageLineNum == 0)
                {
                    tmpPageLineContent1 = PageLineContent1;
                }
                else
                {
                    tmpPageLineContent1 = "";
                }
                if (str_isDiv == "false")
                {
                    string row = getNavi(str_ShowNavi, str_NaviCSS, "", i);
                    row += Analyse_ChRead((int)dt.Rows[i][0], nTitleNum, mystyle, styleid, 0, ChTable, ChID);
                    if (n_Cols == 1)
                    {
                        row += tmpPageLineContent1;
                    }
                    if (n_Cols == 1)
                    {
                        str_newslist += "<tr>" + newLine + "<td>" + newLine + row + newLine + "</td>" + newLine + "</tr>" + newLine;
                    }
                    else
                    {
                        row = "<td width=\"" + (100 / n_Cols) + "%\">" + newLine + row + newLine + "</td>" + newLine;
                        if (((i + 1) % n_Cols == 0))
                        {
                            if ((i + 1) < dtcount)
                            {
                                row += "</tr>" + newLine + "<tr>" + newLine;
                            }
                        }
                        str_newslist += row;
                    }
                }
                else
                {
                    str_isDiv = "true";
                    str_newslist += getNavi(str_ShowNavi, str_NaviCSS, "", i);
                    str_newslist += Analyse_ChRead((int)dt.Rows[i][0], nTitleNum, mystyle, styleid, 0, ChTable, ChID) + tmpPageLineContent1;
                }

                if ((i + 1) % (PerPageNum) == 0)
                {
                    if ((i + 1) < dtcount)
                    {
                        str_newslist += News_List_End(str_isDiv) + getPageTF + News_List_Head(str_isDiv, "", "");
                    }
                }
                if (str_isDiv == "false")
                {
                    if ((i + 1) == dtcount)
                    {
                        if (n_Cols != 1)
                        {
                            str_newslist += "</tr>" + newLine;
                        }
                        str_newslist += News_List_End(str_isDiv) + newLine;
                    }
                }
            }
            dt.Clear(); dt.Dispose();
            return str_newslist + "{/Foosun:NewsLIST}";
        }

        /// <summary>
        /// 生成浏览类信息，频道
        /// </summary>
        /// <param name="ChID">频道ID</param>
        /// <returns>返回值</returns>
        public string Analyse_ChannelContent(int ChID)
        {
            //判断数据库是否存在
            string ChTable = CommonData.DalPublish.GetCHDatable(ChID);
            if (ChTable == "#")
            {
                return "频道数据库找不到";
            }
            return Analyse_ChRead(this.Param_CurrentCHNewsID, 0, "", "", 1, ChTable, ChID);
        }

        public string Analyse_ChannelSearch(int ChID)
        {
            string str_Search = "<div><form id=\"SearchCH_Form_" + ChID + "\" name=\"SearchCH_Form_" + ChID + "\" method=\"get\" action=\"search.html\">";
            string str_Type = this.GetParamValue("FS:Type");
            string str_Cols = this.GetParamValue("FS:Cols");
            string str_RnadNum = Foosun.Common.Rand.Number(3);
            string divPreStr = string.Empty;
            string divReStr = "&nbsp;";
            if (str_Cols == "single")
            {
                divPreStr = "<div>";
                divReStr = "</div>";
            }
            if (str_Type == "normal")
            {
                str_Search += divPreStr + "<input name=\"tags\" type=\"text\"  size=\"10\" maxlength=\"20\" onkeydown=\"javascript:if(event.keyCode==13){SearchCHGo_" + str_RnadNum + "(this.form);}\" />" + divReStr;
                str_Search += divPreStr + "<input name=\"buttongo\" type=\"button\" value=\"搜索\" onclick=\"javascript:SearchCHGo_" + str_RnadNum + "(this.form);\">" + divReStr;
            }
            else
            {
                str_RnadNum = Foosun.Common.Rand.Number(4);
                str_Search += divPreStr + "<input id=\"tags\" name=\"tags\" type=\"text\"  size=\"10\" maxlength=\"20\" onkeydown=\"javascript:if(event.keyCode==13){SearchCHGo_" + str_RnadNum + "(this.form);}\" />" + divReStr;
                str_Search += divPreStr + "<select name=\"fieldname\"  id=\"fieldname\">";
                str_Search += "<option value=\"title\">标题</option>" + newLine;
                str_Search += "<option value=\"content\">全文</option>" + newLine;
                str_Search += "<option value=\"author\">作者</option>" + newLine;
                str_Search += "<option value=\"TAGS\">TAG</option>" + newLine;
                IDataReader dr = CommonData.DalPublish.GetFieldName(this.Param_ChID);
                while (dr.Read())
                {
                    str_Search += "<option value=\"" + dr["EName"].ToString() + "\">" + dr["CName"].ToString() + "</option>" + newLine;
                }
                dr.Close();
                str_Search += "";
                str_Search += "</select >" + divReStr;
                str_Search += divPreStr + "<input name=\"buttongo\" type=\"button\" value=\"搜索\" onclick=\"javascript:SearchCHGo_" + str_RnadNum + "(this.form);\">" + divReStr;
            }
            str_Search += "</form></div>";
            str_Search += "<script language=\"javascript\" type=\"text/javascript\">" + newLine;
            str_Search += "function SearchCHGo_" + str_RnadNum + "(obj)" + newLine;
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
            if (str_Type == "normal")
            {
                str_Search += "window.location.href='" + CommonData.SiteDomain + "/Search.html?type=news";
                str_Search += "&ChID='+" + ChID + "+'";
                str_Search += "&tags='+escape(obj.tags.value)+'';" + newLine;
            }
            else
            {
                str_Search += "window.location.href='" + CommonData.SiteDomain + "/Search.html?type=news&ChID='+" + ChID + "+'&tags='+escape(obj.tags.value)+'&fieldname='+obj.fieldname.value+'';" + newLine;
            }
            str_Search += "}" + newLine;
            str_Search += "</script>" + newLine;
            return str_Search;
        }

        public string Analyse_ChannelRSS(int ChID)
        {
            string str_ClassID = this.GetParamValue("FS:ClassID");
            if (str_ClassID == null)
            {
                str_ClassID = "0";
            }
            string rsslist = string.Empty;
            if (str_ClassID == "0")
            {
                if (this.Param_CurrentCHClassID == 0)
                {
                    rsslist += CommonData.SiteDomain + "/xml/channel/" + ChID + "_index.xml";
                }
                else
                {
                    rsslist += CommonData.SiteDomain + "/xml/channel/" + ChID + "_" + this.Param_CurrentCHClassID + ".xml";
                }
            }
            else
            {
                rsslist += CommonData.SiteDomain + "/xml/channel/" + ChID + "_" + str_ClassID + ".xml";
            }
            return rsslist;
        }

        public string Analyse_ChannelFlash(int ChID)
        {
            string str_FlashFilt = "暂无幻灯新闻";
            string ChTable = CommonData.DalPublish.GetCHDatable(ChID);
            if (ChTable == "#")
            {
                return "频道数据库找不到";
            }
            string str_ClassID = this.GetParamValue("FS:ClassID");
            string str_Flashweight = this.GetParamValue("FS:Flashweight");
            string str_Flashheight = this.GetParamValue("FS:Flashheight");
            string str_FlashBG = this.GetParamValue("FS:FlashBG");
            string str_ShowTitle = this.GetParamValue("FS:ShowTitle");
            string str_Number = this.GetParamValue("FS:Number");
            int IntNumber=5;
            if (str_Number != null && Foosun.Common.Input.IsInteger(str_Number))
            {
                IntNumber = int.Parse(str_Number);
            }
            if (str_Flashweight == null && Foosun.Common.Input.IsInteger(str_Flashweight)==false)
            {
                str_Flashweight = "200";
            }
            if (str_Flashheight == null && Foosun.Common.Input.IsInteger(str_Flashheight) == false)
            {
                str_Flashheight = "150";
            }
            if (str_FlashBG == null)
            {
                str_FlashBG = "FFF";
            }
            string SqlCondition = " Where [isLock]=0 And [ChID]=" + ChID + " and And ContentProperty like '____1%'";
            string SqlOrderBy = " Order By [CreatTime] Desc";

            #region 对栏目进行判断
            DataTable dt = null;
            string Sql = string.Empty;
            if (str_ClassID != null)
            {
                switch (str_ClassID)
                {
                    case "0":
                        if (this._TemplateType == TempType.ChClass)
                        {
                            Sql = "select top " + IntNumber + " * from [" + ChTable + "] " + SqlCondition + " And ClassID=" + this.Param_CurrentCHClassID + " " + SqlOrderBy;
                        }
                        break;
                    case "-1":
                        Sql = "select top " + IntNumber + " * from [" + ChTable + "]" + SqlCondition + SqlOrderBy;
                        break;
                    default:
                        if (Foosun.Common.Input.IsInteger(str_ClassID))
                        {
                            Sql = "select top " + IntNumber + " * from [" + ChTable + "] and ClassID=" + int.Parse(str_ClassID) + "" + SqlCondition + SqlOrderBy;
                        }
                        break;
                }
            }
            else
            {
                Sql = "select top " + IntNumber + " * from [" + ChTable + "] " + SqlCondition + SqlOrderBy;
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
                        PubCHClassInfo ci = CommonData.GetCHClassById(int.Parse(dt.Rows[i]["ClassID"].ToString()));
                        Pics_Path += dt.Rows[i]["PicURL"].ToString() + "|";
                        Link_Str += getCHInfoURL(ChID,int.Parse(dt.Rows[i]["isDelPoint"].ToString()), int.Parse(dt.Rows[i]["id"].ToString()), ci.SavePath, dt.Rows[i]["SavePath"].ToString(), dt.Rows[i]["FileName"].ToString()) + "|";
                        Title_Str += dt.Rows[i]["Title"].ToString() + "|";
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

        public string Analyse_ChRead(int ID, int TitleNumer, string str_Style, string styleid, int NewsTF, string DTable, int ChID)
        {
            Foosun.Model.ChContentParam Nci = new Foosun.Model.ChContentParam();
            if (NewsTF == 1)
            {
                Nci = this.GetCHInfo(this.Param_CurrentCHNewsID, DTable);
            }
            else
            {
                Nci = this.GetCHInfo(ID, DTable);
            }

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
                    str_Style = LabelStyle.GetCHStyleByID(int.Parse(styleids),ChID);
                }
                if (str_Style.Trim() == string.Empty)
                {
                    return string.Empty;
                }
            }
            if (Nci != null)
            {
                if (TitleNumer <= 0)
                    TitleNumer = 15;
                PubCHClassInfo ci = CommonData.GetCHClassById(Nci.ClassID);
                if (ci == null)
                {
                    ci = new PubCHClassInfo();
                }
                PubCHSpecialInfo si = new PubCHSpecialInfo();
                if (Nci.SpecialID != "")
                {
                    si = CommonData.GetCHSpecial(int.Parse(Nci.SpecialID));
                }
                #region 基本
                //标题--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Title}") > -1)
                {
                    string str_title = Nci.Title;
                    if (NewsTF == 0)
                    {
                        str_title = getStyle(Foosun.Common.Input.GetSubString(str_title, TitleNumer), Nci.TitleColor, Nci.TitleITF, Nci.TitleBTF);
                    }
                    str_Style = str_Style.Replace("{CH#Title}", str_title);
                }
                //标题(不可截断)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#uTitle}") > -1)
                    if (NewsTF == 1)
                    {
                        str_Style = str_Style.Replace("{CH#uTitle}", Nci.Title);
                    }
                    else
                    {
                        str_Style = getStyle(Nci.Title, Nci.TitleColor, Nci.TitleITF, Nci.TitleBTF);
                    }
                //连接地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#URL}") > -1)
                {
                    string urls = getCHInfoURL(ChID,Nci.isDelPoint, Nci.ID, ci.SavePath, Nci.SavePath, Nci.FileName);
                    str_Style = str_Style.Replace("{CH#URL}", urls);
                }
                //新闻内容--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Content$") > -1)
                {
                    string str_content = Nci.Content;
                    string tmpcontent = str_content;
                    string ContentNumber = string.Empty;
                    int CHNumber = 0;
                    string pattern = @"\{CH\#Content\$(?<p>[\s\S]+?)\}";
                    Regex Greg = new Regex(pattern, RegexOptions.Compiled);
                    Match mc = Greg.Match(str_Style);
                    if (mc.Success)
                    {
                        ContentNumber = mc.Groups["p"].Value;
                    }
                    if (Foosun.Common.Input.IsInteger(ContentNumber) && ContentNumber.Trim() != "0")
                    {
                        CHNumber = int.Parse(ContentNumber);
                    }
                    if (NewsTF == 0)
                    {
                        string LostResultStr = Foosun.Common.Input.LostHTML(str_content);
                        LostResultStr = Foosun.Common.Input.LostPage(LostResultStr);
                        if (CHNumber == 0)
                        {
                            str_content = Foosun.Common.Input.GetSubString(LostResultStr, 200) + "...";
                        }
                        else
                        {
                            str_content = Foosun.Common.Input.GetSubString(LostResultStr, CHNumber) + "...";
                        }
                        str_Style = str_Style.Replace("{CH#Content$" + ContentNumber + "}", str_content.Replace("[FS:PAGE]", "").Replace("[FS:PAGE", "").Replace("$]", ""));
                    }
                    else
                    {
                        if (str_Style.IndexOf("{CH#PageTitle_select}") > -1 || str_Style.IndexOf("{CH#PageTitle_textdouble}") > -1 || str_Style.IndexOf("{CH#PageTitle_textsinge}") > -1 || str_Style.IndexOf("{CH#PageTitle_textcols}") > -1)
                        {
                            string GetPagecontent = tmpcontent;
                            string Re_Content = string.Empty;
                            string Pagetitstr = string.Empty;
                            if (GetPagecontent.IndexOf("[FS:PAGE=") > -1 && GetPagecontent.IndexOf("$]") > -1)
                            {
                                string pattern1 = @"\[FS:PAGE=(?<p>[\s\S]+?)\$\]";
                                Regex reg = new Regex(pattern1, RegexOptions.Compiled);
                                Match m = reg.Match(GetPagecontent);
                                while (m.Success)
                                {
                                    Pagetitstr += m.Groups["p"].Value + "###";
                                    m = m.NextMatch();
                                }
                                tmpcontent = reg.Replace(tmpcontent, "[FS:PAGE]");
                                if (str_Style.IndexOf("{CH#PageTitle_select}") > -1)
                                {
                                    Re_Content = getPageTitleStyle(Nci.ID.ToString(), Nci.FileName, "", Pagetitstr, 0, Nci.isDelPoint, ChID);
                                    str_Style = str_Style.Replace("{#PageTitle_select}", Re_Content);
                                }
                                if (str_Style.IndexOf("{CH#PageTitle_textdouble}") > -1)
                                {
                                    Re_Content = getPageTitleStyle(Nci.ID.ToString(), Nci.FileName, "", Pagetitstr, 1, Nci.isDelPoint, ChID);
                                    str_Style = str_Style.Replace("{#PageTitle_textdouble}", Re_Content);
                                }
                                if (str_Style.IndexOf("{CH#PageTitle_textsinge}") > -1)
                                {
                                    Re_Content = getPageTitleStyle(Nci.ID.ToString(), Nci.FileName, "", Pagetitstr, 2, Nci.isDelPoint, ChID);
                                    str_Style = str_Style.Replace("{#PageTitle_textsinge}", Re_Content);
                                }
                                if (str_Style.IndexOf("{CH#PageTitle_textcols}") > -1)
                                {
                                    Re_Content = getPageTitleStyle(Nci.ID.ToString(), Nci.FileName, "", Pagetitstr, 3, Nci.isDelPoint, ChID);
                                    str_Style = str_Style.Replace("{CH#PageTitle_textcols}", Re_Content);
                                }
                            }
                            else
                            {
                                str_Style = str_Style.Replace("{CH#PageTitle_select}", "");
                                str_Style = str_Style.Replace("{CH#PageTitle_textdouble}", "");
                                str_Style = str_Style.Replace("{CH#PageTitle_textsinge}", "");
                                str_Style = str_Style.Replace("{CH#PageTitle_textcols}", "");
                            }
                        }
                        else
                        {
                            str_Style = str_Style.Replace("{CH#PageTitle_select}", "");
                            str_Style = str_Style.Replace("{CH#PageTitle_textdouble}", "");
                            str_Style = str_Style.Replace("{CH#PageTitle_textsinge}", "");
                            str_Style = str_Style.Replace("{CH#PageTitle_textcols}", "");
                        }
                    }
                    if (Foosun.Common.Public.readparamConfig("collectTF") == "1")
                    {
                        tmpcontent = tmpcontent.Replace("<div", "<!--source from " + Foosun.Common.Public.readparamConfig("siteDomain") + "--><div");
                    }
                    str_Style = str_Style.Replace("{CH#Content$" + ContentNumber + "}", "<!-FS:STAR=" + tmpcontent + "FS:END->");
                }
                #endregion 基本
                #region 日期
                //录入时间：完整
                if (str_Style.IndexOf("{CH#Date}") > -1)
                    str_Style = str_Style.Replace("{CH#Date}", Nci.CreatTime.ToString() + "");
                //录入时间：年-月-日
                if (str_Style.IndexOf("{CH#DateShort}") > -1)
                    str_Style = str_Style.Replace("{CH#DateShort}", Nci.CreatTime.ToShortDateString().ToString() + "");
                //录入日期:二位年份--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Year02}") > -1)
                    str_Style = str_Style.Replace("{CH#Date:Year02}", Nci.CreatTime.Year.ToString().Remove(0, 2));
                //录入日期:四位年份--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Year04}") > -1)
                    str_Style = str_Style.Replace("{CH#Date:Year04}", Nci.CreatTime.Year.ToString());
                //录入日期:月份--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Month}") > -1)
                    str_Style = str_Style.Replace("{CH#Date:Month}", Nci.CreatTime.Month.ToString());
                //录入日期:日--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Day}") > -1)
                {
                    str_Style = str_Style.Replace("{CH#Date:Day}", Nci.CreatTime.Day.ToString());
                }
                //录入日期:时--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Hour}") > -1)
                {
                    str_Style = str_Style.Replace("{CH#Date:Hour}", Nci.CreatTime.Hour.ToString());
                }
                //录入日期:分--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Minute}") > -1)
                {
                    str_Style = str_Style.Replace("{CH#Date:Minute}", Nci.CreatTime.Minute.ToString());
                }
                //录入日期:秒--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Date:Second}") > -1)
                {
                    str_Style = str_Style.Replace("{CH#Date:Second}", Nci.CreatTime.Second.ToString());
                }
                //点击--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Click}") > -1)
                {
                    if (NewsTF == 0)
                    {
                        str_Style = str_Style.Replace("{CH#Click}", Nci.Click.ToString());
                    }
                    else
                    {
                        string str_Click = "<span id=\"click_CH_" + ChID + "_" + Nci.ID + "\"></span><script language=\"javascript\" type=\"text/javascript\">";
                        str_Click += "pubajax('" + CommonData.SiteDomain + "/click.aspx','id=" + Nci.ID + "&ChID=" + ChID + "','click_CH_" + ChID + "_" + Nci.ID + "');";
                        str_Click += "</script>";
                        str_Style = str_Style.Replace("{CH#Click}", str_Click);
                    }
                }
                #endregion 日期
                #region 其他
                //来源--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Source}") > -1)
                {
                    if (Nci.Souce != string.Empty)
                        str_Style = str_Style.Replace("{CH#Source}", Nci.Souce);
                    else
                        str_Style = str_Style.Replace("{CH#Source}", "");
                }
                //编辑--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Editor}") > -1)
                {
                    if (Nci.Editor != "")
                        str_Style = str_Style.Replace("{CH#Editor}", "<a href=\"" + CommonData.SiteDomain + "/search.html?type=edit&tags=" + Foosun.Common.Input.URLEncode(Nci.Editor) + "&ChID=" + ChID + "\" title=\"查看此编辑的所有新闻\" target=\"_blank\">" + Nci.Editor + "</a>");
                    else
                        str_Style = str_Style.Replace("{CH#Editor}", "");
                }

                //作者--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Author}") > -1)
                {
                    if (Nci.Author != "")
                    {
                        if (Nci.isConstr == 1)
                        {
                            str_Style = str_Style.Replace("{CH#Author}", "<a href=\"" + CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/showuser-" + Nci.Author + ".aspx\" title=\"查看他的资料\">" + Nci.Author + "</a> <a href=\"" + CommonData.SiteDomain + "/search.html?type=author&tags=" + Nci.Author + "\" title=\"此看此作者所有的文章\" target=\"_blank\">发表的文章</a>");
                        }
                        else
                        {
                            str_Style = str_Style.Replace("{CH#Author}", "<a href=\"" + CommonData.SiteDomain + "/search.html?type=author&tags=" + Nci.Author + "&ChID=" + ChID + "\" title=\"此看此作者所有的文章\" target=\"_blank\">" + Nci.Author + "</a>");
                        }
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{CH#Author}", "");
                    }
                }


                //Meta关键字--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#MetaKeywords}") > -1)
                {
                    if (Nci.Metakeywords != "")
                    {
                        str_Style = str_Style.Replace("{CH#MetaKeywords}", Nci.Metakeywords);
                    }
                    else
                        str_Style = str_Style.Replace("{CH#MetaKeywords}", string.Empty);
                }
                //Meta描述--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Metadesc}") > -1)
                {
                    if (Nci.Metadesc != "")
                        str_Style = str_Style.Replace("{CH#Metadesc}", Nci.Metadesc);
                    else
                        str_Style = str_Style.Replace("{CH#Metadesc}", "");
                }
                //图片--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Picture}") > -1)
                {
                    if (Nci.PicURL != "")
                        str_Style = str_Style.Replace("{CH#Picture}", RelpacePicPath(Nci.PicURL));
                    else
                        str_Style = str_Style.Replace("{CH#Picture}", "");
                }
                //导读
                if (str_Style.IndexOf("{CH#NaviContent$") > -1)
                {
                    string NaviNumber = string.Empty;
                    int CHNaviNumber = 0;
                    string pattern = @"\{CH\#NaviContent\$(?<p>[\s\S]+?)\}";
                    Regex nreg = new Regex(pattern, RegexOptions.Compiled);
                    Match mn = nreg.Match(str_Style);
                    if (mn.Success)
                    {
                        NaviNumber = mn.Groups["p"].Value;
                    }
                    if (Foosun.Common.Input.IsInteger(NaviNumber) && NaviNumber.Trim() != "0")
                    {
                        CHNaviNumber = int.Parse(NaviNumber);
                    }

                    if (NewsTF == 1)
                    {
                        str_Style = str_Style.Replace("{CH#NaviContent$" + NaviNumber + "}", Nci.naviContent);
                    }
                    else
                    {
                        if (Nci.naviContent != "")
                        {
                            if (CHNaviNumber == 0)
                            {
                                str_Style = str_Style.Replace("{CH#NaviContent$" + NaviNumber + "}", Foosun.Common.Input.GetSubString(Nci.naviContent, 200));
                            }
                            else
                            {
                                str_Style = str_Style.Replace("{CH#NaviContent$" + NaviNumber + "}", Foosun.Common.Input.GetSubString(Nci.naviContent, CHNaviNumber));
                            }
                        }
                        else
                        {
                            str_Style = str_Style.Replace("{CH#NaviContent$" + NaviNumber + "}", "");
                        }
                    }
                }
                #endregion 其他
                #region 关键字
                //TAG(关键字)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Tags}") > -1)
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
                                tagstr += "<a href=\"" + CommonData.SiteDomain + "/Search.html?type=tag&tags=" + tagARR[im] + "&ChID=" + ChID + "\" target=\"_blank\">" + tagARR[im] + "</a>&nbsp;&nbsp;";
                            }
                        }
                        else
                        {
                            tagstr = "<a href=\"" + CommonData.SiteDomain + "/Search.html?type=tag&tags=" + tagdef + "&ChID=" + ChID + "\"  target=\"_blank\">" + tagdef + "</a>";
                        }
                        str_Style = str_Style.Replace("{CH#Tags}", tagstr);
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{CH#Tags}", "");
                    }
                }
                #endregion 关键字
                #region 互动
                //评论表单--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#CommForm}") > -1)
                    str_Style = str_Style.Replace("{CH#CommForm}", getCommForm(Nci.ID.ToString(), NewsTF, ChID));
                //总评论数--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#CommCount}") > -1)
                    str_Style = str_Style.Replace("{CH#CommCount}", getCommCount(Nci.ID.ToString(), NewsTF, 0, ChID));
                //最新评论数(今日)--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#LastCommCount}") > -1)
                    str_Style = str_Style.Replace("{CH#LastCommCount}", getCommCount(Nci.ID.ToString(), NewsTF, 1, ChID));
                //最新评论列表--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#LastComm}") > -1)
                    str_Style = str_Style.Replace("{CH#LastComm}", getLastComm(Nci.ID.ToString(), NewsTF, ChID));
                //发送给好友连接地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#SendInfo}") > -1)
                    str_Style = str_Style.Replace("{#SendInfo}", getSendInfo(Nci.ID.ToString(), ChID));
                //收藏连接地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#Collection}") > -1)
                    str_Style = str_Style.Replace("{#Collection}", getCollection(Nci.ID.ToString(), ChID));

                //上一篇--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#PrePage}") > -1)
                    str_Style = str_Style.Replace("{CH#PrePage}", getPrePage(Nci.ID.ToString(), DTable, Nci.ClassID.ToString(), 1, ChID, 0));
                //下一篇--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#NextPage}") > -1)
                    str_Style = str_Style.Replace("{CH#NextPage}", getPrePage(Nci.ID.ToString(), DTable, Nci.ClassID.ToString(), 0, ChID, 0));
                //上一篇标题--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#PrePageTitle}") > -1)
                    str_Style = str_Style.Replace("{CH#PrePageTitle}", getPrePage(Nci.ID.ToString(), DTable, Nci.ClassID.ToString(), 1, ChID, 1));
                //下一篇标题--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#NextPageTitle}") > -1)
                    str_Style = str_Style.Replace("{CH#NextPageTitle}", getPrePage(Nci.ID.ToString(), DTable, Nci.ClassID.ToString(), 0, ChID, 1));
                #endregion 互动
                #region 栏目2
                //栏目中文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_Name}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{CH#class_Name}", ci.classCName);
                    else
                        str_Style = str_Style.Replace("{CH#class_Name}", "");
                }
                //栏目英文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_EName}") > -1)
                {
                    if (ci != null)
                    {
                        str_Style = str_Style.Replace("{CH#class_EName}", ci.classEName);
                    }
                    else
                    {
                        str_Style = str_Style.Replace("{CH#class_EName}", "");
                    }
                }
                //栏目访问路径--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_Path}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{CH#class_Path}", getCHClassURL(ChID, ci.isDelPoint, ci.Id, ci.SavePath, ci.FileName));
                    else
                        str_Style = str_Style.Replace("{CH#class_Path}", "");
                }
                //栏目信息:导读--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_Navi}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{CH#class_Navi}", ci.NaviContent);
                    else
                        str_Style = str_Style.Replace("{CH#class_Navi}", "");
                }
                //栏目信息:导读图片地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_NaviPic}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{CH#class_NaviPic}", ci.PicURL);
                    else
                        str_Style = str_Style.Replace("{CH#class_NaviPic}", "");
                }
                //栏目信息:meta关键字--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_Keywords}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{CH#class_Keywords}", ci.MetaKeywords);
                    else
                        str_Style = str_Style.Replace("{CH#class_Keywords}", "");
                }
                //栏目信息:meta描述--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#class_Descript}") > -1)
                {
                    if (ci != null)
                        str_Style = str_Style.Replace("{CH#class_Descript}", ci.MetaDescript);
                    else
                        str_Style = str_Style.Replace("{CH#class_Descript}", "");
                }
                #endregion --

                #region 专题
                //专题中文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#special_Name}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{CH#special_Name}", si.specialCName);
                    else
                        str_Style = str_Style.Replace("{CH#special_Name}", "");
                }
                //专题英文名称--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#special_Ename}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{CH#special_Ename}", si.specialEName);
                    else
                        str_Style = str_Style.Replace("{CH#special_Ename}", "");
                }
                //专题连接路径--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#special_Path}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{CH#special_Path}", getCHSpecialURL(ChID,0, si.Id, si.savePath, si.filename));
                    else
                        str_Style = str_Style.Replace("{CH#special_Path}", "");
                }
                //专题导航文字--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#special_NaviWords}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{CH#special_NaviWords}", si.navicontent);
                    else
                        str_Style = str_Style.Replace("{CH#special_NaviWords}", "");
                }
                //专题导航图片地址--------------------------------------------------------------------------------------------------------
                if (str_Style.IndexOf("{CH#special_NaviPic}") > -1)
                {
                    if (si != null)
                        str_Style = str_Style.Replace("{CH#special_NaviPic}", si.PicURL);
                    else
                        str_Style = str_Style.Replace("{CH#special_NaviPic}", "");
                }

                #endregion 专题
                #region 自定义字段
                string pattern_define = @"\{CH\$(?<dname>[^\}]+)}";
                Regex regPage = new Regex(pattern_define, RegexOptions.Compiled);
                Match mPage = regPage.Match(str_Style);
                while (mPage.Success)
                {
                    string ChannEname = mPage.Groups["dname"].Value;
                    string str_definedvalue = CommonData.DalPublish.GetCHDefinedValue(Nci.ID, ChannEname, DTable);
                    str_Style = str_Style.Replace("{CH$" + ChannEname + "}", str_definedvalue);
                    mPage = mPage.NextMatch();
                }

                if (styleid.Equals(string.Empty))
                {
                    return str_Style;
                }
                else
                {
                    return Mass_Inserted.Replace("[#FS:StyleID=" + styleid + "]", str_Style);
                }
                #endregion 自定义
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 得到频道信息参数
        /// </summary>
        protected  Foosun.Model.ChContentParam GetCHInfo(int ID, string DTable)
        {

            Foosun.Model.ChContentParam cci = new Foosun.Model.ChContentParam();
            IDataReader rd = CommonData.DalPublish.GetCHDetail(ID, DTable);
            if (rd.Read())
            {
                cci.ID = Convert.ToInt32(rd["ID"]);
                cci.OrderID = Convert.ToByte(rd["OrderID"]);
                cci.Title = Convert.ToString(rd["Title"]);
                if (rd["TitleColor"] == DBNull.Value) { cci.TitleColor = ""; } else { cci.TitleColor = Convert.ToString(rd["TitleColor"]); }
                if (rd["TitleITF"] == DBNull.Value) { cci.TitleITF = 0; } else { cci.TitleITF = Convert.ToByte(rd["TitleITF"]); }
                if (rd["TitleBTF"] == DBNull.Value) { cci.TitleBTF = 0; } else { cci.TitleBTF = Convert.ToByte(rd["TitleBTF"]); }
                if (rd["PicURL"] == DBNull.Value) { cci.PicURL = ""; } else { cci.PicURL = Convert.ToString(rd["PicURL"]); }
                cci.ClassID = Convert.ToInt32(rd["ClassID"].ToString());
                if (rd["SpecialID"] == DBNull.Value) { cci.SpecialID = ""; } else { cci.SpecialID = Convert.ToString(rd["SpecialID"]); }
                if (rd["Author"] == DBNull.Value) { cci.Author = ""; } else { cci.Author = Convert.ToString(rd["Author"]); }
                if (rd["Souce"] == DBNull.Value) { cci.Souce = ""; } else { cci.Souce = Convert.ToString(rd["Souce"]); }
                if (rd["Tags"] == DBNull.Value) { cci.Tags = ""; } else { cci.Tags = Convert.ToString(rd["Tags"]); }
                if (rd["ContentProperty"] == DBNull.Value) { cci.ContentProperty = "0|0|0|0|0"; } else { cci.ContentProperty = Convert.ToString(rd["ContentProperty"]); }
                if (rd["Templet"] == DBNull.Value) { cci.Templet = ""; } else { cci.Templet = Convert.ToString(rd["Templet"]); }
                if (rd["Content"] == DBNull.Value) { cci.Content = ""; } else { cci.Content = Convert.ToString(rd["Content"]); }
                if (rd["Metakeywords"] == DBNull.Value) { cci.Metakeywords = ""; } else { cci.Metakeywords = Convert.ToString(rd["Metakeywords"]); }
                if (rd["Metadesc"] == DBNull.Value) { cci.Metadesc = ""; } else { cci.Metadesc = Convert.ToString(rd["Metadesc"]); }
                if (rd["naviContent"] == DBNull.Value) { cci.naviContent = ""; } else { cci.naviContent = Convert.ToString(rd["naviContent"]); }
                if (rd["Click"] == DBNull.Value) { cci.Click = 0; } else { cci.Click = Convert.ToInt32(rd["Click"].ToString()); }
                if (rd["CreatTime"] == DBNull.Value) { cci.CreatTime = DateTime.Now; } else { cci.CreatTime = Convert.ToDateTime(rd["CreatTime"].ToString()); }
                if (rd["SavePath"] == DBNull.Value) { cci.SavePath = ""; } else { cci.SavePath = Convert.ToString(rd["SavePath"]); }
                if (rd["FileName"] == DBNull.Value) { cci.FileName = ""; } else { cci.FileName = Convert.ToString(rd["FileName"]); }
                if (rd["isDelPoint"] == DBNull.Value) { cci.isDelPoint = 0; } else { cci.isDelPoint = Convert.ToInt32(rd["isDelPoint"].ToString()); }
                if (rd["Gpoint"] == DBNull.Value) { cci.Gpoint = 0; } else { cci.Gpoint = Convert.ToInt32(rd["Gpoint"].ToString()); }
                if (rd["iPoint"] == DBNull.Value) { cci.iPoint = 0; } else { cci.iPoint = Convert.ToInt32(rd["iPoint"].ToString()); }
                if (rd["GroupNumber"] == DBNull.Value) { cci.GroupNumber = ""; } else { cci.GroupNumber = Convert.ToString(rd["GroupNumber"]); }
                if (rd["isLock"] == DBNull.Value) { cci.isLock = 0; } else { cci.isLock = Convert.ToByte(rd["isLock"]); }
                if (rd["ChID"] == DBNull.Value) { cci.ChID = 0; } else { cci.ChID = Convert.ToInt32(rd["ChID"].ToString()); }
                if (rd["Editor"] == DBNull.Value) { cci.Editor = ""; } else { cci.Editor = Convert.ToString(rd["Editor"]); }
                if (rd["isHtml"] == DBNull.Value) { cci.isHtml = 0; } else { cci.isHtml = Convert.ToByte(rd["isHtml"]); }
                if (rd["isConstr"] == DBNull.Value) { cci.isConstr = 0; } else { cci.isConstr = Convert.ToByte(rd["isConstr"]); }
            }
            rd.Close();
            return cci;
        }

        /// <summary>
        /// 频道信息地址
        /// </summary>
        public string getCHInfoURL(int ChID, int isDelPoint, int id, string ClassSavePath, string SavePath, string FileName)
        {
            string urls = string.Empty;
            int ishtml = int.Parse(Foosun.Common.Public.readCHparamConfig("isHTML", ChID));
            string Domain = Foosun.Common.Public.readCHparamConfig("bdomain", ChID);
            string linkType = Foosun.Common.Public.readparamConfig("linkTypeConfig");
            string htmldir = Foosun.Common.Public.readCHparamConfig("htmldir", ChID);
            string dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (dirdumm.Trim() != string.Empty){ dirdumm = "/" + dirdumm;}
            if (ishtml != 0 && isDelPoint == 0)
            {
                string flg = string.Empty;
                if (Domain != string.Empty)
                {
                    if (linkType == "1")
                    {
                        if (Domain.IndexOf("http://") > -1) { flg = Domain ; }
                        else { flg = "http://" + Domain; }
                        urls = flg + "/" + ClassSavePath + "/" + SavePath + "/" + FileName;
                    }
                    else
                    {
                        urls = "/" + ClassSavePath + "/" + SavePath + "/" + FileName;
                    }
                }
                else
                {
                    urls = "/" + htmldir + "/" + ClassSavePath + "/" + SavePath + "/" + FileName;
                    urls = urls.Replace("//", "/");
                    urls = CommonData.SiteDomain + urls;
                }
            }
            else
            {
                urls = CommonData.SiteDomain + "/Content.aspx?Id=" + id.ToString() + "&ChID=" + ChID.ToString() + "";
            }
            return urls.ToLower().Replace("{@dirhtml}", Foosun.Config.UIConfig.dirHtml);
        }
        /// <summary>
        /// 频道栏目地址
        /// </summary>
        public string getCHClassURL(int ChID, int isDelPoint, int id, string ClassSavePath, string FileName)
        {
            string urls = string.Empty;
            int ishtml = int.Parse(Foosun.Common.Public.readCHparamConfig("isHTML", ChID));
            string Domain = Foosun.Common.Public.readCHparamConfig("bdomain", ChID);
            string linkType = Foosun.Common.Public.readparamConfig("linkTypeConfig");
            string htmldir = Foosun.Common.Public.readCHparamConfig("htmldir", ChID);
            string dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (dirdumm.Trim() != string.Empty)
            {
                dirdumm = "/" + dirdumm;
            }
            if (ishtml != 0 && isDelPoint == 0)
            {
                string flg = string.Empty;
                if (Domain != string.Empty)
                {
                    if (linkType == "1")
                    {
                        if (Domain.IndexOf("http://") > -1) { flg = Domain; }
                        else { flg = "http://" + Domain; }
                        urls = flg + "/" + ClassSavePath + "/" + FileName;
                    }
                    else
                    {
                        urls = "/" + ClassSavePath + "/" + FileName;
                    }
                }
                else
                {
                    urls = "/" + htmldir + "/" + ClassSavePath + "/" + FileName;
                    urls = urls.Replace("//", "/");
                    urls = CommonData.SiteDomain + urls;
                }
            }
            else
            {
                urls = CommonData.SiteDomain + "/list.aspx?Id=" + id.ToString() + "&ChID=" + ChID.ToString() + "";
            }
            return urls.ToLower().Replace("{@dirhtml}", Foosun.Config.UIConfig.dirHtml);
        }

        /// <summary>
        /// 频道专题地址
        /// </summary>
        public string getCHSpecialURL(int ChID, int isDelPoint, int id, string SpecialSavePath, string FileName)
        {
            string urls = string.Empty;
            int ishtml = int.Parse(Foosun.Common.Public.readCHparamConfig("isHTML", ChID));
            string Domain = Foosun.Common.Public.readCHparamConfig("bdomain", ChID);
            string linkType = Foosun.Common.Public.readparamConfig("linkTypeConfig");
            string htmldir = Foosun.Common.Public.readCHparamConfig("htmldir", ChID);
            string dirdumm = Foosun.Config.UIConfig.dirDumm;
            if (dirdumm.Trim() != string.Empty)
            {
                dirdumm = "/" + dirdumm;
            }
            if (ishtml != 0)
            {
                string flg = string.Empty;
                if (Domain != string.Empty)
                {
                    if (linkType == "1")
                    {
                        if (Domain.IndexOf("http://") > -1) { flg = Domain; }
                        else { flg = "http://" + Domain; }
                        urls = flg + "/" + SpecialSavePath + "/" + FileName;
                    }
                    else
                    {
                        urls = "/" + SpecialSavePath + "/" + FileName;
                    }
                }
                else
                {
                    urls = "/" + htmldir + "/" + SpecialSavePath + "/" + FileName;
                    urls = urls.Replace("//", "/");
                    urls = CommonData.SiteDomain + urls;
                }
            }
            else
            {
                urls = CommonData.SiteDomain + "/special.aspx?Id=" + id.ToString() + "&ChID=" + ChID.ToString() + "";
            }
            return urls.ToLower().Replace("{@dirhtml}", Foosun.Config.UIConfig.dirHtml);
        }
    }
}
