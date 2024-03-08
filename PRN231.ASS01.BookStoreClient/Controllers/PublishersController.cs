using Microsoft.AspNetCore.Mvc;
using PRN231.ASS01.BookStoreClient.Client;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.BookStoreClient.Controllers
{
    public class PublishersController : Controller
    {
        private readonly PublisherClient _client;
        public PublishersController(PublisherClient client)
        {
            _client = client;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            return View(await _client.GetAll());
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return View(await _client.GetById(id));
        }

        // GET: Publishers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherName,City,State,Country")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {

                await _client.Add(publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await _client.GetById(id));
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PubId,PublisherName,City,State,Country")] Publisher publisher)
        {
            if (id != publisher.PubId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _client.Update(id, publisher);
                return RedirectToAction(nameof(Index));
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            return View(await _client.GetById(id));
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
