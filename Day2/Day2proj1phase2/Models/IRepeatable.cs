namespace Day2proj1phase2.Models
{
    public interface IReportable
    {
        void ReportStatus();
        string GetSummary();
        void AdvanceStatus(string newStatus);
        void Display();
    }
}
