using Api.Filters;
using Autofac.Core;
using Business.Abstract;
using Business.Concrete;
using Business.Mapping;
using Business.Validations;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.Context;
using DataAccess.Seeds;
using Entities.Concrete;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

	// Add the Swagger security definition
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme.",
		Type = SecuritySchemeType.Http,
		Scheme = "bearer"
	});
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
        }
    });
});
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
	options.SuppressModelStateInvalidFilter = true;

});
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<BestApiDbContext>(x =>
{
	AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
	x.UseNpgsql(builder.Configuration.GetConnectionString("SqlConnection"), option =>
	{
    });
});
builder.Services.AddIdentity<AppUser, AppRole>()
		.AddEntityFrameworkStores<BestApiDbContext>()
		.AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequiredLength = 6;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireLowercase = false;
	options.Password.RequireUppercase = false;
	options.Password.RequireDigit = false;
});
var testkey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
	options.RequireHttpsMetadata = false;
	options.SaveToken = true;
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"])),
		ValidateIssuer = false,
		ValidateAudience = false
	};
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<SeedService>();
builder.Services.AddScoped<IQrCodeService,QrCodeService>();
var app = builder.Build();
var serviceProvider = builder.Services.BuildServiceProvider();
var seedService = serviceProvider.GetService<SeedService>();
seedService.SeedData();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

		// Add the Swagger security requirement
		c.DocumentTitle = "My API Documentation";
		c.InjectStylesheet("/swagger-ui/custom.css");
		c.InjectJavascript("/swagger-ui/custom.js");
		c.EnableFilter();
		c.EnableDeepLinking();
		c.DisplayRequestDuration();
		c.DocExpansion(DocExpansion.None);
		c.DefaultModelRendering(ModelRendering.Example);
		c.DefaultModelExpandDepth(2);
		c.DefaultModelsExpandDepth(2);
		c.EnableValidator();
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		
	});
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
