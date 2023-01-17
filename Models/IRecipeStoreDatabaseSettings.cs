namespace MealPlannerApi.Models;

public interface IRecipeStoreDatabaseSettings
{
    public string RecipeCollectionName { get; set; }
    public string PlannerCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}