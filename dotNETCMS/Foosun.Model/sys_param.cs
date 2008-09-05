using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    class sys_param
    {
    }

    public struct STsys_param
    {
            public string Str_SiteName;//站点名称
            public string Str_SiteDomain;//站点域名
            public string Str_IndexTemplet;//首页摸板路径
            public string Str_Txt_IndexFileName;//首页生成文件名
            public string Str_FileEXName;//默认的扩展名（主站）
            public string Str_ReadNewsTemplet;//新闻浏览页摸板
            public string Str_ClassListTemplet;//新闻栏目页摸板
            public string Str_SpecialTemplet;//新闻专题页摸板
            #region 前台浏览方式
            public int readtypef;
            #endregion
            public string Str_LoginTimeOut;//后台登陆过期时间
            public string Str_Email;//管理员信箱
            #region 站点采用路径方式
            public int linktypef;
            #endregion
            public string Str_BaseCopyRight;//版权信息
            public string Str_CheckInt;//新闻后台审核机制
            #region 图片防盗
            public int picc;
            #endregion
            public string Str_LenSearch;//关键字长度
            public string InsertPicPosition;
            public int HistoryNum;
            #region 检测栏目标题
            public int chetitle;
            #endregion
            #region 防采集
            public int collect;
            #endregion
            public string Str_SaveClassFilePath;//生成栏目文件保存路径
            public string Str_SaveIndexPage;//生成索引页规则
            public string Str_SaveNewsFilePath;//新闻文件命名规则
            public string Str_SaveNewsDirPath;//新闻文件保存路径
            public string Str_Pram_Index;//每页显示数量
           //-----------------------------------------会员
            public string Str_RegGroupNumber;//默认会员组
            #region 投稿状态
            public int constrr;
            #endregion
            #region 注册
            public int reg;
            #endregion
            #region 验证码
            public int code;
            #endregion
            #region 评论验证
            public int diss;
            public int CommCheck;
            #endregion
            #region 群发
            public int senemessage;
            #endregion
            #region 匿名
            public int n;
            #endregion
            #region html编辑器
            public int htmls;
            #endregion
            public string Str_Commfiltrchar;//评论过滤
            public string Str_IPLimt;
            public string Str_GpointName;//G币
            public string Str_LoginLock;//锁定
            public string Str_setPoint;//注册获得的积分金币
            public string Str_cPointParam;//魅力值增加
            public string Str_aPointparam;//活跃值增加
            public string Str_RegContent;//注册协议
            #region 会员注册参数
            public string strContent;
            public int returnemail;
            public int returnmobile;
            #endregion
            #region 冲值类型
            public int ghclass;
            #endregion
            //----------会员等级设置----------
            public string[] Str_LtitleArr;//存取名称数组
            public string[] Str_LpicurlArr;//存取头像数组
            public string[] Str_iPointArr;//存取金币数组
            //------------------上传，分组刷新
            #region 图片路径域名
            public int picsa;
            #endregion
            public string Str_PicServerDomain;//域名
            public string Str_PicUpLoad;//图片附件目录
            public string Str_UpfilesType;//上传格式
            public string Str_UpFilesSize;//上传大小
            #region 域名
            public int domainnn;
            #endregion
            public string Str_RemoteDomain;//远程图片域名
            public string Str_RemoteSavePath;//远程图片保存路径
            #region 分组刷新
            public string Str_ClassListNum;//列表每次刷新数
            public string Str_NewsNum;//信息每次刷新数
            public string Str_BatDelNum;//批量删除数
            public string Str_SpecialNum;//专题每次刷新数
            #endregion
            //--------js,ftp--------------------
            public string Str_HotJS;
            public string Str_LastJS;
            public string Str_RecJS;
            public string Str_HoMJS;
            public string Str_TMJS;
            public int ftpp;
            public string Str_FTPIP;
            public string Str_Ftpport;
            public string Str_FtpUserName;
            public string Str_FTPPASSword;//字符串加密方式写入数据库
            //-----水印，缩图-------------------
            #region 是否开启水印/缩图
            public int water;
            #endregion
            public string Str_PrintPicTF;//类型
            public string Str_PrintWord;//文字水印
            public string Str_Printfontsize;//字体大小
            public string Str_Printfontfamily;//字体
            public string Str_Printfontcolor;//水印颜色
            public string Str_PrintBTF;//文字是否加粗
            public string Str_PintPicURL;//图片水印路径
            public string Str_PrintPicsize;//图片水印大小
            public string Str_PintPictrans;//透明度
            public string Str_PrintPosition;//位置
            public string Str_PrintSmallTF;//是否开启缩图
            public string Str_PrintSmallSizeStyle;//缩图方式
            public string Str_PrintSmallSize;//缩图大小
            public string Str_PrintSmallinv;//缩图比例
            //-----RSS,WAP----------------------
            public string Str_RssNum;//显示范围
            public string Str_RssContentNum;//截取数
            public string Str_RssTitle;//标题
            public string Str_RssPicURL;//地址
            #region 加入WAP
            public int wapp;
            #endregion
            public string Str_WapPath;//WAP路径
            public string Str_WapDomain;//WAP域名
            public string Str_WapLastNum;//WAP数
        }
}
