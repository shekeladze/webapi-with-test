using System;
using System.Linq;
using webapi_with_test.webapi.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;
using webapi_with_test.webapi.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace webapi_with_test.test
{
    public class TodoUnitTest
    {
        private TodoContext _todoContext;

        public TodoUnitTest()
        {
            InitContext();
        }

        [Fact]
        public void TestGetAllTasks()
        {
            // Arrange
            var controller = new TodoController(_todoContext);

            // Act
            var response = controller.GetAll();

            // Assert
            Assert.Equal(3, response.Count());
            
            // // <PackageReference Include="FluentAssertions" Version="4.19.2" />
            // // Assert
            // var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            // var persons = okResult.Value.Should().BeAssignableTo<IEnumerable<Person>>().Subject;
            // persons.Count().Should().Be(50);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void TestGeTaskById(int id)
        {
            // Arrange
            var controller = new TodoController(_todoContext);

            // Act
            var response = controller.GetById(id);
            
            // Assert
            Assert.IsType(typeof(ObjectResult), response);
        }

        [Fact]
        public void TestGeTaskById_NotFound()
        {
            // Arrange
            var controller = new TodoController(_todoContext);

            // Act
            var response = controller.GetById(0);
            
            // Assert
            Assert.IsType(typeof(NotFoundResult), response);
        }

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("TodoList");
            _todoContext = new TodoContext(builder.Options);
        }
    }
}
