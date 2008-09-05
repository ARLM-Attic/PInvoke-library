using System;
using System.Data;
using System.Reflection;
using Foosun.Model;

namespace Foosun.DALFactory
{
    public interface IUserLogin
    {
        /// <summary>
        /// 检查普通会员登录状态
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="IsCert"></param>
        /// <param name="LimitedIP"></param>
        /// <returns></returns>
        EnumLoginState CheckUserLogin(string UserNum, bool IsCert);
        /// <summary>
        /// 检查管理员登录状态
        /// </summary>
        /// <param name="UserNum"></param>
        /// <param name="LimitedIP"></param>
        /// <returns></returns>
        EnumLoginState CheckAdminLogin(string UserNum);
        /// <summary>
        /// 检查管理员权限
        /// </summary>
        /// <param name="PopCode"></param>
        /// <param name="ClassID"></param>
        /// <param name="SpecialID"></param>
        /// <param name="SiteID"></param>
        /// <returns></returns>
        EnumLoginState CheckAdminAuthority(string PopCode, string ClassID, string SpecialID, string SiteID, string adminLogined);
        /// <summary>
        /// 管理员登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        EnumLoginState AdminLogin(string UserName, string PassWord, out GlobalUserInfo info);
        /// <summary>
        /// 个人用户登录
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        EnumLoginState PersonLogin(string UserName, string PassWord, out GlobalUserInfo info);
        /// <summary>
        /// 登录错误的禁止登录时间
        /// </summary>
        /// <returns></returns>
        int GetLoginSpan();
    }
    public sealed partial class DataAccess
    {
        public static IUserLogin CreateUserLogin()
        {
            string className = path + ".UserLogin";
            return (IUserLogin)Assembly.Load(path).CreateInstance(className);
        }
    }
}