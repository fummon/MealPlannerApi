using MealPlannerApi.Models;
using MongoDB.Driver;

namespace MealPlannerApi.Services;

public class PlannerService : IPlannerService
{
    private readonly IMongoCollection<Day> _planner;

    public PlannerService(IRecipeStoreDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _planner = database.GetCollection<Day>(settings.PlannerCollectionName);
    }
    public Day Create(Day day)
    {
        _planner.InsertOne(day);
        return day;
    }

    public List<Day> Get()
    {
        return _planner.Find(day => true).ToList();
    }

    public List<Day> GetByDay(string dayOfWeek)
    {
        return _planner.Find(day => day.DayOfWeek.Equals(dayOfWeek)).ToList();
    }

    public void Remove(string id)
    {
        _planner.DeleteOne(day => day.Id == id);
    }

    public void RemoveByRecipeId(string recipeId)
    {
        _planner.DeleteMany(recipe => recipe.RecipeId == recipeId);
    }

    public void RemoveAll()
    {
        _planner.DeleteMany(day => true);
    }
}