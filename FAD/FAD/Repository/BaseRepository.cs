using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace FAD.Repository
{
    public abstract class BaseRepository<T> where T : class
    {
        public static SqlConnection _connection;
        public static IConfigurationRoot Configuration;

        public BaseRepository()
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

        public virtual T PopulateRecord(SqlDataReader reader)
        {
            return null;
        }

        protected IEnumerable<T> GetRecords(SqlCommand command)
        {
            var list = new List<T>();
            command.Connection = _connection;
            _connection.Open();
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

            return list;
        }

        protected T GetRecord(SqlCommand command)
        {

            T record = null;
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

        protected void ExecuteRecord(SqlCommand command) {
            command.Connection = _connection;
            _connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            finally {
                _connection.Close();
            }
        }

    }
}

