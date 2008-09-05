using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web.UI;

using Discuz.Common;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;


namespace Discuz.Web.Admin
{

    /// <summary>
    ///	ajax ��ȡ������Ϣ
    /// </summary>
    public class ajaxpostinfo : UserControl
    {
        public string title = "";
        public string message = "";
        public bool isexist = false;

        protected internal GeneralConfigInfo config;

        public ajaxpostinfo()
        {
            config = GeneralConfigs.GetConfig();
            //�Ƿ�����
            if (DNTRequest.GetString("istopic") == "false")
            {
                int pid = DNTRequest.GetInt("pid", 0);
                DataTable dt = DatabaseProvider.GetInstance().GetPost(Posts.GetPostTableName(), pid);
                GetPostInfo(dt);
                dt.Dispose();
            }
            //�Ƿ�������
            if (DNTRequest.GetString("istopic") == "true")
            {
                int tid = DNTRequest.GetInt("tid", 0);
                DataTable dt = DatabaseProvider.GetInstance().GetMainPostByTid(string.Format("{0}posts{1}", BaseConfigs.GetTablePrefix,Posts.GetPostTableID(tid)), tid);
                GetPostInfo(dt);
                dt.Dispose();
            }
        }

        /// <summary>
        /// ��ȡ������Ϣ
        /// </summary>
        /// <param name="dt"></param>
        public void GetPostInfo(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    isexist = true;
                    PostpramsInfo _postpramsinfo = new PostpramsInfo();
                    _postpramsinfo.Fid = Convert.ToInt32(dt.Rows[0]["fid"].ToString());
                    _postpramsinfo.Tid = Convert.ToInt32(dt.Rows[0]["tid"].ToString());
                    _postpramsinfo.Pid = Convert.ToInt32(dt.Rows[0]["pid"].ToString());
                    _postpramsinfo.Jammer = 1;
                    _postpramsinfo.Attachimgpost = config.Attachimgpost;
                    _postpramsinfo.Showattachmentpath = 1;
                    _postpramsinfo.Showimages = 1;
                    _postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
                    _postpramsinfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
                    _postpramsinfo.Smiliesmax = config.Smiliesmax;
                    _postpramsinfo.Bbcodemode = config.Bbcodemode;

                    _postpramsinfo.Smileyoff = Utils.StrToInt(dt.Rows[0]["smileyoff"], 0);
                    _postpramsinfo.Bbcodeoff = Utils.StrToInt(dt.Rows[0]["bbcodeoff"], 0);
                    _postpramsinfo.Parseurloff = Utils.StrToInt(dt.Rows[0]["parseurloff"], 0);
                    _postpramsinfo.Allowhtml = Utils.StrToInt(dt.Rows[0]["htmlon"], 0);
                    _postpramsinfo.Sdetail = dt.Rows[0]["message"].ToString();

                    message = dt.Rows[0]["message"].ToString();

                    //�ǲ��ǼӸ�����
                    if (_postpramsinfo.Jammer == 1)
                    {
                        message = ForumUtils.AddJammer(message);
                    }

                    _postpramsinfo.Sdetail = message;

                    if (!_postpramsinfo.Ubbmode)
                    {
                        message = UBB.UBBToHTML(_postpramsinfo);
                    }
                    else
                    {
                        message = Utils.HtmlEncode(message);
                    }

                    #region ����

                    // �����ϴ�ͼƬͼ�Ļ�������
                    if (dt.Rows[0]["attachment"].ToString().Equals("1") || new Regex(@"\[attach\](\d+?)\[\/attach\]", RegexOptions.IgnoreCase).IsMatch(message))
                    {
                        DataTable dtAttachment = DatabaseProvider.GetInstance().GetAttachmentsByPid(Utils.StrToInt(dt.Rows[0]["pid"], 0));
                        dtAttachment.Columns.Add("attachimgpost", Type.GetType("System.Int16"));

                        string replacement = "";
                        string filesize = "";

                        foreach (DataRow drAt in dtAttachment.Rows)
                        {
                            if (message.IndexOf("[attach]" + drAt["aid"].ToString() + "[/attach]") != -1)
                            {
                                if (Convert.ToInt64(drAt["filesize"]) > 1024)
                                {
                                    filesize = Convert.ToString(Math.Round(Convert.ToDecimal(drAt["filesize"]) / 1024, 2)) + " K";
                                }
                                else
                                {
                                    filesize = drAt["filesize"].ToString() + " B";
                                }

                                if (Utils.IsImgFilename(drAt["attachment"].ToString().Trim()))
                                {
                                    drAt["attachimgpost"] = 1;

                                    if (_postpramsinfo.Showattachmentpath == 1)
                                    {
                                        replacement = "<img src=\"../../upload/" + drAt["filename"].ToString() + "\" onload=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.alt='������´������ͼƬ CTRL+�����ֿɷŴ�/��С';}\" onmouseover=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.style.cursor='hand'; this.alt='������´������ͼƬ CTRL+Mouse ���ֿɷŴ�/��С';}\" onclick=\"if(!this.resized) { return true; } else { window.open(this.src); }\" onmousewheel=\"return imgzoom(this);\" />";
                                    }
                                    else
                                    {
                                        replacement = "<img src=\"../../attachment.aspx?attachmentid=" + drAt["aid"].ToString() + "\" onload=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.alt='������´������ͼƬ CTRL+�����ֿɷŴ�/��С';}\" onmouseover=\"if(this.width>screen.width*0.7) {this.resized=true; this.width=screen.width*0.7; this.style.cursor='hand'; this.alt='������´������ͼƬ CTRL+Mouse ���ֿɷŴ�/��С';}\" onclick=\"if(!this.resized) { return true; } else { window.open(this.src); }\" onmousewheel=\"return imgzoom(this);\" />";
                                    }
                                }
                                else
                                {
                                    drAt["attachimgpost"] = 0;
                                    replacement = "<p><img alt=\"\" src=\"../../images/attachicons/attachment.gif\" border=\"0\" /><span class=\"bold\">����</span>: <a href=\"../../attachment.aspx?attachmentid=" + drAt["aid"].ToString() + "\" target=\"_blank\">" + drAt["attachment"].ToString().Trim() + "</a> (" + drAt["postdatetime"].ToString() + ", " + filesize + ")<br />�ø��������ش��� " + drAt["downloads"].ToString() + "</p>";
                                }

                                message = message.Replace("[attach]" + drAt["aid"].ToString() + "[/attach]", replacement);
                            }
                        }
                        dtAttachment.Dispose();
                    }

                    #endregion

                    title = Utils.RemoveHtml(dt.Rows[0]["title"].ToString().Trim());
                }
            }
        }
    }
}