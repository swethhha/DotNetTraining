using System.Collections.Generic;

namespace Day4Assessment.Models
{
    public abstract class Account : ITransactable
    {
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;
        public double Balance { get; set; }
        public string AccountType { get; protected set; } = string.Empty;

        public List<Transaction> Transactions { get; set; } = new();

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract void Display();
    }
}
