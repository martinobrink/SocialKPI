using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SocialKpiApi.Models;
using SocialKpiApi.Infrastructure.AutoMapper;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "";

/*
if (Debugger.IsAttached)
{
    connectionString = builder.Configuration.GetConnectionString("SocialKpi") ?? "Data Source=socialKpi.db";
    builder.Services.AddSqlite<SocialKpiDbContext>(connectionString);
} 
else
{
    connectionString = builder.Configuration.GetConnectionString("dbConnectionString");

    builder.Services.AddEntityFrameworkNpgsql();
    builder.Services.AddDbContext<SocialKpiDbContext>(options =>
    {
        options.UseNpgsql(connectionString);
    });
}*/
connectionString = builder.Configuration.GetConnectionString("dbConnectionString");

builder.Services.AddEntityFrameworkNpgsql();
builder.Services.AddDbContext<SocialKpiDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddEndpointsApiExplorer();
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

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{builder.Environment.ApplicationName} v1"));
app.MapFallback(() => Results.Redirect("/swagger"));

UpdateDatabase(app);

// Event endpoints.
app.MapGet("/event", async (SocialKpiDbContext db) =>
{
    var events = await db.Events.ToListAsync();
    var eventsOutput = mapper.Map<List<Event>, List< EventOutput>> (events);

    // Get participants.
    var currentEventCount = 0;
    foreach (var ev in events)
    {
        var registrations = await db.EventRegistrations.Where(er => er.EventId == ev.Id).ToListAsync();
        foreach (var registration in registrations)
        {
            var employee = await db.Employees.FirstOrDefaultAsync(emp => emp.Id == registration.EmployeeId);
            eventsOutput[currentEventCount].Participants?.Add(mapper.Map<Employee, EmployeeOutput>(employee));
        }

        currentEventCount++;
    }
    
    return Results.Ok(eventsOutput);
});

app.MapGet("/event/{id}", async (SocialKpiDbContext db, int id) =>
{
    var existingEvent = await db.Events.FindAsync(id);

    if (existingEvent == null)
    {
        return Results.NotFound();
    }

    var eventOutput = mapper.Map<Event, EventOutput>(existingEvent);

    // Get event registrations.
    var registrations = await db.EventRegistrations.Where(er => er.EventId == existingEvent.Id).ToListAsync();
    foreach (var registration in registrations)
    {
        var employee = await db.Employees.FirstOrDefaultAsync(emp => emp.Id == registration.EmployeeId);
        eventOutput.Participants?.Add(mapper.Map<Employee, EmployeeOutput>(employee));
    }

    return Results.Ok(eventOutput);
});

app.MapPost("/event", async (IMapper mapper, SocialKpiDbContext db, EventInput inputEvent) =>
{
    var dbEntity = mapper.Map<EventInput, Event>(inputEvent);

    if (inputEvent.Participants != null && dbEntity.Participants != null)
    {
        foreach (var participant in inputEvent.Participants)
        {
            // Get an existing employee for the given initials.
            var existingEmployee = await db.Employees.FirstOrDefaultAsync(e => e.Initials == participant.Initials);
            if (existingEmployee != null)
            {
                // Add the existing employee to the Event entity.
                dbEntity.Participants[dbEntity.Participants.FindIndex(p => p.Initials == existingEmployee.Initials)] = existingEmployee;
            }
        }
    }

    await db.Events.AddAsync(dbEntity);
    await db.SaveChangesAsync();

    var outputEvent = mapper.Map<Event, EventOutput>(dbEntity);

    return Results.Created($"/event/{dbEntity.Id}", outputEvent);
});

app.MapPut("/event/{id}", async (IMapper mapper, SocialKpiDbContext db, int id, EventInput inputEvent) =>
{
    if (!await db.Events.AnyAsync(x => x.Id == id))
    {
        return Results.NotFound();
    }

    // Get current event from DB and map the input to it.
    var eventToUpdate = await db.Events.FirstOrDefaultAsync(x => x.Id == id);
    mapper.Map(inputEvent, eventToUpdate);

    db.Update(eventToUpdate);
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

// Employee endpoints.
app.MapGet("/employee", async (SocialKpiDbContext db) =>
{
    return mapper.Map<List<Employee>, List<EmployeeOutput>>(await db.Employees.ToListAsync());
});

app.MapGet("/employee/{id}", async (SocialKpiDbContext db, int id) =>
{
    return await db.Employees.FindAsync(id) switch
    {
        Employee foundEmployee => Results.Ok(mapper.Map<Employee, EmployeeOutput>(foundEmployee)),
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
    if (!await db.Employees.AnyAsync(x => x.Id == id))
    {
        return Results.NotFound();
    }

    var employeeToUpdate = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
    mapper.Map(inputEmployee, employeeToUpdate);

    db.Update(employeeToUpdate);
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


static void UpdateDatabase(IApplicationBuilder app)
{
    try
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        serviceScope?.ServiceProvider.GetService<SocialKpiDbContext>()?.Database.Migrate();
    }
    catch (Exception)
    {
        if (Debugger.IsAttached)
        {
            Debugger.Break();
        }
    }
}