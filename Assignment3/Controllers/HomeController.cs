using Assignment3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Assignment3.Controllers
{
    public class HomeController : Controller
    {
        private LibraryEntities db = new LibraryEntities();
        public async Task<ActionResult> Index()
        {
            HomeViewModel viewModel = new HomeViewModel(await db.books.ToListAsync(), await db.students.ToListAsync(), await db.borrows.ToListAsync());
            return View(viewModel);
        }

        public async Task<ActionResult> Report()
        {
            ViewBag.Message = "Report";
            ReportViewModel viewModel = new ReportViewModel(await db.authors.ToListAsync(), await db.borrows.ToListAsync(), await db.types.ToListAsync(), await db.books.ToListAsync(), await db.students.ToListAsync());
            return View(viewModel);
        }

        public async Task<ActionResult> Maintain()
        {
            ViewBag.Message = "Maintain Page";
            MaintainViewModel viewModel = new MaintainViewModel(await db.authors.ToListAsync(), await db.borrows.ToListAsync(), await db.types.ToListAsync());
            return View(viewModel);
        }

        




    }
}