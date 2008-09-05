using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using Foosun.Config;
using Foosun.DALProfile;

namespace Foosun.AccessDAL
{
    public class DbBase : IDbBase
    {
        DbCommand IDbBase.CreateCommand()
        {
            return new OleDbCommand();
        }
        DbConnection IDbBase.CreateConnection()
        {
            return new OleDbConnection();
        }
        DbDataAdapter IDbBase.CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }
        DbParameter IDbBase.CreateParameter()
        {
            return new OleDbParameter();
        }
        protected string Pre;
        public DbBase()
        {
            Pre = DBConfig.TableNamePrefix;
            DbHelper.Provider = this;
        }
    }
}
