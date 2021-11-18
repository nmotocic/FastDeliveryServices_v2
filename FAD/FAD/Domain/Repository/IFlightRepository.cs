using FAD.Models;

namespace FAD.Domain.Repository
{
    public interface IFlightRepository
    {
        List<Flight> GetAll();
        Flight GetByIata(string iata);

        bool FindFlight(Flight flight);

        bool FindAirport(string iata);
        Flight AddFlight(Flight flight);

        void DeleteFlight(Flight flight);
    }
}
