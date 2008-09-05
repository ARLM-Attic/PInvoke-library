using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using Foosun.Model;
using Foosun.DALFactory;
using Foosun.DALProfile;
using Foosun.Config;

namespace Foosun.AccessDAL
{
    public class Pagination : DbBase, IPagination
    {
        //分页查询列表，第一个字段必须是不重复的索引字段，必须有order by 索引字段语句
        //频道管理列表
        protected string[] manage_news_site_list = { "Id", "Id,ChannelID,CName,EName,ChannCName,IsURL,isLock,[Domain],ContrTF,ShowNaviTF", "News_site where isRecyle=0 and (ParentID='" + Foosun.Global.Current.SiteID + "' or ChannelID='" + Foosun.Global.Current.SiteID + "')", "order by Id desc" };

        protected string[] user_constr_constraccount ={ "id", "id,ConID,bankName,bankaccount,bankcard,bankRealName", "sys_userother where UserNum=@UserNum", "order by id desc" };

        protected string[] user_constr_constrclass ={ "id", "id,Ccid,cName,creatTime", "user_ConstrClass where UserNum=@UserNum", "order by Id desc" };

        protected string[] user_constr_constrlist ={ "id", "id,ConID,Title,creatTime,ClassID,isCheck,Tags", "user_Constr where UserNum=@UserNum", "order by Id desc" };

        protected string[] user_constr_constrlistpass ={ "id", "id,ConID,Title,creatTime,ClassID,ispass", "user_Constr where UserNum=@UserNum and  ispass=1 ", "order by Id desc" };

        protected string[] user_constr_constrmoney ={ "id", "id,Money,payTime,constrPayID", "user_constrPay where UserNum=@UserNum", "order by id desc" };

        protected string[] manage_label_style_1 ={ "id", "id,ClassID,Sname,CreatTime,isRecyle", "sys_styleclass where SiteID=@SiteID and isRecyle=0", "order by Id desc" };

        protected string[] manage_label_style_2 ={ "id", "id,styleID,StyleName,CreatTime,Content", "sys_LabelStyle where ClassID=@ClassID and  isRecyle=0 and SiteID=@SiteID", "order by Id desc" };

        protected string[] manage_label_style_3 ={ "id", "id,styleID,StyleName,CreatTime,Content", "sys_LabelStyle where StyleName like Cstr(@Keyword) or styleID like Cstr(@Keyword) and  isRecyle=0 and SiteID=@SiteID", "order by Id desc" };

        protected string[] user_friend_friendlist_1 ={ "id", "id,FriendUserNum,UserNum,bUserNum,UserName,CreatTime", "User_Friend  where UserNum=@UserNum and HailFellow=@HailFellow", "order by Id desc" };

        protected string[] user_friend_friendlist_2 ={ "id", "id,FriendUserNum,UserNum,bUserNum,UserName,CreatTime", "User_Friend  where UserNum=@UserNum", "order by Id desc" };

        //protected string[] user_friend_friendmanage ={ "id", "id,HailFellow,FriendName,CreatTime,(select count(*) from " + Foosun.Config.UIConfig.dataRe + "User_friend where busernum=" + Foosun.Config.UIConfig.dataRe + "User_FriendClass.usernum) as CNT", "User_FriendClass where UserNum=@UserNum or gdfz=1", "order by Id desc" };
        protected string[] user_friend_friendmanage ={ "id", "id,UserNum,HailFellow,FriendName,CreatTime", "User_FriendClass where UserNum=@UserNum or gdfz=1", "order by Id desc" };

        protected string[] user_info_announce ={ "id", "id,title,Content,creatTime,islock,getPoint,GroupNumber", "user_news  where SiteID=@SiteID and islock=0", "order by Id desc" };

        protected string[] user_info_collection_1 ={ "id", "id,FID,Infotitle,CreatTime,datalib,ChID", "api_faviate where UserNum=@UserNum and APIID=@Api", "order by Id desc" };

        protected string[] user_info_collection_2 ={ "id", "id,FID,Infotitle,CreatTime,datalib,ChID", "api_faviate where UserNum=@UserNum ", "order by Id desc" };

        protected string[] user_info_history_1 ={ "id", "id,GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content", "user_Ghistory where UserNUM=@UserNum and ghtype = 0", "order by id desc" };

        protected string[] user_info_history_2 ={ "id", "id,GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content", "user_Ghistory where UserNUM=@UserNum and ghtype = 1", "order by id desc" };

        protected string[] user_info_history_3 ={ "id", "id,GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content", "user_Ghistory where UserNUM=@UserNum and gtype=@gtype", "order by id desc" };

        protected string[] user_info_history_4 ={ "id", "id,GhID,ghtype,Gpoint,iPoint,Money,CreatTime,UserNUM,gtype,content", "user_Ghistory where UserNUM=@UserNum ", "order by id desc" };

        protected string[] manage_label_SysLabel_List_1 ={ "id", "id,ClassID,ClassName,CreatTime,Content,isRecyle", "sys_LabelClass Where isRecyle=0 And SiteID=@SiteID", "order by Id desc" };

        protected string[] manage_label_SysLabel_List_2 ={ "id", "id,LabelID,Label_Name,CreatTime,Description,Label_Content", "sys_Label Where isRecyle=0 And  isBack=0 And SiteID=@SiteID And ClassID=@ClassID", "order by Id desc" };

        protected string[] manage_label_SysLabel_List_3 ={ "id", "id,LabelID,Label_Name,CreatTime,Description,Label_Content", "sys_Label Where isRecyle=0 And  isBack=0 And isSys=0 and SiteID=@SiteID and (Label_Name like Cstr(@Keyword) or Description like cstr(@Keyword))", "order by Id desc" };

        protected string[] manage_label_syslabel_bak ={ "id", "id,LabelID,Label_Name,CreatTime", "sys_Label Where SiteID=@SiteID And isRecyle=0 And isBack=1", "order by Id desc" };

        protected string[] user_Rss_RssFeed_1 ={ "id", "id,ClassID,ClassCName,ClassEName,ParentID", "news_Class where ParentID='0' and isURL=0 and isRecyle=0 and isLock=0 and isPage=0 and SiteID=@SiteID", "order by Id desc" };

        protected string[] user_Rss_RssFeed_2 ={ "id", "id,ClassID,ClassCName,ClassEName,ParentID", "news_Class where ParentID=@ParentID and isURL=0 and isRecyle=0 and isLock=0 and isPage=0 and SiteID=@SiteID", "order by Id desc" };

        protected string[] manage_Sys_admin_list = { "a.Id", "a.Id,a.UserNum,b.RealName,b.Email,a.isSuper,a.isLock", "sys_admin a left join sys_User b  on a.UserNum=b.UserNum Where b.isAdmin=1 and a.SiteID='" + Foosun.Global.Current.SiteID + "'", "order by a.Id desc" };

        protected string[] manage_Sys_admin_list_1 = { "a.Id", "a.Id,a.UserNum,b.RealName,b.Email,a.isSuper,a.isLock", "sys_admin a left join sys_User b  on a.UserNum=b.UserNum Where b.isAdmin=1 and a.SiteID=@SiteID", "order by a.Id desc" };

        protected string[] user_photo_photo ={ "id", "id,PhotoID,PhotoName,PhotoTime,UserNum,PhotoContent,PhotoalbumID,PhotoUrl", "user_photo where PhotoalbumID=@PhotoalbumID", "order by ID desc" };

        protected string[] user_photo_Photoalbumlist ={ "id", "id,PhotoalbumName,PhotoalbumID,UserName,Creatime,pwd", "User_Photoalbum where isDisPhotoalbum=0 and UserName=@UserNum", "order by ID desc" };

        protected string[] user_photo_photoclass ={ "id", "id,ClassID,ClassName,Creatime,UserName", "user_PhotoalbumClass where isDisclass=0 and UserName=@UserNum", "order by Id desc" };

        protected string[] manage_Sys_admin_group ={ "id", "id,adminGroupNumber,GroupName,CreatTime", "sys_AdminGroup Where SiteID=@SiteID", "order by Id desc" };
        //地区管理
        protected string[] manage_user_arealist ={ "id", "id,Cid,cityName,creatTime", "Sys_City where Pid='0' and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by OrderId desc,ID desc" };

        protected string[] manage_user_arealist_City ={ "id", "id,Cid,cityName,creatTime", "Sys_City where Pid=@Cid and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id desc" };

        protected string[] manage_user_discussacti_list_1 ={ "id", "id,AId,Activesubject,UserName,CreaTime,ActiveExpense", "user_DiscussActive where 1=1 and Activesubject like cstr(@titlem) and siteID = @RequestSiteId", "order by Id desc" };

        protected string[] manage_user_discussacti_list_2 ={ "id", "id,AId,Activesubject,UserName,CreaTime,ActiveExpense", "user_DiscussActive where 1=1 and Activesubject like cstr(@titlem)", "order by Id desc" };
        //投稿后台
        protected string[] manage_Contribution_Constr_chicklist ={ "id", "id,ConID,Title,creatTime,UserNum,Source,Tags,Author,ispass,Contrflg", "User_Constr where Mid(Contrflg,3,1) = '1' and isadmidel=0 and isCheck=1 and ispass=0 and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id desc" };

        protected string[] manage_Contribution_Constr_List ={ "id", "id,ConID,Title,creatTime,Source,Tags,Contrflg,Author,ispass", "User_Constr where Mid(Contrflg,3,1) = '1' and isadmidel=0 and isCheck=0 and ispass=0 and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id desc" };

        protected string[] manage_news_Special_List ={ "id", "id,SpecialID,SpecialCName,isLock,CreatTime", "news_special Where SiteID=@SiteID  and isRecyle=0 and ParentID='0'", "order by Id Desc" };
        //公告
        protected string[] manage_user_announce ={ "id", "id,title,Content,creatTime,islock,getPoint,GroupNumber,SiteID", "user_news where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id Desc" };

        //好友验证
        protected string[] user_Requestinformation ={ "id", "id,qUsername,bUsername,datatime,Content,ischick", "user_Requestinformation where bUsername='" + Foosun.Global.Current.UserName + "' and ischick=1", "order by Id Desc" };

        protected string[] manage_user_announce_1 ={ "id", "id,title,Content,creatTime,islock,getPoint,GroupNumber,SiteID", "user_news where SiteID=@SiteID", "order by Id Desc" };
        //稿酬级别
        protected string[] manage_Contribution_Constr_SetParamlist ={ "id", "id,PCId,ConstrPayName,gPoint,iPoint,money,Gunit", "sys_ParmConstr where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id Desc" };

        //稿酬
        protected string[] manage_Contribution_paymentannals ={ "id", "id,constrPayID,userNum,Money,payTime,PayAdmin", "user_constrPay where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by id Desc" };

        //logs 
        protected string[] user_manage_logs_1 = { "id", "id,logID,Title,Content,creatTime,dateNum,LogDateTime,UserNum,SiteID", "user_userlogs Where UserNum='" + Foosun.Global.Current.UserNum + "'", "order by creatTime desc,id desc" };

        //网址收藏
        protected string[] user_info_url = { "id", "id,ClassID,URLName,URL,URLColor,CreatTime,Content", "user_URL where UserNum='" + Foosun.Global.Current.UserNum + "'", "order by id desc" };

        protected string[] user_info_url_1 = { "id", "id,ClassID,URLName,URL,URLColor,CreatTime,Content", "user_URL where ClassID=@ClassID and UserNum='" + Foosun.Global.Current.UserNum + "'", "order by id desc" };

        protected string[] manage_user_discussclass_1 ={ "id", "id,DcID,Cname,Content", "User_DiscussClass where indexnumber='0' and Cname like cstr(titlem) and siteID = @RequestSiteId", "order by Id desc" };

        protected string[] manage_user_discussclass_2 ={ "id", "id,DcID,Cname,Content", "User_DiscussClass where indexnumber='0' and Cname like cstr(titlem)", "order by Id desc" };

        protected string[] manage_user_discussclass_3 ={ "id", "id,DcID,Cname,Content", "User_DiscussClass where indexnumber='0' and Cname like cstr(titlem) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id desc" };

        protected string[] manage_user_discuss_1 ={ "id", "id,DisID,Cname,UserName,Creatime,Authoritymoney", "user_Discuss where siteID = @RequestSiteId and Cname like cstr(titlem)", "order by Id desc" };

        protected string[] manage_user_discuss_2 ={ "id", "id,DisID,Cname,UserName,Creatime,Authoritymoney", "user_Discuss where Cname like cstr(titlem) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id desc" };

        protected string[] user_message_Message_box_1 ={ "Id", "Id,Mid,Title,content,UserNum,Send_DateTime,LevelFlag,FileTF,isRead", "user_Message where Rec_UserNum=@UserNum and isRdel=0 and isRecyle=0", "order by Id desc" };

        protected string[] user_message_Message_box_2 ={ "id", "id,Mid,Title,content,UserNum,Send_DateTime,LevelFlag,FileTF,isRead", "user_Message where UserNum=@UserNum and issDel=0 and issRecyle=0", "order by Id desc" };

        protected string[] user_message_Message_box_3 ={ "id", "id,Mid,Title,content,UserNum,Send_DateTime,LevelFlag,FileTF,isRead", "user_Message where UserNum=@UserNum and SortType=1 and issDel=0 and issRecyle=0", "order by Id desc" };

        protected string[] user_message_Message_box_4 ={ "id", "id,Mid,Title,content,UserNum,Send_DateTime,LevelFlag,FileTF,isRead", "user_Message where ((Rec_UserNum=@UserNum and isRecyle=1) or (UserNum=@UserNum and issRecyle=1)) and issDel=0", "order by Id desc" };

        protected string[] user_discuss_discussacti_list ={ "id", "id,AId,Activesubject,UserName,CreaTime,ActiveExpense", "user_DiscussActive", "order by Id desc" };

        protected string[] user_discuss_discussactiestablish_list ={ "id", "id,AId,Activesubject,UserName,CreaTime", "user_DiscussActive where UserName=@cjUserName", "order by Id desc" };

        protected string[] user_discuss_discussactijoin_list = { "a.Id", "a.Id,a.AId,Activesubject,a.UserName,b.CreaTime,b.PId", "user_DiscussActive a inner join user_DiscussActiveMember b on a.AId=b.AId", "order by a.Id desc" };

        protected string[] user_discuss_discussManage_list ={ "id", "id,DisID,Cname,UserName,Creatime,Authoritymoney,Browsenumber", "user_Discuss where Mid(Authority, 1, 1) = '1'", "order by Id desc" };

        protected string[] user_discuss_discussManageestablish_list ={ "id", "id,DisID,Cname,UserName,Creatime,Browsenumber", "user_Discuss where UserName=@UserName", "order by Id desc" };

        protected string[] user_discuss_discussManagejoin_list = { "a.Id", "a.Id,a.DisID,Cname,a.UserName,b.Creatime,b.Member,a.Browsenumber", "user_Discuss a inner join user_DiscussMember b on a.DisID=b.DisID where b.UserNum=@UserNum", "order by a.Id desc" };

        protected string[] user_discuss_discussPhotoalbumlist ={ "id", "id,PhotoalbumName,PhotoalbumID,UserName,Creatime,pwd", "User_Photoalbum where isDisPhotoalbum=1 and DisID=@DisID", "order by ID desc" };
        //新闻栏目
        protected string[] manage_news_class_list ={ "id", "id,ClassID,ClassCName,ClassEname,ParentID,OrderID,IsURL,IsLock,[Domain],NaviShowtf,isPage,classtemplet,ReadNewsTemplet", "News_Class where isRecyle<>1 and ParentID='0' " + Foosun.Common.Public.getSessionStr() + "", "order by OrderID Desc,id desc" };

        protected string[] manage_news_class_list_1 ={ "id", "id,ClassID,ClassCName,ClassEname,ParentID,OrderID,IsURL,IsLock,[Domain],NaviShowtf,isPage,classtemplet,ReadNewsTemplet", "News_Class where isRecyle<>1 and ParentID='0' and SiteID=@SiteID", "order by OrderID Desc,id desc" };
        //讨论组
        protected string[] user_discuss_discussphotoclass ={ "id", "id,ClassID,ClassName,Creatime,UserName", "user_PhotoalbumClass where isDisclass=1 and DisID=@DisID", "order by Id desc" };

        protected string[] user_discuss_discussTopi_commentary ={ "id", "id,creatTime,DtID,ParentID,UserNum", "User_DiscussTopic where ParentID=@DtIDs or (ParentID='0' and  DtID=@DtIDs)", "order by creatTime asc,id asc" };

        protected string[] user_discuss_discussTopi_list ={ "id", "id,VoteTF,DtID,Title,UserNum,creatTime", "User_DiscussTopic where ParentID='0' and DisID=@DisID", "order by Id desc" };

        protected string[] user_info_applyads = { "AdID", "AdID,adName,adType,ClickNum,ShowNum,creatTime,isLock,TimeOutDay", "ads Where CusID=@CusID ", "order by ID" };

        protected string[] General_manage_1 ={ "id", "id,Cname,gType,URL,EmailURL,isLock,SiteID", "news_Gen where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };

        protected string[] General_manage_2 ={ "id", "id,Cname,gType,URL,EmailURL,isLock,SiteID", "news_Gen where SiteID='" + Foosun.Global.Current.SiteID + "' and gType=@gType", "order by ID desc" };

        protected string[] user_discuss_discussphoto ={ "id", "id,PhotoID,PhotoName,PhotoTime,UserNum,PhotoContent,PhotoalbumID,PhotoUrl", "user_photo where PhotoalbumID=@PhotoalbumID", "order by ID desc" };

        protected string[] manage_news_SortPage ={ "id", "id,ClassID,ClassCName,ClassEname,ParentID,OrderID", "News_Class where isRecyle<>1 and ParentID='0' and ModelID='0'", "order by OrderID desc" };
        //自由标签
        protected string[] manage_label_FreeLabel_List ={ "id", "id,LabelID,LabelName,LabelSQL,StyleContent,Description,CreatTime,SiteID", "sys_LabelFree where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };
        //PSF
        protected string[] manage_publish_psf ={ "id", "id,psfID,psfName,LocalDir,RemoteDir,isSub,CreatTime,isRecyle,SiteID", "sys_PSF where isRecyle=0 and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };
        //计划任务
        protected string[] manage_publish_siteTask ={ "id", "id,taskID,TaskName,isIndex,ClassID,News,TimeSet,CreatTime,SiteID", "sys_SiteTask where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };

        protected string[] Manage_Stat_View_1 ={ "id", "id,statid,classname", "stat_class where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };

        protected string[] Manage_Stat_View_2 ={ "id", "id,vtime,vwhere,vwidth,vOS,vsoft,vpage", "stat_Info where classid=@viewid and SiteID=@SiteID ", "order by ID desc" };
        //常规列表 
        protected string[] configuration_system_Genlist_1 ={ "id", "Cname", "News_Gen where gType=@gType and isLock=0 and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by Id desc" };
        //友情连接------------------后台分类
        protected string[] manage_Friend_Friend_List_1 ={ "id", "id,ClassID,ClassCName,Content", "friend_class where ParentID='0' and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };

        protected string[] manage_Friend_Friend_List_2 ={ "id", "id,Name,ClassID,Type,Author,Url,Lock,isAdmin", "friend_link where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by ID desc" };
        //友情连接------------------前台
        protected string[] user_friend_friend ={ "id", "id,Name,ClassID,Type,Author,Url,Lock,isAdmin", "friend_link where Author='" + Foosun.Global.Current.UserNum + "'", "order by ID desc" };
        //归档
        protected string[] manage_news_History_Manage ={ "id", "id,NewsTitle,NewsType,Author,Souce,oldTime,CheckStat,isLock,DataLib", "old_News", "order by ID desc" };
        //调查
        protected string[] manage_survey_ManageVote ={ "Rid", "Rid,IID,TID,OtherContent,VoteIp,VoteTime,UserNumber,SiteID", "vote_Manage where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by RID desc" };

        protected string[] manage_survey_setClass_1 ={ "vid", "vid,ClassName,Description", "vote_Class WHERE SiteID='" + Foosun.Global.Current.SiteID + "'", "order by vid desc" };

        protected string[] manage_survey_setClass_2 ={ "vid", "vid,ClassName,Description", "vote_Class where VID like cstr(@VID) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by vid desc" };

        protected string[] manage_survey_setClass_3 ={ "vid", "vid,ClassName,Description", "vote_Class where ClassName like cstr(@ClassName) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by vid desc" };

        protected string[] manage_survey_setClass_4 ={ "vid", "vid,ClassName,Description", "vote_Class where Description like cstr(@Description) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by vid desc" };
        //调查选项
        protected string[] manage_survey_setItem_1 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where TID like cstr(@TID) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_2 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_3 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where ItemName like cstr(@ItemName) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_4 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where ItemValue like cstr(@ItemValue) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_5 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where PicSrc like cstr(@PicSrc) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_6 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where DisColor like cstr(@DisColor) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_7 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where VoteCount like cstr(@VoteCount) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setItem_8 ={ "TID", "TID,IID,ItemName,ItemValue,ItemMode,PicSrc,DisColor,VoteCount,ItemDetail", "vote_Item where ItemDetail like cstr(@ItemDetail) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };
        //多步投票管理
        protected string[] manage_survey_setSteps_1 ={ "SID", "SID,TIDS,Steps,TIDU", "vote_Steps where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by sid desc" };

        protected string[] manage_survey_setSteps_2 ={ "SID", "SID,TIDS,Steps,TIDU", "vote_Steps where SID like cstr(@SID) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by sid desc" };

        protected string[] manage_survey_setSteps_3 ={ "SID", "SID,TIDS,Steps,TIDU", "vote_Steps where TIDS like cstr(@TIDS) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by sid desc" };

        protected string[] manage_survey_setSteps_4 ={ "SID", "SID,TIDS,Steps,TIDU", "vote_Steps where Steps like cstr(@Steps) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by sid desc" };

        protected string[] manage_survey_setSteps_5 ={ "SID", "SID,TIDS,Steps,TIDU", "vote_Steps where TIDU like cstr(@TIDU) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by sid desc" };
        //投票主题
        protected string[] manage_survey_setTitle_1 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setTitle_2 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where Title like cstr(@Title) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setTitle_3 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where VID like cstr(@VID) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setTitle_4 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where MaxNum like cstr(@MaxNum) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setTitle_5 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where StartDate like cstr(@StartDate) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setTitle_6 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where EndDate like cstr(@EndDate) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };

        protected string[] manage_survey_setTitle_7 ={ "TID", "TID,VID,Title,Type,MaxNum,DisMode,StartDate,EndDate,ItemMode", "vote_Title where ItemMode like cstr(@ItemMode) and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by TID desc" };
        //自定义字段
        protected string[] manage_Sys_DefineTable_Manage = { "DefineId", "DefineId,DefineInfoId,DefineName,ParentInfoId", "define_class where ParentInfoId='0' and SiteID='" + Foosun.Global.Current.SiteID + "'", "order by DefineId desc" };
        //前台显示文章
        protected string[] user_ShowUser_1 = { "id", "ConID,id,Title,creatTime,ClassID", "user_Constr where UserNum=@UserNum and ClassID=@ClassID and isuserdel=0", "order by Id desc" };

        protected string[] user_ShowUser_1_1 = { "id", "ConID,id,Title,creatTime,ClassID", "user_Constr where UserNum=@UserNum and isuserdel=0", "order by Id desc" };

        protected string[] user_ShowUser_2 ={ "id", "id,PhotoalbumName,PhotoalbumID,pwd", "User_Photoalbum where isDisPhotoalbum=0 and UserName=@UserNum", "order by ID desc" };
        //显示相册

        protected string[] user_show_showphoto ={ "id", "id,PhotoID,PhotoName,PhotoTime,UserNum,PhotoContent,PhotoalbumID,PhotoUrl", "user_photo where PhotoalbumID=@PhotoalbumID", "order by ID desc" };

        //模型列表
        protected string[] manage_channel_list ={ "id", "id,channelName,channelDescript,channelItem,channelEItem,islock,isHTML,DataLib,issys,binddomain", "sys_channel", "order by ID desc" };

        protected string[] manage_channel_value ={ "id", "id,OrderID,CName,EName,ChID,islock,isUser,vType,vLength,fieldLength,isNulls,isSearch,vHeight,HTMLedit", "sys_channelvalue where ChID=@ChID", "order by OrderID desc,id DESC" };

        //频道管理
        protected string[] manage_channel_class_list ={ "id", "id,ChID,ParentID,OrderID,classCName,classEName,isPage,isLock,isDelPoint", "sys_channelclass where ParentID=@ParentID and ChID=@ChID", "order by OrderID desc,id DESC" };
        protected string[] manage_channel_Special_list ={ "id", "id,ChID,ParentID,OrderID,specialCName,specialEName,isRec,isLock", "sys_channelspecial where ParentID=@ParentID and ChID=@ChID", "order by OrderID desc,id DESC" };
        //自定义表单管理
        protected string[] manage_sys_customform ={ "id", "id,formname,formtablename,memo", "customform", "order by id DESC" };
        //自定义表单字段
        protected string[] manage_sys_customform_item ={ "id", "id,formid,seriesnumber,fieldname,itemname,itemtype,iif(isnotnull=1,'是','否') as notnull", "customform_item where formid=@formid", "order by seriesnumber" };
        public DataTable GetPage(string PageName, int PageIndex, int PageSize, out int RecordCount, out int PageCount, params SQLConditionInfo[] SqlCondition)
        {
            string PageType = PageName.Substring(0, PageName.Length - 5);

            string _dirName = Foosun.Config.UIConfig.dirMana.ToLower();

            if (!_dirName.Equals("manage"))
            {
                if (PageType.IndexOf(_dirName) != -1)
                {
                    PageType = PageType.Substring(_dirName.Length, PageType.Length - _dirName.Length);
                    PageType = "manage" + PageType;
                }
            }

            FieldInfo fi = this.GetType().GetField(PageType, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (fi == null)
                throw new Exception("没有找到SQL");
            try
            {
                string[] Sql = (string[])fi.GetValue(this);
                string IndexField = Sql[0];
                string AllFields = Sql[1];
                string Condition = Sql[2];
                string OrderFields = Sql[3];
                Condition = Pre + Condition;
                if (PageType.Equals("manage_label_style_3"))
                {
                    Condition = Condition.Replace("@Keyword", "'" + SqlCondition[1].value.ToString() + "'");
                }
                if (Condition.ToLower().IndexOf(" join ") > 0)
                    Condition = Condition.Replace(" join ", " join " + Pre);
                List<OleDbParameter> param = new List<OleDbParameter>();
                if (SqlCondition != null && SqlCondition.Length > 0)
                {
                    int n = SqlCondition.Length;
                    for (int i = 0; i < n; i++)
                    {
                        if (SqlCondition[i].name != null && SqlCondition[i].name != string.Empty)
                        {
                            OleDbParameter p = new OleDbParameter(SqlCondition[i].name, OleDbType.Variant);
                            p.Value = SqlCondition[i].value;
                            param.Add(p);
                        }
                    }
                }
                //return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
                return DbHelper.ExecutePage(AllFields, Condition, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, Database.getNewParam(param.ToArray(), Database.getSqlParam(Condition)));
            }
            //catch (OleDbException e)
            //{
            //    throw e;
            //}
            catch (Exception e)
            {
                throw new Exception("用于分页的SQL语句无效:" + e.Message);
            }

        }
    }
}