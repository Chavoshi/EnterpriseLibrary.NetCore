﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Caching Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Caching.Properties;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Caching.Instrumentation
{
    /// <summary>
    /// The instrumentation gateway when no instances of the objects from the block are involved.
    /// </summary>
    [EventLogDefinition("Application", EventLogSourceName)]
    public class DefaultCachingEventLogger : InstrumentationListener
    {
        /// <summary>
        /// The event log source name.
        /// </summary>
        public const string EventLogSourceName = CachingInstrumentationProvider.EventLogSourceName;

        readonly IEventLogEntryFormatter eventLogEntryFormatter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCachingEventLogger"/> class, specifying whether 
        /// logging to the event log is allowed.
        /// </summary>
        /// <param name="eventLoggingEnabled"><b>true</b> if writing to the event log is allowed, <b>false</b> otherwise.</param>
        public DefaultCachingEventLogger(bool eventLoggingEnabled)
            : base((string)null, false, eventLoggingEnabled, null)
        {
            eventLogEntryFormatter = new EventLogEntryFormatter(Resources.BlockName);
        }

        /// <summary>
        /// Logs the occurrence of a configuration error for the Enterprise Library Caching Application Block through the 
        /// available instrumentation mechanisms.
        /// </summary>
        /// <param name="instanceName">Name of the <see cref="CacheManager"/> instance in which the configuration error was detected.</param>
        /// <param name="exception">The exception raised for the configuration error.</param>
        public void LogConfigurationError(string instanceName, Exception exception)
        {
            if(exception == null) throw new ArgumentNullException("exception");

            //Not supproted in .Net Core
            //if (EventLoggingEnabled)
            //{
            //    string errorMessage
            //        = string.Format(
            //            CultureInfo.CurrentCulture,
            //            Resources.ErrorCacheConfigurationFailedMessage,
            //            instanceName);
            //    string entryText = eventLogEntryFormatter.GetEntryText(errorMessage, exception);

            //    EventLog.WriteEntry(GetEventSourceName(), entryText, EventLogEntryType.Error);
            //}
        }
    }
}
