// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

namespace Microsoft.Practices.EnterpriseLibrary.Data.Oracle
{
    /// <summary>
    /// Represents the description of an Oracle package mapping.
    /// </summary>
    /// <remarks>
    /// <see cref="IOraclePackage"/> is used to specify how to transform store procedure names 
    /// into package qualified Oracle stored procedure names.
    /// </remarks>
    /// <seealso cref="OracleDatabase"/>
    public interface IOraclePackage
    {
        /// <summary>
        /// When implemented by a class, gets the name of the package.
        /// </summary>
        /// <value>
        /// The name of the package.
        /// </value>
        string Name
        { get; }

        /// <summary>
        /// When implemented by a class, gets the prefix for the package.
        /// </summary>
        /// <value>
        /// The prefix for the package.
        /// </value>
        string Prefix
        { get; }
    }
}
