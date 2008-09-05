use [DO_NET_CMS]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_publish_CHupdateishtml]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [fs_publish_CHupdateishtml]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_publish_updateishtml]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [fs_publish_updateishtml]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_News_URL]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_News_URL]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_User_URL]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_User_URL]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_User_URLClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_User_URLClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_ads]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_ads]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_ads_class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_ads_class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_ads_stat]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_ads_stat]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_adstxt]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_adstxt]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_api_commentary]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_api_commentary]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_api_faviate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_api_faviate]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_api_navi]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_api_navi]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_api_pop]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_api_pop]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_api_qmenu]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_api_qmenu]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_customform]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_customform]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_customform_item]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_customform_item]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_define_class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_define_class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_define_data]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_define_data]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_define_save]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_define_save]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_friend_class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_friend_class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_friend_link]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_friend_link]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_friend_pram]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_friend_pram]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_Class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_Class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_Gen]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_Gen]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_JS]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_JS]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_JSFile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_JSFile]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_JST_Class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_JST_Class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_JSTemplet]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_JSTemplet]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_page]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_page]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_site]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_site]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_special]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_special]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_sub]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_sub]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_topline]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_topline]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_unNews]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_unNews]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_news_vote]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_news_vote]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_old_news]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_old_news]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_special_news]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_special_news]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_stat_Info]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_stat_Info]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_stat_class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_stat_class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_stat_content]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_stat_content]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_stat_param]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_stat_param]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_City]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_City]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_FieldClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_FieldClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_FieldData]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_FieldData]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_Label]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_Label]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_LabelClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_LabelClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_LabelFree]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_LabelFree]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_LabelStyle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_LabelStyle]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_PSF]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_PSF]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_PramUser]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_PramUser]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_Pramother]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_Pramother]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_Province]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_Province]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_SiteTask]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_SiteTask]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_User]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_User]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_UserLevel]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_UserLevel]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_admin]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_admin]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_admingroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_admingroup]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channel]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channel]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channelclass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channelclass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channellabel]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channellabel]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channellabelclass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channellabelclass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channelspecial]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channelspecial]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channelstyle]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channelstyle]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channelstyleclass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channelstyleclass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_channelvalue]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_channelvalue]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_logs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_logs]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_newsIndex]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_newsIndex]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_param]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_param]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_parmConstr]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_parmConstr]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_parmPrint]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_parmPrint]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_styleclass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_styleclass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_userfields]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_userfields]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_sys_userother]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_sys_userother]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Card]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Card]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Constr]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Constr]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_ConstrClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_ConstrClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Discuss]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Discuss]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_DiscussActive]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_DiscussActive]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_DiscussActiveMember]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_DiscussActiveMember]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_DiscussClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_DiscussClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_DiscussContribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_DiscussContribute]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_DiscussMember]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_DiscussMember]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_DiscussTopic]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_DiscussTopic]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Friend]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Friend]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_FriendClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_FriendClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Ghistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Ghistory]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Group]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Group]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Guser]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Guser]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_MessFiles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_MessFiles]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Message]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Message]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Photo]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Photo]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Photoalbum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Photoalbum]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_PhotoalbumClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_PhotoalbumClass]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_Requestinformation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_Requestinformation]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_constrPay]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_constrPay]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_news]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_news]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_note]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_note]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_userlogs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_userlogs]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_user_vote]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_user_vote]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_vote_Item]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_vote_Item]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_vote_Steps]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_vote_Steps]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_vote_class]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_vote_class]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_vote_manage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_vote_manage]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_vote_param]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_vote_param]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_vote_title]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_vote_title]
;

CREATE TABLE [fs_News_URL] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[URLName] [nvarchar] (50) NULL ,
	[NewsID] [nvarchar] (15) NULL ,
	[DataLib] [nvarchar] (30) NULL ,
	[FileURL] [nvarchar] (220) NULL ,
	[CreatTime] [datetime] NULL ,
	[OrderID] [tinyint] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_User_URL] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [int] NULL ,
	[URLName] [nvarchar] (100) NULL ,
	[URL] [nvarchar] (200) NULL ,
	[URLColor] [nvarchar] (10) NULL ,
	[CreatTime] [datetime] NULL ,
	[Content] [nvarchar] (200) NULL ,
	[UserNum] [nvarchar] (15) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_User_URLClass] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassName] [nvarchar] (50) NULL ,
	[ParentID] [int] NULL ,
	[UserNum] [nvarchar] (15) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_ads] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AdID] [nvarchar] (15) NULL ,
	[adName] [nvarchar] (50) NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[CusID] [nvarchar] (12) NULL ,
	[adType] [tinyint] NULL ,
	[leftPic] [nvarchar] (200) NULL ,
	[rightPic] [nvarchar] (200) NULL ,
	[leftSize] [nvarchar] (12) NULL ,
	[rightSize] [nvarchar] (12) NULL ,
	[LinkURL] [nvarchar] (200) NULL ,
	[CycTF] [tinyint] NULL ,
	[CycAdID] [nvarchar] (15) NULL ,
	[CycSpeed] [int] NULL ,
	[CycDic] [tinyint] NULL ,
	[ClickNum] [int] NULL ,
	[ShowNum] [int] NULL ,
	[CondiTF] [tinyint] NULL ,
	[maxShowClick] [int] NULL ,
	[TimeOutDay] [datetime] NULL ,
	[maxClick] [int] NULL ,
	[creatTime] [datetime] NULL ,
	[AdTxtNum] [int] NULL ,
	[isLock] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_ads_class] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AcID] [nvarchar] (12) NULL ,
	[Cname] [nvarchar] (50) NULL ,
	[ParentID] [nvarchar] (12) NULL ,
	[creatTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[Adprice] [int] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_ads_stat] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AdID] [nvarchar] (15) NULL ,
	[IP] [nvarchar] (20) NULL ,
	[Address] [nvarchar] (100) NULL ,
	[creatTime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_adstxt] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AdID] [nvarchar] (15) NULL ,
	[AdTxt] [nvarchar] (200) NULL ,
	[AdCss] [nvarchar] (20) NULL ,
	[AdLink] [nvarchar] (100) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_api_commentary] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Commid] [nvarchar] (12) NULL ,
	[InfoID] [nvarchar] (12) NULL ,
	[APIID] [nvarchar] (20) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[Title] [nvarchar] (200) NULL ,
	[Content] [nvarchar] (200) NULL ,
	[creatTime] [datetime] NULL ,
	[IP] [nvarchar] (20) NULL ,
	[QID] [nvarchar] (12) NULL ,
	[UserNum] [nvarchar] (20) NULL ,
	[isRecyle] [tinyint] NULL ,
	[islock] [tinyint] NULL ,
	[OrderID] [int] NULL ,
	[GoodTitle] [tinyint] NULL ,
	[isCheck] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[commtype] [tinyint] NULL ,
	[ChID] [int] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_api_faviate] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[FID] [nvarchar] (12) NULL ,
	[Infotitle] [nvarchar] (80) NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[CreatTime] [datetime] NULL ,
	[APIID] [nvarchar] (20) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[ChID] [int] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_api_navi] (
	[am_ID] [int] IDENTITY (1, 1) NOT NULL ,
	[api_IdentID] [nvarchar] (30) NULL ,
	[am_ClassID] [nvarchar] (12) NULL ,
	[Am_position] [nvarchar] (5) NULL ,
	[am_Name] [nvarchar] (20) NULL ,
	[Am_Ename] [nvarchar] (20) NULL ,
	[am_FilePath] [nvarchar] (200) NULL ,
	[am_target] [nvarchar] (20) NULL ,
	[am_ParentID] [nvarchar] (12) NULL ,
	[am_type] [tinyint] NULL ,
	[am_creatTime] [datetime] NULL ,
	[am_orderID] [int] NULL ,
	[isSys] [tinyint] NULL ,
	[siteID] [nvarchar] (12) NULL ,
	[userNum] [nvarchar] (15) NULL ,
	[popCode] [nvarchar] (50) NULL ,
	[mainURL] [nvarchar] (100) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_api_pop] (
	[pop_ID] [int] IDENTITY (1, 1) NOT NULL ,
	[api_IdentID] [nvarchar] (30) NOT NULL ,
	[Pop_classid] [int] NULL ,
	[pop_parentID] [int] NULL ,
	[pop_Name] [nvarchar] (50) NOT NULL ,
	[pop_PopID] [nvarchar] (10) NOT NULL ,
	[pop_Content] [nvarchar] (200) NULL ,
	[pop_Standby] [nvarchar] (200) NULL ,
	[Pop_addTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_api_qmenu] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[QmID] [nvarchar] (12) NULL ,
	[qName] [nvarchar] (50) NULL ,
	[FilePath] [nvarchar] (200) NULL ,
	[Ismanage] [tinyint] NULL ,
	[OrderID] [int] NULL ,
	[usernum] [nvarchar] (15) NULL ,
	[siteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_customform] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[formname] [nvarchar] (50) NOT NULL ,
	[formtablename] [nvarchar] (50) NOT NULL ,
	[accessorypath] [nvarchar] (100) NULL ,
	[accessorysize] [int] NULL ,
	[memo] [nvarchar] (255) NULL ,
	[islock] [bit] NOT NULL ,
	[timelimited] [bit] NOT NULL ,
	[starttime] [datetime] NULL ,
	[endtime] [datetime] NULL ,
	[showvalidatecode] [bit] NOT NULL ,
	[submitonce] [bit] NOT NULL ,
	[isdelpoint] [tinyint] NOT NULL ,
	[gpoint] [int] NOT NULL ,
	[ipoint] [int] NOT NULL ,
	[groupnumber] [ntext] NULL ,
	[addtime] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_customform_item] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[seriesnumber] [int] NOT NULL ,
	[formid] [int] NOT NULL ,
	[fieldname] [nvarchar] (50) NOT NULL ,
	[itemname] [nvarchar] (50) NOT NULL ,
	[itemtype] [nvarchar] (50) NOT NULL ,
	[defaultvalue] [nvarchar] (50) NULL ,
	[isnotnull] [bit] NOT NULL ,
	[itemsize] [int] NULL ,
	[islock] [bit] NOT NULL ,
	[prompt] [nvarchar] (255) NULL ,
	[selectitem] [ntext] NULL ,
	[addtime] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_define_class] (
	[DefineId] [int] IDENTITY (1, 1) NOT NULL ,
	[DefineInfoId] [nvarchar] (12) NOT NULL ,
	[DefineName] [nvarchar] (50) NOT NULL ,
	[ParentInfoId] [nvarchar] (12) NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_define_data] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[defineInfoId] [nvarchar] (12) NOT NULL ,
	[defineCname] [nvarchar] (50) NOT NULL ,
	[defineColumns] [nvarchar] (50) NULL ,
	[defineType] [int] NULL ,
	[IsNull] [tinyint] NULL ,
	[defineValue] [ntext] NULL ,
	[defineExpr] [nvarchar] (200) NULL ,
	[definedvalue] [nvarchar] (200) NULL ,
	[SiteID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_define_save] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[DsNewsID] [nvarchar] (12) NULL ,
	[DsEname] [nvarchar] (50) NULL ,
	[DsNewsTable] [nvarchar] (50) NULL ,
	[DsType] [tinyint] NULL ,
	[DsContent] [ntext] NULL ,
	[DsApiID] [nvarchar] (30) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_friend_class] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[ClassCName] [nvarchar] (50) NULL ,
	[ClassEName] [nvarchar] (50) NULL ,
	[Content] [ntext] NULL ,
	[ParentID] [nvarchar] (12) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_friend_link] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) NULL ,
	[Type] [tinyint] NULL ,
	[Url] [nvarchar] (250) NULL ,
	[Content] [ntext] NULL ,
	[PicUrl] [nvarchar] (250) NULL ,
	[Lock] [tinyint] NULL ,
	[IsUser] [tinyint] NULL ,
	[Author] [nvarchar] (50) NULL ,
	[Mail] [nvarchar] (150) NULL ,
	[ContentFor] [ntext] NULL ,
	[LinkContent] [ntext] NULL ,
	[Addtime] [datetime] NULL ,
	[isAdmin] [tinyint] NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_friend_pram] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[IsOpen] [tinyint] NULL ,
	[IsRegister] [tinyint] NULL ,
	[ArrSize] [nvarchar] (10) NULL ,
	[Content] [ntext] NULL ,
	[isLock] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[NewsID] [nvarchar] (12) NOT NULL ,
	[NewsType] [tinyint] NOT NULL ,
	[OrderID] [tinyint] NOT NULL ,
	[NewsTitle] [nvarchar] (100) NOT NULL ,
	[sNewsTitle] [nvarchar] (100) NULL ,
	[TitleColor] [nvarchar] (10) NULL ,
	[TitleITF] [tinyint] NOT NULL ,
	[TitleBTF] [tinyint] NULL ,
	[CommLinkTF] [tinyint] NULL ,
	[SubNewsTF] [tinyint] NULL ,
	[URLaddress] [nvarchar] (200) NULL ,
	[PicURL] [nvarchar] (200) NULL ,
	[SPicURL] [nvarchar] (200) NULL ,
	[ClassID] [nvarchar] (12) NOT NULL ,
	[SpecialID] [nvarchar] (12) NULL ,
	[Author] [nvarchar] (100) NULL ,
	[Souce] [nvarchar] (100) NULL ,
	[Tags] [nvarchar] (100) NULL ,
	[NewsProperty] [nvarchar] (30) NOT NULL ,
	[NewsPicTopline] [tinyint] NOT NULL ,
	[Templet] [nvarchar] (200) NULL ,
	[Content] [ntext] NULL ,
	[Metakeywords] [nvarchar] (200) NULL ,
	[Metadesc] [nvarchar] (200) NULL ,
	[naviContent] [nvarchar] (255) NULL ,
	[Click] [int] NOT NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[EditTime] [datetime] NULL ,
	[SavePath] [nvarchar] (200) NULL ,
	[FileName] [nvarchar] (100) NOT NULL ,
	[FileEXName] [nvarchar] (6) NOT NULL ,
	[isDelPoint] [tinyint] NOT NULL ,
	[Gpoint] [int] NOT NULL ,
	[iPoint] [int] NOT NULL ,
	[GroupNumber] [ntext] NULL ,
	[ContentPicTF] [tinyint] NOT NULL ,
	[ContentPicURL] [nvarchar] (200) NULL ,
	[ContentPicSize] [nvarchar] (10) NULL ,
	[CommTF] [tinyint] NOT NULL ,
	[DiscussTF] [tinyint] NOT NULL ,
	[TopNum] [int] NOT NULL ,
	[VoteTF] [tinyint] NOT NULL ,
	[CheckStat] [nvarchar] (10) NULL ,
	[isLock] [tinyint] NOT NULL ,
	[isRecyle] [tinyint] NOT NULL ,
	[SiteID] [nvarchar] (12) NOT NULL ,
	[DataLib] [nvarchar] (20) NOT NULL ,
	[DefineID] [tinyint] NULL ,
	[isVoteTF] [tinyint] NOT NULL ,
	[Editor] [nvarchar] (18) NULL ,
	[isHtml] [tinyint] NOT NULL ,
	[isConstr] [tinyint] NOT NULL ,
	[isFiles] [tinyint] NULL ,
	[vURL] [nvarchar] (200) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_Class] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [nvarchar] (12) NOT NULL ,
	[ClassCName] [nvarchar] (50) NOT NULL ,
	[ClassEName] [nvarchar] (50) NOT NULL ,
	[ParentID] [nvarchar] (12) NULL ,
	[IsURL] [tinyint] NULL ,
	[OrderID] [tinyint] NULL ,
	[URLaddress] [nvarchar] (200) NULL ,
	[Domain] [nvarchar] (150) NULL ,
	[ClassTemplet] [nvarchar] (200) NULL ,
	[ReadNewsTemplet] [nvarchar] (200) NULL ,
	[SavePath] [nvarchar] (50) NULL ,
	[SaveClassframe] [nvarchar] (200) NULL ,
	[Checkint] [tinyint] NULL ,
	[ClassSaveRule] [nvarchar] (200) NULL ,
	[ClassIndexRule] [nvarchar] (50) NULL ,
	[NewsSavePath] [nvarchar] (50) NULL ,
	[NewsFileRule] [nvarchar] (200) NULL ,
	[PicDirPath] [nvarchar] (150) NULL ,
	[ContentPicTF] [tinyint] NULL ,
	[ContentPICurl] [nvarchar] (200) NULL ,
	[ContentPicSize] [nvarchar] (15) NULL ,
	[InHitoryDay] [int] NULL ,
	[DataLib] [nvarchar] (20) NOT NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[NaviShowtf] [tinyint] NULL ,
	[NaviPIC] [nvarchar] (200) NULL ,
	[NaviContent] [nvarchar] (250) NULL ,
	[MetaKeywords] [nvarchar] (200) NULL ,
	[MetaDescript] [nvarchar] (200) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[GroupNumber] [nvarchar] (250) NULL ,
	[FileName] [nvarchar] (6) NULL ,
	[isLock] [tinyint] NULL ,
	[isRecyle] [tinyint] NULL ,
	[NaviPosition] [ntext] NULL ,
	[NewsPosition] [ntext] NULL ,
	[isComm] [tinyint] NULL ,
	[Defineworkey] [nvarchar] (220) NULL ,
	[CreatTime] [datetime] NULL ,
	[isPage] [tinyint] NULL ,
	[PageContent] [ntext] NULL ,
	[ModelID] [nvarchar] (12) NULL ,
	[isunHTML] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_Gen] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Cname] [nvarchar] (100) NULL ,
	[gType] [tinyint] NULL ,
	[URL] [nvarchar] (200) NULL ,
	[EmailURL] [nvarchar] (200) NULL ,
	[isLock] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_news_JS] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[JsID] [nvarchar] (12) NOT NULL ,
	[jsType] [tinyint] NOT NULL ,
	[JSName] [nvarchar] (50) NOT NULL ,
	[JsTempletID] [nvarchar] (12) NULL ,
	[jsNum] [int] NULL ,
	[jsLenTitle] [int] NULL ,
	[jsLenNavi] [int] NULL ,
	[jsLenContent] [int] NULL ,
	[jsContent] [ntext] NULL ,
	[SiteID] [nvarchar] (12) NOT NULL ,
	[jsColsNum] [int] NULL ,
	[CreatTime] [datetime] NULL ,
	[jsfilename] [nvarchar] (50) NULL ,
	[jssavepath] [nvarchar] (200) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_JSFile] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[JsID] [nvarchar] (12) NOT NULL ,
	[Njf_title] [nvarchar] (100) NOT NULL ,
	[NewsId] [nvarchar] (12) NOT NULL ,
	[NewsTable] [nvarchar] (20) NOT NULL ,
	[PicPath] [nvarchar] (200) NULL ,
	[ClassId] [nvarchar] (12) NULL ,
	[SiteID] [nvarchar] (12) NOT NULL ,
	[CreatTime] [datetime] NULL ,
	[TojsTime] [datetime] NULL ,
	[ReclyeTF] [tinyint] NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_news_JST_Class] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [nvarchar] (12) NOT NULL ,
	[CName] [nvarchar] (50) NOT NULL ,
	[ParentID] [nvarchar] (12) NOT NULL ,
	[Description] [nvarchar] (500) NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_news_JSTemplet] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[TempletID] [nvarchar] (12) NOT NULL ,
	[CName] [nvarchar] (50) NOT NULL ,
	[JSClassid] [nvarchar] (12) NOT NULL ,
	[JSTType] [tinyint] NOT NULL ,
	[JSTContent] [ntext] NOT NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_page] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[title] [nvarchar] (100) NULL ,
	[Content] [ntext] NULL ,
	[CreatTime] [datetime] NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[SavePath] [nvarchar] (100) NULL ,
	[fileName] [nvarchar] (50) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[GroupNumber] [ntext] NULL ,
	[KeyMeta] [nvarchar] (150) NULL ,
	[DescMeta] [nvarchar] (150) NULL ,
	[Editor] [nvarchar] (20) NULL ,
	[Templet] [nvarchar] (200) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_site] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChannelID] [nvarchar] (12) NOT NULL ,
	[ParentID] [nvarchar] (12) NULL ,
	[CName] [nvarchar] (50) NOT NULL ,
	[EName] [nvarchar] (50) NOT NULL ,
	[ChannCName] [nvarchar] (50) NOT NULL ,
	[IsURL] [tinyint] NOT NULL ,
	[Urladdress] [nvarchar] (200) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[IndexTemplet] [nvarchar] (200) NULL ,
	[ClassTemplet] [nvarchar] (200) NULL ,
	[ReadNewsTemplet] [nvarchar] (200) NULL ,
	[SpecialTemplet] [nvarchar] (200) NULL ,
	[isLock] [tinyint] NULL ,
	[Domain] [nvarchar] (100) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[GroupNumber] [ntext] NULL ,
	[isCheck] [tinyint] NULL ,
	[Keywords] [nvarchar] (100) NULL ,
	[Descript] [nvarchar] (200) NULL ,
	[ContrTF] [tinyint] NULL ,
	[ShowNaviTF] [tinyint] NULL ,
	[UpfileType] [nvarchar] (150) NULL ,
	[UpfileSize] [int] NULL ,
	[NaviContent] [nvarchar] (200) NULL ,
	[NaviPicURL] [nvarchar] (200) NULL ,
	[SaveType] [tinyint] NULL ,
	[PicSavePath] [nvarchar] (100) NULL ,
	[SaveFileType] [tinyint] NULL ,
	[SaveDirPath] [nvarchar] (100) NULL ,
	[SaveDirRule] [nvarchar] (200) NULL ,
	[SaveFileRule] [nvarchar] (100) NULL ,
	[NaviPosition] [ntext] NULL ,
	[IndexEXName] [nvarchar] (6) NULL ,
	[ClassEXName] [nvarchar] (6) NULL ,
	[NewsEXName] [nvarchar] (6) NULL ,
	[SpecialEXName] [nvarchar] (6) NULL ,
	[classRefeshNum] [int] NULL ,
	[infoRefeshNum] [int] NULL ,
	[DelNum] [int] NULL ,
	[SpecialNum] [int] NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[isRecyle] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_special] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[SpecialID] [nvarchar] (12) NULL ,
	[SpecialCName] [nvarchar] (50) NULL ,
	[specialEName] [nvarchar] (50) NULL ,
	[ParentID] [nvarchar] (12) NULL ,
	[Domain] [nvarchar] (100) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[GroupNumber] [ntext] NULL ,
	[saveDirPath] [nvarchar] (100) NULL ,
	[SavePath] [nvarchar] (100) NULL ,
	[FileName] [nvarchar] (100) NULL ,
	[FileEXName] [nvarchar] (6) NULL ,
	[NaviPicURL] [nvarchar] (200) NULL ,
	[NaviContent] [ntext] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[Templet] [nvarchar] (200) NULL ,
	[isLock] [tinyint] NULL ,
	[isRecyle] [tinyint] NULL ,
	[CreatTime] [datetime] NULL ,
	[NaviPosition] [ntext] NULL ,
	[ModelID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_news_sub] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[NewsID] [nvarchar] (12) NULL ,
	[getNewsID] [nvarchar] (12) NULL ,
	[NewsTitle] [nvarchar] (200) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[TitleCSS] [nvarchar] (30) NULL ,
	[colsNum] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[CreatTime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_news_topline] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[NewsTF] [tinyint] NULL ,
	[NewsID] [nvarchar] (12) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[tl_font] [nvarchar] (20) NULL ,
	[tl_style] [tinyint] NULL ,
	[tl_size] [tinyint] NULL ,
	[tl_color] [nvarchar] (8) NULL ,
	[tl_space] [tinyint] NULL ,
	[tl_PicColor] [nvarchar] (8) NULL ,
	[tl_SavePath] [nvarchar] (220) NULL ,
	[creattime] [datetime] NULL ,
	[tl_Title] [nvarchar] (150) NULL ,
	[tl_Width] [int] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_news_unNews] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[UnID] [nvarchar] (12) NOT NULL ,
	[unName] [nvarchar] (100) NULL ,
	[TitleCSS] [nvarchar] (50) NULL ,
	[SubCSS] [nvarchar] (50) NULL ,
	[ONewsID] [nvarchar] (12) NULL ,
	[Rows] [int] NULL ,
	[unTitle] [nvarchar] (200) NULL ,
	[NewsTable] [nvarchar] (20) NULL ,
	[CreatTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_news_vote] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[voteNum] [nvarchar] (20) NULL ,
	[NewsID] [nvarchar] (12) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[voteTitle] [nvarchar] (100) NULL ,
	[voteContent] [ntext] NULL ,
	[creattime] [datetime] NULL ,
	[ismTF] [tinyint] NULL ,
	[isMember] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[isTimeOutTime] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_old_news] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[NewsID] [nvarchar] (12) NOT NULL ,
	[NewsType] [tinyint] NOT NULL ,
	[OrderID] [tinyint] NOT NULL ,
	[NewsTitle] [nvarchar] (100) NOT NULL ,
	[sNewsTitle] [nvarchar] (100) NULL ,
	[TitleColor] [nvarchar] (10) NULL ,
	[TitleITF] [tinyint] NULL ,
	[TitleBTF] [tinyint] NULL ,
	[CommLinkTF] [tinyint] NULL ,
	[SubNewsTF] [tinyint] NULL ,
	[URLaddress] [nvarchar] (200) NULL ,
	[PicURL] [nvarchar] (200) NULL ,
	[SPicURL] [nvarchar] (200) NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[SpecialID] [nvarchar] (200) NULL ,
	[Author] [nvarchar] (100) NULL ,
	[Souce] [nvarchar] (100) NULL ,
	[Tags] [nvarchar] (100) NULL ,
	[NewsProperty] [nvarchar] (30) NULL ,
	[NewsPicTopline] [tinyint] NULL ,
	[Templet] [nvarchar] (200) NULL ,
	[Content] [ntext] NULL ,
	[Metakeywords] [nvarchar] (200) NULL ,
	[Metadesc] [nvarchar] (200) NULL ,
	[naviContent] [ntext] NULL ,
	[Click] [int] NULL ,
	[CreatTime] [datetime] NULL ,
	[EditTime] [datetime] NULL ,
	[SavePath] [nvarchar] (200) NULL ,
	[FileName] [nvarchar] (100) NULL ,
	[FileEXName] [nvarchar] (6) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[GroupNumber] [ntext] NULL ,
	[ContentPicTF] [tinyint] NULL ,
	[ContentPicURL] [nvarchar] (200) NULL ,
	[ContentPicSize] [nvarchar] (10) NULL ,
	[CommTF] [tinyint] NULL ,
	[DiscussTF] [tinyint] NULL ,
	[TopNum] [int] NULL ,
	[VoteTF] [tinyint] NULL ,
	[CheckStat] [nvarchar] (10) NULL ,
	[isLock] [tinyint] NULL ,
	[isRecyle] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[DataLib] [nvarchar] (20) NULL ,
	[DefineID] [nvarchar] (12) NULL ,
	[isVoteTF] [tinyint] NULL ,
	[Editor] [nvarchar] (18) NULL ,
	[isHtml] [tinyint] NULL ,
	[oldtime] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_special_news] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[SpecialID] [nvarchar] (20) NULL ,
	[NewsID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_stat_Info] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[vyear] [int] NULL ,
	[vmonth] [int] NULL ,
	[vday] [int] NULL ,
	[vhour] [int] NULL ,
	[vtime] [datetime] NULL ,
	[vweek] [int] NULL ,
	[vip] [nvarchar] (50) NULL ,
	[vwhere] [nvarchar] (250) NULL ,
	[vwheref] [nvarchar] (50) NULL ,
	[vcome] [nvarchar] (250) NULL ,
	[vpage] [nvarchar] (250) NULL ,
	[vsoft] [nvarchar] (50) NULL ,
	[vOS] [nvarchar] (50) NULL ,
	[vwidth] [int] NULL ,
	[classid] [nvarchar] (12) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_stat_class] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[statid] [nvarchar] (12) NULL ,
	[classname] [nvarchar] (20) NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_stat_content] (
	[today] [bigint] NULL ,
	[yesterday] [bigint] NULL ,
	[vdate] [datetime] NULL ,
	[vtop] [bigint] NULL ,
	[starttime] [datetime] NULL ,
	[vhigh] [bigint] NULL ,
	[vhightime] [datetime] NULL ,
	[classid] [nvarchar] (12) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_stat_param] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[SystemName] [nvarchar] (100) NULL ,
	[SystemNameE] [nvarchar] (150) NULL ,
	[ipCheck] [tinyint] NULL ,
	[isOnlinestat] [tinyint] NULL ,
	[ipTime] [int] NULL ,
	[pageNum] [int] NULL ,
	[cookies] [nvarchar] (30) NULL ,
	[pointNum] [int] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_City] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Cid] [nvarchar] (12) NOT NULL ,
	[orderID] [int] NULL ,
	[cityName] [nvarchar] (30) NULL ,
	[Pid] [nvarchar] (12) NULL ,
	[creatTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_FieldClass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Did] [nvarchar] (12) NULL ,
	[Classid] [nvarchar] (12) NULL ,
	[FieldName] [nvarchar] (100) NULL ,
	[FieldEname] [nvarchar] (100) NULL ,
	[FieldType] [tinyint] NULL ,
	[FieldContent] [ntext] NULL ,
	[isNull] [tinyint] NULL ,
	[isLock] [tinyint] NULL ,
	[CreatTIME] [datetime] NULL ,
	[APIID] [nvarchar] (20) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_FieldData] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[DdID] [nvarchar] (12) NULL ,
	[NewsID] [nvarchar] (12) NULL ,
	[FieldName] [nvarchar] (100) NULL ,
	[FieldContent] [ntext] NULL ,
	[CreatTIME] [datetime] NULL ,
	[APIID] [nvarchar] (20) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_Label] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[LabelID] [nvarchar] (12) NOT NULL ,
	[ClassID] [nvarchar] (12) NOT NULL ,
	[Label_Name] [nvarchar] (100) NOT NULL ,
	[Label_Content] [ntext] NULL ,
	[Description] [nvarchar] (200) NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[isBack] [tinyint] NOT NULL ,
	[isRecyle] [tinyint] NULL ,
	[isSys] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[isShare] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_LabelClass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [nvarchar] (12) NOT NULL ,
	[ClassName] [nvarchar] (30) NOT NULL ,
	[Content] [nvarchar] (200) NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[isRecyle] [tinyint] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_LabelFree] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[LabelID] [nvarchar] (12) NOT NULL ,
	[LabelName] [nvarchar] (30) NOT NULL ,
	[LabelSQL] [nvarchar] (1000) NOT NULL ,
	[StyleContent] [ntext] NOT NULL ,
	[Description] [nvarchar] (200) NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_LabelStyle] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[styleID] [nvarchar] (12) NOT NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[StyleName] [nvarchar] (30) NOT NULL ,
	[Content] [ntext] NOT NULL ,
	[Description] [nvarchar] (200) NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[isRecyle] [tinyint] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_PSF] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[psfID] [nvarchar] (12) NOT NULL ,
	[psfName] [nvarchar] (30) NOT NULL ,
	[LocalDir] [nvarchar] (200) NULL ,
	[RemoteDir] [nvarchar] (200) NULL ,
	[isAll] [tinyint] NULL ,
	[isSub] [tinyint] NOT NULL ,
	[CreatTime] [datetime] NULL ,
	[isRecyle] [tinyint] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_PramUser] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[RegGroupNumber] [nvarchar] (20) NULL ,
	[ConstrTF] [tinyint] NULL ,
	[RegTF] [tinyint] NULL ,
	[UserLoginCodeTF] [tinyint] NULL ,
	[CommCodeTF] [tinyint] NULL ,
	[CommCheck] [tinyint] NULL ,
	[SendMessageTF] [tinyint] NULL ,
	[UnRegCommTF] [tinyint] NULL ,
	[CommHTMLLoad] [tinyint] NULL ,
	[Commfiltrchar] [ntext] NULL ,
	[IPLimt] [ntext] NULL ,
	[GpointName] [nvarchar] (10) NULL ,
	[LoginLock] [nvarchar] (10) NULL ,
	[LevelID] [nvarchar] (20) NULL ,
	[RegContent] [ntext] NULL ,
	[setPoint] [nvarchar] (20) NULL ,
	[regItem] [ntext] NULL ,
	[returnemail] [tinyint] NULL ,
	[returnmobile] [tinyint] NULL ,
	[onpayType] [tinyint] NULL ,
	[o_userName] [nvarchar] (100) NULL ,
	[o_key] [nvarchar] (128) NULL ,
	[o_sendurl] [nvarchar] (220) NULL ,
	[o_returnurl] [nvarchar] (220) NULL ,
	[o_md5] [nvarchar] (128) NULL ,
	[o_other1] [nvarchar] (220) NULL ,
	[o_other2] [nvarchar] (220) NULL ,
	[o_other3] [nvarchar] (220) NULL ,
	[GhClass] [tinyint] NULL ,
	[cPointParam] [nvarchar] (30) NULL ,
	[aPointparam] [nvarchar] (30) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_Pramother] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[FtpTF] [tinyint] NULL ,
	[FTPIP] [nvarchar] (100) NULL ,
	[Ftpport] [nvarchar] (5) NULL ,
	[FtpUserName] [nvarchar] (20) NULL ,
	[FTPPASSword] [nvarchar] (50) NULL ,
	[RssNum] [int] NULL ,
	[RssContentNum] [int] NULL ,
	[RssTitle] [nvarchar] (50) NULL ,
	[RssPicURL] [nvarchar] (200) NULL ,
	[WapTF] [tinyint] NULL ,
	[WapPath] [nvarchar] (50) NULL ,
	[WapDomain] [nvarchar] (50) NULL ,
	[WapLastNum] [int] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_Province] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Pid] [nvarchar] (12) NOT NULL ,
	[pName] [nvarchar] (30) NULL ,
	[creatTime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_SiteTask] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[taskID] [nvarchar] (12) NOT NULL ,
	[TaskName] [nvarchar] (30) NOT NULL ,
	[isIndex] [tinyint] NOT NULL ,
	[ClassID] [ntext] NULL ,
	[News] [ntext] NULL ,
	[Special] [ntext] NULL ,
	[TimeSet] [nvarchar] (100) NULL ,
	[CreatTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_User] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[UserNum] [nvarchar] (15) NOT NULL ,
	[UserName] [nvarchar] (18) NOT NULL ,
	[UserPassword] [nvarchar] (32) NOT NULL ,
	[NickName] [nvarchar] (20) NULL ,
	[RealName] [nvarchar] (20) NULL ,
	[isAdmin] [tinyint] NOT NULL ,
	[UserGroupNumber] [nvarchar] (12) NOT NULL ,
	[PassQuestion] [nvarchar] (20) NULL ,
	[PassKey] [nvarchar] (20) NULL ,
	[CertType] [nvarchar] (15) NULL ,
	[CertNumber] [nvarchar] (20) NULL ,
	[Email] [nvarchar] (220) NULL ,
	[mobile] [nvarchar] (15) NULL ,
	[Sex] [tinyint] NULL ,
	[birthday] [datetime] NULL ,
	[Userinfo] [ntext] NULL ,
	[UserFace] [nvarchar] (220) NULL ,
	[userFacesize] [nvarchar] (8) NULL ,
	[marriage] [tinyint] NULL ,
	[iPoint] [int] NOT NULL ,
	[gPoint] [int] NOT NULL ,
	[cPoint] [int] NOT NULL ,
	[ePoint] [int] NOT NULL ,
	[aPoint] [int] NOT NULL ,
	[isLock] [tinyint] NOT NULL ,
	[RegTime] [datetime] NOT NULL ,
	[LastLoginTime] [datetime] NULL ,
	[OnlineTime] [int] NOT NULL ,
	[OnlineTF] [int] NOT NULL ,
	[LoginNumber] [int] NOT NULL ,
	[FriendClass] [nvarchar] (50) NULL ,
	[LoginLimtNumber] [int] NULL ,
	[LastIP] [nvarchar] (16) NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[Addfriend] [int] NULL ,
	[isOpen] [tinyint] NULL ,
	[ParmConstrNum] [int] NULL ,
	[isIDcard] [tinyint] NULL ,
	[IDcardFiles] [nvarchar] (220) NULL ,
	[Addfriendbs] [tinyint] NULL ,
	[EmailATF] [tinyint] NULL ,
	[EmailCode] [nvarchar] (32) NULL ,
	[isMobile] [tinyint] NULL ,
	[BindTF] [tinyint] NULL ,
	[MobileCode] [nvarchar] (32) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_UserLevel] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[LevelID] [nvarchar] (20) NULL ,
	[LTitle] [nvarchar] (50) NULL ,
	[Lpicurl] [nvarchar] (200) NULL ,
	[iPoint] [int] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_admin] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[UserNum] [nvarchar] (15) NOT NULL ,
	[isSuper] [tinyint] NOT NULL ,
	[adminGroupNumber] [nvarchar] (12) NULL ,
	[PopList] [ntext] NULL ,
	[OnlyLogin] [tinyint] NOT NULL ,
	[isChannel] [tinyint] NOT NULL ,
	[isLock] [tinyint] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[isChSupper] [tinyint] NULL ,
	[Iplimited] [ntext] NULL ,
	[verCode] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_admingroup] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[adminGroupNumber] [nvarchar] (8) NOT NULL ,
	[GroupName] [nvarchar] (20) NULL ,
	[ClassList] [ntext] NULL ,
	[SpecialList] [ntext] NULL ,
	[channelList] [ntext] NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_channel] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[channelName] [nvarchar] (50) NULL ,
	[channelItem] [nvarchar] (50) NULL ,
	[channelEItem] [nvarchar] (20) NULL ,
	[channelDescript] [nvarchar] (200) NULL ,
	[channelunit] [nvarchar] (50) NULL ,
	[DataLib] [nvarchar] (30) NULL ,
	[islock] [tinyint] NULL ,
	[isHTML] [tinyint] NULL ,
	[ClassSave] [nvarchar] (50) NULL ,
	[ClassFileName] [nvarchar] (50) NULL ,
	[SavePath] [nvarchar] (50) NULL ,
	[FileName] [nvarchar] (60) NULL ,
	[htmldir] [nvarchar] (100) NULL ,
	[indexFileName] [nvarchar] (50) NULL ,
	[issys] [tinyint] NULL ,
	[upfilessize] [int] NULL ,
	[upfiletype] [nvarchar] (100) NULL ,
	[ischeck] [tinyint] NULL ,
	[TempletPath] [nvarchar] (100) NULL ,
	[indextemplet] [nvarchar] (200) NULL ,
	[classtemplet] [nvarchar] (200) NULL ,
	[newstemplet] [nvarchar] (200) NULL ,
	[specialtemplet] [nvarchar] (200) NULL ,
	[isModelContent] [tinyint] NULL ,
	[channelType] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[isConstr] [tinyint] NULL ,
	[binddomain] [nvarchar] (150) NULL ,
	[keyCert] [nvarchar] (64) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[iPoint] [int] NULL ,
	[gPoint] [int] NULL ,
	[GroupNumber] [nvarchar] (200) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_channelclass] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ParentID] [int] NULL ,
	[ChID] [int] NULL ,
	[classCName] [nvarchar] (50) NULL ,
	[classEName] [nvarchar] (50) NULL ,
	[OrderID] [tinyint] NULL ,
	[isPage] [tinyint] NULL ,
	[PageContent] [ntext] NULL ,
	[Templet] [nvarchar] (200) NULL ,
	[ContentTemplet] [nvarchar] (200) NULL ,
	[SavePath] [nvarchar] (100) NULL ,
	[FileName] [nvarchar] (100) NULL ,
	[ContentSavePath] [nvarchar] (100) NULL ,
	[ContentFileNameRule] [nvarchar] (150) NULL ,
	[isShowNavi] [tinyint] NULL ,
	[PicURL] [nvarchar] (200) NULL ,
	[NaviContent] [nvarchar] (200) NULL ,
	[KeyMeta] [nvarchar] (100) NULL ,
	[DescMeta] [nvarchar] (150) NULL ,
	[isDelPoint] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[GroupNumber] [nvarchar] (200) NULL ,
	[isLock] [tinyint] NULL ,
	[ClassNavi] [ntext] NULL ,
	[ContentNavi] [ntext] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_channellabel] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [int] NULL ,
	[ChID] [int] NULL ,
	[LabelName] [nvarchar] (100) NULL ,
	[LabelContent] [ntext] NULL ,
	[LabelDescript] [nvarchar] (200) NULL ,
	[isLock] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[CreatTime] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_channellabelclass] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ParentID] [int] NULL ,
	[ChID] [int] NULL ,
	[ClassName] [nvarchar] (80) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_channelspecial] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChID] [int] NULL ,
	[ParentID] [int] NULL ,
	[specialCName] [nvarchar] (100) NULL ,
	[specialEName] [nvarchar] (100) NULL ,
	[binddomain] [nvarchar] (100) NULL ,
	[navicontent] [nvarchar] (200) NULL ,
	[savePath] [nvarchar] (100) NULL ,
	[filename] [nvarchar] (100) NULL ,
	[templet] [nvarchar] (200) NULL ,
	[islock] [tinyint] NULL ,
	[isRec] [tinyint] NULL ,
	[PicURL] [nvarchar] (200) NULL ,
	[OrderID] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_channelstyle] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChID] [int] NULL ,
	[classID] [int] NULL ,
	[styleName] [nvarchar] (50) NULL ,
	[styleContent] [ntext] NULL ,
	[isLock] [tinyint] NULL ,
	[styleDescript] [nvarchar] (200) NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[creattime] [datetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_channelstyleclass] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ParentID] [int] NULL ,
	[cName] [nvarchar] (50) NULL ,
	[ChID] [int] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_channelvalue] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ChID] [int] NULL ,
	[OrderID] [tinyint] NULL ,
	[CName] [nvarchar] (50) NULL ,
	[EName] [nvarchar] (50) NULL ,
	[vValue] [nvarchar] (150) NULL ,
	[vitem] [ntext] NULL ,
	[isSearch] [tinyint] NULL ,
	[HTMLedit] [tinyint] NULL ,
	[vHeight] [nvarchar] (6) NULL ,
	[vDescript] [nvarchar] (200) NULL ,
	[vType] [tinyint] NULL ,
	[vLength] [nvarchar] (6) NULL ,
	[isNulls] [tinyint] NULL ,
	[isUser] [tinyint] NULL ,
	[isLock] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[fieldLength] [nvarchar] (5) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_logs] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[title] [nvarchar] (50) NULL ,
	[content] [ntext] NULL ,
	[creatTime] [datetime] NULL ,
	[IP] [nvarchar] (16) NULL ,
	[usernum] [nvarchar] (15) NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[ismanage] [nvarchar] (50) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_newsIndex] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[TableName] [nvarchar] (20) NOT NULL ,
	[CreatTime] [datetime] NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_param] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[SiteName] [nvarchar] (50) NULL ,
	[SiteDomain] [nvarchar] (100) NULL ,
	[IndexTemplet] [nvarchar] (200) NULL ,
	[IndexFileName] [nvarchar] (20) NULL ,
	[ReadNewsTemplet] [nvarchar] (200) NULL ,
	[ClassListTemplet] [nvarchar] (200) NULL ,
	[SpecialTemplet] [nvarchar] (200) NULL ,
	[FileEXName] [nvarchar] (30) NULL ,
	[ReadType] [tinyint] NULL ,
	[LoginTimeOut] [int] NULL ,
	[Email] [nvarchar] (150) NULL ,
	[LinkType] [tinyint] NULL ,
	[CopyRight] [ntext] NULL ,
	[CheckInt] [tinyint] NULL ,
	[UnLinkTF] [tinyint] NULL ,
	[LenSearch] [nvarchar] (8) NULL ,
	[CheckNewsTitle] [tinyint] NULL ,
	[CollectTF] [tinyint] NULL ,
	[SaveClassFilePath] [nvarchar] (200) NULL ,
	[SaveIndexPage] [nvarchar] (100) NULL ,
	[SaveNewsFilePath] [nvarchar] (200) NULL ,
	[SaveNewsDirPath] [nvarchar] (100) NULL ,
	[ConstrTF] [tinyint] NULL ,
	[PicServerTF] [tinyint] NULL ,
	[PicServerDomain] [ntext] NULL ,
	[PicUpLoad] [nvarchar] (200) NULL ,
	[UpfilesType] [nvarchar] (150) NULL ,
	[UpFilesSize] [int] NULL ,
	[RemoteSavePath] [nvarchar] (200) NULL ,
	[ReMoteDomainTF] [tinyint] NULL ,
	[RemoteDomain] [nvarchar] (100) NULL ,
	[HotNewsJs] [nvarchar] (200) NULL ,
	[LastNewsJs] [nvarchar] (200) NULL ,
	[RecNewsJS] [nvarchar] (200) NULL ,
	[HotCommJS] [nvarchar] (200) NULL ,
	[TNewsJS] [nvarchar] (200) NULL ,
	[ClassListNum] [int] NULL ,
	[NewsNum] [int] NULL ,
	[BatDelNum] [int] NULL ,
	[SpecialNum] [int] NULL ,
	[Pram_Index] [int] NULL ,
	[InsertPicPosition] [nvarchar] (20) NULL ,
	[HistoryNum] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_parmConstr] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[PCId] [nvarchar] (18) NULL ,
	[ConstrPayName] [nvarchar] (20) NULL ,
	[gPoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[money] [int] NULL ,
	[Gunit] [nvarchar] (10) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_parmPrint] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[PrintTF] [tinyint] NULL ,
	[PrintPicTF] [tinyint] NULL ,
	[PrintWord] [nvarchar] (50) NULL ,
	[Printfontsize] [int] NULL ,
	[Printfontfamily] [nvarchar] (30) NULL ,
	[Printfontcolor] [nvarchar] (10) NULL ,
	[PrintBTF] [tinyint] NULL ,
	[PintPicURL] [nvarchar] (150) NULL ,
	[PrintPicsize] [nvarchar] (8) NULL ,
	[PintPictrans] [nvarchar] (20) NULL ,
	[PrintPosition] [tinyint] NULL ,
	[PrintSmallTF] [tinyint] NULL ,
	[PrintSmallSize] [nvarchar] (8) NULL ,
	[PrintSmallinv] [nvarchar] (20) NULL ,
	[PrintSmallSizeStyle] [tinyint] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_styleclass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[Sname] [nvarchar] (50) NULL ,
	[CreatTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[isRecyle] [tinyint] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_sys_userfields] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[userNum] [nvarchar] (15) NULL ,
	[province] [nvarchar] (20) NULL ,
	[City] [nvarchar] (20) NULL ,
	[Address] [nvarchar] (50) NULL ,
	[Postcode] [nvarchar] (10) NULL ,
	[FaTel] [nvarchar] (30) NULL ,
	[WorkTel] [nvarchar] (30) NULL ,
	[QQ] [nvarchar] (30) NULL ,
	[MSN] [nvarchar] (150) NULL ,
	[Fax] [nvarchar] (30) NULL ,
	[character] [ntext] NULL ,
	[UserFan] [ntext] NULL ,
	[Nation] [nvarchar] (12) NULL ,
	[nativeplace] [nvarchar] (20) NULL ,
	[Job] [nvarchar] (30) NULL ,
	[education] [nvarchar] (20) NULL ,
	[Lastschool] [nvarchar] (80) NULL ,
	[orgSch] [nvarchar] (10) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_sys_userother] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[ConID] [nvarchar] (15) NULL ,
	[address] [nvarchar] (100) NULL ,
	[postcode] [nvarchar] (20) NULL ,
	[RealName] [nvarchar] (20) NULL ,
	[bankName] [nvarchar] (100) NULL ,
	[bankaccount] [nvarchar] (30) NULL ,
	[bankcard] [nvarchar] (50) NULL ,
	[bankRealName] [nvarchar] (50) NULL ,
	[UserNum] [nvarchar] (15) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Card] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[CaID] [nvarchar] (12) NULL ,
	[CardNumber] [nvarchar] (30) NULL ,
	[CardPassWord] [nvarchar] (150) NULL ,
	[creatTime] [datetime] NULL ,
	[Money] [money] NULL ,
	[Point] [int] NULL ,
	[isBuy] [tinyint] NULL ,
	[isUse] [tinyint] NULL ,
	[isLock] [tinyint] NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[siteID] [nvarchar] (12) NULL ,
	[TimeOutDate] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Constr] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ConID] [nvarchar] (12) NULL ,
	[ClassID] [nvarchar] (12) NULL ,
	[Title] [nvarchar] (100) NULL ,
	[Content] [ntext] NULL ,
	[creatTime] [datetime] NULL ,
	[Source] [nvarchar] (100) NULL ,
	[Tags] [nvarchar] (200) NULL ,
	[Author] [nvarchar] (100) NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[isCheck] [tinyint] NULL ,
	[PicURL] [nvarchar] (200) NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[ispass] [tinyint] NULL ,
	[passcontent] [ntext] NULL ,
	[isadmidel] [tinyint] NULL ,
	[isuserdel] [tinyint] NULL ,
	[Contrflg] [nvarchar] (10) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_user_ConstrClass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Ccid] [nvarchar] (12) NULL ,
	[UserNum] [nvarchar] (12) NULL ,
	[cName] [nvarchar] (100) NULL ,
	[creatTime] [datetime] NULL ,
	[Content] [nvarchar] (200) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Discuss] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[DisID] [nvarchar] (12) NULL ,
	[Cname] [nvarchar] (100) NULL ,
	[Authority] [nvarchar] (12) NULL ,
	[Authoritymoney] [nvarchar] (12) NULL ,
	[UserName] [nvarchar] (15) NULL ,
	[Browsenumber] [int] NULL ,
	[D_Content] [nvarchar] (200) NULL ,
	[D_anno] [nvarchar] (200) NULL ,
	[Creatime] [datetime] NULL ,
	[ClassID] [nvarchar] (50) NULL ,
	[Fundwarehouse] [nvarchar] (50) NULL ,
	[GroupSize] [int] NULL ,
	[GroupPerNum] [int] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_DiscussActive] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AId] [nvarchar] (18) NULL ,
	[Activesubject] [nvarchar] (50) NULL ,
	[ActivePlace] [nvarchar] (50) NULL ,
	[ActiveExpense] [nvarchar] (50) NULL ,
	[Anum] [int] NULL ,
	[ActivePlan] [nvarchar] (200) NULL ,
	[Contactmethod] [nvarchar] (50) NULL ,
	[Cutofftime] [datetime] NULL ,
	[CreaTime] [datetime] NULL ,
	[UserName] [nvarchar] (18) NULL ,
	[AJurisdiction] [int] NULL ,
	[ALabel] [int] NULL ,
	[FileWay] [nvarchar] (50) NULL ,
	[Filename] [nvarchar] (50) NULL ,
	[siteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_DiscussActiveMember] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[AId] [nvarchar] (18) NULL ,
	[PId] [nvarchar] (18) NULL ,
	[UserNum] [nvarchar] (18) NULL ,
	[Telephone] [nvarchar] (18) NULL ,
	[ParticipationNum] [int] NULL ,
	[isCompanion] [int] NULL ,
	[CreaTime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_DiscussClass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[DcID] [nvarchar] (50) NULL ,
	[Cname] [nvarchar] (50) NULL ,
	[Content] [nvarchar] (200) NULL ,
	[Creatime] [datetime] NULL ,
	[indexnumber] [nvarchar] (50) NULL ,
	[beordered] [int] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[UserNum] [nvarchar] (15) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_DiscussContribute] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Member] [nvarchar] (12) NULL ,
	[Membermoney] [nvarchar] (50) NULL ,
	[DisID] [nvarchar] (12) NULL ,
	[Creatime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_DiscussMember] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Member] [nvarchar] (12) NULL ,
	[DisID] [nvarchar] (12) NULL ,
	[UserNum] [nvarchar] (12) NULL ,
	[Creatime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_DiscussTopic] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[DtID] [nvarchar] (12) NULL ,
	[Title] [nvarchar] (200) NULL ,
	[Content] [ntext] NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[ParentID] [nvarchar] (12) NULL ,
	[IP] [nvarchar] (20) NULL ,
	[creatTime] [datetime] NULL ,
	[VoteTF] [tinyint] NULL ,
	[voteTime] [datetime] NULL ,
	[DisID] [nvarchar] (18) NULL ,
	[source] [tinyint] NULL ,
	[DtUrl] [nvarchar] (50) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_user_Friend] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[FriendUserNum] [nvarchar] (15) NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[bUserNum] [nvarchar] (15) NULL ,
	[UserName] [nvarchar] (50) NULL ,
	[HailFellow] [nvarchar] (12) NULL ,
	[CreatTime] [datetime] NULL ,
	[hyyz] [tinyint] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_FriendClass] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[FriendName] [nvarchar] (50) NULL ,
	[Content] [nvarchar] (200) NULL ,
	[CreatTime] [datetime] NULL ,
	[HailFellow] [nvarchar] (12) NULL ,
	[gdfz] [int] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Ghistory] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[GhID] [nvarchar] (12) NULL ,
	[ghtype] [tinyint] NULL ,
	[Gpoint] [int] NULL ,
	[iPoint] [int] NULL ,
	[Money] [money] NULL ,
	[CreatTime] [datetime] NULL ,
	[UserNUM] [nvarchar] (15) NULL ,
	[gtype] [tinyint] NULL ,
	[content] [ntext] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_user_Group] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[GroupNumber] [nvarchar] (12) NULL ,
	[GroupName] [nvarchar] (50) NULL ,
	[iPoint] [int] NULL ,
	[Gpoint] [int] NULL ,
	[Rtime] [int] NULL ,
	[Discount] [float] NULL ,
	[LenCommContent] [int] NULL ,
	[CommCheckTF] [tinyint] NULL ,
	[PostCommTime] [int] NULL ,
	[upfileType] [nvarchar] (200) NULL ,
	[upfileNum] [int] NULL ,
	[upfileSize] [int] NULL ,
	[DayUpfilenum] [int] NULL ,
	[ContrNum] [int] NULL ,
	[DicussTF] [tinyint] NULL ,
	[PostTitle] [tinyint] NULL ,
	[ReadUser] [tinyint] NULL ,
	[MessageNum] [int] NULL ,
	[MessageGroupNum] [nvarchar] (15) NULL ,
	[IsCert] [tinyint] NULL ,
	[CharTF] [tinyint] NULL ,
	[CharHTML] [tinyint] NULL ,
	[CharLenContent] [int] NULL ,
	[RegMinute] [int] NULL ,
	[PostTitleHTML] [tinyint] NULL ,
	[DelSelfTitle] [tinyint] NULL ,
	[DelOTitle] [tinyint] NULL ,
	[EditSelfTitle] [tinyint] NULL ,
	[EditOtitle] [tinyint] NULL ,
	[ReadTitle] [tinyint] NULL ,
	[MoveSelfTitle] [tinyint] NULL ,
	[MoveOTitle] [tinyint] NULL ,
	[TopTitle] [tinyint] NULL ,
	[GoodTitle] [tinyint] NULL ,
	[LockUser] [tinyint] NULL ,
	[UserFlag] [nvarchar] (100) NULL ,
	[CheckTtile] [tinyint] NULL ,
	[IPTF] [tinyint] NULL ,
	[EncUser] [tinyint] NULL ,
	[OCTF] [tinyint] NULL ,
	[StyleTF] [tinyint] NULL ,
	[UpfaceSize] [int] NULL ,
	[GIChange] [nvarchar] (10) NULL ,
	[GTChageRate] [nvarchar] (30) NULL ,
	[LoginPoint] [nvarchar] (20) NULL ,
	[RegPoint] [nvarchar] (20) NULL ,
	[GroupTF] [tinyint] NULL ,
	[GroupSize] [int] NULL ,
	[GroupPerNum] [int] NULL ,
	[GroupCreatNum] [int] NULL ,
	[CreatTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Guser] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[UserNum] [nvarchar] (15) NOT NULL ,
	[InfoID] [int] NULL ,
	[CreatTime] [datetime] NOT NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[ErrorNum] [int] NOT NULL ,
	[IP] [nvarchar] (15) NOT NULL ,
	[LastErrorTime] [datetime] NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_MessFiles] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[MfID] [nvarchar] (18) NULL ,
	[mID] [nvarchar] (18) NULL ,
	[UserNum] [nvarchar] (16) NULL ,
	[FileName] [nvarchar] (50) NULL ,
	[IsDown] [tinyint] NULL ,
	[FileUrl] [nvarchar] (200) NULL ,
	[Content] [nvarchar] (100) NULL ,
	[CreatTime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Message] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Mid] [nvarchar] (12) NULL ,
	[UserNum] [nvarchar] (15) NULL ,
	[Title] [nvarchar] (50) NULL ,
	[Content] [ntext] NULL ,
	[CreatTime] [datetime] NULL ,
	[Send_DateTime] [datetime] NULL ,
	[SortType] [tinyint] NULL ,
	[Rec_UserNum] [nvarchar] (15) NULL ,
	[FileTF] [tinyint] NULL ,
	[MakeList] [nvarchar] (200) NULL ,
	[AppointTimeTF] [tinyint] NULL ,
	[AppointTime] [datetime] NULL ,
	[LevelFlag] [tinyint] NULL ,
	[issDel] [tinyint] NULL ,
	[issRecyle] [tinyint] NULL ,
	[isRdel] [tinyint] NULL ,
	[isRecyle] [tinyint] NULL ,
	[isRead] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_user_Photo] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[PhotoalbumID] [nvarchar] (18) NULL ,
	[PhotoID] [nvarchar] (18) NULL ,
	[PhotoName] [nvarchar] (18) NULL ,
	[PhotoContent] [nvarchar] (200) NULL ,
	[PhotoUrl] [nvarchar] (200) NULL ,
	[PhotoTime] [datetime] NULL ,
	[UserNum] [nvarchar] (50) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Photoalbum] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[PhotoalbumID] [nvarchar] (18) NULL ,
	[PhotoalbumName] [nvarchar] (200) NULL ,
	[DisID] [nvarchar] (18) NULL ,
	[ClassID] [nvarchar] (18) NULL ,
	[UserName] [nvarchar] (18) NULL ,
	[PhotoalbumJurisdiction] [nvarchar] (50) NULL ,
	[isDisPhotoalbum] [tinyint] NULL ,
	[pwd] [nvarchar] (18) NULL ,
	[PhotoalbumUrl] [nvarchar] (220) NULL ,
	[Creatime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_PhotoalbumClass] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassID] [nvarchar] (18) NULL ,
	[ClassName] [nvarchar] (50) NULL ,
	[Creatime] [datetime] NULL ,
	[UserName] [nvarchar] (18) NULL ,
	[isDisclass] [tinyint] NULL ,
	[DisID] [nvarchar] (18) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_Requestinformation] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[qUsername] [nvarchar] (50) NULL ,
	[bUsername] [nvarchar] (50) NULL ,
	[datatime] [datetime] NULL ,
	[Content] [nvarchar] (50) NULL ,
	[ischick] [tinyint] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_constrPay] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[constrPayID] [nvarchar] (12) NULL ,
	[userNum] [nvarchar] (15) NULL ,
	[Money] [int] NULL ,
	[payTime] [datetime] NULL ,
	[SiteID] [nvarchar] (12) NULL ,
	[payAdmin] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_news] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[newsID] [nvarchar] (12) NULL ,
	[Title] [nvarchar] (50) NULL ,
	[content] [ntext] NULL ,
	[creatTime] [datetime] NULL ,
	[GroupNumber] [nvarchar] (12) NULL ,
	[getPoint] [nvarchar] (50) NULL ,
	[SiteId] [nvarchar] (12) NULL ,
	[isLock] [tinyint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_user_note] (
	[id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[infoType] [tinyint] NULL ,
	[infoID] [nvarchar] (12) NULL ,
	[logTime] [datetime] NULL ,
	[IP] [nvarchar] (16) NULL ,
	[UserNum] [nvarchar] (15) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_user_userlogs] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[logID] [nvarchar] (12) NULL ,
	[Title] [nvarchar] (50) NULL ,
	[content] [ntext] NULL ,
	[creatTime] [datetime] NULL ,
	[dateNum] [smallint] NULL ,
	[LogDateTime] [datetime] NULL ,
	[userNum] [nvarchar] (15) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_user_vote] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[DtID] [nvarchar] (18) NULL ,
	[VoteID] [nvarchar] (18) NULL ,
	[votegenre] [tinyint] NULL ,
	[Voteitem] [nvarchar] (50) NULL ,
	[VoteNum] [int] NULL ,
	[CreaTime] [datetime] NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_vote_Item] (
	[IID] [int] IDENTITY (1, 1) NOT NULL ,
	[TID] [int] NULL ,
	[ItemName] [nvarchar] (200) NULL ,
	[ItemValue] [nvarchar] (10) NULL ,
	[ItemMode] [tinyint] NULL ,
	[PicSrc] [nvarchar] (200) NULL ,
	[DisColor] [nvarchar] (7) NULL ,
	[VoteCount] [int] NULL ,
	[ItemDetail] [ntext] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_vote_Steps] (
	[SID] [int] IDENTITY (1, 1) NOT NULL ,
	[TIDS] [int] NULL ,
	[Steps] [int] NULL ,
	[TIDU] [int] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_vote_class] (
	[VID] [int] IDENTITY (1, 1) NOT NULL ,
	[ClassName] [nvarchar] (20) NULL ,
	[Description] [nvarchar] (50) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_vote_manage] (
	[RID] [int] IDENTITY (1, 1) NOT NULL ,
	[IID] [int] NULL ,
	[TID] [int] NULL ,
	[OtherContent] [nvarchar] (50) NULL ,
	[VoteIp] [nvarchar] (15) NULL ,
	[VoteTime] [datetime] NULL ,
	[UserNumber] [nvarchar] (15) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_vote_param] (
	[SysID] [int] IDENTITY (1, 1) NOT NULL ,
	[IPtime] [nvarchar] (20) NULL ,
	[IsReg] [tinyint] NULL ,
	[IpLimit] [nvarchar] (200) NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_vote_title] (
	[TID] [int] IDENTITY (1, 1) NOT NULL ,
	[VID] [int] NULL ,
	[Title] [nvarchar] (50) NULL ,
	[Type] [tinyint] NULL ,
	[MaxNum] [nvarchar] (50) NULL ,
	[DisMode] [tinyint] NULL ,
	[StartDate] [datetime] NULL ,
	[EndDate] [datetime] NULL ,
	[ItemMode] [tinyint] NULL ,
	[isSteps] [tinyint] NULL ,
	[SiteID] [nvarchar] (12) NULL 
) ON [PRIMARY]
;

ALTER TABLE [fs_News_URL] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_URL] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_User_URL] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_URL] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_User_URLClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_URLClass] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_ads] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_ads] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_ads_class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_ads_class] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_ads_stat] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_ads_stat] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_adstxt] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_adstxt] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_api_commentary] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_API_commentary] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_api_faviate] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_API_Faviate] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_api_navi] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_API_Navi] PRIMARY KEY  CLUSTERED 
	(
		[am_ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_api_pop] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_API_POP] PRIMARY KEY  CLUSTERED 
	(
		[pop_ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_api_qmenu] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_API_Qmenu] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_customform] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_customform] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_customform_item] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_customform_field] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_define_class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Define_Class] PRIMARY KEY  CLUSTERED 
	(
		[DefineId]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_define_data] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Define_Data] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_define_save] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_define_save] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_friend_class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_friend_class] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_friend_link] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_friend_link] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_friend_pram] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_friend_pram] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_Class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_Class] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_Gen] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_Gen] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_JS] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_JS] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_JSFile] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_JSFile] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_JST_Class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_JSTemplets_Class] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_JSTemplet] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_JSTemplet] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_page] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_news_page] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_site] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_site] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_special] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_special] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_sub] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_Sub] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_topline] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_news_topline] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_unNews] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_News_unNews] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_vote] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_news_vote] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_old_news] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_old_News] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_special_news] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Special_News] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_stat_Info] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_stat_Info] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_stat_class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_stat_class] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_stat_param] WITH NOCHECK ADD 
	CONSTRAINT [PK_statParam] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_City] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Sys_City] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_FieldClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_FieldClass] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_FieldData] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_FieldData] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_Label] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_Label] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_LabelClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_LabelClass] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_LabelFree] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_LabelFree] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_LabelStyle] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_LabelStyle] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_PSF] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_PSF] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_PramUser] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_SYS_UserPram] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_Pramother] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_pramother] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_Province] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Sys_Province] PRIMARY KEY  CLUSTERED 
	(
		[Pid]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_SiteTask] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_SiteTask] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_User] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_User] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_UserLevel] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Sys_UserLevel] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_admin] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_admin] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_admingroup] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_AdminGroup] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channel] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_model] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channelclass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_chClass] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channellabel] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_channellabel] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channellabelclass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_channellabelclass] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channelspecial] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_channelspecial] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channelstyle] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_channelstyle] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channelstyleclass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_channelstyleclass] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_channelvalue] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_modelvalue] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_logs] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_logs] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_newsIndex] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_NewsIndex] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_param] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_Param] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_parmConstr] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_ParmConstr] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_parmPrint] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_SYS_ParmPrint] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_userfields] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_userfields] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_userother] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_sys_userother] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Card] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Card] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Constr] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Constr] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_ConstrClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_ConstrClass] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Discuss] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_Discuss] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_DiscussActive] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_DiscussActive] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_DiscussActiveMember] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_DiscussActiveMember] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_DiscussClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_DiscussClass] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_DiscussContribute] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_DiscussContribute] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_DiscussMember] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_DiscussMember] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_DiscussTopic] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_DiscussTopic] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Friend] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Friend] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_FriendClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_FriendClass] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Ghistory] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Ghistory] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Group] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Group] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Guser] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Guser] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_MessFiles] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_MessFiles] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Message] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Message] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Photo] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_Photo] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Photoalbum] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_Photoalbum] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_PhotoalbumClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_PhotoalbumClass] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_Requestinformation] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_requestinformation] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_constrPay] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_constrPay] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_news] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_news] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_note] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_note] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_userlogs] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_user_userlogs] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_user_vote] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_User_Vote] PRIMARY KEY  CLUSTERED 
	(
		[Id]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_vote_Item] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_vote_Item] PRIMARY KEY  CLUSTERED 
	(
		[IID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_vote_Steps] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_vote_Steps] PRIMARY KEY  CLUSTERED 
	(
		[SID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_vote_class] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_vote_Class] PRIMARY KEY  CLUSTERED 
	(
		[VID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_vote_manage] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_vote_Manage] PRIMARY KEY  CLUSTERED 
	(
		[RID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_vote_param] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_vote_Pram] PRIMARY KEY  CLUSTERED 
	(
		[SysID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_vote_title] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_vote_Title] PRIMARY KEY  CLUSTERED 
	(
		[TID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_api_commentary] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_api_commentary_isRecyle] DEFAULT (0) FOR [isRecyle],
	CONSTRAINT [DF_fs_api_commentary_OrderID] DEFAULT (0) FOR [OrderID],
	CONSTRAINT [DF_fs_api_commentary_GoodTitle] DEFAULT (0) FOR [GoodTitle],
	CONSTRAINT [DF_fs_api_commentary_isCheck] DEFAULT (0) FOR [isCheck]
;

ALTER TABLE [fs_customform_item] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_customform_item_seriesnumber] DEFAULT (0) FOR [seriesnumber]
;

ALTER TABLE [fs_define_class] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_Define_Class_ParentId] DEFAULT (0) FOR [ParentInfoId]
;

ALTER TABLE [fs_friend_class] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_friend_class_ParentID] DEFAULT (0) FOR [ParentID]
;

ALTER TABLE [fs_news] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_News_OrderID] DEFAULT (50) FOR [OrderID],
	CONSTRAINT [DF_fs_News_Click] DEFAULT (0) FOR [Click],
	CONSTRAINT [DF_fs_News_CheckStat] DEFAULT ('0|0|0') FOR [CheckStat],
	CONSTRAINT [DF_fs_News_isLock] DEFAULT (0) FOR [isLock],
	CONSTRAINT [DF_fs_News_isRecyle] DEFAULT (0) FOR [isRecyle]
;

ALTER TABLE [fs_news_Class] WITH NOCHECK ADD 
	CONSTRAINT [IX_fs_news_Class] UNIQUE  NONCLUSTERED 
	(
		[ClassID]
	)  ON [PRIMARY] ,
	CONSTRAINT [CK_fs_news_Class] CHECK (len([ClassID]) = 12)
;

ALTER TABLE [fs_news_Gen] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_News_Gen_isLock] DEFAULT (0) FOR [isLock]
;

ALTER TABLE [fs_news_JS] WITH NOCHECK ADD 
	CONSTRAINT [IX_fs_news_JS] UNIQUE  NONCLUSTERED 
	(
		[JsID]
	)  ON [PRIMARY] ,
	CONSTRAINT [CK_fs_news_JS] CHECK ([jsType] = 0 or [jsType] = 1)
;

ALTER TABLE [fs_news_JSFile] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_news_JSFile_ReclyeTF] DEFAULT (0) FOR [ReclyeTF]
;

ALTER TABLE [fs_news_JST_Class] WITH NOCHECK ADD 
	CONSTRAINT [IX_fs_news_JST_Class] UNIQUE  NONCLUSTERED 
	(
		[ClassID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_JSTemplet] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_news_JSTemplet_jsTType] DEFAULT (0) FOR [JSTType],
	CONSTRAINT [IX_fs_news_JSTemplet] UNIQUE  NONCLUSTERED 
	(
		[TempletID]
	)  ON [PRIMARY] ,
	CONSTRAINT [CK_fs_news_JSTemplet] CHECK ([JSTType] = 0 or [JSTType] = 1)
;

ALTER TABLE [fs_news_site] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_news_site_isRecyle] DEFAULT (0) FOR [isRecyle],
	CONSTRAINT [IX_fs_news_site] UNIQUE  NONCLUSTERED 
	(
		[EName]
	)  ON [PRIMARY] ,
	CONSTRAINT [IX_fs_news_site_1] UNIQUE  NONCLUSTERED 
	(
		[ChannelID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_news_topline] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_news_topline_TodayPic_space] DEFAULT (0) FOR [tl_space],
	CONSTRAINT [DF_fs_news_topline_TodayWidth] DEFAULT (300) FOR [tl_Width]
;

ALTER TABLE [fs_old_news] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_old_News_OrderID] DEFAULT (50) FOR [OrderID],
	CONSTRAINT [DF_fs_old_News_Click] DEFAULT (0) FOR [Click],
	CONSTRAINT [DF_fs_old_News_CheckStat] DEFAULT ('0|0|0') FOR [CheckStat],
	CONSTRAINT [DF_fs_old_News_isLock] DEFAULT (0) FOR [isLock],
	CONSTRAINT [DF_fs_old_News_isRecyle] DEFAULT (0) FOR [isRecyle]
;

ALTER TABLE [fs_stat_content] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_stat_content_today] DEFAULT (1) FOR [today],
	CONSTRAINT [DF_fs_stat_content_yesterday] DEFAULT (0) FOR [yesterday],
	CONSTRAINT [DF_fs_stat_content_vdate] DEFAULT (0 - 0 - 0) FOR [vdate],
	CONSTRAINT [DF_fs_stat_content_vtop] DEFAULT (1) FOR [vtop],
	CONSTRAINT [DF_fs_stat_content_starttime] DEFAULT (0 - 0 - 0) FOR [starttime],
	CONSTRAINT [DF_fs_stat_content_vhigh] DEFAULT (1) FOR [vhigh],
	CONSTRAINT [DF_fs_stat_content_vhightime] DEFAULT (0 - 0 - 0) FOR [vhightime]
;

ALTER TABLE [fs_stat_param] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_stat_param_ipCheck] DEFAULT (0) FOR [ipCheck],
	CONSTRAINT [DF_fs_stat_param_isOnlinestat] DEFAULT (1) FOR [isOnlinestat]
;

ALTER TABLE [fs_sys_LabelFree] WITH NOCHECK ADD 
	CONSTRAINT [IX_fs_sys_LabelFree] UNIQUE  NONCLUSTERED 
	(
		[LabelID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_sys_PSF] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_sys_PSF_isRecyle] DEFAULT (0) FOR [isRecyle]
;

ALTER TABLE [fs_sys_PramUser] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_sys_PramUser_GhClass] DEFAULT (1) FOR [GhClass]
;

ALTER TABLE [fs_sys_User] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_sys_User_Sex] DEFAULT (0) FOR [Sex],
	CONSTRAINT [DF_fs_sys_User_marriage] DEFAULT (0) FOR [marriage],
	CONSTRAINT [DF_fs_sys_User_iPoint] DEFAULT (0) FOR [iPoint],
	CONSTRAINT [DF_fs_sys_User_gPoint] DEFAULT (0) FOR [gPoint],
	CONSTRAINT [DF_fs_sys_User_cPoint] DEFAULT (0) FOR [cPoint],
	CONSTRAINT [DF_fs_sys_User_ePoint] DEFAULT (0) FOR [ePoint],
	CONSTRAINT [DF_fs_sys_User_aPoint] DEFAULT (0) FOR [aPoint],
	CONSTRAINT [DF_fs_sys_User_isLock] DEFAULT (0) FOR [isLock],
	CONSTRAINT [DF_fs_sys_User_OnlineTime] DEFAULT (0) FOR [OnlineTime],
	CONSTRAINT [DF_fs_sys_User_OnlineTF] DEFAULT (0) FOR [OnlineTF],
	CONSTRAINT [DF_fs_sys_User_LoginNumber] DEFAULT (0) FOR [LoginNumber],
	CONSTRAINT [DF_fs_sys_User_LoginLimtNumber] DEFAULT (0) FOR [LoginLimtNumber],
	CONSTRAINT [DF_fs_sys_User_Addfriend] DEFAULT (2) FOR [Addfriend],
	CONSTRAINT [DF_fs_sys_User_isOpen] DEFAULT (0) FOR [isOpen],
	CONSTRAINT [DF_fs_sys_User_ParmConstrNum] DEFAULT (0) FOR [ParmConstrNum],
	CONSTRAINT [DF_fs_sys_User_isIDcard] DEFAULT (0) FOR [isIDcard],
	CONSTRAINT [DF_fs_sys_User_friendEstablishment] DEFAULT (2) FOR [Addfriendbs],
	CONSTRAINT [DF_fs_sys_User_EmailATF] DEFAULT (0) FOR [EmailATF],
	CONSTRAINT [DF_fs_sys_User_isMobile] DEFAULT (0) FOR [isMobile],
	CONSTRAINT [DF_fs_sys_User_BindTF] DEFAULT (0) FOR [BindTF]
;

ALTER TABLE [fs_sys_UserLevel] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_Sys_UserLevel_iPoint] DEFAULT (0) FOR [iPoint]
;

ALTER TABLE [fs_sys_param] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_sys_Param_ConstrTF] DEFAULT (1) FOR [ConstrTF],
	CONSTRAINT [DF_fs_sys_Param_PicServerTF] DEFAULT (0) FOR [PicServerTF],
	CONSTRAINT [DF_fs_sys_Param_ReMoteDomainTF] DEFAULT (1) FOR [ReMoteDomainTF],
	CONSTRAINT [DF_fs_sys_Param_Pram_Index] DEFAULT (10) FOR [Pram_Index]
;

ALTER TABLE [fs_sys_styleclass] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_sys_styleclass_isRecyle] DEFAULT (0) FOR [isRecyle]
;

ALTER TABLE [fs_user_Discuss] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_user_Discuss_Browsenumber] DEFAULT (0) FOR [Browsenumber]
;

ALTER TABLE [fs_user_DiscussActive] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_user_DiscussActive_AJurisdiction] DEFAULT (0) FOR [AJurisdiction]
;

ALTER TABLE [fs_user_Group] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_user_Group_MessageGroupNum] DEFAULT (0 | 0) FOR [MessageGroupNum]
;

ALTER TABLE [fs_user_Message] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_user_Message_islock] DEFAULT (0) FOR [isRead]
;

ALTER TABLE [fs_vote_title] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_vote_Title_isSteps] DEFAULT (0) FOR [isSteps]
;

 CREATE  INDEX [PK_fs_NewsID] ON [fs_news]([NewsType] DESC , [OrderID] DESC ) ON [PRIMARY]
;

SET QUOTED_IDENTIFIER OFF 
;
SET ANSI_NULLS OFF 
;




SET QUOTED_IDENTIFIER OFF 
;
SET ANSI_NULLS ON 
;

SET QUOTED_IDENTIFIER OFF 
;
SET ANSI_NULLS OFF 
;




SET QUOTED_IDENTIFIER OFF 
;
SET ANSI_NULLS ON 
;




if exists (select * from dbo.sysobjects where id = object_id(N'[FK_fs_Collect_RuleRender_fs_Collect_Rule]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [fs_Collect_RuleApply] DROP CONSTRAINT FK_fs_Collect_RuleRender_fs_Collect_Rule
;

if exists (select * from dbo.sysobjects where id = object_id(N'[FK_fs_Collect_RuleRender_fs_Collect_Site]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [fs_Collect_RuleApply] DROP CONSTRAINT FK_fs_Collect_RuleRender_fs_Collect_Site
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_Collect_News]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_Collect_News]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_Collect_Rule]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_Collect_Rule]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_Collect_RuleApply]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_Collect_RuleApply]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_Collect_Site]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_Collect_Site]
;

if exists (select * from dbo.sysobjects where id = object_id(N'[fs_Collect_SiteFolder]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_Collect_SiteFolder]
;

CREATE TABLE [fs_Collect_News] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Title] [nvarchar] (100) NOT NULL ,
	[Links] [nvarchar] (200) NOT NULL ,
	[Author] [nvarchar] (100) NULL ,
	[Source] [nvarchar] (100) NULL ,
	[Content] [ntext] NOT NULL ,
	[AddDate] [datetime] NULL ,
	[ImagesCount] [int] NULL ,
	[SiteID] [int] NOT NULL ,
	[Catched_Form] [nvarchar] (50) NULL ,
	[History] [bit] NOT NULL ,
	[RecTF] [bit] NOT NULL ,
	[TodayNewsTF] [bit] NOT NULL ,
	[MarqueeNews] [bit] NOT NULL ,
	[SBSNews] [bit] NOT NULL ,
	[ReviewTF] [bit] NOT NULL ,
	[CollectTime] [datetime] NOT NULL ,
	[ChannelID] [nvarchar] (12) NOT NULL ,
	[ClassID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

CREATE TABLE [fs_Collect_Rule] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[RuleName] [nvarchar] (50) NOT NULL ,
	[OldContent] [nvarchar] (100) NOT NULL ,
	[ReContent] [nvarchar] (100) NOT NULL ,
	[AddDate] [datetime] NOT NULL ,
	[IgnoreCase] [bit] NOT NULL ,
	[ChannelID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_Collect_RuleApply] (
	[SiteID] [int] NOT NULL ,
	[RuleID] [int] NOT NULL ,
	[RefreshTime] [datetime] NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_Collect_Site] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[SiteName] [nvarchar] (50) NOT NULL ,
	[objURL] [nvarchar] (250) NOT NULL ,
	[Encode] [nvarchar] (50) NOT NULL ,
	[OtherPara] [nvarchar] (50) NULL ,
	[MaxNum] [int] NOT NULL ,
	[ListSetting] [nvarchar] (4000) NULL ,
	[LinkSetting] [nvarchar] (4000) NULL ,
	[OtherType] [int] NULL ,
	[OtherPageSetting] [nvarchar] (4000) NULL ,
	[StartPageNum] [int] NULL ,
	[EndPageNum] [int] NULL ,
	[PagebodySetting] [nvarchar] (4000) NULL ,
	[PageTitleSetting] [nvarchar] (4000) NULL ,
	[OtherNewsType] [int] NULL ,
	[OtherNewsPageSetting] [nvarchar] (4000) NULL ,
	[AuthorSetting] [nvarchar] (4000) NULL ,
	[SourceSetting] [nvarchar] (4000) NULL ,
	[AddDateSetting] [nvarchar] (4000) NULL ,
	[IsAutoCollect] [bit] NOT NULL ,
	[CollectDate] [int] NULL ,
	[TextTF] [bit] NOT NULL ,
	[SaveRemotePic] [bit] NOT NULL ,
	[Audit] [int] NOT NULL ,
	[IsStyle] [bit] NOT NULL ,
	[IsDIV] [bit] NOT NULL ,
	[IsA] [bit] NOT NULL ,
	[IsClass] [bit] NOT NULL ,
	[IsFont] [bit] NOT NULL ,
	[IsSpan] [bit] NOT NULL ,
	[IsObject] [bit] NOT NULL ,
	[IsIFrame] [bit] NOT NULL ,
	[IsScript] [bit] NOT NULL ,
	[IsReverse] [bit] NOT NULL ,
	[IsAutoPicNews] [bit] NOT NULL ,
	[HandSetAuthor] [nvarchar] (50) NULL ,
	[HandSetSource] [nvarchar] (50) NULL ,
	[HandSetAddDate] [datetime] NULL ,
	[Folder] [int] NULL ,
	[ClassID] [nvarchar] (12) NOT NULL ,
	[ChannelID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY]
;

CREATE TABLE [fs_Collect_SiteFolder] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[SiteFolder] [nvarchar] (50) NOT NULL ,
	[SiteFolderDetail] [ntext] NULL ,
	[ChannelID] [nvarchar] (12) NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

ALTER TABLE [fs_Collect_News] WITH NOCHECK ADD 
	CONSTRAINT [PK_Collect_News] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_Collect_Rule] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Rule] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_Collect_RuleApply] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Collect_RuleRender] PRIMARY KEY  CLUSTERED 
	(
		[SiteID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_Collect_Site] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Site] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_Collect_SiteFolder] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_SiteFolder] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
;

ALTER TABLE [fs_Collect_News] WITH NOCHECK ADD 
	CONSTRAINT [DF__fs_News__History__32AB8735] DEFAULT (0) FOR [History],
	CONSTRAINT [DF__fs_News__RecTF__3493CFA7] DEFAULT (0) FOR [RecTF],
	CONSTRAINT [DF__fs_News__TodayNe__3587F3E0] DEFAULT (0) FOR [TodayNewsTF],
	CONSTRAINT [DF__fs_News__Marquee__367C1819] DEFAULT (0) FOR [MarqueeNews],
	CONSTRAINT [DF__fs_News__SBSNews__37703C52] DEFAULT (0) FOR [SBSNews],
	CONSTRAINT [DF__fs_News__ReviewT__3864608B] DEFAULT (0) FOR [ReviewTF]
;

ALTER TABLE [fs_Collect_Rule] WITH NOCHECK ADD 
	CONSTRAINT [DF_fs_Collect_Rule_IgnoreCase] DEFAULT (1) FOR [IgnoreCase]
;

ALTER TABLE [fs_Collect_Site] WITH NOCHECK ADD 
	CONSTRAINT [DF__fs_Site__MaxNum__3C34F16F] DEFAULT (0) FOR [MaxNum],
	CONSTRAINT [DF__fs_Site__OtherTy__3D2915A8] DEFAULT (0) FOR [OtherType],
	CONSTRAINT [DF__fs_Site__OtherNe__3E1D39E1] DEFAULT (0) FOR [OtherNewsType],
	CONSTRAINT [DF__fs_Site__IsAutoC__3F115E1A] DEFAULT (0) FOR [IsAutoCollect],
	CONSTRAINT [DF__fs_Site__Collect__40058253] DEFAULT (0) FOR [CollectDate],
	CONSTRAINT [DF__fs_Site__TextTF__41EDCAC5] DEFAULT (0) FOR [TextTF],
	CONSTRAINT [DF__fs_Site__SaveRem__42E1EEFE] DEFAULT (0) FOR [SaveRemotePic],
	CONSTRAINT [DF__fs_Site__Audit__43D61337] DEFAULT (1) FOR [Audit],
	CONSTRAINT [DF__fs_Site__IsStyle__44CA3770] DEFAULT (0) FOR [IsStyle],
	CONSTRAINT [DF__fs_Site__IsDIV__45BE5BA9] DEFAULT (0) FOR [IsDIV],
	CONSTRAINT [DF__fs_Site__IsA__46B27FE2] DEFAULT (0) FOR [IsA],
	CONSTRAINT [DF__fs_Site__IsClass__47A6A41B] DEFAULT (0) FOR [IsClass],
	CONSTRAINT [DF__fs_Site__IsFont__489AC854] DEFAULT (0) FOR [IsFont],
	CONSTRAINT [DF__fs_Site__IsSpan__498EEC8D] DEFAULT (0) FOR [IsSpan],
	CONSTRAINT [DF__fs_Site__IsObjec__4A8310C6] DEFAULT (0) FOR [IsObject],
	CONSTRAINT [DF__fs_Site__IsIFram__4B7734FF] DEFAULT (0) FOR [IsIFrame],
	CONSTRAINT [DF__fs_Site__IsScrip__4C6B5938] DEFAULT (0) FOR [IsScript],
	CONSTRAINT [DF__fs_Site__IsAutoP__5224328E] DEFAULT (0) FOR [IsAutoPicNews]
;

ALTER TABLE [fs_Collect_RuleApply] ADD 
	CONSTRAINT [FK_fs_Collect_RuleRender_fs_Collect_Rule] FOREIGN KEY 
	(
		[RuleID]
	) REFERENCES [fs_Collect_Rule] (
		[ID]
	),
	CONSTRAINT [FK_fs_Collect_RuleRender_fs_Collect_Site] FOREIGN KEY 
	(
		[SiteID]
	) REFERENCES [fs_Collect_Site] (
		[ID]
	)
;


if exists (select * from dbo.sysobjects where id = object_id(N'[fs_Sys_Help]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [fs_Sys_Help]
;

CREATE TABLE [fs_Sys_Help] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[HelpID] [nvarchar] (30) NULL ,
	[TitleCN] [nvarchar] (80) NULL ,
	[ContentCN] [ntext] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
;

ALTER TABLE [fs_Sys_Help] WITH NOCHECK ADD 
	CONSTRAINT [PK_fs_Sys_Help] PRIMARY KEY  CLUSTERED 
	(
		[id]
	)  ON [PRIMARY] 
;







