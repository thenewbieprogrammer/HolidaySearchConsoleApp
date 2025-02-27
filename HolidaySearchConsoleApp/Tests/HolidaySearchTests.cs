using HolidaySearchConsoleApp.BusinessLogic;
using HolidaySearchConsoleApp.Domain;
using Xunit;

namespace HolidaySearchConsoleApp.Tests;

public class HolidaySearchTests
{
    [Fact]
    public void HolidaySearch_GivenCustomerCriteria1_ReturnFlight2AndHotel9()
    {
        var criteria = new HolidaySearchCriteria
        {
            DepartingFrom = "MAN",
            TravelingTo = "AGP",
            DepartureDate = new DateTime(2023, 7, 1),
            Duration = 7
        };
        var search = new HolidaySearch(criteria);
        
        var result = search.BestDeal;

        Assert.NotNull(result);
        Assert.Equal(2, result.First().Flight.Id);
        Assert.Equal(9, result.First().Hotel.Id);
    }
    
    [Fact]
    public void HolidaySearch_GivenCustomerCriteria2_ReturnFlight6AndHotel5()
    {
        var criteria = new HolidaySearchCriteria
        {
            DepartingFrom = "Any London Airport",
            TravelingTo = "PMI",
            DepartureDate = new DateTime(2023, 6, 15),
            Duration = 10
        };
        var search = new HolidaySearch(criteria);
        
        var result = search.BestDeal;

        Assert.NotNull(result);
        Assert.Equal(6, result.First().Flight.Id);
        Assert.Equal(5, result.First().Hotel.Id);
    }
}