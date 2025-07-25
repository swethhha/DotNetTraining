using System;
using Day4Assessment.Models;

namespace Day4Assessment.Services
{
    public class AccountService : IAccountService
    {
        public void ShowTransactions(Account account)
        {
            Console.WriteLine("--- Transactions ---");
            if (account.Transactions.Count == 0)
            {
                Console.WriteLine("No transactions available.");
            }
            else
            {
                foreach (var txn in account.Transactions)
                {
                    Console.WriteLine(txn);
                }
            }
        }
    }
}
