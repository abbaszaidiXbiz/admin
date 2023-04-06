using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace admin.Collections;

[BsonIgnoreExtraElements]
public class Plane
{
     [BsonId]
    public string _id { get; set; }  = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
    public int TailNumber { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }   
    public int? FlyingRange { get; set; }
    public DateTime? DateBuilt { get; set; }
    public string? DatePurchased { get; set; }
    public string? Status { get; set; }
    public int? SeatingCapacity { get; set; }
    public DateTime? DateOfFirstFlight { get; set; }
    public int? TankCapacity { get; set; }
}