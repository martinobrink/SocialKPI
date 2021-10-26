using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SocialKpiApi.Models;
using SocialKpiApi.Infrastructure.AutoMapper;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Todos") ?? "Data Source=Todos.db";

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<SocialKpiDbContext>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = builder.Environment.ApplicationName, Version = "v1" });
});

// Add AutoMapper as a service.
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new EventAutoMapperProfile());
    mc.AddProfile(new EmployeeAutoMapperProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
}


app.MapGet("/todos", async (SocialKpiDbContext db) =>
{
    return await db.Todos.ToListAsync();
});

app.MapGet("/todos/{id}", async (SocialKpiDbContext db, int id) =>
{
    return await db.Todos.FindAsync(id) switch
    {
        Todo todo => Results.Ok(todo),
        null => Results.NotFound()
    };
});

app.MapPost("/todos", async (SocialKpiDbContext db, Todo todo) =>
{
    await db.Todos.AddAsync(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todo/{todo.Id}", todo);
});

app.MapPut("/todos/{id}", async (SocialKpiDbContext db, int id, Todo todo) =>
{
    if (id != todo.Id)
    {
        return Results.BadRequest();
    }

    if (!await db.Todos.AnyAsync(x => x.Id == id))
    {
        return Results.NotFound();
    }

    db.Update(todo);
    await db.SaveChangesAsync();

    return Results.Ok();
});


app.MapDelete("/todos/{id}", async (SocialKpiDbContext db, int id) =>
{
    var todo = await db.Todos.FindAsync(id);
    if (todo is null)
    {
        return Results.NotFound();
    }

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();

    return Results.Ok();
});

// Event endpoints.
app.MapGet("/event", async (SocialKpiDbContext db) =>
{
    return await db.Events.ToListAsync();
});

app.MapGet("/event/{id}", async (SocialKpiDbContext db, int id) =>
{
    return await db.Events.FindAsync(id) switch
    {
        Event foundEvent => Results.Ok(foundEvent),
        null => Results.NotFound()
    };
});

app.MapPost("/event", async (IMapper mapper, SocialKpiDbContext db, EventInput inputEvent) =>
{
    var dbEntity = mapper.Map<EventInput, Event>(inputEvent);

    await db.Events.AddAsync(dbEntity);
    await db.SaveChangesAsync();

    return Results.Created($"/event/{dbEntity.Id}", dbEntity);
});

app.MapPut("/event/{id}", async (IMapper mapper, SocialKpiDbContext db, int id, EventInput inputEvent) =>
{
    var dbEntity = mapper.Map<EventInput, Event>(inputEvent);

    if (!await db.Events.AnyAsync(x => x.Id == id))
    {
        return Results.NotFound();
    }

    db.Update(dbEntity);
    await db.SaveChangesAsync();

    return Results.Ok();
});


app.MapDelete("/event/{id}", async (SocialKpiDbContext db, int id) =>
{
    var eventToDelete = await db.Events.FindAsync(id);
    if (eventToDelete is null)
    {
        return Results.NotFound();
    }

    db.Events.Remove(eventToDelete);
    await db.SaveChangesAsync();

    return Results.Ok();
});

// Employee.
app.MapGet("/employee", async (SocialKpiDbContext db) =>
{
    return await db.Employees.ToListAsync();
});


app.MapGet("/employee/{id}", async (SocialKpiDbContext db, int id) =>
{
    return await db.Employees.FindAsync(id) switch
    {
        Employee foundEmployee => Results.Ok(foundEmployee),
        null => Results.NotFound()
    };
});

app.MapPost("/employee", async (IMapper mapper, SocialKpiDbContext db, EmployeeInput inputEmployee) =>
{
    var dbEntity = mapper.Map<EmployeeInput, Employee>(inputEmployee);

    await db.Employees.AddAsync(dbEntity);
    await db.SaveChangesAsync();

    return Results.Created($"/employee/{dbEntity.Id}", dbEntity);
});

app.MapPut("/employee/{id}", async (IMapper mapper, SocialKpiDbContext db, int id, EmployeeInput inputEmployee) =>
{
    var dbEntity = mapper.Map<EmployeeInput, Employee>(inputEmployee);

    if (!await db.Employees.AnyAsync(x => x.Id == id))
    {
        return Results.NotFound();
    }

    db.Update(dbEntity);
    await db.SaveChangesAsync();

    return Results.Ok();
});


app.MapDelete("/employee/{id}", async (SocialKpiDbContext db, int id) =>
{
    var employeeToDelete = await db.Employees.FindAsync(id);
    if (employeeToDelete is null)
    {
        return Results.NotFound();
    }

    db.Employees.Remove(employeeToDelete);
    await db.SaveChangesAsync();

    return Results.Ok();
});

app.Run();
