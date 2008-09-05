using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IUserAdapt
    {
        bool isExist(string username);
        string getUserNumByUserName(string username);
    }

    public sealed partial class DataAccess
    {
        public static IUserAdapt CreateUserAdapt()
        {
            string className = path + ".UserAdapt";
            return (IUserAdapt)Assembly.Load(path).CreateInstance(className);
        }       
    }
}
