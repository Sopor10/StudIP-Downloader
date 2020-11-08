using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudIPDownloader.Domain;
using Cookie = System.Net.Cookie;

namespace StudIPDownloader.WebApi.Controllers
{
    [ApiController]
    [Route("api/studip")]
    public class StudipDownloadController : ControllerBase
    {
        public ILoginActivityService LoginActivityService { get; }
        public ILogger<StudipDownloadController> Logger { get; }

        [HttpGet]
        public async Task Get([FromQuery]InputContract input)
        {
            Logger.LogInformation($"Lade Daten von StudIP zu {input}");
            string SessionCookie = await LoginActivityService.Login(input.Username,input.Password, input.Url);
            string path = "data";
            
            var cookie = new Cookie("Seminar_Session", SessionCookie, "/", new Uri(StudIPClient.BASE).Host);
            var client = new StudIPClient(cookie,false);

            client.syncFiles(path);
        }

        public StudipDownloadController(ILoginActivityService loginActivityService, ILogger<StudipDownloadController> logger)
        {
            LoginActivityService = loginActivityService;
            Logger = logger;
        }
    }
}