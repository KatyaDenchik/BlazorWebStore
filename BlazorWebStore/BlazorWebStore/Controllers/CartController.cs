using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IEnumerable<CartItemDTO>> GetCartItems(int userId)
        {
            return await cartService.GetCartItemsAsync(userId);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDTO cartItem)
        {
            await cartService.AddOrUpdateCartItemAsync(cartItem);
            return Ok(new { Message = "Товар доданий до кошика" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            await cartService.RemoveCartItemAsync(id);
            return Ok(new { Message = "Товар видалено з кошика" });
        }

        [HttpDelete("clear/{userId}")]
        public async Task<IActionResult> ClearCart(int userId)
        {
            await cartService.ClearCartAsync(userId);
            return Ok(new { Message = "Кошик очищений" });
        }
    }
}
