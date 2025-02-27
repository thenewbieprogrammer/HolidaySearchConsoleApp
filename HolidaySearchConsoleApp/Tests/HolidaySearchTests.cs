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
        Assert.Equal(2, result.Flight.Id);
        Assert.Equal(9, result.Hotel.Id);

    }
}