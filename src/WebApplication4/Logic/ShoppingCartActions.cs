using BWSC.Data;
using BWSC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BWSC.Logic
{
    public class ShoppingCartActions : IDisposable
    {
        public string ShoppingCartId { get; set; }

        private SwimmingClubContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public ClaimsPrincipal User { get; private set; }

        public const string CartSessionKey = "CartId";

        public ShoppingCartActions(
            IHttpContextAccessor httpContextAccessor,
            SwimmingClubContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;

        }

        public void AddToCart(int id, String userID)
        {
            // Retrieve the product from the database.           
            var ShoppingCartId = GetCartId(userID);

            var cartItem = _context.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.ProductId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    ProductId = id,
                    CartId = ShoppingCartId,
                    Product = _context.Products.SingleOrDefault(
                   p => p.ID == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };

                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                //_context = null;
            }
        }

        public string GetCartId(String _userId)
        {
            if (_userId == null)
            {
                if (_session.GetString("CartID") == null)
                {
                    // Generate a new random GUID using System.Guid class.     
                    Guid tempCartId = Guid.NewGuid();
                    _session.SetString("CartID", tempCartId.ToString());
                }
            }
            else
            {
                _session.SetString("CartID", _userId);
            }
            return _session.GetString("CartID").ToString();
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return _context.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }

    }
}