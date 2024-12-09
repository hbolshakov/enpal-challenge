namespace Enpal.HomeChallenge.Infrastructure.Dtos;

public sealed record CalendarSlotDto
{
    public int AvailableCount { get; set; }
    public DateTime StartDate { get; set; }
}