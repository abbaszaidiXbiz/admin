using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace admin.Collections;

public class Airport
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string _id { get; set; } = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
    public string airportCode { get; set; } = string.Empty;
    public string name { get; set; } = string.Empty;
    public string country { get; set; } = string.Empty;
    public string city { get; set; } = string.Empty;
    public double hourlyWaitCharge { get; set; } = 0;
    public double refeulingCostperLiter { get; set; } = 0;
}

