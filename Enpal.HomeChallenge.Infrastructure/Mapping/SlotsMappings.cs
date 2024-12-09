using Enpal.HomeChallenge.Core.Calendar.Models;
using Enpal.HomeChallenge.Infrastructure.Dtos;

namespace Enpal.HomeChallenge.Infrastructure.Mapping;

public static class SlotsMappings
{
    public static CalendarSlot MapToCoreModel(this CalendarSlotDto dto)
    {
        return new CalendarSlot(dto.AvailableCount, dto.StartDate);
    }
}