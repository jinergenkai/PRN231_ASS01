using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;

namespace PRN231.ASS01.BookStoreAPI.Controllers
{

    public class BooksController : ODataController
    {
        private readonly IRepository<Book> _repository;
        public BooksController(IRepository<Book> repository)
        {
            _repository = repository;
        }
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.FindAllAsync(b => b.Publisher, b => b.BookAuthors));
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entiy = await _repository.FindByIdAsync(b => b.BookId == key, b => b.Publisher, b => b.BookAuthors);
            if (entiy == null)
            {
                return NotFound();
            }
            return Ok(entiy);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Book entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(await _repository.AddAsync(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Book entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != entity.BookId)
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
