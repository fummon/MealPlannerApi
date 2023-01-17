using MealPlannerApi.Models;

namespace MealPlannerApi.Services;

public interface IPlannerService
{
    public Day Create(Day day);

    public List<Day> GetByDay(string dayOfWeek);

    public List<Day> Get();

    public void Remove(string id);

    public void RemoveByRecipeId(string recipeId);

    public void RemoveAll();
}