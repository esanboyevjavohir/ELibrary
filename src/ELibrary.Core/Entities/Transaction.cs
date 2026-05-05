using ELibrary.Core.Common;
using ELibrary.Core.Enums;

namespace ELibrary.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public TransactionType Type { get; set; } 
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
