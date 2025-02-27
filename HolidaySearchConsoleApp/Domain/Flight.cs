namespace HolidaySearchConsoleApp.Domain;

public class Flight
{
    public int Id { get; set; }
    public string Airline { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public decimal Price { get; set; }
    public DateTime DepartureDate { get; set; }
}