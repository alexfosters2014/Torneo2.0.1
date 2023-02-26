using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using TorneoEspectadores;
using TorneoEspectadores.DataServices;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
 
string backendUrlLocal = builder.Configuration.GetValue<string>("DefaultConnectionWebApi");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrlLocal) });

builder.Services.AddSingleton<HubConnection>(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
      .WithUrl(navigationManager.ToAbsoluteUri($"{backendUrlLocal}/partidohubs"))
      .WithAutomaticReconnect()
      .Build();
});

builder.Services.AddScoped<DataServiceTorneoPartido>();

await builder.Build().RunAsync();
