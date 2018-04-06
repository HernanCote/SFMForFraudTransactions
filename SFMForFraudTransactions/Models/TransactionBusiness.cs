namespace SFMForFraudTransactions.Models
{
    /// <summary>
    /// Transaction business class
    /// </summary>
    public class TransactionBusiness
    {
        private Customer _originCustomer;
        private Customer _destinationCustomer;
        private int _amount;

        public TransactionBusiness(Customer originCustomer, Customer destinationCustomer, int amount)
        {
            _originCustomer = originCustomer;
            _destinationCustomer = destinationCustomer;
            _amount = amount;
        }

        /// <summary>
        /// Get old balance from the origin account
        /// </summary>
        public int OldBalanceOrigin
        {
            get { return (_originCustomer.Balance); }
        }

        /// <summary>
        /// Get new balance from the origin account
        /// </summary>
        public int NewBalanceOrigin
        {
            get { return (_originCustomer.Balance - _amount); }
        }

        /// <summary>
        /// Get Old Balance from the destination Account
        /// </summary>
        public int OldBalanceDestination
        {
            get { return (_destinationCustomer.Balance); }
        }

        /// <summary>
        /// Get New balance from the destination account
        /// </summary>
        public int NewBalanceDestination
        {
            get { return (_destinationCustomer.Balance + _amount); }
        }
    }
}
