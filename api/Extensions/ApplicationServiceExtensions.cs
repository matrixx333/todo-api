using Microsoft.EntityFrameworkCore;

static class ApplicationServiceExtensions
{
    public static void AddTodoDatabaseContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TodoContext>(options => options.UseSqlite(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();
    }

    public static void AddTodoRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Todo>, EntityFrameworkRepository<Todo>>();
    }

    public static void AddTodoServices(this IServiceCollection services)
    {
        services.AddScoped<TodoService>();    
    }
}


