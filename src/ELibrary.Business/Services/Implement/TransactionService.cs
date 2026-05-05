using AutoMapper;
using ELibrary.Business.Models.TransactionModel;
using ELibrary.Business.Models;
using ELibrary.Business.Services.Interface;
using ELibrary.Core.Enums;
using ELibrary.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using ELibrary.Core.Entities;

namespace ELibrary.Business.Services.Implement
{
    public class TransactionService : ITransactionService
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public TransactionService(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ApiResult<CreateTransactionResponseModel>> BuyAsync(Guid bookId, Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                return ApiResult<CreateTransactionResponseModel>.Failure(["Foydalanuvchi topilmadi"]);

            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book is null)
                return ApiResult<CreateTransactionResponseModel>.Failure(["Kitob topilmadi"]);

            // Nusxa bormi?
            if (book.AvailableCopies <= 0)
                return ApiResult<CreateTransactionResponseModel>.Failure(["Kitobning mavjud nusxasi yo'q"]);

            // Balans yetarlimi?
            if (user.Balance < book.Price)
                return ApiResult<CreateTransactionResponseModel>.Failure(["Balans yetarli emas"]);

            // Balansdan yechish
            user.Balance -= book.Price;

            // Nusxani kamaytirish
            book.AvailableCopies--;

            // Tranzaksiya yaratish
            var transaction = new Transaction
            {
                UserId = userId,
                BookId = bookId,
                Type = TransactionType.Purchase,
                Date = DateTime.Now
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            return ApiResult<CreateTransactionResponseModel>.Success(
                new CreateTransactionResponseModel { Id = transaction.Id });
        }
    }
}
