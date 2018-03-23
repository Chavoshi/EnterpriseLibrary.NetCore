// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration
{
    /// <summary>
    /// Describes a <see cref="OracleDatabase"/> instance, aggregating information from a 
    /// <see cref="ConnectionStringSettings"/> and any Oracle-specific database information.
    /// </summary>
    public class OracleDatabaseData : DatabaseData
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="OracleDatabaseData"/> class with a connection string and a configuration
        /// source.
        ///</summary>
        ///<param name="connectionStringSettings">The <see cref="ConnectionStringSettings"/> for the represented database.</param>
        ///<param name="configurationSource">The <see cref="IConfigurationSource"/> from which Oracle-specific information 
        /// should be retrieved.</param>
        public OracleDatabaseData(ConnectionStringSettings connectionStringSettings, Func<string, ConfigurationSection> configurationSource)
            : base(connectionStringSettings, configurationSource)
        {
            var settings = (OracleConnectionSettings)
                           configurationSource(OracleConnectionSettings.SectionName);

            if (settings != null)
            {
                ConnectionData = settings.OracleConnectionsData.Get(connectionStringSettings.Name);
            }
        }

        ///<summary>
        /// Gets the Oracle package mappings for the represented database.
        ///</summary>
        public IEnumerable<OraclePackageData> PackageMappings
        {
            get
            {
                return ConnectionData != null
                           ? (IEnumerable<OraclePackageData>)ConnectionData.Packages
                           : new OraclePackageData[0];
            }
        }

        private OracleConnectionData ConnectionData { get; set; }

        /// <summary>
        /// Builds the <see cref="Database" /> represented by this configuration object.
        /// </summary>
        /// <returns>
        /// A database.
        /// </returns>
        public override Database BuildDatabase()
        {
#pragma warning disable 612, 618
            return new OracleDatabase(this.ConnectionString, this.PackageMappings.Cast<IOraclePackage>().ToArray());
#pragma warning restore 612, 618
        }
    }
}
