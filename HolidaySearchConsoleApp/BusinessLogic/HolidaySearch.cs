using HolidaySearchConsoleApp.Domain;

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
        throw new NotImplementedException();
    }

    private List<Flight> LoadFlights()
    {
        throw new NotImplementedException();
    }
}