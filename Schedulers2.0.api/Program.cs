
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Schedulers2._0.api.Data;
using Schedulers2._0.api.Repository;
using Schedulers2._0.api.Service;
using Schedulers2._0.api.Service.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddDbContext<DotnetDbContext>(options => options.UseSqlServer(
        builder.Configuration["ConnectionStrings:DefaultConnection"],
        b => b.MigrationsAssembly("Schedulers2.0.api")).UseSnakeCaseNamingConvention());
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddHangfireServer();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IJobScheduler, JobSchedulerRepo>();
builder.Services.AddScoped<Utilities>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseHangfireDashboard();
app.MapControllers();

app.Run();
