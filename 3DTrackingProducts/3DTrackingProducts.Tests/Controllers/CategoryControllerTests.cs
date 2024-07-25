using _3DTrackingProducts.Api.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using _3DTrackingProducts.Api.Persistence.Repositories;
using _3DTrackingProducts.Api.Models;
using _3DTrackingProducts.Api.Persistence;
using _3DTrackingProducts.Api.Controllers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using _3DTrackingProducts.Api.Resources;

namespace _3DTrackingProductsTests.Controllers
{
    public class CategoryControllerTests
    { 
        private UnitOfWork _unitOfWork;
        private ILogger<CategoryController> _logger;
        private IMapper _mapper;
        private CategoryController _categoryController;
        public CategoryControllerTests()
        {
            _unitOfWork = A.Fake<UnitOfWork>();
            _logger = A.Fake<ILogger<CategoryController>>();
            _mapper = A.Fake<IMapper>();

            _unitOfWork.Category = A.Fake<ICategoryRepository>();

            //SUT
            _categoryController = new CategoryController(_logger, _unitOfWork, _mapper);
        }

        [Fact]
        public async void CategoryController_getAllCategories()
        {
            //Arrange
            var categories = A.Fake<List<Category>>();
            A.CallTo(() => _unitOfWork.Category.GetAll()).Returns(categories);

            //Act
            var result = await _categoryController.getAllCategories();

            //Assert
            result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async void CategoryController_getCategoryById_returnsOk()
        {
            //Arrange
            var categorie = A.Fake<Category>();
           
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categorie);

            //Act
            var result = await _categoryController.getCategoryById(1);

            //Assert
            result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async void CategoryController_getCategoryById_returnsNotFound()
        {
            //Arrange
            Category? categorie = null;

            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categorie);

            //Act
            var result = await _categoryController.getCategoryById(1);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();

        }

        /*
        [Fact]
        public async void CategoryController_getCategoryByName_returnsOk()
        {
            //Arrange
            var categorie = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.CategoryRepository.GetByNameAsync(A<string>._)).Returns(categorie);

            //Act
            var result = await _categoryController.getCategoryByName("");

            //Assert
            result.Should().BeOfType<OkObjectResult>();

        }
        

        [Fact]
        public async void CategoryController_getCategoryByName_categoryDoesntExist_returnsNotFound()
        {
            //Arrange
            Category? categorie = null;

            A.CallTo(() => _unitOfWork.CategoryRepository.GetByNameAsync(A<string>._)).Returns(categorie);

            //Act
            var result = await _categoryController.getCategoryByName("");

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();

        }
        */
        [Fact]
        public async void CategoryController_AddCategory_returnsCreatedAtAction()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            categoryResource.Name = "Not empty String";
            var categorieMapped = A.Fake<Category>();
            Category? categoryAlreadyExists = null;

            A.CallTo(() => _mapper.Map<CategoryResource, Category>(A<CategoryResource>._)).Returns(categorieMapped);
            A.CallTo(() => _unitOfWork.Category.GetByNameAsync(A<string>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.AddCategoryAsync(A<Category>._)).Returns(true);

            //Act
            var result = await _categoryController.AddCategory(categoryResource);

            //Assert
            result.Should().BeOfType<CreatedAtActionResult>();

        }

        [Fact]
        public async void CategoryController_AddCategory_nullOrEmptyCategoryResourceName_returnsBadRequest()
        {
            //Arrange
            var categoryResource = new CategoryResource();      

            //Act
            var result = await _categoryController.AddCategory(categoryResource);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();

        }

        [Fact]
        public async void CategoryController_AddCategory_categoryAlreadyExist_returnsBadRequest()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            categoryResource.Name = "Not empty String";
            var categorieMapped = A.Fake<Category>();
            var categoryAlreadyExists = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<CategoryResource, Category>(A<CategoryResource>._)).Returns(categorieMapped);
            A.CallTo(() => _unitOfWork.Category.GetByNameAsync(A<string>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.AddCategoryAsync(A<Category>._)).Returns(true);

            //Act
            var result = await _categoryController.AddCategory(categoryResource);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();

        }

        [Fact]
        public async void CategoryController_AddCategory_AddCategoryAsyncFalse_returnsBadRequest()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            categoryResource.Name = "Not empty String";
            var categorieMapped = A.Fake<Category>();
            var categoryAlreadyExists = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<CategoryResource, Category>(A<CategoryResource>._)).Returns(categorieMapped);
            A.CallTo(() => _unitOfWork.Category.GetByNameAsync(A<string>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.AddCategoryAsync(A<Category>._)).Returns(false);

            //Act
            var result = await _categoryController.AddCategory(categoryResource);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();

        }

        [Fact]
        public async void CategoryController_UpdateCategory_returnsNoContent()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            categoryResource.Name = "Not empty String";
            var categorieMapped = A.Fake<Category>();
            var categoryAlreadyExists = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<CategoryResource, Category>(A<CategoryResource>._)).Returns(categorieMapped);
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.UpdateCategoryAsync(A<int>._,A<Category>._)).Returns(true);

            //Act
            var result = await _categoryController.UpdateCategory(1,categoryResource);

            //Assert
            result.Should().BeOfType<NoContentResult>();

        }

        [Fact]
        public async void CategoryController_UpdateCategory_nullOrEmptyCategoryResourceName_returnsBadRequest()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            
            //Act
            var result = await _categoryController.UpdateCategory(1, categoryResource);

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();

        }

        [Fact]
        public async void CategoryController_UpdateCategory_categoryDoesntExist_returnsNotFound()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            categoryResource.Name = "Not empty String";
            var categorieMapped = A.Fake<Category>();
            Category? categoryAlreadyExists = null;

            A.CallTo(() => _mapper.Map<CategoryResource, Category>(A<CategoryResource>._)).Returns(categorieMapped);
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categoryAlreadyExists);

            //Act
            var result = await _categoryController.UpdateCategory(1, categoryResource);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();

        }

        [Fact]
        public async void CategoryController_UpdateCategory_UpdatedCategoryAsyncFalse_returnsStatusCode500()
        {
            //Arrange
            var categoryResource = new CategoryResource();
            categoryResource.Name = "Not empty String";
            var categorieMapped = A.Fake<Category>();
            var categoryAlreadyExists = A.Fake<Category>();

            A.CallTo(() => _mapper.Map<CategoryResource, Category>(A<CategoryResource>._)).Returns(categorieMapped);
            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.UpdateCategoryAsync(A<int>._, A<Category>._)).Returns(false);

            //Act
            var result = await _categoryController.UpdateCategory(1, categoryResource);
            var resultAction = result as ObjectResult;

            //Assert
            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);

        }

        [Fact]
        public async void CategoryController_DeleteCategory_returnsNoContent()
        {
            //Arrange
            var categorieMapped = A.Fake<Category>();
            var categoryAlreadyExists = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.DeleteCategoryAsync(A<int>._)).Returns(true);

            //Act
            var result = await _categoryController.DeleteCategory(1);

            //Assert
            result.Should().BeOfType<NoContentResult>();

        }

        [Fact]
        public async void CategoryController_DeleteCategory_CategoryDoesntExist_returnsNotFound()
        {
            //Arrange
            Category? categoryAlreadyExists = null;

            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categoryAlreadyExists);

            //Act
            var result = await _categoryController.DeleteCategory(1);

            //Assert
            result.Should().BeOfType<NotFoundObjectResult>();

        }

        [Fact]
        public async void CategoryController_DeleteCategory_DeleteCategoryAsyncFalse_returnsStatusCode500()
        {
            //Arrange
            var categoryAlreadyExists = A.Fake<Category>();

            A.CallTo(() => _unitOfWork.Category.GetByIdAsync(A<int>._)).Returns(categoryAlreadyExists);
            A.CallTo(() => _unitOfWork.Category.DeleteCategoryAsync(A<int>._)).Returns(false);

            //Act
            var result = await _categoryController.DeleteCategory(1);
            var resultAction = result as ObjectResult;

            //Assert
            resultAction.Should().NotBeNull();
            resultAction.StatusCode.Should().Be(500);

        }

    }
}
