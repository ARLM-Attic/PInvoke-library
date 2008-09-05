using System;
using System.Collections.Generic;
using System.Data;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class Search
    {
        public static DataTable SearchGetPage(string DTable,int PageIndex, int PageSize, out int RecordCount, out int PageCount, Foosun.Model.Search si)
        {
            ISearch dal = DataAccess.CreateSearch();
            return dal.SearchGetPage(DTable,PageIndex, PageSize, out RecordCount, out PageCount, si);
        }
        public static string getSaveClassframe(string ClassID)
        {
            ISearch dal = DataAccess.CreateSearch();

            return dal.getSaveClassframe(ClassID);
        }
    }
}
