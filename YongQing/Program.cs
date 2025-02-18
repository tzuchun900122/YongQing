using Microsoft.EntityFrameworkCore;
using YongQing.Entities;
using YongQing.Repositories;
using YongQing.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 設定 DbContext 並使用 SQL Server
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnection")));

// 註冊泛型 Northwind 的資料層和邏輯層到依賴注入容器
builder.Services.AddScoped(typeof(IRepository<,>), typeof(NorthwindRepository<,>));
builder.Services.AddScoped(typeof(IDbService<,>), typeof(NorthwindDbService<,>));

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
