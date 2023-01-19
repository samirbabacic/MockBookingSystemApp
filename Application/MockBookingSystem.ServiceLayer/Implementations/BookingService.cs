using MockBookingSystem.Core;
using MockBookingSystem.Core.Enums;
using MockBookingSystem.Entities;
using MockBookingSystem.Objects.Booking;
using MockBookingSystem.Objects.Destination;
using MockBookingSystem.ServiceLayer.Contracts;
using MockBookingSystem.ServiceLayer.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockBookingSystem.ServiceLayer.Implementations
{
    public sealed class BookingService : IBookingService
    {
        private readonly IDestinationSearchHelperFactory _destinationSearchHelperFactory;
        private readonly IDAL _dal;

        public BookingService(IDestinationSearchHelperFactory destinationSearchHelperFactory, IDAL dal)
        {
            _destinationSearchHelperFactory = destinationSearchHelperFactory;
            _dal = dal;
        }

        public async Task<BookResponseDTO> BookAsync(string optionCode, SearchModel searchModel)
        {
            var booking = new Booking { Type = Core.Enums.BookingType.HotelOnly };
            _dal.Insert(booking);

            //This could have be done with hangfire. That would be the best way
            _ = Task.Run(async () =>
            {
                await Task.Delay(booking.SleepTime.Value * 1000);

                var destinationSearchHelper = _destinationSearchHelperFactory.GetDestinationSearch(searchModel);

                booking.Status = destinationSearchHelper.GetBookingStatusBasedOnSearch(searchModel);

                _dal.Replace(booking);
            });

            return new BookResponseDTO
            {
                BookingCode = booking.Code,
                BookingStatus= booking.Status,
            };
        }

        public async Task<BookingStatus> CheckStatusAsync(string bookingCode)
        {
            //I could have used search by id as well, as the code is being id. 
            //But decided not too, cause we can change the way we store ids in any second, and there it was required 
            //to get by bookingCode
            var booking = _dal.FirstOrDefault<Booking>(x => x.Code == bookingCode);

            return booking.Status;
        }
    }
}
