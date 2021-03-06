using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using dotnet_webapi.Data;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace dotnet_webapi.Controllers
{
    // [Route("v1/business")]
    [EnableCors]
    public class BusinessController : ControllerBase
    {

        private readonly IDistributedCache _distributedCache;
        private readonly DataContext _context;
        public BusinessController(DataContext context, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _context = context;
        }


        /// <summary>
        /// Get all Business.
        /// </summary>
        /// <returns>All Business in Db</returns>
        [HttpGet("v1/business")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Business>>> Get([FromServices] DataContext context)
        {
            var business = await context.Business.Include(x => x.Category).AsNoTracking().ToListAsync();
            return business;
        }

        /*
               [HttpGet("v1/business_redis")]
               [AllowAnonymous]
               public async Task<IActionResult> GetAllBusinessUsingRedisCache()
               {
                   var cacheKey = "businessList";
                   string serializedBusinessList;
                   var BusinessList = new List<Business>();
                   var redisBusinessList = await _distributedCache.GetAsync(cacheKey);
                   if (redisBusinessList != null)
                   {
                       serializedBusinessList = Encoding.UTF8.GetString(redisBusinessList);
                       BusinessList = JsonConvert.DeserializeObject<List<Business>>(serializedBusinessList);
                   }
                   else
                   {
                       BusinessList = await _context.Business.ToListAsync();
                       serializedBusinessList = JsonConvert.SerializeObject(BusinessList);
                       redisBusinessList = Encoding.UTF8.GetBytes(serializedBusinessList);
                       var options = new DistributedCacheEntryOptions()
                           .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                           .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                       await _distributedCache.SetAsync(cacheKey, redisBusinessList, options);
                   }
                   return Ok(BusinessList);
               }

               */



        /// <summary>
        /// GetById a specific Business.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>        
        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<Business>> GetById([FromServices] DataContext context, int id)
        {
            var business = await context.Business.Include(x => x.Category).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return business;
        }

        /// <summary>
        /// GetById a specific Category.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>   
        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Business>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var business = await context.Business.Include(x => x.Category).AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
            return business;
        }

        /// <summary>
        /// Creates a Business.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Business
        ///     {
        ///         "name": "It??s Something ????",
        ///         "description": "Some Description Such Wow",
        ///         "mobilePhone": "+351 964 575 619",
        ///         "categoryId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="context"></param>
        /// <param name="model"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response> 
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