namespace BooksLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int NumberOfBooks { get; set; } = 0;
        public int MaxNumberOfBooks { get; set; } = 0;
        public IList<ShelfBook> ShelfBooks { get; set; } = new List<ShelfBook>();

        public Book()
        {
        }

        public Book(int id, string title, string description, string author, int numberOfBooks, int maxNumberOfBooks, IList<ShelfBook> shelfBooks)
        {
            Id=id;
            Title=title??throw new ArgumentNullException(nameof(title));
            Description=description??throw new ArgumentNullException(nameof(description));
            Author=author??throw new ArgumentNullException(nameof(author));
            NumberOfBooks=numberOfBooks;
            MaxNumberOfBooks=maxNumberOfBooks;
            ShelfBooks=shelfBooks??throw new ArgumentNullException(nameof(shelfBooks));
        }
    }
}
