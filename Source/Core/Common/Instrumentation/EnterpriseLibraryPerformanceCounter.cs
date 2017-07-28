﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Core
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    /// <summary>
	/// Provides a virtual PerformanceCounter interface that allows an application to maintain both individually 
	/// named counter instances and a single counter total instance.
    /// </summary>
    public class EnterpriseLibraryPerformanceCounter
    {
        //ToDo: Not supported in .Net Core
        //private PerformanceCounter [] counters;
        private string[] instanceNames;
        private string counterName;
        private string counterCategoryName;

        //ToDo: Not supported in .Net Core
        ///// <summary>
        ///// Gets the list of performance counter instances managed by this object.
        ///// </summary>
        //public PerformanceCounter [] Counters { get { return counters; }}

        /// <summary>
        /// Initializes a single performance counter instance named "Total"
        /// </summary>
        /// <param name="counterCategoryName">Performance counter category name, as defined during installation</param>
        /// <param name="counterName">Performance counter name, as defined during installation</param>
        public EnterpriseLibraryPerformanceCounter(string counterCategoryName, string counterName)
            : this(counterCategoryName, counterName, new string[] { "Total" })
        {
        }

        /// <summary>
		/// Initializes multiple instances of performance counters to be managed by this object. 
		/// </summary>
		/// <param name="counterCategoryName">Performance counter category name, as defined during installation</param>
		/// <param name="counterName">Performance counter name, as defined during installation</param>
		/// <param name="instanceNames">Param array of instance names to be managed</param>
        public EnterpriseLibraryPerformanceCounter(string counterCategoryName, string counterName, params string[] instanceNames)
        {
            if (instanceNames == null) throw new ArgumentNullException("instanceNames");

            this.instanceNames = instanceNames;
            this.counterName = counterName;
            this.counterCategoryName = counterCategoryName;

            //ToDo: Not supported in .Net Core
            //counters = new PerformanceCounter[instanceNames.Length];
            //for(int i = 0; i < counters.Length; i++)
            //{
            //	counters[i] = InstantiateCounter(instanceNames[i]);
            //}
        }

        //ToDo: Not supported in .Net Core
        ///// <summary>
        ///// Initializes this object with performance counters created externally. It is the responsibility of the external
        ///// counter factory to create an instance for the "Total" counter.
        ///// </summary>
        ///// <param name="counters">Param array of already initialized <see cref="PerformanceCounter"></see>s to be managed 
        ///// by this instance.</param>
        //public EnterpriseLibraryPerformanceCounter(params PerformanceCounter[] counters)
        //{
        //    this.counters = counters;
        //}

        /// <summary>
        /// Initializes this object with performance counters created externally. It is the responsibility of the external
        /// counter factory to create an instance for the "Total" counter.
        /// </summary>
        public EnterpriseLibraryPerformanceCounter()
        {
        }

        /// <summary>
        /// Clears the raw count associated with all managed performance counters
        /// </summary>
        public void Clear()
        {
            //ToDo: Not supported in .Net Core
            //foreach (PerformanceCounter counter in counters)
            //{
            //    counter.RawValue = 0;
            //}
        }

        /// <summary>
		/// This method supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
		/// </summary>
        public long Value
        {
            get
            {
                //ToDo: Not supported in .Net Core
                //return counters[0].RawValue;

                return -1;
            }
        }

        /// <summary>
        /// Increments each performance counter managed by this instance.
        /// </summary>
        public void Increment()
        {
            //ToDo: Not supported in .Net Core
            //foreach (PerformanceCounter counter in counters)
            //{
            //    counter.Increment();
            //}
        }

        /// <summary>
        /// Increments by the given <paramref name="value"></paramref> each performance counter managed by this instance.
        /// </summary>
        /// <param name="value">Amount by which to increment each counter</param>
        public void IncrementBy(long value)
        {
            //ToDo: Not supported in .Net Core
            //foreach (PerformanceCounter counter in counters)
            //{
            //    counter.IncrementBy(value);
            //}
        }

        /// <summary>
        /// Gets the current value of the given performance counter instance.
        /// </summary>
        /// <param name="instanceName">Instance name of counter for which to get value.</param>
        /// <returns>Value of the given performance counter.</returns>
        public long GetValueFor(string instanceName)
        {
            //ToDo: Not supported in .Net Core
            //foreach(PerformanceCounter counter in counters)
            //{
            //	if(counter.InstanceName.Equals(instanceName)) return counter.RawValue;
            //}

            return -1;
        }

        /// <summary>
        /// Sets the value of the given performance counter instance.
        /// </summary>
        /// <param name="instanceName">Instance name of counter for which to set the value.</param>
        /// <param name="value">Value to which the given instance should be set.</param>
        public void SetValueFor(string instanceName, long value)
        {
            //ToDo: Not supported in .Net Core
            //foreach (PerformanceCounter counter in counters)
            //{
            //	if (counter.InstanceName.Equals(instanceName))
            //	{
            //		counter.RawValue = value;
            //		break;
            //	}
            //}
        }

        //ToDo: Not supported in .Net Core
        //      /// <summary>
        //      /// Initializes a performance counter, giving it the specified <paramref name="instanceName"></paramref>.
        //      /// </summary>
        //      /// <param name="instanceName">Instance name to be given to the instantiated <see cref="PerformanceCounter"></see></param>.
        ///// <returns>Initialized <see cref="PerformanceCounter"></see></returns>.
        //      protected PerformanceCounter InstantiateCounter(string instanceName)
        //      {
        //          return new PerformanceCounter(counterCategoryName, counterName, instanceName, false);
        //      }

        /// <summary>
        /// Increments the associated performance counters by one.
        /// </summary>
        /// <param name="instanceName">The instance to be incremented.</param>
        public void Increment(string instanceName)
        {
            //ToDo: Not supported in .Net Core
            //PerformanceCounter counter = InstantiateCounter(instanceName);
            //counter.Increment();
        }
    }
}
