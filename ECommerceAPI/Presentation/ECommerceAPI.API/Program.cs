using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Azure;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using ECommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
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
builder.Services.AddSwaggerGen(opt =>
{
	opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer"
	});

	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});

//jwt package ---jwtbearerdefaults.authenticationScheme->default shema

//bu app-e token uzerinden bir request gelirse bu tokeni dogrularken JWT oldugu bil ve dogrularken bu optionslar uzerinden
//dogrula

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("Admin", options =>
	{
		options.TokenValidationParameters = new()
		{
			ValidateAudience = true, //Olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�erdir. -> www.bilmemne.com
			ValidateIssuer = true, //Olu�turulacak token de�erini kimin da��tt�n� ifade edece�imiz aland�r. -> www.myapi.com
			ValidateLifetime = true, //Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r.
			ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden suciry key verisinin do�rulanmas�d�r.

			ValidAudience = builder.Configuration["Token:Audience"],
			ValidIssuer = builder.Configuration["Token:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
			LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

			NameClaimType = ClaimTypes.Name //JWT �zerinde Name claimne kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
