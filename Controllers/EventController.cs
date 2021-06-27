using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Newtonsoft.Json;

namespace dotnet_webapi.Controllers
{
    [Route("v1/events")]
    [EnableCors]
    public class EventController : ControllerBase
    {
        private readonly DataContext _context;
        public EventController(DataContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        [AllowAnonymous]
        //   public async Task<ActionResult<List<Event>>> Get(EventParameters eventParameters)
        public IActionResult Get(EventParameters eventParameters)
        {
            // var events = await _context.Event.AsNoTracking().Skip((eventParameters.PageNumber-1) * eventParameters.PageSize).Take(eventParameters.PageSize).ToListAsync();

            var events =  _context.GetEvents(eventParameters);

            var metadata = new
            {

                events.TotalCount,
                events.PageSize,
                events.CurrentPage,
                events.HasNext,
                events.HasPrevious
                
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(events);
           // return events;
        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<Event> GetById(int id)
        {
            var events = await _context.Event
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return events;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Event>> Post([FromBody] Event model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            try
            {
                _context.Event.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível criar o evento" });
            }
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Event>> Update(int id, [FromBody] Event model)
        {
            if (id != model.Id)
                return NotFound(new { message = "Evento não encontrado" });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Entry<Event>(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar o evento" });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Event>> Delete(int id)
        {
            var events = await _context.Event
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (events == null)
            {
                return NotFound(new { message = "Evento não encontrado" });
            }

            try
            {
                _context.Remove(events);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover o evento" });
            }
        }

    }
}