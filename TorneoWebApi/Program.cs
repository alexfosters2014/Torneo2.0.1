using BaseDatosContext;
using Entidades;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Negocio;
using TorneoWebApi.EndPoints;
using TorneoWebApi.PartidoHub;

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

builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
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
app.UseResponseCompression();

//Configuración de Endpoints
app.MapImageEndpoints();
app.MapJugadoresEndpoints();
app.MapEquiposEndpoints();
app.MapTorneosEndpoints();

app.UseRouting();
//app.UseEndpoints(endpoints => endpoints.MapHub<PartidoHub>("/partidohubs"));
app.MapHub<PartidoHub>("/partidohubs");

app.Run();


