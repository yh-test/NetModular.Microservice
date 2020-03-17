using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyServiceA
{
    public class ServiceLocator
    {
        public static IApplicationBuilder ApplicationBuilder { get; set; }
    }
}
