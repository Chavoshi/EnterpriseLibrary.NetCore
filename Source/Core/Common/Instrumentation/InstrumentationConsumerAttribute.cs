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

namespace Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation
{
    /// <summary>
    /// Defines methods that are consuming instrumentation events.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class InstrumentationConsumerAttribute : InstrumentationBaseAttribute
    {
        /// <summary>
        /// Initializes this instance with the instrumentation subject name being consumed.
        /// </summary>
        /// <param name="subjectName">Subject name of the event being consumed.</param>
        public InstrumentationConsumerAttribute(string subjectName)
        : base(subjectName)
        {
        }
    }
}
