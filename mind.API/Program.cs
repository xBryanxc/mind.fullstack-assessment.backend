using Microsoft.EntityFrameworkCore;
using mind.API.Middlewares;
using mind.Core.Interfaces.IRepositories;
using mind.Core.Interfaces.IServices;
using mind.Core.Services;
using mind.Infraestructure.Data;
using mind.Infraestructure.Repositories;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File.Header;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => 
            {
                options.AddPolicy("AllowAll", builder => 
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

// Dependency Injection of the services and repositories
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

Log.Logger = new LoggerConfiguration()
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(le => le.Level == LogEventLevel.Information)
                .WriteTo.File("./Logs/RequestLogs/RequestLogs-.txt", rollingInterval: RollingInterval.Day, hooks: new HeaderWriter("Date - ApiCall - Method - Body"), outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm} - {Message}{NewLine}"))
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(le => le.Level == LogEventLevel.Error)
                .WriteTo.File("./Logs/ErrorLogs/ErrorLogs-.txt", rollingInterval: RollingInterval.Day, hooks: new HeaderWriter("Date - ApiCall - Method - Body - Exception"), outputTemplate: "{Message}{NewLine}{Exception}"))
            .CreateLogger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<LogginMiddleware>();

app.MapControllers();

app.Run();

