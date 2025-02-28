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
    
    [Fact]
    public void HolidaySearch_GivenCustomerCriteria3_ReturnFlight7AndHotel6()
    {
        var criteria = new HolidaySearchCriteria
        {
            DepartingFrom = "Any Airport",
            TravelingTo = "LPA",
            DepartureDate = new DateTime(2022, 11, 10),
            Duration = 14
        };
        var search = new HolidaySearch(criteria);
        
        var result = search.BestDeal;

        Assert.NotNull(result);
        Assert.Equal(7, result.First().Flight.Id);
        Assert.Equal(6, result.First().Hotel.Id);
    }
    
    [Fact]

    public void HolidaySearch_GivenInvalidCriteria_ReturnsNull()
    {
        var criteria = new HolidaySearchCriteria
        {
            DepartingFrom = "XYZ",
            TravelingTo = "T",
            DepartureDate = new DateTime(2023, 7, 1),
            Duration = 7
        };
        var search = new HolidaySearch(criteria);
        
        var result = search.BestDeal;

        Assert.Empty(result);
    }
    
    [Fact]
    public void HolidaySearch_Results_AreOrderedByTotalPrice()
    {

        var criteria = new HolidaySearchCriteria
        {
            DepartingFrom = "Any London Airport",
            TravelingTo = "PMI",
            DepartureDate = new DateTime(2023, 6, 15),
            Duration = 10
        };

        // Act: Execute the holiday search.
        var search = new HolidaySearch(criteria);
        var results = search.BestDeal;

        Assert.NotNull(results);
        Assert.True(results.Count > 1, "There should be multiple packages for ordering by Total Price.");
        for (int i = 1; i < results.Count; i++)
        {
            Assert.True(results[i].TotalPrice >= results[i - 1].TotalPrice,
                $"Package at index {i} should have a total price greater than or equal to compared to index {i - 1}");
        }
    }

}