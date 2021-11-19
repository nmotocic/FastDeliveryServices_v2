using FAD.Models;

namespace FAD.Domain.Repository
{
    public interface IDeliveryAirportRepository
    {
        Airport GetAirport(string iata);

        bool FindAirport(string iata);
    }
}
