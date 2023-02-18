using BaseDatosContext;
using Entidades;
using Microsoft.EntityFrameworkCore;
using Negocio;
using TorneoWebApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opciones => {
    opciones.AddPolicy("cors", policy =>
    {
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowAnyOrigin();
    }
    );
});

builder.Services.AddDbContext<TorneoContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<EquiposService>();
builder.Services.AddScoped<JugadoresService>();
builder.Services.AddScoped<TorneoService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors");
app.UseHttpsRedirection();

//Configuración de Endpoints
app.MapImageEndpoints();
app.MapJugadoresEndpoints();
app.MapEquiposEndpoints();
app.MapTorneosEndpoints();

app.Run();


