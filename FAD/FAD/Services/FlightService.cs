using FAD.Domain.Repository;
using FAD.Domain.Services;
using FAD.Models;
using FAD.Repository;
using Microsoft.Extensions.Options;

namespace FAD.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _repository;

        public FlightService(IOptions<DataConnection> options)
        {
            var connection = options.Value;
            _repository = new FlightRepository(connection.ConnectionString);
        }

        public bool FindAirport(string iata) {
            return _repository.FindAirport(iata);
        }

        public bool FindFlight(Flight flight) {
            return _repository.FindFlight(flight);
        }

        public Flight AddFlight(Flight flight) {
            return _repository.AddFlight(flight);
        }
    }
}
