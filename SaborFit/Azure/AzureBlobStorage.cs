﻿using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using System.Text.RegularExpressions;

namespace SaborFit.Azure
{
    public class AzureBlobStorage
    {
        public string UploadImage(string image)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=saborfit;AccountKey=23jnCzQNOL/6ZBxrb0jTwgxqxKYeQ28hcIuLl5KJ0eQXYgDF1S8RzioSCbHbTPESuAVsgW85EuqK+AStbLbCHw==;EndpointSuffix=core.windows.net";
            string containerName = "saborfit";

            // Gera um nome randomico para imagem
            var fileName = Guid.NewGuid().ToString() + ".jpg";

            // Limpa o hash enviado
            var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(image, "");

            // Gera um array de Bytes
            byte[] imageBytes = Convert.FromBase64String(data);

            // Define o BLOB no qual a imagem será armazenada
            var blobClient = new BlobClient(connectionString, containerName, fileName);

            var blobHttpHeader = new BlobHttpHeaders { ContentType = "image/jpg" };

            // Envia a imagem
            using (var stream = new MemoryStream(imageBytes))
            {
                blobClient.Upload(stream, new BlobUploadOptions()
                {
                    HttpHeaders = blobHttpHeader,
                });
            }

            // Retorna a URL da imagem
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
