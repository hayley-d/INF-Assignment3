using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Assignment3.Models;

namespace Assignment3.Controllers
{
    public class booksController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: books
        public async Task<ActionResult> Index()
        {
            var books = db.books.Include(b => b.author).Include(b => b.type);
            return View(await books.ToListAsync());
        }

        // GET: books/Create
        public ActionResult Create()
        {
            ViewBag.authorId = new SelectList(db.authors, "authorId", "name");
            ViewBag.typeId = new SelectList(db.types, "typeId", "name");
            return View("_CreateBookPartial");
        }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "bookId,name,pagecount,point,authorId,typeId")] book book)
        {
            if (ModelState.IsValid)
            {
                db.books.Add(book);
                await db.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }

            ViewBag.authorId = new SelectList(db.authors, "authorId", "name", book.authorId);
            ViewBag.typeId = new SelectList(db.types, "typeId", "name", book.typeId);
            return View(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
