using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUDMongo.Domain.Entities;
public class Playlist
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? UserName { get; set; }

    [BsonElement("items")]
    [JsonPropertyName("items")]
    public List<string>? MovieIds { get; set; }

}