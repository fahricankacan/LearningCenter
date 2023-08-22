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

app.MapGet("/lifts", async (LiftDb db) =>
    await db.Lifts.ToListAsync());


app.MapGet("/lifts/squats", async (LiftDb db) =>
    await db.Lifts.Where(t => t.Name == Lift.LiftName.Squat).ToListAsync());


app.MapGet("/lifts/{id}", async (int id, LiftDb db) =>
    await db.Lifts.FindAsync(id)
        is Lift todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/lifts", async (Lift lift, LiftDb db) =>
{
    db.Lifts.Add(lift);
    await db.SaveChangesAsync();

    return Results.Created($"/lifts/{lift.Id}", lift);
});

app.MapPut("/lifts/{id}", async (int id, Lift inputLift, LiftDb db) =>
{
    var lift = await db.Lifts.FindAsync(id);

    if (lift is null) return Results.NotFound();

    lift.Name = inputLift.Name;
    lift.Weight = inputLift.Weight;
    lift.Reps = inputLift.Reps;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/lifts/{id}", async (int id, LiftDb db) =>
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
