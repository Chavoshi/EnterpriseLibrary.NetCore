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


namespace Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent
{

    /// <summary>
    /// Fluent interface that allows tracelisteners to be configured.
    /// </summary>
    public interface ILoggingConfigurationSendTo : IFluentInterface
    {
        /// <summary>
        /// Creates a reference to an existing Trace Listener with a specific name.
        /// </summary>
        /// <param name="listenerName">The name of the Trace Listener a reference should be made for.</param>
        ILoggingConfigurationCategoryContd SharedListenerNamed(string listenerName);

    }

}
