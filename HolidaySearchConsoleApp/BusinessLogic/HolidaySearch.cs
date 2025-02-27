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

    private HolidayPackage SearchBestDeal(HolidaySearchCriteria searchCriteria)
    {
        throw new NotImplementedException();
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