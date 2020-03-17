using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyServiceA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalServerInterfaceController : ControllerBase
    {
        private readonly ILogger<InternalServerInterfaceController> _logger;

        public InternalServerInterfaceController(ILogger<InternalServerInterfaceController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Get()
        {
            // 获取当前服务地址和端口
            var features = ServiceLocator.ApplicationBuilder.Properties["server.Features"] as FeatureCollection;
            var address = features.Get<IServerAddressesFeature>().Addresses.First();
            return Ok("This is the internal interface");
        }
    }
}