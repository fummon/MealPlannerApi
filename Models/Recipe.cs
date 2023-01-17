using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MealPlannerApi.Models;

public class Recipe
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("category")]
    public MealCategory Category { get; set; } = MealCategory.None;

    [BsonElement("notes")]
    public string Notes { get; set; } = string.Empty;

    [BsonElement("src")]
    public string ImageSource { get; set; } = string.Empty;

    [BsonElement("url")]
    public string Url { get; set; } = string.Empty;

    [BsonElement("createdDate")]
    public string CreatedDate { get; set; } = string.Empty;
}

