namespace SFMForFraudTransactions.Models
{
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

        public int OldBalanceOrigin
        {
            get { return (_originCustomer.Balance); }
        }
        public int NewBalanceOrigin
        {
            get { return (_originCustomer.Balance - _amount); }
        }
        public int OldBalanceDestination
        {
            get { return (_destinationCustomer.Balance); }
        }
        public int NewBalanceDestination
        {
            get { return (_destinationCustomer.Balance + _amount); }
        }
    }
}
