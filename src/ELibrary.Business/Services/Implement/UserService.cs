using AutoMapper;
using ELibrary.Business.Models.User;
using ELibrary.Business.Models;
using ELibrary.Business.Services.Interface;
using ELibrary.Core.Entities;
using ELibrary.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using ELibrary.Business.Helpers.GenerateJWT;

namespace ELibrary.Business.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public UserService(
        DatabaseContext context,
        IMapper mapper,
        IJwtTokenHandler jwtTokenHandler)
        {
            _context = context;
            _mapper = mapper;
            _jwtTokenHandler = jwtTokenHandler;
        }

        public async Task<ApiResult<CreateUserResponseModel>> RegisterAsync(CreateUserModel model)
        {
            var exists = await _context.Users
                .AnyAsync(u => u.Email == model.Email);
            if (exists)
                return ApiResult<CreateUserResponseModel>.Failure(["Email allaqachon mavjud"]);

            var user = _mapper.Map<User>(model);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return ApiResult<CreateUserResponseModel>.Success(
                new CreateUserResponseModel 
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            });
        }

        public async Task<ApiResult<LoginResponseModel>> LoginAsync(LoginUserModel model)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                return ApiResult<LoginResponseModel>.Failure(["Email yoki parol noto'g'ri"]);

            var accessToken = _jwtTokenHandler.GenerateAccessToken(user);

            return ApiResult<LoginResponseModel>.Success(new LoginResponseModel
            {
                Email = user.Email,
                AccessToken = accessToken
            });
        }

        public async Task<ApiResult<UserResponseModel>> GetProfileAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                return ApiResult<UserResponseModel>.Failure(["Foydalanuvchi topilmadi"]);

            return ApiResult<UserResponseModel>.Success(_mapper.Map<UserResponseModel>(user));
        }
    }
}
