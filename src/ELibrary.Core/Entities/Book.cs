using ELibrary.Core.Common;

namespace ELibrary.Core.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public string Genre { get; set; }
        public int AvailableCopies { get; set; }

        // Navigation properties
        public ICollection<Transaction> Transactions { get; set; }
    }
}
