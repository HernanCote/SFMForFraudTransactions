using SFMForFraudTransactions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SFMForFraudTransactions.Data
{
    /// <summary>
    /// Transaction Repository Interface
    /// </summary>
    public interface ITransactionsRepository
    {
        void SaveTransaction(Transaction transaction);
        Transaction UpdateTransaction(Transaction transaction);
        IEnumerable<Transaction> GetAllTranstactions(string query = null);
        Transaction GetTransactionById(int id);
        Task<bool> SaveAsync();
    }
}
