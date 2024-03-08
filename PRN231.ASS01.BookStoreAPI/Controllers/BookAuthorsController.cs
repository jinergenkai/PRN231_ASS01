using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;

namespace PRN231.ASS01.BookStoreAPI.Controllers
{

    public class BookAuthorsController : ODataController
    {
        private readonly IBookAuthorRepository _repository;
        public BookAuthorsController(IBookAuthorRepository repository)
        {
            _repository = repository;
        }
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.FindAllAsync());
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int keyAuthorId, [FromODataUri] int keyBookId)
        {
            var entiy = await _repository.FindByIdAsync(keyAuthorId, keyBookId);
            if (entiy == null)
            {
                return NotFound();
            }
            return Ok(entiy);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookAuthor entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(await _repository.AddAsync(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int keyAuthorId, [FromODataUri] int keyBookId, [FromBody] BookAuthor entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (keyAuthorId != entity.AuthorId || keyBookId != entity.BookId)
            {
                return BadRequest();
            }

            return Updated(await _repository.UpdateAsync(entity));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int keyAuthorId, [FromODataUri] int keyBookId)
        {
            await _repository.DeleteAsync(keyAuthorId, keyBookId);
            return NoContent();
        }
    }
}
