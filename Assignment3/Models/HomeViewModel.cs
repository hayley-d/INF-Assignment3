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

        public HomeViewModel(List<book> books, List<student> students)
        {
            Books = books;
            Students = students;
        }
    }
}