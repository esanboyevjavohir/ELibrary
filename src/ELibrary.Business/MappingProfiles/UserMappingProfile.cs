using AutoMapper;
using ELibrary.Business.Models.User;
using ELibrary.Core.Entities;

namespace ELibrary.Business.MappingProfiles
{
    public class UserMappingProfile : Profile, IMappingProfilesMarker
    {
        public UserMappingProfile()
        {
            // CreateUserModel -> User
            CreateMap<CreateUserModel, User>();

            // User -> UserResponseModel
            CreateMap<User, UserResponseModel>();
        }
    }
}
