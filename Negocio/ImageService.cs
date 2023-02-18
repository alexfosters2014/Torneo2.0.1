using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
    public class ImageService
    {

        private string _extension = ".png";
        public async Task<string> UploadImage(string keyAzureBlob, string storageNameAzureBlob, byte[] image)
        {
            try
            {
                await using var imagenStream = new MemoryStream();
                await imagenStream.WriteAsync(image);
                imagenStream.Position = 0;

                string imageName = Guid.NewGuid().ToString() + _extension;

                BlobContainerClient blobContainer = new BlobContainerClient(keyAzureBlob, storageNameAzureBlob);
                var blob = blobContainer.GetBlobClient(imageName);

                await blob.UploadAsync(imagenStream);

                return imageName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> UpdateImage(string keyAzureBlob, string storageNameAzureBlob, byte[] image,string nombreArchivoAnterior)
        {
            try
            {
                await using var imagenStream = new MemoryStream();
                await imagenStream.WriteAsync(image);
                imagenStream.Position = 0;

                string imageName = Guid.NewGuid().ToString() + _extension;

                BlobContainerClient blobContainer = new BlobContainerClient(keyAzureBlob, storageNameAzureBlob);


                var blobAEliminar = blobContainer.GetBlobClient(nombreArchivoAnterior);
                await EliminarImagen(blobAEliminar);


                var blob = blobContainer.GetBlobClient(imageName);
                await blob.UploadAsync(imagenStream);

                return imageName;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task EliminarImagen(BlobClient blob)
        {
            try
            {
                await blob.DeleteAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


    }
}
