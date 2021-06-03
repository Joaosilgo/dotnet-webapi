using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi.Controllers
{

    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [Route("")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Category.AsNoTracking().ToListAsync();

            return categories;
        }






        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context,[FromBody] Category model)
        {
            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Category.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar a categoria" });

            }
        }

    }






}