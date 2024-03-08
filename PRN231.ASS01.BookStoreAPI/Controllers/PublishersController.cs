using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using PRN231.ASS01.Repository.Models;
using PRN231.ASS01.Repository.Repository;

namespace PRN231.ASS01.BookStoreAPI.Controllers
{
    public class PublishersController : ODataController
    {
        private readonly IRepository<Publisher> _publisherRepository;
        public PublishersController(IRepository<Publisher> publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok(await _publisherRepository.FindAllAsync(p => p.Users, p => p.Books));
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var publisher = await _publisherRepository.FindByIdAsync(p => p.PubId == key, p => p.Users, p => p.Books);
            if (publisher == null)
            {
                return NotFound();
            }
            return Ok(publisher);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Publisher publisher)
        {
            //Console.WriteLine("Post publisher");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Created(await _publisherRepository.AddAsync(publisher));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Publisher publisher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != publisher.PubId)
            {
                return BadRequest();
            }

            return Updated(await _publisherRepository.UpdateAsync(publisher));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _publisherRepository.DeleteAsync(key);
            return NoContent();
        }
    }
}
