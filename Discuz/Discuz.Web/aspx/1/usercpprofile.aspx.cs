using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;
using System.Text.RegularExpressions;

namespace Discuz.Web
{
    /// <summary>
    /// 更新用户档案页面
    /// </summary>
    public class usercpprofile : PageBase
    {
        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");

                return;
            }
            user = Users.GetUserInfo(userid);

            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }
                //实名验证
                if (config.Realnamesystem == 1)
                {
                    if (DNTRequest.GetString("realname").Trim() == "")
                    {
                        AddErrLine("真实姓名不能为空");
                    }
                    else if (DNTRequest.GetString("realname").Trim().Length > 10)
                    {
                        AddErrLine("真实姓名不能大于10个字符");
                    }
                    if (DNTRequest.GetString("idcard").Trim() == "")
                    {
                        AddErrLine("身份证号码不能为空");
                    }
                    else if (DNTRequest.GetString("idcard").Trim().Length > 20)
                    {
                        AddErrLine("身份证号码不能大于20个字符");
                    }
                    if (DNTRequest.GetString("mobile").Trim() == "" && DNTRequest.GetString("phone").Trim() == "")
                    {
                        AddErrLine("移动电话号码和是固定电话号码必须填写其中一项");
                    }
                    if (DNTRequest.GetString("mobile").Trim().Length > 20)
                    {
                        AddErrLine("移动电话号码不能大于20个字符");
                    }
                    if (DNTRequest.GetString("phone").Trim().Length > 20)
                    {
                        AddErrLine("固定电话号码不能大于20个字符");
                    }
                }


                if (DNTRequest.GetString("idcard").Trim() != "" &&
                    !Regex.IsMatch(DNTRequest.GetString("idcard").Trim(), @"^[\x20-\x80]+$"))
                {
                    AddErrLine("身份证号码中含有非法字符");
                }

                if (DNTRequest.GetString("mobile").Trim() != "" &&
                    !Regex.IsMatch(DNTRequest.GetString("mobile").Trim(), @"^[\d|-]+$"))
                {
                    AddErrLine("移动电话号码中含有非法字符");
                }

                if (DNTRequest.GetString("phone").Trim() != "" &&
                    !Regex.IsMatch(DNTRequest.GetString("phone").Trim(), @"^[\d|-]+$"))
                {
                    AddErrLine("固定电话号码中含有非法字符");
                }

                //
                string email = DNTRequest.GetString("email").Trim().ToLower();

                if (email.Equals(""))
                {
                    AddErrLine("Email不能为空");
                    return;
                }

                else if (!Utils.IsValidEmail(email))
                {
                    AddErrLine("Email格式不正确");
                    return;
                }
                else
                {
                    int tmpUserid = Users.FindUserEmail(email);
                    if (tmpUserid != userid && tmpUserid != -1 && config.Doublee == 0)
                    {
                        AddErrLine("Email: \"" + email + "\" 已经被其它用户注册使用");
                        return;
                    }

                    string emailhost = Utils.GetEmailHostName(email);
                    // 允许名单规则优先于禁止名单规则
                    if (config.Accessemail.Trim() != "")
                    {
                        // 如果email后缀 不属于 允许名单
                        if (!Utils.InArray(emailhost, config.Accessemail.Replace("\r\n", "\n"), "\n"))
                        {
                            AddErrLine("Email: \"" + email + "\" 不在本论坛允许范围之类, 本论坛只允许用户使用这些Email地址注册: " +
                                       config.Accessemail.Replace("\n", ",&nbsp;"));
                            return;
                        }
                    }
                    else if (config.Censoremail.Trim() != "")
                    {
                        // 如果email后缀 属于 禁止名单
                        if (Utils.InArray(emailhost, config.Censoremail.Replace("\r\n", "\n"), "\n"))
                        {
                            AddErrLine("Email: \"" + email + "\" 不允许在本论坛使用, 本论坛不允许用户使用的Email地址包括: " +
                                       config.Censoremail.Replace("\n", ",&nbsp;"));
                            return;
                        }
                    }
                    if (DNTRequest.GetString("bio").Length > 500)
                    {
                        //如果自我介绍超过500...
                        AddErrLine("自我介绍不得超过500个字符");
                        return;
                    }
                    if (DNTRequest.GetString("signature").Length > 500)
                    {
                        //如果签名超过500...
                        AddErrLine("签名不得超过500个字符");
                        return;
                    }
                }

                if (page_err == 0)
                {
                    UserInfo __userinfo = new UserInfo();
                    __userinfo.Uid = userid;
                    __userinfo.Nickname = Utils.HtmlEncode(DNTRequest.GetString("nickname"));
                    __userinfo.Gender = DNTRequest.GetInt("gender", 0);
                    __userinfo.Realname = DNTRequest.GetString("realname");
                    __userinfo.Idcard = DNTRequest.GetString("idcard");
                    __userinfo.Mobile = DNTRequest.GetString("mobile");
                    __userinfo.Phone = DNTRequest.GetString("phone");
                    __userinfo.Email = email;
                    __userinfo.Bday = Utils.HtmlEncode(DNTRequest.GetString("bday"));
                    __userinfo.Showemail = DNTRequest.GetInt("showemail", 1);
                    if (DNTRequest.GetString("website").IndexOf(".") > -1 &&
                        !DNTRequest.GetString("website").ToLower().StartsWith("http"))
                    {
                        __userinfo.Website = Utils.HtmlEncode("http://" + DNTRequest.GetString("website"));
                    }
                    else
                    {
                        __userinfo.Website = Utils.HtmlEncode(DNTRequest.GetString("website"));
                    }
                    __userinfo.Icq = Utils.HtmlEncode(DNTRequest.GetString("icq"));
                    __userinfo.Qq = Utils.HtmlEncode(DNTRequest.GetString("qq"));
                    __userinfo.Yahoo = Utils.HtmlEncode(DNTRequest.GetString("yahoo"));
                    __userinfo.Msn = Utils.HtmlEncode(DNTRequest.GetString("msn"));
                    __userinfo.Skype = Utils.HtmlEncode(DNTRequest.GetString("skype"));
                    __userinfo.Location = Utils.HtmlEncode(DNTRequest.GetString("location"));
                    __userinfo.Bio = Utils.HtmlEncode(DNTRequest.GetString("bio"));

                    Users.UpdateUserProfile(__userinfo);
                    SetUrl("usercpprofile.aspx");
                    SetMetaRefresh();
                    SetShowBackLink(true);
                    AddMsgLine("修改个人档案完毕");
                }
            }
        }
    }
}