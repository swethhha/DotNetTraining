using System;
using Day4Assesment.Models;
using Day4Assesment.Interfaces;

namespace Day4Assesment.Services
{
    public class AccountService : IAccountService
    {
        public void Deposit(Account acc, double amt)
        {
            acc.Deposit(amt);
        }

        public void Withdraw(Account acc, double amt)
        {
            acc.Withdraw(amt);
        }

        public void ShowTxn(Account acc)
        {
            Console.WriteLine();
            Console.WriteLine("Transaction History:");
            foreach (var t in acc.Txns)
            {
                t.Print();
            }
        }

        public void ShowDetails(Account acc)
        {
            acc.Display();
        }
    }
}
