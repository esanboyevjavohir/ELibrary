using ELibrary.Core.Common;
using System.Transactions;

namespace ELibrary.Core.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }        
        public string PasswordHash { get; set; } // xeshlangan
        public decimal Balance { get; set; } = 0;

        // Navigation properties
        public ICollection<Transaction> Transactions { get; set; }
    }
}
