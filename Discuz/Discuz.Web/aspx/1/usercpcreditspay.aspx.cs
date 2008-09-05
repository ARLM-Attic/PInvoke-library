using System;
using System.Data;
using System.Data.SqlClient;
using Discuz.Common;
using Discuz.Forum;
using Discuz.Web.UI;
using Discuz.Entity;
using Discuz.Config;

namespace Discuz.Web
{
    /// <summary>
    /// 积分兑换
    /// </summary>
    public class usercpcreditspay : PageBase
    {
        #region 页面变量

        /// <summary>
        /// 扩展积分列表
        /// </summary>
        public DataTable extcreditspaylist;

        /// <summary>
        /// 积分交易税
        /// </summary>
        public float creditstax;

        /// <summary>
        /// 积分计算器js脚本
        /// </summary>
        public string jscreditsratearray;

        /// <summary>
        /// 当前用户信息
        /// </summary>
        public UserInfo user = new UserInfo();

        /// <summary>
        /// 可用的积分名称列表
        /// </summary>
        public string[] score;

        #endregion

        private float extcredits1rate, extcredits2rate;

        protected override void ShowPage()
        {
            pagetitle = "用户控制面板";

            creditstax = Scoresets.GetCreditsTax();
            extcreditspaylist = Scoresets.GetScorePaySet(0);
            score = Scoresets.GetValidScoreName();

            jscreditsratearray = "<script type=\"text/javascript\">\r\nvar creditsrate = new Array();\r\n";
            foreach (DataRow dr in extcreditspaylist.Rows)
            {
                jscreditsratearray = jscreditsratearray + "creditsrate[" + dr["id"].ToString() + "] = " +
                                     dr["rate"].ToString() + ";\r\n";
            }
            jscreditsratearray = jscreditsratearray + "\r\n</script>";

            if (userid == -1)
            {
                AddErrLine("你尚未登录");

                return;
            }
            user = Users.GetUserInfo(userid);
            if (DNTRequest.IsPost())
            {
                if (ForumUtils.IsCrossSitePost())
                {
                    AddErrLine("您的请求来路不正确，无法提交。如果您安装了某种默认屏蔽来路信息的个人防火墙软件(如 Norton Internet Security)，请设置其不要禁止来路信息后再试。");
                    return;
                }

                if (Utils.MD5(DNTRequest.GetString("password")) != password)
                {
                    AddErrLine("密码错误");
                    return;
                }

                int paynum = DNTRequest.GetInt("paynum", 0);
                if (paynum <= 0)
                {
                    AddErrLine("数量必须是大于0的整数");
                    return;
                }

                int extcredits1 = DNTRequest.GetInt("extcredits1", 0);
                int extcredits2 = DNTRequest.GetInt("extcredits2", 0);
                if (extcredits1 < 1 || extcredits2 < 1 || extcredits1 > 8 || extcredits2 > 8)
                {
                    AddErrLine("请正确选择要兑换的积分类型!");
                    return;
                }
                if (extcredits1 == extcredits2)
                {
                    AddErrLine("不能兑换相同类型的积分");
                    return;
                }

                //对交易后的积分增减进行修改设置
                UserExtcreditsInfo extcredits1info = Scoresets.GetScoreSet(extcredits1);
                UserExtcreditsInfo extcredits2info = Scoresets.GetScoreSet(extcredits2);

                if ((extcredits1info.Name.Trim() == "") || (extcredits2info.Name.Trim() == ""))
                {
                    AddErrLine("错误的输入!");
                    return;
                }


                UserInfo __userinfo = Users.GetUserInfo(userid);
                if ((Users.GetUserExtCredits(userid, extcredits1) - paynum) < Scoresets.GetExchangeMinCredits())
                {
                    AddErrLine("抱歉, 您的 \"" + extcredits1info.Name + "\" 不足." +
                               Scoresets.GetExchangeMinCredits().ToString());
                    return;
                }

                //计算并更新2个扩展积分的新值
                extcredits1rate = extcredits1info.Rate;
                extcredits2rate = extcredits2info.Rate;
                float extcredit2paynum =
                    (float) Math.Round(paynum*(extcredits1rate/extcredits2rate)*(1 - creditstax), 2);
                Users.UpdateUserExtCredits(userid, extcredits1, paynum*-1);
                Users.UpdateUserExtCredits(userid, extcredits2, extcredit2paynum);
                CreditsLogs.AddCreditsLog(userid, userid, extcredits1, extcredits2, paynum, extcredit2paynum,
                                          Utils.GetDateTime(), 1);

                SetUrl("usercpcreaditstransferlog.aspx");
                SetMetaRefresh();
                SetShowBackLink(false);
                AddMsgLine("积分兑换完毕, 正在返回积分兑换与转帐记录");
            }
        }
    } //class end
}