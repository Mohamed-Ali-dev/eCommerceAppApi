﻿using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Domain.Models;
using Microsoft.AspNetCore.Http;
using System.Linq.Expressions;

namespace eCommerceApp.Application.Services.Implementation
{
    public class ProductService(IGeneric<Product> productInterface,IGeneric<Category> categoryInterface, IMapper mapper,
        IHttpContextAccessor httpContextAccessor) : IProductService
    {
        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var rowData = await productInterface.GetAllAsync();
            if (!rowData.Any())
                return [];
            var getProducts = mapper.Map<IEnumerable<GetProductDto>>(rowData);
            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/";
            foreach (var product in getProducts)
            {
                if (!string.IsNullOrEmpty(product.Image))
                {
                    product.Image = baseUrl + product.Image;
                }
                product.Category = (await categoryInterface.GetAsync(c => c.Id == product.CategoryId)).Name;

            }

            return getProducts;
        }
        public async Task<GetProductDto> GetAsync(Expression<Func<Product, bool>> filter, bool tracking = false)
        {
            var productFromDb = await productInterface.GetAsync(filter,tracking);
            if (productFromDb == null)
                return new GetProductDto();
            var product =  mapper.Map<GetProductDto>(productFromDb);
            product.Category = (await categoryInterface.GetAsync(c => c.Id == product.CategoryId)).Name;

            var baseUrl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}/";

            if (!string.IsNullOrEmpty(product.Image))
            {
                product.Image = baseUrl + product.Image;
            }
            return product;
        }
        public async Task<ServiceResponse> AddAsync(CreateProductDto productDto)
        {
            var category = await categoryInterface.GetAsync(c => c.Id == productDto.CategoryId);
            if (category == null)
                return new ServiceResponse(false, "Category not found");
            
            if (productDto.Image == null)
                return new ServiceResponse(false, "No file uploaded");

            if (productDto.Image.Length > 2 * 1024 * 1024) 
            {
                return new ServiceResponse(false, "Image size exceeds 2MB.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(Path.GetExtension(productDto.Image.FileName).ToLower()))
            {
                return new ServiceResponse(false, "Invalid image format. Allowed: JPG, JPEG, PNG, GIF.");
            }
     
            var imagePath =await SaveImage(productDto.Image);
           
            var mappedData = mapper.Map<Product>(productDto);
            mappedData.Image = imagePath;

            int result = await productInterface.AddAsync(mappedData);

            return result > 0 ? new ServiceResponse(true, "Product added!")
             : new ServiceResponse(false, "Product failed to be added!");
        }
        public async Task<ServiceResponse> UpdateAsync(UpdateProductDto productDto)
        {
            var category = await categoryInterface.GetAsync(c => c.Id == productDto.CategoryId);
            if (category == null)
                return new ServiceResponse(false, "Category not found");
            var productFromDb = await productInterface.GetAsync(u => u.Id == productDto.Id, true);

            var product = mapper.Map<Product>(productDto);

            //string imagePath = "";
            if (productDto.Image != null)
            {
                //return new ServiceResponse(false, "No file uploaded");
                if (productDto.Image.Length > 2 * 1024 * 1024)
                {
                    return new ServiceResponse(false, "Image size exceeds 2MB.");
                }

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(Path.GetExtension(productDto.Image.FileName).ToLower()))
                {
                    return new ServiceResponse(false, "Invalid image format. Allowed: JPG, JPEG, PNG, GIF.");
                }


                if (productFromDb == null)
                    return new ServiceResponse(false, "Product not found");
                //remove old image
                if (!string.IsNullOrEmpty(productFromDb.Image))
                {
                    string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productFromDb.Image);
                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }
                var imagePath = await SaveImage(productDto.Image);
                product.Image = imagePath;

            }
                product.Image = productFromDb.Image;
            productFromDb.CategoryId = product.CategoryId;
            productFromDb.Price = product.Price;
            productFromDb.Quantity = product.Quantity;
            productFromDb.Description = product.Description;
            productFromDb.Image = product.Image;
            productFromDb.Name = product.Name;
            var result = await productInterface.UpdateAsync(productFromDb);
            return result > 0 ? new ServiceResponse(true, "Product updated!"):
                new ServiceResponse(false, "Product failed to be updated!");
        }
        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            var productFromDb = await productInterface.GetAsync(u => u.Id == id);
            if (productFromDb == null)
            {
                return new ServiceResponse(false, "Product not found");
            }
            if (!string.IsNullOrEmpty(productFromDb.Image))
            {
                string oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productFromDb.Image);
                if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                }
            }
            int result = await productInterface.DeleteAsync(id);

            return result > 0 ? new ServiceResponse(true, "Product deleted!")
                : new ServiceResponse(false, "Product not found or failed to be delete!");
        }

        private  async Task<string> SaveImage(IFormFile file)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "product-images");

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imagePath = Path.Combine("product-images", uniqueFileName).Replace("\\", "/");
            return imagePath;
        } 
    
    }
}
