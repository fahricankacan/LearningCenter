using LearningCenter.Business.Abstract;
using LearningCenter.Business.Concrate;
using LearningCenter.Entity.Concrate;
using LearningCenter.Repository.Abstract;
using LearningCenter.Repository.Concrate;
using LearningCenter.WhatIsMinimalApi.Middleware;
using LearningCenter.WhatIsMinimalApi.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using static LearningCenter.Entity.Concrate.Lift;

Log.Logger = new LoggerConfiguration()
    .WriteTo
    .Console()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(); // <-- Add this line


    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDbContext<LiftDb>(opt => opt.UseInMemoryDatabase("LiftList"));
    //builder.Services.AddDatabaseDeveloperPageExceptionFilter();


    builder.Services.AddScoped<ILiftService, LiftManager>();
    builder.Services.AddScoped<ILiftRepository, LiftRepository>();
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

    app.UseExceptionHandleMiddleware(); // Our middelware to handle exceptions

    var liftsEndPoint = app.MapGroup("/lifts");


    liftsEndPoint.MapGet("/", GetAllLifts);
    liftsEndPoint.MapGet("/getByLiftName", GetLiftByName);
    liftsEndPoint.MapGet("/{id}", GetLift);
    liftsEndPoint.MapPost("/", CreateLift);
    liftsEndPoint.MapPut("/{id}", UpdateLift);
    liftsEndPoint.MapDelete("/{id}", DeleteLift);
    app.MapGet("/fakeError", FakeError);

    app.Run();

    static async Task<IResult> GetAllLifts(ILiftService liftService)
    {
        var result = await liftService.GetAllAsync();
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> GetLiftByName(ILiftService liftService, LiftName name)
    {
        var result = await liftService.GetByLiftName(name);
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> GetLift(int id, ILiftService liftService)
    {
        var result = await liftService.GetAsync(new Lift { Id = id });
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> CreateLift(Lift lift, ILiftService liftService)
    {
        var result = await liftService.CreateAsync(lift);
        return result.Success ? TypedResults.Created($"/lifts/{lift.Id}", result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> UpdateLift(Lift inputLift, ILiftService liftService)
    {
        var result = await liftService.UpdateAsync(inputLift);
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> DeleteLift(int id, ILiftService liftService)
    {
        var result = await liftService.DeleteAsync(id);
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

    }

    static Task<IResult> FakeError()
    {

        throw new InvalidOperationException("Fake error");

    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}