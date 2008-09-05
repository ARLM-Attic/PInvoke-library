using System;
using System.Text;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// UBB ��ժҪ˵����
    /// </summary>
    public class UBB
    {
        private static RegexOptions options = RegexOptions.IgnoreCase;

        public static Regex[] r = new Regex[20];


        static UBB()
        {
            r[0] = new Regex(@"\s*\[code\]([\s\S]+?)\[\/code\]\s*", options);
            r[1] = new Regex(@"(\[upload=([^\]]{1,4})\])(.*?)(\[\/upload\])", options);
            r[2] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[3] = new Regex(@"(\[uploadimage\])(.*?)(\[\/uploadimage\])", options);
            r[4] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[5] = new Regex(@"(\[uploadfile\])(.*?)(\[\/uploadfile\])", options);
            r[6] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[7] = new Regex(@"(\[upload\])(.*?)(\[\/upload\])", options);
            r[8] = new Regex(@"viewfile.asp\?id=(\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            r[9] = new Regex(@"(\r\n((&nbsp;)|��)+)(?<����>\S+)", options);
            r[10] = new Regex(@"\s*\[hide\][\n\r]*([\s\S]+?)[\n\r]*\[\/hide\]\s*", RegexOptions.IgnoreCase);
            //r[11] = new Regex(@"\s*\[table(=(\d{1,4}%?)(,([\(\)%,#\w]+))?)?\][\n\r]*([\s\S]+?)[\n\r]*\[\/table\]\s*", options);
            r[11] = new Regex(@"\[table(?:=(\d{1,4}%?)(?:,([\(\)%,#\w ]+))?)?\]\s*([\s\S]+?)\s*\[\/table\]", options);
            r[12] = new Regex(@"\[media=(\w{1,4}),(\d{1,4}),(\d{1,4}),(\d)\]\s*([^\[\<\r\n]+?)\s*\[\/media\]", options);
            r[13] = new Regex(@"\[attach\](\d+)(\[/attach\])*", options);
            r[14] = new Regex(@"\[attachimg\](\d+)(\[/attachimg\])*", options);
            r[15] = new Regex(@"\s*\[free\][\n\r]*([\s\S]+?)[\n\r]*\[\/free\]\s*", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// UBB���봦����
        /// </summary>
        ///	<param name="_postpramsinfo">UBBת��������</param>
        /// <returns>����ַ���</returns>
        public static string UBBToHTML(PostpramsInfo _postpramsinfo)
        {

            Match m;

            string sDetail = _postpramsinfo.Sdetail;
            _postpramsinfo.Allowhtml = 1;

            //sDetail = Utils.HtmlDecode(sDetail);

            StringBuilder sb = new StringBuilder();
            int pcodecount = -1;
            string sbStr = "";
            string prefix = _postpramsinfo.Pid.ToString();
            if (_postpramsinfo.Bbcodeoff == 0)
            {

                for (m = r[0].Match(sDetail); m.Success; m = m.NextMatch())
                {
                    sbStr = Parsecode(m.Groups[1].ToString(), prefix, ref pcodecount, ref sb);
                    sDetail = sDetail.Replace(m.Groups[0].ToString(), sbStr);
                }
            }


            #region html��ǩ

            //if (_postpramsinfo.Allowhtml!=1)
            //{
            //sDetail = sDetail.Replace("<","&lt;");
            ///sDetail = sDetail.Replace(">","&gt;");
            //}

            #endregion

            if (_postpramsinfo.Bbcodeoff == 0)
            {
                sDetail = HideDetail(sDetail, _postpramsinfo.Hide);
            }


            //�����Ч��smilie��ǩ
            sDetail = Regex.Replace(sDetail, @"\[smilie\](.+?)\[\/smilie\]", "$1", options);

            #region ����smile�����ǩ

            if (_postpramsinfo.Smileyoff == 0 && _postpramsinfo.Smiliesinfo != null)
            {
                sDetail = ParseSmilies(sDetail, _postpramsinfo.Smiliesinfo, _postpramsinfo.Smiliesmax);
            }

            #endregion

            // [smilie]�����
            sDetail = Regex.Replace(sDetail, @"\[smilie\](.+?)\[\/smilie\]", "<img src=\"$1\" />", options);



            if (_postpramsinfo.Bbcodeoff == 0)
            {


                /*   ȡ����ȫ�պ�
                                // Bold, Italic, Underline
                                //
                                sDetail = Regex.Replace(sDetail, @"\[b(?:\s*)\]([\s]||[\s\S]+?)\[/b(?:\s*)\]", "<b>$1</b>", options);
                                sDetail = Regex.Replace(sDetail, @"\[i(?:\s*)\]([\s]||[\s\S]+?)\[/i(?:\s*)\]", "<i>$1</i>", options);
                                sDetail = Regex.Replace(sDetail, @"\[u(?:\s*)\]([\s]||[\s\S]+?)\[/u(?:\s*)\]", "<u>$1</u>", options);

                                // Sub/Sup
                                //
                                sDetail = Regex.Replace(sDetail, @"\[sup(?:\s*)\]([\s]||[\s\S]+?)\[/sup(?:\s*)\]", "<sup>$1</sup>", options);
                                sDetail = Regex.Replace(sDetail, @"\[sub(?:\s*)\]([\s]||[\s\S]+?)\[/sub(?:\s*)\]", "<sub>$1</sub>", options);
                */

                if (sDetail.ToLower().Contains("[free]") || sDetail.ToLower().Contains("[/free]"))
                {
                    for (m = r[15].Match(sDetail); m.Success; m = m.NextMatch())
                    {
                        sDetail = sDetail.Replace(m.Groups[0].ToString(), "<br /><div class=\"msgheader\">�������:</div><div class=\"msgborder\">" + m.Groups[1].ToString() + "</div><br />");

                    }
                }



                // Bold, Italic, Underline
                //
                sDetail = Regex.Replace(sDetail, @"\[b(?:\s*)\]", "<b>", options);
                sDetail = Regex.Replace(sDetail, @"\[i(?:\s*)\]", "<i>", options);
                sDetail = Regex.Replace(sDetail, @"\[u(?:\s*)\]", "<u>", options);
                sDetail = Regex.Replace(sDetail, @"\[/b(?:\s*)\]", "</b>", options);
                sDetail = Regex.Replace(sDetail, @"\[/i(?:\s*)\]", "</i>", options);
                sDetail = Regex.Replace(sDetail, @"\[/u(?:\s*)\]", "</u>", options);

                // Sub/Sup
                //
                sDetail = Regex.Replace(sDetail, @"\[sup(?:\s*)\]", "<sup>", options);
                sDetail = Regex.Replace(sDetail, @"\[sub(?:\s*)\]", "<sub>", options);
                sDetail = Regex.Replace(sDetail, @"\[/sup(?:\s*)\]", "</sup>", options);
                sDetail = Regex.Replace(sDetail, @"\[/sub(?:\s*)\]", "</sub>", options);

                // P
                //
                sDetail = Regex.Replace(sDetail, @"((\r\n)*\[p\])(.*?)((\r\n)*\[\/p\])", "<p>$3</p>", RegexOptions.IgnoreCase | RegexOptions.Singleline);




                // Anchors
                //
                sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\](www\.(.*?))\[/url(?:\s*)\]", "<a href=\"http://$1\" target=\"_blank\">$1</a>", options);
                sDetail = Regex.Replace(sDetail, @"\[url(?:\s*)\]\s*((https?://|ftp://|gopher://|news://|telnet://|rtsp://|mms://|callto://|bctp://|ed2k://|tencent)([^\[""']+?))\s*\[\/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$1</a>", options);
                sDetail = Regex.Replace(sDetail, @"\[url=www.([^\[""']+?)(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"http://www.$1\" target=\"_blank\">$2</a>", options);
                sDetail = Regex.Replace(sDetail, @"\[url=((https?://|ftp://|gopher://|news://|telnet://|rtsp://|mms://|callto://|bctp://|ed2k://|tencent://)([^\[""']+?))(?:\s*)\]([\s\S]+?)\[/url(?:\s*)\]", "<a href=\"$1\" target=\"_blank\">$4</a>", options);

                // Email
                //
                sDetail = Regex.Replace(sDetail, @"\[email(?:\s*)\](.*?)\[\/email\]", "<a href=\"mailto:$1\" target=\"_blank\">$1</a>", options);
                sDetail = Regex.Replace(sDetail, @"\[email=(.[^\[]*)(?:\s*)\](.*?)\[\/email(?:\s*)\]", "<a href=\"mailto:$1\" target=\"_blank\">$2</a>", options);

                #region

                // Font
                //
                sDetail = Regex.Replace(sDetail, @"\[color=([^\[\<]+?)\]", "<font color=\"$1\">", options);
                sDetail = Regex.Replace(sDetail, @"\[size=(\d+?)\]", "<font size=\"$1\">", options);
                sDetail = Regex.Replace(sDetail, @"\[size=(\d+(\.\d+)?(px|pt|in|cm|mm|pc|em|ex|%)+?)\]", "<font style=\"font-size: $1\">", options);
                sDetail = Regex.Replace(sDetail, @"\[font=([^\[\<]+?)\]", "<font face=\"$1\">", options);

                sDetail = Regex.Replace(sDetail, @"\[align=([^\[\<]+?)\]", "<p align=\"$1\">", options);
                sDetail = Regex.Replace(sDetail, @"\[float=(left|right)\]", "<br style=\"clear: both\"><span style=\"float: $1;\">", options);


                sDetail = Regex.Replace(sDetail, @"\[/color(?:\s*)\]", "</font>", options);
                sDetail = Regex.Replace(sDetail, @"\[/size(?:\s*)\]", "</font>", options);
                sDetail = Regex.Replace(sDetail, @"\[/font(?:\s*)\]", "</font>", options);
                sDetail = Regex.Replace(sDetail, @"\[/align(?:\s*)\]", "</p>", options);
                sDetail = Regex.Replace(sDetail, @"\[/float(?:\s*)\]", "</span>", options);

                // BlockQuote
                //
                sDetail = Regex.Replace(sDetail, @"\[indent(?:\s*)\]", "<blockquote>", options);
                sDetail = Regex.Replace(sDetail, @"\[/indent(?:\s*)\]", "</blockquote>", options);
                sDetail = Regex.Replace(sDetail, @"\[simpletag(?:\s*)\]", "<blockquote>", options);
                sDetail = Regex.Replace(sDetail, @"\[/simpletag(?:\s*)\]", "</blockquote>", options);

                // List
                //
                sDetail = Regex.Replace(sDetail, @"\[list\]", "<ul>", options);
                sDetail = Regex.Replace(sDetail, @"\[list=(1|A|a|I|i| )\]", "<ul type=\"$1\">", options);
                sDetail = Regex.Replace(sDetail, @"\[\*\]", "<li>", options);
                sDetail = Regex.Replace(sDetail, @"\[/list\]", "</ul>", options);


                #endregion
                /*
				// Font
				//
				sDetail = Regex.Replace(sDetail, @"\[color=([^\[\<]+?)\]([\s]||[\s\S]+?)\[/color(?:\s*)\]", "<font color=\"$1\">$2</font>", options);
				sDetail = Regex.Replace(sDetail, @"\[size=(\d+?)\]([\s]||[\s\S]+?)\[/size(?:\s*)\]", "<font size=\"$1\">$2</font>", options);
				sDetail = Regex.Replace(sDetail, @"\[size=(\d+(\.\d+)?(px|pt|in|cm|mm|pc|em|ex|%)+?)\]([\s]||[\s\S]+?)\[/size(?:\s*)\]", "<font style=\"font-size: $1\">$4</font>", options);
				sDetail = Regex.Replace(sDetail, @"\[font=([^\[\<]+?)\]([\s]||[\s\S]+?)\[/font(?:\s*)\]", "<font face=\"$1\">$2</font>", options);

                sDetail = Regex.Replace(sDetail, @"\[align=(left|center|right)\]([\s]||[\s\S]+?)\[/align(?:\s*)\]", "<p align=\"$1\">$2</p>", options);
                sDetail = Regex.Replace(sDetail, @"\[float=(left|right)\]([\s]||[\s\S]+?)\[/float(?:\s*)\]", "<br style=\"clear: both\" /><span style=\"float: $1;\">$2</span>", options);



				// BlockQuote
				//
				sDetail = Regex.Replace(sDetail, @"\[indent(?:\s*)\]([\s]||[\s\S]+?)\[/indent(?:\s*)\]", "<blockquote>$1</blockquote>", options);
				sDetail = Regex.Replace(sDetail, @"\[simpletag(?:\s*)\]([\s]||[\s\S]+?)\[/simpletag(?:\s*)\]", "<blockquote>$1</blockquote>", options);

				// List
				//
				sDetail = Regex.Replace(sDetail, @"\[list\]([\s]||[\s\S]+?)\[/list\]", "<ul>$1</ul>", options);
				sDetail = Regex.Replace(sDetail, @"\[list=(1|A|a|I|i| )\]([\s]||[\s\S]+?)\[/list\]", "<ul type=\"$1\">$2</ul>", options);
				sDetail = Regex.Replace(sDetail, @"\[\*\]", "<li>", options);
*/

                #region ѭ��ת��table

                sDetail = ParseTable(sDetail);

                #endregion

                // shadow
                //
                sDetail = Regex.Replace(sDetail, @"(\[SHADOW=)(\d*?),(#*\w*?),(\d*?)\]([\s]||[\s\S]+?)(\[\/SHADOW\])", "<table width='$2'  style='filter:SHADOW(COLOR=$3, STRENGTH=$4)'>$5</table>", options);

                // glow
                //
                sDetail = Regex.Replace(sDetail, @"(\[glow=)(\d*?),(#*\w*?),(\d*?)\]([\s]||[\s\S]+?)(\[\/glow\])", "<table width='$2'  style='filter:GLOW(COLOR=$3, STRENGTH=$4)'>$5</table>", options);

                // center
                //
                sDetail = Regex.Replace(sDetail, @"\[center\]([\s]||[\s\S]+?)\[\/center\]", "<center>$1</center>", options);


                // Media
                //                
                MatchCollection mc = r[12].Matches(sDetail);
                foreach (Match match in mc)
                {
                    sDetail = sDetail.Replace(match.Groups[0].Value, ParseMedia(match.Groups[1].Value, Utils.StrToInt(match.Groups[2].Value, 64), Utils.StrToInt(match.Groups[3].Value, 48), match.Groups[4].Value == "1" ? true : false, match.Groups[5].Value));
                }


                #region �Զ����ǩ

                if (_postpramsinfo.Customeditorbuttoninfo != null)
                {
                    sDetail = ReplaceCustomTag(sDetail, _postpramsinfo.Customeditorbuttoninfo);
                }

                #endregion

                #region ��ʱδ�õ��ı�ǩ

                //				#region ��[flash=x][/flash]���
                //				//��[mp=x][/mp]���
                //				r = new Regex(@"(\[flash=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/flash\])",RegexOptions.IgnoreCase);
                //				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				{
                //					sDetail = sDetail.Replace(m.Groups[0].ToString(),
                //						"<a href=" + m.Groups[4].ToString() + " TARGET=_blank><IMG SRC=editor/images/swf.gif border=0 alt=������´������͸�FLASH����!> [ȫ������]</a><br /><br /><OBJECT codeBase=http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0 classid=clsid:D27CDB6E-AE6D-11cf-96B8-444553540000 width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><PARAM NAME=movie VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=quality VALUE=high><param name=menu value=false><embed src=" + m.Groups[4].ToString() + " quality=high menu=false pluginspage=http://www.macromedia.com/go/getflashplayer type=application/x-shockwave-flash width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + ">" + m.Groups[4].ToString() + "</embed></OBJECT>");
                //				}
                //				#endregion
                //
                //				#region ��[dir=x][/DIR]���
                //				//��[dir=x][/DIR]���
                //				r = new Regex(@"(\[dir=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/dir\])",RegexOptions.IgnoreCase);
                //				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				{
                //					sDetail = sDetail.Replace(m.Groups[0].ToString(),
                //						"<object classid=clsid:166B1BCA-3F9C-11CF-8075-444553540000 codebase=http://download.macromedia.com/pub/shockwave/cabs/director/sw.cab#version=7,0,2,0 width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><param name=src value=" + m.Groups[4].ToString() + "><embed src=" + m.Groups[4].ToString() + " pluginspage=http://www.macromedia.com/shockwave/download/ width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "></embed></object>");
                //				}
                //				#endregion
                //
                //				#region ��[rm=x][/rm]���
                //				//��[rm=x][/rm]���
                //				r = new Regex(@"(\[rm=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/rm\])",RegexOptions.IgnoreCase);
                //				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				{
                //					sDetail = sDetail.Replace(m.Groups[0].ToString(),
                //						"<OBJECT classid=clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA class=OBJECT id=RAOCX width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "><PARAM NAME=SRC VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=CONSOLE VALUE=Clip1><PARAM NAME=CONTROLS VALUE=imagewindow><PARAM NAME=AUTOSTART VALUE=true></OBJECT><br /><OBJECT classid=CLSID:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA height=32 id=video2 width=" + m.Groups[2].ToString() + "><PARAM NAME=SRC VALUE=" + m.Groups[4].ToString() + "><PARAM NAME=AUTOSTART VALUE=-1><PARAM NAME=CONTROLS VALUE=controlpanel><PARAM NAME=CONSOLE VALUE=Clip1></OBJECT>");
                //				}
                //				#endregion
                //
                //				#region ��[mp=x][/mp]���
                //				//��[mp=x][/mp]���
                //				r = new Regex(@"(\[mp=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/mp\])",RegexOptions.IgnoreCase);
                //				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				{
                //					sDetail = sDetail.Replace(m.Groups[0].ToString(),
                //						"<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + " ><param name=ShowStatusBar value=-1><param name=Filename value=" + m.Groups[4].ToString() + "><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=" + m.Groups[4].ToString() + "  width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "></embed></object>");
                //				}
                //				#endregion
                //
                //				#region ��[wmv=x][/wmv]���
                //				//��[wmv=x][/wmv]���
                //				r = new Regex(@"(\[wmv=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/wmv\])",RegexOptions.IgnoreCase);
                //				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				{
                //					sDetail = sDetail.Replace(m.Groups[0].ToString(),
                //						"<object align=middle classid=CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95 class=OBJECT id=MediaPlayer width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + " ><param name=ShowStatusBar value=-1><param name=Filename value=" + m.Groups[4].ToString() + "><embed type=application/x-oleobject codebase=http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701 flename=mp src=" + m.Groups[4].ToString() + "  width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + "></embed></object>");
                //				}
                //				#endregion
                //
                //				#region ��[qt=x][/QT]���
                //				//��[qt=x][/QT]���
                //				r = new Regex(@"(\[qt=)(\d*?),(\d*?)\]([\S\t]*?)(\[\/qt\])",RegexOptions.IgnoreCase);
                //				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				{
                //					sDetail = sDetail.Replace(m.Groups[0].ToString(),
                //						"<embed src=" + m.Groups[4].ToString() + " width=" + m.Groups[2].ToString() + " height=" + m.Groups[3].ToString() + " autoplay=true loop=false controller=true playeveryframe=false cache=false scale=TOFIT bgcolor=#000000 kioskmode=false targetcache=false pluginspage=http://www.apple.com/quicktime/>");
                //				}
                //				#endregion
                //				#region ����[move][/move]���
                //				//			r = new Regex(@"(\[move\])([ \S\t]*?)(\[\/move\])",RegexOptions.IgnoreCase);
                //				//			for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				//			{
                //				//				sDetail = sDetail.Replace(m.Groups[0].ToString(),"<MARQUEE scrollamount=3>" + m.Groups[2].ToString() + "</MARQUEE>");
                //				//			}
                //				#endregion
                //
                //				#region ����[fly][/fly]���
                //				//			r = new Regex(@"(\[FLY\])([ \S\t]*?)(\[\/FLY\])",RegexOptions.IgnoreCase);
                //				//			for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
                //				//			{
                //				//				sDetail = sDetail.Replace(m.Groups[0].ToString(),"<MARQUEE width=80% behavior=alternate scrollamount=3>" + m.Groups[2].ToString() + "</MARQUEE>");
                //				//			}
                //				#endregion

                #endregion

                #region ����[quote][/quote]���

                int intQuoteIndexOf = sDetail.ToLower().IndexOf("[quote]");
                int quotecount = 0;
                while (intQuoteIndexOf >= 0 && sDetail.ToLower().IndexOf("[/quote]") >= 0 && quotecount < 3)
                {
                    quotecount++;
                    sDetail = Regex.Replace(sDetail, @"\[quote\]([\s\S]+?)\[/quote\]", "<br /><br /><div class=\"msgheader\">����:</div><div class=\"msgborder\">$1</div>", options);


                    intQuoteIndexOf = sDetail.ToLower().IndexOf("[quote]", intQuoteIndexOf + 7);
                }
                //sDetail = sDetail.Replace("[quote]", string.Empty).Replace("[/quote]", string.Empty);

                #endregion


                //����[area]��ǩ
                sDetail = Regex.Replace(sDetail, @"\[area=([\s\S]+?)\]([\s\S]+?)\[/area\]", "<br /><br /><div class=\"msgheader\">$1</div><div class=\"msgborder\">$2</div>", options);

                sDetail = Regex.Replace(sDetail, @"\[area\]([\s\S]+?)\[/area\]", "<br /><br /><div class=\"msgheader\"></div><div class=\"msgborder\">$1</div>", options);


                #region ��������ģʽubb

                //[upload]..[/upload]

                if (_postpramsinfo.Bbcodemode == 1)
                {
                    ///[upload=jpg].jpg[/upload]

                    string replacement = "";
                    for (m = r[1].Match(sDetail); m.Success; m = m.NextMatch())
                    {

                        Match m1 = r[2].Match(m.Groups[3].ToString().ToLower());
                        if (m1.Success)
                        {
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), "[attach]" + m1.Groups[1] + "[/attach]");
                            continue;
                        }

                        string attachPath = m.Groups[3].ToString().ToLower().Replace("viewfile.asp?id", "attachment.aspx?attachmentid");
                        if (attachPath.IndexOf("attachment.aspx?attachmentid") == -1)
                        {
                            attachPath = BaseConfigs.GetForumPath + "upload/" + attachPath;
                        }
                        else
                        {
                            attachPath = BaseConfigs.GetForumPath + attachPath;
                        }


                        if ("jpg,jpeg,gif,bmp,png".IndexOf(m.Groups[2].ToString().ToLower()) != -1)
                        {
                            if (_postpramsinfo.Showimages == 1)
                            {
                                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<img src=\"" + attachPath + "\" border=\"0\" onload=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.alt=\'������´������ͼƬ\\nCTRL+Mouse ���ֿɷŴ�/��С\';}\" onmouseover=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.style.cursor=\'hand\'; this.alt=\'������´������ͼƬ\\nCTRL+Mouse ���ֿɷŴ�/��С\';}\" onclick=\"if(!this.resized) {return true;} else {window.open(this.src);}\" onmousewheel=\"return imgzoom(this);\" />");
                            }
                            else
                            {
                                replacement = attachPath;
                                if (replacement.LastIndexOf("/") > 0)
                                {
                                    replacement = Utils.CutString(replacement, replacement.LastIndexOf("/"));
                                }
                                replacement = "<p><img alt=\"\" src=\"images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">����</span>: <a href=\"" + attachPath + "\" target=\"_blank\">" + replacement + "</a> </p>";
                                sDetail = sDetail.Replace(m.Groups[0].ToString(), replacement);
                            }

                        }
                        else
                        {
                            replacement = attachPath;
                            if (replacement.LastIndexOf("/") > 0)
                            {
                                replacement = Utils.CutString(replacement, replacement.LastIndexOf("/"));
                            }
                            replacement = "<p><img alt=\"\" src=\"images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">����</span>: <a href=\"" + attachPath + "\" target=\"_blank\">" + replacement + "</a> </p>";
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), replacement);
                        }
                    }


                    sDetail = Regex.Replace(sDetail, @"\[uploadimage\](\d{1,})\[/uploadimage\]", "[attach]$1[/attach]", options);



                    //[uplpadimage].*[/uplpadimage]

                    replacement = "";
                    for (m = r[3].Match(sDetail); m.Success; m = m.NextMatch())
                    {
                        Match m1 = r[4].Match(m.Groups[2].ToString().ToLower());
                        if (m1.Success)
                        {
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), "[attach]" + m1.Groups[1] + "[/attach]");
                            continue;
                        }

                        string attachPath = m.Groups[2].ToString().ToLower().Replace("viewfile.asp?id", "attachment.aspx?attachmentid");
                        if (attachPath.IndexOf("attachment.aspx?attachmentid") == -1)
                        {
                            attachPath = BaseConfigs.GetForumPath + "upload/" + attachPath;
                        }
                        else
                        {
                            attachPath = BaseConfigs.GetForumPath + attachPath;
                        }

                        if (_postpramsinfo.Showimages == 1)
                        {
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), "<img src=\"" + attachPath + "\" border=\"0\" onload=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.alt=\'������´������ͼƬ\\nCTRL+Mouse ���ֿɷŴ�/��С\';}\" onmouseover=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.style.cursor=\'hand\'; this.alt=\'������´������ͼƬ\\nCTRL+Mouse ���ֿɷŴ�/��С\';}\" onclick=\"if(!this.resized) {return true;} else {window.open(this.src);}\" onmousewheel=\"return imgzoom(this);\" />");
                        }
                        else
                        {
                            replacement = attachPath;
                            if (replacement.LastIndexOf("/") > 0)
                            {
                                replacement = Utils.CutString(replacement, replacement.LastIndexOf("/"));
                            }
                            replacement = "<p><img alt=\"\" src=\"images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">����</span>: <a href=\"" + attachPath + "\" target=\"_blank\">" + replacement + "</a> </p>";
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), replacement);
                        }

                    }


                    sDetail = Regex.Replace(sDetail, @"\[uploadfile\](\d{1,})\[/uploadfile\]", "[attach]$1[/attach]", options);



                    //[uploadfile].*[/uploadfile]
                    replacement = "";
                    for (m = r[5].Match(sDetail); m.Success; m = m.NextMatch())
                    {
                        Match m1 = r[6].Match(m.Groups[2].ToString().ToLower());
                        if (m1.Success)
                        {
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), "[attach]" + m1.Groups[1] + "[/attach]");
                            continue;
                        }

                        replacement = m.Groups[2].ToString().ToLower().Replace("viewfile.asp?id", "attachment.aspx?attachmentid");
                        if (replacement.IndexOf("attachment.aspx?attachmentid") == -1)
                        {
                            replacement = BaseConfigs.GetForumPath + "upload/" + replacement;
                        }
                        else
                        {
                            replacement = BaseConfigs.GetForumPath + replacement;
                        }

                        if (replacement.LastIndexOf("/") > 0)
                        {
                            replacement = Utils.CutString(replacement, replacement.LastIndexOf("/"));
                        }
                        replacement = "<p><img alt=\"\" src=\"images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">����</span>: <a href=\"" + BaseConfigs.GetForumPath + m.Groups[2].ToString().ToLower().Replace("viewfile.asp?id", "attachment.aspx?attachmentid") + "\" target=\"_blank\">" + replacement + "</a> </p>";
                        sDetail = sDetail.Replace(m.Groups[0].ToString(), replacement);

                    }


                    sDetail = Regex.Replace(sDetail, @"\[upload\](\d{1,})\[/upload\]", "[attach]$1[/attach]", options);


                    //[upload].*[/upload]
                    replacement = "";
                    for (m = r[7].Match(sDetail); m.Success; m = m.NextMatch())
                    {
                        Match m1 = r[8].Match(m.Groups[2].ToString().ToLower());
                        if (m1.Success)
                        {
                            sDetail = sDetail.Replace(m.Groups[0].ToString(), "[attach]" + m1.Groups[1] + "[/attach]");
                            continue;
                        }

                        replacement = BaseConfigs.GetForumPath + m.Groups[2].ToString().ToLower().Replace("viewfile.asp?id", "attachment.aspx?attachmentid");
                        if (replacement.IndexOf("attachment.aspx?attachmentid") == -1)
                        {
                            replacement = BaseConfigs.GetForumPath + "upload/" + replacement;
                        }
                        else
                        {
                            replacement = BaseConfigs.GetForumPath + replacement;
                        }

                        if (replacement.LastIndexOf("/") > 0)
                        {
                            replacement = Utils.CutString(replacement, replacement.LastIndexOf("/"));
                        }
                        replacement = "<p><img alt=\"\" src=\"images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">����</span>: <a href=\"" + BaseConfigs.GetForumPath + m.Groups[2].ToString().ToLower().Replace("viewfile.asp?id", "attachment.aspx?attachmentid") + "\" target=\"_blank\">" + replacement + "</a> </p>";
                        sDetail = sDetail.Replace(m.Groups[0].ToString(), replacement);

                    }


                    //[uplpadimage]..[/uploadfile]
                    //[uploadfile]..[/uploadfile]
                }


                #endregion
            }


            #region ����ַ�ַ���ת��Ϊ����

            if (_postpramsinfo.Parseurloff == 0)
            {
                sDetail = sDetail.Replace("&amp;", "&");

                // p2p link
                sDetail = Regex.Replace(sDetail, @"^((tencent|ed2k|thunder|vagaa):\/\/[\[\]\|A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
                sDetail = Regex.Replace(sDetail, @"((tencent|ed2k|thunder|vagaa):\/\/[\[\]\|A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)$", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
                sDetail = Regex.Replace(sDetail, @"[^>=\]""]((tencent|ed2k|thunder|vagaa):\/\/[\[\]\|A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", "<a target=\"_blank\" href=\"$1\">$1</a>", options);


                /*				ֻ�ڿͻ��˶��ж�
                                sDetail = Regex.Replace(sDetail, @"([^>=\]" + "\"" + @"'\/]|^)((((https?|ftp):\/\/)|www\.)([\w\-]+\.)*[\w\-\u4e00-\u9fa5]+\.([\.a-zA-Z0-9]+|\u4E2D\u56FD|\u7F51\u7EDC|\u516C\u53F8)((\?|\/|:)+[\w\.\/=\?%\-&~`@':+!]*)+\.(jpg|gif|png|bmp|jpeg))", "$1<img src=\"$2\" border=\"0\" />", options);
                                sDetail = Regex.Replace(sDetail, @"([^>=\]" + "\"" + @"'\/]|^)((((https?|ftp|gopher|news|telnet|rtsp|mms|callto|bctp|ed2k):\/\/)|www\.)([\w\-]+\.)*[\w\-\u4e00-\u9fa5]+\.([\.a-zA-Z0-9]+|\u4E2D\u56FD|\u7F51\u7EDC|\u516C\u53F8)((\?|\/|:)+[\w\.\/=\?%\-&~`@':+!#]*)*)", "$1<a target=\"_blank\" href=\"$2\">$2</a>", options);
                                sDetail = Regex.Replace(sDetail, @"([^\w>=\]:" + "\"" + @"'\.]|^)(([\-\.\w]+@[\.\-\w]+(\.\w+)+))", "$1<a href=\"mailto:$2\">$2</a>", options);
                //				sDetail = Regex.Replace(sDetail, @"^((http||https|ftp|rtsp|mms|gopher|mailto|telnet|news|callto):\/\/[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
                //				sDetail = Regex.Replace(sDetail, @"((http||https|ftp|rtsp|mms|gopher|mailto|telnet|news|callto):\/\/[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)$", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
                //				sDetail = Regex.Replace(sDetail, @"[^>=\]""]((http|https|ftp|rtsp|mms|gopher|mailto|telnet|news|callto):\/\/[A-Za-z0-9\.\/=\?%\-&_~`@':+!]+)", "<a target=\"_blank\" href=\"$1\">$1</a>", options);
                */
            }

            #endregion


            #region ����[img][/img]���

            if (_postpramsinfo.Showimages == 1)
            {
                //����ǩ��html����
                if (_postpramsinfo.Signature == 1)
                {
                    sDetail = Regex.Replace(sDetail, @"\[img\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$1\" border=\"0\" />", options);
                    sDetail = Regex.Replace(sDetail, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" border=\"0\" />", options);
                    sDetail = Regex.Replace(sDetail, @"\[image\](http://[\s\S]+?)\[/image\]", "<img src=\"$1\" border=\"0\" />", options);
                }
                else
                {
                    sDetail = Regex.Replace(sDetail, @"\[img\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$1\" border=\"0\" onload=\"attachimg(this, 'load');\" onclick=\"zoom(this)\" />", options);
                    sDetail = Regex.Replace(sDetail, @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*(http://[^\[\<\r\n]+?)\s*\[\/img\]", "<img src=\"$3\" width=\"$1\" height=\"$2\" border=\"0\" onload=\"attachimg(this, 'load');\" onclick=\"zoom(this)\"  />", options);
                    sDetail = Regex.Replace(sDetail, @"\[image\](http://[\s\S]+?)\[/image\]", "<img src=\"$1\" border=\"0\" />", options);
                }
            }

            #endregion

            //			if (_postpramsinfo.Bbcodeoff == 0)
            //			{
            //				#region  ����δ֪���
            //			
            ////				r = new Regex(@"\[([\/]?(div|p|span|font|center|hr|nobr|link|p|strike|storange|address|a|em|blockquote|ol|ul|li|img|br|h1|h2|h3|h4|h5|h6|script))((\>|\s)*)\]",RegexOptions.IgnoreCase);
            ////				for (m = r.Match(sDetail); m.Success; m = m.NextMatch()) 
            ////				{
            ////					sDetail = sDetail.Replace(m.Groups[0].ToString(),
            ////						"<" + m.Groups[1].ToString() + ">");
            ////				}
            //
            //				#endregion
            //			}


            pcodecount = 0;
            foreach (string str in Utils.SplitString(sb.ToString(), "<>"))
            {
                sDetail = sDetail.Replace("[\tDISCUZ_CODE_" + prefix + "_" + pcodecount.ToString() + "\t]", str);
                pcodecount++;
            }

            #region ����ո�

            sDetail = sDetail.Replace("\t", "&nbsp; &nbsp; ");
            sDetail = sDetail.Replace("  ", "&nbsp; ");

            #endregion

            // [r/]
            //
            sDetail = Regex.Replace(sDetail, @"\[r/\]", "\r", options);

            // [n/]
            //
            sDetail = Regex.Replace(sDetail, @"\[n/\]", "\n", options);
            //
            //			// ����
            //			//
            //			sDetail = Regex.Replace(sDetail, @"(\r\n((&nbsp;)|��)+)(?<����>\S+)", "<br />����$����", options);


            #region ������

            //������,��ÿ�����е�ǰ���������ȫ�ǿո�
            for (m = r[9].Match(sDetail); m.Success; m = m.NextMatch())
            {
                sDetail = sDetail.Replace(m.Groups[0].ToString(), "<br />����" + m.Groups["����"].ToString());
            }

            //������,��ÿ�����е�ǰ���������ȫ�ǿո�
            sDetail = sDetail.Replace("\r\n", "<br />");
            sDetail = sDetail.Replace("\r", "");
            sDetail = sDetail.Replace("\n\n", "<br /><br />");
            sDetail = sDetail.Replace("\n", "<br />");
            sDetail = sDetail.Replace("{rn}", "\r\n");
            sDetail = sDetail.Replace("{nn}", "\n\n");
            sDetail = sDetail.Replace("{r}", "\r");
            sDetail = sDetail.Replace("{n}", "\n");

            #endregion

            return sDetail;


        }


        /// <summary>
        /// ����[hide]��ǩ�е�����
        /// </summary>
        /// <param name="str">��������</param>
        /// <param name="hide">hide���</param>
        /// <returns>��������</returns>
        private static string HideDetail(string str, int hide)
        {
            if (hide == 0)
            {
                return str;
            }

            Match m;

            int intTableIndexOf = str.ToLower().IndexOf("[hide]");

            while (intTableIndexOf >= 0 && str.ToLower().IndexOf("[/hide]") >= 0)
            {
                for (m = r[10].Match(str); m.Success; m = m.NextMatch())
                {
                    if (hide == 1)
                    {
                        str = str.Replace(m.Groups[0].ToString(), "<div class=\"hide\"><div class=\"hidestyle\">***** ���������Ա�ظ��ſ���� *****</div></div>");
                    }
                    else
                    {
                        str = str.Replace(m.Groups[0].ToString(), "<div class=\"hide\"><div class=\"hidestyle\">�������ݻ�Ա�����ظ����ܿ���</div><div class=\"hidetext\"><br />==============================<br /><br />" + m.Groups[1].ToString() + "<br /><br />==============================</div></div>");
                    }
                }
                if (intTableIndexOf + 7 > str.Length)
                {
                    intTableIndexOf = str.ToLower().IndexOf("[table", str.Length);
                }
                else
                {
                    intTableIndexOf = str.ToLower().IndexOf("[table", intTableIndexOf + 7);
                }

            }

            return str;

        }


        /// <summary>
        /// ת������
        /// </summary>
        /// <param name="sDetail">��������</param>
        /// <param name="__smiliesinfo">��������</param>
        /// <param name="smiliesmax">ÿ�ֱ�������ʹ����</param>
        /// <returns>��������</returns>
        private static string ParseSmilies(string sDetail, SmiliesInfo[] smiliesinfo, int smiliesmax)
        {
            if (smiliesinfo == null)
                return sDetail;

            string smilieformatstr = "[smilie]{0}editor/images/smilies/{1}[/smilie]";
            for (int i = 0; i < Smilies.regexSmile.Length; i++)
            {
                if (smiliesmax > 0)
                {
                    sDetail = Smilies.regexSmile[i].Replace(sDetail, string.Format(smilieformatstr, BaseConfigs.GetForumPath, smiliesinfo[i].Url), smiliesmax);
                }
                else
                {
                    sDetail = Smilies.regexSmile[i].Replace(sDetail, string.Format(smilieformatstr, BaseConfigs.GetForumPath, smiliesinfo[i].Url));
                }
            }
            return sDetail;
        }


        /// <summary>
        /// ת���Զ����ǩ
        /// </summary>
        /// <param name="sDetail">��������</param>
        /// <param name="__customeditorbuttoninfo">�Զ����ǩ����</param>
        /// <returns>��������</returns>
        private static string ReplaceCustomTag(string sDetail, CustomEditorButtonInfo[] customeditorbuttoninfo)
        {
            if (customeditorbuttoninfo == null)
                return sDetail;

            string replacement = "";
            int b_params = 0;
            string tempReplacement;

            Match m;

            for (int i = 0; i < Editors.regexCustomTag.Length; i++)
            {
                replacement = customeditorbuttoninfo[i].Replacement;
                b_params = customeditorbuttoninfo[i].Params;

                for (int k = 0; k < customeditorbuttoninfo[i].Nest; k++)
                {
                    for (m = Editors.regexCustomTag[i].Match(sDetail); m.Success; m = m.NextMatch())
                    {
                        tempReplacement = replacement.Replace(@"{1}", m.Groups[m.Groups.Count - 1].ToString());
                        if (b_params > 1)
                        {
                            for (int j = 2; j <= b_params; j++)
                            {
                                if (m.Groups.Count > j)
                                {
                                    tempReplacement = tempReplacement.Replace("{" + j + "}", m.Groups[j].ToString());
                                }
                            }
                        }
                        sDetail = sDetail.Replace(m.Groups[0].ToString(), tempReplacement);
                        sDetail = sDetail.Replace("{RANDOM}", Guid.NewGuid().ToString());
                    }
                }

            }

            return sDetail;
        }



        /// <summary>
        /// ת�����
        /// </summary>
        /// <param name="str">��������</param>
        /// <returns>��������</returns>
        private static string ParseTable(string str)
        {
            //RegexOptions options = RegexOptions.IgnoreCase;

            Match m;
            string stable = "";
            string width = "";
            string bgcolor = "";
            int intTableIndexOf = str.ToLower().IndexOf("[table");

            //r = new Regex(@"\s*\[table(=(\d{1,3}%?))?\][\n\r]*([\s\S]+?)[\n\r]*\[\/table\]\s*", RegexOptions.IgnoreCase);


            while (intTableIndexOf >= 0 && str.ToLower().IndexOf("[/table]") >= 0)
            {
                for (m = r[11].Match(str); m.Success; m = m.NextMatch())
                {
                    width = m.Groups[1].ToString();
                    width = Utils.CutString(width, width.Length - 1, width.Length).Equals("%") ? (Utils.StrToInt(Utils.CutString(width, 0, width.Length - 1), 100) <= 98 ? width : "98%") : (Utils.StrToInt(width, 560) <= 560 ? width : "560");

                    bgcolor = m.Groups[2].ToString();

                    stable = "<table class=\"t_table\" cellspacing=\"1\" cellpadding=\"4\" style=\"";
                    stable += width.Equals("") ? "" : ("width:" + width + ";");
                    stable += "".Equals(bgcolor) ? "" : ("background: " + bgcolor + ";");
                    stable += "\">";


                    width = m.Groups[3].ToString();

                    width = Regex.Replace(width, @"\[td=(\d{1,2}),(\d{1,2})(,(\d{1,4}%?))?\]", "<td colspan=\"$1\" rowspan=\"$2\" width=\"$4\" class=\"t_table\">", options);


                    width = Regex.Replace(width, @"\[tr\]", "<tr>", options);
                    width = Regex.Replace(width, @"\[td\]", "<td>", options);
                    width = Regex.Replace(width, @"\[\/td\]", "</td>", options);
                    width = Regex.Replace(width, @"\[\/tr\]", "</tr>", options);

                    width = Regex.Replace(width, @"\<td\>\<\/td\>", "<td>&nbsp;</td>", options);


                    stable += width;
                    stable += "</table>";

                    str = str.Replace(m.Groups[0].ToString(), stable);
                }
                intTableIndexOf = str.ToLower().IndexOf("[table", intTableIndexOf + 7);

            }

            return str;
        }


        /// <summary>
        /// ת��code��ǩ
        /// </summary>
        /// <param name="text">��������</param>
        /// <param name="pcodecount">code������</param>
        /// <param name="builder">ת���������</param>
        /// <returns>��������</returns>
        private static string Parsecode(string text, string prefix, ref int pcodecount, ref StringBuilder builder)
        {
            //RegexOptions options = RegexOptions.IgnoreCase;
            text = Regex.Replace(text, @"^[\n\r]*([\s\S]+?)[\n\r]*$", "$1", options);

            if (!builder.ToString().Equals(""))
            {
                builder.Append("<>");
            }
            builder.Append("<br /><br /><div class=\"msgheader\"><div class=\"right\"><a href=\"###\" class=\"smalltxt\" onclick=\"copycode($('code" + prefix + "_" + pcodecount.ToString() + "'));\">[���Ƶ�������]</a></div>CODE:</div><div id=\"code" + prefix + "_" + pcodecount.ToString() + "\" class=\"msgborder\">");
            builder.Append(text);
            builder.Append("</div><br /><br />");
            pcodecount++;
            text = "[\tDISCUZ_CODE_" + prefix + "_" + pcodecount.ToString() + "\t]";
            return text;
        }

        public static string ParseMedia(string type, int width, int height, bool autostart, string url)
        {
            if (!Utils.InArray(type, "ra,rm,wma,wmv,mp3,mov"))
                return "";
            url = url.Replace("\\\\", "\\").Replace("<", string.Empty).Replace(">", string.Empty);
            Random r = new Random(3);
            string mediaid = "media_" + r.Next();
            switch (type)
            {
                case "ra":
                    return string.Format(@"<object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""{0}"" height=""32""><param name=""autostart"" value=""{1}"" /><param name=""src"" value=""{2}"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""{3}_"" /><embed src=""{2}"" type=""audio/x-pn-realaudio-plugin"" controls=""ControlPanel"" {4} console=""{3}_"" width=""{0}"" height=""32""></embed></object>", width, autostart ? 1 : 0, url, mediaid, autostart ? "autostart=\"true\"" : string.Empty);
                case "rm":
                    return string.Format(@"<object classid=""clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA"" width=""{0}"" height=""{1}""><param name=""autostart"" value=""{2}"" /><param name=""src"" value=""{3}"" /><param name=""controls"" value=""imagewindow"" /><param name=""console"" value=""{4}_"" /><embed src=""{3}"" type=""audio/x-pn-realaudio-plugin"" controls=""IMAGEWINDOW"" console=""{4}_"" width=""{0}"" height=""{1}""></embed></object><br /><object classid=""clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA"" width=""{0}"" height=""32""><param name=""src"" value=""{3}"" /><param name=""controls"" value=""controlpanel"" /><param name=""console"" value=""{4}_"" /><embed src=""{3}"" type=""audio/x-pn-realaudio-plugin"" controls=""ControlPanel"" {5} console=""{4}_"" width=""{0}"" height=""32""></embed></object>", width, height, autostart ? 1 : 0, url, mediaid, autostart ? "autostart=\"true\"" : string.Empty);
                case "wma":
                    return string.Format(@"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""{0}"" height=""64""><param name=""autostart"" value=""{1}"" /><param name=""url"" value=""{2}"" /><embed src=""{2}"" autostart=""{1}"" type=""audio/x-ms-wma"" width=""{0}"" height=""64""></embed></object>", width, autostart ? 1 : 0, url);
                case "wmv":
                    return string.Format(@"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""{0}"" height=""{1}""><param name=""autostart"" value=""{2}"" /><param name=""url"" value=""{3}"" /><embed src=""{3}"" autostart=""{2}"" type=""video/x-ms-wmv"" width=""{0}"" height=""{1}""></embed></object>", width, height, autostart ? 1 : 0, url);
                case "mp3":
                    return string.Format(@"<object classid=""clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6"" width=""{0}"" height=""64""><param name=""autostart"" value=""{1}""/><param name=""url"" value=""{2}"" /><embed src=""{2}"" autostart=""{1}"" type=""application/x-mplayer2"" width=""{0}"" height=""64""></embed></object>", width, autostart ? 1 : 0, url);
                case "mov":
                    return string.Format(@"<object classid=""clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B"" width=""{0}"" height=""{1}""><param name=""autostart"" value=""{2}"" /><param name=""src"" value=""{3}"" /><embed controller=""true"" width=""{0}"" height=""{1}"" src=""{3}"" autostart=""{2}""></embed></object>", width, height, autostart.ToString().ToLower(), url);
            }

            return string.Empty;

            //str_replace(array('<', '>'), '', str_replace('\\"', '\"', $url));
            /*string mediaid = "media_" + ;
            $autostartw = $autostart ? 1 : 0;
            switch($type) {
                case 'ra'	: return '<object classid="clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA" width="'.$width.'" height="32"><param name="autostart" value="'.$autostart.'" /><param name="src" value="'.$url.'" /><param name="controls" value="controlpanel" /><param name="console" value="'.$mediaid.'_" /><embed src="'.$url.'" type="audio/x-pn-realaudio-plugin" controls="ControlPanel" '.($autostart ? 'autostart="true"' : '').' console="'.$mediaid.'_" width="'.$width.'" height="32"></embed></object>';break;
                case 'rm'	: return '<object classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA" width="'.$width.'" height="'.$height.'"><param name="autostart" value="'.$autostart.'" /><param name="src" value="'.$url.'" /><param name="controls" value="imagewindow" /><param name="console" value="'.$mediaid.'_" /><embed src="'.$url.'" type="audio/x-pn-realaudio-plugin" controls="IMAGEWINDOW" console="'.$mediaid.'_" width="'.$width.'" height="'.$height.'"></embed></object><br /><object classid="clsid:CFCDAA03-8BE4-11CF-B84B-0020AFBBCCFA" width="'.$width.'" height="32"><param name="src" value="'.$url.'" /><param name="controls" value="controlpanel" /><param name="console" value="'.$mediaid.'_" /><embed src="'.$url.'" type="audio/x-pn-realaudio-plugin" controls="ControlPanel" '.($autostart ? 'autostart="true"' : '').' console="'.$mediaid.'_" width="'.$width.'" height="32"></embed></object>';break;
                case 'wma'	: return '<object classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="'.$width.'" height="64"><param name="autostart" value="'.$autostart.'" /><param name="url" value="'.$url.'" /><embed src="'.$url.'" autostart="'.$autostart.'" type="audio/x-ms-wma" width="'.$width.'" height="64"></embed></object>';break;
                case 'wmv'	: return '<object classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="'.$width.'" height="'.$height.'"><param name="autostart" value="'.$autostart.'" /><param name="url" value="'.$url.'" /><embed src="'.$url.'" autostart="'.$autostart.'" type="video/x-ms-wmv" width="'.$width.'" height="'.$height.'"></embed></object>';break;
                case 'mp3'	: return '<object classid="clsid:6BF52A52-394A-11d3-B153-00C04F79FAA6" width="'.$width.'" height="64"><param name="autostart" value="'.$autostart.'" /><param name="url" value="'.$url.'" /><embed src="'.$url.'" autostart="'.$autostart.'" type="application/x-mplayer2" width="'.$width.'" height="64"></embed></object>';break;
                case 'mov'	: return '<object classid="clsid:02BF25D5-8C17-4B23-BC80-D3488ABDDC6B" width="'.$width.'" height="'.$height.'"><param name="autostart" value="'.($autostart ? 'true' : 'false').'" /><param name="src" value="'.$url.'" /><embed controller="true" width="'.$width.'" height="'.$height.'" src="'.$url.'" autostart="'.($autostart ? 'true' : 'false').'"></embed></object>';break;
    }*/

        }

        #region Useless codes

        //		private static string HtmlSpecialchars(string str)
        //		{
        ////			str = str.Replace("&", "&amp;");
        ////			str = str.Replace("<", "&lt;");
        ////			str = str.Replace(">", "&gt;");
        ////			str = str.Replace("\"", "&quot;");
        //
        //			return str;
        //		}
        #endregion


        /// <summary>
        /// ���UBB��ǩ
        /// </summary>
        /// <param name="sDetail">��������</param>
        /// <returns>��������</returns>
        public static string ClearUBB(string sDetail)
        {
            return Regex.Replace(sDetail, @"\[[^\]]*?\]", string.Empty, RegexOptions.IgnoreCase);

        }

        /// <summary>
        /// ���UBB��ǩ
        /// </summary>
        /// <param name="sDetail">��������</param>
        /// <returns>��������</returns>
        public static string ClearBR(string sDetail)
        {
            return Regex.Replace(sDetail, @"[\r\n]", string.Empty, RegexOptions.IgnoreCase);

        }

        /// <summary>
        /// ���[attach][attachimg]��ǩ
        /// </summary>
        /// <param name="sDetail"></param>
        /// <returns></returns>
        public static string ClearAttachUBB(string sDetail)
        {
            sDetail = r[13].Replace(sDetail, string.Empty);
            return r[14].Replace(sDetail, string.Empty);
        }
    }


}