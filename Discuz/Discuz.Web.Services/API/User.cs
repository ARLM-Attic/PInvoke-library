using System;
#if NET1
#else
using System.Collections.Generic;
#endif
using System.Text;
using System.Xml.Serialization;
using Discuz.Common;

namespace Discuz.Web.Services.API
{
    public class Affiliation
    {
        [XmlElement("nid")]
        public long NId;

        [XmlElement("name")]
        public string Name;

        [XmlElement("type")]
        public string Type;

        [XmlElement("status")]
        public string Status;

        [XmlElement("year")]
        public string Year;
    }

    public class Affiliations
    {
        [XmlElement("affiliation")]
        public Affiliation[] affiliations_array;

        [XmlIgnore()]
        public Affiliation[] AffiliationCollection
        {
#if NET1
			get { return affiliations_array == null ? new Affiliation[0] : affiliations_array; }
#else
            get { return affiliations_array ?? new Affiliation[0]; }
#endif
        }
    }

    public class Concentrations
    {
        [XmlElement("concentration")]
        public string[] concentration_array;

        [XmlIgnore()]
        public string[] ConcentrationCollection
        {
#if NET1
			get { return concentration_array == null ? new string[0] : concentration_array; }
#else
            get { return concentration_array ?? new string[0]; }
#endif
        }
    }

    public class EducationHistory
    {
        [XmlElement("education_info")]
        public EducationInfo[] educations_array;

        [XmlIgnore()]
        public EducationInfo[] EducationInfo
        {
#if NET1
			get { return educations_array == null ? new EducationInfo[0] : educations_array; }
#else
            get { return educations_array ?? new EducationInfo[0]; }
#endif
        }
    }

    public class EducationInfo
    {
        [XmlElement("name")]
        public string Name;

        [XmlElement("year")]
        public int Year;

        [XmlElement("concentrations")]
        public Concentrations concentrations;

        [XmlIgnore()]
        public string[] Concentrations
        {
            get { return concentrations.ConcentrationCollection; }
        }
    }

    public class HighSchoolInfo
    {
        [XmlElement("hs1_info")]
        public string HighSchoolOneName;

        [XmlElement("hs2_info")]
        public string HighSchoolTwoName;

        [XmlElement("grad_year")]
        public int GraduationYear;

        [XmlElement("hs1_id")]
        public int HighSchoolOneId;

        [XmlElement("hs2_id")]
        public int HighSchoolTwoId;
    }

    public class MeetingFor
    {
        [XmlElement("seeking")]
        public string[] seeking;

        [XmlIgnore()]
        public string[] Seeking
        {
#if NET1
			get { return seeking == null ? new string[0] : seeking; }
#else
            get { return seeking ?? new string[0]; }
#endif
        }
    }

    public class MeetingSex
    {
        [XmlElement("sex")]
        public string[] sex;

        [XmlIgnore()]
        public string[] Sex
        {
#if NET1
			get { return sex == null ? new string[0] : sex; }
#else
            get { return sex ?? new string[0]; }
#endif
        }
    }

    public class Status
    {
        [XmlElement("message")]
        public string Message;

        [XmlElement("time")]
        public long Time;
    }

    public class WorkHistory
    {
        [XmlElement("work_info")]
        public WorkInfo[] workinfo_array;

        [XmlIgnore()]
        public WorkInfo[] WorkInfo
        {
#if NET1
			get { return workinfo_array == null ? new WorkInfo[0] : workinfo_array; }
#else
            get { return workinfo_array ?? new WorkInfo[0]; }
#endif
        }
    }

    public class WorkInfo
    {
        [XmlElement("location")]
        public Location Location;

        [XmlElement("company_name")]
        public string CompanyName;

        [XmlElement("position")]
        public string Position;

        [XmlElement("description")]
        public string Description;

        [XmlElement("start_date")]
        public string StartDate;

        [XmlElement("end_date")]
        public string EndDate;
    }

    public class User //: Friend
    {
/*
        public static readonly string[] FIELDS = { "about_me", "activities", "affiliations", "birthday", "books", 
			"current_location", "education_history", "first_name", "hometown_location", "interests", "last_name", 
			"movies", "music", "name", "notes_count", "pic", "pic_big", "pic_small", "political", "profile_update_time", 
			"quotes", "relationship_status", "religion", "sex", "significant_other_id", 
			"status", "timezone", "tv", "uid", "wall_count" };
        [XmlElement("about_me")]
        public string AboutMe;

        [XmlElement("activities")]
        public string Activities;

        [XmlElement("affiliations")]
        public Affiliations affiliations;

        [XmlIgnore()]
        public Affiliation[] Affiliations
        {
            get
            {
                if (affiliations == null)
                {
                    return new Affiliation[0];
                }
                else
                {
                    return affiliations.AffiliationCollection ?? new Affiliation[0];
                }
            }
        }

        [XmlElement("birthday")]
        public string Birthday;

        [XmlElement("books")]
        public string Books;

        [XmlElement("current_location")]
        public Location CurrentLocation;

        [XmlElement("education_history")]
        public EducationHistory EducationHistory;

        [XmlElement("first_name")]
        public string FirstName;

        [XmlElement("hometown_location")]
        public Location HomeTownLocation;

        [XmlElement("hs_info")]
        public HighSchoolInfo HighSchoolInfo;

        [XmlElement("interests")]
        public string Interests;

        [XmlElement("is_app_user")]
        public string is_app_user;

        public bool IsAppUser
        {
            get
            {
                return Utils.StrToBool(is_app_user, false);
            }
        }

        [XmlElement("last_name")]
        public string LastName;

        [XmlElement("meeting_for")]
        public MeetingFor MeetingFor;

        [XmlElement("meeting_sex")]
        public MeetingSex MeetingSex;

        [XmlElement("movies")]
        public string Movies;

        [XmlElement("music")]
        public string Music;

        [XmlElement("name")]
        public string Name;

        [XmlElement("notes_count")]
        public string notes_count;

        [XmlIgnore()]
        public int NotesCount
        {
            get
            {
                return Utils.StrToInt(notes_count, -1);
            }
        }

        [XmlElement("pic")]
        public string Pic;

        [XmlIgnore()]
        public Uri PicUri
        {
            get { return new Uri(Pic); }
        }

        [XmlElement("pic_big")]
        public string PicBig;

        [XmlIgnore()]
        public Uri PicBigUri
        {
            get { return new Uri(PicBig); }
        }

        [XmlElement("pic_small")]
        public string PicSmall;

        [XmlIgnore()]
        public Uri PicSmallUri
        {
            get { return new Uri(PicSmall); }
        }

        [XmlElement("political")]
        public string Political;

        [XmlElement("profile_update_time")]
        public long ProfileUpdateTime;

        [XmlElement("quotes")]
        public string Quotes;

        [XmlElement("relationship_status")]
        public string RelationshipStatus;

        [XmlElement("religion")]
        public string Religion;

        [XmlElement("sex")]
        public string Sex;

        [XmlElement("significant_other_id")]
        public string significant_other_id;

        [XmlIgnore()]
        public long SignificantOtherId
        {
            get
            {
                return Utils.StrToInt(significant_other_id, -1);
            }
        }

        [XmlElement("status")]
        public Status Status;

        [XmlElement("timezone")]
        public string timezone;

        public int TimeZone
        {
            get
            {
                return Utils.StrToInt(timezone, -1);
            }
        }

        [XmlElement("tv")]
        public string Tv;

        [XmlElement("wall_count")]
        public string wall_count;

        [XmlIgnore()]
        public int WallCount
        {
            get
            {
                return Utils.StrToInt(wall_count, -1);
            }
        }

        [XmlElement("work_history")]
        public WorkHistory WorkHistory;
 */
        [XmlElement("uid", IsNullable=false)]
        public int Uid;	//用户uid

        [XmlElement("user_name", IsNullable = false)]
        public string UserName;	//用户名

        [XmlElement("nick_name", IsNullable = false)]
        public string NickName;	//昵称

        [XmlElement("password", IsNullable = false)]
        public string Password;	//用户密码

        [XmlElement("space_id", IsNullable = false)]
        public int SpaceId; //个人空间ID,0为用户还未申请空间;负数是用户已经申请,等待管理员开通,绝对值为开通以后的真实Spaceid;正数是用户已经开通的Spaceid

        [XmlElement("secques", IsNullable = false)]
        public string Secques;	//用户安全提问码

        [XmlElement("gender", IsNullable = false)]
        public int Gender;	//性别

        [XmlElement("admin_id", IsNullable = false)]
        public int Adminid;	//管理组ID

        [XmlElement("group_id", IsNullable = false)]
        public int GroupId;	//用户组ID

        [XmlElement("group_expiry", IsNullable = false)]
        public int GroupExpiry;	//组过期时间

        [XmlElement("ext_groupids", IsNullable = false)]
        public string ExtGroupids;	//扩展用户组

        [XmlElement("reg_ip", IsNullable = false)]
        public string RegIp;	//注册IP

        [XmlElement("join_date", IsNullable = false)]
        public string JoinDate;	//注册时间

        [XmlElement("last_ip", IsNullable = false)]
        public string LastIp;	//上次登录IP

        [XmlElement("last_visit", IsNullable = false)]
        public string LastVisit;	//上次访问时间

        [XmlElement("last_activity", IsNullable = false)]
        public string LastActivity;	//最后活动时间

        [XmlElement("last_post", IsNullable = false)]
        public string LastPost;	//最后发贴时间

        [XmlElement("last_post_id", IsNullable = false)]
        public int LastPostid;	//最后发贴id

        [XmlElement("last_post_title", IsNullable = false)]
        public string LastPostTitle;	//最后发贴标题

        [XmlElement("post_count", IsNullable = false)]
        public int Posts;	//发贴数

        [XmlElement("digest_post_count", IsNullable = false)]
        public int DigestPosts;	//精华贴数

        [XmlElement("online_time", IsNullable = false)]
        public int OnlineTime;	//在线时间

        [XmlElement("page_view_count", IsNullable = false)]
        public int PageViews;	//页面浏览量

        [XmlElement("credits", IsNullable = false)]
        public int Credits;	//积分数

        [XmlElement("ext_credits_1", IsNullable = false)]
        public float ExtCredits1;	//扩展积分1

        [XmlElement("ext_credits_2", IsNullable = false)]
        public float ExtCredits2;	//扩展积分2

        [XmlElement("ext_credits_3", IsNullable = false)]
        public float ExtCredits3;	//扩展积分3

        [XmlElement("ext_credits_4", IsNullable = false)]
        public float ExtCredits4;	//扩展积分4

        [XmlElement("ext_credits_5", IsNullable = false)]
        public float ExtCredits5;	//扩展积分5

        [XmlElement("ext_credits_6", IsNullable = false)]
        public float ExtCredits6;	//扩展积分6

        [XmlElement("ext_credits_7", IsNullable = false)]
        public float ExtCredits7;	//扩展积分7

        [XmlElement("ext_credits_8", IsNullable = false)]
        public float ExtCredits8;	//扩展积分8

        [XmlIgnore]
        public int AvatarShowId;	//头像ID

        [XmlElement("email", IsNullable = false)]
        public string Email;	//邮件地址

        [XmlElement("birthday", IsNullable = false)]
        public string Birthday;	//生日

        [XmlIgnore]
        public int SigStatus;	//签名

        [XmlElement("tpp", IsNullable = false)]
        public int Tpp;	//每页主题数

        [XmlElement("ppp", IsNullable = false)]
        public int Ppp;	//每页贴数

        [XmlElement("template_id", IsNullable = false)]
        public int Templateid;	//风格ID

        [XmlElement("pm_sound", IsNullable = false)]
        public int PmSound;	//短消息铃声

        [XmlElement("show_email", IsNullable = false)]
        public int ShowEmail;	//是否显示邮箱

        //[XmlElement("tv")]
        //public ReceivePMSettingType m_newsletter;	//是否接收论坛通知

        [XmlElement("invisible", IsNullable = false)]
        public int Invisible;	//是否隐身
        //private string m_timeoffset;	//时差

        [XmlElement("has_new_pm", IsNullable = false)]
        public int NewPm;	//是否有新消息

        [XmlElement("new_pm_count", IsNullable = false)]
        public int NewPmCount;	//新短消息数量

        [XmlElement("access_masks", IsNullable = false)]
        public int AccessMasks;	//是否使用特殊权限

        [XmlElement("online_state", IsNullable = false)]
        public int OnlineState;	//在线状态, 1为在线, 0为不在线





        [XmlElement("web_site", IsNullable = false)]
        public string WebSite;	//网站

        [XmlElement("icq", IsNullable = false)]
        public string Icq;	//icq号码

        [XmlElement("qq", IsNullable = false)]
        public string Qq;	//qq号码

        [XmlElement("yahoo", IsNullable = false)]
        public string Yahoo;	//yahoo messenger帐号

        [XmlElement("msn", IsNullable = false)]
        public string Msn;	//msn messenger帐号

        [XmlElement("skype", IsNullable = false)]
        public string Skype;	//skype帐号

        [XmlElement("location", IsNullable = false)]
        public string Location;	//来自

        [XmlElement("custom_status", IsNullable = false)]
        public string CustomStatus;	//自定义头衔

        [XmlElement("avatar", IsNullable = false)]
        public string Avatar;	//头像宽度

        [XmlElement("avatar_width", IsNullable = false)]
        public int AvatarWidth;	//头像宽度

        [XmlElement("avatar_height", IsNullable = false)]
        public int AvatarHeight;	//头像高度

        [XmlElement("medals", IsNullable = false)]
        public string Medals; //勋章列表

        [XmlElement("about_me", IsNullable = false)]
        public string Bio;	//自我介绍

        [XmlIgnore]
        public string Signature;	//签名

        [XmlElement("signature", IsNullable = false)]
        public string Sightml;	//签名Html(自动转换得到)

        [XmlIgnore]
        public string AuthStr;	//验证码

        [XmlIgnore]
        public string AuthTime;	//验证码生成日期

        [XmlIgnore]
        public byte AuthFlag;	//验证码使用标志(0 未使用,1 用户邮箱验证及用户信息激活, 2 用户密码找回)

        [XmlElement("real_name", IsNullable = false)]
        public string RealName;  //用户实名

        [XmlElement("id_card", IsNullable = false)]
        public string IdCard;    //用户身份证件号

        [XmlElement("mobile", IsNullable = false)]
        public string Mobile;    //用户移动电话

        [XmlElement("telephone", IsNullable = false)]
        public string Phone;     //用户固定电话

    }
}
