
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using dotnet_webapi.Repositories;

//using dotnet_webapi.Models;
using dotnet_webapi.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

//using dotnet_webapi.Repositories.UserRepository;


namespace dotnet_webapi.Controllers
{
    [Route("v1/account")]
    [EnableCors]
    public class UserController : Controller
    {

/*
        public class AuthenticateRequest
        {
            [Required]
            public string IdToken { get; set; }
        }

        private readonly JwtGenerator _jwtGenerator;

        public UserController(IConfiguration configuration)
        {
            _jwtGenerator = new JwtGenerator(configuration.GetValue<string>("JwtPrivateSigningKey"));
        }

*/

        /*
                [AllowAnonymous]
                [HttpPost("authenticate")]
                public IActionResult Authenticate([FromBody] AuthenticateRequest data)
                {
                    GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();

                    // Change this to your google client ID
                    settings.Audience = new List<string>() { "652390042886-5dpn5dsusjin5ces7qm6hk4fc3atvcv8.apps.googleusercontent.com" };

                    GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(data.IdToken, settings).Result;
                    return Ok(new { AuthToken = _jwtGenerator.CreateUserAuthToken(payload.Email) });
                }

                */


        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate/{token}")]
        public async Task<IActionResult> Authenticate([FromRoute] string token)
        {
            try
            {
                var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[]{"652390042886-5dpn5dsusjin5ces7qm6hk4fc3atvcv8.apps.googleusercontent.com"}

                }
                );

                return Ok();

            }
            catch (Exception exception)
            {

                return BadRequest();
           }
        }


        /// <summary>
        /// POST- Login in the system
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        [EnableCors]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model, [FromServices] DataContext context)
        {
            // Recupera o usu??rio
            // var user = UserRepository.Get(model.Username, model.Password);
            var user = context.User.Where(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password).FirstOrDefault();

            // Verifica se o usu??rio existe
            if (user == null)
                return NotFound(new { message = "Usu??rio ou senha inv??lidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.Password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "An??nimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcion??rio";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";


    }
}