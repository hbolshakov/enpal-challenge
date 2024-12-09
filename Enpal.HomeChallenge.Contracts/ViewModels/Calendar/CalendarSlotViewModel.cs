namespace Enpal.HomeChallenge.Contracts.ViewModels.Calendar;

public sealed record CalendarSlotViewModel
{
    public int AvailableCount { get; set; }
    public DateTime StartDate { get; set; }
    
    public CalendarSlotViewModel(int availableCount, DateTime startDate)
    {
        AvailableCount = availableCount;
        StartDate = startDate;
    }
}