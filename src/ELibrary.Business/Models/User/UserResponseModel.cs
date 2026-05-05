namespace ELibrary.Business.Models.User
{
    public class UserResponseModel : BaseResponseModel
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
