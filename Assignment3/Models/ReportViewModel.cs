using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Razor.Generator;
using System.Web.Services.Description;

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
         * Generates a sorted dictionary of books sorted in descending order of the number of times a book has been borrowed.
         * @return A dictinary of books and the amount of times each book has been taken out.
         */
        public Dictionary<string,int> GetPopularBooks()
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
                          BookId = book.bookId,  
                          BookTitle = book.name, 
                          Count = bc.BorrowCount
                      })
                .GroupBy(x => x.BookTitle) 
                .Select(group => new
                {
                    BookTitle = group.First().BookTitle, 
                    TotalCount = group.Sum(x => x.Count) 
                })
                .OrderByDescending(x => x.TotalCount)
                .Take(10)
                .ToDictionary(x => x.BookTitle, x => x.TotalCount);

            return popularBooks;
        }

        /**
         * Generates a dictionary linking each book type to the count of books associated with that type.
         * @return A Dictionary<string, int> where the key is the type name and the value is the count of books of that type.
         */
        public Dictionary<string,int> GetTypeCounts()
        {
            Dictionary<string, int> bookCountByType = Types
        .GroupBy(type => type.name) // Group by the type name
        .Select(group => new
        {
            TypeName = group.Key,
            BookCount = Books.Count(book => book.typeId == group.First().typeId) // Count books for this type
        })
        .Take(5)
        .ToDictionary(x => x.TypeName, x => x.BookCount);

            return bookCountByType;
        }

        /**
          * Generates a dictionary of students' full names and their respective borrow count, 
          * sorted in descending order by the amount of books borrowed.
          * @return Dictionary where the key is the student's full name and the value is the borrow count.
          */
        public Dictionary<string, int> GetTopBorrowers()
        {
            var topBorrowers = Borrows
                .GroupBy(b => b.studentId)
                .Select(group => new
                {
                    StudentId = group.Key,
                    BorrowCount = group.Count()
                })
                .OrderByDescending(x => x.BorrowCount)
                .Take(10)
                .ToList();

            var result = (from borrower in topBorrowers
                          join student in Students on borrower.StudentId equals student.studentId
                          select new
                          {
                              FullName = student.name + " " + student.surname,
                              BorrowCount = borrower.BorrowCount
                          })
                         .ToDictionary(x => x.FullName, x => x.BorrowCount);

            return result;
        }

        /**
         * Generates a dictionary of author names and their respective borrow count.
         * Sorted in descending order by the amount of times borrowed.
         * @return Dictionary where the key is the author name and the value is the borrow count.
         */
        public Dictionary<string,int> GetTopAuthors()
        {
            var top10Authors = Borrows
            .GroupBy(borrow => Books.First(book => book.bookId == borrow.bookId).authorId)  
            .Select(group => new
            {
                AuthorId = group.Key,
                BorrowCount = group.Count()                             
            })
            .OrderByDescending(author => author.BorrowCount)            
            .Take(10)                                                   
            .Join(Authors,                                              
                  borrow => borrow.AuthorId,
                  author => author.authorId,
                  (borrow, author) => new { author.name, borrow.BorrowCount }) 
            .ToDictionary(author => author.name, author => author.BorrowCount);

            return top10Authors;
        }


        

    }
}