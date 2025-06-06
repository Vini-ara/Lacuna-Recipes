using Microsoft.EntityFrameworkCore;
using Recipes.Entities;
using Recipes.Services;

var builder = WebApplication.CreateBuilder(args);


try {
  // Add services to the container.
  builder.Services.AddDbContext<AppDbContext>(options => 
      options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
  );
} catch (Exception ex) {
}

builder.Services.AddScoped<IngredientsService>();
builder.Services.AddScoped<RecipesService>();
builder.Services.AddScoped<RecipeIngredientService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

try {
  using (var scope = app.Services.CreateScope()) {
      var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
      db.Database.Migrate();
  }
} catch (Exception ex) {
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
