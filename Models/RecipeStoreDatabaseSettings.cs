namespace MealPlannerApi.Models;

public class RecipeStoreDatabaseSettings : IRecipeStoreDatabaseSettings
{
    public string RecipeCollectionName { get; set; } = string.Empty;
    public string PlannerCollectionName { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
}