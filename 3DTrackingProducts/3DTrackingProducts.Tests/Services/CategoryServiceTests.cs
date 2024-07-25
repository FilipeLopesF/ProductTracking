using _3DTrackingProducts.Api.Data;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DTrackingProductsTests.Services
{
    public class CategoryServiceTests
    {
        private async Task<ApplicationDbContext> GetApplicationDbContextAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            if (await _context.Categories.CountAsync() <= 0)
            {

                for (var i = 0; i < 10; i++)
                {
                    _context.Categories.Add(
                        new Category
                        {
                            Name = i.ToString(),
                        }
                    );

                    await _context.SaveChangesAsync();
                }
            }

            return _context;
        }

        private ApplicationDbContext GetEmptyDatabaseApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
            var _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
            return _context;
        }

        [Fact]
        public async void GetAllCategories_returnsEmptyList_whenDataNotFound()
        {
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetAllCategoriesAsync();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<Category>>();
            result.As<List<Category>>().Should().BeEmpty();
        }

        [Fact]
        public async void GetAllCategories_returnsListCategories_whenDataFound()
        {
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetAllCategoriesAsync();

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<List<Category>>();
            result.As<List<Category>>().Count().Should().Be(10);
        }

        [Fact]
        public async void GetCategoryById_returnsCategory_whenCategoryWithIdExists()
        {
            int id = 1;
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Category>();
        }

        [Fact]
        public async void GetCategoryById_returnsNull_whenCategoryWithIdDoesntExist()
        {
            int id = 100;
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetByIdAsync(id);

            result.Should().BeNull();
        }

        [Fact]
        public async void GetCategoryById_returnsNull_whenDataNotFound()
        {
            int id = 100;
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetByIdAsync(id);

            result.Should().BeNull();
        }

        [Fact]
        public async void GetCategoryByName_returnsCategory_whenCategoryWithNameExists()
        {
            int id = 1;
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetByNameAsync(id.ToString());

            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Category>();
        }

        [Fact]
        public async void GetCategoryByName_returnsNull_whenCategoryWithNameDoesntExists()
        {
            int id = 100;
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetByNameAsync(id.ToString());

            result.Should().BeNull();
        }

        [Fact]
        public async void GetCategoryByName_returnsNull_whenDataNotFound()
        {
            int id = 100;
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.GetByNameAsync(id.ToString());

            result.Should().BeNull();
        }

        [Fact]
        public async void AddCategory_returnsTrue_whenCategoryInserted()
        {
            string name = 20.ToString();
            Category newCategory = new Category { Name = name };
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.AddCategoryAsync(newCategory);

            result.Should().BeTrue();

            var confirmResult = await service.GetByNameAsync(name);

            confirmResult.Should().NotBeNull();
            confirmResult.Should().BeAssignableTo<Category>();
        }

        [Fact]
        public async void DeleteCategory_returnsTrue_whenCategoryExists()
        {
            int id = 1;
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.DeleteCategoryAsync(id);

            result.Should().BeTrue();

            var confirmResult = await service.GetByIdAsync(id);
            
            confirmResult.Should().BeNull();
        }

        [Fact]
        public async void DeleteCategory_returnsFalse_whenCategoryDoesntExists()
        {
            int id = 1;
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.DeleteCategoryAsync(id);

            result.Should().BeFalse();
        }

        [Fact]
        public async void UpdateCategory_returnsTrue_whenCategoryExists()
        {
            Category newValuesCategory = new Category { Name = "Try" };
            int id = 1;
            var dbContext = await GetApplicationDbContextAsync();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.UpdateCategoryAsync(id, newValuesCategory);

            result.Should().BeTrue();

            var confirmResult = await service.GetByIdAsync(id);

            confirmResult.Should().NotBeNull();
            confirmResult.Should().BeAssignableTo<Category>();
            confirmResult.As<Category>().Name.Should().Be(newValuesCategory.Name);
        }

        [Fact]
        public async void UpdateCategory_returnsFalse_whenCategoryDoesntExists()
        {
            Category newValuesCategory = new Category { Name = "Try" };
            int id = 1;
            var dbContext = GetEmptyDatabaseApplicationDbContext();
            var service = new UnitOfWork(dbContext).Category;

            var result = await service.UpdateCategoryAsync(id, newValuesCategory);

            result.Should().BeFalse();
        }

    }
}
