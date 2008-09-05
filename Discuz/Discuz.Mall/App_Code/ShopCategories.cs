using System;
using System.Data;
using System.IO;
using System.Text;

using Discuz.Common;
using Discuz.Entity;
using Discuz.Config;
using Discuz.Mall.Data;
using Discuz.Forum;

namespace Discuz.Mall
{
    /// <summary>
    /// ���̷�����������
    /// </summary>
    public class ShopCategories
    {

        /// <summary>
        /// �������̷���
        /// </summary>
        /// <param name="shopcategoryinfo">���̷�����Ϣ</param>
        /// <param name="targetshopcategoryinfo">Ҫ�����Ŀ�������Ϣ</param>
        /// <param name="addtype">��ӷ�ʽ(1:��Ϊͬ������ 2:��Ϊ�ӷ��� ����:�����)</param>
        /// <returns>�������̷���id</returns>
        public static int CreateShopCategory(Shopcategoryinfo shopcategoryinfo, Shopcategoryinfo targetshopcategoryinfo, int addtype)
        {
            switch(addtype)
            {
                case 1: //��Ϊͬ������
                    {
                        shopcategoryinfo.Parentid = targetshopcategoryinfo.Parentid;
                        shopcategoryinfo.Parentidlist = targetshopcategoryinfo.Parentidlist;
                        shopcategoryinfo.Layer = targetshopcategoryinfo.Layer;
                        break;
                    }
                case 2: //��Ϊ�ӷ���
                    {
                        shopcategoryinfo.Parentid = targetshopcategoryinfo.Categoryid;
                        shopcategoryinfo.Parentidlist = targetshopcategoryinfo.Parentidlist == "0" ? targetshopcategoryinfo.Categoryid.ToString() : targetshopcategoryinfo.Parentidlist + "," + targetshopcategoryinfo.Categoryid;
                        shopcategoryinfo.Layer = targetshopcategoryinfo.Layer + 1;
                        break;
                    }
                default:
                    {
                        shopcategoryinfo.Parentid = 0;
                        shopcategoryinfo.Parentidlist = "0";
                        shopcategoryinfo.Layer = 0;
                        break;
                    }
            }
            return CreateShopCategory(shopcategoryinfo);
        }

        /// <summary>
        /// �������̷���
        /// </summary>
        /// <param name="shopcategoryinfo">���̷�����Ϣ</param>
        /// <returns>�������̷���id</returns>
        public static int CreateShopCategory(Shopcategoryinfo shopcategoryinfo)
        {
            int returnval =  DbProvider.GetInstance().CreateShopCategory(shopcategoryinfo);
            SetShopCategoryDispalyorder(shopcategoryinfo.Shopid);
            return returnval;
        }

        /// <summary>
        /// ��ȡָ�����̵���Ʒ����
        /// </summary>
        /// <param name="shopid">����id</param>
        /// <returns>������Ʒ�����</returns>
        public static DataTable GetShopCategoryTable(int shopid)
        {
            return DbProvider.GetInstance().GetShopCategoryTableToJson(shopid);
        }

        /// <summary>
        /// ��ȡ���̵���Ʒ��������(json��ʽ)
        /// </summary>
        /// <param name="shopid">����id</param>
        /// <returns>���̵���Ʒ��������</returns>
        public static string GetShopCategoryJson(DataTable dt)
        {
            StringBuilder sb_category = new StringBuilder();
            sb_category.Append(Utils.DataTableToJSON(dt));
            return sb_category.ToString();
        }

        /// <summary>
        /// ��ȡ���̵���Ʒ��������(option��ʽ)
        /// </summary>
        /// <param name="shopid">����id</param>
        /// <returns>��Ʒ��������</returns>
        public static string GetShopCategoryOption(DataTable dt, bool optgroup)
        {
            StringBuilder sb_category = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                if (optgroup && dr["childcount"].ToString() != "0")
                {
                    sb_category.Append("<optgroup label=\"" + dr["name"].ToString() + "\"></optgroup>");
                }
                else
                {
                    sb_category.Append("<option value=\"");
                    sb_category.Append(dr["categoryid"].ToString());
                    sb_category.Append("\">");
                    sb_category.Append(Utils.GetSpacesString(Utils.StrToInt(dr["layer"].ToString(), 0)));
                    sb_category.Append(dr["name"].ToString().Trim());
                    sb_category.Append("</option>");
                }
            }
            return sb_category.ToString();
        }

        /// <summary>
        /// ��ȡָ������id�ĵ�����Ʒ��������
        /// </summary>
        /// <param name="categoryid">����id</param>
        /// <returns>������Ʒ������Ϣ</returns>
        public static Shopcategoryinfo GetShopCategoryByCategoryId(int categoryid)
        {
            return DTO.GetShopCategoryInfo(DbProvider.GetInstance().GetShopCategoryByCategoryId(categoryid));
        }

        /// <summary>
        /// ɾ��ָ���ĵ�����Ʒ����
        /// </summary>
        /// <param name="categoryid">Ҫɾ���ĵ�����Ʒ����id</param>
        /// <returns>�Ƿ�ɾ���ɹ�</returns>
        public static bool DeleteCategoryByCategoryId(Shopcategoryinfo shopcategoryinfo)
        {
            if (DbProvider.GetInstance().IsExistSubShopCategory(shopcategoryinfo))
            {
                return false;
            }

            DbProvider.GetInstance().DeleteShopCategory(shopcategoryinfo);
            return true;
        }

        /// <summary>
        /// �ƶ���Ʒ����
        /// </summary>
        /// <param name="shopcategoryinfo">Դ������Ʒ����</param>
        /// <param name="targetshopcategoryinfo">Ŀ�������Ʒ����</param>
        /// <param name="isaschildnode">�Ƿ���Ϊ�ӽڵ�</param>
        /// <returns>�Ƿ��ƶ��ɹ�</returns>
        public static bool MoveShopCategory(Shopcategoryinfo shopcategoryinfo, Shopcategoryinfo targetshopcategoryinfo, bool isaschildnode)
        {
            DbProvider.GetInstance().MovingShopCategoryPos(shopcategoryinfo, targetshopcategoryinfo, isaschildnode);
            SetShopCategoryDispalyorder(targetshopcategoryinfo.Shopid);
            return true;
        }

        /// <summary>
        /// ���õ�����Ʒ������ʾ˳��
        /// </summary>
        public static void SetShopCategoryDispalyorder(int shopid)
        {
            DataTable dt = DbProvider.GetInstance().GetShopCategoryByShopId(shopid);

            //���µ�����Ʒ�����µ��ӷ�����
            foreach (DataRow dr in dt.Rows)
            {
                DbProvider.GetInstance().UpdateShopCategoryChildCount(dt.Select("parentid=" + dr["categoryid"].ToString()).Length, int.Parse(dr["categoryid"].ToString()));
            }

            if (dt.Rows.Count == 1) return;

            int displayorder = 1;
            string categoryidlist;
            foreach (DataRow dr in dt.Select("parentid=0"))
            {
                if (dr["parentid"].ToString() == "0")
                {
                    ChildNode = "0";
                    categoryidlist = ("," + FindChildNode(dr["categoryid"].ToString())).Replace(",0,", "");

                    foreach (string categoryid in categoryidlist.Split(','))
                    {
                        DbProvider.GetInstance().UpdateShopCategoryDisplayOrder(displayorder, int.Parse(categoryid));
                        displayorder++;
                    }

                }
            }
        }


        #region  �ݹ�ָ����̳����µ������Ӱ��

        public static string ChildNode = "0";

        /// <summary>
        /// �ݹ������ӽڵ㲢�����ַ���
        /// </summary>
        /// <param name="correntfid">��ǰ</param>
        /// <returns>�Ӱ��ļ���,��ʽ:1,2,3,4,</returns>
        public static string FindChildNode(string categoryid)
        {
            lock (ChildNode)
            {
                DataTable dt = DbProvider.GetInstance().GetCategoryidInShopByParentid(int.Parse(categoryid));

                ChildNode = ChildNode + "," + categoryid;

                if (dt.Rows.Count > 0)
                {
                    //���ӽڵ�
                    foreach (DataRow dr in dt.Rows)
                    {
                        FindChildNode(dr["categoryid"].ToString());
                    }
                    dt.Dispose();
                }
                else
                {
                    dt.Dispose();
                }
                return ChildNode;
            }
        }

        #endregion

        /// <summary>
        /// ���µ��̷���
        /// </summary>
        /// <param name="shopcategoryinfo">���̷�����Ϣ</param>
        /// <returns>�Ƿ���³ɹ�</returns>
        public static bool UpdateShopCategory(Shopcategoryinfo shopcategoryinfo)
        {
            return DbProvider.GetInstance().UpdateShopCategory(shopcategoryinfo);
        }

        /// <summary>
        /// ����ת��������
        /// </summary>
        public class DTO
        {

            /// <summary>
            /// ��õ��̷�����Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת��������</param>
            /// <returns>���ص��̷�����Ϣ</returns>
            public static Shopcategoryinfo GetShopCategoryInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Shopcategoryinfo shopcategoryinfo = new Shopcategoryinfo();
                    shopcategoryinfo.Categoryid = Convert.ToInt32(reader["categoryid"].ToString());
                    shopcategoryinfo.Parentid = Convert.ToInt32(reader["parentid"].ToString());
                    shopcategoryinfo.Parentidlist = reader["parentidlist"].ToString().Trim();
                    shopcategoryinfo.Layer = Convert.ToInt32(reader["layer"].ToString());
                    shopcategoryinfo.Childcount = Convert.ToInt32(reader["childcount"].ToString());
                    shopcategoryinfo.Syscategoryid = Convert.ToInt32(reader["syscategoryid"].ToString());
                    shopcategoryinfo.Name = reader["name"].ToString().Trim();
                    shopcategoryinfo.Categorypic = reader["categorypic"].ToString().Trim();
                    shopcategoryinfo.Shopid = Convert.ToInt32(reader["shopid"].ToString());
                    shopcategoryinfo.Displayorder = Convert.ToInt32(reader["displayorder"].ToString());

                    reader.Close();
                    return shopcategoryinfo;
                }
                return null;
            }

            /// <summary>
            /// ��õ��̷�����Ϣ(DTO)
            /// </summary>
            /// <param name="__idatareader">Ҫת�������ݱ�</param>
            /// <returns>���ص��̷�����Ϣ</returns>
            public static Shopcategoryinfo[] GetShopCategoryArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Shopcategoryinfo[] shopcategoryinfoarray = new Shopcategoryinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    shopcategoryinfoarray[i] = new Shopcategoryinfo();
                    shopcategoryinfoarray[i].Categoryid = Convert.ToInt32(dt.Rows[i]["categoryid"].ToString());
                    shopcategoryinfoarray[i].Parentid = Convert.ToInt32(dt.Rows[i]["parentid"].ToString());
                    shopcategoryinfoarray[i].Parentidlist = dt.Rows[i]["parentidlist"].ToString().Trim();
                    shopcategoryinfoarray[i].Layer = Convert.ToInt32(dt.Rows[i]["layer"].ToString());
                    shopcategoryinfoarray[i].Childcount = Convert.ToInt32(dt.Rows[i]["childcount"].ToString());
                    shopcategoryinfoarray[i].Syscategoryid = Convert.ToInt32(dt.Rows[i]["syscategoryid"].ToString());
                    shopcategoryinfoarray[i].Name = dt.Rows[i]["name"].ToString();
                    shopcategoryinfoarray[i].Categorypic = dt.Rows[i]["categorypic"].ToString();
                    shopcategoryinfoarray[i].Shopid = Convert.ToInt32(dt.Rows[i]["shopid"].ToString());
                    shopcategoryinfoarray[i].Displayorder = Convert.ToInt32(dt.Rows[i]["displayorder"].ToString());

                }
                dt.Dispose();
                return shopcategoryinfoarray;
            }

        }
    }

    
}
