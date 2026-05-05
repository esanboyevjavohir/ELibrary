using ELibrary.Business.Models.BookModel;
using ELibrary.Business.Models;

namespace ELibrary.Business.Services.Interface
{
    public interface IBookService
    {
        Task<ApiResult<CreateBookResponseModel>> CreateAsync(CreateBookModel model);
        Task<ApiResult<UpdateBookResponseModel>> UpdateAsync(Guid id, UpdateBookModel model);
        Task<ApiResult<bool>> DeleteAsync(Guid id);
        Task<ApiResult<BookResponseModel>> GetByIdAsync(Guid id);
        Task<ApiResult<List<BookResponseModel>>> GetAllAsync(string? genre, string? author, int page, int pageSize);
    }
}
