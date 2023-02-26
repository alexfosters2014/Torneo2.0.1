using Entidades;
using Microsoft.AspNetCore.SignalR;
using ViewModels;

namespace TorneoWebApi.PartidoHub
{
    public class PartidoHub : Hub
    {
        public async Task SendEstadoHub(int estado)
        {
            await Clients.All.SendAsync("RecibirEstado", estado);
        }

        public async Task SendEstadoBolsaHub(PartidoVM partido)
        {
            await Clients.All.SendAsync("RecibirEnBolsa", partido);
        }


    }
}
