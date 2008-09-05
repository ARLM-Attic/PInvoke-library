using System;
using System.Collections.Generic;
using System.Text;

namespace Foosun.Model
{
    /// <summary>
    /// 用户登录的状态
    /// </summary>
    public enum EnumLoginState
    {
        /// <summary>
        /// session超时
        /// </summary>
        Err_TimeOut,
        /// <summary>
        /// 管理员超时
        /// </summary>
        Err_AdminTimeOut,
        /// <summary>
        /// 用户被锁定
        /// </summary>
        Err_Locked,
        /// <summary>
        /// 管理员锁定
        /// </summary>
        Err_AdminLocked,
        /// <summary>
        /// 用户编号不存在
        /// </summary>
        Err_UserNumInexistent,
        /// <summary>
        /// 管理员表编号不存在
        /// </summary>
        Err_AdminNumInexistent,
        /// <summary>
        /// 用户IP被限制
        /// </summary>
        Err_IPLimited,
        /// <summary>
        /// 用户权限不足
        /// </summary>
        Err_NoAuthority,
        /// <summary>
        /// 发生数据库异常
        /// </summary>
        Err_DbException,
        /// <summary>
        /// 登录状态正常
        /// </summary>
        Succeed,
        /// <summary>
        /// 未通过电子邮件激活
        /// </summary>
        Err_UnEmail,
        /// <summary>
        /// 未通过手机激活
        /// </summary>
        Err_UnMobile,
        /// <summary>
        /// 管理员是否登陆
        /// </summary>
        Err_AdminLogined,
        /// <summary>
        /// 未通过实名认证
        /// </summary>
        Err_UnCert,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        Err_NameOrPwdError,
        /// <summary>
        /// 非管理员
        /// </summary>
        Err_NotAdmin,
        /// <summary>
        /// 连续登录错误锁定
        /// </summary>
        Err_DurativeLogError,
        /// <summary>
        /// 会员组超期
        /// </summary>
        Err_GroupExpire
        
        
    }
}
