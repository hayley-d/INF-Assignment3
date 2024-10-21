using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor.Generator;

namespace Assignment3.Models
{
    public class ReportViewModel
    {
        public List<author> Authors { get; set; }
        public List<borrow> Borrows { get; set; }
        public List<student> Students { get; set; }
        public List<book> Books { get; set; }
        public List<type> Types { get; set; }

        public ReportViewModel(List<author> authors, List<borrow> borrows, List<type> types, List<book> books,List<student> students)
        {
            Authors = authors;
            Borrows = borrows;
            Types = types;
            Books = books;
            Students = students;
        }

        /**
         * Generates a list of students who currently have overdue books.
         * @return A list of students with books overdue.
         */
        public List<student> GetOverdueBooks()
        {
            return this.Students;
        }

        /**
         * Generates a sorted list of books sorted in descending order of the number of times a book has been borrowed.
         * @return A list of books.
         */
        public List<book> GetPopularBooks()
        {
            return this.Books;
        }

        /**
         * Generates a map, linking the amount of borrows per month in the year 2015 (as per database).
         * @return A dictionary<String,int> where string is the month name and int is the count of books for the month.
         */
        public Dictionary<String,int> GetBorrowsPerMonth()
        {
            return new Dictionary<string, int>();
        }

        /**
         * Generates a list of strudents sorted in descending order by the amount of books borrowed.
         * @return List of students sorted.
         */
        public List<student> GetTopBorrowers()
        {
            return this.Students;
        }

    }
}