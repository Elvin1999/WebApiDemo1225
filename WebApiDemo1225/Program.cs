using Microsoft.EntityFrameworkCore;
using WebApiDemo1225.Data;
using WebApiDemo1225.Formatters;
using WebApiDemo1225.Repositories.Abstract;
using WebApiDemo1225.Repositories.Concrete;
using WebApiDemo1225.Services.Abstract;
using WebApiDemo1225.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, new VcardInputFormatter());
    options.OutputFormatters.Insert(0, new VCardOutputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

var connection = builder.Configuration.GetConnectionString("myconn");
builder.Services.AddDbContext<StudentDbContext>(opt =>
{
    opt.UseSqlServer(connection);
});

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
