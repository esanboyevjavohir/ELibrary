using ELibrary.Business.Helpers.GenerateJWT;
using ELibrary.Business.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ELibrary.API.Controllers
{
    [Authorize]
    public class TransactionController : ApiController
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("buy/{bookId}")]
        public async Task<IActionResult> Buy(Guid bookId)
        {
            var userId = Guid.Parse(User.FindFirst(CustomClaimNames.Id)!.Value);
            var result = await _transactionService.BuyAsync(bookId, userId);
            if (!result.Succedded)
                return BadRequest(result.Errors);
            return Ok(result.Result);
        }
    }
}
