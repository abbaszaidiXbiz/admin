using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace admin.Collections
{

    public class Account
    {
        [BsonId]
        public string Id { get; set; } = new BsonObjectId(ObjectId.GenerateNewId()).ToString();

        [BsonElement("userid")]
        public string UserId { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]

        public string Email { get; set; }

        [BsonElement("lockout")]
        public bool lockout { get; set; }

    }
}