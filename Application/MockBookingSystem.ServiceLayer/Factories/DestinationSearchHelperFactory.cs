using MockBookingSystem.Objects.Destination;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystem.ServiceLayer.Helpers;
using MockBookingSystem.ServiceLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Factories
{
    public interface IDestinationSearchHelperFactory
    {
        IDestinationSearchHelper GetDestinationSearch(SearchModel searchModel);
    }

    public sealed class DestinationSearchHelperFactory : IDestinationSearchHelperFactory
    {
        private readonly ITripxClient _tripxClient;
        private readonly IDAL _dal;

        public DestinationSearchHelperFactory(ITripxClient tripxClient, IDAL dal)
        {
            _tripxClient = tripxClient;
            _dal = dal;
        }

        public IDestinationSearchHelper GetDestinationSearch(SearchModel searchModel)
        {
            if (DateTime.Now.AddDays(45) >= searchModel.FromDate)
                return new LastMinuteSearch(_tripxClient, _dal);

            if (string.IsNullOrEmpty(searchModel.DepartureAirport))
                return new HotelOnlySearch(_tripxClient, _dal);

            return new HotelAndFlightSearch(_tripxClient, _dal);
        }
    }
}
