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
        public Transaction GetTransactionById(int id)
        {
            return _context.Transactions.Include(t => t.OriginCustomer).Include(t => t.DestinationCustomer).FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Transaction> GetTransactionByQuery()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTranstactions(string query = null)
        {
            var transactions = _context.Transactions.Include(t => t.OriginCustomer).Include(t => t.DestinationCustomer).ToList();

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

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void SaveTransaction(Transaction transaction)
        {
            var business = new TransactionBusiness(transaction.OriginCustomer, transaction.DestinationCustomer, transaction.Amount);

            transaction.OldBalanceOrigin = business.OldBalanceOrigin;
            transaction.NewBalanceOrigin = business.NewBalanceOrigin;
            transaction.OldBalanceDestination = business.OldBalanceDestination;
            transaction.NewBalanceDestination = business.NewBalanceDestination;

            _context.Transactions.Add(transaction);
        }

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
