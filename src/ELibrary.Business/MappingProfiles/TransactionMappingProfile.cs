using AutoMapper;
using ELibrary.Business.Models.TransactionModel;
using ELibrary.Core.Entities;

namespace ELibrary.Business.MappingProfiles
{
    public class TransactionMappingProfile : Profile, IMappingProfilesMarker
    {
        public TransactionMappingProfile()
        {
            // Transaction -> TransactionResponseModel
            CreateMap<Transaction, TransactionResponseModel>()
                .ForMember(dest => dest.BookTitle,
                    opt => opt.MapFrom(src => src.Book.Title));
        }
    }
}
