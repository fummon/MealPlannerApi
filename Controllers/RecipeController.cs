using MealPlannerApi.Models;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers;

[ApiController]
[Route("recipes")]
public class RecipeController : ControllerBase
{
    private readonly IRecipeService _recipeService;
    private readonly IPlannerService _plannerService;

    public RecipeController(IRecipeService recipeService, IPlannerService plannerService)
    {
        _recipeService = recipeService;
        _plannerService = plannerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Recipe>> GetAll()
    {
        return _recipeService.Get();
    }

    [HttpGet]
    [Route("recipe")]
    public ActionResult<Recipe> GetByUrl(string url)
    {
        Recipe? recipe = _recipeService.GetByUrl(url);

        if (recipe is null)
        {
            return NotFound(url);
        }

        return recipe;
    }

    [HttpPost]
    public ActionResult<Recipe> Create(Recipe recipe)
    {
        _recipeService.Create(recipe);

        return Ok(recipe);
    }

    [HttpPut]
    public ActionResult<Recipe> Update(Recipe recipe)
    {
        Recipe existingRecipe = _recipeService.Get(recipe.Id);

        if (existingRecipe is null)
        {
            return NotFound($"Recipe with id = {recipe.Id} not found");
        }

        _recipeService.Update(recipe.Id, recipe);

        return Ok(recipe);
    }

    [HttpDelete("{id}")]
    public ActionResult<Recipe> Delete(string id)
    {
        Recipe existingRecipe = _recipeService.Get(id);

        if (existingRecipe is null)
        {
            return NotFound($"Recipe with id = {id} not found");
        }

        _recipeService.Remove(id);
        _plannerService.RemoveByRecipeId(id);

        return Ok(new Recipe());
    }
}
