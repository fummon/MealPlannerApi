using MealPlannerApi.Models;
using MealPlannerApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealPlannerApi.Controllers;

[ApiController]
[Route("planner")]
public class PlannerController : ControllerBase
{
    private readonly IPlannerService _plannerService;

    public PlannerController(IPlannerService plannerService)
    {
        _plannerService = plannerService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Day>> GetAll()
    {
        return _plannerService.Get();
    }

    [HttpGet]
    [Route("recipesByDay")]
    public ActionResult<IEnumerable<Day>> GetByDay(string dayOfWeek)
    {
        IEnumerable<Day> recipesByDay = _plannerService.GetByDay(dayOfWeek);

        return Ok(recipesByDay);
    }

    [HttpPost]
    public ActionResult<Recipe> Create(Day day)
    {
        _plannerService.Create(day);

        return Ok(day);
    }

    [HttpDelete("{id}")]
    public ActionResult<Day> Delete(string id)
    {
        _plannerService.Remove(id);

        return Ok();
    }

    [HttpDelete("recipesByDay")]
    public ActionResult<Day> DeleteAll()
    {
        _plannerService.RemoveAll();

        return Ok();
    }
}
