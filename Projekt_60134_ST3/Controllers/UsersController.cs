using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_60134_ST3.Data;
using Projekt_60134_ST3.Models;
using Projekt_60134_ST3.Models.Domain;

namespace Projekt_60134_ST3.Controllers
{
	public class UsersController : Controller
	{
		private readonly MVCDbContext mvcDbContext;

		public UsersController(MVCDbContext mvcDemoDbContext)
		{
			this.mvcDbContext = mvcDemoDbContext;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var users = await mvcDbContext.Users.ToListAsync();
			return View(users);
		}

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Add(AddUserViewModel addUserRequest)
		{
			var user = new User()
			{
				Id = Guid.NewGuid(),
				Name = addUserRequest.Name,
				Email = addUserRequest.Email,
				DateOfBirth = addUserRequest.DateOfBirth
			};
			await mvcDbContext.Users.AddAsync(user);
			await mvcDbContext.SaveChangesAsync();
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> View(Guid id)
		{
			var user = await mvcDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

			if (user != null)
			{
				var viewModel = new UpdateUserViewModel()
				{
					Id = user.Id,
					Name = user.Name,
					Email = user.Email,
					DateOfBirth = user.DateOfBirth
				};
				return await Task.Run(() => View("View", viewModel));
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> View(UpdateUserViewModel model)
		{
			var user = await mvcDbContext.Users.FindAsync(model.Id);

			if (user != null)
			{
				user.Name = model.Name;
				user.Email = model.Email;
				user.DateOfBirth = model.DateOfBirth;

				await mvcDbContext.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Delete(UpdateUserViewModel model)
		{
			var user = await mvcDbContext.Users.FindAsync(model.Id);

			if (user != null)
			{
				mvcDbContext.Users.Remove(user);
				await mvcDbContext.SaveChangesAsync();

				return RedirectToAction("Index");
			}
			return RedirectToAction("Index");
		}
	}
}
