using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assignment3.Models
{
    public class MaintainViewModel
    {
        public List<author> Authors { get; set; }
        public List<borrow> Borrows { get; set; }

        public List<type> Types { get; set; }

        public MaintainViewModel(List<author> authors, List<borrow> borrows, List<type> types)
        {
            Authors = authors;
            Borrows = borrows;
            Types = types;
        }
    }
}