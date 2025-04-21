using Domain.Exception;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;
using MISA.AMISDemo.Core.Exceptions;
using MISA.AMISDemo.Core.Interfaces.Accounts;
using MISA.AMISDemo.Core.Interfaces.Base;
using MISA.AMISDemo.Core.Interfaces.Caches;
using MISA.AMISDemo.Core.Interfaces.CustomerGroups;
using MISA.AMISDemo.Core.Interfaces.Customers;
using MISA.AMISDemo.Core.Interfaces.Departments;
using MISA.AMISDemo.Core.Interfaces.Employees;
using MISA.AMISDemo.Core.Interfaces.Positions;
using MISA.AMISDemo.Core.Services;
using MISA.AMISDemo.Core.UnitOfWorks;
using MISA.AMISDemo.Infracstructure.Interfaces;
using MISA.AMISDemo.Infracstructure.MISADatabaseContexts;
using MISA.AMISDemo.Infracstructure.Repositories;
using MISA.AMISDemo.Infracstructure.Repository;
using MISA.AMISDemo.Infracstructure.UnitOfWorks;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowedOrigins",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000") 
                               .AllowAnyMethod() // Cho phép tất cả các phương thức
                             .AllowAnyHeader(); // Cho phép tất cả các tiêu đề
                    });
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerGroupRepository, CustomerGroupRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICacheService, CacheService>();


// context
builder.Services.AddScoped<IMISAdbContext, MysqlDbContext>();

// service 
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerGroupService, CustomerGroupService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// kiểm tra token 
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret");
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = false,

        ValidateAudience = false,
        // đồng bọo đồng hồ của hai máy chủ 
        ClockSkew = TimeSpan.Zero,
    };

});
// caching response
builder.Services.AddResponseCaching();
builder.Services.AddControllers(option => {
    option.CacheProfiles.Add("Default30",
       new CacheProfile()
       {
           Duration = 30
       });
    //option.ReturnHttpNotAcceptable=true;
}).ConfigureApiBehaviorOptions(
    options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values.SelectMany(x => x.Errors);
            return new BadRequestObjectResult(new BaseException()
            {
                status = System.Net.HttpStatusCode.BadRequest,
                userMsg = MISA.AMISDemo.Core.Resource.Resource_VN.UsgMessage,
                devMsg = MISA.AMISDemo.Core.Resource.Resource_VN.UsgMessage,
                traceId = "",
                success = false,
                errors = errors
            }.ToString() ?? "");
        };
    }
    ).AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });




builder.Services.AddEndpointsApiExplorer();

//hiển thị các phần đề cập đến xác thực JWT.
//builder.Services.AddSwaggerGen(options =>
//{
//    // open api security scheme
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//                                          {
//                                              Description = MISA.AMISDemo.Core.Resource.Resource_VN.DescriptionAuthorize,
//                                              // name là tên key gửi trong postman 
//                                              Name = "Authorization",
//                                              // In là ở đâu trong Header
//                                              In = ParameterLocation.Header,
//                                              Scheme = "Bearer"
//                                          });
//options.AddSecurityRequirement(new OpenApiSecurityRequirement()
//            {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        },
//                        Scheme = "oauth2",
//                        Name = "Bearer",
//                        In = ParameterLocation.Header
//                    },
//                    new List<string>()
//                }
//            });
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyAllowedOrigins"); 
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<HandleException>();
app.UseAuthorization();
app.MapControllers();

app.Run();
