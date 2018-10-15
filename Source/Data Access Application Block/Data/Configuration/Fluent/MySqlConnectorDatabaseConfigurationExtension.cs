using MySql.Data.MySqlClient;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Fluent {

    /// <summary>
    /// MySql Server Database configuration options.
    /// </summary>
    public interface IDatabaseMySqlConnectorDatabaseConfiguration : IDatabaseDefaultConnectionString, IDatabaseConfigurationProperties {
        /// <summary>
        /// Define a connection string using the <see cref="MySqlConnectionStringBuilder"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        IDatabaseConfigurationProperties WithConnectionString(MySqlConnectionStringBuilder builder);
    }

    internal class MySqlConnectorDatabaseConfigurationExtension : DatabaseConfigurationExtension, IDatabaseMySqlConnectorDatabaseConfiguration {

        public MySqlConnectorDatabaseConfigurationExtension(IDatabaseConfigurationProviders context)
            : base(context) {
            base.ConnectionString.ProviderName = DbProviderMapping.DefaultMySqlConnectorProviderName;
        }

        public IDatabaseConfigurationProperties WithConnectionString(MySqlConnectionStringBuilder builder) {
            return base.WithConnectionString(builder);
        }
    }
}
