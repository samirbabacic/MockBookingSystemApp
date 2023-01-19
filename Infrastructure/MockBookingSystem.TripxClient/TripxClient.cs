using MockBookingSystem.Objects.DestinationOption;
using MockBookingSystem.ServiceLayer.Wrappers;
using System.Net.Http.Json;

namespace MockBookingSystem.TripxClient
{
    public class TripxClient : ITripxClient
    {
        private readonly HttpClient _httpClient;
        public TripxClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DestinationAirportDTO>> GetAirportsAsync(string departureAirport, string arrivalAirport)
        {
            return
                await _httpClient.GetFromJsonAsync<List<DestinationAirportDTO>>($"SearchFlights?departureAirport={departureAirport}&arrivalAirport={arrivalAirport}");
        }

        public async Task<List<DestinationHotelDTO>> GetHotelsAsync(string destinationCode)
        {
            return
                await _httpClient.GetFromJsonAsync<List<DestinationHotelDTO>>($"SearchHotels?destinationCode={destinationCode}");
        }
    }
}