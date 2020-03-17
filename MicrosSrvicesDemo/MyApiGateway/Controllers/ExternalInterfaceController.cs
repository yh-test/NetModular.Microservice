using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalInterfaceController : ControllerBase
    {
        private readonly ILogger<ExternalInterfaceController> _logger;

        public ExternalInterfaceController(ILogger<ExternalInterfaceController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public ActionResult Get()
        {
            string url = "http://MyServiceA/api/InternalServerInterface/get";
            string result = WebApiHelperExtend.InvokeApi(url);
            result += "After the internal interface is requested, it will be  returned to the TestApiGatewayDemo";
            return Ok(result);
        }
    }
}