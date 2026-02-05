using Microsoft.EntityFrameworkCore;
using Schedule.Core.Repositories;
using Schedule.Core.Service;
using Schedule.Data;
using Schedule.Data.Repositories;
using Schedule.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentService,StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlServer(@"Server=R51047;Database=ScheduleDB;
TrustServerCertificate=True;Trusted_Connection=True"));


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

app.Run();
