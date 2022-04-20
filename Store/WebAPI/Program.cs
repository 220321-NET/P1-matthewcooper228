using DL;
using BL;
using Models;
using Serilog;


// logging
// using var log = new LoggerConfiguration().WriteTo.File("../logs/logs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("../logs/log.txt", rollingInterval: RollingInterval.Day)
);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository>(ctx => new DBRepository(builder.Configuration.GetConnectionString("StoreDB")));
builder.Services.AddScoped<IStoreBL, StoreBL>();

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
