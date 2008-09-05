using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    public class UserLog1
    {
        public int Id;
        public string LogID;
        public string title;
        public string content;
        public DateTime creatTime;
        public int dateNum;
        public DateTime LogDateTime;
        public string usernum;
    }
    public class message
    {
        public string Mid;
        public string UserNum;
        public string Title;
        public string Content;
        public DateTime CreatTime;
        public DateTime Send_DateTime;
        public int SortType;
        public string Rec_UserNum;
        public int FileTF;
        public int LevelFlag;
    }

    /// <summary>
    /// 会员详细信息实体类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int Id;
        /// <summary>
        /// 会员编号
        /// </summary>
        public string UserNum;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName;
        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword;
        /// <summary>
        /// 会员昵称
        /// </summary>
        public string NickName;
        /// <summary>
        /// 会员真实姓名
        /// </summary>
        public string RealName;
        /// <summary>
        /// 是否是管理员，0为普通用户，1为管理员
        /// </summary>
        public int isAdmin;
        /// <summary>
        /// 会员所属于的会员组
        /// </summary>
        public string UserGroupNumber;
        /// <summary>
        /// 密码问题
        /// </summary>
        public string PassQuestion;
        /// <summary>
        /// 密码答案
        /// </summary>
        public string PassKey;
        /// <summary>
        /// 证件类型
        /// </summary>
        public string CertType;
        /// <summary>
        /// 证件号码
        /// </summary>
        public string CertNumber;
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email;
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile;
        /// <summary>
        /// 会员性别，1为男，2为女，0为不知道
        /// </summary>
        public int Sex;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime birthday;
        /// <summary>
        /// 用户签名
        /// </summary>
        public string Userinfo;
        /// <summary>
        /// 头像地址	
        /// </summary>
        public string UserFace;
        /// <summary>
        /// 头像宽高，100|120
        /// </summary>
        public string userFacesize;
        /// <summary>
        /// 是否结婚，0为保密，1为未，2为已结婚
        /// </summary>
        public int marriage;
        /// <summary>
        /// 积分
        /// </summary>
        public int iPoint;
        /// <summary>
        /// 金币
        /// </summary>
        public int gPoint;
        /// <summary>
        /// 魅力值
        /// </summary>
        public int cPoint;
        /// <summary>
        /// 人气值
        /// </summary>
        public int ePoint;
        /// <summary>
        /// 活跃值
        /// </summary>
        public int aPoint;
        /// <summary>
        /// 是否锁定，0为否，1为是
        /// </summary>
        public int isLock;
        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegTime;
        /// <summary>
        /// 最后登录后台日期
        /// </summary>
        public DateTime LastLoginTime;
        /// <summary>
        /// 用户在线时间，单位小时
        /// </summary>
        public int OnlineTime;
        /// <summary>
        /// 用户在线状态，1为在线，0为不在线
        /// </summary>
        public int OnlineTF;
        /// <summary>
        /// 用户登陆次数
        /// </summary>
        public int LoginNumber;
        /// <summary>
        /// 系统好友默认分组（1，我的好友，0|2，陌生人,0|3，黑名单，0 |）
        /// </summary>
        public string FriendClass;
        /// <summary>
        /// 后台同一IP最大登陆次数,0为不限制。
        /// </summary>
        public int LoginLimtNumber;
        /// <summary>
        /// 后台最后登陆IP
        /// </summary>
        public string LastIP;
        /// <summary>
        /// 频道编号ID
        /// </summary>
        public string SiteID;
        /// <summary>
        /// 
        /// </summary>
        public string Addfriend;
        /// <summary>
        /// 是否开放自己的资料
        /// </summary>
        public int isOpen;
        /// <summary>
        /// 稿酬数目
        /// </summary>
        public int ParmConstrNum;
        /// <summary>
        /// 是否通过实名认证
        /// </summary>
        public int isIDcard;
        /// <summary>
        /// 实名认证附件地址
        /// </summary>
        public string IDcardFiles;
        /// <summary>
        /// 添加好友验证设置0为不允许任何人把我列为好友  1为需要身份认证才能把我列为好友2为允许任何人把我列为好友
        /// </summary>
        public int Addfriendbs;
        /// <summary>
        /// 是否开启电子邮件验证
        /// </summary>
        public int EmailATF;
        /// <summary>
        /// 电子邮件验证码
        /// </summary>
        public string EmailCode;
        /// <summary>
        /// 是否开启手机验证
        /// </summary>
        public int isMobile;
        /// <summary>
        /// 是否绑定手机
        /// </summary>
        public int BindTF;
        /// <summary>
        /// 手机验证码
        /// </summary>
        public string MobileCode;
    }

    /// <summary>
    /// 站点会员参数实体类
    /// </summary>
    public class UserParam
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID;
        /// <summary>
        /// 会员注册默认会员组编号
        /// </summary>
        public string RegGroupNumber;
        public int ConstrTF;
        /// <summary>
        /// 是否允许注册：会员注册开启（1）或者关闭（0）
        /// </summary>
        public int RegTF;
        /// <summary>
        /// 会员登陆是否需要验证码。(1)需要，(0)不需要
        /// </summary>
        public int UserLoginCodeTF;
        /// <summary>
        /// 评论是否需要显示验证码：(1)需要，(0)不需要
        /// </summary>
        public int CommCodeTF;
        public int CommCheck;
        /// <summary>
        /// 是否开启群发功能：会员好友群发功能(1)开启，(0)不开启
        /// </summary>
        public int SendMessageTF;
        /// <summary>
        /// 是否允许匿名评论：(1)开启，(0)不开启
        /// </summary>
        public int UnRegCommTF;
        /// <summary>
        /// 评论是否需要加载html编辑器. (1)开启，(0)不开启
        /// </summary>
        public int CommHTMLLoad;
        /// <summary>
        /// 评论过滤字符：用|分开。如：妈的|奶奶的|fuck
        /// </summary>
        public string Commfiltrchar;
        /// <summary>
        /// 会员IP登陆限制：IP每行一个
        /// </summary>
        public string IPLimt;
        /// <summary>
        /// 会员G币单位
        /// </summary>
        public string GpointName;
        /// <summary>
        /// 会员登陆多少次错误后锁定帐号|锁定时间.如：3|30表示登陆3次失败后锁定帐号不许再登陆，锁定帐号30分钟(最大2000分钟)
        /// </summary>
        public string LoginLock;
        public string LevelID;
        /// <summary>
        /// 注册协议
        /// </summary>
        public string RegContent;
        /// <summary>
        /// 10|0 会员注册获得的积分|G币
        /// </summary>
        public string setPoint;
        /// <summary>
        /// 会员注册选择项
        /// </summary>
        public string regItem;
        /// <summary>
        /// 注册是否需要电子邮件认证
        /// </summary>
        public int returnemail;
        /// <summary>
        /// 注册是否需要手机认证
        /// </summary>
        public int returnmobile;
        /// <summary>
        /// 在线支付ISP，0为支付宝，1为网银,2为云网，3为其他
        /// </summary>
        public int onpayType;
        /// <summary>
        /// 帐号（电子邮件等）
        /// </summary>
        public string o_userName;
        /// <summary>
        /// 密码
        /// </summary>
        public string o_key;
        /// <summary>
        /// 发送对方接收页面
        /// </summary>
        public string o_sendurl;
        /// <summary>
        /// 返回结果，接收页面
        /// </summary>
        public string o_returnurl;
        /// <summary>
        /// MD5校对。可选择参数
        /// </summary>
        public string o_md5;
        /// <summary>
        /// 其他参数1
        /// </summary>
        public string o_other1;
        /// <summary>
        /// 其他参数2
        /// </summary>
        public string o_other2;
        /// <summary>
        /// 其他参数3
        /// </summary>
        public string o_other3;
        public int GhClass;
        /// <summary>
        /// 魅力值增加  1(登陆)|1（发表评论，投票，讨论组，投稿）|1(扩展1)|1(扩展2|1(扩展3)
        /// </summary>
        public string cPointParam;
        /// <summary>
        /// 活跃值增加  1(登陆)| 1（发表评论，投票，讨论组，投稿）|1(扩展1)|1(扩展2|1(扩展3)
        /// </summary>
        public string aPointparam;
        /// <summary>
        /// 站点编号
        /// </summary>
        public string SiteID;
    }

    /// <summary>
    /// 会员附件实体类
    /// </summary>
    public class UserFields
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userNum;
        /// <summary>
        /// 省份
        /// </summary>
        public string province;
        /// <summary>
        /// 城市
        /// </summary>
        public string City;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address;
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Postcode;
        /// <summary>
        /// 家庭联系电话
        /// </summary>
        public string FaTel;
        /// <summary>
        /// 工作单位联系电话
        /// </summary>
        public string WorkTel;
        /// <summary>
        /// 多个QQ用|分开
        /// </summary>
        public string QQ;
        /// <summary>
        /// MSN号，只能使用一个MSN
        /// </summary>
        public string MSN;
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax;
        /// <summary>
        /// 性格
        /// </summary>
        public string character;
        /// <summary>
        /// 用户爱好
        /// </summary>
        public string UserFan;
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation;
        /// <summary>
        /// 籍贯
        /// </summary>
        public string nativeplace;
        /// <summary>
        /// 职业
        /// </summary>
        public string Job;
        /// <summary>
        /// 学历，如：本科，研究生
        /// </summary>
        public string education;
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string Lastschool;
        /// <summary>
        /// 组织关系，如：党员，团员等
        /// </summary>
        public string orgSch;
    }

    /// <summary>
    /// 会员组实体类
    /// </summary>
    public class UserGroup
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int Id;
        /// <summary>
        /// 编号
        /// </summary>
        public string GroupNumber;
        /// <summary>
        /// 会员组名
        /// </summary>
        public string GroupName;
        /// <summary>
        /// 达到此组需要的积分
        /// </summary>
        public int iPoint;
        /// <summary>
        /// 达到此组需要的G币
        /// </summary>
        public int Gpoint;
        /// <summary>
        /// 有效期限：0-5000的正整数
        /// </summary>
        public int Rtime;
        /// <summary>
        /// 购买东西获得的折扣
        /// </summary>
        public double Discount;
        /// <summary>
        /// 评论内容字数限制。默认为500字符，最大3000字符。范围:0~3000
        /// </summary>
        public int LenCommContent;
        /// <summary>
        /// 评论是否需要审核. (1)是，(0)不是
        /// </summary>
        public int CommCheckTF;
        /// <summary>
        /// 发表评论间隔时间：单位秒。范围：0~600
        /// </summary>
        public int PostCommTime;
        /// <summary>
        /// 允许上传格式：gif|jpg
        /// </summary>
        public string upfileType;
        /// <summary>
        /// 上传文件个数：0-500个整数，0表示不限制
        /// </summary>
        public int upfileNum;
        /// <summary>
        /// 单个文件大小：0-10000的正整数，单位kb
        /// </summary>
        public int upfileSize;
        /// <summary>
        /// 每天最大上传数：0-500个整数，0表示不限制
        /// </summary>
        public int DayUpfilenum;
        /// <summary>
        /// 最多允许投稿数：0-10000,0表示不限制
        /// </summary>
        public int ContrNum;
        /// <summary>
        /// 是否允许创建讨论组：是(1)，否(0)
        /// </summary>
        public int DicussTF;
        /// <summary>
        /// 是否允许发表主题：是(1)，否(0)
        /// </summary>
        public int PostTitle;
        /// <summary>
        /// 允许查看其他会员资料：是(1)，否(0)
        /// </summary>
        public int ReadUser;
        /// <summary>
        /// 允许的最大发送短消息数：0-1000的正整数
        /// </summary>
        public int MessageNum;
        /// <summary>
        /// 是否支持群发消息，1|5,表示：1表示可以群发，5表示可以给多少个人群发
        /// </summary>
        public string MessageGroupNum;
        /// <summary>
        /// 注册是否需要实名认证：是(1)，否(0)
        /// </summary>
        public int IsCert;
        /// <summary>
        /// 允许设置签名：是(1)，否(0)
        /// </summary>
        public int CharTF;
        /// <summary>
        /// 签名允许使用html语法：是(1)，否(0)
        /// </summary>
        public int CharHTML;
        /// <summary>
        /// 签名最大长度：0-1000的正整数
        /// </summary>
        public int CharLenContent;
        /// <summary>
        /// 注册多少分钟后允许发言：0-1000的正整数
        /// </summary>
        public int RegMinute;
        /// <summary>
        /// 发言是否允许HTML编辑器：是(1)，否(0)
        /// </summary>
        public int PostTitleHTML;
        /// <summary>
        /// 可以删除自己的主题：是(1)，否(0)
        /// </summary>
        public int DelSelfTitle;
        /// <summary>
        /// 删除其他人的帖子：是(1)，否(0)
        /// </summary>
        public int DelOTitle;
        /// <summary>
        /// 可以编辑自己的发言：是(1)，否(0)
        /// </summary>
        public int EditSelfTitle;
        /// <summary>
        /// 可以编辑他人帖子：是(1)，否(0)
        /// </summary>
        public int EditOtitle;
        /// <summary>
        /// 允许浏览发言：是(1)，否(0)
        /// </summary>
        public int ReadTitle;
        /// <summary>
        /// 可以移动自己的帖子：是(1)，否(0)
        /// </summary>
        public int MoveSelfTitle;
        /// <summary>
        /// 可以移动他人帖子：是(1)，否(0)
        /// </summary>
        public int MoveOTitle;
        /// <summary>
        /// 可以解固/固顶帖子：是(1)，否(0)
        /// </summary>
        public int TopTitle;
        /// <summary>
        /// 精华帖子操作：是(1)，否(0)
        /// </summary>
        public int GoodTitle;
        /// <summary>
        /// 可以锁定用户：是(1)，否(0)
        /// </summary>
        public int LockUser;
        /// <summary>
        /// 用户标识：可以选择颜色，粗体等；如：左HTML|右HTML
        /// </summary>
        public string UserFlag;
        /// <summary>
        /// 可以审核主题：是(1)，否(0)
        /// </summary>
        public int CheckTtile;
        /// <summary>
        /// 可以限制IP访问：是(1)，否(0)
        /// </summary>
        public int IPTF;
        /// <summary>
        /// 可以对独立用户进行奖励/惩罚：是(1)，否(0)
        /// </summary>
        public int EncUser;
        /// <summary>
        /// 可以打开/关闭其它人帖子：是(1)，否(0)
        /// </summary>
        public int OCTF;
        /// <summary>
        /// 允许用户选择风格：是(1)，否(0)
        /// </summary>
        public int StyleTF;
        /// <summary>
        /// 会员上传头像最大允许：单位kb,默认为50kb
        /// </summary>
        public int UpfaceSize;
        /// <summary>
        /// 是否允许积分兑换金币/金币兑换积分.0|1，0表示不允许，1表示允许
        /// </summary>
        public string GIChange;
        /// <summary>
        /// 兑换比例。1000|1/10000  1000表示1000积分兑换1G，1/10000表示1个G兑换10000积分
        /// </summary>
        public string GTChageRate;
        /// <summary>
        /// 登陆时候获得的积分|G币，格式：1|0
        /// </summary>
        public string LoginPoint;
        public string RegPoint;
        /// <summary>
        /// 是否允许创建社群：是(1)，否(0)
        /// </summary>
        public int GroupTF;
        /// <summary>
        /// 社群空间大小:默认2M
        /// </summary>
        public int GroupSize;
        /// <summary>
        /// 社群最大允许人数
        /// </summary>
        public int GroupPerNum;
        /// <summary>
        /// 允许最大建立数量
        /// </summary>
        public int GroupCreatNum;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatTime;
        /// <summary>
        /// 频道
        /// </summary>
        public string SiteID;
    }

    /// <summary>
    /// 会员冲值记录实体类
    /// </summary>
    public class UserGhistory
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int Id;
        /// <summary>
        /// 12位随机编号
        /// </summary>
        public string GhID;
        /// <summary>
        /// 1表示增加（收入），0表示减小（支出）
        /// </summary>
        public int ghtype;
        /// <summary>
        /// G币数
        /// </summary>
        public int Gpoint;
        /// <summary>
        /// 积分数
        /// </summary>
        public int iPoint;
        /// <summary>
        /// 现金数量
        /// </summary>
        public double Money;
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatTime;
        /// <summary>
        /// 用户编号
        /// </summary>
        public string userNum;
        /// <summary>
        /// 类型1捐献，2在线充值，3 积分兑换，4稿酬，5阅读权限，6登录获得，7注册获得
        /// </summary>
        public int gtype;
        /// <summary>
        /// 说明
        /// </summary>
        public string content;
        /// <summary>
        /// 站点编号
        /// </summary>
        public string SiteID;
    }
}
