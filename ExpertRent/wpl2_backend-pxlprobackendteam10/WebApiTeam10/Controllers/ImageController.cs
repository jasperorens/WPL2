using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text.Json.Nodes;
using ClassLibTeam06.Business.Entities;
using Newtonsoft.Json;

namespace WebApiTeam10.Controllers;


    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private static string ConnectionString =
            "DefaultEndpointsProtocol=https;AccountName=expertrent;AccountKey=A3Mw9p36RJtO9+U5qT7Bmxe9rip93f6VodmY9TnPhtsqQScIfTF3y+n/ySXTt1C2Pwkxs6xDU5qF+AStecdvnQ==;EndpointSuffix=core.windows.net";

        private static string Container = "images";

        [HttpPost]
        [Route(("SendImage"))]
        public IActionResult UploadProfilePicture(string userid, [FromBody] JsonObject jsonObject)
        {
            ImageModel imageModel = JsonConvert.DeserializeObject<ImageModel>(jsonObject.ToString());
            string base64 = imageModel.GetImage();
            
            if (base64.StartsWith("data:image"))
            {
                int commaIndex = base64.IndexOf(',');
                base64 = base64.Substring(commaIndex + 1);
            }
            
            byte[] imageBytes = Convert.FromBase64String(base64);
            
            string fileName = userid + ".jpg";
            
            
            using (MemoryStream stream = new MemoryStream(imageBytes))
            {
                BlobServiceClient blobServiceClient = new BlobServiceClient(ConnectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(Container);
                BlobClient blobClient = containerClient.GetBlobClient(fileName);

                blobClient.Upload(stream, overwrite: true);
                Console.Write("Uploaded successfully!");
            }
            return Ok("Image uploaded successfully.");
        }
    }