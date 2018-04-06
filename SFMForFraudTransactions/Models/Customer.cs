namespace SFMForFraudTransactions.Models
{
    /// <summary>
    /// Customer POCO
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Balance { get; set; }
    }
}
