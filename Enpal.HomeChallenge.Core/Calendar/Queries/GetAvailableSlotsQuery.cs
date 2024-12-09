using Enpal.HomeChallenge.Core.Calendar.Models;
using MediatR;

namespace Enpal.HomeChallenge.Core.Calendar.Queries;

public sealed record GetAvailableSlotsQuery : IRequest<List<CalendarSlot>>
{
    public DateOnly Date { get; }
    public IEnumerable<string> Products { get; }
    public IEnumerable<string> Languages { get; }
    public IEnumerable<string> Ratings { get; }

    public GetAvailableSlotsQuery(
        DateOnly date,
        IEnumerable<string> products,
        IEnumerable<string> languages,
        IEnumerable<string> ratings)
    {
        Date = date;
        Products = products;
        Languages = languages;
        Ratings = ratings;
    }
}