using FAD.Models;

namespace FAD.Domain.Services
{
    public interface IFlightService
    {
        bool FindFlight(Flight flight);
        bool FindAirport(string iata);
        Flight AddFlight(Flight flight);
    }
}
