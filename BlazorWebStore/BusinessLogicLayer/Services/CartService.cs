using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository.Interfaces;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CartService
    {
        private readonly ICartRepository cartRepository;
        private readonly IMapper mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<CartItemDTO>> GetCartItemsAsync(int userId)
        {
            var cartItems = await cartRepository.GetAsync(c => c.UserId == userId);
            return mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
        }

        public async Task AddOrUpdateCartItemAsync(CartItemDTO cartItemDto)
        {
            var cartItemEntity = mapper.Map<CartItemEntity>(cartItemDto);
            await cartRepository.CreateAsync(cartItemEntity);
        }

        public async Task RemoveCartItemAsync(int id)
        {
            await cartRepository.DeleteByIdAsync(id);
        }

        public async Task ClearCartAsync(int userId)
        {
            await cartRepository.DeleteAsync(c => c.UserId == userId);
        }
    }
}
