using MockBookingSystem.Objects.Destination;
using MockBookingSystem.Objects.DestinationOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Contracts
{
    public interface IDestinationOptionService
    {
        public Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model);
    }
}
