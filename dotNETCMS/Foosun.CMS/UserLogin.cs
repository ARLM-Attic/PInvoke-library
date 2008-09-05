using System;
using System.Collections.Generic;
using System.Text;
using Foosun.DALFactory;
using Foosun.Model;

namespace Foosun.CMS
{
    public class UserLogin
    {
        private IUserLogin dal;

        public UserLogin()
        {
            dal = DataAccess.CreateUserLogin();
        }
        /// <summary>
        /// 检查普通会员登录状态
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="IsCert"></param>
        /// <param name="LimitedIP"></param>
        /// <returns></returns>
        public EnumLoginState CheckUserLogin(string UserNum, bool IsCert)
        {
            return dal.CheckUserLogin(UserNum, IsCert);
        }
        /// <summary>
        /// 检查管理员登录状态
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="LimitedIP"></param>
        /// <returns></returns>
        public EnumLoginState CheckAdminLogin(string UserNum)
        {
            return dal.CheckAdminLogin(UserNum);
        }
        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="PopCode"></param>
        /// <param name="ClassID"></param>
        /// <param name="SpecialID"></param>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        public EnumLoginState CheckAdminAuthority(string PopCode, string ClassID, string SpecialID, string SiteID,string adminLogined)
        {
            return dal.CheckAdminAuthority(PopCode, ClassID, SpecialID, SiteID, adminLogined);
        }
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public EnumLoginState AdminLogin(string UserName, string PassWord, out GlobalUserInfo info)
        {
            return dal.AdminLogin(UserName, PassWord, out info);
        }
        /// <summary>
        /// 个人用户登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public EnumLoginState PersonLogin(string UserName, string PassWord, out GlobalUserInfo info)
        {
            return dal.PersonLogin(UserName, PassWord, out info);
        }
        public int GetLoginSpan()
        {
            return dal.GetLoginSpan();
        }
    }
}
