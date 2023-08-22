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


liftsEndPoint.MapGet("/", GetAllLifts);
liftsEndPoint.MapGet("/getSquats", GetLiftByName);
liftsEndPoint.MapGet("/{id}", GetLift);
liftsEndPoint.MapPost("/", CreateLift);
liftsEndPoint.MapPut("/{id}", UpdateLift);
liftsEndPoint.MapDelete("/{id}", DeleteLift);


app.Run();

static async Task<IResult> GetAllLifts(LiftDb db)
{
    return TypedResults.Ok(await db.Lifts.ToArrayAsync());
}

static async Task<IResult> GetLiftByName(LiftDb db)
{
    return TypedResults.Ok(await db.Lifts.Where(t => t.Name == Lift.LiftName.Squat).ToListAsync());
}

static async Task<IResult> GetLift(int id, LiftDb db)
{
    return await db.Lifts.FindAsync(id)
        is Lift todo
            ? Results.Ok(todo)
            : Results.NotFound();
}

static async Task<IResult> CreateLift(Lift lift, LiftDb db)
{
    db.Lifts.Add(lift);
    await db.SaveChangesAsync();

    return TypedResults.Created($"/todoitems/{lift.Id}", lift);
}

static async Task<IResult> UpdateLift(int id, Lift inputLift, LiftDb db)
{
    var lift = await db.Lifts.FindAsync(id);

    if (lift is null) return Results.NotFound();

    lift.Name = inputLift.Name;
    lift.Weight = inputLift.Weight;
    lift.Reps = inputLift.Reps;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteLift(int id, LiftDb db)
{
    if (await db.Lifts.FindAsync(id) is Lift todo)
    {
        db.Lifts.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }


    return TypedResults.NotFound();
}