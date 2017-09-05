using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using FilePostTest.Services.Models;
using Newtonsoft.Json;

namespace FilePostTest.Services.Controllers
{
    [RoutePrefix("api/File")]
    public class FileServiceController : ApiController
    {
        [Route("upload")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<IHttpActionResult> Upload()
        {
            if (!this.Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            await this.Request.Content.ReadAsMultipartAsync(provider);

            foreach (HttpContent content in provider.Contents)
            {
                String nombreArchivo = content.Headers.ContentDisposition.FileName?.Trim();

                Int64? tamaño = content.Headers.ContentLength;

                Byte[] contenidoImagen = await content.ReadAsByteArrayAsync();

            }

            return Ok(new UploadResultModel() { Id=Guid.NewGuid() });
        }

        [Route("uploadie")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public async Task<HttpResponseMessage> UploadIE()
        {
            if (!this.Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();

            await this.Request.Content.ReadAsMultipartAsync(provider);

            foreach (HttpContent content in provider.Contents)
            {
                String nombreArchivo = content.Headers.ContentDisposition.FileName?.Trim();

                Int64? tamaño = content.Headers.ContentLength;

                Byte[] contenidoImagen = await content.ReadAsByteArrayAsync();

            }

            UploadResultModel model = new UploadResultModel() { Id = Guid.NewGuid() };

            StringBuilder html = new StringBuilder();

            html.Append("<textarea data-type='application/json'>");
            html.Append(JsonConvert.SerializeObject(model));
            html.Append("</textarea>");

            var response = new HttpResponseMessage()
            {
                Content = new StringContent(html.ToString())
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
