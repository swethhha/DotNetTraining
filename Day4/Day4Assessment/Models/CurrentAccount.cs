using System;

namespace Day4Assessment.Models
{
    public class CurrentAccount : Account
    {
        public CurrentAccount()
        {
            AccountType = "Current";
        }

        public override void Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                Transactions.Add(new Transaction { Date = DateTime.Now, Type = "Deposit", Amount = amount });
                Console.WriteLine($"Deposited ₹{amount} to Current Account.");
            }
        }

        public override void Withdraw(double amount)
        {
            if (amount > 0 && amount <= Balance)
            {
                Balance -= amount;
                Transactions.Add(new Transaction { Date = DateTime.Now, Type = "Withdraw", Amount = amount });
                Console.WriteLine($"Withdrawn ₹{amount} from Current Account.");
            }
            else
            {
                Console.WriteLine("Not enough balance.");
            }
        }

        public override void Display()
        {
            Console.WriteLine($"Account Holder : {AccountHolderName}");
            Console.WriteLine($"Account Number : {AccountNumber}");
            Console.WriteLine($"Account Type   : {AccountType}");
            Console.WriteLine($"Current Balance: ₹{Balance}");
        }
    }
}
