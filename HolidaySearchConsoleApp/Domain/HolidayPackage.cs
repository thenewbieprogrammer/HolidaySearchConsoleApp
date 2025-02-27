namespace HolidaySearchConsoleApp.Domain;

public class HolidayPackage
{
    public Flight Flight { get; set; }
    public Hotel Hotel { get; set; }
    public decimal TotalPrice => Flight.Price + (Hotel.PricePerNight * Hotel.Nights);
}