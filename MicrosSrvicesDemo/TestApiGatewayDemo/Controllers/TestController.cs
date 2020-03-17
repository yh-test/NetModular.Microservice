using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestApiGatewayDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        public IActionResult Test()
        {
            string url = "http://MyServiceA/api/ExternalInterface/get";
            string result = WebApiHelperExtend.InvokeApi(url);
            return Ok(result);
        }
       
    }
}