using EFinder.API.Extensions;
using EFinder.API.Requests;
using EFinder.API.Response;
using EFinder.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EFinder.API.Controllers;

[Route("api/email")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IFinderService _finderService;
    private readonly IMemoryCache _cache;

    public EmailController(IFinderService finderService, IMemoryCache cache)
    {
        _finderService = finderService;
        _cache = cache;
    }

    [Route("find")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] EmailFindRequest request)
    {
        if (request.HasErrors()) return BadRequest(request.Errors);
        var result = await FindValidEmail(request);
        return Ok(result);
    }

    private async Task<EmailFindResponse> FindValidEmail(EmailFindRequest request)
    {
        var result = new EmailFindResponse();

        foreach (var domain in request.Domains)
        {
            if (_cache.TryGetValue(request.GetKeyForCache(domain), out EmailFindResponse parcialResult))
            {
                result.Emails = result.Emails.Concat(parcialResult.Emails).ToList();
                result.MailExchangeServers = result.MailExchangeServers
                                                   .Concat(parcialResult.MailExchangeServers)
                                                   .ToList();
                continue;
            }

            parcialResult = (EmailFindResponse)await _finderService.FindValidEmail(
                                                                    request.FirstName, 
                                                                    request.LastName, 
                                                                    domain);

            result.Emails = result.Emails.Concat(parcialResult.Emails).ToList();
            result.MailExchangeServers = result.MailExchangeServers
                                               .Concat(parcialResult.MailExchangeServers)
                                               .ToList();

            SetCache(request.GetKeyForCache(domain), result);
        }

        return result;
    }

    private void SetCache(string key, EmailFindResponse result)
    {
        var memoryCacheOptions = new MemoryCacheEntryOptions()
        {
            SlidingExpiration = TimeSpan.FromDays(1)
        };
        
        _cache.Set(key, result, memoryCacheOptions);
    }
}