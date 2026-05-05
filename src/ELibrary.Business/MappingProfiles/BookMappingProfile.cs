using AutoMapper;
using ELibrary.Business.Models.BookModel;
using ELibrary.Core.Entities;

namespace ELibrary.Business.MappingProfiles
{
    public class BookMappingProfile : Profile, IMappingProfilesMarker
    {
        public BookMappingProfile()
        {
            // CreateBookModel -> Book
            CreateMap<CreateBookModel, Book>();

            // UpdateBookModel -> Book
            CreateMap<UpdateBookModel, Book>();

            // Book -> BookResponseModel
            CreateMap<Book, BookResponseModel>();
        }
    }
}
