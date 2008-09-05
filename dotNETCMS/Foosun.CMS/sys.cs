//===========================================================
//==     (c)2007 Foosun Inc. by dotNETCMS 1.0              ==
//==             Forum:bbs.foosun.net                      ==
//==            website:www.foosun.net                     ==
//==     Address:NO.109 HuiMin ST.,Chengdu ,China          ==
//==         TEL:86-28-85098980/66026180                   ==
//==         qq:655071,MSN:ikoolls@gmail.com               ==
//==               Code By Simplt.Xie                      ==
//===========================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class sys
    {
        Foosun.DALFactory.Isys dal;
        public sys()
        {
            dal = DataAccess.Createsys();
        }
        /// <summary>
        /// 检查新闻表数量
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableRecord()
        {
            DataTable dt = dal.GetTableRecord();
            return dt;
        }
        /// <summary>
        /// 检查新闻表数量
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableExsit(string TableName)
        {
            DataTable dt = dal.GetTableExsit(TableName);
            return dt;
        }

        /// <summary>
        /// 插入新闻表记录
        /// </summary>
        /// <returns></returns>
        public void InsertTableLab(string TableName)
        {
            dal.InsertTableLab(TableName);
        }

        /// <summary>
        /// 创建新闻表结构
        /// </summary>
        /// <returns></returns>
        public void CreatTableData(string TableName)
        {
            dal.CreatTableData(TableName);
        }

        /// <summary>
        /// 检查新闻表数量
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableList()
        {
            DataTable dt = dal.GetTableList();
            return dt;
        }

        #region 常规管理
        /// <summary>
        /// 单个删除常规管理
        /// </summary>
        /// <param name="Gid"></param>
        public void General_M_Del(int Gid)
        {
            dal.General_M_Del(Gid);
        }

        /// <summary>
        /// 单个锁定常规管理
        /// </summary>
        /// <param name="Gid"></param>
        public void General_M_Suo(int Gid)
        {
            dal.General_M_Suo(Gid);
        }       
 
        /// <summary>
        /// 单个解锁常规管理
        /// </summary>
        /// <param name="Gid"></param>
        public void General_M_UnSuo(int Gid)
        {
            dal.General_M_UnSuo(Gid);
        }     
 
        /// <summary>
        /// 删除全部
        /// </summary>
        /// <param name="Gid"></param>
        public void General_DelAll()
        {
            dal.General_DelAll();
        }

        /// <summary>
        /// 获取标题是否重复
        /// </summary>
        /// <param name="Cname"></param>
        /// <returns></returns>
        public DataTable GetGeneralRecord(string Cname)
        {
            DataTable dt = dal.GetGeneralRecord(Cname);
            return dt;
        }

        /// <summary>
        /// 插入记录
        /// </summary>
        /// <param name="Gid"></param>
        public void insertGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email)
        {
            dal.insertGeneral(_Sel_Type, _Name, _LinkUrl, _Email);
        }

        public void UpdateGeneral(string _Sel_Type, string _Name, string _LinkUrl, string _Email, int GID)
        {
            dal.UpdateGeneral(_Sel_Type, _Name, _LinkUrl, _Email,GID);
        }

        /// <summary>
        /// 修改页面，获取值
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        public DataTable getGeneralIdInfo(int GID)
        {
            DataTable dt = dal.getGeneralIdInfo(GID);
            return dt;
        }

        #endregion 常规管理
        #region 参数设置
        public DataTable UserGroup()//取会员组
        {
            return dal.UserGroup();
        }
        public DataTable UserReg()//会员注册
        {
            return dal.UserReg();
        }
        public DataTable BasePramStart()//初始化基本参数设置
        {
            return dal.BasePramStart();
        }
        public DataTable FtpRss()//FTP，RSS初始化
        {
            return dal.FtpRss();
        }
        public DataTable UserPram()//会员参数设置
        {
            return dal.UserPram();
        }
        public DataTable UserLeavel()//会员等级设置
        {
            return dal.UserLeavel();
        }
        public DataTable WaterStart()//水印初始化
        {
            return dal.WaterStart();
        }
        public int Update_BaseInfo(STsys_param sys)//修改参数设置
        {
            return dal.Update_BaseInfo(sys);
        }
        public int Update_UserRegInfo(STsys_param sys)//修改注册参数
        {
            return dal.Update_UserRegInfo(sys);
        }
        public int Update_UserInfo(STsys_param sys)//修改会员参数
        {
            return dal.Update_UserInfo(sys);
        }
        public int Update_Leavel(STsys_param sys, int k)//修改会员参数
        {
            return dal.Update_Leavel(sys, k);
        }
        public int Update_FtpInfo(STsys_param sys)//修改上传部分
        {
            return dal.Update_FtpInfo(sys);
        }
        public int Update_JS(STsys_param sys)//JS 
        {
            return dal.Update_JS(sys);
        }
        public int Update_JFtP(STsys_param sys)//ftp
        {
            return dal.Update_JFtP(sys);
        }
        public int Update_Water(STsys_param sys)//水印
        {
            return dal.Update_Water(sys);
        }
        public int Update_RssWap(STsys_param sys)//wap.rss
        {
            return dal.Update_RssWap(sys);
        }
        public DataTable ShowJS1()
        {
            return dal.ShowJS1();
        }
        public DataTable ShoeJs2()
        {
            return dal.ShoeJs2();
        }
        public DataTable showJs3()
        {
            return dal.showJs3();
        }
        public DataTable JsTemplet1()
        {
            return dal.JsTemplet1();
        }
        public DataTable JsTemplet2()
        {
            return dal.JsTemplet2();
        }
        public DataTable JsTemplet3()
        {
            return dal.JsTemplet3();
        }
        public DataTable JsTemplet4()
        {
            return dal.JsTemplet4();
        }
        public DataTable JsTemplet5()
        {
            return dal.JsTemplet5();
        }
        #endregion
    }
}
