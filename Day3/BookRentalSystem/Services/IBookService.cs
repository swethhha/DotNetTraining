using BookRentalSystem.Models;

namespace BookRentalSystem.Services
{
    public interface IBookService
    {
        void DisplayAll(List<Book> books);
        void ShowRentalStatus(List<IRentable> items);
    }
}
