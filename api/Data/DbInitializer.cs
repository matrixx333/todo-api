static class DbInitializer
{
    public static void Initialize(TodoContext context)
    {
        context.Database.EnsureCreated();

        if (context.Todos.Any())
        {
            return;
        }

        context.Todos.AddRange(
            new Todo
            {
                Title = "Create Todo API for Tonic",
                IsComplete = true
            },
            new Todo
            {
                Title = "Add 'tests' project to solution",
                IsComplete = true
            },
            new Todo
            {
                Title = "Build UI for the Todo API",
                DueDate = DateTime.Now.AddDays(21),
                IsComplete = false
            }
        );

        context.SaveChanges();
    }
}