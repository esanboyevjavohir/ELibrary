using ELibrary.Core.Enums;

namespace ELibrary.Business.Models.TransactionModel
{
    public class TransactionResponseModel : BaseResponseModel
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; } = null!;
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
