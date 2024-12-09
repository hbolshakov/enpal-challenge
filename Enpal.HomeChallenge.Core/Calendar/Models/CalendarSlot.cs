namespace Enpal.HomeChallenge.Core.Calendar.Models;

public sealed record CalendarSlot
{
    public int AvailableCount { get; set; }
    public DateTime StartDate { get; set; }
    
    public CalendarSlot(int availableCount, DateTime startDate)
    {
        AvailableCount = availableCount;
        StartDate = startDate;
    }
}