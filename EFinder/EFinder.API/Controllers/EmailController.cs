using EFinder.API.Requests;
using EFinder.API.Response;
using EFinder.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EFinder.API.Controllers;

[Route("api/email")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IFinderService _finderService;

    public EmailController(IFinderService finderService)
    {
        _finderService = finderService;
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
            var parcialResult = (EmailFindResponse)await _finderService
                .FindValidEmail(request.FirstName,
                    request.LastName,
                    domain);

            result.Emails = result.Emails.Concat(parcialResult.Emails).ToList();
            result.MailExchangeServers = result.MailExchangeServers.Concat(parcialResult.MailExchangeServers).ToList();
        }

        return result;
    }
}