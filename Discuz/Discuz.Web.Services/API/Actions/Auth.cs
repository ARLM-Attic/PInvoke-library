using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Entity;
using System.Xml.Serialization;
using System.IO;
using System.Security.Cryptography;
using Discuz.Config;

namespace Discuz.Web.Services.API.Actions
{
    public class Auth : ActionBase
    {
        /// <summary>
        /// 获得会话
        /// </summary>
        /// <returns></returns>
        public string GetSession()
        {
            string returnStr = "";
            if (Signature != GetParam("sig").ToString())
            {
                ErrorCode = (int)ErrorType.API_EC_SIGNATURE;
                return returnStr;
            }

            if (GetParam("auth_token") == null)
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return returnStr;
            }

            string auth_token = GetParam("auth_token").ToString().Replace("[", "+");
            string a = Discuz.Common.DES.Decode(auth_token, Secret.Substring(0, 10));

            string[] userstr = a.Split(',');
            if (userstr.Length != 3)
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return returnStr; 
            }

            int olid = Utils.StrToInt(userstr[0], -1);
            OnlineUserInfo oluser = OnlineUsers.GetOnlineUser(olid);
            if (oluser == null)
            {
                ErrorCode = (int)ErrorType.API_EC_SESSIONKEY;
                return returnStr;
            }
            string time = DateTime.Parse(oluser.Lastupdatetime).ToString("yyyy-MM-dd HH:mm:ss");
            if (time != userstr[1])
            {
                ErrorCode = (int)ErrorType.API_EC_PARAM;
                return returnStr;
            }
            byte[] md5_result = MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(olid.ToString() + Secret));

            StringBuilder sessionkey_builder = new StringBuilder();

            foreach (byte b in md5_result)
                sessionkey_builder.Append(b.ToString("x2"));

            string sessionkey = string.Format("{0}-{1}", sessionkey_builder.ToString(), oluser.Userid.ToString());
            SessionInfo session = new SessionInfo();
            session.SessionKey = sessionkey;
            session.UId = oluser.Userid;
            session.UserName = oluser.Username;
            session.Expires = Utils.StrToInt(userstr[2], 0);
            //session.Expires = (DateTime.Parse(oluser.Lastupdatetime).AddMinutes(0 - GeneralConfigs.GetConfig().Onlinetimeout)).Ticks - DateTime.Now.Ticks;

            if(Format == FormatType.JSON)
                returnStr = string.Format(@"{{""session_key"":""{0}"",""uid"":{1},""user_name"":""{2}"",""expires"":{3}}}", sessionkey, Uid, session.UserName, session.Expires);
            else
                returnStr = SerializationHelper.Serialize(session);

            OnlineUsers.UpdateAction(olid, UserAction.Login.ActionID, 0, GeneralConfigs.GetConfig().Onlinetimeout);


            return returnStr;
        }
    }
}
