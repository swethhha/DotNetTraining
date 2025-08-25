namespace HostelManagement.Core.DTOs
{
    public class ErrorResponseDTO
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public string Details { get; set; }
        public string CorrelationId { get; set; }
    }
}
