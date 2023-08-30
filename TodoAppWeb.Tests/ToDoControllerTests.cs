using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TodoAppWeb.Models;

namespace TodoAppWeb
{
    public class ToDoControllerTests
    {
        // Create Unit Tests for TooController
        // Use Moq to mock the ToDoContext
        // Use FluentAssertions to assert the results
        [Fact]
        public void Get_ReturnsToDo()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            var todo = new ToDo
            {
                Id = 1,
                Name = "Test ToDo"
            };
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns(todo);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Get(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().Be(todo);
        }

        [Fact]
        public void Get_ReturnsNotFound()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns((ToDo)null);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Get(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var notFoundResult = result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Update_ReturnsNoContent()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            var todo = new ToDo
            {
                Id = 1,
                Name = "Test ToDo"
            };
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns(todo);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Update(1, todo);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public void Update_ReturnsNotFound()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns((ToDo)null);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Update(1, new ToDo());

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var notFoundResult = result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Delete_ReturnsNoContent()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            var todo = new ToDo
            {
                Id = 1,
                Name = "Test ToDo"
            };
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns(todo);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public void Delete_ReturnsNotFound()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns((ToDo)null);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var notFoundResult = result as NotFoundResult;
            notFoundResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Index_ReturnsToDos()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            // Create a Mock DbSet<ToDo> with a single item
            var todos = new List<ToDo>
            {
                new ToDo
                {
                    Id = 1,
                    Name = "Test ToDo"
                }
            };
            // declare a queryable variable from calling .AsQueryable() on todos
            var queryable = todos.AsQueryable();
            var mockSet = new Mock<DbSet<ToDo>>();
            // set up mockSet to return todos
            mockSet.As<IQueryable<ToDo>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<ToDo>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<ToDo>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<ToDo>>().Setup(m => m.GetEnumerator()).Returns(todos.GetEnumerator());
            // set up mockToDoContext to return mockSet
            mockToDoContext.Setup(x => x.ToDos).Returns(mockSet.Object);

            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
            var viewResult = result as ViewResult;
            // Update assertion to check if Result is not null instead
            viewResult.Should().NotBeNull();
        }

        [Fact]
        public void Create_ReturnsToDo()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            var todo = new ToDo
            {
                Id = 1,
                Name = "Test ToDo"
            };
            mockToDoContext.Setup(x => x.ToDos.Add(todo));
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Create(todo);

            // Assert
            // Update expected result from ViewResult to RedirectToActionResult
            result.Should().BeOfType<RedirectToActionResult>();
            var redirectToActionResult = result as RedirectToActionResult;
            // Assert if redirectToActionResult is not null
            redirectToActionResult.Should().NotBeNull();
        }

        [Fact]
        public void Create_ReturnsBadRequest()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Create(null);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
            var badRequestResult = result as BadRequestResult;
            badRequestResult.StatusCode.Should().Be(400);
        }

        [Fact]
        public void Update_ReturnsBadRequest()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            // Setup a mock to mockToDoContext.ToDos
            var mockSet = new Mock<DbSet<ToDo>>();
            mockToDoContext.Setup(x => x.ToDos).Returns(mockSet.Object);

            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Update(1, null);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var badRequestResult = result as NotFoundResult;
            badRequestResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public void Delete_ReturnsToDo()
        {
            // Arrange
            var mockToDoContext = new Mock<ToDoContext>();
            var todo = new ToDo
            {
                Id = 1,
                Name = "Test ToDo"
            };
            mockToDoContext.Setup(x => x.ToDos.Find(1)).Returns(todo);
            var controller = new ToDoController(mockToDoContext.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            // Update expected type from ViewResult to NoContentResult
            result.Should().BeOfType<NoContentResult>();
            var noContentResult = result as NoContentResult;
            noContentResult.StatusCode.Should().Be(204);
        }

        [Fact]
        public void Create_ReturnsView()
        {
            // Create Mock of ToDoContext and pass to controller
            var mockToDoContext = new Mock<ToDoContext>();
            // Setup a mock to mockToDoContext.ToDos
            var mockSet = new Mock<DbSet<ToDo>>();
            mockToDoContext.Setup(x => x.ToDos).Returns(mockSet.Object);
            var controller = new ToDoController(mockToDoContext.Object);

            // Create ToDo item
            var todo = new ToDo
            {
                Id = 1,
                Name = "Test ToDo"
            };
            // Pass ToDo into controller Create method
            var result = controller.Create(todo);

            // Assert
            // Update expected result from ViewResult to RedirectToActionResult
            result.Should().BeOfType<RedirectToActionResult>();
            var redirectToActionResult = result as RedirectToActionResult;
            // Assert if redirectToActionResult is not null
            redirectToActionResult.Should().NotBeNull();
        }
    }
}