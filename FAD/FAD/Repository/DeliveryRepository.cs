using FAD.Domain.Repository;
using FAD.Models;
using System.Data.SqlClient;

namespace FAD.Repository
{
    public class DeliveryRepository : BaseRepository<Flight>, IDeliveryRepository
    {
        private static SqlConnection _connection;
        public static IConfigurationRoot Configuration;
       
        public DeliveryRepository()
        {
            _connection = new SqlConnection(GetConnectionString());
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            var connectionString = Configuration["MyConnectionString:ConnectionString"];
            return connectionString;
        }
        public Flight GetByIata(string iataFrom, string iataTo)
        {
            using (var command = new SqlCommand("SELECT * FROM Flights WHERE IataFrom = @iataFrom AND IataTo = @iataTo"))
            {
                command.Parameters.Add(new SqlParameter("@iataFrom", iataFrom));
                command.Parameters.Add(new SqlParameter("@iataTo", iataTo));
                return GetRecord(command);
            }
        }

        public override Flight PopulateRecord(SqlDataReader reader)
        {
            return new Flight
            {
                From = reader.GetString(0),
                To = reader.GetString(1)
            };
        }

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

        public bool FindFlight(string from, string to)
        {
            using (var command = new SqlCommand("SELECT * FROM Flights WHERE IataFrom = @IATAFrom AND IataTo = @IATATo"))
            {
                command.Parameters.Add(new SqlParameter("@IATAFrom", from));
                command.Parameters.Add(new SqlParameter("@IATATo", to));

                var record = GetRecord(command);
                if (record == null)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
