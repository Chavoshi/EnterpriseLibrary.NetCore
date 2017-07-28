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
    /// Defines the logical events that can be instrumented for hash providers.
    /// </summary>
    [HasInstallableResourcesAttribute]
    //[PerformanceCountersDefinition(counterCategoryName, "CryptographyHelpResourceName")]
    [EventLogDefinition("Application", "Enterprise Library Cryptography")]
    public class HashAlgorithmInstrumentationProvider : InstrumentationListener, IHashAlgorithmInstrumentationProvider
    {
        static EnterpriseLibraryPerformanceCounterFactory factory = new EnterpriseLibraryPerformanceCounterFactory();


        /// <summary>
        /// Made public for testing
        /// </summary>
        public const string TotalHashOperationPerformedPerformanceCounterName = "Total Hash Operations";

        /// <summary>
        /// Made public for testing
        /// </summary>
        public const string TotalHashComparisonPerformedPerformanceCounterName = "Total Hash Comparisons";

        /// <summary>
        /// Made public for testing
        /// </summary>
        public const string TotalHashMismatchesPerformedPerformanceCounterName = "Total Hash Mismatches";

        //Not supproted in .Net Core
        //[PerformanceCounter("Hash Operations/sec", "HashOperationPerformedHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        //EnterpriseLibraryPerformanceCounter hashOperationPerformedCounter;

        //[PerformanceCounter(TotalHashOperationPerformedPerformanceCounterName, "TotalHashOperationPerformedHelpResource", PerformanceCounterType.NumberOfItems32)]
        //EnterpriseLibraryPerformanceCounter totalHashOperationPerformedPerformanceCounter;

        //[PerformanceCounter("Hash Comparisons/sec", "HashComparisonPerformedHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        //EnterpriseLibraryPerformanceCounter hashComparisonPerformedCounter;

        //[PerformanceCounter(TotalHashComparisonPerformedPerformanceCounterName, "TotalHashComparisonPerformedHelpResource", PerformanceCounterType.NumberOfItems32)]
        //EnterpriseLibraryPerformanceCounter totalHashComparisonPerformedPerformanceCounter;

        //[PerformanceCounter("Hash Mismatches/sec", "HashMismatchDetectedHelpResource", PerformanceCounterType.RateOfCountsPerSecond32)]
        //EnterpriseLibraryPerformanceCounter hashMismatchDetectedCounter;

        //[PerformanceCounter(TotalHashMismatchesPerformedPerformanceCounterName, "TotalHashMismatchDetectedHelpResource", PerformanceCounterType.NumberOfItems32)]
        //EnterpriseLibraryPerformanceCounter totalHashMismatchesPerformedPerformanceCounter;

        private string instanceName;

        /// <summary>
        /// Made public for testing
        /// </summary>
        public const string counterCategoryName = "Enterprise Library Cryptography Counters";

        /// <summary>
        /// Initializes a new instance of the <see cref="HashAlgorithmInstrumentationProvider"/> class.
        /// </summary>
        /// <param name="instanceName">The name of the <see cref="IHashProvider"/> instance this instrumentation listener is created for.</param>
        /// <param name="performanceCountersEnabled"><b>true</b> if performance counters should be updated.</param>
        /// <param name="eventLoggingEnabled"><b>true</b> if event log entries should be written.</param>
        /// <param name="applicationInstanceName">The application instance name.</param>
        public HashAlgorithmInstrumentationProvider(string instanceName,
                                           bool performanceCountersEnabled,
                                           bool eventLoggingEnabled,
                                           string applicationInstanceName)
            : this(instanceName, performanceCountersEnabled, eventLoggingEnabled, new AppDomainNameFormatter(applicationInstanceName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HashAlgorithmInstrumentationProvider"/> class.
        /// </summary>
        /// <param name="instanceName">The name of the <see cref="IHashProvider"/> instance this instrumentation listener is created for.</param>
        /// <param name="performanceCountersEnabled"><b>true</b> if performance counters should be updated.</param>
        /// <param name="eventLoggingEnabled"><b>true</b> if event log entries should be written.</param>
        /// <param name="nameFormatter">The <see cref="IPerformanceCounterNameFormatter"/> that is used to creates unique name for each <see cref="PerformanceCounter"/> instance.</param>
        public HashAlgorithmInstrumentationProvider(string instanceName,
                                           bool performanceCountersEnabled,
                                           bool eventLoggingEnabled,
                                           IPerformanceCounterNameFormatter nameFormatter)
            : base(instanceName, performanceCountersEnabled, eventLoggingEnabled, nameFormatter)
        {
            this.instanceName = instanceName;
        }

        /// <summary>
        /// </summary>
        /// <param name="message">The message that describes the failure.</param>
        /// <param name="exception">The exception thrown during the failure.</param>
        public void FireCyptographicOperationFailed(string message, Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");

            //Not supproted in .Net Core
            //if (EventLoggingEnabled)
            //{
            //    string errorMessage
            //        = string.Format(
            //            CultureInfo.CurrentCulture,
            //            Resources.ErrorCryptographicOperationFailed,
            //            instanceName);
            //    string entryText = new EventLogEntryFormatter(Resources.BlockName).GetEntryText(errorMessage, exception, message);

            //    EventLog.WriteEntry(GetEventSourceName(), entryText, EventLogEntryType.Error);
            //}
        }

        /// <summary>
        /// </summary>
        public void FireHashOperationPerformed()
        {
            //Not supproted in .Net Core
            //if (PerformanceCountersEnabled)
            //{
            //    hashOperationPerformedCounter.Increment();
            //    totalHashOperationPerformedPerformanceCounter.Increment();
            //}
        }

        /// <summary>
        /// </summary>
        public void FireHashComparisonPerformed()
        {
            //Not supproted in .Net Core
            //if (PerformanceCountersEnabled)
            //{
            //    hashComparisonPerformedCounter.Increment();
            //    totalHashComparisonPerformedPerformanceCounter.Increment();
            //}
        }

        /// <summary>
        /// </summary>
        public void FireHashMismatchDetected()
        {
            //Not supproted in .Net Core
            //if (PerformanceCountersEnabled)
            //{
            //    hashMismatchDetectedCounter.Increment();
            //    totalHashMismatchesPerformedPerformanceCounter.Increment();
            //}
        }

        /// <summary>
        /// Creates the performance counters to instrument the hash provider events for the specified instance names.
        /// </summary>
        /// <param name="instanceNames">The instance names for the performance counters.</param>
        protected override void CreatePerformanceCounters(string[] instanceNames)
        {
            //Not supproted in .Net Core
            //hashOperationPerformedCounter
            //    = factory.CreateCounter(counterCategoryName, "Hash Operations/sec", instanceNames);
            //hashComparisonPerformedCounter
            //    = factory.CreateCounter(counterCategoryName, "Hash Comparisons/sec", instanceNames);
            //hashMismatchDetectedCounter
            //    = factory.CreateCounter(counterCategoryName, "Hash Mismatches/sec", instanceNames);
            //totalHashOperationPerformedPerformanceCounter
            //    = factory.CreateCounter(counterCategoryName, TotalHashOperationPerformedPerformanceCounterName, instanceNames);
            //totalHashComparisonPerformedPerformanceCounter
            //    = factory.CreateCounter(counterCategoryName, TotalHashComparisonPerformedPerformanceCounterName, instanceNames);
            //totalHashMismatchesPerformedPerformanceCounter
            //    = factory.CreateCounter(counterCategoryName, TotalHashMismatchesPerformedPerformanceCounterName, instanceNames);
        }
    }
}
