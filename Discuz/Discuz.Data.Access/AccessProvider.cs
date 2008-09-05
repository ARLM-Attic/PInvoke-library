using System;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;


namespace Discuz.Data
{
#if NET1

    public class AccessProvider : IDbProvider
    {
        public IDbProviderFactory Instance()
        {
             return AccessClientFactory.Instance;
        }

        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as OleDbCommand) != null)
            {
                OleDbCommandBuilder.DeriveParameters(cmd as OleDbCommand);
            }
        }

        public IDataParameter MakeParam(string ParamName, DbType DbType, Int32 Size)
        {
            OleDbParameter param;

            if (Size > 0)
                param = new OleDbParameter(ParamName, (OleDbType)DbType, Size);
            else
                param = new OleDbParameter(ParamName, (OleDbType)DbType);

            return param;
        }

        public bool IsFullTextSearchEnabled()
        {
            return false;
        }

        public bool IsCompactDatabase()
        {
            return false;
        }

        public bool IsBackupDatabase()
        {
            return false;
        }


	   public string GetLastIdSql()
	   {
		   return "SELECT @@IDENTITY";
	   }
	   public bool IsDbOptimize()
	   {

		   return false;
	   }
	   public bool IsShrinkData()
	   {
		   return false;
	   }

	   public bool IsStoreProc()
	   {

		   return false;
	   }
    }

    public class AccessClientFactory : IDbProviderFactory
    {
        public static readonly AccessClientFactory Instance;

        static AccessClientFactory()
        {
            Instance = new AccessClientFactory();
        }

        private AccessClientFactory()
        {
        }


        public IDbConnection CreateConnection()
        {
            return new OleDbConnection();
        }

        
        public IDbCommand CreateCommand()
        {
            return new OleDbCommand();
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }

    }
#else

    public class AccessProvider : IDbProvider
    {

        public DbProviderFactory Instance()
        {
            return OleDbFactory.Instance;
        }

        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as OleDbCommand) != null)
            {
                OleDbCommandBuilder.DeriveParameters(cmd as OleDbCommand);
            }
        }


        public DbParameter MakeParam(string ParamName, DbType DbType, Int32 Size)
        {
            OleDbParameter param;

            if (Size > 0)
                param = new OleDbParameter(ParamName, (OleDbType)DbType, Size);
            else
                param = new OleDbParameter(ParamName, (OleDbType)DbType);

            return param;
        }

        public bool IsFullTextSearchEnabled()
        {
            return false;
        }

        public bool IsCompactDatabase()
        {
            return false;
        }

        public bool IsBackupDatabase()
        {
            return false;
        }

        public string GetLastIdSql()
        {
            return "SELECT @@IDENTITY";
        }

        public bool IsDbOptimize()
        {

            return false;
        }

        public bool IsShrinkData()
        {


            return false;
        }


        public bool IsStoreProc()
        {

            return false;
        }
    }
#endif
}
