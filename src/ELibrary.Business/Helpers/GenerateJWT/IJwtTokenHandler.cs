using ELibrary.Core.Entities;

namespace ELibrary.Business.Helpers.GenerateJWT
{
    public interface IJwtTokenHandler
    {
        string GenerateAccessToken(User user);
        string GenerateAccessToken(User user, string sessionToken);
    }
}
