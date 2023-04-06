using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Collections;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class CabinCrew
{
    public string EmployeeID { get; set; }
    public string FullName { get; set; }
}

public class Crew
{
    public List<Pilot> Pilots { get; set; }
    public List<CabinCrew> CabinCrew { get; set; }
}

public class EndAirport
{
    public string Code { get; set; }
    public string Name { get; set; }
}

public class Pilot
{
    public string PilotLicenseNumber { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
}
public class Flight
{
    public string _id { get; set; }
    public string FlightNumber { get; set; }
    public StartAirport startAirport { get; set; }
    public EndAirport endAirport { get; set; }
    public Plane Plane { get; set; }
    public string departureDateTime { get; set; }
    public string arrivalDateTime { get; set; }
    public int estimatedDuration { get; set; }
    public int distance { get; set; }
    public Crew Crew { get; set; }
    public int operatingCost { get; set; }
    public int seatingCostPerPessenger { get; set; }
}

public class StartAirport
{
    public string Code { get; set; }
    public string Name { get; set; }
}

