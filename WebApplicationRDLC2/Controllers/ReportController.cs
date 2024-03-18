using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplicationRDLC2.Controllers
{
    public class ReportController : ApiController
    {
        private readonly ServiceReport serviceReport = new ServiceReport();

        // GET api/report
        public HttpResponseMessage GetReport()
        {
            System.Diagnostics.Debug.WriteLine("GetReport invoked");
            byte[] responseBytes = this.serviceReport.CreateReportFile(@"C:\Users\SaranphonPhaithoon\source\repos\WebApplicationRDLC2\WebApplicationRDLC2\Report\Report1.rdlc");
            
            var stream = new MemoryStream(responseBytes);
   
            // processing the stream.

            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "report.pdf"
                };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }
    }
}
