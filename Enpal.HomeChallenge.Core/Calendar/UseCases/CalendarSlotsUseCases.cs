using Enpal.HomeChallenge.Core.Calendar.Models;
using Enpal.HomeChallenge.Core.Calendar.Queries;
using MediatR;

namespace Enpal.HomeChallenge.Core.Calendar.UseCases;

public sealed class CalendarSlotsUseCases : IRequestHandler<GetAvailableSlotsQuery, List<CalendarSlot>>
{
    private readonly ICalendarService _calendarService;

    public CalendarSlotsUseCases(ICalendarService calendarService) 
        => _calendarService = calendarService;

    public Task<List<CalendarSlot>> Handle(GetAvailableSlotsQuery request, CancellationToken ct) 
        => _calendarService.GetAvailableSlotsAsync(
            request.Date,
            request.Products,
            request.Languages,
            request.Ratings,
            ct);
}