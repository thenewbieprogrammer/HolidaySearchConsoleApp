using HolidaySearchConsoleApp.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HolidaySearchConsoleApp.BusinessLogic;

public class HolidaySearch
{
    private readonly List<Flight> flights;
    private readonly List<Hotel> hotels;
    public HolidayPackage BestDeal { get; private set; }
    
    public HolidaySearch(HolidaySearchCriteria searchCriteria)
    {
        flights = LoadFlights();
        hotels = LoadHotels();
        BestDeal = SearchBestDeal(searchCriteria);
    }

    private HolidayPackage SearchBestDeal(HolidaySearchCriteria criteria)
    {
        List<string> departingAirports;

        if (criteria.DepartingFrom.Equals("Any Airport", StringComparison.OrdinalIgnoreCase))
        {
            departingAirports = flights.Select(f => f.From).Distinct().ToList();
        }
        else if (criteria.DepartingFrom.Equals("Any London Airport", StringComparison.OrdinalIgnoreCase))
        {
            departingAirports = new List<string> { "LGW", "LTN" };
        }
        else
        {
            departingAirports = new List<string> { criteria.DepartingFrom };
        }


        Flight bestFlight = null;

        foreach (var flight in flights)
        {
            if (departingAirports.Contains(flight.From, StringComparer.OrdinalIgnoreCase) &&
                flight.To.Equals(criteria.TravelingTo, StringComparison.OrdinalIgnoreCase) &&
                flight.DepartureDate == criteria.DepartureDate)
            {
                if (bestFlight == null || flight.Price < bestFlight.Price)
                {
                    bestFlight = flight;
                }
            }
        }

        Hotel bestHotel = null;
        foreach (var hotel in hotels)
        {
            if (hotel.ArrivalDate == criteria.DepartureDate && hotel.Nights == criteria.Duration &&
                hotel.LocalAirports.Any(a => a.Equals(criteria.TravelingTo, StringComparison.OrdinalIgnoreCase)))
            {
                var hotelCost = hotel.PricePerNight * hotel.Nights;
                if (bestHotel == null || hotelCost < bestHotel.PricePerNight * bestHotel.Nights)
                {
                    bestHotel = hotel;
                }
                
            }
        }

        if (bestFlight == null || bestHotel == null)
        {
            return null;
        }

        return new HolidayPackage { Flight = bestFlight, Hotel = bestHotel };    
    }

    private List<Hotel> LoadHotels()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Hotels.json");
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Hotels data file not found.", filePath);
        }

        string hotelsJson = File.ReadAllText(filePath);

        var jsonSerializeSettings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd",
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        return JsonConvert.DeserializeObject<List<Hotel>>(hotelsJson, jsonSerializeSettings);
    }

    private List<Flight> LoadFlights()
    {
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Flights.json");
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Flights data file not found.", filePath);
        }

        string flightsJson = File.ReadAllText(filePath);
        var jsonSerializeSettings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd",
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
        
        return JsonConvert.DeserializeObject<List<Flight>>(flightsJson, jsonSerializeSettings);
    }
}