using System;
using Day4Assesment.Models;
using Day4Assesment.Services;
using Day4Assesment.Interfaces;

namespace Day4Assesment
{
    class Program
    {
        static void Main(string[] args)
        {
            SavingsAccount s = new SavingsAccount
            {
                AccNo = 1001,
                Name = "Swetha",
                Bal = 9000
            };

            CurrentAccount c = new CurrentAccount
            {
                AccNo = 1002,
                Name = "Karthik",
                Bal = 8000
            };

            IAccountService svc = new AccountService();

            Console.WriteLine("Welcome " + s.Name);
            Console.WriteLine("Account Type: Savings");
            Console.WriteLine("Account No. : " + s.AccNo);

            svc.Deposit(s, 10000);
            svc.Withdraw(s, 3000);
            svc.Deposit(s, 2000);
            svc.Withdraw(s, 15000); // Fail

            svc.ShowDetails(s);
            svc.ShowTxn(s);
        }
    }
}
