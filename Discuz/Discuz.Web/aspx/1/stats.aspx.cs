using System;
using System.Collections;
using System.Web;
using Discuz.Web.UI;
using Discuz.Common.Generic;
using Discuz.Entity;
using Discuz.Forum;
using Discuz.Common;
using System.Text;

namespace Discuz.Web
{
    /// <summary>
    /// 标签列表页
    /// </summary>
    public class stats : PageBase
    {
        #region Fields
        //public Dictionary<string, int> totalstats = new Dictionary<string,int>();
        public Hashtable totalstats = new Hashtable();
        public Hashtable osstats = new Hashtable();
        public Hashtable browserstats = new Hashtable();
        public Hashtable monthstats = new Hashtable();
        public Hashtable weekstats = new Hashtable();
        public Hashtable hourstats = new Hashtable();
        public Hashtable mainstats = new Hashtable();

        public Hashtable daypostsstats = new Hashtable();
        public Hashtable monthpostsstats = new Hashtable();
        public Hashtable forumsrankstats = new Hashtable();
        public Hashtable onlinesstats = new Hashtable();
        public Hashtable postsrankstats = new Hashtable();
        public Hashtable teamstats = new Hashtable();
        public Hashtable teamcategories;
        public Hashtable teamforums;
        public Hashtable teamadmins;
        public Hashtable teammoderators;
        public Hashtable teammembers;
        public Hashtable teamavgoffdays;
        public Hashtable teamsavgthismonthposts;
        public Hashtable teamavgtotalol;
        public Hashtable teamavgthismonthol;
        public Hashtable teamavgmodactions;
        public Hashtable creditsrankstats = new Hashtable();
        public Hashtable tradestats = new Hashtable();

        public string lastupdate = string.Empty;
        public string nextupdate = string.Empty;

        public string type = string.Empty;

        #region Main
        public int members;
        public int mempost;
        public string admins;
        public int memnonpost;
        public string lastmember;
        public double mempostpercent;
        public string bestmem;
        public int bestmemposts;
        public int forums;
        public double mempostavg;
        public double postsaddavg;
        public double membersaddavg;
        public double topicreplyavg;
        public double pageviewavg;
        public ForumInfo hotforum;
        public int topics;
        public int posts;
        public string postsaddtoday;
        public string membersaddtoday;
        public string activeindex;
        public bool statstatus;
        public string monthpostsofstatsbar = string.Empty;
        public string daypostsofstatsbar = string.Empty;
        public string monthofstatsbar = string.Empty;
        public int runtime;
        #endregion

        #region Views
        public string weekofstatsbar = string.Empty;
        public string hourofstatsbar = string.Empty;
        #endregion

        #region Client
        public string browserofstatsbar = string.Empty;
        public string osofstatsbar = string.Empty;
        #endregion

        #region TopicsRank
        public string hotreplytopics;
        public string hottopics;
        #endregion

        #region PostsRank
        public string postsrank;
        public string digestpostsrank;
        public string thismonthpostsrank;
        public string todaypostsrank;
        #endregion

        #region ForumsRank
        public string topicsforumsrank;
        public string postsforumsrank;
        public string thismonthforumsrank;
        public string todayforumsrank;
        #endregion

        #region CreditsRank
        public string[] score;
        public string creditsrank;
        public string extcreditsrank1;
        public string extcreditsrank2;
        public string extcreditsrank3;
        public string extcreditsrank4;
        public string extcreditsrank5;
        public string extcreditsrank6;
        public string extcreditsrank7;
        public string extcreditsrank8;
        #endregion

        #region OnlineRank
        public string totalonlinerank;
        public string thismonthonlinerank;
        #endregion


        public int maxos = 0;
        public int maxbrowser = 0;
        public int maxmonth = 0;
        public int yearofmaxmonth = 0;
        public int monthofmaxmonth = 0;
        public int maxweek = 0;
        public string dayofmaxweek;
        public int maxhour = 0;
        public int maxhourfrom = 0;
        public int maxhourto = 0;

        public int maxmonthposts;
        public int maxdayposts;

        public int statscachelife = 120;

        Dictionary<string, string> statvars = new Dictionary<string, string>();
        #endregion

        public bool needlogin = false;

        protected override void ShowPage()
        {
            pagetitle = "统计";
            if (usergroupinfo.Allowviewstats == 0)
            {
                AddErrLine("您所在的用户组 ( <b>" + usergroupinfo.Grouptitle + "</b> ) 没有查看统计信息的权限");
                if (userid < 1)
                {
                    needlogin = true;
                }
                return;

            }


            //判断权限


            statscachelife = config.Statscachelife;
            if (statscachelife <= 0)
            {
                statscachelife = 120;
            }
            StatInfo[] stats = Stats.GetAllStats();
            statstatus = config.Statstatus == 1;
            //statstatus = false;

            //initialize
            totalstats["hits"] = 0;
            totalstats["maxmonth"] = 0;
            totalstats["guests"] = 0;
            totalstats["visitors"] = 0;


            foreach (StatInfo stat in stats)
            {
                switch (stat.Type)
                { 
                    case "total":
                        SetValue(stat, totalstats);
                        break;
                    case "os":
                        SetValue(stat, osstats);
                        if (stat.Count > maxos)
                        {
                            maxos = stat.Count;
                        }
                        break;
                    case "browser":
                        SetValue(stat, browserstats);
                        if (stat.Count > maxbrowser)
                        {
                            maxbrowser = stat.Count;
                        }
                        break;
                    case "month":
                        SetValue(stat, monthstats);
                        if (stat.Count > maxmonth)
                        {
                            maxmonth = stat.Count;
                            yearofmaxmonth = Utils.StrToInt(stat.Variable, 0) / 100;
                            monthofmaxmonth = Utils.StrToInt(stat.Variable, 0) - yearofmaxmonth * 100;
                        }
                        break;
                    case "week":
                        SetValue(stat, weekstats);
                        if (stat.Count > maxweek)
                        {
                            maxweek = stat.Count;
                            dayofmaxweek = stat.Variable;
                        }
                        break;
                    case "hour":
                        SetValue(stat, hourstats);
                        if (stat.Count > maxhour)
                        {
                            maxhour = stat.Count;
                            maxhourfrom = Utils.StrToInt(stat.Variable, 0);
                            maxhourto = maxhourfrom + 1;
                        }
                        break;
                }
            }

            StatVarInfo[] statvars = Stats.GetAllStatVars();
            foreach (StatVarInfo statvar in statvars)
            {
                if (statvar.Variable == "lastupdate" && Utils.IsNumeric(statvar.Value))
                    continue;
                switch (statvar.Type)
                {
                    case "dayposts":
                        SetValue(statvar, daypostsstats);
                        break;
                    case "creditsrank":
                        SetValue(statvar, creditsrankstats);
                        break;
                    case "forumsrank":
                        SetValue(statvar, forumsrankstats);
                        break;
                    case "postsrank":
                        SetValue(statvar, postsrankstats);
                        break;
                    case "main":
                        SetValue(statvar, mainstats);
                        break;
                    case "monthposts":
                        SetValue(statvar, monthpostsstats);
                        break;
                    case "onlines":
                        SetValue(statvar, onlinesstats);
                        break;
                    case "team":
                        SetValue(statvar, teamstats);
                        break;
                    case "trade":
                        SetValue(statvar, tradestats);
                        break;
                }
             
            }

            type = DNTRequest.GetString("type");

            if ((type == "" && !statstatus) || type == "posts")
            {
                maxmonthposts = maxdayposts = 0;
                /*
                daypostsstats["starttime"] = DateTime.Now.AddDays(-30);
                */
                //daypostsstats.Add("starttime", DateTime.Now.AddDays(-30));

                Stats.DeleteOldDayposts();

                /*
                if (!monthpostsstats.Contains("starttime"))
                { 
                    DateTime starttime = Stats.GetPostStartTime();
                    monthpostsstats["starttime"] = starttime;
                    Stats.UpdateStatVars("monthposts", "starttime", starttime.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                */

                //monthposts
                monthpostsstats = Stats.GetMonthPostsStats(monthpostsstats);
                maxmonthposts = (int)monthpostsstats["maxcount"];
                monthpostsstats.Remove("maxcount");
                //dayposts
                daypostsstats = Stats.GetDayPostsStats(daypostsstats);
                maxdayposts = (int)daypostsstats["maxcount"];
                daypostsstats.Remove("maxcount");



            }



            switch (type)
            { 
                case "views":
                    GetViews();
                    break;
                case "client":
                    GetClient();
                    break;
                case "posts":
                    GetPosts();
                    break;
                case "forumsrank":
                    GetForumsRank();
                    break;
                case "topicsrank":
                    GetTopicsRank();
                    break;
                case "postsrank":
                    GetPostsRank();
                    break;
                case "creditsrank":
                    GetCreditsRank();
                    break;
                case "trade":
                    GetTrade();
                    break;
                case "onlinetime":
                    GetOnlinetime();
                    break;
                case "team":
                    GetTeam();
                    break;
                case "modworks":
                    GetModWorks();
                    break;
                case "": 
                    Default();
                    break;
                default: 
                    AddErrLine("未定义操作请返回");
                    SetShowBackLink(false);
                    return;

            }
        }


        #region Helper
        
        private void SetValue(StatInfo stat, Hashtable ht)
        {
            ht[stat.Variable] = stat.Count;
        }

        private void SetValue(StatVarInfo statvar, Hashtable ht)
        {
            ht[statvar.Variable] = statvar.Value;
        }

        #endregion

        /// <summary>
        /// 基本状况
        /// </summary>
        private void Default()
        {
            lastmember = Statistics.GetStatisticsRowItem("lastusername");
            //StatVarInfo[] mainstatvars = Stats.GetStatVarsByType("main");
            foreach (string key in mainstats.Keys)
            {
                statvars[key] = mainstats[key].ToString();
            }

            if (!statvars.ContainsKey("lastupdate") || (DateTime.Now - DateTime.Parse(statvars["lastupdate"])).TotalMinutes > statscachelife)
            {
                statvars.Clear();
                statvars["lastupdate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Stats.UpdateStatVars("main", "lastupdate", statvars["lastupdate"]);
            }

            forums = Stats.GetForumCount();
            topics = Stats.GetTopicCount();
            posts = Stats.GetPostCount();
            members = Stats.GetMemberCount();

            //运行时间 从第一帖到现在
            if (statvars.ContainsKey("runtime"))
            {
                runtime = Utils.StrToInt(statvars["runtime"], 0);
            }
            else 
            {
                runtime = (DateTime.Now - Convert.ToDateTime(monthpostsstats["starttime"])).Days;
                //runtime = Stats.GetRuntime();
                Stats.UpdateStatVars("main", "runtime", runtime.ToString());
            }

            //今日新增帖数
            if (statvars.ContainsKey("postsaddtoday"))
            {
                postsaddtoday = statvars["postsaddtoday"];
            }
            else 
            {
                postsaddtoday = Stats.GetTodayPostCount().ToString();
                Stats.UpdateStatVars("main", "postsaddtoday", postsaddtoday);
            }

            //今日新增会员数
            if (statvars.ContainsKey("membersaddtoday"))
            {
                membersaddtoday = statvars["membersaddtoday"];
            }
            else
            {
                membersaddtoday = Stats.GetTodayNewMemberCount().ToString();
                Stats.UpdateStatVars("main", "membersaddtoday", membersaddtoday);
            }

            //管理人员数
            if (statvars.ContainsKey("admins"))
            {
                admins = statvars["admins"];
            }
            else
            {
                admins = Stats.GetAdminCount().ToString();
                Stats.UpdateStatVars("main", "admins", admins);
            }

            //未发帖会员数
            if (statvars.ContainsKey("memnonpost"))
            {
                memnonpost = Utils.StrToInt(statvars["memnonpost"], 0);
            }
            else
            {
                memnonpost = Stats.GetNonPostMemCount();
                Stats.UpdateStatVars("main", "memnonpost", memnonpost.ToString());
            }

            //热门论坛
            if (statvars.ContainsKey("hotforum"))
            {
                hotforum = (ForumInfo)SerializationHelper.DeSerialize(typeof(ForumInfo), statvars["hotforum"]);
            }
            else
            {
                hotforum = Stats.GetHotForum();
                Stats.UpdateStatVars("main", "hotforum", SerializationHelper.Serialize(hotforum));
            }

            //今日最佳会员及其今日帖数
            if (statvars.ContainsKey("bestmem") && statvars.ContainsKey("bestmemposts"))
            {
                bestmem = statvars["bestmem"];
                bestmemposts = Utils.StrToInt(statvars["bestmemposts"], 0);
            }
            else
            {
                Stats.GetBestMember(out bestmem, out bestmemposts);

                Stats.UpdateStatVars("main", "bestmem", bestmem);
                Stats.UpdateStatVars("main", "bestmemposts", bestmemposts.ToString());

            }
            mempost = members - memnonpost;
            mempostavg = (double)Math.Round((double)posts / (double)members, 2);
            topicreplyavg = (double)Math.Round((double)(posts - topics) / (double)topics, 2);
            mempostpercent = (double)Math.Round((double)(mempost * 100) / (double)members, 2);
            postsaddavg = (double)Math.Round((double)posts / (double)runtime, 2);
            membersaddavg = members / runtime;

            int visitors = Utils.StrToInt(totalstats["members"], 0) + Utils.StrToInt(totalstats["guests"], 0);
            totalstats["visitors"] = visitors;
            pageviewavg = (double)Math.Round((double)Utils.StrToInt(totalstats["hits"], 0) / (double)(visitors == 0 ? 1 : visitors), 2);
            activeindex = ((Math.Round(membersaddavg /(double) (members == 0 ? 1 : members), 2) + Math.Round(postsaddavg /(double) (posts == 0 ? 1 : posts), 2)) * 1500.00 + topicreplyavg * 10.00 + mempostavg + Math.Round(mempostpercent / 10.00, 2) + pageviewavg).ToString();

            if (statstatus)
            {
                monthofstatsbar = Stats.GetStatsDataHtml("month", monthstats, maxmonth);
            }
            else
            {
                monthpostsofstatsbar = Stats.GetStatsDataHtml("monthposts", monthpostsstats, maxmonthposts);
                daypostsofstatsbar = Stats.GetStatsDataHtml("dayposts", daypostsstats, maxdayposts);
            }

            lastupdate = statvars["lastupdate"];
            nextupdate = DateTime.Parse(statvars["lastupdate"]).AddMinutes(statscachelife).ToString("yyyy-MM-dd HH:mm:ss");

            
        }

        /// <summary>
        /// 管理统计
        /// </summary>
        private void GetModWorks()
        {
        }

        /// <summary>
        /// 管理团队
        /// </summary>
        private void GetTeam()
        {
            foreach (string key in teamstats.Keys)
            {
                statvars[key] = teamstats[key].ToString();
            }

            if (!statvars.ContainsKey("lastupdate") || (DateTime.Now - DateTime.Parse(statvars["lastupdate"])).TotalMinutes > statscachelife)
            {
                statvars.Clear();
                statvars["lastupdate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Stats.UpdateStatVars("team", "lastupdate", statvars["lastupdate"]);
            }



            lastupdate = statvars["lastupdate"];
            nextupdate = DateTime.Parse(statvars["lastupdate"]).AddMinutes(statscachelife).ToString("yyyy-MM-dd HH:mm:ss");

        }

        /// <summary>
        /// 在线时间
        /// </summary>
        private void GetOnlinetime()
        {
            if (config.Oltimespan == 0)
            {
                totalonlinerank = "<li>未开启在线时长统计</li>";
                thismonthonlinerank = "<li></li>";

                return;
            }

            foreach (string key in onlinesstats.Keys)
            {
                statvars[key] = onlinesstats[key].ToString();
            }

            if (!statvars.ContainsKey("lastupdate") || (DateTime.Now - DateTime.Parse(statvars["lastupdate"])).TotalMinutes > statscachelife)
            {
                statvars.Clear();
                statvars["lastupdate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Stats.UpdateStatVars("onlines", "lastupdate", statvars["lastupdate"]);
            }
            ShortUserInfo[] total;
            if (statvars.ContainsKey("total"))
            {
                total = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["total"]);
            }
            else
            {
                total = Stats.GetUserOnlinetime("total");
                Stats.UpdateStatVars("onlines", "total", SerializationHelper.Serialize(total));
            }

            ShortUserInfo[] thismonth;
            if (statvars.ContainsKey("thismonth"))
            {
                thismonth = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["thismonth"]);
            }
            else
            {
                thismonth = Stats.GetUserOnlinetime("thismonth");
                Stats.UpdateStatVars("onlines", "thismonth", SerializationHelper.Serialize(thismonth));
            }

            int maxrows = Math.Max(total.Length, thismonth.Length);

            totalonlinerank = Stats.GetUserRankHtml(total, "onlinetime", maxrows);
            thismonthonlinerank = Stats.GetUserRankHtml(thismonth, "onlinetime", maxrows);

            lastupdate = statvars["lastupdate"];
            nextupdate = DateTime.Parse(statvars["lastupdate"]).AddMinutes(statscachelife).ToString("yyyy-MM-dd HH:mm:ss");

        }

        /// <summary>
        /// 交易记录
        /// </summary>
        private void GetTrade()
        {
        }

        /// <summary>
        /// 信用记录
        /// </summary>
        private void GetCreditsRank()
        {
            score = Scoresets.GetValidScoreName();
            foreach (string key in creditsrankstats.Keys)
            {
                statvars[key] = creditsrankstats[key].ToString();
            }

            if (!statvars.ContainsKey("lastupdate") || (DateTime.Now - DateTime.Parse(statvars["lastupdate"])).TotalMinutes > statscachelife)
            {
                statvars.Clear();
                statvars["lastupdate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Stats.UpdateStatVars("creditsrank", "lastupdate", statvars["lastupdate"]);
            }

            ShortUserInfo[] credits;
            ShortUserInfo[][] extendedcredits;
            if (statvars.ContainsKey("credits"))
            {
                credits = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["credits"]);
            }
            else
            {
                credits = Stats.GetUserArray("credits");
                Stats.UpdateStatVars("creditsrank", "credits", SerializationHelper.Serialize(credits));
            }

            if (statvars.ContainsKey("extendedcredits"))
            {
                extendedcredits = (ShortUserInfo[][])SerializationHelper.DeSerialize(typeof(ShortUserInfo[][]), statvars["extendedcredits"]);
            }
            else
            {
                extendedcredits = Stats.GetExtsRankUserArray();
                Stats.UpdateStatVars("creditsrank", "extendedcredits", SerializationHelper.Serialize(extendedcredits));
            }

            int maxrows = 0;
            maxrows = Math.Max(credits.Length, maxrows);
            maxrows = Math.Max(extendedcredits[0].Length, maxrows);
            maxrows = Math.Max(extendedcredits[1].Length, maxrows);
            maxrows = Math.Max(extendedcredits[2].Length, maxrows);
            maxrows = Math.Max(extendedcredits[3].Length, maxrows);
            maxrows = Math.Max(extendedcredits[4].Length, maxrows);
            maxrows = Math.Max(extendedcredits[5].Length, maxrows);
            maxrows = Math.Max(extendedcredits[6].Length, maxrows);
            maxrows = Math.Max(extendedcredits[7].Length, maxrows);

            creditsrank = Stats.GetUserRankHtml(credits, "credits", maxrows);
            extcreditsrank1 = Stats.GetUserRankHtml(extendedcredits[0], "extcredits1", maxrows);
            extcreditsrank2 = Stats.GetUserRankHtml(extendedcredits[1], "extcredits2", maxrows);
            extcreditsrank3 = Stats.GetUserRankHtml(extendedcredits[2], "extcredits3", maxrows);
            extcreditsrank4 = Stats.GetUserRankHtml(extendedcredits[3], "extcredits4", maxrows);
            extcreditsrank5 = Stats.GetUserRankHtml(extendedcredits[4], "extcredits5", maxrows);
            extcreditsrank6 = Stats.GetUserRankHtml(extendedcredits[5], "extcredits6", maxrows);
            extcreditsrank7 = Stats.GetUserRankHtml(extendedcredits[6], "extcredits7", maxrows);
            extcreditsrank8 = Stats.GetUserRankHtml(extendedcredits[7], "extcredits8", maxrows);


            lastupdate = statvars["lastupdate"];
            nextupdate = DateTime.Parse(statvars["lastupdate"]).AddMinutes(statscachelife).ToString("yyyy-MM-dd HH:mm:ss");



        }

        /// <summary>
        /// 发帖排行
        /// </summary>
        private void GetPostsRank()
        {
            foreach (string key in postsrankstats.Keys)
            {
                statvars[key] = postsrankstats[key].ToString();
            }

            if (!statvars.ContainsKey("lastupdate") || (DateTime.Now - DateTime.Parse(statvars["lastupdate"])).TotalMinutes > statscachelife)
            {
                statvars.Clear();
                statvars["lastupdate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Stats.UpdateStatVars("postsrank", "lastupdate", statvars["lastupdate"]);
            }

            ShortUserInfo[] posts;
            ShortUserInfo[] digestposts;
            ShortUserInfo[] thismonth;
            ShortUserInfo[] today;

            if (statvars.ContainsKey("posts"))
            {
                posts = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["posts"]);
            }
            else
            {
                posts = Stats.GetUserArray("posts");
                Stats.UpdateStatVars("postsrank", "posts", SerializationHelper.Serialize(posts));
            }

            if (statvars.ContainsKey("digestposts"))
            {
                digestposts = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["digestposts"]);
            }
            else
            {
                digestposts = Stats.GetUserArray("digestposts");
                Stats.UpdateStatVars("postsrank", "digestposts", SerializationHelper.Serialize(digestposts));
            }

            if (statvars.ContainsKey("thismonth"))
            {
                thismonth = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["thismonth"]);
            }
            else
            {
                thismonth = Stats.GetUserArray("thismonth");
                Stats.UpdateStatVars("postsrank", "thismonth", SerializationHelper.Serialize(thismonth));
            }

            if (statvars.ContainsKey("today"))
            {
                today = (ShortUserInfo[])SerializationHelper.DeSerialize(typeof(ShortUserInfo[]), statvars["today"]);
            }
            else
            {
                today = Stats.GetUserArray("today");
                Stats.UpdateStatVars("postsrank", "today", SerializationHelper.Serialize(today));
            }

            int maxrows = 0;
            maxrows = Math.Max(posts.Length, maxrows);
            maxrows = Math.Max(digestposts.Length, maxrows);
            maxrows = Math.Max(thismonth.Length, maxrows);
            maxrows = Math.Max(today.Length, maxrows);

            postsrank = Stats.GetUserRankHtml(posts, "posts", maxrows);
            digestpostsrank = Stats.GetUserRankHtml(digestposts, "digestposts", maxrows);
            thismonthpostsrank = Stats.GetUserRankHtml(thismonth, "thismonth", maxrows);
            todaypostsrank = Stats.GetUserRankHtml(today, "today", maxrows);

            lastupdate = statvars["lastupdate"];
            nextupdate = DateTime.Parse(statvars["lastupdate"]).AddMinutes(statscachelife).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 主题排行
        /// </summary>
        private void GetTopicsRank()
        {
            hottopics = Stats.GetHotTopicsHtml();
            hotreplytopics = Stats.GetHotReplyTopicsHtml();
        }

        /// <summary>
        /// 板块排行
        /// </summary>
        private void GetForumsRank()
        {
            foreach (string key in forumsrankstats.Keys)
            {
                statvars[key] = forumsrankstats[key].ToString();
            }
            if (!statvars.ContainsKey("lastupdate") || (DateTime.Now - DateTime.Parse(statvars["lastupdate"])).TotalMinutes > statscachelife)
            {
                statvars.Clear();
                statvars["lastupdate"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Stats.UpdateStatVars("forumsrank", "lastupdate", statvars["lastupdate"]);
            }

            ForumInfo[] topics;
            ForumInfo[] posts;
            ForumInfo[] thismonth;
            ForumInfo[] today;

            int maxrows = 0;

            if (statvars.ContainsKey("topics"))
            {
                topics = (ForumInfo[])SerializationHelper.DeSerialize(typeof(ForumInfo[]), statvars["topics"]);
            }
            else
            {
                topics = Stats.GetForumArray("topics"); 
                Stats.UpdateStatVars("forumsrank", "topics", SerializationHelper.Serialize(topics));
            }

            if (statvars.ContainsKey("posts"))
            {
                posts = (ForumInfo[])SerializationHelper.DeSerialize(typeof(ForumInfo[]), statvars["posts"]);
            }
            else
            {
                posts = Stats.GetForumArray("posts"); 
                Stats.UpdateStatVars("forumsrank", "posts", SerializationHelper.Serialize(posts));
            }

            if (statvars.ContainsKey("thismonth"))
            {
                thismonth = (ForumInfo[])SerializationHelper.DeSerialize(typeof(ForumInfo[]), statvars["thismonth"]);
            }
            else
            {
                thismonth = Stats.GetForumArray("thismonth"); 
                Stats.UpdateStatVars("forumsrank", "thismonth", SerializationHelper.Serialize(thismonth));
            }

            if (statvars.ContainsKey("today"))
            {
                today = (ForumInfo[])SerializationHelper.DeSerialize(typeof(ForumInfo[]), statvars["today"]);
            }
            else
            {
                today = Stats.GetForumArray("today");
                Stats.UpdateStatVars("forumsrank", "today", SerializationHelper.Serialize(today));
            }

            maxrows = Math.Max(topics.Length, maxrows);
            maxrows = Math.Max(posts.Length, maxrows);
            maxrows = Math.Max(thismonth.Length, maxrows);
            maxrows = Math.Max(today.Length, maxrows);


            topicsforumsrank = Stats.GetForumsRankHtml(topics, "topics", maxrows);
            postsforumsrank = Stats.GetForumsRankHtml(posts, "posts", maxrows);
            thismonthforumsrank = Stats.GetForumsRankHtml(thismonth, "thismonth", maxrows);
            todayforumsrank = Stats.GetForumsRankHtml(today, "today", maxrows);

            lastupdate = statvars["lastupdate"];
            nextupdate = DateTime.Parse(statvars["lastupdate"]).AddMinutes(statscachelife).ToString("yyyy-MM-dd HH:mm:ss");

        }

        /// <summary>
        /// 发帖量记录
        /// </summary>
        private void GetPosts()
        {
            monthpostsofstatsbar = Stats.GetStatsDataHtml("monthposts", monthpostsstats, maxmonthposts);
            daypostsofstatsbar = Stats.GetStatsDataHtml("dayposts", daypostsstats, maxdayposts);
        }

        /// <summary>
        /// 客户软件
        /// </summary>
        private void GetClient()
        {
            if (!statstatus)
                return;
            browserofstatsbar = Stats.GetStatsDataHtml("browser", browserstats, maxbrowser);
            osofstatsbar = Stats.GetStatsDataHtml("os", osstats, maxos);
        }

        /// <summary>
        /// 流量统计
        /// </summary>
        private void GetViews()
        {
            if (!statstatus)
                return;
            weekofstatsbar = Stats.GetStatsDataHtml("week", weekstats, maxweek);
            hourofstatsbar = Stats.GetStatsDataHtml("hour", hourstats, maxhour);
        }

    }
}
