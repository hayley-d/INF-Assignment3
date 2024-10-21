using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace Assignment3.Models
{
    public class HomeViewModel
    {
        public List<student> Students { get; set; }
        public List<book> Books { get; set; }

        public List<borrow> Borrows { get; set; }

        /**
         * Int for the bookId and bool is true is the book is available else false if it has been taken out.
         */
        public Dictionary<int,bool> Status { get; set; }

        public HomeViewModel(List<book> books, List<student> students, List<borrow> borrows)
        {
            Books = books;
            Students = students;
            Borrows = borrows;

            this.Status = books.ToDictionary(
                book => book.bookId,
                book => borrows.Any(b => b.bookId == book.bookId && b.broughtDate.HasValue) // Check if any borrow record has a BroughtDate
            );
        }

        public String getStatus(int id)
        {
            return Status.FirstOrDefault(s => s.Key == id).Value ? "Available" : "Out";
        }

    }
}