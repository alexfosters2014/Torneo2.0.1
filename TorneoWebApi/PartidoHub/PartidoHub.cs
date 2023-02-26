using Entidades;
using Microsoft.AspNetCore.SignalR;
using ViewModels;

namespace TorneoWebApi.PartidoHub
{
    public class PartidoHub : Hub
    {
        public async Task SendPartidoHub(ViewModelTorneoPartidoHub torneoVM)
        {
            await Clients.All.SendAsync("RecibirPartidoVM", torneoVM);
        }

        public async Task PeticionActualizarHub(ViewModelTorneo torneoVM)
        {
            await Clients.All.SendAsync("RecibirActualizarPartido", torneoVM);
        }
    }
}
