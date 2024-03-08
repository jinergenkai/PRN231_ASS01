using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN231.ASS01.BookStoreClient.Client;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.BookStoreClient.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookClient _bookClient;
        private readonly PublisherClient _publisherClient;

        public BooksController(BookClient bookClient, PublisherClient publisherClient)
        {
            _bookClient = bookClient;
            _publisherClient = publisherClient;
        }

        // GET: Books
        public async Task<IActionResult> Index(string searchName, decimal minPrice = 0, decimal maxPrice = 1000000)
        {
            ViewData["searchName"] = searchName;
            ViewData["minPrice"] = minPrice;
            ViewData["maxPrice"] = maxPrice;
            return View(await _bookClient.GetAll(searchName, minPrice, maxPrice));
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View(await _bookClient.GetById(id));
        }

        // GET: Books/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Type,PubId,Price,Advance,Royalty,YtdSales,Notes,PublishedDate")] Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookClient.Add(book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName", book.PubId);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookClient.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName", book.PubId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Type,PubId,Price,Advance,Royalty,YtdSales,Notes,PublishedDate")] Book book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _bookClient.Update(id, book);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName", book.PubId);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View(await _bookClient.GetById(id));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookClient.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(string searchName, decimal minPrice, decimal maxPrice)
        {
            return RedirectToAction(nameof(Index), new { searchName, minPrice, maxPrice });
        }

    }
}
