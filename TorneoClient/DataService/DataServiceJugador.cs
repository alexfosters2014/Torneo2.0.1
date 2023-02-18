using Entidades;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace TorneoClient.DataService
{
    public class DataServiceJugador
    {
        private readonly HttpClient _httpClient;

        public DataServiceJugador(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }

        public async Task<Jugador> GetJugador(string cedula)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Jugador/Get/{cedula}");
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Jugador>(content);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> NuevoJugador(Jugador jugador)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Jugador/Nuevo", jugador);
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<int>(content);
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
