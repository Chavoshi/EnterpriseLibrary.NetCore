// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using System.ComponentModel;


namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle.Configuration
{
    /// <summary>
    /// Oracle-specific connection information.
    /// </summary>
    [ResourceDescription(typeof(DesignResources), "OracleConnectionDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "OracleConnectionDataDisplayName")]
    [NameProperty("Name", NamePropertyDisplayFormat = "Oracle Packages for {0}")]
    public class OracleConnectionData : NamedConfigurationElement
    {
        private const string packagesProperty = "packages";

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleConnectionData"/> class with default values.
        /// </summary>
        public OracleConnectionData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [Reference(typeof(ConnectionStringSettingsCollection), typeof(ConnectionStringSettings))]
        [ResourceDescription(typeof(DesignResources), "OracleConnectionDataNameDescription")]
        [ResourceDisplayName(typeof(DesignResources), "OracleConnectionDataNameDisplayName")]
        public override string Name
        {
            get{ return base.Name; }
            set{ base.Name = value; }
        }

        /// <summary>
        /// Gets a collection of <see cref="OraclePackageData"/> objects.
        /// </summary>
        /// <value>
        /// A collection of <see cref="OraclePackageData"/> objects.
        /// </value>
        [ConfigurationProperty(packagesProperty, IsRequired = true)]
        [ConfigurationCollection(typeof(OraclePackageData))]
        [ResourceDescription(typeof(DesignResources), "OracleConnectionDataPackagesDescription")]
        [ResourceDisplayName(typeof(DesignResources), "OracleConnectionDataPackagesDisplayName")]
        [Editor(CommonDesignTime.EditorTypes.CollectionEditor, CommonDesignTime.EditorTypes.FrameworkElement)]
        [EnvironmentalOverrides(false)]
        [DesignTimeReadOnly(false)]
        public NamedElementCollection<OraclePackageData> Packages
        {
            get
            {
                return (NamedElementCollection<OraclePackageData>)base[packagesProperty];
            }
        }
    }
}
