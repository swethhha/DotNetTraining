using BookRentalSystem.Models;

namespace BookRentalSystem.Services
{
    public class BookService : IBookService
    {
        public void DisplayAll(List<Book> books)
        {
            Console.WriteLine("=== Book List ===");
            foreach (var book in books)
            {
                book.Display();
            }
            Console.WriteLine();
        }

        public void ShowRentalStatus(List<IRentable> items)
        {
            foreach (var item in items)
            {
                item.ReportStatus();
            }
            Console.WriteLine();
        }
    }
}
