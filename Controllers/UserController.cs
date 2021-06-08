
using System;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using dotnet_webapi.Repositories;

//using dotnet_webapi.Models;
using dotnet_webapi.Services;




using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//using dotnet_webapi.Repositories.UserRepository;


namespace dotnet_webapi.Controllers
{
    [Route("v1/account")]
    public class UserController : Controller
    {
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model, [FromServices] DataContext context)
        {
            // Recupera o usuário
         // var user = UserRepository.Get(model.Username, model.Password);
            var user = context.User.Where(x => x.Username.ToLower() == model.Username.ToLower() && x.Password == model.Password).FirstOrDefault();

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

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
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";


    }
}