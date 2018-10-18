using Npgsql;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Fluent {

    /// <summary>
    /// PostgreSql Server Database configuration options.
    /// </summary>
    public interface IDatabaseNpgsqlDatabaseConfiguration : IDatabaseDefaultConnectionString, IDatabaseConfigurationProperties {
        /// <summary>
        /// Define a connection string using the <see cref="NpgsqlConnectionStringBuilder"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        IDatabaseConfigurationProperties WithConnectionString(NpgsqlConnectionStringBuilder builder);
    }

    internal class NpgsqlDatabaseConfigurationExtension : DatabaseConfigurationExtension, IDatabaseNpgsqlDatabaseConfiguration {

        public NpgsqlDatabaseConfigurationExtension(IDatabaseConfigurationProviders context)
            : base(context) {
            base.ConnectionString.ProviderName = DbProviderMapping.DefaultNpgsqlProviderName;
        }

        public IDatabaseConfigurationProperties WithConnectionString(NpgsqlConnectionStringBuilder builder) {
            return base.WithConnectionString(builder);
        }
    }
}
