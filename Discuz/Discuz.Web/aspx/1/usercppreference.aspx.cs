using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 用户个性设置
    /// </summary>
    public class usercppreference : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        /// <summary>
        /// 用户头像
        /// </summary>
        public string avatar;

        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string avatarurl;

        /// <summary>
        /// 用户头像类型
        /// </summary>
        public int avatartype;

        /// <summary>
        /// 头像宽度
        /// </summary>
        public int avatarwidth;

        /// <summary>
        /// 头像高度
        /// </summary>
        public int avatarheight;

        /// <summary>
        /// 可用的模板列表
        /// </summary>
        public DataTable templatelist;

        /// <summary>
        /// 系统头像列表
        /// </summary>
        public DataTable avatarfilelist;

        #endregion

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";
            if (userid == -1)
            {
                AddErrLine("你尚未登录");
                return;
            }
            user = Users.GetUserInfo(userid);            

            avatarwidth = 100;
            avatarheight = 100;

            if (DNTRequest.IsPost())
            {
                int avatartype = DNTRequest.GetInt("avatartype", -1);
                if (avatartype != -1)
                {
                    switch (avatartype)
                    {
                        case 0: //从系统选择
                            avatar = DNTRequest.GetString("usingavatar");
                            avatar = Utils.UrlDecode(avatar.Substring(avatar.IndexOf("avatar")));
                            avatarwidth = 0;
                            avatarheight = 0;
                            if (!File.Exists(Utils.GetMapPath(BaseConfigs.GetForumPath) + avatar))
                            {
                                AddErrLine("不存在的头像文件");
                                return;
                            }

                            break;

                        case 1: //上传头像

                            if (usergroupinfo.Allowavatar < 3)
                            {
                                AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有上传头像的权限");
                                return;
                            }
                            avatar = ForumUtils.SaveRequestAvatarFile(userid, config.Maxavatarsize);
                            if (avatar.Equals(""))
                            {
                                AddErrLine(
                                    string.Format("头像图片不合法, 系统要求必须为gif jpg png图片, 且宽高不得超过 {0}x{1}, 大小不得超过 {2} 字节",
                                                  config.Maxavatarwidth, config.Maxavatarheight, config.Maxavatarsize));
                                return;
                            }
                            Thumbnail thumb = new Thumbnail();
                            if (!thumb.SetImage(avatar))
                            {
                                AddErrLine("非法的图片格式");
                                return;
                            }
                            thumb.SaveThumbnailImage(config.Maxavatarwidth, config.Maxavatarheight);
                            avatarwidth = 0;
                            avatarheight = 0;
                            break;

                        case 2: //自定义头像Url

                            if (usergroupinfo.Allowavatar < 2)
                            {
                                AddErrLine(string.Format("您当前的身份 \"{0}\" 没有使用自定义头像的权限", usergroupinfo.Grouptitle));
                                return;
                            }
                            avatar = DNTRequest.GetString("avatarurl").Trim();
                            if (avatar.Length < 10)
                            {
                                AddErrLine("头像路径不合法");

                                return;
                            }
                            if (!avatar.Substring(0, 7).ToLower().Equals("http://"))
                            {
                                AddErrLine("头像路径必须以http://开始");
                                return;
                            }
                            string fileextname = Path.GetExtension(avatar).ToLower();
                            // 判断 文件扩展名/文件大小/文件类型 是否符合要求
                            if (
                                !(fileextname.Equals(".jpg") || fileextname.Equals(".gif") || fileextname.Equals(".png")))
                            {
                                AddErrLine("头像路径必须是.jpg .gif或.png结尾");
                                return;
                            }

                            avatarwidth = DNTRequest.GetInt("avatarwidth", config.Maxavatarwidth);
                            avatarheight = DNTRequest.GetInt("avatarheight", config.Maxavatarheight);
                            if (avatarwidth <= 0 || avatarwidth > config.Maxavatarwidth || avatarheight <= 0 ||
                                avatarheight > config.Maxavatarheight)
                            {
                                AddErrLine(
                                    string.Format("自定义URL地址头像尺寸必须大于0, 且必须小于系统当前设置的最大尺寸 {0}x{1}", config.Maxavatarwidth,
                                                  config.Maxavatarheight));

                                return;
                            }
                            break;
                    }                    
                }
                else
                { 
                    //当允许使用头像时
                    if (usergroupinfo.Allowavatar > 0)
                    {
                        AddErrLine("请指定新头像的信息<br />");
                        return;
                    }
                }

                //当不允许使用头像时
                if (usergroupinfo.Allowavatar == 0)
                {
                    avatar = user.Avatar;
                    avatarwidth = user.Avatarwidth;
                    avatarheight = user.Avatarheight;
                }
                Users.UpdateUserPreference(userid, avatar, avatarwidth, avatarheight,
                                               DNTRequest.GetInt("templateid", 0));
                SetUrl("usercppreference.aspx");
                SetMetaRefresh();
                SetShowBackLink(true);
                AddMsgLine("修改个性设置完毕");
            }
            else
            {
                templatelist = Templates.GetValidTemplateList();
                avatarfilelist = Caches.GetAvatarList();

                UserInfo __userinfo = Users.GetUserInfo(userid);
                avatar = __userinfo.Avatar;
                avatarurl = "";
                avatartype = 1;
                avatarwidth = 0;
                avatarheight = 0;
                if (Utils.CutString(avatar, 0, 15).ToLower().Equals(@"avatars\common\"))
                {
                    avatartype = 0;
                }
                else if (Utils.CutString(avatar, 0, 7).ToLower().Equals("http://"))
                {
                    avatarurl = avatar;
                    avatartype = 2;
                    avatarwidth = __userinfo.Avatarwidth;
                    avatarheight = __userinfo.Avatarheight;
                }
            }
        }
    }
}