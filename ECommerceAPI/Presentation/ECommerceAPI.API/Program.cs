using ECommerceAPI.Persistence;
using ECommerceAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddPersistenceServices();

// browser cors-a icaze vermek (yungulleshdirmek)
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
	policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers();

//builder.Services.AddDbContext<ECommerceAPIDbContext>(options =>
//		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//cors middleware-ni cagirmaq
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
