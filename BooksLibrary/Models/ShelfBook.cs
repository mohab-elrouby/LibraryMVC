namespace BooksLibrary.Models
{
    public class ShelfBook
    {
        public int Id { get; set; }
        public string Ispn { get; set; }
        public Book Book { get; set; }

    }
}
