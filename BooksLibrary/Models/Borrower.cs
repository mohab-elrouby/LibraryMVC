namespace BooksLibrary.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        IList<BorrowedBook> BorrowedBooks { get; set;} = new List<BorrowedBook>();

    }
}
