﻿// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration.Fluent
{
    /// <summary>
    /// Oracle configuration options
    /// </summary>
    public interface IDatabaseOracleConfiguration : IDatabaseConfigurationProperties
    {
        /// <summary>
        /// Define an Oracle connection with a connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        IDatabaseOracleConfiguration WithConnectionString(string connectionString);

        /// <summary>
        /// Define an Oracle connection with the <see cref="OracleConnectionStringBuilder"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
#pragma warning disable 612, 618
        IDatabaseOracleConfiguration WithConnectionString(OracleConnectionStringBuilder builder);
#pragma warning restore 612, 618

        /// <summary>
        /// Define an Oracle package with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IDatabaseOraclePackageConfiguration WithPackageNamed(string name);

    }

    /// <summary>
    /// Oracle package configuration options.
    /// </summary>
    public interface IDatabaseOraclePackageConfiguration : IFluentInterface
    {
        /// <summary>
        /// Define the prefix for the Oracle package.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        IDatabaseConfigurationProperties AndPrefix(string prefix);
    }


    internal class OracleConfigurationExtension : DatabaseConfigurationExtension,
                                                  IDatabaseOraclePackageConfiguration,
                                                  IDatabaseOracleConfiguration
    {
        private OracleConnectionSettings currentOracleSettings;
        private OraclePackageData currentOraclePackageData;
        private OracleConnectionData currentOracleConnectionData;

        public OracleConfigurationExtension(IDatabaseConfigurationProviders context) : base(context)
        {
            ConnectionString.ProviderName = DbProviderMapping.DefaultOracleProviderName;
        }

        IDatabaseOracleConfiguration IDatabaseOracleConfiguration.WithConnectionString(string connectionString)
        {
            base.WithConnectionString(connectionString);
            return this;
        }

#pragma warning disable 612, 618
        IDatabaseOracleConfiguration IDatabaseOracleConfiguration.WithConnectionString(OracleConnectionStringBuilder builder)
        {
            base.WithConnectionString(builder);
            return this;
        }
#pragma warning restore 612, 618

        /// <summary />
        IDatabaseConfigurationProperties IDatabaseOraclePackageConfiguration.AndPrefix(string prefix)
        {
            if (String.IsNullOrEmpty(prefix))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "prefix");

            currentOraclePackageData.Prefix = prefix;
            return this;
        }

        /// <summary />
        public IDatabaseOraclePackageConfiguration WithPackageNamed(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException(Resources.ExceptionStringNullOrEmpty, "name");


            EnsureOracleSettings();
            EnsureOracleConnectionData();

            currentOraclePackageData = new OraclePackageData() { Name = name };
            currentOracleConnectionData.Packages.Add(currentOraclePackageData);

            return this;
        }

        private void EnsureOracleSettings()
        {
            currentOracleSettings = Builder.Get<OracleConnectionSettings>(OracleConnectionSettings.SectionName);
            if (currentOracleSettings != null) return;
            currentOracleSettings = new OracleConnectionSettings();
            Builder.AddSection(OracleConnectionSettings.SectionName, currentOracleSettings);
        }

        private void EnsureOracleConnectionData()
        {
            if (currentOracleConnectionData != null) return;
            currentOracleConnectionData = new OracleConnectionData() { Name = ConnectionString.Name };
            currentOracleSettings.OracleConnectionsData.Add(currentOracleConnectionData);
        }
    }
}
