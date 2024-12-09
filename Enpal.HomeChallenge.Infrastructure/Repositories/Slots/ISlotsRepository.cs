using Enpal.HomeChallenge.Infrastructure.Dtos;

namespace Enpal.HomeChallenge.Infrastructure.Repositories.Slots;

public interface ISlotsRepository
{
    Task<List<CalendarSlotDto>> GetSlotsAsync(
        DateOnly date,
        IEnumerable<string> products,
        IEnumerable<string> languages,
        IEnumerable<string> ratings,
        CancellationToken ct);
}