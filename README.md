![example workflow](https://github.com/matrixx333/todo-api/actions/workflows/dotnet.yml/badge.svg)

# Project Summary

Hi Tonic Team! 

I chose to implement my solution as an ASP.NET Core Web API solution. The main entry point for the application is in the Program.cs file. Below is an overview of how to get started using the project, as well as an API endpoint overview. 

## Features

- Use C# and .NET
    - ASP.NET Core Web API
- Create a database
    - SQLite implementation
- Implement all CRUD actions

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository: `git clone https://github.com/matrixx333/todo-api.git`
2. Once the repo has been cloned, you can build the project by: 

```sh
    cd .\todo-api\api
    dotnet build
```
3. To start the server run: 

```sh
    dotnet run
```
4. To test the application:

```sh
    cd ..\tests
    dotnet test
```

## api\Program.cs

The Program.cs file is the entry point for the application. Below is an overview of the API endpoints provided by the application: 

### GET /todos

Gets all todo items.

- Method: `GetAllTodos()`
- Response: A list of `Todo` objects

### GET /todos/complete

Gets all completed todo items.

- Method: `GetCompletedTodos()`
- Response: A list of completed `Todo` objects

### GET /todos/{id}

Gets a specific todo item by ID.

- Method: `GetTodoItemById(int id)`
- Path parameter: `id` (integer)
- Response: The `Todo` object with the specified ID, or `null` if not found

### POST /todos

Creates a new todo item.

- Method: `CreateTodoItem(Todo todo)`
- Request body: A `Todo` object
- Response: The created `Todo` object

### PUT /todos/{id}

Updates a specific todo item.

- Method: `UpdateTodoItem(Todo todo)`
- Path parameter: `id` (integer)
- Request body: A `Todo` object with the updated details
- Response: The updated `Todo` object, or `null` if not found

### DELETE /todos/{id}

Deletes a specific todo item by ID.

- Method: `DeleteTodoItem(int id)`
- Path parameter: `id` (integer)
- Response: A confirmation message if the `Todo` object with the specified ID was deleted, or an error message if not found

## tests\TodoServiceTests.cs

This file contains a handful of simple service tests to ensure that the project is working as expected. 