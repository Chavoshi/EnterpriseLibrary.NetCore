// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Data.Odbc;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Fluent
{
    /// <summary>   
    /// Odbc database configuration options. 
    /// </summary>
    public interface IOdbcDatabaseConfiguration : IDatabaseDefaultConnectionString, IDatabaseConfigurationProperties
    {
        /// <summary>
        /// Define a connection string with the <see cref="OdbcConnectionStringBuilder"/>.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        IDatabaseConfigurationProperties WithConnectionString(OdbcConnectionStringBuilder builder);
    }


    internal class OdbcConfigurationExtension : DatabaseConfigurationExtension, IOdbcDatabaseConfiguration
    {
        public OdbcConfigurationExtension(IDatabaseConfigurationProviders context) : base(context)
        {
            base.ConnectionString.ProviderName = "System.Data.Odbc";
        }

        public IDatabaseConfigurationProperties WithConnectionString(OdbcConnectionStringBuilder builder)
        {
            return base.WithConnectionString(builder);
        }
    }
}
