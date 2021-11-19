using FAD.Domain.Repository;
using FAD.Models;
using System.Data.SqlClient;

namespace FAD.Repository
{
    public class DeliveryAirportRepository : BaseRepository<Airport>, IDeliveryAirportRepository
    {

        public DeliveryAirportRepository() : base()
        {}

        public bool FindAirport(string iata)
        {
            using (var command = new SqlCommand("SELECT * FROM Airports WHERE IATA = @iata"))
            {
                command.Parameters.Add(new SqlParameter("@iata", iata));

                var record = GetRecord(command);
                if (record == null)
                {
                    return false;
                }

                return true;
            }
        }
    

        public Airport GetAirport(string iata)
        {
            using (var command = new SqlCommand("SELECT * FROM Airports WHERE IATA = @iata"))
            {
                command.Parameters.Add(new SqlParameter("@iata", iata));

                return GetRecord(command);
            }
        }
    }
}
