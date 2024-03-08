using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;

namespace PRN231.ASS01.BookStoreAPI.Controllers
{
    public class AuthorsController : ODataController
    {
        private readonly IRepository<Author> _repository;
        public AuthorsController(IRepository<Author> repository)
        {
            _repository = repository;
        }
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.FindAllAsync(a => a.BookAuthors));
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entiy = await _repository.FindByIdAsync(a => a.AuthorId == key, a => a.BookAuthors);
            if (entiy == null)
            {
                return NotFound();
            }
            return Ok(entiy);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Author entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(await _repository.AddAsync(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Author entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != entity.AuthorId)
            {
                return BadRequest();
            }

            return Updated(await _repository.UpdateAsync(entity));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _repository.DeleteAsync(key);
            return NoContent();
        }
    }
}
