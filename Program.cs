using System.Text;
using MealPlannerApi.Models;
using MealPlannerApi.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RecipeStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(RecipeStoreDatabaseSettings)));
builder.Services.AddSingleton<IRecipeStoreDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<RecipeStoreDatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("RecipeStoreDatabaseSettings:ConnectionString")));
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IPlannerService, PlannerService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                      });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
