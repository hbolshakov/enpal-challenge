using Enpal.HomeChallenge.Api.Mapping;
using Enpal.HomeChallenge.Contracts.Requests.Calendar;
using Enpal.HomeChallenge.Contracts.ViewModels.Calendar;
using Enpal.HomeChallenge.Core.Calendar.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enpal.HomeChallenge.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class CalendarController : Controller
{
    private readonly ILogger<CalendarController> _logger;
    private readonly IMediator _mediator;

    public CalendarController(ILogger<CalendarController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("query")]
    public async Task<ActionResult<IEnumerable<CalendarSlotViewModel>>> GetAvailableSlotsAsync(
        [FromBody] GetAvailableSlotsRequest request,
        CancellationToken ct)
    {
        var query = new GetAvailableSlotsQuery(
            request.Date,
            request.Products,
            [request.Language],
            [request.Rating]
        );
        
        var slots = await _mediator.Send(query, ct);

        return Ok(slots.Select(x => x.MapToViewModel()).ToList());
    }
}