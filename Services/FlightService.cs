using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using admin.Dtos;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace admin.Services
{
    public class FlightService
    {
        private readonly IMongoCollection<Airport> _airportCollection;
        private readonly IMongoCollection<Flight> _flightCollection;

        public FlightService(IMongoDatabase database)
        {
            _airportCollection = database.GetCollection<Airport>("airport");
            _flightCollection = database.GetCollection<Flight>("flight");
        }

        public async Task<ApiResponse> GetAirports()
        {

            var filter = Builders<Airport>.Filter.Regex(x => x.name, new BsonRegularExpression(new Regex($"^{"abc"}", RegexOptions.IgnoreCase)));

            var airports = await _airportCollection.Find(_ => true).ToListAsync();
            return new ApiResponse
            {
                Success = true,
                ResponseCode = HttpStatusCode.OK,
                Data = airports
            };
        }

        public async Task<ApiResponse> GetAirportByName(string SearchRequest)
        {

            var airportFilter = Builders<Airport>.Filter.Or(
                        Builders<Airport>.Filter.Regex(x => x.name, new BsonRegularExpression(new Regex($"^{SearchRequest}", RegexOptions.IgnoreCase))), 
                        Builders<Airport>.Filter.Regex(x => x.country, new BsonRegularExpression(new Regex($"^{SearchRequest}", RegexOptions.IgnoreCase))), 
                        Builders<Airport>.Filter.Regex(x => x.city, new BsonRegularExpression(new Regex($"^{SearchRequest}", RegexOptions.IgnoreCase)))
                    );

          
        
            var airports =  _airportCollection.Find(airportFilter).ToList();
            

            return new ApiResponse
            {
                Success = true,
                ResponseCode = HttpStatusCode.OK,
                Data = airports
            };
        }



    }
}