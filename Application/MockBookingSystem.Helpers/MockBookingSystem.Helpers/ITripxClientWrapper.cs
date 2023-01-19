using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Wrappers
{
    public interface ITripxClientWrapper
    {
        public Task<List<>> GetHotels(string destinationCode);
    }
    public sealed class TripxClientWrapper : ITripxClientWrapper
    {
        public Task<List<>> GetHotels(string destinationCode)
        {
            throw new NotImplementedException();
        }
    }
}
