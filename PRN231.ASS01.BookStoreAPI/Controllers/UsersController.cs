using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;

namespace PRN231.ASS01.BookStoreAPI.Controllers
{

    public class UsersController : ODataController
    {
        private readonly IRepository<User> _repository;
        public UsersController(IRepository<User> repository)
        {
            _repository = repository;
        }
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.FindAllAsync(u => u.Publisher, u => u.Role));
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entiy = await _repository.FindByIdAsync(u => u.UserId == key, u => u.Publisher, u => u.Role);
            if (entiy == null)
            {
                return NotFound();
            }
            return Ok(entiy);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(await _repository.AddAsync(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] User entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != entity.UserId)
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
