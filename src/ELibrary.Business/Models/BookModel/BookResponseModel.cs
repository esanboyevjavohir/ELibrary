namespace ELibrary.Business.Models.BookModel
{
    public class BookResponseModel : BaseResponseModel
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public decimal Price { get; set; }
        public string Genre { get; set; } = null!;
        public int AvailableCopies { get; set; }
    }
}
