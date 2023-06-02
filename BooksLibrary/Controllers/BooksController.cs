using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksLibrary.Data;
using BooksLibrary.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BooksLibrary.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
              return _context.Books != null ? 
                          View(await _context.Books.ToListAsync()) :
                          Problem("Entity set 'LibraryContext.Books'  is null.");
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Book book)
        {
            if (ModelState.IsValid)
            {
                book.MaxNumberOfBooks = book.NumberOfBooks;
                _context.Add(book);
                for(int i = 0 ; i < book.NumberOfBooks; i++)
                {
                    var shelfBook = new ShelfBook() { Book = book , Ispn = book.Title+i.ToString()};
                    _context.ShelfBooks.Add(shelfBook);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'LibraryContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Borrow
        public async Task<IActionResult> Borrow(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> MakeBorrow(int borrowerId, int bookId, DateTime start, DateTime end, int quantity)
        {
            if (borrowerId == null || _context.Books == null)
            {
                return NotFound();
            }

            //var book = await _context.Books.FindAsync(id);
            if (bookId == null)
            {
                return NotFound();
            }

            try
            {
                var book = _context.Books.Find(bookId);
                if (book.NumberOfBooks >= quantity)
                {
                    var shelfBooks = _context.ShelfBooks.Where(b => b.Book.Id == bookId).Skip(book.MaxNumberOfBooks - book.NumberOfBooks).Take(quantity).ToList();
                    book.NumberOfBooks -= quantity;
                    foreach (var item in shelfBooks)
                    {
                        _context.BorrowedBooks.Add(new BorrowedBook { BorrowerId = borrowerId, BorrowDate = start, ReturnDate = end, ShelfBookId = item.Id });
                    }
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index");
        }
        public IActionResult GetBorrowers(string searchText)
        {
            // Code to retrieve data
            return Json(_context.Borrowers.Where(b => b.Phone.Contains(searchText))); // Return the data as JSON
        }
        public IActionResult GetBorrower(string searchText)
        {
            // Code to retrieve data
            return Json(_context.Borrowers.Where(b => b.Phone.Contains(searchText)).FirstOrDefault()); // Return the data as JSON
        }
        private bool BookExists(int id)
        {
          return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }




    }
}
