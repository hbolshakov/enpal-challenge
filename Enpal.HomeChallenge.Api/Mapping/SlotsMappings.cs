using Enpal.HomeChallenge.Contracts.ViewModels.Calendar;
using Enpal.HomeChallenge.Core.Calendar.Models;

namespace Enpal.HomeChallenge.Api.Mapping;

public static class SlotsMappings
{
    public static CalendarSlotViewModel MapToViewModel(this CalendarSlot model)
    {
        return new CalendarSlotViewModel(model.AvailableCount, DateTime.SpecifyKind(model.StartDate, DateTimeKind.Utc));
    }
}