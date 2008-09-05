using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.Config;

namespace Foosun.Publish
{
    /// <summary>
    /// 有关标签中样式的处理类
    /// </summary>
    public class LabelStyle
    {
        private string original = string.Empty;
        //private bool iscustom = false; 
        public LabelStyle(string style)
        {
            original = style;
        }
        /// <summary>
        /// 根据样式的ID取出其内容(普通)
        /// </summary>
        /// <param name="id">标签的12位ID</param>
        /// <param name="cn">数据库连接</param>
        /// <returns>如果没有找到或内容为null返回string.Empty,否则返回该内容</returns>
        public static string GetStyleByID(string id)
        {
            return CommonData.DalPublish.GetStyleContent(id);
        }

        /// <summary>
        /// 根据样式的ID取出其内容(频道)
        /// </summary>
        /// <param name="id">标签的12位ID</param>
        /// <param name="cn">数据库连接</param>
        /// <returns>如果没有找到或内容为null返回string.Empty,否则返回该内容</returns>
        public static string GetCHStyleByID(int id, int ChID)
        {
            return CommonData.DalPublish.GetCHStyleContent(id, ChID);
        }
    }
    /*
    public class FixStyle
    {
        
        public static string GetFiled(string name)
        {
            return "";
           
      private static string[] Style_Filed ={  "{@Title}","",
          "{@sTitle}","",
          "{@URL}","",
    "{@Content}","",
    
          
          //发布日期
    //{@Date:Year02}；{@Date:Year04}
    //{@Date:Month}；{@Date:Day}；{@Date:Hour}；
    //{@Date:Minute}；{@Date:Second}
    "{@Click}","",
    "{@Source}","",
    "{@Author}","",
    "{@Editor}","",
    "{@Picture}","",
    //栏目名称：{@ClassName}
    //栏目连接路径：{@ClassPath}
    //专题名称：{@SpecialName}
    //专题连接：{@SpecialPath}
    "{@Tags}","",
    //评论表单：{@CommForm}
    //总评论数：{@CommCount}
    //最新评论：{@LastComm}
    //最新讨论：{@LastGroup}
    //总讨论数：{@GroupCount}
    //发送给好友：{@SendInfo}
    //收藏：{@Collection}
    //打印：{@Print }
    //分页样式(栏目也可以采用此标签)：
    //首页连接：{@IndexLink}
    //最后一页连接：{@_EndLink}
    //上一页连接：{@PreLink}
    //下一页连接：{@NextLink}
    //新闻页总数：{@Count}	
    //新闻页当前页：{@CurrentNews}
    //中间页循环显示方式：
    //{NewsPage:Loop =显示多少页,显示样式}
    //       显示多少页：1-10的数字
    //显示样式：0表示,1,2,3,4,5….,   1表示一,二,三,四,五,2表示背景图片
    //{/NewsPage:Loop}
    //上一篇：{@PrePage}
    //下一篇：{@NextPage}
    //Digg(数量) {@TopNum}
    //Digg(连接地址) {@TopURL}
    //附件{@NewsFiles}
    //视频播放地址{@NewsvURL}
    //参看： XML/CuslabeStyle/cstylebase.xml
    //	栏目的固定标签
    //{@class_Name} 栏目中文名称
    //{@class_EName} 栏目英文名称
    //{@class_Path} 栏目的访问路径
    //{@class_Navi} 栏目导读
    //{@class_NaviPic} 栏目导读图片地址
    //{@class_Keywords} 栏目meta关键字
    //{@class_Descript} 栏目meta描述
    //{@class_DefineName} 栏目自定义副本。可变
    //	专题固定标签
    //{@special_Name} 专题中文名称
    //{@special_Ename} 专题英文名称
    //{@special_Path}专题连接路径
    //{@special_NaviWords}专题导航文字
    //{@special_NaviPic}专题导航图片地址
        }
    }*/
}
