using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    [Serializable]
    //栏目增加/修改构造函数
    public class ClassContent
    {
        public int Id;
        public string ClassID;
        public string ClassCName;
        public string ClassEName;
        public string ParentID;
        public int IsURL;
        public int OrderID;
        public string URLaddress;
        public string Domain;
        public string ClassTemplet;
        public string ReadNewsTemplet;
        public string SavePath;
        public string SaveClassframe;
        public int Checkint;
        public string ClassSaveRule;
        public string ClassIndexRule;
        public string NewsSavePath;
        public string NewsFileRule;
        public string PicDirPath;
        public string FileName;
        public int ContentPicTF;
        public string ContentPICurl;
        public string ContentPicSize;
        public int InHitoryDay;
        public string SiteID;
        public int NaviShowtf;
        public string NaviPIC;
        public string NaviContent;
        public string MetaKeywords;
        public string MetaDescript;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber;
        public int isLock;
        public int isRecyle;
        public string NaviPosition;
        public string NewsPosition;
        public int isComm;
        public string Defineworkey;
        public DateTime CreatTime;
    }

    public class PageContent
    {
        public int Id;
        public string ClassID;
        public string ClassCName;
        public string ClassEName;
        public string ParentID;
        public int IsURL;
        public int OrderID;
        public string ClassTemplet;
        public string SavePath;
        public string SiteID;
        public int NaviShowtf;
        public string MetaKeywords;
        public string MetaDescript;
        public int isDelPoint;
        public int Gpoint;
        public int iPoint;
        public string GroupNumber;
        public DateTime CreatTime;
        public int isPage;
        public string Content;
    }

    public class NewsContent
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID;
        /// <summary>
        /// 新闻唯一编号。12位随机数
        /// </summary>
        public string NewsID;
        /// <summary>
        /// 新闻类型,0普通，1图片，2标题
        /// </summary>
        public int NewsType;
        /// <summary>
        /// 新闻权重。1-50的数字。0为置顶。数字越小，权重越高
        /// </summary>
        public int OrderID;
        /// <summary>
        /// 标题
        /// </summary>
        public string NewsTitle;
        /// <summary>
        /// 副标题
        /// </summary>
        public string sNewsTitle;
        /// <summary>
        /// 标题颜色
        /// </summary>
        public string TitleColor;
        /// <summary>
        /// 是否斜体，1是，0否
        /// </summary>
        public int TitleITF;
        /// <summary>
        /// 是否粗体，1是，0否
        /// </summary>
        public int TitleBTF;
        /// <summary>
        /// 评论连接，1是，0否
        /// </summary>
        public int CommLinkTF;

        /// <summary>
        /// 是否有子新闻
        /// </summary>
        public int SubNewsTF;
        /// <summary>
        /// 连接地址
        /// </summary>
        public string URLaddress;
        /// <summary>
        /// 图片地址(大)
        /// </summary>
        public string PicURL;
        /// <summary>
        /// 图片地址(小)
        /// </summary>
        public string SPicURL;
        /// <summary>
        /// 所属栏目
        /// </summary>
        public string ClassID;
        /// <summary>
        /// 所属专题
        /// </summary>
        public string SpecialID;
        /// <summary>
        /// 作者
        /// </summary>
        public string Author;
        /// <summary>
        /// 来源
        /// </summary>
        public string Souce;
        /// <summary>
        /// Tags,多个用”，”分开
        /// </summary>
        public string Tags;
        /// <summary>
        /// 新闻属性推荐,滚动,热点,幻灯,头条(头条可以直接生成图片头条),公告,WAP,精彩格式如:0,1,1,0,1,0,0,1
        /// </summary>
        public string NewsProperty;


        /// <summary>
        /// 是否是图片头条
        /// </summary>
        public int NewsPicTopline;
        /// <summary>
        /// 模板
        /// </summary>
        public string Templet;
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content;
        /// <summary>
        /// 点击
        /// </summary>
        public int Click;
        /// <summary>
        /// META关键字
        /// </summary>
        public string Metakeywords;
        /// <summary>
        /// META描述
        /// </summary>
        public string Metadesc;
        /// <summary>
        /// 导航内容
        /// </summary>
        public string naviContent;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreatTime;
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName;
        
        /// <summary>
        /// 扩展名
        /// </summary>
        public string FileEXName;
        /// <summary>
        /// 如果频道有浏览权限.则有效.0 表示都可以查看,1扣出G,2扣除点数,3扣除G和点,4要达到G,5到点数,6要达到G和点
        /// </summary>
        public int isDelPoint;
        /// <summary>
        /// 浏览需要G币
        /// </summary>
        public int Gpoint;
        /// <summary>
        /// 浏览需要积分
        /// </summary>
        public int iPoint;
        /// <summary>
        /// 需要某个权限组才能查看.
        /// </summary>
        public string GroupNumber;
        /// <summary>
        /// 是否设置画中画
        /// </summary>
        public int ContentPicTF;
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ContentPicURL;
        /// <summary>
        /// 图片高|宽，如：100|800
        /// </summary>
        public string ContentPicSize;
        /// <summary>
        /// 是否允许评论，1是，0否
        /// </summary>
        public int CommTF;
        /// <summary>
        /// 允许把创建讨论组：0否，1是
        /// </summary>
        public int DiscussTF;
 
        /// <summary>
        /// 被网友顶数量
        /// </summary>
        public int TopNum;
        /// <summary>
        /// 允许投票 ：0否，1是
        /// </summary>
        public int VoteTF;
        /// <summary>
        /// 1|2|3第一个数组表示：1级审核状态，第2个表示2级审核状态，第3个表示3级审核状态
        /// </summary>
        public string CheckStat;
        /// <summary>
        /// 是否锁定，0否，1是
        /// </summary>
        public int isLock;
        /// <summary>
        /// 是否在回收站中。 
        /// </summary>
        public int isRecyle;
        /// <summary>
        /// 新闻所属的频道
        /// </summary>
        public string SiteID;
        /// <summary>
        /// 新闻使用数据库表
        /// </summary>
        public string DataLib;
        /// <summary>
        /// 此新闻所属的自定义数据编号
        /// </summary>
        public int DefineID;
        /// <summary>
        /// 是否有投票,1是，0否
        /// </summary>
        public int isVoteTF;
        /// <summary>
        /// 编辑
        /// </summary>
        public string Editor;
        /// <summary>
        /// 是否已生成静态文件
        /// </summary>
        public int isHtml;
        /// <summary>
        /// 是否投稿
        /// </summary>
        public int isConstr;
        /// <summary>
        /// 是否有附件
        /// </summary>
        public int isFiles;
        /// <summary>
        /// 视频地址
        /// </summary>
        public string vURL;
    }
    
    public class ChContentParam
    {
        /// <summary>
        /// 自动编号
        /// </summary>
        public int ID;
        /// <summary>
        /// 新闻权重。1-50的数字。0为置顶。数字越小，权重越高
        /// </summary>
        public int OrderID;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title;
        /// <summary>
        /// 标题颜色
        /// </summary>
        public string TitleColor;
        /// <summary>
        /// 是否斜体，1是，0否
        /// </summary>
        public int TitleITF;
        /// <summary>
        /// 是否粗体，1是，0否
        /// </summary>
        public int TitleBTF;
        /// <summary>
        /// 图片地址(大)
        /// </summary>
        public string PicURL;
        /// <summary>
        /// 所属栏目
        /// </summary>
        public int ClassID;
        /// <summary>
        /// 所属专题
        /// </summary>
        public string SpecialID;
        /// <summary>
        /// 作者
        /// </summary>
        public string Author;
        /// <summary>
        /// 来源
        /// </summary>
        public string Souce;
        /// <summary>
        /// Tags,多个用”，”分开
        /// </summary>
        public string Tags;
        /// <summary>
        /// 新闻属性推荐,滚动,热点,幻灯,头条(头条可以直接生成图片头条),公告,WAP,精彩格式如:0,1,1,0,1,0,0,1
        /// </summary>
        public string ContentProperty;

        /// <summary>
        /// 模板
        /// </summary>
        public string Templet;
        /// <summary>
        /// 新闻内容
        /// </summary>
        public string Content;
        /// <summary>
        /// 点击
        /// </summary>
        public int Click;
        /// <summary>
        /// META关键字
        /// </summary>
        public string Metakeywords;
        /// <summary>
        /// META描述
        /// </summary>
        public string Metadesc;
        /// <summary>
        /// 导航内容
        /// </summary>
        public string naviContent;
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreatTime;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath;
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName;

        /// <summary>
        /// 如果频道有浏览权限.则有效.0 表示都可以查看,1扣出G,2扣除点数,3扣除G和点,4要达到G,5到点数,6要达到G和点
        /// </summary>
        public int isDelPoint;
        /// <summary>
        /// 浏览需要G币
        /// </summary>
        public int Gpoint;
        /// <summary>
        /// 浏览需要积分
        /// </summary>
        public int iPoint;
        /// <summary>
        /// 需要某个权限组才能查看.
        /// </summary>
        public string GroupNumber;
        /// <summary>
        /// 是否锁定，0否，1是
        /// </summary>
        public int isLock;
        /// <summary>
        /// 频道
        /// </summary>
        public int ChID;
        /// <summary>
        /// 编辑
        /// </summary>
        public string Editor;
        /// <summary>
        /// 是否已生成静态文件
        /// </summary>
        public int isHtml;
        /// <summary>
        /// 是否投稿
        /// </summary>
        public int isConstr;
    }

    public class NewsContentTT
    {
        public int Id;
        public int NewsTF;
        public string NewsID;
        public string DataLib;
        public DateTime Creattime;
        public string tl_font;
        public int tl_size;
        public int tl_style;
        public string tl_color;
        public int tl_space;
        public string tl_PicColor;
        public string tl_SavePath;
        public string tl_Title;
        public int tl_Width;
        public string SiteID;
    }
    public class VoteContent
    {
        public string voteNum;
        public string voteTitle;
        public string voteContent;
        public DateTime creattime;
        public int ismTF;
        public int isMember;
        public string NewsID;
        public string SiteID;
        public string DataLib;
        public DateTime isTimeOutTime;
    }
}
