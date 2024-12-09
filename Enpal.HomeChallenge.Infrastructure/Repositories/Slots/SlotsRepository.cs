using Dapper;
using Enpal.HomeChallenge.Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;

namespace Enpal.HomeChallenge.Infrastructure.Repositories.Slots;

public class SlotsRepository : BaseRepository, ISlotsRepository
{
    public SlotsRepository(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<List<CalendarSlotDto>> GetSlotsAsync(
        DateOnly date,
        IEnumerable<string> products,
        IEnumerable<string> languages,
        IEnumerable<string> ratings,
        CancellationToken ct)
    {
        using var connection = Connection;
        connection.Open();

        var query = """
                    SELECT s.start_date AS StartDate, COUNT(s.id) AS AvailableCount 
                    FROM slots s
                    LEFT JOIN sales_managers sm ON s.sales_manager_id = sm.id
                    WHERE s.booked = false AND 
                          s.start_date::TIMESTAMP::DATE = @Date::DATE AND
                          sm.products @> ARRAY [@Products]::varchar[] AND
                          sm.languages @> ARRAY[@Languages]::varchar[] AND
                          sm.customer_ratings @> ARRAY[@Ratings]::varchar[] AND
                          (SELECT COUNT(id) 
                            FROM slots
                            WHERE sales_manager_id = s.sales_manager_id AND
                            booked = true AND
                            ((end_date > s.start_date AND end_date <= s.end_date) OR (start_date >= s.start_date AND start_date < s.end_date)) AND 
                            id != s.id) = 0
                    GROUP BY s.start_date
                    ORDER BY s.start_date
                    """;

        var parameters = new DynamicParameters();
        parameters.Add("@Date", date.ToString("yyyy-MM-dd"));
        parameters.Add("@Products", products.ToArray());
        parameters.Add("@Languages", languages.ToArray());
        parameters.Add("@Ratings", ratings.ToArray());

        var slots = await connection.QueryAsync<CalendarSlotDto>(query, parameters);

        return slots.ToList();
    }
}