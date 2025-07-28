using Day4Assesment.Models;

namespace Day4Assesment.Interfaces
{
    public interface IAccountService
    {
        void Deposit(Account acc, double amt);
        void Withdraw(Account acc, double amt);
        void ShowTxn(Account acc);
        void ShowDetails(Account acc);
    }
}
