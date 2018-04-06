using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Data
{
    public interface ITransactionsRepository
    {
        void SaveTransaction(Transaction transaction);
        Transaction UpdateTransaction(Transaction transaction);
        IEnumerable<Transaction> GetAllTranstactions(string query = null);
        Transaction GetTransactionById(int id);
        Task<bool> SaveAsync();
    }
}
