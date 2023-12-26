using Moq;

public class TodoServiceTests
{
    private readonly Mock<IRepository<Todo>> _mockRepository;
    private readonly TodoService _service;

    public TodoServiceTests()
    {
        _mockRepository = new Mock<IRepository<Todo>>();
        _service = new TodoService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllTodos_ReturnsAllTodos()
    {
        // Arrange
        var todos = new List<Todo> { new Todo { Id = 1, Title = "Test Todo" } };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _service.GetAllTodos();

        // Assert
        Assert.Equal(todos, result);
    }

    [Fact]
    public async Task GetTodoItemById_ReturnsTodo_WhenTodoExists()
    {
        // Arrange
        var todo = new Todo { Id = 1, Title = "Test Todo" };
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(todo);

        // Act
        var result = await _service.GetTodoItemById(1);

        // Assert
        Assert.Equal(todo, result);
    }

    [Fact]
    public async Task CreateTodoItem_ReturnsCreatedTodo()
    {
        // Arrange
        var todo = new Todo { Id = 1, Title = "Test Todo" };
        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Todo>())).ReturnsAsync(todo);

        // Act
        var result = await _service.CreateTodoItem(todo);

        // Assert
        Assert.Equal(todo, result);
    }

    [Fact]
    public void UpdateTodoItem_UpdatesTodo()
    {
        // Arrange
        var todo = new Todo { Id = 1, Title = "Test Todo" };
        _mockRepository.Setup(repo => repo.Update(It.IsAny<Todo>()));

        // Act
        _service.UpdateTodoItem(todo);

        // Assert
        _mockRepository.Verify(repo => repo.Update(It.IsAny<Todo>()), Times.Once);
    }

    [Fact]
    public void DeleteTodoItem_DeletesTodo()
    {
        // Arrange
        var todo = new Todo { Id = 1, Title = "Test Todo" };
        _mockRepository.Setup(repo => repo.Delete(It.IsAny<Todo>()));

        // Act
        _service.DeleteTodoItem(todo);

        // Assert
        _mockRepository.Verify(repo => repo.Delete(It.IsAny<Todo>()), Times.Once);
    }

    [Fact]
    public async Task GetCompletedTodos_ReturnsCompletedTodos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Test Todo", IsComplete = true },
            new Todo { Id = 2, Title = "Test Todo 2", IsComplete = false }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _service.GetCompletedTodos();

        // Assert
        Assert.Single(result);
        Assert.Equal(todos[0], result.First());
    }
}