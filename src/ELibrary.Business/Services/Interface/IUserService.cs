using ELibrary.Business.Models.User;
using ELibrary.Business.Models;

namespace ELibrary.Business.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResult<CreateUserResponseModel>> RegisterAsync(CreateUserModel model);
        Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model);
        Task<ApiResult<UserResponseModel>> GetProfileAsync(Guid userId);
    }
}
