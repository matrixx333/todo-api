public class TodoService(IRepository<Todo> todoRepository)
{
    public Task<List<Todo>> GetAllTodos()
    {
        return todoRepository.GetAllAsync();
    }

    public Task<Todo?> GetTodoItemById(int id)
    {
        return todoRepository.GetByIdAsync(id);
    }

    public Task<Todo> CreateTodoItem(Todo todo)
    {
        return todoRepository.AddAsync(todo);
    }

    public void UpdateTodoItem(Todo todo)
    {
        todoRepository.Update(todo);
    }

    public void DeleteTodoItem(Todo todo)
    {
        todoRepository.Delete(todo);
    }

    public async Task<IEnumerable<Todo>> GetCompletedTodos()
    {
        var todos = await todoRepository.GetAllAsync();        
        return todos.Where(t => t.IsComplete);
    }   
}