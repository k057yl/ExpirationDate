using ExpirationDate.Models.DTO;
using ExpirationDate.Models.Entitys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ExpirationDate.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ExpirationDate.Models;
using System.Globalization;

namespace ExpirationDate.Controllers
{
    [Authorize]
    public class ItemController : BaseController<ItemController>
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly CurrencyConverterService _currencyConverterService;

        public ItemController(AppDbContext context, UserManager<IdentityUser> userManager, IStringLocalizer<ItemController> localizer, CurrencyConverterService currencyConverterService) : base(localizer)
        {
            _context = context;
            _userManager = userManager;
            _currencyConverterService = currencyConverterService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    string? imagePath = null;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(model.ImageFile.FileName);
                        var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        if (!Directory.Exists(uploads))
                        {
                            Directory.CreateDirectory(uploads);
                        }
                        var filePath = Path.Combine(uploads, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        imagePath = $"/images/{fileName}";
                    }

                    var item = new Item
                    {
                        Name = model.Name,
                        ExpirationDate = model.ExpirationDate,
                        ImagePath = imagePath,
                        Description = model.Description,
                        Rating = model.Rating,
                        Price = model.Price,
                        UserId = user.Id,
                    };

                    _context.Items.Add(item);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "User not found.");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserItems()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var items = await _context.Items
                .Where(i => i.UserId == user.Id)
                .ToListAsync();

            var currencyConversionTasks = items.Select(async item =>
            {
                var currentCulture = CultureInfo.CurrentCulture.Name;
                item.Price = await _currencyConverterService.ConvertPriceForLanguageAsync(item.Price, currentCulture);
            });

            await Task.WhenAll(currencyConversionTasks);

            return View(items);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null || item.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserItems", "Item");
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            var currentCulture = CultureInfo.CurrentCulture.Name;
            item.Price = await _currencyConverterService.ConvertPriceForLanguageAsync(item.Price, currentCulture);

            return View(item);
        }
    }
}
/*
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null || item.UserId != _userManager.GetUserId(User))
            {
                return NotFound();
            }

            var model = new ItemDTO
            {
                ItemId = item.ItemId,
                Name = item.Name,
                ExpirationDate = item.ExpirationDate,
                ImageFile = null,
                Description = item.Description,
                Rating = item.Rating,
                Price = item.Price
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ItemDTO model)
        {
            if (ModelState.IsValid)
            {
                var item = await _context.Items.FindAsync(model.ItemId);
                
                if (item == null || item.UserId != _userManager.GetUserId(User))
                {
                    return NotFound();
                }
                
                string imagePath = item.ImagePath;

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var fileName = Path.GetFileName(model.ImageFile.FileName);
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    var filePath = Path.Combine(uploads, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    imagePath = $"/images/{fileName}";
                }

                item.Name = model.Name;
                item.ExpirationDate = model.ExpirationDate;
                item.ImagePath = imagePath;
                item.Description = model.Description;
                item.Rating = model.Rating;
                item.Price = model.Price;

                _context.Items.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Item");
            }
            return View(model);
        }
        */