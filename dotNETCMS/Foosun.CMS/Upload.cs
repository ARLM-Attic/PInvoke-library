using System;
using System.Collections.Generic;
using System.Data;
using Foosun.Model;
using Foosun.DALFactory;

namespace Foosun.CMS
{
    public class Upload
    {
        private IrootPublic ir;
        public Upload()
        {
            ir = DataAccess.CreaterootPublic();
        }
        public DataTable getUploadInfo()
        {
            DataTable dt = ir.getUploadInfo();
            return dt;
        }
    }
}
