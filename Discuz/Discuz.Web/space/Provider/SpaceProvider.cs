using System;
using System.Data;
using Discuz.Cache;
using Discuz.Common;
#if NET1
#else
using Discuz.Common.Generic;
#endif
using Discuz.Forum;
using Discuz.Space.Entities;
using Discuz.Entity;
using Discuz.Data;

namespace Discuz.Space.Provider
{
	/// <summary>
	/// SpaceProvider 的摘要说明。
	/// </summary>
	public class SpaceProvider
	{
		public SpaceProvider()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 对ThemeInfo的操作

		

		public static ThemeInfo GetThemeInfoById(int themeInfoId)
		{

#if NET1
            ThemeInfoCollection themes = GetThemeInfos();
#else
            List<ThemeInfo> themes = GetThemeInfos();
#endif
            foreach (ThemeInfo info in themes)
            {
                if (info.ThemeId == themeInfoId)
                {
                    return info;
                }
            }

            return null;
		}

#if NET1
        public static ThemeInfoCollection GetThemeInfos()
#else
        public static Discuz.Common.Generic.List<ThemeInfo> GetThemeInfos()
#endif
        {
            DNTCache cache = DNTCache.GetCacheService();
#if NET1
            ThemeInfoCollection themes = cache.RetrieveObject("/Space/ThemeList") as ThemeInfoCollection;
#else
            List<ThemeInfo> themes = cache.RetrieveObject("/Space/ThemeList") as List<ThemeInfo>;
#endif

            if (themes == null)
            {
                IDataReader reader = Space.Data.DbProvider.GetInstance().GetThemeInfos();
                themes = GetThemeInfoArray(reader);

                cache.AddObject("/Space/ThemeList", themes);
            }

            return themes;
        }
        
#if NET1
        private static ThemeInfoCollection GetThemeInfoArray(IDataReader reader)
#else
        private static Discuz.Common.Generic.List<ThemeInfo> GetThemeInfoArray(IDataReader reader)
#endif
		{
			if (reader == null)
				return null;
#if NET1
			ThemeInfoCollection tic = new ThemeInfoCollection();
#else
            Discuz.Common.Generic.List<ThemeInfo> tic = new Discuz.Common.Generic.List<ThemeInfo>();
#endif
			while (reader.Read())
			{
				tic.Add(GetThemeEntity(reader));
			}
            reader.Close();
			return tic;
		}

		private static ThemeInfo GetThemeEntity(IDataReader reader)
		{
			ThemeInfo ti = new ThemeInfo();
			ti.ThemeId = Utils.StrToInt(reader["themeid"], 0);
			ti.Directory = reader["directory"].ToString();
			ti.Name = reader["name"].ToString();
			ti.Type = Utils.StrToInt(reader["type"], 0);
			ti.Author = reader["author"].ToString();
			ti.CreateDate = reader["createdate"].ToString();
			ti.CopyRight = reader["copyright"].ToString();

			return ti;
		}
		#endregion

        //#region 对TemplateInfo的操作
        //public static TemplateInfoCollection GetTemplateInfos()
        //{
        //    IDataReader reader = Space.Data.DbProvider.GetInstance().GetTemplateInfos();
			
        //    return GetTemplateInfoArray(reader);
        //}

        //public static TemplateInfo GetTemplateInfoById(int templateInfoId)
        //{
        //    IDataReader reader = Space.Data.DbProvider.GetInstance().GetTemplateInfoById(templateInfoId);

        //    return reader.Read() ? GetTemplateEntity(reader) : null;
        //}

        //private static TemplateInfoCollection GetTemplateInfoArray(IDataReader reader)
        //{
        //    if (reader == null)
        //        return null;
        //    TemplateInfoCollection tic = new TemplateInfoCollection();

        //    while (reader.Read())
        //    {
        //        tic.Add(GetTemplateEntity(reader));
        //    }

        //    return tic.Count == 0 ? null : tic;
        //}

        //private static TemplateInfo GetTemplateEntity(IDataReader reader)
        //{
        //    TemplateInfo ti = new TemplateInfo();
        //    ti.TemplateId = Utils.StrToInt(reader["templateid"], 0);
        //    ti.Name = reader["name"].ToString();
        //    ti.Path = reader["path"].ToString();

        //    return ti;
        //}
        //#endregion

		#region 对ModuleDefInfo的操作


        /// <summary>
        /// 根据Url获得ModuleDef对象
        /// </summary>
        /// <returns></returns>
        public static ModuleDefInfo GetModuleDefInfoByUrl(string moduleDefInfoUrl)
        {
#if NET1
            ModuleDefInfoCollection mdilist = GetModuleDefInfoList();
#else
            List<ModuleDefInfo> mdilist = GetModuleDefInfoList();
#endif

            foreach (ModuleDefInfo info in mdilist)
            {
                if (info.ConfigFile == moduleDefInfoUrl)
                    return info;
            }

            return null;

            //IDataReader reader = Space.Data.DbProvider.GetInstance().GetModuleDefInfoById(moduleDefInfoId);

            //return reader.Read() ? GetModuleDefEntity(reader) : null;
        }

		/// <summary>
		/// 根据Id获得ModuleDef对象
		/// </summary>
		/// <param name="moduleDefInfoId"></param>
		/// <returns></returns>
		public static ModuleDefInfo GetModuleDefInfoById(int moduleDefInfoId)
		{
#if NET1
            ModuleDefInfoCollection mdilist = GetModuleDefInfoList();
#else
            List<ModuleDefInfo> mdilist = GetModuleDefInfoList();
#endif

		    foreach (ModuleDefInfo info in mdilist)
		    {
		        if (info.ModuleDefID == moduleDefInfoId)
		            return info;
		    }

		    return null;

            //IDataReader reader = Space.Data.DbProvider.GetInstance().GetModuleDefInfoById(moduleDefInfoId);

            //return reader.Read() ? GetModuleDefEntity(reader) : null;
		}

#if NET1
        public static ModuleDefInfoCollection GetModuleDefInfoList()
#else
        public static List<ModuleDefInfo> GetModuleDefInfoList()
#endif
        {
            DNTCache cache = DNTCache.GetCacheService();

#if NET1
            ModuleDefInfoCollection result = cache.RetrieveObject("/Space/ModuleDefList") as ModuleDefInfoCollection;
#else
            List<ModuleDefInfo> result = cache.RetrieveObject("/Space/ModuleDefList") as List<ModuleDefInfo>;
#endif

            if (result == null)
            {
                result = GetModuleDefInfoArray(Space.Data.DbProvider.GetInstance().GetModuleDefList());

                cache.AddObject("/Space/ModuleDefList", result);
            }

            return result;
        }

#if NET1
		private static ModuleDefInfoCollection GetModuleDefInfoArray(IDataReader reader)
#else
        private static Discuz.Common.Generic.List<ModuleDefInfo> GetModuleDefInfoArray(IDataReader reader)
#endif
		{
			if (reader == null)
				return null;
#if NET1
            ModuleDefInfoCollection mdic = new ModuleDefInfoCollection();
#else
            Discuz.Common.Generic.List<ModuleDefInfo> mdic = new Discuz.Common.Generic.List<ModuleDefInfo>();
#endif
			while (reader.Read())
			{
				mdic.Add(GetModuleDefEntity(reader));
			}
            reader.Close();
			return mdic.Count == 0 ? null : mdic;
		}

		private static ModuleDefInfo GetModuleDefEntity(IDataReader reader)
		{
			ModuleDefInfo mdi = new ModuleDefInfo();
			mdi.ModuleDefID = Utils.StrToInt(reader["moduledefid"], 0);
			mdi.ModuleName = reader["modulename"].ToString();
			mdi.CacheTime = Utils.StrToInt(reader["cachetime"], 0);
			mdi.ConfigFile = reader["configfile"].ToString();
			mdi.BussinessController = reader["controller"].ToString();

			return mdi;
		}

        public static void UpdateModuleDefInfo(ModuleDefInfo mdi)
        {
            Space.Data.DbProvider.GetInstance().UpdateModuleDef(mdi);
        }
        
        public static void AddModuleDefInfo(ModuleDefInfo mdi)
        {
            Space.Data.DbProvider.GetInstance().AddModuleDefInfo(mdi);
        }

        public static void DeleteModuleDefByUrl(string url)
        {
            Space.Data.DbProvider.GetInstance().DeleteModuleDefByUrl(url);
        }
		#endregion

		#region 对ModuleInfo的操作

		public static int GetModulesCountByTabId(int tabId, int uid)
		{
			return Space.Data.DbProvider.GetInstance().GetModulesCountByTabId(tabId, uid);
		}

#if NET1
		public static ModuleInfoCollection GetModuleInfosByTabId(int tabId, int uid)
#else
        public static Discuz.Common.Generic.List<ModuleInfo> GetModuleInfosByTabId(int tabId, int uid)
#endif
    	{
			IDataReader reader = Space.Data.DbProvider.GetInstance().GetModulesByTabId(tabId, uid);

			return GetModuleInfoArray(reader);
		}

		public static ModuleInfo GetModuleInfoById(int moduleInfoId, int uid)
		{
			IDataReader reader = Space.Data.DbProvider.GetInstance().GetModuleInfoById(moduleInfoId, uid);

			return reader.Read() ? GetModuleEntity(reader) : null;
		}

#if NET1
		private static ModuleInfoCollection GetModuleInfoArray(IDataReader reader)
#else
        private static Discuz.Common.Generic.List<ModuleInfo> GetModuleInfoArray(IDataReader reader)
#endif
    	{
			if (reader == null)
				return null;

#if NET1
			ModuleInfoCollection mdic = new ModuleInfoCollection();
#else
            Discuz.Common.Generic.List<ModuleInfo> mdic = new Discuz.Common.Generic.List<ModuleInfo>();
#endif
			while (reader.Read())
			{
				mdic.Add(GetModuleEntity(reader));
			}
            reader.Close();
			return mdic.Count == 0 ? null : mdic;
		}

		private static ModuleInfo GetModuleEntity(IDataReader reader)
		{
			ModuleInfo mi = new ModuleInfo();
			mi.ModuleID = Utils.StrToInt(reader["moduleid"], 0);
			mi.TabID = Utils.StrToInt(reader["tabid"], 0);
			mi.Uid = Utils.StrToInt(reader["uid"], -1);
			mi.ModuleDefID = Utils.StrToInt(reader["moduledefid"], 0);
			mi.PaneName = reader["panename"].ToString();
			mi.DisplayOrder = Utils.StrToInt(reader["displayorder"], 0);
			mi.UserPref = reader["userpref"].ToString();
			mi.Val = Utils.StrToInt(reader["val"], 0);
			mi.ModuleUrl = reader["moduleurl"].ToString();
			int moduletype = Utils.StrToInt(reader["moduletype"], 4);
			if (moduletype < 1 || moduletype > 4)
			{
				moduletype = 4;
			}
			mi.ModuleType = (ModuleType)moduletype;

			return mi;
		}

#if NET1
        public static ModuleInfoCollection GetModuleCollectionByUserId(int uid)
#else
        public static Common.Generic.List<ModuleInfo> GetModuleCollectionByUserId(int uid)
#endif
        {
            IDataReader reader = Space.Data.DbProvider.GetInstance().GetModulesByUserId(uid);

            return GetModuleInfoArray(reader);
        }

		#endregion

		#region 对TabInfo的操作

		/// <summary>
		/// 根据Uid获得TabInfo
		/// </summary>
		/// <param name="uid"></param>
		/// <returns></returns>
#if NET1         
        public static TabInfoCollection GetTabInfosByUid(int uid)
#else
		public static Discuz.Common.Generic.List<TabInfo> GetTabInfosByUid(int uid)
#endif
		{
			IDataReader reader = Space.Data.DbProvider.GetInstance().GetTabInfosByUid(uid);

//			TabInfoCollection tabc = new TabInfoCollection();
//
//			while (reader.Read())
//			{
//				tabc.Add(GetTabEntity(reader));
//			}
//
//			return tabc.Count == 0 ? null : tabc;
			return GetTabInfoArray(reader);
		}


		public static TabInfo GetTabInfoById(int tabInfoId, int uid)
		{
			IDataReader reader = Space.Data.DbProvider.GetInstance().GetTabInfoById(tabInfoId, uid);

			return reader.Read() ? GetTabEntity(reader) : null;
		}

#if NET1
        private static TabInfoCollection GetTabInfoArray(IDataReader reader)
#else
		private static Discuz.Common.Generic.List<TabInfo> GetTabInfoArray(IDataReader reader)
#endif
		{
			if (reader == null)
				return null;
#if NET1
			TabInfoCollection tabc = new TabInfoCollection();
#else
            Discuz.Common.Generic.List<TabInfo> tabc = new Discuz.Common.Generic.List<TabInfo>();
#endif
			while (reader.Read())
			{
				tabc.Add(GetTabEntity(reader));
			}
            reader.Close();
			return tabc.Count == 0 ? null : tabc;
		}


		private static TabInfo GetTabEntity(IDataReader reader)
		{
			TabInfo tab = new TabInfo();
			tab.TabID = Utils.StrToInt(reader["tabid"], 0);
			tab.UserID = Utils.StrToInt(reader["uid"], 0);
			tab.DisplayOrder = Utils.StrToInt(reader["displayorder"], 0);
			tab.TabName = reader["tabname"].ToString();
			tab.IconFile = reader["iconfile"].ToString();
			tab.Template = reader["template"].ToString();

			return tab;
		}
		#endregion

        /// <summary>
        /// 获取使用指定Tag的空间日志数
        /// </summary>
        /// <param name="tagid">TagId</param>
        /// <returns></returns>
        public static int GetSpacePostCountWithSameTag(int tagid)
        {
            return Space.Data.DbProvider.GetInstance().GetSpacePostCountWithSameTag(tagid);
        }
    }
}
