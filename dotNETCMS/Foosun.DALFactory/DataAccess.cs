using System.Reflection;
using System.Configuration;

namespace Foosun.DALFactory
{
    public sealed partial class DataAccess
    {
        private static readonly string path =Foosun.Config.UIConfig.WebDAL;
       
        public  DataAccess() { }
    }
}
