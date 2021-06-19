using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi.Controllers
{
    [Route("v1/business")]
    [EnableCors]
    public class BusinessController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Business>>> Get([FromServices] DataContext context)
        {
            var business = await context.Business.Include(x => x.Category).AsNoTracking().ToListAsync();
            return business;
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Business>> GetById([FromServices] DataContext context, int id)
        {
            var business = await context.Business.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return business;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Business>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var business = await context.Business.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return business;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Business>> Post(
            [FromServices] DataContext context,
            [FromBody] Business model)
        {
            if (ModelState.IsValid)
            {
                context.Business.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}