using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Data;
using System.Data.Common;

namespace EnterpriseLibrary.Test.MySql
{
    class Program
    {
        static void Main(string[] args)
        {
            TestOracle();
            TestMySql();
        }

        private static void TestOracle()
        {
            const string sqlCmd = @"INSERT INTO CITY
                                    (
                                        CITYID,
	                                    NAME
                                    )
                                    VALUES
                                    (
                                        :CityID,
	                                    :Name
                                    )";

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("BizDbOracle");

            using (DbCommand cmd = db.GetSqlStringCommand(sqlCmd))
            {
                try
                {
                    db.AddInParameter(cmd, "CITYID", DbType.Int32, 2);
                    db.AddInParameter(cmd, "NAME", DbType.String, "London");
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        private static void TestMySql()
        {
            const string sqlCmd = @"INSERT INTO City
                                    (
                                        CityID,
	                                    Name
                                    )
                                    VALUES
                                    (
                                        @CityID,
	                                    @Name
                                    )";

            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory(new SystemConfigurationSource(false).GetSection), false);
            Database db = DatabaseFactory.CreateDatabase("BizDbMySql");

            using (DbCommand cmd = db.GetSqlStringCommand(sqlCmd))
            {
                try
                {
                    db.AddInParameter(cmd, "@CityID", DbType.Int32, 2);
                    db.AddInParameter(cmd, "@Name", DbType.String, "London");
                    db.ExecuteNonQuery(cmd);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
