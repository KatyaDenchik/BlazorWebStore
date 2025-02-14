using AutoMapper;
using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using DataAccessLayer.Repository.Interfaces;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            var products = await productRepository.GetAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = (await productRepository.GetByIdAsync(id));
            return mapper.Map<ProductDTO>(product);
        }

        public async Task AddOrUpdateProductAsync(ProductDTO productDto)
        {
            var productEntity = mapper.Map<ProductEntity>(productDto);
            await productRepository.CreateAsync(productEntity);
        }

        public async Task DeleteProductAsync(int id)
        {
            await productRepository.DeleteByIdAsync(id);
        }
    }
}
