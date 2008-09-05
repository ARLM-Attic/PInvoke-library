using System;
using System.Data;
using System.Data.Common;

using Discuz.Common;
using Discuz.Data;
using Discuz.Config;
using System.Text;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// �û��������
    /// </summary>
    public class UserGroups
    {


        /// <summary>
        /// ����û�������
        /// </summary>
        /// <returns>�û�������</returns>
        public static UserGroupInfo[] GetUserGroupList()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            UserGroupInfo[] infoArray = cache.RetrieveObject("/Forum/UserGroupList") as UserGroupInfo[];
            if (infoArray == null)
            {
                DataTable dt = DatabaseProvider.GetInstance().GetUserGroups();
                infoArray = new UserGroupInfo[dt.Rows.Count];
                UserGroupInfo info;
                int Index = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    info = new UserGroupInfo();
                    info.Groupid = Int16.Parse(dr["groupid"].ToString());
                    info.Radminid = Int32.Parse(dr["radminid"].ToString());
                    info.Type = Int16.Parse(dr["type"].ToString());
                    info.System = Int16.Parse(dr["system"].ToString());
                    info.Color = dr["color"].ToString().Trim();
                    info.Grouptitle = dr["grouptitle"].ToString().Trim();
                    info.Creditshigher = Int32.Parse(dr["creditshigher"].ToString());
                    info.Creditslower = Int32.Parse(dr["creditslower"].ToString());
                    info.Stars = Int32.Parse(dr["stars"].ToString());
                    info.Groupavatar = dr["groupavatar"].ToString();
                    info.Readaccess = Int32.Parse(dr["readaccess"].ToString());
                    info.Allowvisit = Int32.Parse(dr["allowvisit"].ToString());
                    info.Allowpost = Int32.Parse(dr["allowpost"].ToString());
                    info.Allowreply = Int32.Parse(dr["allowreply"].ToString());
                    info.Allowpostpoll = Int32.Parse(dr["allowpostpoll"].ToString());
                    info.Allowdirectpost = Int32.Parse(dr["allowdirectpost"].ToString());
                    info.Allowgetattach = Int32.Parse(dr["allowgetattach"].ToString());
                    info.Allowpostattach = Int32.Parse(dr["allowpostattach"].ToString());
                    info.Allowvote = Int32.Parse(dr["allowvote"].ToString());
                    info.Allowmultigroups = Int32.Parse(dr["allowmultigroups"].ToString());
                    info.Allowsearch = Int32.Parse(dr["allowsearch"].ToString());
                    info.Allowavatar = Int32.Parse(dr["allowavatar"].ToString());
                    info.Allowcstatus = Int32.Parse(dr["allowcstatus"].ToString());
                    info.Allowuseblog = Int32.Parse(dr["allowuseblog"].ToString());
                    info.Allowinvisible = Int32.Parse(dr["allowinvisible"].ToString());
                    info.Allowtransfer = Int32.Parse(dr["allowtransfer"].ToString());
                    info.Allowsetreadperm = Int32.Parse(dr["allowsetreadperm"].ToString());
                    info.Allowsetattachperm = Int32.Parse(dr["allowsetattachperm"].ToString());
                    info.Allowhidecode = Int32.Parse(dr["allowhidecode"].ToString());
                    info.Allowhtml = Int32.Parse(dr["allowhtml"].ToString());
                    info.Allowcusbbcode = Int32.Parse(dr["allowcusbbcode"].ToString());
                    info.Allownickname = Int32.Parse(dr["allownickname"].ToString());
                    info.Allowsigbbcode = Int32.Parse(dr["allowsigbbcode"].ToString());
                    info.Allowsigimgcode = Int32.Parse(dr["allowsigimgcode"].ToString());
                    info.Allowviewpro = Int32.Parse(dr["allowviewpro"].ToString());
                    info.Allowviewstats = Int32.Parse(dr["allowviewstats"].ToString());
                    info.Disableperiodctrl = Int32.Parse(dr["disableperiodctrl"].ToString());
                    info.Reasonpm = Int32.Parse(dr["reasonpm"].ToString());
                    info.Maxprice = Int16.Parse(dr["maxprice"].ToString());
                    info.Maxpmnum = Int16.Parse(dr["maxpmnum"].ToString());
                    info.Maxsigsize = Int16.Parse(dr["maxsigsize"].ToString());
                    info.Maxattachsize = Int32.Parse(dr["maxattachsize"].ToString());
                    info.Maxsizeperday = Int32.Parse(dr["maxsizeperday"].ToString());
                    info.Attachextensions = dr["attachextensions"].ToString().Trim();
                    info.Raterange = dr["raterange"].ToString().Trim();
                    info.Allowspace = Int16.Parse(dr["allowspace"].ToString());
                    info.Maxspaceattachsize = Int32.Parse(dr["maxspaceattachsize"].ToString());
                    info.Maxspacephotosize = Int32.Parse(dr["maxspacephotosize"].ToString());
                    info.Allowbonus = Int32.Parse(dr["allowbonus"].ToString());
                    info.Allowdebate = Int32.Parse(dr["allowdebate"].ToString());
                    info.Minbonusprice = Int16.Parse(dr["minbonusprice"].ToString());
                    info.Maxbonusprice = Int16.Parse(dr["maxbonusprice"].ToString());
                    info.Allowtrade = Int32.Parse(dr["allowtrade"].ToString());
                    info.Allowdiggs = Int32.Parse(dr["allowdiggs"].ToString());
                    infoArray[Index] = info;
                    Index++;
                }

                cache.AddObject("/Forum/UserGroupList", infoArray);
            }
            return infoArray;
        }


        /// <summary>
        /// ��ȡָ�������Ϣ
        /// </summary>
        /// <param name="groupid">��id</param>
        /// <returns>����Ϣ</returns>
        public static UserGroupInfo GetUserGroupInfo(int groupid)
        {
            UserGroupInfo[] infoArray = GetUserGroupList();

            // ����û���idΪ7��Ϊ�ο�
            if (groupid == 7)
            {
                return infoArray[6];
            }

            // idΪ����
            int id = 0;
            foreach (UserGroupInfo info in infoArray)
            {
                if (info.Groupid == groupid)
                {
                    return infoArray[id];
                }
                id++;
            }
            // ������Ҳ�����Ϊ�ο�
            return infoArray[6];

        }



        /// <summary>
        /// ͨ����ID�õ���������ַ�Χ,����������򷵻ؿձ�
        /// </summary>
        /// <param name="groupid">��ID</param>
        /// <returns>���ַ�Χ</returns>
        public static DataTable GroupParticipateScore(int groupid)
        {
            DataTable dt = DatabaseProvider.GetInstance().GetUserGroupRateRange(groupid);
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0][0].ToString().Trim() == "")
                {
                    //���û���δ������������ַ�Χʱ���ؿձ�
                    return (DataTable)null;
                }
                else
                {

                    //��������ʼ����ṹ
                    DataTable templateDT = new DataTable("templateDT");

                    templateDT.Columns.Add("id", Type.GetType("System.Int32"));
                    //�Ƿ��������ֶ�
                    templateDT.Columns.Add("available", Type.GetType("System.Boolean"));
                    //���ִ���
                    templateDT.Columns.Add("ScoreCode", Type.GetType("System.Int32"));
                    //��������
                    templateDT.Columns.Add("ScoreName", Type.GetType("System.String"));
                    //������Сֵ
                    templateDT.Columns.Add("Min", Type.GetType("System.String"));
                    //�������ֵ
                    templateDT.Columns.Add("Max", Type.GetType("System.String"));
                    //24Сʱ���������
                    templateDT.Columns.Add("MaxInDay", Type.GetType("System.String"));

                    //options HTML���� 
                    templateDT.Columns.Add("Options", Type.GetType("System.String"));
                    //templateDT.Columns["Options"].MaxLength = 2000;

                    //����м���Ĭ������
                    for (int rowcount = 0; rowcount < 8; rowcount++)
                    {
                        DataRow dr = templateDT.NewRow();
                        dr["id"] = rowcount + 1;
                        dr["available"] = false;
                        dr["ScoreCode"] = rowcount + 1;
                        dr["ScoreName"] = "";
                        dr["Min"] = "";
                        dr["Max"] = "";
                        dr["MaxInDay"] = "";
                        templateDT.Rows.Add(dr);
                    }

                    //ͨ��CONFIG�ļ��õ���ص�ScoreName��������
                    DataRow scoresetname = Scoresets.GetScoreSet().Rows[0];
                    for (int count = 0; count < 8; count++)
                    {
                        if ((scoresetname[count + 2].ToString().Trim() != "") && (scoresetname[count + 2].ToString().Trim() != "0"))
                        {
                            templateDT.Rows[count]["ScoreName"] = scoresetname[count + 2].ToString().Trim();
                        }
                    }

                    //�����ݿ��еļ�¼������װ���Ĭ������
                    int i = 0;
                    foreach (string raterangestr in dt.Rows[0][0].ToString().Trim().Split('|'))
                    {
                        if (raterangestr.Trim() != "")
                        {
                            string[] scoredata = raterangestr.Split(',');
                            //�ж��Ƿ��������ֶε������ж�
                            if (scoredata[1].Trim() == "True")
                            {
                                templateDT.Rows[i]["available"] = true;
                            }
                            //���������ֶ�
                            templateDT.Rows[i]["Min"] = scoredata[4].Trim();
                            templateDT.Rows[i]["Max"] = scoredata[5].Trim();
                            templateDT.Rows[i]["MaxInDay"] = scoredata[6].Trim();
                        }
                        i++;
                    }
                    return templateDT;
                }
            }

            //���û��鲻����ʱ���ؿ�
            return null;

        }


        /// <summary>
        /// ͨ����ID��UID�õ���������ַ�Χ,����������򷵻ؿձ�
        /// </summary>
        /// <param name="uid">�û�ID</param>
        /// <param name="gid">�û����</param>
        /// <returns>ID��UID��������ַ�Χ</returns>
        public static DataTable GroupParticipateScore(int uid, int gid)
        {
            DataTable dt = GroupParticipateScore(gid);
            int[] extcredits = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            StringBuilder sb = new StringBuilder();
            int offset = 1;
            if (dt != null)
            {
                IDataReader reader = DatabaseProvider.GetInstance().GetUserTodayRate(uid);
                while (reader.Read())
                {
                    extcredits[Utils.StrToInt(reader["extcredits"], 0)] = Utils.StrToInt(reader["todayrate"], 0);
                }
                reader.Close();

                DataRow dr = null;
                for (int r = dt.Rows.Count - 1; r >= 0; r--)
                {
                    dr = dt.Rows[r];
                    if (!Convert.ToBoolean(dr["available"]))
                    {
                        dr.Delete();
                        continue;
                    }
                    dr["MaxInDay"] = Utils.StrToInt(dr["MaxInDay"], 0) - extcredits[Utils.StrToInt(dr["ScoreCode"], 0)];
                    if (Utils.StrToInt(dr["MaxInDay"], 0) <= 0)
                    {
                        dr.Delete();
                        continue;
                    }

                    offset = Convert.ToInt32(Math.Abs(Math.Ceiling((Utils.StrToInt(dr["Max"], 0) - Utils.StrToInt(dr["Min"], 0)) / 32.0)));

                    sb.Remove(0, sb.Length);
                    for (int i = Utils.StrToInt(dr["Min"], 0); i <= Utils.StrToFloat(dr["Max"], 0); i += offset)
                    {
                        if (Math.Abs(i) <= Utils.StrToInt(dr["MaxInDay"], 0))
                        {
                            sb.Append("\n<option value=\"");
                            sb.Append(i);
                            sb.Append("\">");
                            sb.Append(i > 0 ? "+" : "");
                            sb.Append(i);
                            sb.Append("</option>");
                        }
                    }
                    dr["Options"] = sb.ToString();
                }
                dt.AcceptChanges();
            }
            if (dt == null)
            {
                return new DataTable();
            }
            return dt;

        }

    }
}
