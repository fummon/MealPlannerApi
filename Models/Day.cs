using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MealPlannerApi.Models;

public class Day
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("day")]
    public DayOfWeek DayOfWeek { get; set; } = DayOfWeek.Monday;

    [BsonElement("recipeId")]
    public string RecipeId { get; set; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("category")]
    public MealCategory Category { get; set; } = MealCategory.None;
}

