using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Azure;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddAplicationServices();
//localstroage verildiyi ucun  o ishleyecek
//builder.Services.AddStorage(StorageType.Azure);
builder.Services.AddStorage<AzureStorage>();

// browser cors-a icaze vermek (yungulleshdirmek)
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
	policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));


//addcontrollersin icinde olanlar validationfilteri ucundur
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
	.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
	.ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
//butun validatonlari register edir. hamisi ucun


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

app.UseStaticFiles();

//cors middleware-ni cagirmaq
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
