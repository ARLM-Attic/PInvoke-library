using System;
using System.Data;
using System.Web;
using Discuz.Common;
using Discuz.Config;
using Discuz.Forum;
using System.Data.Common;
using Discuz.Web.UI;
using Discuz.Entity;
using System.Text.RegularExpressions;

namespace Discuz.Web
{
    /// <summary>
    /// 购买主题页面类
    /// </summary>
    public class buytopic : PageBase
    {
        #region 页面变量
        /// <summary>
        /// 所要购买的主题信息
        /// </summary>
        public TopicInfo topic;
        /// <summary>
        /// 最后5个回复列表
        /// </summary>
        public DataTable lastpostlist;
        /// <summary>
        /// 已购买的支付记录列表
        /// </summary>
        public DataTable paymentloglist;
        /// <summary>
        /// 用户积分策略信息
        /// </summary>
        public UserExtcreditsInfo userextcreditsinfo;
        /// <summary>
        /// 所属版块Id
        /// </summary>
        public int forumid;
        /// <summary>
        /// 所属版块名称
        /// </summary>
        public string forumname;
        /// <summary>
        /// 主题Id
        /// </summary>
        public int topicid;
        /// <summary>
        /// 论坛导航信息
        /// </summary>
        public string forumnav;
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageid;
        /// <summary>
        /// 主题购买总次数
        /// </summary>
        public int buyers;
        /// <summary>
        /// 分页总数
        /// </summary>
        public int pagecount;
        /// <summary>
        /// 页码链接字串
        /// </summary>
        public string pagenumbers;
        /// <summary>
        /// 主题标题
        /// </summary>
        public string topictitle;
        /// <summary>
        /// 是否显示购买信息列表
        /// </summary>
        public int showpayments;
        /// <summary>
        /// 在判断此值等于1时显示点击购买主题后的确认购买界面
        /// </summary>
        public int buyit;
        /// <summary>
        /// 主题售价
        /// </summary>
        public int topicprice;
        /// <summary>
        /// 作者所得
        /// </summary>
        public float netamount;
        /// <summary>
        /// 单个主题最高收入
        /// </summary>
        public int maxincpertopic;
        /// <summary>
        /// 单个主题最高出售时限(小时)
        /// </summary>
        public int maxchargespan;
        /// <summary>
        /// 积分交易税
        /// </summary>
        public float creditstax;
        /// <summary>
        /// 主题售价
        /// </summary>
        public int price;
        /// <summary>
        /// 主题作者Id
        /// </summary>
        public int posterid;
        /// <summary>
        /// 主题作者用户名
        /// </summary>
        public string poster;
        /// <summary>
        /// 购买后余额
        /// </summary>
        public float userlastprice;
        public bool needlogin = false;

        public ShowtopicPagePostInfo postinfo;
        public string postmessage = "";
        public static Regex r = new Regex(@"\s*\[free\][\n\r]*([\s\S]+?)[\n\r]*\[\/free\]\s*", RegexOptions.IgnoreCase);
        #endregion

        private int ismoder = 0;
        private int pagesize = 16;
        private const string NO_PERMISSION = "您无权购买本主题";
        private const string UNKNOWN_REASON = "未知原因,交易无法进行,给您带来的不方便我们很抱歉";
        private const string NOT_ENOUGH_MONEY = "对不起,您的账户余额少于交易额,无法进行交易";
        private const string PURCHASE_SUCCESS = "购买主题成功,返回该主题";
        private const string WRONG_TOPIC = "无效的主题ID";
        private const string NOT_EXIST_TOPIC = "不存在的主题ID";
        private const string NOT_NEED_TO_PURCHASE = "此主题无需购买";
        private const string NOT_ENOUGH_MONEY_TO = "对不起,您的账户余额 <span class=\"bold\">{0}</span> 少于交易额 {1} ,无法进行交易";

        protected override void ShowPage()
        {
            topictitle = "";
            forumnav = "";

            //AdminGroupInfo admininfo = AdminGroups.GetAdminGroupInfo(useradminid);

            showpayments = DNTRequest.GetInt("showpayments", 0);
            buyit = DNTRequest.GetInt("buyit", 0);
            topicid = DNTRequest.GetInt("topicid", -1);
            // 如果主题ID非数字
            if (topicid == -1)
            {
                AddErrLine(WRONG_TOPIC);
                return;
            }

            // 获取该主题的信息
            TopicInfo topic = Topics.GetTopicInfo(topicid);
            // 如果该主题不存在
            if (topic == null)
            {
                AddErrLine(NOT_EXIST_TOPIC);
                return;
            }

            if (topic.Posterid == userid)
            {
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + base.ShowTopicAspxRewrite(topic.Tid, 0));
                return;
            }

            if (topic.Price <= 0)
            {
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + base.ShowTopicAspxRewrite(topic.Tid, 0));
                return;
            }

            topictitle = topic.Title.Trim();
            topicprice = topic.Price;
            poster = topic.Poster;
            posterid = topic.Posterid;
            pagetitle = topictitle.Trim();
            forumid = topic.Fid;
            ForumInfo forum = Forums.GetForumInfo(forumid);
            forumname = forum.Name.Trim();
            forumnav = ForumUtils.UpdatePathListExtname(forum.Pathlist.Trim(), config.Extname);
            postinfo = Posts.GetSinglePost(topicid);

            Match m;
            if (postinfo.Message.ToLower().Contains("[free]") || postinfo.Message.ToLower().Contains("[/free]"))
            {
                for (m = r.Match(postinfo.Message); m.Success; m = m.NextMatch())
                {
                    postmessage += "<br /><div class=\"msgheader\">免费内容:</div><div class=\"msgborder\">" + m.Groups[1].ToString() + "</div><br />";

                }

            }
            //判断是否为回复可见帖, price=0为非购买可见(正常), price>0 为购买可见, price=-1为购买可见但当前用户已购买
            price = 0;
            if (topic.Price > 0)
            {
                price = topic.Price;
                if (PaymentLogs.IsBuyer(topicid, userid) || (Utils.StrDateDiffHours(topic.Postdatetime, Scoresets.GetMaxChargeSpan()) > 0 && Scoresets.GetMaxChargeSpan() != 0))//判断当前用户是否已经购买
                {
                    price = -1;
                }
            }

            if (useradminid != 0)
            {
                ismoder = Moderators.IsModer(useradminid, userid, forumid) ? 1 : 0;
            }

            if (topic.Readperm > usergroupinfo.Readaccess && topic.Posterid != userid && useradminid != 1 &&
                ismoder != 1)
            {
                AddErrLine(string.Format("本主题阅读权限为: {0}, 您当前的身份 \"{1}\" 阅读权限不够", topic.Readperm.ToString(), usergroupinfo.Grouptitle));
                return;
            }

            if (topic.Displayorder == -1)
            {
                AddErrLine("此主题已被删除！");
                return;
            }

            if (topic.Displayorder == -2)
            {
                AddErrLine("此主题未经审核！");
                return;
            }

            if (forum.Password != "" &&
                Utils.MD5(forum.Password) != ForumUtils.GetCookie("forum" + forumid.ToString() + "password"))
            {
                AddErrLine("本版块被管理员设置了密码");
                //SetBackLink("showforum-" + forumid.ToString() + config.Extname);
                HttpContext.Current.Response.Redirect(BaseConfigs.GetForumPath + "showforum-" + forumid.ToString() + config.Extname, true);
                return;
            }

            if (!Forums.AllowViewByUserID(forum.Permuserlist, userid)) //判断当前用户在当前版块浏览权限
            {
                if (forum.Viewperm == null || forum.Viewperm == string.Empty) //当板块权限为空时，按照用户组权限
                {
                    if (usergroupinfo.Allowvisit != 1)
                    {
                        AddErrLine("您当前的身份 \"" + usergroupinfo.Grouptitle + "\" 没有浏览该版块的权限");
                        return;
                    }
                }
                else //当板块权限不为空，按照板块权限
                {
                    if (!Forums.AllowView(forum.Viewperm, usergroupid))
                    {
                        AddErrLine("您没有浏览该版块的权限");
                        return;
                    }
                }
            }

            userextcreditsinfo = Scoresets.GetScoreSet(Scoresets.GetCreditsTrans());
            maxincpertopic = Scoresets.GetMaxIncPerTopic();
            maxchargespan = Scoresets.GetMaxChargeSpan();
            creditstax = Scoresets.GetCreditsTax() * 100;

            netamount = topicprice - topicprice * creditstax / 100;
            if (topicprice > maxincpertopic)
            {
                netamount = maxincpertopic - maxincpertopic * creditstax / 100;
            }

            if (price != -1)
            {
                IDataReader reader = Users.GetUserInfoToReader(userid);
                if (reader == null)
                {
                    AddErrLine(NO_PERMISSION);
                    needlogin = true;
                    return;
                }

                if (!reader.Read())
                {
                    AddErrLine(NO_PERMISSION);
                    needlogin = true;
                    reader.Close();
                    return;
                }

                if (Utils.StrToFloat(reader["extcredits" + Scoresets.GetCreditsTrans().ToString()], 0) < topic.Price)
                {
                    AddErrLine(string.Format(NOT_ENOUGH_MONEY_TO, Utils.StrToFloat(reader["extcredits" + Scoresets.GetCreditsTrans().ToString()], 0), topic.Price));
                    reader.Close();

                    return;
                }

                userlastprice = Utils.StrToFloat(reader["extcredits" + Scoresets.GetCreditsTrans().ToString()], 0) - topic.Price;
                reader.Close();
            }



            //如果不是提交...
            if (!ispost)
            {
                buyers = PaymentLogs.GetPaymentLogByTidCount(topic.Tid);

                //显示购买信息列表
                if (showpayments == 1)
                {
                    //得到当前用户请求的页数
                    pageid = DNTRequest.GetInt("page", 1);
                    //获取主题总数
                    //获取总页数
                    pagecount = buyers % pagesize == 0 ? buyers / pagesize : buyers / pagesize + 1;
                    if (pagecount == 0)
                    {
                        pagecount = 1;
                    }
                    //修正请求页数中可能的错误
                    if (pageid < 1)
                    {
                        pageid = 1;
                    }
                    if (pageid > pagecount)
                    {
                        pageid = pagecount;
                    }

                    //获取收入记录并分页显示
                    paymentloglist = PaymentLogs.GetPaymentLogByTid(pagesize, pageid, topic.Tid);
                }

                //判断是否为回复可见帖, hide=0为非回复可见(正常), hide>0为回复可见, hide=-1为回复可见但当前用户已回复
                int hide = 0;
                if (topic.Hide == 1)
                {
                    hide = topic.Hide;
                    if (Posts.IsReplier(topicid, userid))
                    {
                        hide = -1;
                    }
                }

                PostpramsInfo _postpramsinfo = new PostpramsInfo();
                _postpramsinfo.Fid = forum.Fid;
                _postpramsinfo.Tid = topicid;
                _postpramsinfo.Jammer = forum.Jammer;
                _postpramsinfo.Pagesize = 5;
                _postpramsinfo.Pageindex = 1;
                _postpramsinfo.Getattachperm = forum.Getattachperm;
                _postpramsinfo.Usergroupid = usergroupid;
                _postpramsinfo.Attachimgpost = config.Attachimgpost;
                _postpramsinfo.Showattachmentpath = config.Showattachmentpath;
                _postpramsinfo.Hide = hide;
                _postpramsinfo.Price = price;
                _postpramsinfo.Ubbmode = false;

                _postpramsinfo.Showimages = forum.Allowimgcode;
                _postpramsinfo.Smiliesinfo = Smilies.GetSmiliesListWithInfo();
                _postpramsinfo.Customeditorbuttoninfo = Editors.GetCustomEditButtonListWithInfo();
                _postpramsinfo.Smiliesmax = config.Smiliesmax;
                _postpramsinfo.Bbcodemode = config.Bbcodemode;

                lastpostlist = Posts.GetLastPostList(_postpramsinfo);
            }
            else
            {

                int reval = PaymentLogs.BuyTopic(userid, topic.Tid, topic.Posterid, topic.Price, netamount);
                if (reval > 0)
                {
                    SetUrl(base.ShowTopicAspxRewrite(topic.Tid, 0));

                    SetMetaRefresh();
                    SetShowBackLink(false);
                    AddMsgLine(PURCHASE_SUCCESS);
                    return;
                }
                else
                {
                    SetBackLink(base.ShowForumAspxRewrite(topic.Fid, 0));

                    if (reval == -1)
                    {
                        AddErrLine(NOT_ENOUGH_MONEY);
                        return;
                    }
                    else if (reval == -2)
                    {
                        AddErrLine(NO_PERMISSION);
                        return;
                    }
                    else
                    {
                        AddErrLine(UNKNOWN_REASON);
                        return;
                    }
                }
            }

        }

    }
}
