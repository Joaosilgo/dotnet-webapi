using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_webapi.Data;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_webapi.Controllers
{

    [Route("v1/categories")]
    [EnableCors]
    public class CategoryController : ControllerBase
    {

        private readonly DataContext _context;


        public CategoryController(DataContext context)
        {
                _context=context;

        }







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
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category model)
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



        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put(
            [FromServices] DataContext context,
            int id,
            [FromBody] Category model)
        {
            // Verifica se o ID informado é o mesmo do modelo
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            // Verifica se os dados são válidos
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return model;
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Não foi possível atualizar a categoria" });

            }
        }

        [HttpDelete]
        [Route("{id:int}")]

        public async Task<ActionResult<Category>> Delete(
            [FromServices] DataContext context,
            int id)
        {
            var category = await context.Category.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
                return NotFound(new { message = "Categoria não encontrada" });

            try
            {
                context.Category.Remove(category);
                await context.SaveChangesAsync();
                return category;
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possível remover a categoria" });

            }
        }





        private readonly Category _defaultCategory = new Category
        {
            Id = 99,
            Name = "Patch Teste",
            Description = "Description Patch Test"
        };



        /// <summary>
        /// Path Example in Category.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        /// {
        ///     "op":"replace",
        ///     "path":"Name",
        ///     "value":"Its Something🌮"
        /// }
        ///
        /// </remarks>
        /// <param name="categoryPatch"></param> 

        /* 
        [HttpPatch("update")]
         public Task<ActionResult<Category>> Patch([FromBody] JsonPatchDocument<Category> categoryPatch)
         {

             categoryPatch.ApplyTo(_defaultCategory);
             return _defaultCategory;
         }
         */

        // VideoGameController.cs

         [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<Category> patchCategoria)
        {
            if (patchCategoria == null)
            {
                return BadRequest();
            }
            var categoriaDB = await _context.Category.FirstOrDefaultAsync(cat => cat.Id == id);
            if (categoriaDB == null)
            {
                return NotFound();
            }
            patchCategoria.ApplyTo(categoriaDB, ModelState);
            var isValid = TryValidateModel(categoriaDB);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }






}