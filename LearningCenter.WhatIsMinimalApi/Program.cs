using LearningCenter.WhatIsMinimalApi.Entity;
using LearningCenter.WhatIsMinimalApi.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LiftDb>(opt => opt.UseInMemoryDatabase("LiftList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var liftsEndPoint = app.MapGroup("/lifts");

//Before
//app.MapGet("/lifts", async (LiftDb db) =>
//    await db.Lifts.ToListAsync());

//After
liftsEndPoint.MapGet("/", async (LiftDb db) =>
    await db.Lifts.ToListAsync());


liftsEndPoint.MapGet("/squats", async (LiftDb db) =>
    await db.Lifts.Where(t => t.Name == Lift.LiftName.Squat).ToListAsync());


liftsEndPoint.MapGet("/{id}", async (int id, LiftDb db) =>
    await db.Lifts.FindAsync(id)
        is Lift todo
            ? Results.Ok(todo)
            : Results.NotFound());

liftsEndPoint.MapPost("/", async (Lift lift, LiftDb db) =>
{
    db.Lifts.Add(lift);
    await db.SaveChangesAsync();

    return Results.Created($"/{lift.Id}", lift);
});

liftsEndPoint.MapPut("/{id}", async (int id, Lift inputLift, LiftDb db) =>
{
    var lift = await db.Lifts.FindAsync(id);

    if (lift is null) return Results.NotFound();

    lift.Name = inputLift.Name;
    lift.Weight = inputLift.Weight;
    lift.Reps = inputLift.Reps;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

liftsEndPoint.MapDelete("/{id}", async (int id, LiftDb db) =>
{
    if (await db.Lifts.FindAsync(id) is Lift todo)
    {
        db.Lifts.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});
app.Run();
