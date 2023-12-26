using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("TodosContextSQLite") ?? throw new Exception("Connection string is null");

builder.Services.AddTodoDatabaseContext(connectionString);    
builder.Services.AddTodoRepositories();
builder.Services.AddTodoServices();

// // Add services to the container.
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TodoContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

var todoItems = app.MapGroup("/todo-items");

todoItems.MapGet("/", GetAllTodos);
todoItems.MapGet("/complete", GetCompletedTodos);
todoItems.MapGet("/{id}", GetTodo);
todoItems.MapPost("/", CreateTodo);
todoItems.MapPut("/{id}", UpdateTodo);
todoItems.MapDelete("/{id}", DeleteTodo);

app.Run();

async Task<IResult> GetAllTodos(TodoService service)
{
    var todos = await service.GetAllTodos();
    return TypedResults.Ok(todos);
}

async Task<IResult> GetCompletedTodos(TodoService service)
{
    var completedTodos = await service.GetCompletedTodos();
    return TypedResults.Ok(completedTodos);
}

async Task<IResult> GetTodo(int id, TodoService service)
{
    return await service.GetTodoItemById(id)
        is Todo todo
            ? TypedResults.Ok(todo)
            : TypedResults.NotFound();
}

async Task<IResult> CreateTodo(Todo todo, TodoService service)
{
    var entity = await service.CreateTodoItem(todo);
    return TypedResults.Created($"/todo-items/{entity.Id}", entity);
}

async Task<IResult> UpdateTodo(int id, Todo request, TodoService service)
{
    var todo = await service.GetTodoItemById(id);
    
    if (todo is null) return TypedResults.NotFound();

    todo.Title = request.Title;
    todo.DueDate = request.DueDate;
    todo.IsComplete = request.IsComplete;

    service.UpdateTodoItem(todo);

    return TypedResults.NoContent();
}

async Task<IResult> DeleteTodo(int id, TodoService service)
{
    var todo = await service.GetTodoItemById(id);
    
    if (todo is not null) 
    {
        service.DeleteTodoItem(todo);
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}
