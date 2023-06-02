namespace BooksLibrary.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int ShelfBookId { get; set; }
        public ShelfBook ShelfBook { get; set; }
        public int BorrowerId { get; set; }
        public Borrower Borrower { get; set; }

    }
}
