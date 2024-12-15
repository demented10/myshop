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
    public class AddCategoryService
    {
        private readonly ICategoryRepository<Category> _categoryRepository;

        public AddCategoryService(ICategoryRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDto>> AddCategoryAsync(CategoryDto categoryDto, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category
                {
                    Name = categoryDto.name
                };

                await _categoryRepository.CreateAsync(category);
                return Result.Ok(new CategoryDto(category.Id, category.Name));

            }
            catch(Exception ex)
            {
                return Result.Fail("Не удалось добавить категорию")
                    .WithError(ex.Message)
                    .WithError(ex.StackTrace);
            }
        }

    }
}
