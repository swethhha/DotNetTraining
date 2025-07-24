namespace BookRentalSystem.Models
{
    public interface IRentable
    {
        void Rent();
        void Return();
        void ReportStatus();
    }
}
