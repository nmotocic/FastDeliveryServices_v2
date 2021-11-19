using FAD.Models;

namespace FAD.Domain.Services
{
    public interface IDeliveryService
    {
        Flight GetFlight(string iataFrom, string iataTo);

        bool FindAirport(string iata);

        Airport GetAirport(string iata);
    }
}
