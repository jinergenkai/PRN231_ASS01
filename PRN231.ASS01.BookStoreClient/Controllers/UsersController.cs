using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PRN231.ASS01.BookStoreClient.Client;
using PRN231.ASS01.Repository.Models;

namespace PRN231.ASS01.BookStoreClient.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserClient _client;
        private readonly PublisherClient _publisherClient;
        private readonly RoleClient _roleClient;
        public UsersController(UserClient client, PublisherClient publisherClient, RoleClient roleClient)
        {
            _client = client;
            _publisherClient = publisherClient;
            _roleClient = roleClient;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {

            return View(await _client.GetAll());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View(await _client.GetById(id));
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName");
            ViewData["RoleId"] = new SelectList(await _roleClient.GetAll(), "RoleId", "RoleDesc");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailAddress,Password,Source,FirstName,MiddleName,LastName,RoleId,PubId,HireDate")] User user)
        {
            if (ModelState.IsValid)
            {
                await _client.Add(user);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName", user.PubId);
            ViewData["RoleId"] = new SelectList(await _roleClient.GetAll(), "RoleId", "RoleDesc", user.RoleId);
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _client.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName", user.PubId);
            ViewData["RoleId"] = new SelectList(await _roleClient.GetAll(), "RoleId", "RoleDesc", user.RoleId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,EmailAddress,Password,Source,FirstName,MiddleName,LastName,RoleId,PubId,HireDate")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _client.Update(id, user);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PubId"] = new SelectList(await _publisherClient.GetAll(), "PubId", "PublisherName", user.PubId);
            ViewData["RoleId"] = new SelectList(await _roleClient.GetAll(), "RoleId", "RoleDesc", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            return View(await _client.GetById(id));
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _client.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
