namespace ELibrary.Business.Models.BookModel
{
    public class UpdateBookModel
    {
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public decimal Price { get; set; }
        public string Genre { get; set; } = null!;
        public int AvailableCopies { get; set; }
    }

    public class UpdateBookResponseModel : BaseResponseModel { }
}
