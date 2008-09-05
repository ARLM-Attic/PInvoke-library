using System;
using System.Data;
using System.Text;

using Discuz.Common;
using Discuz.Config;
using Discuz.Mall.Data;
using Discuz.Entity;

namespace Discuz.Mall
{
    /// <summary>
    /// 商品所在地管理操作类
    /// </summary>
    public class Locations : WriteFile
    {
        #region 私有变量
        private static volatile Locations instance = null;
        private static object lockHelper = new object();
        private static string jsonPath = "";
        #endregion

        #region 返回唯一实例
        private Locations()
        {
            jsonPath = Utils.GetMapPath(BaseConfigs.GetForumPath + "\\javascript\\locations.js");
        }

        public static Locations GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                        {
                            instance = new Locations();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion


        /// <summary>
        /// 通过指定的LID获取所有地. 格式: 省/州,城市
        /// </summary>
        /// <param name="lid">所在地id</param>
        /// <returns></returns>
        public static string GetLocusByLID(int lid)
        {
            DataTable dt = DbProvider.GetInstance().GetLocusByLID(lid);
            string locus = "";
            if (dt.Rows.Count > 0)
            {
                locus = dt.Rows[0]["state"].ToString() + "," + dt.Rows[0]["city"].ToString();
            }
            dt.Dispose();
            return locus;
        }

        /// <summary>
        /// 生成商品分类表的JSON文件
        /// </summary>
        /// <returns>是否写入成功</returns>
        public override bool WriteJsonFile()
        {
            StringBuilder sb_locations = new StringBuilder();

            sb_locations.Append("var locations = ");
            sb_locations.Append(Utils.DataTableToJSON(DbProvider.GetInstance().GetLocationsTable()));

            sb_locations.Append("\r\n\r\nvar states = ");
            sb_locations.Append(Utils.DataTableToJSON(DbProvider.GetInstance().GetStatesTable()));

            sb_locations.Append("\r\n\r\nvar countries = ");
            sb_locations.Append(Utils.DataTableToJSON(DbProvider.GetInstance().GetCountriesTable()));

            return base.WriteJsonFile(jsonPath, sb_locations);
        }

        /// <summary>
        /// 数据转换对象类
        /// </summary>
        public class DTO
        {
            /// <summary>
            /// 获得所在地信息(DTO)
            /// </summary>
            /// <param name="__idatareader">要转换的数据</param>
            /// <returns>返回所在地信息</returns>
            public static Locationinfo GetLocationInfo(IDataReader reader)
            {
                if (reader.Read())
                {
                    Locationinfo locationinfo = new Locationinfo();
                    locationinfo.Lid = Convert.ToInt32(reader["lid"].ToString());
                    locationinfo.City = reader["city"].ToString().Trim();
                    locationinfo.State = reader["state"].ToString().Trim();
                    locationinfo.Country = reader["country"].ToString().Trim();
                    locationinfo.Zipcode = reader["zipcode"].ToString().Trim();

                    reader.Close();
                    return locationinfo;
                }
                return null;
            }


            public static Locationinfo[] GetLocationInfoArray(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                    return null;

                Locationinfo[] locationinfoarray = new Locationinfo[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    locationinfoarray[i] = new Locationinfo();
                    locationinfoarray[i].Lid = Convert.ToInt32(dt.Rows[i]["lid"].ToString());
                    locationinfoarray[i].City = dt.Rows[i]["city"].ToString();
                    locationinfoarray[i].State = dt.Rows[i]["state"].ToString();
                    locationinfoarray[i].Country = dt.Rows[i]["country"].ToString();
                    locationinfoarray[i].Zipcode = dt.Rows[i]["zipcode"].ToString();

                }
                dt.Dispose();
                return locationinfoarray;
            }
        }
    }
}
