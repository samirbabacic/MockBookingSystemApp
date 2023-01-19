using MockBookingSystem.Objects.Destination;
using MockBookingSystem.Objects.DestinationOption;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystem.ServiceLayer.Factories;
using MockBookingSystem.ServiceLayer.Helpers;
using MockBookingSystem.ServiceLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Implementations
{
    public sealed class DestinationOptionService : IDestinationOptionService
    {
        private readonly IDestinationSearchHelperFactory _destinationSearchHelperFactory;

        public DestinationOptionService(IDestinationSearchHelperFactory destinationSearchHelperFactory)
        {
            _destinationSearchHelperFactory = destinationSearchHelperFactory;
        }

        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var destinationSearch = _destinationSearchHelperFactory.GetDestinationSearch(model);

            var destinations = await destinationSearch.SearchAsync(model);

            return new(destinations);
        }
    }
}
