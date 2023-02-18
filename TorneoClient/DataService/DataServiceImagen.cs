using Entidades;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TorneoClient.DataService
{
    public class DataServiceImagen
    {
        private readonly HttpClient _httpClient;
        private long maxSizeFile = long.MaxValue;
        private string _format = "image/png";

        public DataServiceImagen(HttpClient _httpClient)
        {
            this._httpClient = _httpClient;
        }


        public async Task<string> SubirImagenFile(InputFileChangeEventArgs e)
        {
            try
            {
                var formData = new MultipartFormDataContent();

                foreach (var image in e.GetMultipleFiles(1))
                {
                    var imagenRedimensionado = await image.RequestImageFileAsync(_format, 200, 200);

                    var memoryStream = new MemoryStream();
                    await imagenRedimensionado.OpenReadStream(maxSizeFile).CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    var fileContent = new StreamContent(memoryStream);

                    string fileName = image.Name;
                    formData.Add(fileContent, "request", fileName);
                }

                var response = await _httpClient.PostAsync("/Image/Upload",formData);

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

        public async Task<string> ActualizarImagenFile(InputFileChangeEventArgs e, string nombreArchivoActual)
        {
            try
            {
                var formData = new MultipartFormDataContent();

                foreach (var image in e.GetMultipleFiles(1))
                {
                    var imagenRedimensionado = await image.RequestImageFileAsync(_format, 200, 200);

                    var memoryStream = new MemoryStream();
                    await imagenRedimensionado.OpenReadStream(maxSizeFile).CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    var fileContent = new StreamContent(memoryStream);

                    formData.Add(fileContent, "request", nombreArchivoActual);
                }

                var response = await _httpClient.PostAsync("/Image/Update", formData);

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
    }
}
