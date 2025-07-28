using System;
using System.Collections.Generic;
using Day4Assesment.Interfaces;

namespace Day4Assesment.Models
{
    public abstract class Account : ITransactable
    {
        public int AccNo { get; set; }
        public string Name { get; set; } = "";
        public double Bal { get; set; }
        public List<Transaction> Txns { get; set; } = new();

        public void Deposit(double amt)
        {
            if (amt > 0)
            {
                Bal += amt;
                Txns.Add(new Transaction("Deposit", amt));
                Console.WriteLine("Amount deposited.");
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        public abstract void Withdraw(double amt);

        public virtual void Display()
        {
            Console.WriteLine();
            Console.WriteLine("Account Summary");
            Console.WriteLine("Name      : " + Name);
            Console.WriteLine("Acc No    : " + AccNo);
            Console.WriteLine("Type      : " + (this is SavingsAccount ? "Savings" : "Current"));
            Console.WriteLine("Balance   : Rs." + Bal);
        }
    }
}
