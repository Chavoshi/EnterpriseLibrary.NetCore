using MySql.Data.MySqlClient;
using System.Data.Common;

namespace EnterpriseLibrary.Test.MySql
{
    /// <summary>
    /// This is a unique Factory class for MySQL Connector/Net.
    /// This is to avoid bugs that exist in version 6.10.
    /// https://bugs.mysql.com/bug.php?id=88660
    /// 
    /// This is a limited implementation that focuses on .NET Framework. (It does not support .NET Standard.)
    /// Also, it does not support Entity Framework.
    /// </summary>
    public class DetouringMySqlFactory : DbProviderFactory
    {
        public static DetouringMySqlFactory Instance = new DetouringMySqlFactory();
        public override bool CanCreateDataSourceEnumerator => false;
        public override DbConnectionStringBuilder CreateConnectionStringBuilder() => new MySqlConnectionStringBuilder();
        public override DbConnection CreateConnection() => new MySqlConnection();
        public override DbCommandBuilder CreateCommandBuilder() => new MySqlCommandBuilder();
        public override DbCommand CreateCommand() => new MySqlCommand();
        public override DbParameter CreateParameter() => new MySqlParameter();
        public override DbDataAdapter CreateDataAdapter() => new MySqlDataAdapter();
    }
}
