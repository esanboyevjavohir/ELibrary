using AutoMapper;
using ELibrary.Business.Models.BookModel;
using ELibrary.Business.Models;
using ELibrary.Business.Services.Interface;
using ELibrary.Core.Entities;
using ELibrary.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ELibrary.Business.Services.Implement
{
    public class BookService : IBookService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public BookService(DatabaseContext context, IMapper mapper, IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<ApiResult<CreateBookResponseModel>> CreateAsync(CreateBookModel model)
        {
            var book = _mapper.Map<Book>(model);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return ApiResult<CreateBookResponseModel>.Success(
                new CreateBookResponseModel { Id = book.Id });
        }

        public async Task<ApiResult<UpdateBookResponseModel>> UpdateAsync(Guid id, UpdateBookModel model)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book is null)
                return ApiResult<UpdateBookResponseModel>.Failure(["Kitob topilmadi"]);

            _mapper.Map(model, book);
            await _context.SaveChangesAsync();

            // Cache ni tozala
            await _cache.RemoveAsync($"book:{id}");

            return ApiResult<UpdateBookResponseModel>.Success(
                new UpdateBookResponseModel { Id = book.Id });
        }

        public async Task<ApiResult<bool>> DeleteAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book is null)
                return ApiResult<bool>.Failure(["Kitob topilmadi"]);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            // Cache ni tozala
            await _cache.RemoveAsync($"book:{id}");

            return ApiResult<bool>.Success(true);
        }

        public async Task<ApiResult<BookResponseModel>> GetByIdAsync(Guid id)
        {
            // 1. Avval Redis dan qidir
            var cacheKey = $"book:{id}";
            var cached = await _cache.GetStringAsync(cacheKey);
            if (cached is not null)
            {
                var cachedBook = JsonSerializer.Deserialize<BookResponseModel>(cached);
                return ApiResult<BookResponseModel>.Success(cachedBook!);
            }

            // 2. Redis da yo'q — bazadan ol
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book is null)
                return ApiResult<BookResponseModel>.Failure(["Kitob topilmadi"]);

            var response = _mapper.Map<BookResponseModel>(book);

            // 3. Redis ga saqlа
            await _cache.SetStringAsync(cacheKey,
                JsonSerializer.Serialize(response),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                });

            return ApiResult<BookResponseModel>.Success(response);
        }

        public async Task<ApiResult<List<BookResponseModel>>> GetAllAsync(
            string? genre, string? author, int page, int pageSize)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre == genre);

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author == author);

            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return ApiResult<List<BookResponseModel>>.Success(
                _mapper.Map<List<BookResponseModel>>(books));
        }
    }
}
