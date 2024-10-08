using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using astro_backend.models;

namespace astro_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PurchasingController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Get AST token price for the logged-in user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        // Get AST token price for the logged-in user
        [HttpGet("astro-price")]
        public async Task<IActionResult> GetAstroPrice(string email)
        {
            var user = await _context.Users
                                    .Include(u => u.Account)
                                    .SingleOrDefaultAsync(u => u.email == email);

            if (user == null) return NotFound("User not found");
            if (user.Account == null) return NotFound("Account not found for the user");

            var astro = await _context.Astros.SingleOrDefaultAsync(a => a.account_id == user.Account.account_id);

            if (astro == null) return NotFound("Astro record not found");

            return Ok(new { astro.price, astro.tokens });
        }

        // Calculate transaction fee based on user's account status
        [HttpGet("transaction-fee")]
        public async Task<IActionResult> GetTransactionFee(string email)
        {
            var user = await _context.Users
                                    .Include(u => u.Account)
                                    .ThenInclude(a => a.Astro) // Ensure Astro is loaded with Account
                                    .SingleOrDefaultAsync(u => u.email == email);

            if (user == null) return NotFound("User not found");
            if (user.Account == null) return NotFound("Account not found");

            var status = await _context.Statuss
                                    .SingleOrDefaultAsync(s => s.status_id == user.Account.account_status_id);

            if (status == null) return NotFound("Status not found");
            if (user.Account.Astro == null) return NotFound("Astro record not found");

            decimal fee = user.Account.Astro.tokens == 0 ? 0 : status.transaction_fee;

            return Ok(new { fee });
        }


        // Confirm transaction and update the user's Astro record
        [HttpPost("confirm-transaction")]
        public async Task<IActionResult> ConfirmTransaction([FromBody] ConfirmTransactionRequest request)
        {
            var user = await _context.Users
                                    .Include(u => u.Account)
                                    .ThenInclude(a => a.Astro)
                                    .SingleOrDefaultAsync(u => u.email == request.Email);

            if (user == null) return NotFound("User not found");
            if (user.Account == null) return NotFound("Account not found");

            var astro = user.Account.Astro;
            if (astro == null) return NotFound("Astro record not found");

            // Calculate the transaction fee based on the user's account status
            var status = await _context.Statuss.SingleOrDefaultAsync(s => s.status_id == user.Account.account_status_id);
            if (status == null) return NotFound("Status not found");

            // If the user has 0 tokens, set the transaction fee to 0
            decimal transactionFee = astro.tokens == 0 ? 0 : status.transaction_fee;

            // Check if the user has enough tokens to cover the transaction and fee
            if (astro.tokens == 0 && request.TokensPurchased < transactionFee)
            {
                return BadRequest("Insufficient tokens to cover the transaction fee.");
            }
            else if (astro.tokens != 0 && astro.tokens < request.TokensPurchased + transactionFee)
            {
                return BadRequest("Insufficient tokens to cover the transaction and fee.");
            }

            // Update the tokens based on the transaction and fee
            if (astro.tokens == 0)
            {
                astro.tokens += request.TokensPurchased;
            }
            else
            {
                astro.tokens += request.TokensPurchased - transactionFee;
            }

            // Calculate the amount for the transaction
            decimal tokenPrice = 80000;
            decimal amount = request.TokensPurchased * tokenPrice;

            // Find the current max transaction_id
            int maxTransactionId = await _context.Transactions.MaxAsync(t => t.transaction_id);

            // Add a new transaction record
            var transaction = new Transaction
            {
                transaction_id = maxTransactionId + 1,
                transaction_type = "Deposit",
                amount = amount,
                timestamp = DateTime.UtcNow,
                from_account_id = user.Account.account_id,
                to_account_id = user.Account.account_id
            };

            _context.Transactions.Add(transaction);

            // Update the user's account balance
            user.Account.balance = astro.tokens * tokenPrice;

            await _context.SaveChangesAsync();

            // Return the updated Astro tokens to the frontend
            return Ok(new { message = "Transaction confirmed and Astro record updated", updatedTokens = astro.tokens });
        }

        [HttpPost("withdraw-astro-tokens")]
        public async Task<IActionResult> WithdrawAstroTokens([FromBody] WithdrawAstroTokensRequest request)
        {
            var user = await _context.Users
                                    .Include(u => u.Account)
                                    .ThenInclude(a => a.Astro)
                                    .SingleOrDefaultAsync(u => u.email == request.Email);

            if (user == null) return NotFound("User not found");
            if (user.Account == null) return NotFound("Account not found");

            var astro = user.Account.Astro;
            if (astro == null) return NotFound("Astro record not found");

            // Calculate the transaction fee based on the user's account status
            var status = await _context.Statuss.SingleOrDefaultAsync(s => s.status_id == user.Account.account_status_id);
            if (status == null) return NotFound("Status not found");

            // Calculate the total tokens required (tokens to withdraw + transaction fee)
            decimal transactionFee = status.transaction_fee;
            decimal totalTokensRequired = request.TokensToWithdraw + transactionFee;

            // Check if the user has enough tokens to cover the withdrawal and the fee
            if (astro.tokens < totalTokensRequired)
            {
                return BadRequest("Insufficient tokens to cover the withdrawal and transaction fee.");
            }

            // Deduct the tokens (including the fee) from the user's account
            astro.tokens -= totalTokensRequired;

            // Calculate the withdrawal amount in currency
            decimal tokenPrice = 80000;
            decimal withdrawalAmount = request.TokensToWithdraw * tokenPrice;

            // Find the current max transaction_id
            int maxTransactionId = await _context.Transactions.MaxAsync(t => t.transaction_id);

            // Add a new transaction record for the withdrawal
            var transaction = new Transaction
            {
                transaction_id = maxTransactionId + 1,
                transaction_type = "Withdrawal",
                amount = withdrawalAmount,
                timestamp = DateTime.UtcNow,
                from_account_id = user.Account.account_id,
                to_account_id = user.Account.account_id
            };

            _context.Transactions.Add(transaction);

            // Update the user's account balance
            user.Account.balance = astro.tokens * tokenPrice;

            await _context.SaveChangesAsync();

            // Return the updated Astro tokens to the frontend
            return Ok(new { message = "Withdrawal successful and Astro record updated", updatedTokens = astro.tokens });
        }



        public class ConfirmTransactionRequest
        {
            public string Email { get; set; }
            public decimal TokensPurchased { get; set; }
        }

        public class WithdrawAstroTokensRequest
        {
            public string Email { get; set; }
            public decimal TokensToWithdraw { get; set; }
        }


        [HttpGet("available-users")]
        public async Task<IActionResult> GetAvailableUsers(string email)
        {
            var users = await _context.Users
                                    .Where(u => u.email != email)
                                    .Select(u => new { u.username, u.email })
                                    .ToListAsync();

            if (users == null || !users.Any())
            {
                return NotFound("No other users found.");
            }

            return Ok(users);
        }

        [HttpPost("make-payment")]
        public async Task<IActionResult> MakePayment([FromBody] MakePaymentRequest request)
        {
            // Get the sender's account
            var sender = await _context.Users
                                    .Include(u => u.Account)
                                    .ThenInclude(a => a.Astro)
                                    .SingleOrDefaultAsync(u => u.email == request.SenderEmail);

            if (sender == null || sender.Account == null || sender.Account.Astro == null)
            {
                return NotFound("Sender not found or account information is missing.");
            }

            // Get the recipient's account
            var recipient = await _context.Users
                                    .Include(u => u.Account)
                                    .ThenInclude(a => a.Astro)
                                    .SingleOrDefaultAsync(u => u.email == request.RecipientEmail);

            if (recipient == null || recipient.Account == null || recipient.Account.Astro == null)
            {
                return NotFound("Recipient not found or account information is missing.");
            }

            // Calculate the transaction fee based on the sender's account status
            var status = await _context.Statuss.SingleOrDefaultAsync(s => s.status_id == sender.Account.account_status_id);
            if (status == null)
            {
                return NotFound("Sender's account status not found.");
            }

            decimal transactionFee = status.transaction_fee;
            decimal totalTokensRequired = request.Tokens + transactionFee;

            // Check if the sender has enough tokens
            if (sender.Account.Astro.tokens < totalTokensRequired)
            {
                return BadRequest("Insufficient tokens to cover the payment and transaction fee.");
            }

            // Deduct tokens from the sender
            sender.Account.Astro.tokens -= totalTokensRequired;

            // Add tokens to the recipient
            recipient.Account.Astro.tokens += request.Tokens;

            // Update account balances
            decimal tokenPrice = 80000;
            sender.Account.balance = sender.Account.Astro.tokens * tokenPrice;
            recipient.Account.balance = recipient.Account.Astro.tokens * tokenPrice;

            // Record the transaction
            int maxTransactionId = await _context.Transactions.MaxAsync(t => t.transaction_id);
            var transaction = new Transaction
            {
                transaction_id = maxTransactionId + 1,
                transaction_type = "Transaction",
                amount = request.Tokens * tokenPrice,
                timestamp = DateTime.UtcNow,
                from_account_id = sender.Account.account_id,
                to_account_id = recipient.Account.account_id
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Return the updated Astro tokens for the sender
            return Ok(new { message = "Payment successful", updatedTokens = sender.Account.Astro.tokens });
        }

        public class MakePaymentRequest
        {
            public string SenderEmail { get; set; }
            public string RecipientEmail { get; set; }
            public decimal Tokens { get; set; }
        }


    }
}
