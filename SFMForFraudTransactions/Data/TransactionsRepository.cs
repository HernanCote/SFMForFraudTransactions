using Microsoft.EntityFrameworkCore;
using SFMForFraudTransactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Data
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly ApplicationDbContext _context;

        public TransactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a transaction by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Transaction GetTransactionById(int id)
        {
            return _context.Transactions.Include(t => t.OriginCustomer).Include(t => t.DestinationCustomer).FirstOrDefault(t => t.Id == id);
        }


        /// <summary>
        /// Return all transactions in the database. If a query string is passed to this method,
        /// the logic will evaluate the term and return all transactions that contains the term.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<Transaction> GetAllTranstactions(string query = null)
        {
            var transactions = _context.Transactions.Include(t => t.OriginCustomer).Include(t => t.DestinationCustomer).OrderBy(t => t.Date).ToList();

            if (!String.IsNullOrEmpty(query))
            {
                DateTime date;
                DateTime.TryParse(query, out date);
                transactions = transactions.Where(t => t.DestinationCustomer.Name.ToLower().Contains(query) ||
                                                        t.Date.ToString().Contains(date.ToString()) ||
                                                        t.IsFraud.ToString().ToLower().Contains(query)).ToList();
            }

            return transactions;
        }

        /// <summary>
        /// Apply changes to the databases
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }


        /// <summary>
        /// Save a transaction in the database.
        /// </summary>
        /// <param name="transaction"></param>
        public void SaveTransaction(Transaction transaction)
        {
            var business = new TransactionBusiness(transaction.OriginCustomer, transaction.DestinationCustomer, transaction.Amount);

            transaction.OldBalanceOrigin = business.OldBalanceOrigin;
            transaction.NewBalanceOrigin = business.NewBalanceOrigin;
            transaction.OldBalanceDestination = business.OldBalanceDestination;
            transaction.NewBalanceDestination = business.NewBalanceDestination;

            _context.Transactions.Add(transaction);
        }


        /// <summary>
        /// Update a transaction in the database
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Transaction UpdateTransaction(Transaction transaction)
        {
            var record = _context.Transactions.FirstOrDefault(t => t.Id == transaction.Id);

            if (record != null)
            {
                record.IsFlaggedFraud = transaction.IsFlaggedFraud;
                record.IsFraud = transaction.IsFraud;
                _context.Transactions.Update(record);
            }

            return record;
        }
    }
}
