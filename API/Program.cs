using Microsoft.EntityFrameworkCore;
using Pizzaria_Nat_2.Infra.Repositories;
using Pizzaria_Nat_2.Repositories;
using Pizzaria_Nat_2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddSingleton<IPizzaRepository, PizzaRepository>();

builder.Services.AddDbContext<PizzariaDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    ));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379"; // ou a URL do Redis remoto
    options.InstanceName = "Pedidos:";
});

builder.Services.AddScoped<PizzaService>();

builder.Services.AddScoped<IPizzaRepository, PizzaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();