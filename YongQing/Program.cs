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

// �]�w DbContext �èϥ� SQL Server
builder.Services.AddDbContext<NorthwindDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NorthwindConnection")));

// ���U�x�� Northwind ����Ƽh�M�޿�h��̿�`�J�e��
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
