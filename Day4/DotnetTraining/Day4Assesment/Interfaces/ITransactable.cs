namespace Day4Assesment.Interfaces
{
    public interface ITransactable
    {
        void Deposit(double amt);
        void Withdraw(double amt);
        void Display();
    }
}
