using HolidaySearchConsoleApp.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HolidaySearchConsoleApp.BusinessLogic;

public class HolidaySearch
{
    private readonly List<Flight> flights;
    private readonly List<Hotel> hotels;
    public List<HolidayPackage> BestDeal { get; private set; }
    
    public HolidaySearch(HolidaySearchCriteria searchCriteria)
    {
        flights = LoadFlights();
        hotels = LoadHotels();
        BestDeal = SearchBestDeal(searchCriteria);
    }

    private List<HolidayPackage> SearchBestDeal(HolidaySearchCriteria criteria)
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

        var matchingFlights = flights.Where(f =>
            departingAirports.Contains(f.From, StringComparer.OrdinalIgnoreCase) &&
            f.To.Equals(criteria.TravelingTo, StringComparison.OrdinalIgnoreCase) &&
            f.DepartureDate == criteria.DepartureDate
        ).ToList();

        var matchingHotels = hotels.Where(h =>
            h.ArrivalDate == criteria.DepartureDate &&
            h.Nights == criteria.Duration &&
            h.LocalAirports.Any(a => a.Equals(criteria.TravelingTo, StringComparison.OrdinalIgnoreCase))
        ).ToList();

        var packages = (from flight in matchingFlights
                from hotel in matchingHotels
                select new HolidayPackage { Flight = flight, Hotel = hotel })
            .ToList();

        var orderedPackages = packages.OrderBy(p => p.TotalPrice).ToList();

        return orderedPackages;
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

}