﻿namespace SFMForFraudTransactions.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OldBalance { get; set; }
        public int NewBalance { get; set; }
    }
}