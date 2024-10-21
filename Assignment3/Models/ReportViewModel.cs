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
         * Generates a dictionary of authors and their book counts
         * @return A list of authors and their book counts.
         */
        public Dictionary<author,int> GetAuthorCounts()
        {
            var booksPerAuthor = Books
            .GroupBy(b => b.authorId) 
            .Select(group => new
            {
                AuthorId = group.Key,
                BookCount = group.Count() 
            })
            .ToList();

            var authorsWithBookCount = (from result in booksPerAuthor
                                        join author in Authors on result.AuthorId equals author.authorId
                                        select new
                                        {
                                            Author = author,
                                            BookCount = result.BookCount
                                        })
                                       .ToDictionary(a => a.Author, a => a.BookCount);

            return authorsWithBookCount;
        }

        /**
         * Generates a sorted dictionary of books sorted in descending order of the number of times a book has been borrowed.
         * @return A dictinary of books and the amount of times each book has been taken out.
         */
        public Dictionary<book,int> GetPopularBooks()
        {
            var borrowCounts = Borrows
            .GroupBy(b => b.bookId)
            .Select(group => new
            {
                BookId = group.Key,
                BorrowCount = group.Count()
            })
            .ToList();

            var popularBooks = Books
            .Join(borrowCounts,
                  book => book.bookId,
                  bc => bc.BookId,
                  (book, bc) => new
                  {
                      Book = book,
                      Count = bc.BorrowCount
                  })
            .OrderByDescending(x => x.Count) 
            .ToDictionary(x => x.Book, x => x.Count);
            return popularBooks;
        }

        /**
         * Generates a map, linking the amount of borrows per month in the year 2015 (as per database).
         * @return A dictionary<String,int> where string is the month name and int is the count of books for the month.
         */
        public Dictionary<String,int> GetBorrowsPerMonth()
        {
            var borrowsPerMonth = Borrows
            .GroupBy(b => b.takenDate?.Month) 
            .Select(group => new
            {
                Month = group.Key,
                Count = group.Count() 
            })
            .ToList();

            var monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;
            var result = borrowsPerMonth.ToDictionary(
                x => monthNames[(int)x.Month - 1], 
                x => x.Count);
            return new Dictionary<string, int>();
        }

        /**
         * Generates a list of students sorted in descending order by the amount of books borrowed.
         * @return List of students sorted.
         */
        public List<student> GetTopBorrowers()
        {
            var topBorrowers = Borrows
            .GroupBy(b => b.studentId) 
            .Select(group => new
            {
                StudentId = group.Key,
                BorrowCount = group.Count() 
            })
            .OrderByDescending(x => x.BorrowCount) 
            .ToList();

            var students = (from borrower in topBorrowers
                            join student in Students on borrower.StudentId equals student.Id
                            select student)
                        .ToList();
            return students;
        }

    }
}