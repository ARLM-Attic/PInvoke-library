using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using Foosun.Config;
using Foosun.Model;

namespace Foosun.Publish
{
    public partial class LabelMass
    {
        /// <summary>
        /// 取得新闻访问地址
        /// </summary>
        /// <param name="SavePath">新闻保存路径</param>
        /// <param name="FileName">新闻文件名</param>
        /// <param name="SaveClassframe">栏目存储新闻的规则</param>
        /// <param name="FileEXName">新闻文件名后缀</param>   
        /// <returns>返回新闻访问地址</returns>
        protected string getNewsURL(string isDelPoint,string NewsID,string SavePath, string SaveClassframe, string FileName, string FileEXName)
        {
            string str_temppath = "";
            if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
            {
                str_temppath = "/content.aspx?id=" + NewsID;
            }
            else
            {
                if (isDelPoint != "0")
                {
                    str_temppath = "/content.aspx?id=" + NewsID;
                }
                else
                {
                    str_temppath = "/" + SaveClassframe + "/" + SavePath + "/" + FileName + FileEXName;
                }
            }
            string SiteDomain = getNewsDomain(NewsID);
            str_temppath = SiteDomain + str_temppath.Replace("//", "/").Replace("//", "/");
            return str_temppath;
        }

        /// <summary>
        /// 传入新闻编号取得新闻地址
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns></returns>
        protected string getNewsURL1(DataRow r)
        {
            PubClassInfo ci = CommonData.GetClassById(r["ClassID"].ToString());
            return getNewsURL(r["isDelPoint"].ToString(), r["NewsID"].ToString(), r["SavePath"].ToString(), ci.SavePath + "/" + ci.SaveClassframe, r["FileName"].ToString(), r["FileExName"].ToString());
        }
        protected string getNewsURL1(string ClassID, string isDelPoint, string NewsID, string SavePath, string FileName, string FileExName)
        {
            PubClassInfo ci = CommonData.GetClassById(ClassID);
            return getNewsURL(isDelPoint, NewsID, SavePath, ci.SavePath + "/" + ci.SaveClassframe, FileName, FileExName);
        }
        /// <summary>
        /// 得到图片路径
        /// </summary>
        /// <returns></returns>
        protected string RelpacePicPath(string PicPath)
        {
            return PicPath.ToLower().Replace("{@dirfile}", Foosun.Config.UIConfig.dirFile);
        }

        /// <summary>
        /// 取得标题样式
        /// </summary>
        /// <param name="dr">数据行</param>
        /// <param name="StyleTf">是否显示标题样式</param>
        /// <param name="TitleNum">标题字数</param>
        /// <returns></returns>
        protected string getNewstitleStyle(DataRow dr, int StyleTf, string TitleNum)
        {
            int i_TitleNum = 0;
            if (TitleNum != string.Empty && TitleNum != null)
            {
                i_TitleNum = int.Parse(TitleNum);
            }
            string NewsTitle = dr["NewsTitle"].ToString();
            if (i_TitleNum != 0)
                NewsTitle = Foosun.Common.Input.GetSubString(NewsTitle, i_TitleNum);

            if (StyleTf == 1)
            {
                string TitleColor = dr["TitleColor"].ToString();
                string TitleITF = dr["TitleITF"].ToString();
                string TitleBTF = dr["TitleBTF"].ToString();
                if (TitleColor != "" && TitleColor != null)
                    NewsTitle = "<span style=\"color:" + TitleColor + ";\">" + NewsTitle + "</span>";
                if (TitleITF.Equals("1"))
                    NewsTitle = "<em>" + NewsTitle + "</em>";
                if (TitleBTF.Equals("1"))
                    NewsTitle = "<strong>" + NewsTitle + "</strong>";
            }
            return NewsTitle;
        }

        /// <summary>
        /// 得到标题样式
        /// </summary>
        /// <param name="Title">标题</param>
        /// <param name="Color">颜色</param>
        /// <param name="Istr">斜体</param>
        /// <param name="Bstr">粗体</param>
        /// <returns></returns>
        protected string getStyle(string Title, string sColor, int Istr, int Bstr)
        {
            string tmpstr = Title;
            if (sColor.Trim() != string.Empty)
            {
                tmpstr = "<span style=\"color:" + sColor + ";\">" + tmpstr + "</span>";
            }
            if (Istr == 1)
            {
                tmpstr = "<em>" + tmpstr + "</em>";
            }
            if (Bstr == 1)
            {
                tmpstr = "<strong>" + tmpstr + "</strong>";
            }
            return tmpstr;
        }

        /// <summary>
        /// 取得栏目访问地址
        /// </summary>
        /// <param name="SavePath">栏目保存路径</param>
        /// <param name="ClassSaveRule">栏目保存规则</param>
        /// <returns>返回访问地址</returns>
        protected string getClassURL(string Domain,int isDelPoint,string ClassID,string SavePath, string SaveClassframe, string ClassSaveRule)
        {
            string tmstr = "";
            if (Domain.Length > 5)
            {
                if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
                {
                    tmstr = "/list.aspx?id=" + ClassID;
                    return CommonData.SiteDomain + tmstr.Replace("//", "/").Replace("//", "/");
                }
                else
                {
                    if (isDelPoint != 0)
                    {
                        tmstr = "/list.aspx?id=" + ClassID;
                        return CommonData.SiteDomain + tmstr.Replace("//", "/").Replace("//", "/");
                    }
                    else
                    {
                        tmstr = "/" + ClassSaveRule;
                        return Domain + tmstr.Replace("//", "/").Replace("//", "/");
                    }
                }
            }
            else
            {
                if (Foosun.Common.Public.readparamConfig("ReviewType") == "1")
                {
                    tmstr = "/list.aspx?id=" + ClassID;
                }
                else
                {
                    if (isDelPoint != 0)
                    {
                        tmstr = "/list.aspx?id=" + ClassID;
                    }
                    else
                    {
                        //bug修改 周峻平 2008-6-5
                        if (SavePath != null && !SavePath.Equals(""))
                            tmstr = "/" + SavePath;
                        if (SaveClassframe != null && !SaveClassframe.Equals(""))
                            tmstr += "/" + SaveClassframe;
                        if (ClassSaveRule != null && !ClassSaveRule.Equals(""))
                            tmstr += "/" + ClassSaveRule;
                    }
                }
                return CommonData.SiteDomain + tmstr.Replace("//", "/").Replace("//", "/");
            }
        }

        /// <summary>
        /// 取得专题访问地址
        /// </summary>
        /// <param name="SavePath">专题保存路径</param>
        /// <param name="SaveDirPath">专题保存目录</param>
        /// <param name="FileName">专题文件名</param>
        /// <param name="FileEXName">专题文件名后缀</param>
        /// <returns>返回访问地址</returns>
        protected string getSpeacilURL(string isDelPoint,string SpecialID,string SavePath, string SaveDirPath, string FileName, string FileEXName)
        {
            string tmstr = "";
            if (Foosun.Common.Public.readparamConfig("ReviewType") == "1" || isDelPoint!="0")
            {
                tmstr = "/special.aspx?id=" + SpecialID;
            }
            else
            {
                tmstr = "/" + SavePath + "/" + SaveDirPath + "/" + FileName + FileEXName;
            }
            return CommonData.SiteDomain + tmstr.Replace("//", "/").Replace("//", "/");
        }

        /// <summary>
        /// 取得评论表单
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回表单字符串</returns>
        protected string getCommForm(string NewsID, int NewsTF,int ChID)
        {
            if (NewsTF == 1)
            {
                string str_CommForm = "<a name=\"commList\"></a><div id=\"Div_CommentForm\"><img src=\"" + CommonData.SiteDomain +  "/sysimages/folder/loading.gif\" border=\"0\" />评论表单加载中...</div>" + newLine;
                str_CommForm += "<script language=\"javascript\" type=\"text/javascript\">" + newLine;
                str_CommForm += "function GetAddCommentForm()" + newLine;
                str_CommForm += "{" + newLine;
                str_CommForm += "   var Action='id=" + NewsID + "&ChID=" + ChID + "&CommentType=GetAddCommentForm';";
                str_CommForm += "   var options={ " + newLine;
                str_CommForm += "                  method:'get', " + newLine;
                str_CommForm += "                  parameters:Action, " + newLine;
                str_CommForm += "                  onComplete:function(transport) " + newLine;
                str_CommForm += "                  { " + newLine;
                str_CommForm += "                      var returnvalue=transport.responseText; " + newLine;
                str_CommForm += "                      var arrreturnvalue=returnvalue.split('$$$'); " + newLine;
                str_CommForm += "                      if (arrreturnvalue[0]==\"ERR\") " + newLine;
                str_CommForm += "                          document.getElementById(\"Div_CommentForm\").innerHTML='加载评论表单失败!'; " + newLine;
                str_CommForm += "                      else " + newLine;
                str_CommForm += "                          document.getElementById(\"Div_CommentForm\").innerHTML=arrreturnvalue[1]; " + newLine;
                str_CommForm += "                  } " + newLine;
                str_CommForm += "               }; " + newLine;
                str_CommForm += "   new  Ajax.Request('" + CommonData.SiteDomain + "/comment.aspx?no-cache='+Math.random(),options);" + newLine;
                str_CommForm += "}" + newLine;
                str_CommForm += "GetAddCommentForm();" + newLine;

                str_CommForm += "function CommandSubmit(obj)" + newLine;
                str_CommForm += "{" + newLine;
                str_CommForm += "    if(obj.UserNum.value==\"\")" + newLine;
                str_CommForm += "    {" + newLine;
                str_CommForm += "        alert('帐号不能为空');" + newLine;
                str_CommForm += "        return false;" + newLine;
                str_CommForm += "    }" + newLine;
                str_CommForm += "    if(obj.Content.value==\"\")" + newLine;
                str_CommForm += "    {" + newLine;
                str_CommForm += "        alert('评论内容不能为空');" + newLine;
                str_CommForm += "        return false;" + newLine;
                str_CommForm += "    }" + newLine;
                str_CommForm += "    var r = obj.commtype; " + newLine;
                str_CommForm += "    var commtypevalue = '2'; " + newLine;
                str_CommForm += "    for(var i=0;i<r.length;i++) " + newLine;
                str_CommForm += "    {" + newLine;
                str_CommForm += "        if(r[i].checked)" + newLine;
                str_CommForm += "           commtypevalue=r[i].value;" + newLine;
                str_CommForm += "    }" + newLine;
                str_CommForm += "    var Action='CommentType=AddComment&UserNum='+escape(obj.UserNum.value)+'&UserPwd='+escape(obj.UserPwd.value)+'&commtype='+escape(commtypevalue)+'&Content='+escape(obj.Content.value)+'&IsQID='+escape(obj.IsQID.value)+'&id=" + NewsID + "';" + newLine;
                str_CommForm += "    var options={ " + newLine;
                str_CommForm += "                    method:'get', " + newLine;
                str_CommForm += "                    parameters:Action, " + newLine;
                str_CommForm += "                    onComplete:function(transport) " + newLine;
                str_CommForm += "                    { " + newLine;
                str_CommForm += "                        var returnvalue=transport.responseText; " + newLine;
                str_CommForm += "                        var arrreturnvalue=returnvalue.split('$$$'); " + newLine;
                str_CommForm += "                        if (arrreturnvalue[0]==\"ERR\") " + newLine;
                str_CommForm += "                        { " + newLine;
                str_CommForm += "                           alert(arrreturnvalue[1]); " + newLine;
                str_CommForm += "                           GetAddCommentForm(); " + newLine;
                str_CommForm += "                        } " + newLine;
                str_CommForm += "                        else " + newLine;
                str_CommForm += "                        { " + newLine;
                str_CommForm += "                           alert('发表评论成功!'); " + newLine;
                str_CommForm += "                           document.getElementById(\"Div_CommentList\").innerHTML=arrreturnvalue[1]; " + newLine;
                str_CommForm += "                           GetAddCommentForm(); " + newLine;
                str_CommForm += "                        } " + newLine;
                str_CommForm += "                    } " + newLine;
                str_CommForm += "                 }; " + newLine;
                str_CommForm += "     new  Ajax.Request('" + Foosun.Publish.CommonData.SiteDomain +"/comment.aspx?no-cache='+Math.random(),options); " + newLine;
                str_CommForm += "} " + newLine;

                str_CommForm += "function CommentLoginOut()" + newLine;
                str_CommForm += "{" + newLine;
                str_CommForm += "    var Action='CommentType=LoginOut';" + newLine;
                str_CommForm += "    var options={ " + newLine;
                str_CommForm += "                  method:'get', " + newLine;
                str_CommForm += "                  parameters:Action, " + newLine;
                str_CommForm += "                  onComplete:function(transport) " + newLine;
                str_CommForm += "                  { " + newLine;
                str_CommForm += "                      var returnvalue=transport.responseText; " + newLine;
                str_CommForm += "                      var arrreturnvalue=returnvalue.split('$$$'); " + newLine;
                str_CommForm += "                      if (arrreturnvalue[0]==\"ERR\") " + newLine;
                str_CommForm += "                          alert('未知错误!'); " + newLine;
                str_CommForm += "                      else " + newLine;
                str_CommForm += "                          document.getElementById(\"Div_CommentForm\").innerHTML=arrreturnvalue[1]; " + newLine;
                str_CommForm += "                   } " + newLine;
                str_CommForm += "                 }; " + newLine;
                str_CommForm += "      new  Ajax.Request('" + Foosun.Publish.CommonData.SiteDomain + "/comment.aspx?no-cache='+Math.random(),options);" + newLine;
                str_CommForm += "}" + newLine;                
                
                str_CommForm += "</script>" + newLine;
                return str_CommForm;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 取得总评论数/今日
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <param name="td">1今日</param>
        /// <returns>返回总评论数</returns>
        protected string getCommCount(string NewsID, int NewsTF, int td,int ChID)
        {
            string CommStr = "";
            if (NewsTF == 1)
            {
                string radnum = Foosun.Common.Rand.Number(3);
                CommStr += "<a href=\"" + CommonData.SiteDomain + "/Comment.aspx?CommentType=getlist&id=" + NewsID + "&ChID=" + ChID + "\"><span id=\"gCount" + NewsID + radnum + td + "\"></span></a>" + newLine;
                CommStr += "<script language=\"javascript\" type=\"text/javascript\">";
                CommStr += "pubajax('" + CommonData.SiteDomain + "/comment.aspx','id=" + NewsID + "&commCount=1&ChID=" + ChID + "&Today=" + td + "','gCount" + NewsID + radnum + td + "');";
                CommStr += "</script>";
            }
            else
            {
                CommStr = CommonData.DalPublish.GetCommCount(NewsID, td, ChID).ToString();
            }
            return CommStr;
        }

        /// <summary>
        /// 得到投票。
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="NewsTF"></param>
        /// <returns></returns>
        public string getVoteItem(string NewsID, int NewsTF)
        {
            string votelist = "";
            if (NewsTF == 1)
            {
                string radnum = Foosun.Common.Rand.Number(5);
                votelist += "<div id=\"vote" + NewsID + radnum + "\">投票加载中...</div>" + newLine;
                votelist += "<script language=\"javascript\" type=\"text/javascript\">";
                votelist += "pubajax('" + CommonData.SiteDomain + "/vote.aspx','NewsID=" + NewsID + "','vote" + NewsID + radnum + "');";
                votelist += "</script>";
            }
            else
            {
                votelist = "";
            }
            return votelist;
        }

        /// <summary>
        /// 得到评论列表
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回评论列表</returns>
        protected string getLastComm(string NewsID, int NewsTF,int ChID)
        {
            string str_LastCommList = "";
            string RamStr = Foosun.Common.Rand.Number(5);
            if (NewsTF == 1)
            {
                str_LastCommList += "<a name=\"commList\"></a><div id=\"Div_CommentList\">正在加载评论列表...</div>" + newLine;
                str_LastCommList += "<script language=\"javascript\" type=\"text/javascript\">" + newLine;
                str_LastCommList += "function GetCommentList(page)" + newLine;
                str_LastCommList += "{" + newLine;
                str_LastCommList += "   var Action='id=" + NewsID + "&ChID=" + ChID + "&CommentType=GetCommentList&page='+page;";
                str_LastCommList += "   var options={ " + newLine;
                str_LastCommList += "                  method:'get', " + newLine;
                str_LastCommList += "                  parameters:Action, " + newLine;
                str_LastCommList += "                  onComplete:function(transport) " + newLine;
                str_LastCommList += "                  { " + newLine;
                str_LastCommList += "                      var returnvalue=transport.responseText; " + newLine;
                str_LastCommList += "                      if (returnvalue.indexOf(\"??\")>-1) " + newLine;
                str_LastCommList += "                          document.getElementById(\"Div_CommentList\").innerHTML='加载评论列表失败'; " + newLine;
                str_LastCommList += "                      else " + newLine;
                str_LastCommList += "                          document.getElementById(\"Div_CommentList\").innerHTML=returnvalue; " + newLine;
                str_LastCommList += "                  } " + newLine;
                str_LastCommList += "               }; " + newLine;
                str_LastCommList += "   new  Ajax.Request('" + CommonData.SiteDomain + "/comment.aspx?no-cache='+Math.random(),options);" + newLine;
                str_LastCommList += "}" + newLine;
                str_LastCommList += "GetCommentList(1);" + newLine;
                str_LastCommList += "</script>" + newLine;
            }
            return str_LastCommList;
        }

        /// <summary>
        /// 取得总讨论数
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回总讨论数</returns>
        protected string getGroupCount(string NewsID)
        {
            return "";
        }

        /// <summary>
        /// 发送到好友
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回发送到好友链接地址</returns>
        protected string getSendInfo(string NewsID, int ChID)
        {
            return CommonData.SiteDomain + "/SendMail.aspx?ChID=" + ChID + "&id=" + NewsID;
        }

        /// <summary>
        /// 收藏连接地址
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回收藏连接地址</returns>
        protected string getCollection(string NewsID, int ChID)
        {
            return CommonData.SiteDomain + "/" + Foosun.Config.UIConfig.dirUser + "/index.aspx?urls=info/collection.aspx?ChID=" + ChID + "|Add|" + NewsID;
        }


        /// <summary>
        /// 取得上一篇新闻
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <param name="NewsID">0为下一篇，1为上一篇</param>
        /// <returns>返回上一篇新闻链接地址</returns>
        protected string getPrePage(string id, string DataLib, string ClassID, int Num,int ChID,int isTitle)
        {
            string str = "";
            if (ChID == 0)
            {
                IDataReader rd = CommonData.DalPublish.GetPrePage(int.Parse(id), DataLib, Num, ClassID, ChID);
                if (rd.Read())
                {
                    if (isTitle == 0)
                    {
                        str = getNewsURL(rd["isDelPoint"].ToString(), rd["NewsID"].ToString(), rd["savePath"].ToString(), rd["savePath1"].ToString() + "/" + rd["saveClassFrame"].ToString(), rd["FileName"].ToString(), rd["FileEXName"].ToString());
                    }
                    else
                    {
                        str = rd["NewsTitle"].ToString();  
                    }
                }
                else
                {
                    if(isTitle==0)
                        str = "javascript:;";
                    else
                        str = "没有了";
                }
                rd.Close();
            }
            else
            {
                IDataReader rd1 = CommonData.DalPublish.GetPrePage(int.Parse(id), DataLib, Num, ClassID, ChID);
                if (rd1.Read())
                {
                    if (isTitle == 0)
                    {
                        str = getCHInfoURL(ChID, int.Parse(rd1["isDelPoint"].ToString()), int.Parse(rd1["id"].ToString()), rd1["savePath1"].ToString(), rd1["savePath"].ToString(), rd1["FileName"].ToString());
                    }
                    else
                    {
                        str = rd1["Title"].ToString();
                    }
                }
                else
                {
                    if (isTitle == 0)
                        str = "javascript:;";
                    else
                        str = "没有了";
                }
                rd1.Close();
            }
            return str;
        }

        /// <summary>
        /// 得到DIG数量
        /// </summary>
        /// <param name="NewsID"></para>
        /// <returns></returns>
        protected string getTopNum(string NewsID, int NewsTF, string TopNum,string filename)
        {
            string CommStr = "";
            if (NewsTF == 1)
            {
                CommStr += "<span id=\"n_" + NewsID + "\"></span>";
                CommStr += "<script language=\"javascript\" type=\"text/javascript\">";
                CommStr += "pubajax('" + CommonData.SiteDomain + "/digg.aspx','newsid=" + NewsID + "&spanid=n_" + NewsID + "&getNum=0','n_" + NewsID + "');";
                CommStr += "</script>";
            }
            else
            {
                CommStr += "<span id=\"l_" + NewsID + filename + "\"></span>";
                CommStr += "<script language=\"javascript\" type=\"text/javascript\">";
                CommStr += "pubajax('" + CommonData.SiteDomain + "/digg.aspx','newsid=" + NewsID + "&spanid=l_" + NewsID + filename + "&getNum=0','l_" + NewsID + filename + "');";
                CommStr += "</script>";
            }
            return CommStr;
        }

        /// <summary>
        /// 取得Digg(连接地址)
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回取得Digg(连接地址)</returns>
        protected string getTopURL(string NewsID,int NewsTF,string filename)
        {
            string TopURL="";
            if (NewsTF == 1)
            {
                TopURL = "javascript:getTopNum('" + CommonData.SiteDomain + "/digg.aspx','" + NewsID + "',1,'n_" + NewsID + "');";
            }
            else
            {
                TopURL = "javascript:getTopNum('" + CommonData.SiteDomain + "/digg.aspx','" + NewsID + "',1,'l_" + NewsID + filename + "');";
            }
            return TopURL;
        }

        /// <summary>
        /// 取得附件地址
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回附件地址</returns>
        protected string getNewsFiles(string NewsID, int NewsTF)
        {
            string str = "";
            IDataReader rd = CommonData.DalPublish.GetNewsFiles(NewsID);
            while (rd.Read())
            {
                str += "<a href=\"" + CommonData.SiteDomain + "/down-" + rd["id"].ToString() + ".aspx\">" + rd["URLName"].ToString() + "</a>";
            }
            rd.Close();
            return str;
        }

        /// <summary>
        /// 取得视频地址
        /// </summary>
        /// <param name="NewsID">新闻编号</param>
        /// <returns>返回视频地址</returns>
        protected string getNewsvURL(string NewsID, int NewsTF, string vURL,string heightstr,string widthstr)
        {
            string str = "";
            int dotposion = vURL.LastIndexOf(".");
            string getFileEXname = "";
            int vtype = 0;
            if (dotposion > -1)
            {
                getFileEXname = vURL.Substring(dotposion);
            }
            switch (getFileEXname.ToLower())
            {
                case ".asf":
                    break;
                case ".flv":
                    vtype = 2;
                    break;
                case ".rm":
                    vtype = 1;
                    break;
                case ".rmvb":
                    vtype = 1;
                    break;
                case ".mp3":
                    vtype = 1;
                    break;
                case ".wma":
                    break;
                case ".avi":
                    break;
                case ".mpg":
                    break;
                case ".wmv":
                    break;
                case ".swf":
                    vtype = 3;
                    break;
                default:
                    break;
            }
            vURL = RelpacePicPath(vURL);

            if (NewsTF == 0)
            {
                str = CommonData.SiteDomain + "/vplay.html?vtype=" + vtype + "&NewsID=" + NewsID + "&height=" + heightstr + "&width=" + widthstr + "";//流媒体播放
            }
            else
            {
                if (vtype == 0)
                {
                    str = "<object id=\"nstv\" classid=\"CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6\" width=\""+heightstr+"\" height=\""+widthstr+"\" codebase=\"http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#\" Version=\"5,1,52,701standby=Loading Microsoft? Windows Media? Player components...\" type=\"application/x-oleobject\">" + newLine;
                    str += "<param name=\"URL\" value=\"" + vURL + "\">" + newLine;
                    str += "<PARAM NAME=\"UIMode\" value=\"full\">" + newLine;
                    str += "<PARAM NAME=\"AutoStart\" value=\"true\">" + newLine;
                    str += "<PARAM NAME=\"Enabled\" value=\"true\">" + newLine;
                    str += "<PARAM NAME=\"enableContextMenu\" value=\"false\">" + newLine;
                    str += "<param name=\"WindowlessVideo\" value=\"true\">" + newLine;
                    str += "</object>" + newLine;
                }
                else if (vtype == 1)
                {
                    str = "<object id=\"player\" name=\"player\" classid=\"clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA\" width=\"" + widthstr + "\" height=\"" + heightstr + "\">" + newLine;
                    str += "<param name=_ExtentX value=18415>" + newLine;
                    str += "<param name=_ExtentY value=9102>" + newLine;
                    str += "<param name=AUTOSTART value=-1>" + newLine;
                    str += "<param name=SHUFFLE value=0>" + newLine;
                    str += "<param name=PREFETCH value=0>" + newLine;
                    str += "<param name=NOLABELS value=-1>" + newLine;
                    str += "<param name=SRC value=" + vURL + ">" + newLine;
                    str += "<param name=CONTROLS value=Imagewindow>" + newLine;
                    str += "<param name=CONSOLE value=clip1>" + newLine;
                    str += "<param name=LOOP value=0>" + newLine;
                    str += "<param name=NUMLOOP value=0>" + newLine;
                    str += "<param name=CENTER value=0>" + newLine;
                    str += "<param name=MAINTAINASPECT value=0>" + newLine;
                    str += "<param name=BACKGROUNDCOLOR value=#000000>" + newLine;
                    str += "</object><br>" + newLine;
                    str += "<object ID=RP2 CLASSID=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA WIDTH=400 HEIGHT=50>" + newLine;
                    str += "<param name=_ExtentX value=18415>" + newLine;
                    str += "<param name=_ExtentY value=1005>" + newLine;
                    str += "<param name=AUTOSTART value=-1>" + newLine;
                    str += "<param name=SHUFFLE value=0>" + newLine;
                    str += "<param name=PREFETCH value=0>" + newLine;
                    str += "<param name=NOLABELS value=-1>" + newLine;
                    str += "<param name=SRC value=" + vURL + ">" + newLine;
                    str += "<PARAM NAME=CONTROLS VALUE=ControlPanel,StatusBar>" + newLine;
                    str += "<param name=CONSOLE value=clip1>" + newLine;
                    str += "<param name=LOOP value=0>" + newLine;
                    str += "<param name=NUMLOOP value=0>" + newLine;
                    str += "<param name=CENTER value=0>" + newLine;
                    str += "<param name=MAINTAINASPECT value=0>" + newLine;
                    str += "<param name=BACKGROUNDCOLOR value=#000000>" + newLine;
                    str += "</object>" + newLine;
                }
                else if (vtype == 2)
                {
                    str = "<embed src=\"" + CommonData.SiteDomain +  "/FlvPlayer.swf?id=" + vURL + "\" type=\"application/x-shockwave-flash\" wmode=\"transparent\" quality=\"high\" height=\"" + heightstr + "\" width=\"" + widthstr + "\" autostart=\"true\"></embed>" + newLine;
                }
                else if (vtype == 3)
                {
                    str = "<embed src=\"" + vURL + "?bgcolor=000000\" quality=\"high\" pluginspage=\"http://www.adobe.com/support/documentation/zh-CN/flashplayer/help/settings_manager04a.html\" type=\"application/x-shockwave-flash\" width=\"" + widthstr + "\" height=\"" + heightstr + "\" id=\"cfplay\"></embed>";
                }
            }
            return str;
        }

        /// <summary>
        /// 调用页面标题
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Str"></param>
        /// <returns></returns>
        protected string getPageTitle(string id, string Str, int ChID)
        {
            if (Str == "News" || Str == "Class" || Str == "Special")
            {
                return CommonData.DalPublish.GetPageTitle(id, Str);
            }
            else
            {
                return CommonData.DalPublish.GetCHPageTitle(int.Parse(id), Str, ChID);
            }
        }

        /// <summary>
        /// 得到META类
        /// </summary>
        /// <param name="id"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        protected string getMetaContent(string id, string Str, int num)
        {
            return CommonData.DalPublish.GetMetaContent(id, Str, num);
        }

        /// <summary>
        /// 得到站点名称
        /// </summary>
        /// <returns></returns>
        protected string getSiteName()
        {
            string retval = "";
            IDataReader rd = CommonData.DalPublish.GetSysParam();
            if (rd.Read())
            {
                if (rd["SiteName"] != DBNull.Value)
                    retval = rd["SiteName"].ToString();
            }
            rd.Close();
            return retval;
        }

        /// <summary>
        /// 得到分页标题
        /// </summary>
        /// <param name="Content"></param>
        /// <returns></returns>
        protected string getPageTitleStyle(string NewsID,string FileName, string FileEXName, string Content,int PageNum,int isPop,int ChID)
        {
            string PageStr = "";
            string[] PageARR = null;
            string ReadType = Foosun.Common.Public.readparamConfig("ReviewType");
            string getDyUrl = string.Empty;
            string Pmstr = string.Empty;
            string CHSTR = string.Empty;
            if (ChID != 0)
            {
                CHSTR = "&ChID=" + ChID.ToString() + "";
            }
            if (Content.IndexOf("###") > -1)
            {
                switch (PageNum)
                {
                    case 0:
                        PageARR = Content.Split(new string[] { "###" }, StringSplitOptions.None);
                        PageStr += "<form name=\"getPageform1\" id=\"getPageform1\"><select name=\"PageSelectOption\"  id=\"PageSelectOption\"  onChange=\"javascript:window.location=this.options[this.selectedIndex].value;\">" + newLine;
                        for (int i = 0; i < PageARR.Length; i++)
                        {
                            if (PageARR[i].Trim() == string.Empty)
                            {
                                break;
                            }
                            if (ReadType == "1" || isPop != 0)
                            {
                                if (i < 1)
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR;
                                }
                                else
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR + "&Page=" + (i + 1).ToString();
                                }
                                getDyUrl = Pmstr;
                            }
                            else
                            {
                                if (i < 1)
                                {
                                    Pmstr = "";
                                }
                                else
                                {
                                    Pmstr = "_" + (i + 1).ToString();
                                }
                                getDyUrl = FileName + Pmstr + FileEXName;
                            }
                            PageStr += "<option value=\"" + getDyUrl + "\">第" + (i + 1) + "页:" + PageARR[i] + "</option>" + newLine;
                        }
                        PageStr += "</select></form>" + newLine;
                        PageStr += "<script  language=\"javascript\" type=\"text/javascript\">" + newLine;
                        int loadNum = 0;
                        if (ReadType == "1" || isPop != 0)
                        {
                            loadNum = 1;
                        }
                        PageStr += "window.getPageInfoURLFileName('" + loadNum + "')" + newLine;
                        PageStr += "</script>" + newLine;
                        break;
                    case 1:
                        PageARR = Content.Split(new string[] { "###" }, StringSplitOptions.None);
                        PageStr += "<table border=\"0\" cellspacing=\"2\" cellpadding=\"2\">\r<tr>\r";
                        for (int j = 0; j < PageARR.Length; j++)
                        {
                            if (PageARR[j].Trim() == string.Empty)
                            {
                                break;
                            }
                            if (ReadType == "1" || isPop != 0)
                            {
                                if (j < 1)
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR;
                                }
                                else
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR + "&Page=" + (j + 1).ToString();
                                }
                                getDyUrl = Pmstr;
                            }
                            else
                            {
                                if (j < 1)
                                {
                                    Pmstr = "";
                                }
                                else
                                {
                                    Pmstr = "_" + (j + 1).ToString();
                                }
                                getDyUrl = FileName + Pmstr + FileEXName;
                            }
                            PageStr += "<td style=\"padding-right:30px;\"><a href=\"" + getDyUrl + "\">第" + (j + 1) + "页:" + PageARR[j] + "</a></td>\r" + newLine;
                            if ((j + 1) % 2 == 0)
                            {
                                PageStr += "</tr><tr>";
                            }
                        }
                        PageStr += "</tr>\r</table>\r";
                        break;
                    case 2:
                        PageARR = Content.Split(new string[] { "###" }, StringSplitOptions.None);
                        for (int m = 0; m < PageARR.Length; m++)
                        {
                            if (PageARR[m].Trim() == string.Empty)
                            {
                                break;
                            }
                            if (ReadType == "1" || isPop != 0)
                            {
                                if (m < 1)
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR;
                                }
                                else
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR + "&Page=" + (m + 1).ToString();
                                }
                                getDyUrl = Pmstr;
                            }
                            else
                            {
                                if (m < 1)
                                {
                                    Pmstr = "";
                                }
                                else
                                {
                                    Pmstr = "_" + (m + 1).ToString();
                                }
                                getDyUrl = FileName + Pmstr + FileEXName;
                            }
                            PageStr += "<div><a href=\"" + getDyUrl + "\">第" + (m + 1) + "页:" + PageARR[m] + "</a></div>" + newLine;
                        }
                        break;
                    default:
                        PageARR = Content.Split(new string[] { "###" }, StringSplitOptions.None);
                        for (int ij = 0; ij < PageARR.Length; ij++)
                        {
                            if (PageARR[ij].Trim() == string.Empty)
                            {
                                break;
                            }
                            if (ReadType == "1" || isPop != 0)
                            {
                                if (ij < 1)
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR;
                                }
                                else
                                {
                                    Pmstr = "content.aspx?id=" + NewsID + CHSTR + "&Page=" + (ij + 1).ToString();
                                }
                                getDyUrl = Pmstr;
                            }
                            else
                            {
                                if (ij < 1)
                                {
                                    Pmstr = "";
                                }
                                else
                                {
                                    Pmstr = "_" + (ij + 1).ToString();
                                }
                                getDyUrl = FileName + Pmstr + FileEXName;
                            }
                            PageStr += "<a href=\"" + getDyUrl + "\">第" + (ij + 1) + "页:" + PageARR[ij] + "</a>&nbsp;&nbsp;" + newLine;
                        }
                        break;
                }
            }
            else
            {
                PageStr = Content;
            }
            return PageStr;
        }

        /// <summary>
        /// 得到不规则新闻
        /// </summary>
        /// <param name="NewsID"></param>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public string getSubSTR(string NewsID, string str_SubNaviCSS)
        {
            string str_unRule = string.Empty;
            int int_rows = 0;
            int int_rows1 = 1;
            string str_titleCss = string.Empty;
            string naviStr = string.Empty;
            if (str_SubNaviCSS != string.Empty)
            {
                naviStr = Foosun.Common.Input.ToshowTxt(Foosun.Common.Input.isPicStr(str_SubNaviCSS));
            }
            DataTable dt = CommonData.DalPublish.GetSubUnRule(NewsID);
            if (dt != null && dt.Rows.Count > 0)
            {
                str_unRule += "<div>";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    str_titleCss = dt.Rows[i]["TitleCSS"].ToString();
                    if (str_titleCss != null && str_titleCss != string.Empty)
                    {
                        str_titleCss = " class=\"" + str_titleCss + "\"";
                    }
                    else{ str_titleCss = string.Empty;}
                    int_rows = int.Parse(dt.Rows[i]["colsNum"].ToString());
                    IDataReader rd = CommonData.DalPublish.GetNewsSavePath(dt.Rows[i]["getNewsID"].ToString());
                    string str_NewsUrl = string.Empty;
                    if (rd.Read())
                    {
                        PubClassInfo ci = CommonData.GetClassById(rd["ClassID"].ToString());
                        if (ci != null && rd["SavePath"] != DBNull.Value)
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
                                str_unRule += naviStr + "<a href=\"" + str_NewsUrl + "\" target=\"_blank\" " + str_titleCss + ">" + dt.Rows[i]["NewsTitle"].ToString() + "</a>&nbsp;\r";
                            }
                            else
                            {
                                str_unRule += "<a href=\"" + str_NewsUrl + "\" target=\"_blank\" " + str_titleCss + ">" + dt.Rows[i]["NewsTitle"].ToString() + "</a>&nbsp;\r";
                            }
                        }
                        else
                        {
                            int_rows1 = int_rows1 + 1;
                            str_unRule += "<br />" + naviStr + "<a href=\"" + str_NewsUrl + "\" target=\"_blank\" " + str_titleCss + ">" + dt.Rows[i]["NewsTitle"].ToString() + "</a>&nbsp;\r";
                        }
                    }
                    rd.Close();
                }
                dt.Clear(); dt.Dispose();
                str_unRule += "</div>\r";
            }
            return str_unRule;
        }

        /// <summary>
        /// 动态得到位置导航
        /// </summary>
        /// <param name="DynStr"></param>
        /// <param name="ID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string getPositionSTR(string DynStr,string ClassID,int Num)
        {
            if (dimmDir.Trim() != string.Empty)
            {
                dimmDir = "/" + dimmDir;
            }
            string PStr = string.Empty;
            if (Num == 0)
            {
                DataTable dt = CommonData.DalPublish.GetPosition(ClassID, 0);
                if (dt != null && dt.Rows.Count > 0)
                {
                    PStr = "<a href=\"" + getClassURL(dt.Rows[0]["Domain"].ToString(),int.Parse(dt.Rows[0]["isDelPoint"].ToString()), ClassID, dt.Rows[0]["savePath"].ToString(), dt.Rows[0]["saveClassFrame"].ToString(), dt.Rows[0]["ClassSaveRule"].ToString()) + "\">" + dt.Rows[0]["ClassCName"].ToString() + "</a>" + DynStr;
                    if (dt.Rows[0]["ParentID"].ToString() != "0")
                    {
                        PStr = getPositionSTR(DynStr, dt.Rows[0]["ParentID"].ToString(),0) + PStr;
                    }
                    dt.Clear(); dt.Dispose();
                }
            }
            else
            {
                DataTable dt = CommonData.DalPublish.GetPosition(ClassID, 1);
                if (dt != null && dt.Rows.Count > 0)
                {
                    PStr = "<a href=\"" + getSpeacilURL(dt.Rows[0]["isDelPoint"].ToString(), dt.Rows[0]["SpecialID"].ToString(), dt.Rows[0]["savePath"].ToString(), dt.Rows[0]["saveDirPath"].ToString(), dt.Rows[0]["FileName"].ToString(), dt.Rows[0]["FileEXName"].ToString()) + "\">" + dt.Rows[0]["SpecialCName"].ToString() + "</a>" + DynStr;
                    if (dt.Rows[0]["ParentID"].ToString() != "0")
                    {
                        PStr = getPositionSTR(DynStr, dt.Rows[0]["ParentID"].ToString(),1) + PStr;
                    }
                    dt.Clear(); dt.Dispose();
                }
            }
            return PStr;
        }

        public string getCHPositionSTR(string DynStr, int ID, string Str,int ChID)
        {
            if (dimmDir.Trim() != string.Empty)
            {
                dimmDir = "/" + dimmDir;
            }
            string PStr = string.Empty;
            switch (Str)
            {
                case "ChClass":
                    IDataReader dr = CommonData.DalPublish.GetCHPosition(ID, 0, ChID);
                    if (dr.Read())
                    {
                        PStr = "<a href=\"" + getCHClassURL(ChID, int.Parse(dr["isDelPoint"].ToString()), int.Parse(dr["id"].ToString()), dr["SavePath"].ToString(), dr["FileName"].ToString()) + "\">" + dr["classCName"].ToString() + "</a>" + DynStr;
                        if (dr["ParentID"].ToString() != "0")
                        {
                            PStr = getCHPositionSTR(DynStr, int.Parse(dr["ParentID"].ToString()),Str,ChID) + PStr;
                        }
                    }
                    dr.Close();
                    break;
                case "ChNews":
                    IDataReader drn = CommonData.DalPublish.GetCHPosition(ID, 1, ChID);
                    if (drn.Read())
                    {
                        PStr = "<a href=\"" + getCHClassURL(ChID, int.Parse(drn["isDelPoint"].ToString()), int.Parse(drn["id"].ToString()), drn["SavePath"].ToString(), drn["FileName"].ToString()) + "\">" + drn["classCName"].ToString() + "</a>" + DynStr;
                        if (drn["ParentID"].ToString() != "0")
                        {
                            PStr = getCHPositionSTR(DynStr, int.Parse(drn["ParentID"].ToString()), Str, ChID) + PStr;
                        }
                    }
                    PStr += "正文";
                    drn.Close();
                    break;
                case "ChSpecial":
                    IDataReader drs = CommonData.DalPublish.GetCHPosition(ID, 2, ChID);
                    if (drs.Read())
                    {
                        PStr = "<a href=\"" + getCHSpecialURL(ChID, 0, int.Parse(drs["id"].ToString()), drs["SavePath"].ToString(), drs["FileName"].ToString()) + "\">" + drs["specialCName"].ToString() + "</a>" + DynStr;
                        if (drs["ParentID"].ToString() != "0")
                        {
                            PStr = getCHPositionSTR(DynStr, int.Parse(drs["ParentID"].ToString()), Str, ChID) + PStr;
                        }
                    }
                    PStr += "专题报道";
                    drs.Close();
                    break;
                default:
                    break;
            }
            return PStr;
        }

        protected string getNewsDomain(string NewsID)
        {
            string newsDomain = CommonData.DalPublish.getNewsDomain(NewsID);
            if (newsDomain != null && newsDomain == "")
            {
                return CommonData.SiteDomain;
            }
            else
            {
                if (newsDomain.StartsWith(@"http://"))
                {
                    return newsDomain;
                }
                else
                {
                    return @"http://" + newsDomain;
                }
            }
        }
    }
}
