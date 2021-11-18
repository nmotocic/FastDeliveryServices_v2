using FAD.Domain.Repository;
using FAD.Models;

namespace FAD.Repository
{
    public class DeliveryRepository : BaseRepository<Flight>, IDeliveryRepository
    {
        public DeliveryRepository(string connectionString) : base(connectionString)
        {
        }


    }
}
