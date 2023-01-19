using MockBookingSystem.Core;
using MockBookingSystem.Core.Enums;
using MockBookingSystem.Entities;
using MockBookingSystem.Objects.Destination;
using MockBookingSystem.Objects.DestinationOption;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystem.ServiceLayer.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Helpers
{
    public interface IDestinationSearchHelper
    {
        Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model);
        BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel);
    }

    //I know there is a code duplication between HotelOnly and LastMinuteSearch now, but it is done like that because of the req.
    //Probably LastMinuteSearch will need to be extended in the future
    public class HotelOnlySearch : IDestinationSearchHelper
    {
        private readonly ITripxClient _tripxClient;
        private readonly IDAL _dal;

        public HotelOnlySearch(ITripxClient tripxClient, IDAL dal)
        {
            _tripxClient = tripxClient;
            _dal = dal;
        }

        public BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel)
        {
            return BookingStatus.Success;
        }

        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var hotels = await _tripxClient.GetHotelsAsync(model.Destination);

            List<DestinationOptionDTO> result = new();

            foreach (var hotel in hotels)
            {
                if (hotel.HotelCode == null) continue;

                DestinationOption destinationOptions = new()
                {
                    HotelCode = hotel.HotelCode.Value,
                    Price = RandomGenerator.GenerateInt(400, 4000)
                };

                _dal.AddOrUpdate(destinationOptions);

                //Could have used automapper. But did not include in project. 
                result.Add(new()
                {
                    OptionCode = destinationOptions.Code,
                    HotelCode = destinationOptions.HotelCode,
                    Price = destinationOptions.Price
                });
            }

            return result;
        }
    }

    public class LastMinuteSearch : IDestinationSearchHelper
    {
        private readonly ITripxClient _tripxClient;
        private readonly IDAL _dal;

        public LastMinuteSearch(ITripxClient tripxClient, IDAL dal)
        {
            _tripxClient = tripxClient;
            _dal = dal;
        }


        public BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel)
        {
            return BookingStatus.Failed;
        }

        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var hotels = await _tripxClient.GetHotelsAsync(model.Destination);

            List<DestinationOptionDTO> result = new();

            foreach (var hotel in hotels)
            {
                if (hotel.HotelCode == null) continue;

                DestinationOption destinationOptions = new()
                {
                    HotelCode = hotel.HotelCode.Value,
                    Price = RandomGenerator.GenerateInt(400, 4000)
                };

                _dal.AddOrUpdate(destinationOptions);

                result.Add(new()
                {
                    OptionCode = destinationOptions.Code,
                    HotelCode = destinationOptions.HotelCode,
                    Price = destinationOptions.Price
                });
            }

            return result;
        }
    }

    public class HotelAndFlightSearch : IDestinationSearchHelper
    {
        private readonly ITripxClient _tripxClient;
        private readonly IDAL _dal;

        public HotelAndFlightSearch(ITripxClient tripxClient, IDAL dal)
        {
            _tripxClient = tripxClient;
            _dal = dal;
        }

        public BookingStatus GetBookingStatusBasedOnSearch(SearchModel searchModel)
        {
            return BookingStatus.Success;
        }

        public async Task<List<DestinationOptionDTO>> SearchAsync(SearchModel model)
        {
            var hotels = await _tripxClient.GetHotelsAsync(model.Destination);
            var airports = await _tripxClient.GetAirportsAsync(model.DepartureAirport, model.Destination);

            List<DestinationOptionDTO> result = new();

            foreach (var hotel in hotels)
            {
                if (hotel.HotelCode == null)
                    continue;

                foreach (var airport in airports)
                {
                    DestinationOption destinationOptions = new()
                    {
                        HotelCode = hotel.HotelCode.Value,
                        DepartureAirport = airport.DepartureAirport,
                        ArrivalAirport = airport.ArrivalAirport,
                        FlightCode = airport.FlightCode,
                        Price = RandomGenerator.GenerateInt(400, 4000)
                    };

                    _dal.AddOrUpdate(destinationOptions);

                    //Could have used automapper. But did not include in project. 
                    result.Add(new()
                    {
                        OptionCode = destinationOptions.Code,
                        HotelCode = destinationOptions.HotelCode,
                        Price = destinationOptions.Price,
                        ArrivalAirport = destinationOptions.ArrivalAirport,
                        FlightCode = destinationOptions.FlightCode,
                    });
                }
            }

            return result;
        }
    }
}
