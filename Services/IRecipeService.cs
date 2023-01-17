using MealPlannerApi.Models;

namespace MealPlannerApi.Services;

public interface IRecipeService
{
    public Recipe Create(Recipe recipe);

    public Recipe? GetByUrl(string url);

    public List<Recipe> Get();

    public Recipe Get(string id);

    public void Remove(string id);

    public void Update(string id, Recipe recipe);
}