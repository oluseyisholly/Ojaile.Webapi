

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ojaile.Abstraction;
using Ojaile.Webapi.Data;
using Ojaile.Webapi.Models;
using System.Text;
using Ojaile.Facade;
using Ojaile.Data.DBModel;
using Serilog;
using Ojaile.Core1;
using Ojaile.Webapi;
using AutoMapper;
using Ojaile.Webapi.Helpers;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);



// Add services to the container.
builder.Services.AddScoped<IPropertyItemService, PropertyItemService>();
builder.Services.AddScoped<IPropertyImageService, PropertyImageService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddAutoMapper(typeof(Program));

var configmap = new MapperConfiguration(m =>
{
    m.AddProfile(new AutoMapperHelper());
});
IMapper mapper = configmap.CreateMapper();
builder.Services.AddSingleton(mapper);
//builder.Services.AddTransient<IPropertyItemService, IPropertyItemService>();
//builder.Services.AddSingleton<IPropertyItemService, IPropertyItemService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/as pnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDbContext<OJAILEContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptions =>
    {
        sqlServerOptions.MigrationsAssembly("Ojaile.Data");
    });
});

//////////////////////////
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));




builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
        { 
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 0;
            options.Password.RequireDigit = false; 
        })
    .AddEntityFrameworkStores<MyDbContext>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
    });
var app = builder.Build();

builder.Services.AddLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
    app.UseHttpLogging();
    app.UserPartialConfiguration();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
