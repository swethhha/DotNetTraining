using System;
using Day4Assessment.Models;
using Day4Assessment.Services;

namespace Day4Assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount account = new SavingsAccount
            {
                AccountNumber = 1001,
                AccountHolderName = "Swetha",
                Balance = 0
            };

            Console.WriteLine($"Welcome {account.AccountHolderName}!");
            Console.WriteLine($"Account Type: {account.AccountType}");
            Console.WriteLine($"Account No. : {account.AccountNumber}");
            Console.WriteLine();

            account.Deposit(10000);
            account.Withdraw(3000);
            account.Deposit(2000);
            account.Withdraw(10000); 


            IAccountService service = new AccountService();

            Console.WriteLine();
            service.ShowTransactions(account);

            Console.WriteLine();
            Console.WriteLine("Account Summary:");
            account.Display();
        }
    }
}
