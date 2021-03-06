using System;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Aiia.Sample.Data;
using Aiia.Sample.Models;
using Aiia.Sample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aiia.Sample.Controllers
{
    [Route("aiia/accounts")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAiiaService _aiiaService;

        public AccountController(ApplicationDbContext dbContext, IAiiaService aiiaService)
        {
            _dbContext = dbContext;
            _aiiaService = aiiaService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Accounts()
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);

            var providers = await _aiiaService.GetProviders();
            providers = providers
                        .OrderBy(x => x.CountryCode)
                        .ThenBy(y => y.Name)
                        .ToImmutableList();

            // If user hasn't connected to Aiia or his access token is expired, show empty accounts page
            if (user?.AiiaAccessToken == null || user.AiiaAccessTokenExpires < DateTimeOffset.UtcNow)
            {
                return View(new AccountsViewModel
                            {
                                AccountsGroupedByProvider = null,
                                AiiaConnectUrl = _aiiaService.GetAuthUri(user?.Email).ToString(),
                                AiiaOneTimeConnectUrl = _aiiaService.GetAuthUri(null, true).ToString(),
                                EmailEnabled = user?.EmailEnabled ?? false,
                                Providers = providers,
                                Email = user?.Email,
                                ConsentId = user?.AiiaConsentId
                            });
            }

            var accounts = await _aiiaService.GetUserAccounts(User);
            var groupedAccounts = accounts.ToLookup(x => x.AccountProvider?.Id, x => x);
            var allAccountsSelected = await _aiiaService.AllAccountsSelected(User);
            var model = new AccountsViewModel
                        {
                            AccountsGroupedByProvider = groupedAccounts,
                            AiiaConnectUrl = _aiiaService.GetAuthUri(user.Email).ToString(),
                            AiiaOneTimeConnectUrl = _aiiaService.GetAuthUri(null, true).ToString(),
                            JwtToken = new JwtSecurityTokenHandler().ReadJwtToken(user.AiiaAccessToken),
                            RefreshToken = new JwtSecurityTokenHandler().ReadJwtToken(user.AiiaRefreshToken),
                            EmailEnabled = user.EmailEnabled,
                            Providers = providers,
                            ConsentId = user.AiiaConsentId,
                            Email = user.Email,
                            AllAccountsSelected = allAccountsSelected
                        };
            return View(model);
        }

        [HttpPost("{accountId}/transactions/query")]
        public async Task<IActionResult> FetchTransactions(string accountId,
                                                           [FromBody] TransactionQueryRequestViewModel body)
        {
            var transactions = await _aiiaService.GetAccountTransactions(User, accountId, body);
            return Ok(new TransactionsViewModel(transactions.Transactions,
                                                transactions.PagingToken,
                                                body.IncludeDeleted));
        }


        [HttpGet("{accountId}/transactions")]
        public async Task<IActionResult> Transactions(string accountId)
        {
            var transactions = await _aiiaService.GetAccountTransactions(User, accountId);
            return View(new TransactionsViewModel(transactions.Transactions, transactions.PagingToken, false));
        }
    }
}
