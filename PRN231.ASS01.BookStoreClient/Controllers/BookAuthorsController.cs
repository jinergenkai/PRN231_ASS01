using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN231.ASS01.BookStoreClient.Client;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.BookStoreClient.Controllers
{
    public class BookAuthorsController : Controller
    {
        private readonly BookAuthorClient _client;
        private readonly AuthorClient _authorClient;

        public BookAuthorsController(BookAuthorClient client, AuthorClient authorClient)
        {
            _client = client;
            _authorClient = authorClient;
        }

        // GET: BookAuthors
        public async Task<IActionResult> Index(int id)
        {
            ViewData["BookId"] = id.ToString();
            return View(await _client.GetAll(id));
        }

        //// GET: BookAuthors/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.BookAuthors == null)
        //    {
        //        return NotFound();
        //    }

        //    var bookAuthor = await _context.BookAuthors
        //        .Include(b => b.Author)
        //        .Include(b => b.Book)
        //        .FirstOrDefaultAsync(m => m.BookId == id);
        //    if (bookAuthor == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bookAuthor);
        //}

        //// GET: BookAuthors/Create
        public async Task<IActionResult> Create(int id)
        {
            ViewData["AuthorId"] = new SelectList(await _authorClient.GetAll(), "AuthorId", "FirstName");
            ViewData["BookId"] = id;
            return View();
        }

        // POST: BookAuthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorId,BookId,AuthorOrder,RoyalityPercentage")] BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                await _client.Add(bookAuthor);
                return RedirectToAction(nameof(Index), new { id = bookAuthor.BookId });
            }
            ViewData["AuthorId"] = new SelectList(await _authorClient.GetAll(), "AuthorId", "FirstName", bookAuthor.AuthorId);
            ViewData["BookId"] = bookAuthor.BookId;
            return View(bookAuthor);
        }

        //// GET: BookAuthors/Edit/5
        public async Task<IActionResult> Edit(int? bookId, int? authorId)
        {
            if (bookId == null || authorId == null)
            {
                return NotFound();
            }

            var bookAuthor = await _client.GetById(bookId, authorId);
            if (bookAuthor == null)
            {
                return NotFound();
            }
            return View(bookAuthor);
        }

        //// POST: BookAuthors/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("AuthorId,BookId,AuthorOrder,RoyalityPercentage")] BookAuthor bookAuthor)
        {


            if (ModelState.IsValid)
            {
                await _client.Update(bookAuthor.BookId, bookAuthor.AuthorId, bookAuthor);
                return RedirectToAction(nameof(Index), new { id = bookAuthor.BookId });
            }
            return View(bookAuthor);
        }

        // GET: BookAuthors/Delete/5
        public async Task<IActionResult> Delete(int? bookId, int? authorId)
        {
            if (bookId == null || authorId == null)
            {
                return NotFound();
            }

            var bookAuthor = await _client.GetById(bookId, authorId);
            if (bookAuthor == null)
            {
                return NotFound();
            }
            return View(bookAuthor);
        }

        //// POST: BookAuthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int bookId, int authorId)
        {
            await _client.Delete(bookId, authorId);
            return RedirectToAction(nameof(Index), new { id = bookId });
        }


    }
}
