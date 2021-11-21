using FAD.Domain.Repository;
using FAD.Domain.Services;
using FAD.Models;
using FAD.Repository;

namespace FAD.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _repository;
        private readonly IDeliveryAirportRepository _airportRepository;
        public DeliveryService(){
            _repository = new DeliveryRepository();
            _airportRepository = new DeliveryAirportRepository();
        }

        public bool FindAirport(string iata)
        {
            return _airportRepository.FindAirport(iata);
        }

        public Airport GetAirport(string iata) {
            return _airportRepository.GetAirport(iata);
        }

        public Flight GetFlight(string iataFrom, string iataTo)
        {
            return _repository.GetByIata(iataFrom, iataTo);
        }
        public bool FindFlight(string from, string to)
        {
            return _repository.FindFlight(from, to);
        }

    }
}
