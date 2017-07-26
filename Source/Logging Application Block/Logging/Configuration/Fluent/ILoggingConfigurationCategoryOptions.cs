﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Diagnostics;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{
    /// <summary>
    /// Fluent interface to further configure a logging category source.
    /// </summary>
    public interface ILoggingConfigurationCategoryOptions : ILoggingConfigurationCategoryContd
    {
        /// <summary>
        /// Specifed the default <see cref="SourceLevels"/> for this Category.<br/>
        /// By default the source level is set to <see cref="SourceLevels.All"/>.
        /// </summary>
        /// <param name="sourceLevels">The <see cref="SourceLevels"/> to be set as default.</param>
        /// <returns>Fluent interface that allows for this Category Source to be configured further.</returns>
        ILoggingConfigurationCategoryOptions ToSourceLevels(SourceLevels sourceLevels);

        /// <summary>
        /// Specifies that Flush doesnt have to be called after every write to a listener.<br/>
        /// By default a Flush will be called after every write to a listener.
        /// </summary>
        /// <returns>Fluent interface that allows for this Category Source to be configured further.</returns>
        ILoggingConfigurationCategoryOptions DoNotAutoFlushEntries();
    }
}
