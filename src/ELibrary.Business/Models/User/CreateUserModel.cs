namespace ELibrary.Business.Models.User
{
    public class CreateUserModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateUserResponseModel : BaseResponseModel
    {
        public string FullName { get; set; } 
        public string Email { get; set; } 
    }
}
