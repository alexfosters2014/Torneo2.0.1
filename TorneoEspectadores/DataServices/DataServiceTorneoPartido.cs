using Entidades;
using Newtonsoft.Json;
using System.Net.Http.Json;
using ViewModels;

namespace TorneoEspectadores.DataServices
{
    public class DataServiceTorneoPartido
    {
        private readonly HttpClient _httpClient;

        public DataServiceTorneoPartido(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }


        public async Task<List<ViewModelTorneo>> GetTorneosVigentes()
        {
            try
            {
                var response = await _httpClient.GetAsync("/Torneo/Get/Vigentes");
                if (!response.IsSuccessStatusCode)
                {
                    var contentError = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<string>(contentError);
                    throw new Exception(error);
                }

                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonConvert.DeserializeObject<List<ViewModelTorneo>>(content);
                return resultado;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
