using System;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Text.RegularExpressions;

using Discuz.Common;
using Discuz.Data;
using Discuz.Forum;
using Discuz.Config;
using Discuz.Entity;

namespace Discuz.Forum
{
    /// <summary>
    /// �༭��������
    /// </summary>
    public class Editors
    {

        public static Regex[] regexCustomTag = null;

        static Editors()
        {
            InitRegexCustomTag();
        }

        /// <summary>
        /// ��ʼ���Զ����ǩ�����������
        /// </summary>
        public static void InitRegexCustomTag()
        {
            CustomEditorButtonInfo[] tagList = Editors.GetCustomEditButtonListWithInfo();
            if (tagList != null)
            {
                int tagCount = tagList.Length;

                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < tagCount; i++)
                {
                    if (builder.Length > 0)
                    {
                        builder.Remove(0, builder.Length);
                    }
                    builder.Append(@"(\[");
                    builder.Append(tagList[i].Tag);
                    if (tagList[i].Params > 1)
                    {
                        builder.Append("=");
                        for (int j = 2; j <= tagList[i].Params; j++)
                        {
                            builder.Append(@"(.*?)");
                            if (j < tagList[i].Params)
                            {
                                builder.Append(",");
                            }
                        }
                    }

                    builder.Append(@"\])([\s\S]+?)\[\/");
                    builder.Append(tagList[i].Tag);
                    builder.Append(@"\]");

                    regexCustomTag[i] = new Regex(builder.ToString(), RegexOptions.IgnoreCase);
                }
            }
        }

        /// <summary>
        /// ���¼��ز���ʼ���Զ����ǩ�����������
        /// </summary>
        /// <param name="smiliesList">�Զ����ǩ��������</param>
        public static void ResetRegexCustomTag(CustomEditorButtonInfo[] tagList)
        {
            int tagCount = tagList.Length;

            // �����Ŀ��ͬ�����´�������, ���ⷢ������Խ��
            if (regexCustomTag == null || tagCount != regexCustomTag.Length)
            {
                regexCustomTag = new Regex[tagCount];
            }

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < tagCount; i++)
            {

                if (builder.Length > 0)
                {
                    builder.Remove(0, builder.Length);
                }
                builder.Append(@"(\[");
                builder.Append(tagList[i].Tag);
                if (tagList[i].Params > 1)
                {
                    builder.Append("=");
                    for (int j = 2; j <= tagList[i].Params; j++)
                    {
                        builder.Append(@"(.*?)");
                        if (j < tagList[i].Params)
                        {
                            builder.Append(",");
                        }

                    }

                }

                builder.Append(@"\])([\s\S]+?)\[\/");
                builder.Append(tagList[i].Tag);
                builder.Append(@"\]");

                regexCustomTag[i] = new Regex(builder.ToString(), RegexOptions.IgnoreCase);

            }
        }


        /// <summary>
        /// ��DataReader�����Զ���༭����ť�б�
        /// </summary>
        /// <returns></returns>
        public static IDataReader GetCustomEditButtonList()
        {
            return DatabaseProvider.GetInstance().GetCustomEditButtonList();
        }

        /// <summary>
        /// ��DataTable�����Զ��尴ť�б�
        /// </summary>
        /// <returns></returns>
        public static DataTable GetCustomEditButtonListWithTable()
        {
            return DatabaseProvider.GetInstance().GetCustomEditButtonListWithTable();
        }



        /// <summary>
        /// ��CustomEditorButtonInfo������ʽ�����Զ��尴ť
        /// </summary>
        /// <returns></returns>
        public static CustomEditorButtonInfo[] GetCustomEditButtonListWithInfo()
        {
            Discuz.Cache.DNTCache cache = Discuz.Cache.DNTCache.GetCacheService();
            CustomEditorButtonInfo[] buttonArray = cache.RetrieveObject("/Forum/UI/CustomEditButtonInfo") as CustomEditorButtonInfo[];
            if (buttonArray == null)
            {
                DataTable dt = GetCustomEditButtonListWithTable();
                if (dt == null)
                {
                    return null;
                }

                if (dt.Rows.Count <= 0)
                {
                    return null;
                }

                buttonArray = new CustomEditorButtonInfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    buttonArray[i] = new CustomEditorButtonInfo();
                    buttonArray[i].Id = Utils.StrToInt(dt.Rows[i]["id"], 0);
                    buttonArray[i].Tag = dt.Rows[i]["Tag"].ToString();
                    buttonArray[i].Icon = dt.Rows[i]["Icon"].ToString();
                    buttonArray[i].Available = Utils.StrToInt(dt.Rows[i]["Available"], 0);
                    buttonArray[i].Example = dt.Rows[i]["Example"].ToString();
                    buttonArray[i].Explanation = dt.Rows[i]["Explanation"].ToString();
                    buttonArray[i].Params = Utils.StrToInt(dt.Rows[i]["Params"], 0);
                    buttonArray[i].Nest = Utils.StrToInt(dt.Rows[i]["Nest"], 0);
                    buttonArray[i].Paramsdefvalue = dt.Rows[i]["Paramsdefvalue"].ToString();
                    buttonArray[i].Paramsdescript = dt.Rows[i]["Paramsdescript"].ToString();
                    buttonArray[i].Replacement = dt.Rows[i]["Replacement"].ToString();
                }
                dt.Dispose();
                cache.AddObject("/Forum/UI/CustomEditButtonInfo", buttonArray);

                // ���黺�����¼���ʱ���³�ʼ�����������������
                ResetRegexCustomTag(buttonArray);
            }
            return buttonArray;
        }


    }
}
