using System;
using System.Data;
using System.Xml;
using System.Data.Common;
using Discuz.Data;
using MySql.Data.MySqlClient;


namespace Discuz.Data
{
    
    /// <summary>
    /// MySql数据库的Discuz!NT接口, 有关MySql的更多信息见 http://dev.mysql.com/downloads/connector/net/5.0.html
    /// </summary>
    /// 
#if NET1
    public class MySqlProvider : IDbProvider
    {
        public IDbProviderFactory Instance()
        {
            return (IDbProviderFactory) MySqlClientFactory.Instance;
        }

        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as MySqlCommand) != null)
            {
                MySqlCommandBuilder.DeriveParameters(cmd as MySqlCommand);
            }
        }

        public IDataParameter MakeParam(string ParamName, DbType DbType, Int32 Size)
        {
            MySqlParameter param;

            if (Size > 0)
                param = new MySqlParameter(ParamName, (MySqlDbType)DbType, Size);
            else
                param = new MySqlParameter(ParamName, (MySqlDbType)DbType);

            return (IDataParameter) param;
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
			return "SELECT LAST_INSERT_ID()";
		}
		public bool IsDbOptimize()
		{

			return true;
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

    public class MySqlClientFactory : IDbProviderFactory
    {
        public static readonly MySqlClientFactory Instance;

        static MySqlClientFactory()
        {
            Instance = new MySqlClientFactory();
        }

        private MySqlClientFactory()
        {
        }


        public IDbConnection CreateConnection()
        {
            return new MySqlConnection();
        }

        
        public IDbCommand CreateCommand()
        {
            return new MySqlCommand();
        }

        public IDbDataAdapter CreateDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        //public void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        GC.SuppressFinalize(this);
        //    }
        //}

       
 

    }
#else

    public class MySqlProvider : Discuz.Data.IDbProvider
    {
        public DbProviderFactory Instance()
        {
            return MySqlClientFactory.Instance;
        }

        public void DeriveParameters(IDbCommand cmd)
        {
            if ((cmd as MySqlCommand) != null)
            {
                MySqlCommandBuilder.DeriveParameters(cmd as MySqlCommand);
            }
        }


        public DbParameter MakeParam(string ParamName, DbType DbType, Int32 Size)
        {
            MySqlParameter param;

            if (Size > 0)
                param = new MySqlParameter(ParamName, (MySqlDbType)DbType, Size);
            else
                param = new MySqlParameter(ParamName, (MySqlDbType)DbType);

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
            return "SELECT LAST_INSERT_ID()";
        }


        public bool IsDbOptimize()
        {

            return true;
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
