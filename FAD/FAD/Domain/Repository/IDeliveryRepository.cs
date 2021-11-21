using FAD.Models;

namespace FAD.Domain.Repository
{
    public interface IDeliveryRepository
    {

        Flight GetByIata(string iataFrom, string iataTo);
        bool FindFlight(string from, string to);
    }
}
