
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web;


namespace dotnet_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultController : ControllerBase
    {
        [Route("/")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<Default> Get()
        {



            // Create a list of endpoints.
            List<Endpoints> endpoints = new List<Endpoints>();

            // Add parts to the list.
            endpoints.Add(new Endpoints() { Name = "Swagger", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
            endpoints.Add(new Endpoints() { Name = "Business", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
            endpoints.Add(new Endpoints() { Name = "Categories", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
        //     endpoints.Add(new Endpoints() { Name = "Categories", Url_Endpoints = new Uri(HttpContext.Request.Scheme)});


            return Enumerable.Range(1, 1).Select(index => new Default
            {
                Name = "Default Page",
                //  Endpoints = [ new Endpoints{Endpoints:}]
                Endpoints = endpoints,
                Summary = "APIs RESTful expressas usando JSON."


            })
           .ToArray();
        }
    }
}


