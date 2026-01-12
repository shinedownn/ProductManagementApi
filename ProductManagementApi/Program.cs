using ProductManagementApi.DataAccess.Abstract;
using ProductManagementApi.DataAccess.Concrete;
using ProductManagementApi.Filters;
using ProductManagementApi.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using ProductManagementApi;
using ProductManagementApi.Services.FakestoreApi.Concrete;
using ProductManagementApi.Services.FakestoreApi.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});
 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(UserEntityProfile));
builder.Services.AddAutoMapper(typeof(UserParamsProfile));

builder.Services.AddAutoMapper(typeof(ProductEntityProfile));  
builder.Services.AddAutoMapper(typeof(ProductParamsProfile));     

//builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IExternalProductService, ExternalProductService>();

//Add JWT to Swagger
builder.Services.AddSwaggerGen(swagger =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath); 
    swagger.SwaggerDoc("v1", new OpenApiInfo
    { 
        Title = "Product Management API",
        Description = "ASP.NET Core 8 Web API"
    });  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Sadece token deðerini girin. 'Bearer blablabla' þeklinde kullanmayýnýz !!!  ",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

 
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation(); 

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.Configure<AppSettingsModel>(builder.Configuration);
var app = builder.Build();

ProductManagementApi.Helpers.ServiceHelper.Services = app.Services;
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
