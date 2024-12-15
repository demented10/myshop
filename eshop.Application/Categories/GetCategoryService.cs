using eshop.Application.eshop.Application.Products;
using eshop.Domain.Entities;
using eshop.Infrastructure.Repositories;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Application.Categories
{
    public class GetCategoryService
    {

        private readonly ICategoryRepository<Category> _categoryRepository;

        public GetCategoryService(ICategoryRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDto>> GetCategoryByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetByIdAsync(id,cancellationToken);
                return Result.Ok(new CategoryDto(category.Id, category.Name));
            }
            catch (Exception ex)
            {
                return Result.Fail($"Не удалось получить категорию с id {id}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }
        public async Task<Result<IEnumerable<CategoryDto>>> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var items = await _categoryRepository.GetAllAsync(cancellationToken);

                return Result.Ok(items.Select(i => new CategoryDto(i.Id,i.Name)));

            }
            catch (Exception ex)
            {
                return Result.Fail("Не удалось получить список категорий").WithError(ex.Message).WithError(ex.StackTrace);
            }
        }
        public async Task<Result<IEnumerable<ProductDto>>> GetAllProductsInCategoryAsync(int categoryId, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryWithProductsAsync(categoryId, cancellationToken);
                return Result.Ok(category.Products.Select(p => new ProductDto(p.Id, p.Name,p.Description,p.Price, p.CategoryId)));
            }
            catch (Exception ex)
            {
                return Result.Fail($"Не удалось получить товары для категории с id {categoryId}")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }

    }
}
