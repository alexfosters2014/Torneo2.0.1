using Entidades;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;
using ViewModels;

namespace TorneoClient.DataService
{
    public class DataServiceInscripcion
    {
        private readonly HttpClient _httpClient;

        public DataServiceInscripcion(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }

        public async Task<List<ViewModelTorneo>> GetTorneosSegunDeporte(string deporte)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Torneo/Get/Inscripcion/{deporte}");
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<ViewModelTorneo>>(content, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> NuevaInscripcionATorneo(ViewModelInscripcion torneo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("/Torneo/Inscripcion", torneo);
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<string>(content);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Torneo>> GetTorneosPorNombre(string nombre)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/Torneo/Get/Nombre/{nombre}");
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<Torneo>>(content);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
