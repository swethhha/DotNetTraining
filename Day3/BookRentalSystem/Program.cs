using BookRentalSystem.Models;
using BookRentalSystem.Services;
using System;
using System.Collections.Generic;

namespace BookRentalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Book fictionBook = new Fiction
            {
                Id = 1,
                Title = "The Alchemist",
                Author = "Paulo Coelho",
                Genre = "Adventure"
            };

            Book nonFictionBook = new NonFiction
            {
                Id = 2,
                Title = "Clean Code",
                Author = "Robert C. Martin",
                Category = "Programming"
            };

            List<Book> books = new List<Book> { fictionBook, nonFictionBook };
            List<IRentable> rentableBooks = new List<IRentable> {
                (IRentable)fictionBook,
                (IRentable)nonFictionBook
            };

            IBookService bookService = new BookService();

            Console.WriteLine("Welcome to Book Rental System\n");


            Console.WriteLine("Available Books:\n---------------------------");
            bookService.DisplayAll(books);


            Console.WriteLine("\nRenting Books...");
            foreach (var item in rentableBooks)
            {
                item.Rent();
            }


            Console.WriteLine("\nRental Status After Renting:\n---------------------------");
            bookService.ShowRentalStatus(rentableBooks);


            Console.WriteLine("\nReturning Books...");
            foreach (var item in rentableBooks)
            {
                item.Return();
            }

            Console.WriteLine("\nRental Status After Returning:\n---------------------------");
            bookService.ShowRentalStatus(rentableBooks);

            Console.WriteLine("\nThank you for using Book Rental System!");
        }
    }
}
