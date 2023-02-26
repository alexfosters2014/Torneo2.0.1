using Blazored.Modal;
using Blazored.Toast;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using MudBlazor.Services;
using TorneoClient;
using TorneoClient.DataService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();
builder.Services.AddMudServices();

string backendUrlLocal = builder.Configuration.GetValue<string>("DefaultConnectionWebApi");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("DefaultConnectionWebApi")) });

builder.Services.AddSingleton<HubConnection>(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl(navigationManager.ToAbsoluteUri($"{builder.Configuration.GetValue<string>(backendUrlLocal)}/partidohubs"))
      .WithAutomaticReconnect()
      .Build();
});

builder.Services.AddScoped<DataServiceEquipo>();
builder.Services.AddScoped<DataServiceImagen>();
builder.Services.AddScoped<DataServiceJugador>();
builder.Services.AddScoped<DataServiceInscripcion>();
builder.Services.AddScoped<DataServiceTorneo>();


await builder.Build().RunAsync();
