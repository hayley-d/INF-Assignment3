﻿using System;
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
    public class authorsController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: authors
        public async Task<ActionResult> Index()
        {
            return View(await db.authors.ToListAsync());
        }

        // GET: authors/Create
        public ActionResult Create()
        {
            return PartialView("_CreateAuthorPartial");
        }

        // POST: authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "authorId,name,surname")] author author)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(author);
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain", "Home");
            }

            return View(author);
        }

        // GET: authors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = await db.authors.FindAsync(id);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
               return PartialView("_EditAuthorPartial", author);
            }

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "authorId,name,surname")] author author)
        {
            if (ModelState.IsValid)
            {
                db.Entry(author).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Maintain", "Home");
            }
            return View(author);
        }

        // GET: authors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = await db.authors.FindAsync(id);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_DeleteAuthorPartial", author);
            }
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var author = await db.authors.FindAsync(id);

            bool hasBooks = await db.books.AnyAsync(b => b.authorId == id);
            if (hasBooks)
            {
                Console.WriteLine("Cannot Delete: Author has books in the database");
                return RedirectToAction("Maintain", "Home");
            }
            db.authors.Remove(author);
            await db.SaveChangesAsync();
            return RedirectToAction("Maintain", "Home");
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
