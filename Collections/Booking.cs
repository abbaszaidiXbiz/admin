using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Collections;


public class INTERMEDIATEFLIGHT
{
    public string flightNumber { get; set; }
    public string departureAirport { get; set; }
    public string departureDateTime { get; set; }
    public string arrivalAirport { get; set; }
    public string arrivalDateTime { get; set; }
    public int totalDuration { get; set; }
    public int seatCostPerPassenger { get; set; }
}

public class Journey
{
    public string departureDateTime { get; set; }
    public StartAirport startAirport { get; set; }
    public string arrivalDateTime { get; set; }
    public EndAirport endAirport { get; set; }
    public int lengthTime { get; set; }
}


public class Booking
{
    public string _id { get; set; }
    public string bookingCode { get; set; }
    public string bookingDateTime { get; set; }
    public int numberOfPassengers { get; set; }
    public int cost { get; set; }
    public Journey journey { get; set; }
    public List<Passenger> passengers { get; set; }
    public List<INTERMEDIATEFLIGHT> INTERMEDIATE_FLIGHTS { get; set; }
}



