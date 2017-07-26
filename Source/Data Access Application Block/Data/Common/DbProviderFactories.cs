using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace System.Data.Common
{
    internal static class DbProviderFactories
    {
        private const string AssemblyQualifiedName = "AssemblyQualifiedName";
        private const string Instance = "Instance";
        private const string InvariantName = "InvariantName";
        private const string Name = "Name";
        private const string Description = "Description";

        private static ConnectionState _initState; // closed, connecting, open
        private static DataTable _providerTable;
        private static object _lockobj = new object();

        internal static DbProviderFactory GetFactory(string providerInvariantName)
        {
            DbProviderFactory factory = null;

            switch (providerInvariantName)
            {
                case DbProviderFactoriesConfigurationHandler.sqlclientProviderName:
                    factory = SqlClientFactory.Instance;
                    break;
                default:
                    break;
            }

            return factory;
        }

        internal static DataTable GetFactoryClasses()
        {
            // NOTES: Include the Framework Providers and any other Providers listed in the config file.
            DataTable dataTable = GetProviderTable();

            if (null != dataTable)
            {
                dataTable = dataTable.Copy();
            }
            else
            {
                dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();
            }
            return dataTable;
        }

        static private DataTable GetProviderTable()
        {
            DataTable dataTable = DbProviderFactoriesConfigurationHandler.CreateProviderDataTable();

            // NOTES: Adding the following Framework DbProviderFactories
            //  <add name="Odbc Data Provider" invariant="System.Data.Odbc" description=".Net Framework Data Provider for Odbc" type="System.Data.Odbc.OdbcFactory, System.Data, Version=%ASSEMBLY_VERSION%, Culture=neutral, PublicKeyToken=%ECMA_PUBLICKEY%"/>
            //  <add name="OleDb Data Provider" invariant="System.Data.OleDb" description=".Net Framework Data Provider for OleDb" type="System.Data.OleDb.OleDbFactory, System.Data, Version=%ASSEMBLY_VERSION%, Culture=neutral, PublicKeyToken=%ECMA_PUBLICKEY%"/>
            //  <add name="OracleClient Data Provider" invariant="System.Data.OracleClient" description=".Net Framework Data Provider for Oracle" type="System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient, Version=%ASSEMBLY_VERSION%, Culture=neutral, PublicKeyToken=%ECMA_PUBLICKEY%"/>
            //  <add name="SqlClient Data Provider" invariant="System.Data.SqlClient" description=".Net Framework Data Provider for SqlServer" type="System.Data.SqlClient.SqlClientFactory, System.Data, Version=%ASSEMBLY_VERSION%, Culture=neutral, PublicKeyToken=%ECMA_PUBLICKEY%"/>
            Type sysDataType = typeof(System.Data.SqlClient.SqlClientFactory);
            string asmQualName = sysDataType.AssemblyQualifiedName.ToString().Replace(DbProviderFactoriesConfigurationHandler.sqlclientPartialAssemblyQualifiedName, DbProviderFactoriesConfigurationHandler.oracleclientPartialAssemblyQualifiedName);
            DbProviderFactoryConfigSection[] dbFactoriesConfigSection = new DbProviderFactoryConfigSection[(int)DbProvidersIndex.DbProvidersIndexCount];
            //dbFactoriesConfigSection[(int)DbProvidersIndex.Odbc] = new DbProviderFactoryConfigSection(typeof(System.Data.Odbc.OdbcFactory), DbProviderFactoriesConfigurationHandler.odbcProviderName, DbProviderFactoriesConfigurationHandler.odbcProviderDescription);
            //dbFactoriesConfigSection[(int)DbProvidersIndex.OleDb] = new DbProviderFactoryConfigSection(typeof(System.Data.OleDb.OleDbFactory), DbProviderFactoriesConfigurationHandler.oledbProviderName, DbProviderFactoriesConfigurationHandler.oledbProviderDescription);
            //dbFactoriesConfigSection[(int)DbProvidersIndex.OracleClient] = new DbProviderFactoryConfigSection(DbProviderFactoriesConfigurationHandler.oracleclientProviderName, DbProviderFactoriesConfigurationHandler.oracleclientProviderNamespace, DbProviderFactoriesConfigurationHandler.oracleclientProviderDescription, asmQualName);
            dbFactoriesConfigSection[(int)DbProvidersIndex.SqlClient] = new DbProviderFactoryConfigSection(typeof(System.Data.SqlClient.SqlClientFactory), DbProviderFactoriesConfigurationHandler.sqlclientProviderName, DbProviderFactoriesConfigurationHandler.sqlclientProviderDescription);

            for (int i = 0; i < dbFactoriesConfigSection.Length; i++)
            {
                if (dbFactoriesConfigSection[i].IsNull() == false)
                {
                    bool flagIncludeToTable = false;

                    if (i == ((int)DbProvidersIndex.OracleClient)) // OracleClient Provider: Include only if it installed
                    {
                        Type providerType = Type.GetType(dbFactoriesConfigSection[i].AssemblyQualifiedName);
                        if (providerType != null)
                        {
                            // NOTES: Try and create a instance; If it fails, it will throw a System.NullReferenceException exception;
                            System.Reflection.FieldInfo providerInstance = providerType.GetField(Instance, System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                            if ((null != providerInstance) && (providerInstance.FieldType.IsSubclassOf(typeof(DbProviderFactory))))
                            {
                                Debug.Assert(providerInstance.IsPublic, "field not public");
                                Debug.Assert(providerInstance.IsStatic, "field not static");

                                object factory = providerInstance.GetValue(null);
                                if (null != factory)
                                {
                                    flagIncludeToTable = true;
                                } // Else ignore and don't add to table
                            } // Else ignore and don't add to table
                        }
                    }
                    else
                    {
                        flagIncludeToTable = true;
                    }

                    if (flagIncludeToTable == true)
                    {
                        DataRow row = dataTable.NewRow();
                        row[Name] = dbFactoriesConfigSection[i].Name;
                        row[InvariantName] = dbFactoriesConfigSection[i].InvariantName;
                        row[Description] = dbFactoriesConfigSection[i].Description;
                        row[AssemblyQualifiedName] = dbFactoriesConfigSection[i].AssemblyQualifiedName;
                        dataTable.Rows.Add(row);
                    } // Else Ignore and do not include to table;
                }
            }

            return dataTable;
        }
    }

    internal enum DbProvidersIndex : int
    {
        Odbc = 0,
        OleDb,
        OracleClient,
        SqlClient,
        DbProvidersIndexCount // As enums are 0-based index, the DbProvidersIndexCount will hold the maximum count of the enum objects;
    }

    public class DbProviderFactoriesConfigurationHandler //: IConfigurationSectionHandler
    { // V1.2.3300
        internal const string sectionName = "system.data";
        internal const string providerGroup = "DbProviderFactories";

        // NOTES: Framework-Based DbProviderFactories Details
        internal const string odbcProviderName = "Odbc Data Provider";
        internal const string odbcProviderDescription = ".Net Framework Data Provider for Odbc";

        internal const string oledbProviderName = "OleDb Data Provider";
        internal const string oledbProviderDescription = ".Net Framework Data Provider for OleDb";

        internal const string oracleclientProviderName = "OracleClient Data Provider";
        internal const string oracleclientProviderNamespace = "System.Data.OracleClient";
        internal const string oracleclientProviderDescription = ".Net Framework Data Provider for Oracle";

        internal const string sqlclientProviderName = "SqlClient Data Provider";
        internal const string sqlclientProviderDescription = ".Net Framework Data Provider for SqlServer";

        internal const string sqlclientPartialAssemblyQualifiedName = "System.Data.SqlClient.SqlClientFactory, System.Data,";
        internal const string oracleclientPartialAssemblyQualifiedName = "System.Data.OracleClient.OracleClientFactory, System.Data.OracleClient,";

        public DbProviderFactoriesConfigurationHandler()
        { // V1.2.3300
        }

        internal static DataTable CreateProviderDataTable()
        {
            DataColumn frme = new DataColumn("Name", typeof(string));
            frme.ReadOnly = true;
            DataColumn desc = new DataColumn("Description", typeof(string));
            desc.ReadOnly = true;
            DataColumn invr = new DataColumn("InvariantName", typeof(string));
            invr.ReadOnly = true;
            DataColumn qual = new DataColumn("AssemblyQualifiedName", typeof(string));
            qual.ReadOnly = true;

            DataColumn[] primaryKey = new DataColumn[] { invr };
            DataColumn[] columns = new DataColumn[] { frme, desc, invr, qual };

            DataTable table = new DataTable(DbProviderFactoriesConfigurationHandler.providerGroup);

            table.Columns.AddRange(columns);
            table.PrimaryKey = primaryKey;
            return table;
        }
    }

    internal class DbProviderFactoryConfigSection
    {
        Type factType;
        string name;
        string invariantName;
        string description;
        string assemblyQualifiedName;

        public DbProviderFactoryConfigSection(Type FactoryType, string FactoryName, string FactoryDescription)
        {
            try
            {
                factType = FactoryType;
                name = FactoryName;
                invariantName = factType.Namespace.ToString();
                description = FactoryDescription;
                assemblyQualifiedName = factType.AssemblyQualifiedName.ToString();
            }
            catch
            {
                factType = null;
                name = string.Empty;
                invariantName = string.Empty;
                description = string.Empty;
                assemblyQualifiedName = string.Empty;
            }
        }

        public DbProviderFactoryConfigSection(string FactoryName, string FactoryInvariantName, string FactoryDescription, string FactoryAssemblyQualifiedName)
        {
            factType = null;
            name = FactoryName;
            invariantName = FactoryInvariantName;
            description = FactoryDescription;
            assemblyQualifiedName = FactoryAssemblyQualifiedName;
        }

        public bool IsNull()
        {
            if ((factType == null) && (invariantName == string.Empty))
                return true;
            else
                return false;
        }

        public string Name
        {
            get { return name; }
        }

        public string InvariantName
        {
            get { return invariantName; }
        }

        public string Description
        {
            get { return description; }
        }

        public string AssemblyQualifiedName
        {
            get { return assemblyQualifiedName; }
        }
    }
}
