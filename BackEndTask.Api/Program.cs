using BackEndTask.Api.MeddilWares;
using BackEndTask.Api.UIDependancyInjection;
using BackEndTask.Application.MeddilWares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// application dependacnies
builder.Services.CollectDependancies(builder);
var app = builder.Build();
// seed data
await app.SeedData();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
       
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionHandlerMeddilware>();
app.UseMiddleware<StepValidationMeddileWare>();
app.Run();
