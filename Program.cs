using Microsoft.EntityFrameworkCore;
using ToDoApi;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TasksContex>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");

app.MapGet("/", () => "Hello World!");
app.MapGet("/items", async(TasksContex tc)=>
await tc.Items.ToListAsync());

app.MapPost("/items", async (Item item, TasksContex tc) =>
{
    var todoItem = new Item
    {
        IsComplete = item.IsComplete,
        Name = item.Name
    };

    tc.Items.Add(todoItem);
    await tc.SaveChangesAsync();

    return Results.Created($"/items/{todoItem.Id}",todoItem);
});

app.MapPut("/items", async (int Id, TasksContex tc) =>
{
    var todo = await tc.Items.FindAsync(Id);

    if (todo is null) return Results.NotFound();

   if( todo.IsComplete == true)
    todo.IsComplete = false;
    else 
     todo.IsComplete = true;
    await tc.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/items", async (int Id, TasksContex tc) =>
{
    if (await tc.Items.FindAsync(Id) is Item todo)
    {
        tc.Items.Remove(todo);
        await tc.SaveChangesAsync();
        return Results.Ok(todo);
    }

    return Results.NotFound();
});


    
app.Run();

