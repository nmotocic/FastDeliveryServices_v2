using FAD.Domain.Repository;
using FAD.Models;
using System.Data.SqlClient;


namespace FAD.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private static SqlConnection _connection;
        public static IConfigurationRoot Configuration;

        public FlightRepository()
        {
            //string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
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

        public List<Flight> GetAll()
        {
            _connection.Open();
            using (var command = new SqlCommand("SELECT * FROM Flights"))
            {
                var list = new List<Flight>();
                command.Connection = _connection;
                
                try
                {
                    var reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            list.Add(PopulateRecord(reader));
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                finally
                {
                    _connection.Close();
                }

                
                return list.ToList();
            }

        }
        public Flight GetByIata(string iata)
        {
            using (var command = new SqlCommand("SELECT * FROM Flights WHERE IataFrom = @iata"))
            {
                command.Parameters.Add(new SqlParameter("@iata", iata));
                return GetRecord(command);
            }
        }

        

        //REFACTOR
        public bool FindFlight(Flight flight) {


            using (var command = new SqlCommand("SELECT * FROM Flights WHERE IataFrom = @IATAFrom AND IataTo = @IATATo"))
            {
                command.Parameters.Add(new SqlParameter("@IATAFrom", flight.From));
                command.Parameters.Add(new SqlParameter("@IATATo", flight.To));

                var record = GetRecord(command);
                if (record == null) {
                    return false;
                }

                return true;
            }
        }

        //REFACTOR
        public bool FindAirport(string iata) {
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

        //REFACTOR
        public Flight AddFlight(Flight flight) {
            using (var command = new SqlCommand("INSERT INTO Flights (IATAFrom, IATATo) VALUES (@IATAFrom, @IATATo)"))
            {
                command.Parameters.Add(new SqlParameter("@IATAFrom", flight.From));
                command.Parameters.Add(new SqlParameter("@IATATo", flight.To));

                AddRecord(command);

            }
            return flight;
        }

        private void AddRecord(SqlCommand command)
        {
            command.Connection = _connection;
            _connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteFlight(Flight flight) {
            using (var command = new SqlCommand("DELETE FROM Flights WHERE WHERE IataFrom = @IATAFrom AND IataTo = @IATATo")) {
                command.Parameters.Add(new SqlParameter("@IATAFrom", flight.From));
                command.Parameters.Add(new SqlParameter("@IATATo", flight.To));

                RemoveRecord(command);
            }
        }

        private void RemoveRecord(SqlCommand command)
        {
            command.Connection = _connection;
            _connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                _connection.Close();
            }
        }

        public Flight PopulateRecord(SqlDataReader reader)
        {
            return new Flight
            {
                From = reader.GetString(0),
                To = reader.GetString(1)
            };
        }

        private Flight GetRecord(SqlCommand command)
        {

           
                Flight record = null;
                command.Connection = _connection;
                _connection.Open();
                try
                {
                    var reader = command.ExecuteReader();
                    try
                    {
                        while (reader.Read())
                        {
                            record = PopulateRecord(reader);
                            break;
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
                finally
                {
                    _connection.Close();
                }

                return record;
            
            
        }
    } 
}

