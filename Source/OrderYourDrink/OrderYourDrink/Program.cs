using OrderYourDrink.BLL;
using OrderYourDrink.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCorsSettings();

// Setup services
builder.Services.AddOrderYourDrinkServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(CorsSettings. AllowAll);
}else if (app.Environment.IsEnvironment("Test"))
{
    app.UseCors(CorsSettings.AllowTest);
}
else
{
    app.UseCors(CorsSettings.AllowProd);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
