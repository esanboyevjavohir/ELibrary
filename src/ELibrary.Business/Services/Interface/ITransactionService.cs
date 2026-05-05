using ELibrary.Business.Models.TransactionModel;
using ELibrary.Business.Models;

namespace ELibrary.Business.Services.Interface
{
    public interface ITransactionService
    {
        Task<ApiResult<CreateTransactionResponseModel>> BuyAsync(Guid bookId, Guid userId);
    }
}
