using FAD.Domain.Repository;
using FAD.Models;
using System.Data.SqlClient;


namespace FAD.Repository
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        public FlightRepository()
        {
            
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

        public Flight AddFlight(Flight flight) {
            using (var command = new SqlCommand("INSERT INTO Flights (IATAFrom, IATATo) VALUES (@IATAFrom, @IATATo)"))
            {
                command.Parameters.Add(new SqlParameter("@IATAFrom", flight.From));
                command.Parameters.Add(new SqlParameter("@IATATo", flight.To));

                ExecuteRecord(command);

            }
            return flight;
        }

        public void DeleteFlight(Flight flight) {
            using (var command = new SqlCommand("DELETE FROM Flights WHERE WHERE IataFrom = @IATAFrom AND IataTo = @IATATo")) {
                command.Parameters.Add(new SqlParameter("@IATAFrom", flight.From));
                command.Parameters.Add(new SqlParameter("@IATATo", flight.To));

                ExecuteRecord(command);
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

        
    } 
}

