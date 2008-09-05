using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IPsframe
    {
        void Del_PSF(string Psfid);
        void DelAll_PSF();
        void InsertPSF(PSF uc);
        int UpdatePSF(PSF uc);
        DataTable getTitleRecord(string psfName);
        DataTable getPSFParam(string psfID);
        int IsExitPSFID(string PSFID);
        int DelOneTask(string taskid);
        void DelPTask(string boxs);
        int DelAllTask();
        DataTable getTaskParam(string TaskID);
        DataTable getTaskName(string TaskName);
        void insertTask(Foosun.Model.Task uc);
        void UpdateTask(Foosun.Model.Task uc);
        DataTable getTaskIDInfo(string TaskID);
    }

    public sealed partial class DataAccess
    {
        public static IPsframe CreatePsframe()
        {
            string className = path + ".Psframe";
            return (IPsframe)Assembly.Load(path).CreateInstance(className);
        }
    }
}
