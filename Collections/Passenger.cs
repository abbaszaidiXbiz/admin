using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace admin.Collections;

[BsonIgnoreExtraElements]
public class Passenger
{
    [BsonId]
    public string _id { get; set; } = new BsonObjectId(ObjectId.GenerateNewId()).ToString();
    public string email { get; set; }
    public string fullName { get; set; }
    public string title { get; set; }
    public string age { get; set; }
    public string gender { get; set; }
}