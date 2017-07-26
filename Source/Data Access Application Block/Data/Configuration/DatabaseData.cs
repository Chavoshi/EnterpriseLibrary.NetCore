﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Configuration
{
    /// <summary>
    /// Describes a <see cref="Database"/> instance, aggregating information from a <see cref="ConnectionStringSettings"/>
    /// and potentially other sources of configuration.
    /// </summary>
    public abstract class DatabaseData
    {
        ///<summary>
        /// Initializes a new instance of the <see cref="DatabaseData"/> class with a connection string and a configuration
        /// source.
        ///</summary>
        ///<param name="connectionStringSettings">The <see cref="ConnectionStringSettings"/> for the represented database.</param>
        ///<param name="configurationSource">The <see cref="IConfigurationSource"/> from which additional information can 
        /// be retrieved if necessary.</param>
        protected DatabaseData(ConnectionStringSettings connectionStringSettings, Func<string, ConfigurationSection> configurationSource)
        {
            ConnectionStringSettings = connectionStringSettings;
            ConfigurationSource = configurationSource;
        }

        /// <summary>
        /// Gets the <see cref="ConnectionStringSettings"/> for the database data.
        /// </summary>
        protected ConnectionStringSettings ConnectionStringSettings { get; private set; }

        ///<summary>
        /// Gets the function to access configuration information.
        ///</summary>
        protected Func<string, ConfigurationSection> ConfigurationSource { get; private set; }

        /// <summary>
        /// Gets the name for the represented database.
        /// </summary>
        public string Name
        {
            get { return ConnectionStringSettings.Name; }
        }

        /// <summary>
        /// Gets the connection string for the represented database.
        /// </summary>
        public string ConnectionString
        {
            get { return ConnectionStringSettings.ConnectionString; }
        }

        /// <summary>
        /// Builds the <see cref="Database"/> represented by this configuration object.
        /// </summary>
        /// <returns>A database.</returns>
        public abstract Database BuildDatabase();
    }
}
