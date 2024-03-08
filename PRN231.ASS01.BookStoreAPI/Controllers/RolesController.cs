using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;

namespace PRN231.ASS01.BookStoreAPI.Controllers
{
    public class RolesController : ODataController
    {
        private readonly IRepository<Role> _repository;
        public RolesController(IRepository<Role> repository)
        {
            _repository = repository;
        }
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repository.FindAllAsync(r => r.Users));
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entiy = await _repository.FindByIdAsync(r => r.RoleId == key, r => r.Users);
            if (entiy == null)
            {
                return NotFound();
            }
            return Ok(entiy);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Role entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(await _repository.AddAsync(entity));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Role entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != entity.RoleId)
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
