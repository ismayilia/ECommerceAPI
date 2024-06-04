using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Azure;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

//jwt package ---jwtbearerdefaults.authenticationScheme->default shema

//bu app-e token uzerinden bir request gelirse bu tokeni dogrularken JWT oldugu bil ve dogrularken bu optionslar uzerinden
//dogrula

builder.Services.AddAuthentication("Admin").AddJwtBearer(options =>
{
	options.TokenValidationParameters = new()
	{
		ValidateAudience = true, // yaradicilaq token deyerini kimlerin/hansi originlerin/saytlarin istifade edeceyi deyerdir -> www.ak.com example
		ValidateIssuer = true, // yaradicilaq token deyerini kimin verdiyini efadece edecek yerdir -> www.myapi.com example bizim api address
		ValidateLifetime = true, // yaradilan token deyerinin vaxtini kontrol edecek olan dogrulamadir
		ValidateIssuerSigningKey = true, // yaradilan token deyerinin app-imize aid bir deyer oldugunu ifade eden security ker verilenin dogrulamasidir

		//yuxaridakilarin valid qarshiligi

		ValidAudience = builder.Configuration["Token:Audince"],
		ValidIssuer = builder.Configuration["Token:Issuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"]))
	
	
	
	};
});

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
