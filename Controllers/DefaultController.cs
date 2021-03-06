
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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace dotnet_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class DefaultController : ControllerBase
    {
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(ILogger<DefaultController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Dá-nos a informação de todos os endpoints da API
        /// </summary>
        /// <returns>Objeto contendo valores url de todos os EndPoints.</returns>
        [Route("/")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IEnumerable<Default> Get()
        {



            // Create a list of endpoints.
            List<Endpoints> endpoints = new List<Endpoints>();

            // Add parts to the list.
            // endpoints.Add(new Endpoints() { Name = "Swagger", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
            // endpoints.Add(new Endpoints() { Name = "Business", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
            // endpoints.Add(new Endpoints() { Name = "Categories", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
            //  endpoints.Add(new Endpoints() { Name = "Swagger", Url_Endpoints = new Uri(HttpContext.Request.Host.ToString() + "/swagger") });
            endpoints.Add(new Endpoints() { Name = "Swagger", Summary = "Swagger UI", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/swagger/") });
            endpoints.Add(new Endpoints() { Name = "Swagger", Summary = "Hangfire DashBoard", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/hangfire/") });
            endpoints.Add(new Endpoints() { Name = "Business", Summary = "Get/Post", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/business") });
            endpoints.Add(new Endpoints() { Name = "Business", Summary = "Get /v1/business/{id}", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/business/0") });
            endpoints.Add(new Endpoints() { Name = "Business", Summary = "Get /v1/business/categories/{id}", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/business/categories/1") });
            endpoints.Add(new Endpoints() { Name = "Categories", Summary = "Get/Post", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/categories") });
            endpoints.Add(new Endpoints() { Name = "Categories", Summary = "Put Delete", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/categories/1") });
            endpoints.Add(new Endpoints() { Name = "Default", Summary = "Info Default Endpoints", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/") });
            endpoints.Add(new Endpoints() { Name = "User", Summary = "Post", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/account/login") });
            endpoints.Add(new Endpoints() { Name = "User", Summary = "Get ", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/account/anonymous") });
            endpoints.Add(new Endpoints() { Name = "User", Summary = "Get ", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/account/employee") });
            endpoints.Add(new Endpoints() { Name = "User", Summary = "Get ", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/account/authenticated") });
            endpoints.Add(new Endpoints() { Name = "User", Summary = "Get ", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/v1/account/manager") });
            endpoints.Add(new Endpoints() { Name = "Template", Url_Endpoints = new Uri("https://dotnet-webapi.herokuapp.com/WeatherForecast") });


            _logger.LogInformation(message: "Hello Default Routes");
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


