using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace admin.Collections
{
    public class User
    {
        [BsonId]
        public string Id { get; set; } = new BsonObjectId(ObjectId.GenerateNewId()).ToString();

        [BsonElement("firstname")]
        public string FirstName { get; set; }

        [BsonElement("lastname")]
        public string LastName { get; set; }

        [BsonElement("dob")]
        public DateTime Dob { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("photos")]
        public List<Photo> Photos { get; set; }

        [BsonElement("createdon")]
        public DateTime CreatedOn { get; set; }

    }

    public class Photo
    {
        [BsonId]
        public string Id { get; set; } = new BsonObjectId(ObjectId.GenerateNewId()).ToString();

        [BsonElement("url")]
        public string Url { get; set; }

        [BsonElement("uploadtime")]
        public DateTime UploadedOn { get; set; } = System.DateTime.Now;

        [BsonElement("main")]
        public bool IsMain { get; set; }

        [BsonElement("hidden")]
        public bool hidden { get; set; }

    }
}