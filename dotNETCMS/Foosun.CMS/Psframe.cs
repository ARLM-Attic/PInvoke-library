using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class Psframe
    {
        Foosun.DALFactory.IPsframe dal;
        public Psframe()
        {
            dal = DataAccess.CreatePsframe();
        }
        /// <summary>
        /// 删除PSF到回收站
        /// </summary>
        /// <returns></returns>
        public void Del_PSF(string Psfid)
        {
            dal.Del_PSF(Psfid);
        } 
        
        /// <summary>
        /// 删除PSF到回收站
        /// </summary>
        /// <returns></returns>
        public void DelAll_PSF()
        {
            dal.DelAll_PSF();
        }
        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <returns></returns>
        public DataTable getPSFParam(string psfID)
        {
            return dal.getPSFParam(psfID);
        }

        /// <summary>
        /// 检查是否重复
        /// </summary>
        /// <returns></returns>
        public DataTable getTitleRecord(string psfName)
        {
            DataTable dt = dal.getTitleRecord(psfName);
            return dt;
        }
        /// <summary>
        /// 创建PSF
        /// </summary>
        /// <param name="uc"></param>
        public void InsertPSF(Foosun.Model.PSF uc)
        {
            dal.InsertPSF(uc);
        }

        /// <summary>
        /// 修改PSF
        /// </summary>
        /// <param name="uc"></param>
        public int UpdatePSF(Foosun.Model.PSF uc)
        {
            return dal.UpdatePSF(uc);
        }
        
        public int IsExitPSFID(string PSFID)
        {
            return dal.IsExitPSFID(PSFID);
        }
        /// <summary>
        /// 删除单个task
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        public int DelOneTask(string taskid)
        {
            return dal.DelOneTask(taskid);
        }
        /// <summary>
        /// 批量删除任务
        /// </summary>
        /// <param name="boxs"></param>
        /// <returns></returns>
        public void DelPTask(string boxs)
        {
            dal.DelPTask(boxs);
        }
        /// <summary>
        /// 删除全部task
        /// </summary>
        /// <returns></returns>
        public int DelAllTask()
        {
            return dal.DelAllTask();
        }

        /// <summary>
        /// 检查Task是否重复
        /// </summary>
        /// <returns></returns>
        public DataTable getTaskParam(string TaskID)
        {
            return dal.getPSFParam(TaskID);
        }

        /// <summary>
        /// 检查Task名称是否重复
        /// </summary>
        /// <returns></returns>
        public DataTable getTaskName(string TaskName)
        {
            return dal.getTaskName(TaskName);
        }

        /// <summary>
        /// 创建Task
        /// </summary>
        /// <param name="uc"></param>
        public void insertTask(Foosun.Model.Task uc)
        {
            dal.insertTask(uc);
        } 
        
        /// <summary>
        /// 修改Task
        /// </summary>
        /// <param name="uc"></param>
        public void UpdateTask(Foosun.Model.Task uc)
        {
            dal.UpdateTask(uc);
        }
        /// <summary>
        /// 获取Task的 ID 信息
        /// </summary>
        /// <param name="TaskName"></param>
        /// <returns></returns>
        public DataTable getTaskIDInfo(string TaskID)
        {
            return dal.getTaskIDInfo(TaskID);
        }   
    }
}
