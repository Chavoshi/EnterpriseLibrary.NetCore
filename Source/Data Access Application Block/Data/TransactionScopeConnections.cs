﻿//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace Microsoft.Practices.EnterpriseLibrary.Data
{
    /// <summary>
    ///		This class manages the connections that will be used when transactions are active
    ///		as a result of instantiating a <see cref="TransactionScope"/>. When a transaction
    ///		is active, all database access must be through this single connection unless you want
    ///		to use a distributed transaction, which is an expensive operation.
    /// </summary>
    public static class TransactionScopeConnections
    {
        // There's a reason why this field is not thread-static: notifications for completed Oracle transactions
        // may happen in a different thread
        static readonly Dictionary<Transaction, Dictionary<string, DatabaseConnectionWrapper>> transactionConnections =
            new Dictionary<Transaction, Dictionary<string, DatabaseConnectionWrapper>>();

        /// <summary>
        ///		Returns a connection for the current transaction. This will be an existing <see cref="DbConnection"/>
        ///		instance or a new one if there is a <see cref="TransactionScope"/> active. Otherwise this method
        ///		returns null.
        /// </summary>
        /// <param name="db"></param>
        /// <returns>Either a <see cref="DbConnection"/> instance or null.</returns>
        public static DatabaseConnectionWrapper GetConnection(Database db)
        {
            Transaction currentTransaction = Transaction.Current;

            if (currentTransaction == null)
                return null;

            Dictionary<string, DatabaseConnectionWrapper> connectionList;
            DatabaseConnectionWrapper connection;

            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(currentTransaction, out connectionList))
                {
                    // We don't have a list for this transaction, so create a new one
                    connectionList = new Dictionary<string, DatabaseConnectionWrapper>();
                    transactionConnections.Add(currentTransaction, connectionList);

                    // We need to know when this previously unknown transaction is completed too
                    currentTransaction.TransactionCompleted += OnTransactionCompleted;
                }
            }

            lock (connectionList)
            {
                // Next we'll see if there is already a connection. If not, we'll create a new connection and add it
                // to the transaction's list of connections.
                // This collection should only be modified by the thread where the transaction scope was created
                // while the transaction scope is active.
                // However there's no documentation to confirm this, so we err on the safe side and lock.
                if (!connectionList.TryGetValue(db.ConnectionString, out connection))
                {
                    // we're betting the cost of acquiring a new finer-grained lock is less than 
                    // that of opening a new connection, and besides this allows threads to work in parallel
                    var dbConnection = db.GetNewOpenConnection();
                    connection = new DatabaseConnectionWrapper(dbConnection);
                    connectionList.Add(db.ConnectionString, connection);
                }
                connection.AddRef();
            }

            return connection;
        }

        /// <summary>
        ///		This event handler is called whenever a transaction is about to be disposed, which allows
        ///		us to remove the transaction from our list and dispose the connection instance we created.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void OnTransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, DatabaseConnectionWrapper> connectionList;

            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(e.Transaction, out connectionList))
                {
                    // we don't know about this transaction. odd.
                    return;
                }

                // we know about this transaction - remove it from the mappings
                transactionConnections.Remove(e.Transaction);
            }

            lock (connectionList)
            {
                // acquiring this lock should not be necessary unless there's a possibility for this event to be fired
                // while the transaction involved in the event is still set as the current transaction for a 
                // different thread.
                foreach (var connectionWrapper in connectionList.Values)
                {
                    connectionWrapper.Dispose();
                }
            }
        }
    }
}
