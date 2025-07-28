using System;

namespace Day4Assesment.Models
{
    public class SavingsAccount : Account
    {
        public override void Withdraw(double amt)
        {
            if (amt > 0 && amt <= Bal)
            {
                Bal -= amt;
                Txns.Add(new Transaction("Withdraw", amt));
                Console.WriteLine("Amount withdrawn.");
            }
            else
            {
                Console.WriteLine("Not enough balance.");
            }
        }
    }
}
