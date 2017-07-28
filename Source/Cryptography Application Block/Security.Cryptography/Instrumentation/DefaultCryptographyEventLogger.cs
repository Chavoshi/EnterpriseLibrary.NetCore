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
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Properties;
using System.Globalization;

namespace Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.Instrumentation
{
    /// <summary>
    /// The instrumentation gateway when no instances of the objects from the block are involved.
    /// </summary>
    [EventLogDefinition("Application", EventLogSourceName)]
    public class DefaultCryptographyEventLogger : InstrumentationListener, IDefaultCryptographyInstrumentationProvider
    {
        private readonly IEventLogEntryFormatter eventLogEntryFormatter;

        /// For testing purposes
        public const string EventLogSourceName = "Enterprise Library Cryptography";

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCryptographyEventLogger"/> class, specifying whether 
        /// logging to the event log is allowed.
        /// </summary>
        /// <param name="eventLoggingEnabled"><b>true</b> if writing to the event log is allowed, <b>false</b> otherwise.</param>
        public DefaultCryptographyEventLogger(bool eventLoggingEnabled)
            : this(false, eventLoggingEnabled, null)
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCryptographyEventLogger"/> class, specifying whether 
        /// logging to the event log is allowed.
        /// </summary>
        /// <param name="performanceCountersEnabled"><code>true</code> if updating performance counters is allowed, <code>false</code> otherwise.</param>
        /// <param name="eventLoggingEnabled"><code>true</code> if writing to the event log is allowed, <code>false</code> otherwise.</param>
        /// <param name="applicationInstanceName">The application name to use with performance counters.</param>
        public DefaultCryptographyEventLogger(bool performanceCountersEnabled,
            bool eventLoggingEnabled,
            string applicationInstanceName)
            : base(performanceCountersEnabled, eventLoggingEnabled, new AppDomainNameFormatter(applicationInstanceName))
        {
            eventLogEntryFormatter = new EventLogEntryFormatter(Resources.BlockName);
        }


        /// <summary>
        /// Logs the occurrence of a configuration error for the Enterprise Library Cryptography Application Block through the 
        /// available instrumentation mechanisms.
        /// </summary>
        /// <param name="instanceName">Name of the cryptographic provider instance in which the configuration error was detected.</param>
        /// <param name="messageTemplate">The template to build the event log entry.</param>
        /// <param name="exception">The exception raised for the configuration error.</param>
        public void LogConfigurationError(string instanceName, string messageTemplate, Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");

            //ToDo: Not supported in .Net Core
            //if (EventLoggingEnabled)
            //{
            //    string errorMessage
            //        = string.Format(
            //            CultureInfo.CurrentCulture,
            //            messageTemplate,
            //            instanceName);
            //    string entryText = eventLogEntryFormatter.GetEntryText(errorMessage, exception);

            //    EventLog.WriteEntry(GetEventSourceName(), entryText, EventLogEntryType.Error);
            //}
        }

        /// <summary>
        /// Logs the occurrence of a configuration error for the Enterprise Library Cryptography Application Block through the 
        /// available instrumentation mechanisms.
        /// </summary>
        /// <param name="instanceName">Name of the cryptographic provider instance in which the configuration error was detected.</param>
        /// <param name="messageTemplate">The template to build the event log entry.</param>
        public void LogConfigurationError(string instanceName, string messageTemplate)
        {
            //ToDo: Not supported in .Net Core
            //if (EventLoggingEnabled)
            //{
            //    string errorMessage
            //        = string.Format(
            //            CultureInfo.CurrentCulture,
            //            messageTemplate,
            //            instanceName);
            //    string entryText = eventLogEntryFormatter.GetEntryText(errorMessage);

            //    EventLog.WriteEntry(GetEventSourceName(), entryText, EventLogEntryType.Error);
            //}
        }

        /// <summary>
        /// Fires the CryptographyErrorOccurred event.
        /// </summary>
        /// <param name="providerName">The name of the provider with the errror.</param>
        /// <param name="instanceName">The name of the instance with the errror.</param>
        /// <param name="message">The message that describes the failure.</param>
        public void FireCryptographyErrorOccurred(string providerName, string instanceName, string message)
        {
            LogConfigurationError(instanceName, message);
        }
    }
}
