using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BWSC.Data;
using BWSC.Models;
using BWSC.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BWSC.Models
{
    public class ProductsController : Controller
    {
        private readonly SwimmingClubContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ShoppingCartActions usersShoppingCart;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProductsController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SwimmingClubContext context, 
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
            usersShoppingCart = new ShoppingCartActions(_httpContextAccessor, _context);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var cartID = usersShoppingCart.GetCartId(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            //ViewData["CartCount"] = usersShoppingCart.GetCartItems().Count;
            List<Product> products = await _context.Products.ToListAsync();
            List<CartItem> cartItems = await _context.ShoppingCartItems
                .Where(i => i.CartId.Equals(cartID))
                .ToListAsync();

            var ProductsList = from p in products
                        join c in cartItems on p.ID equals c.ProductId into cp
                        from ProductCart in cp.DefaultIfEmpty()
                        select new {p.ID,p.ShortName,p.Description,p.SellingPrice, p.ImageFileName
                        , Quantity = (ProductCart == null ? 0 : ProductCart.Quantity
                        ) };

            ViewData["CartCount"] = ProductsList.Sum(cart => cart.Quantity);

            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Description,ImageFileName,SellingPrice,ShortName")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/AddToCart/5
        public async Task<IActionResult> AddToCart(int? id)
        {
            HttpContext.Session.SetString("a", "b");
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            usersShoppingCart.AddToCart(product.ID, this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index");
        }

        // POST: Products/AddToCart/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id, [Bind("ID,Description,ImageFileName,SellingPrice,ShortName")] Product product)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.ID == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}
