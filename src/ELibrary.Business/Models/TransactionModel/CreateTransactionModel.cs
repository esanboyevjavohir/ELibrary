using ELibrary.Core.Enums;

namespace ELibrary.Business.Models.TransactionModel
{
    public class CreateTransactionModel
    {
        public Guid BookId { get; set; }
        public TransactionType Type { get; set; }
    }

    public class CreateTransactionResponseModel : BaseResponseModel { }
}
