using MealPlannerApi.Models;
using MongoDB.Driver;
using HtmlAgilityPack;

namespace MealPlannerApi.Services;

public class RecipeService : IRecipeService
{
    private readonly IMongoCollection<Recipe> _recipes;

    public RecipeService(IRecipeStoreDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _recipes = database.GetCollection<Recipe>(settings.RecipeCollectionName);
    }
    public Recipe Create(Recipe recipe)
    {
        _recipes.InsertOne(recipe);
        return recipe;
    }

    public Recipe? GetByUrl(string url)
    {
        Recipe? recipe = ParseHtml(url);
        if (recipe is null)
        {
            return null;
        }
        recipe.Url = url;

        return recipe;
    }

    public List<Recipe> Get()
    {
        return _recipes.Find(recipe => true).ToList();
    }

    public Recipe Get(string id)
    {
        return _recipes.Find(recipe => recipe.Id == id).FirstOrDefault();
    }

    public void Remove(string id)
    {
        _recipes.DeleteOne(recipe => recipe.Id == id);
    }

    public void Update(string id, Recipe recipe)
    {
        _recipes.ReplaceOne(recipe => recipe.Id == id, recipe);
    }

    private Recipe? ParseHtml(string url)
    {
        HtmlWeb web = new HtmlWeb();

        var htmlDoc = web.Load(url);

        if (htmlDoc is null)
        {
            return null;
        }

        string? title = htmlDoc.DocumentNode.SelectSingleNode("//title").InnerHtml;

        Recipe recipe = new();

        if (title is not null)
        {
            recipe.Title = title;

            string[] titleArr = title.Split();
            string searchTerm = titleArr[0];

            var imgNodes = htmlDoc.DocumentNode.SelectNodes("//img");

            if (imgNodes is not null)
            {
                foreach (var node in imgNodes)
                {
                    if (node.GetAttributeValue("alt", "").Contains(searchTerm))
                    {
                        recipe.ImageSource = node.GetAttributeValue("src", "");
                    }
                }
            }
        }

        return recipe;
    }
}