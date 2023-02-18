using Microsoft.Extensions.Configuration;
using Negocio;

namespace TorneoWebApi.EndPoints
{
    public static class ImagesEndpoints
    {
        private static string _keyAzureStorage;
        private static string _storageNameAzure;
        public static void MapImageEndpoints(this WebApplication app)
        {
            _keyAzureStorage = app.Configuration.GetValue<string>("KeyAzureStorage");
            _storageNameAzure = app.Configuration.GetValue<string>("AzureStorageName");

            app.MapPost("/Image/Upload",UploadImage);
            app.MapPost("/Image/Update",UpdateImage);

        }

        public static async Task<IResult> UploadImage(ImageService serviceImage, HttpRequest request)
        {
            if (!request.Form.Files.Any()) return Results.BadRequest("No ha seleccionado ninguna imagen");

            byte[] stream = await GetBytes(request.Form.Files[0]);

            string resultado = await serviceImage.UploadImage(_keyAzureStorage, _storageNameAzure, stream);
            if (resultado == null) return Results.NoContent();

            return Results.Ok(resultado);
        }

        public static async Task<IResult> UpdateImage(ImageService serviceImage, HttpRequest request)
        {
            if (!request.Form.Files.Any()) return Results.BadRequest("No ha seleccionado ninguna imagen");

            byte[] stream = await GetBytes(request.Form.Files[0]);

            string resultado = await serviceImage.UpdateImage(_keyAzureStorage, _storageNameAzure, stream, request.Form.Files[0].FileName);
            if (resultado == null) return Results.NoContent();

            return Results.Ok(resultado);
        }

        public static async Task<byte[]> GetBytes(IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            //string s = Convert.ToBase64String(imageArray);
            return memoryStream.ToArray();
        }
    }
}
