using System;
using System.Collections.Generic;
using BooksLibrary.Models;
using Microsoft.EntityFrameworkCore;


namespace BooksLibrary.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() { }
        public LibraryContext(DbContextOptions<LibraryContext> options): base(options) { }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<ShelfBook> ShelfBooks { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

    }

}
