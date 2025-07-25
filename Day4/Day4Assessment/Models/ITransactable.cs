namespace Day4Assessment.Models
{
    public interface ITransactable
    {
        void Deposit(double amount);
        void Withdraw(double amount);
        void Display();
    }
}