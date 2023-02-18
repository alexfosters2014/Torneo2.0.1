using Entidades;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace TorneoClient.DataService
{
    public class DataServiceEquipo
    {
        private readonly HttpClient _httpClient;

        public DataServiceEquipo(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }

        public async Task<Equipo> GetEquipo(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Equipo/Get/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<Equipo>(content);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> NuevoEquipo(Equipo equipo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Equipo/Nuevo", equipo);
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

        public async Task<List<Equipo>> GetsEquiposPorNombre(string nombre)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Equipo/Get/Find/{nombre}");
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Equipo>>(content);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
