using Enpal.HomeChallenge.Core.Calendar.Models;

namespace Enpal.HomeChallenge.Core.Calendar;

public interface ICalendarService
{
    public Task<List<CalendarSlot>> GetAvailableSlotsAsync(
        DateOnly date,
        IEnumerable<string> products,
        IEnumerable<string> languages,
        IEnumerable<string> ratings,
        CancellationToken ct);
}