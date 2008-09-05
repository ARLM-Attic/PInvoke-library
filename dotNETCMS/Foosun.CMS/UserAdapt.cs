using System;
using System.Collections.Generic;
using System.Text;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class UserAdapt
    {
        Foosun.DALFactory.IUserAdapt dal;
        public UserAdapt()
        {
            dal = DataAccess.CreateUserAdapt();
        }

        public bool isExist(string username)
        {
            return dal.isExist(username);
        }

        public string getUserNumByUserName(string username)
        {
            return dal.getUserNumByUserName(username);
        }
    }
}
