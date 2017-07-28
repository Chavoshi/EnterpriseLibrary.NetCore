﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Cryptography Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Instrumentation;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Configuration
{
    /// <summary>
    /// <para>Configuration data for the <c>SymmetricAlgorithm</c> provider.</para>
    /// </summary>	
    [TypePickingCommand("AlgorithmTypeName", Replace = CommandReplacement.DefaultAddCommandReplacement, CommandModelTypeName = CryptographyDesignTime.CommandTypeNames.AddSymmetricAlgorithmProviderCommand)]
    [ResourceDescription(typeof(DesignResources), "SymmetricAlgorithmProviderDataDescription")]
    [ResourceDisplayName(typeof(DesignResources), "SymmetricAlgorithmProviderDataDisplayName")]
    [ViewModel(CryptographyDesignTime.ViewModelTypeNames.SymmetricAlgorithmProviderDataViewModel)]
    [Command(CryptographyDesignTime.CommandTypeNames.ExportKeyCommand, TitleResourceType=typeof(DesignResources), TitleResourceName="ExportCyrptographyKeyCommandTitle")]
    public class SymmetricAlgorithmProviderData : SymmetricProviderData
    {
        private AssemblyQualifiedTypeNameConverter typeConverter = new AssemblyQualifiedTypeNameConverter();

        private const string algorithmTypeProperty = "algorithmType";
        private const string protectedKeyFilename = "protectedKeyFilename";
        private const string protectedKeyProtectionScope = "protectedKeyProtectionScope";

        /// <summary>
        /// <para>Initializes a new instance of <see cref="SymmetricAlgorithmProviderData"/> class.</para>
        /// </summary>
        public SymmetricAlgorithmProviderData()
            : base(typeof(SymmetricAlgorithmProvider))
        {
        }

        /// <summary>
        /// <para>Initializes a new instance of <see cref="SymmetricAlgorithmProviderData"/> class.</para>
        /// </summary>
        /// <param name="name"><para>The name for the provider.</para></param>
        /// <param name="algorithmType"><para>The type name of the hash algorithm.</para></param>
        /// <param name="protectedKeyFilename">File name where key is stored</param>
        /// <param name="protectedKeyProtectionScope">DPAPI protection scope used to store key</param>
        public SymmetricAlgorithmProviderData(string name, Type algorithmType, string protectedKeyFilename, DataProtectionScope protectedKeyProtectionScope)
            : base(name, typeof(SymmetricAlgorithmProvider))
        {
            AlgorithmType = algorithmType;
            ProtectedKeyProtectionScope = protectedKeyProtectionScope;
            ProtectedKeyFilename = protectedKeyFilename;
        }

        /// <summary>
        /// <para>Gets or sets the type of <see cref="System.Security.Cryptography.SymmetricAlgorithm"/>.</para>
        /// </summary>
        /// <value>
        /// <para>The type of <see cref="System.Security.Cryptography.SymmetricAlgorithm"/>.</para>
        /// </value>
        public Type AlgorithmType
        {
            get { return (Type)typeConverter.ConvertFrom(AlgorithmTypeName); }
            set { AlgorithmTypeName = typeConverter.ConvertToString(value); }
        }

        /// <summary>
        /// Gets or sets the fully qualified name of the type of <see cref="System.Security.Cryptography.SymmetricAlgorithm"/>.
        /// </summary>
        /// <value>
        /// The fully qualified name of the type of <see cref="System.Security.Cryptography.SymmetricAlgorithm"/>.
        /// </value>
        [DesignTimeReadOnly(true)]
        [ConfigurationProperty(algorithmTypeProperty)]
        [System.ComponentModel.Editor(CommonDesignTime.EditorTypes.TypeSelector, CommonDesignTime.EditorTypes.UITypeEditor)]
        [BaseType(typeof(SymmetricAlgorithm))]
        [ResourceDescription(typeof(DesignResources), "SymmetricAlgorithmProviderDataAlgorithmTypeNameDescription")]
        [ResourceDisplayName(typeof(DesignResources), "SymmetricAlgorithmProviderDataAlgorithmTypeNameDisplayName")]
        public string AlgorithmTypeName
        {
            get { return (string)this[algorithmTypeProperty]; }
            set { this[algorithmTypeProperty] = value; }
        }
        /// <summary>
        /// 
        /// </summary>	
        [ConfigurationProperty(protectedKeyFilename)]
        [ResourceDescription(typeof(DesignResources), "SymmetricAlgorithmProviderDataProtectedKeyFilenameDescription")]
        [ResourceDisplayName(typeof(DesignResources), "SymmetricAlgorithmProviderDataProtectedKeyFilenameDisplayName")]
        [System.ComponentModel.Browsable(false)]
        public string ProtectedKeyFilename
        {
            get { return (string)this[protectedKeyFilename]; }
            set { this[protectedKeyFilename] = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [ConfigurationProperty(protectedKeyProtectionScope)]
        [ResourceDescription(typeof(DesignResources), "SymmetricAlgorithmProviderDataProtectedKeyProtectionScopeDescription")]
        [ResourceDisplayName(typeof(DesignResources), "SymmetricAlgorithmProviderDataProtectedKeyProtectionScopeDisplayName")]
        [System.ComponentModel.Browsable(false)]
        public DataProtectionScope ProtectedKeyProtectionScope
        {
            get { return (DataProtectionScope)this[protectedKeyProtectionScope]; }
            set { this[protectedKeyProtectionScope] = value; }
        }

        /// <summary>
        /// Creates a <see cref="TypeRegistration"/> instance describing the provider represented by 
        /// this configuration object.
        /// </summary>
        /// <returns>A <see cref="TypeRegistration"/> instance describing a provider.</returns>
        /// <param name="configurationSource">TODO</param>
        public override IEnumerable<TypeRegistration> GetRegistrations(IConfigurationSource configurationSource)
        {
            yield return base.GetInstrumentationProviderRegistration(configurationSource);

            yield return
                new TypeRegistration<ISymmetricCryptoProvider>(
                    () =>
                        new SymmetricAlgorithmProvider(
                            AlgorithmType,
                            ProtectedKeyFilename,
                            ProtectedKeyProtectionScope,
                            Container.Resolved<ISymmetricAlgorithmInstrumentationProvider>(Name)))
                {
                    Name = Name,
                    Lifetime = TypeRegistrationLifetime.Transient
                };
        }
    }
}
