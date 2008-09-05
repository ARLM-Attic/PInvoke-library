using Discuz.Common;
using Discuz.Entity;
using Discuz.Forum;

namespace Discuz.Web
{
    /// <summary>
    /// 显示短消息页面
    /// </summary>
    public class usercpshowpm : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 短消息发件人
        /// </summary>
        public string msgfrom = "";

        /// <summary>
        /// 短消息标题
        /// </summary>
        public string subject = "";

        /// <summary>
        /// 短消息内容
        /// </summary>
        public string message = "";

        /// <summary>
        /// 短消息回复标题
        /// </summary>
        public string resubject = "";

        /// <summary>
        /// 短消息回复内容
        /// </summary>
        public string remessage = "";

        /// <summary>
        /// 短消息发送时间
        /// </summary>
        public string postdatetime = "";

        /// <summary>
        /// 短消息Id
        /// </summary>
        public int pmid = 0;

        /// <summary>
        /// 是否能够回复短消息
        /// </summary>
        public bool canreplypm = true;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();
        #endregion

        protected override void ShowPage()
        {
            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }

            pagetitle = "查看短消息";
            user = Users.GetUserInfo(userid);

            pmid = DNTRequest.GetQueryInt("pmid", -1);
            if (pmid <= 0)
            {
                AddErrLine("参数无效");
                return;
            }

            if (!UserCredits.CheckUserCreditsIsEnough(userid, 1, CreditsOperationType.SendMessage, -1))
            {
                canreplypm = false;
            }

            PrivateMessageInfo messageinfo = PrivateMessages.GetPrivateMessageInfo(pmid);

            if (messageinfo == null)
            {
                AddErrLine("无效的短消息ID");
                return;
            }

            if (messageinfo.Msgfrom == "系统" && messageinfo.Msgfromid == 0)
            {
                messageinfo.Message = Utils.HtmlDecode(messageinfo.Message);
            }

            if (messageinfo != null)
            {
                //判断当前用户是否有权阅读此消息
                if (messageinfo.Msgtoid == userid || messageinfo.Msgfromid == userid)
                {
                    string action = DNTRequest.GetQueryString("action");
                    if (action.CompareTo("delete") == 0)
                    {
                        ispost = true;
                        int retval = PrivateMessages.DeletePrivateMessage(userid, pmid);
                        if (retval < 1)
                        {
                            AddErrLine("消息未找到,可能已被删除");
                            return;
                        }
                        else
                        {
                            AddMsgLine("指定消息成功删除,现在将转入消息列表");
                            SetUrl("usercpinbox.aspx");
                            SetMetaRefresh();
                            return;
                        }
                    }

                    if (action.CompareTo("noread") == 0)
                    {
                        PrivateMessages.SetPrivateMessageState(pmid, 1); //将短消息的状态置 1 表示未读
                        ispost = true;
                        if (messageinfo.New != 1 && messageinfo.Folder == 0)
                        {
                            Users.DecreaseNewPMCount(userid, -1); //将用户的未读短信息数据加 1
                            AddMsgLine("指定消息已被置成未读状态,现在将转入消息列表");
                            SetUrl("usercpinbox.aspx");
                            SetMetaRefresh();
                        }
                    }
                    else
                    {
                        PrivateMessages.SetPrivateMessageState(pmid, 0); //将短消息的状态置 0 表示已读

                        if (messageinfo.New == 1 && messageinfo.Folder == 0)
                        {
                            Users.DecreaseNewPMCount(userid); //将用户的未读短信息数据减 1
                        }
                    }

                    msgfrom = messageinfo.Msgfrom;
                    subject = messageinfo.Subject;
                    message = Utils.StrFormat(messageinfo.Message);
                    postdatetime = messageinfo.Postdatetime;
                    resubject = "re:" + messageinfo.Subject;
                    remessage = Utils.HtmlEncode("> ") + messageinfo.Message.Replace("\n", "\n> ") + "\r\n\r\n";
                    return;
                }
            }
            AddErrLine("对不起, 短消息不存在或已被删除.");
        }
    } //class end
}