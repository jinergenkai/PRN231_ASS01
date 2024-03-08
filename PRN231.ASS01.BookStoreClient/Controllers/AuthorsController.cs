using Microsoft.AspNetCore.Mvc;
using PRN231.ASS01.BookStoreClient.Client;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.BookStoreClient.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly AuthorClient _client;

        public AuthorsController(AuthorClient client)
        {
            _client = client;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _client.GetAll());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View(await _client.GetById(id));
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LastName,FirstName,Phone,Address,City,State,ZipCode,EmailAddress")] Author author)
        {
            if (ModelState.IsValid)
            {
                await _client.Add(author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return View(await _client.GetById(id));
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorId,LastName,FirstName,Phone,Address,City,State,ZipCode,EmailAddress")] Author author)
        {
            if (id != author.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _client.Update(id, author);
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View(await _client.GetById(id));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
