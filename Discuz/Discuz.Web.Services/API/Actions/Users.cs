using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using Discuz.Common;
using Discuz.Entity;

namespace Discuz.Web.Services.API.Actions
{
    public class Users : ActionBase
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetInfo()
        {
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return "";
            }

            if (Uid < 1)
            {
                ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
                return "";
            }

            if (CallId <= LastCallId)
            {
                ErrorCode = (int)ErrorType.API_EC_CALLID;
                return "";
            }

            if (GetParam("uids") == null && !Utils.IsNumericArray(GetParam("uids").ToString().Split(',')))
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return "";
            }

            //if (GetParam("uids") == null || GetParam("fields") == null)
            //{
            //    ErrorCode = (int)ErrorType.API_EC_PARAM;
            //    return "";
            //}

            int userid = Utils.StrToInt(GetParam("uids").ToString().Split(',')[0], -1);

            if (userid < 1)
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return "";
            }

            UserInfo userinfo = Forum.Users.GetUserInfo(userid);

            User user = new User();
            user.AccessMasks = userinfo.Accessmasks;
            user.Adminid = userinfo.Adminid;
            user.AvatarShowId = userinfo.Avatarshowid;
            user.Birthday = userinfo.Bday.Trim();
            user.Credits = userinfo.Credits;
            user.DigestPosts = userinfo.Digestposts;
            user.Email = userinfo.Email.Trim();
            user.ExtCredits1 = userinfo.Extcredits1;
            user.ExtCredits2 = userinfo.Extcredits2;
            user.ExtCredits3 = userinfo.Extcredits3;
            user.ExtCredits4 = userinfo.Extcredits4;
            user.ExtCredits5 = userinfo.Extcredits5;
            user.ExtCredits6 = userinfo.Extcredits6;
            user.ExtCredits7 = userinfo.Extcredits7;
            user.ExtCredits8 = userinfo.Extcredits8;
            user.ExtGroupids = userinfo.Extgroupids.Trim();
            user.Gender = userinfo.Gender;
            user.GroupExpiry = userinfo.Groupexpiry;
            user.GroupId = userinfo.Groupid;
            user.Invisible = userinfo.Invisible;
            user.JoinDate = userinfo.Joindate;
            user.LastActivity = userinfo.Lastactivity;
            user.LastIp = userinfo.Lastip.Trim();
            user.LastPost = userinfo.Lastpost;
            user.LastPostid = userinfo.Lastpostid;
            user.LastPostTitle = userinfo.Lastposttitle;
            user.LastVisit = userinfo.Lastvisit;
            user.NewPm = userinfo.Newpm;
            user.NewPmCount = userinfo.Newpmcount;
            user.NickName = userinfo.Nickname;
            user.OnlineState = userinfo.Onlinestate;
            user.OnlineTime = userinfo.Oltime;
            user.PageViews = userinfo.Pageviews;
            if (userid == Uid)
                user.Password = userinfo.Password;
            user.PmSound = userinfo.Pmsound;
            user.Posts = userinfo.Posts;
            user.Ppp = userinfo.Ppp;
            user.RegIp = userinfo.Regip;
            user.Secques = userinfo.Secques;
            user.ShowEmail = userinfo.Showemail;
            user.SigStatus = userinfo.Sigstatus;
            user.SpaceId = userinfo.Spaceid;
            user.Templateid = userinfo.Templateid;
            user.Tpp = userinfo.Tpp;
            user.Uid = userinfo.Uid;
            user.UserName = userinfo.Username;
            

            user.WebSite = userinfo.Website;	//网站
            user.Icq = userinfo.Icq;	//icq号码
            user.Qq = userinfo.Qq;	//qq号码
            user.Yahoo = userinfo.Yahoo;	//yahoo messenger帐号
            user.Msn = userinfo.Msn;	//msn messenger帐号
            user.Skype = userinfo.Skype;	//skype帐号
            user.Location = userinfo.Location;	//来自
            user.CustomStatus = userinfo.Customstatus;	//自定义头衔
            user.Avatar = userinfo.Avatar.Replace("\\", ",");
            user.AvatarWidth = userinfo.Avatarwidth;	//头像宽度
            user.AvatarHeight = userinfo.Avatarheight;	//头像高度
            user.Medals = userinfo.Medals; //勋章列表
            user.Bio = userinfo.Bio;	//自我介绍
            user.Signature = userinfo.Signature;	//签名
            user.Sightml = userinfo.Sightml;	//签名Html(自动转换得到)
            user.AuthStr = userinfo.Authstr;	//验证码
            user.AuthTime = userinfo.Authtime;	//验证码生成日期
            user.AuthFlag = userinfo.Authflag;	//验证码使用标志(0 未使用,1 用户邮箱验证及用户信息激活, 2 用户密码找回)
            user.RealName = userinfo.Realname;  //用户实名
            user.IdCard = userinfo.Idcard;    //用户身份证件号
            user.Mobile = userinfo.Mobile;    //用户移动电话
            user.Phone = userinfo.Phone;     //用户固定电话

            UserInfoResponse uir = new UserInfoResponse();
            User[] users = new User[1];
            users[0] = user;
            uir.user_array = users;
            

            return SerializationHelper.Serialize(uir); 
        }

        /// <summary>
        /// 获得当前登录用户
        /// </summary>
        /// <returns></returns>
        public string GetLoggedInUser()
        {
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return "";
            }

            //if (Uid < 1)
            //{
            //    ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
            //    return "";
            //}

            float callid = Utils.StrToFloat(GetParam("call_id"), -1);
            if (callid <= LastCallId)
            {
                ErrorCode = (int)ErrorType.API_EC_CALLID;
                return "";
            }

            if (Format == FormatType.JSON)
                return string.Format("\"{0}\"", Uid);

            LoggedInUserResponse loggeduser = new LoggedInUserResponse();
            loggeduser.List = true;
            loggeduser.Uid = Uid;

            return SerializationHelper.Serialize(loggeduser);
        }
    }
}
