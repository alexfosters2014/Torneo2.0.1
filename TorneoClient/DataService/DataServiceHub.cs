using Entidades;
using Microsoft.AspNetCore.SignalR.Client;
using ViewModels;

namespace TorneoClient.DataService
{
    public class DataServiceHub
    {
        private readonly HubConnection _hubConnection;
        public DataServiceHub(HubConnection _hubConnection)
        {
            this._hubConnection = _hubConnection;
            this._hubConnection.StartAsync();
        }

        public async Task EnviarPartidoATodos(int torneoId, PartidoVM partidoVM)
        {
            ViewModelTorneoPartidoHub viewModelTorneoPartidoHub = new()
            {
                TorneoId = torneoId,
                PartidoVM = partidoVM
            };
            await _hubConnection.SendAsync("SendPartidoHub", viewModelTorneoPartidoHub);
        }

        public void Dispose()
        {
           _hubConnection.DisposeAsync();
        }
    }
}
