using Enpal.HomeChallenge.Core.Calendar;
using Enpal.HomeChallenge.Core.Calendar.Models;
using Enpal.HomeChallenge.Infrastructure.Dtos;
using Enpal.HomeChallenge.Infrastructure.Mapping;
using Enpal.HomeChallenge.Infrastructure.Repositories.Slots;

namespace Enpal.HomeChallenge.Infrastructure.Services;

public class CalendarService : ICalendarService
{
    private readonly ISlotsRepository _slotsRepository;

    public CalendarService(ISlotsRepository slotsRepository)
        => _slotsRepository = slotsRepository;

    public async Task<List<CalendarSlot>> GetAvailableSlotsAsync(
        DateOnly date,
        IEnumerable<string> products,
        IEnumerable<string> languages,
        IEnumerable<string> ratings,
        CancellationToken ct)
    {
        List<CalendarSlotDto> slots = await _slotsRepository.GetSlotsAsync(
            date,
            products,
            languages,
            ratings,
            ct);

        return slots.Select(x => x.MapToCoreModel()).ToList();
    }
}