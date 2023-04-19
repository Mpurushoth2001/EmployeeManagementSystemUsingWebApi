using EmployeeManagement.Behaviour;
using EmployeeManagement.Configurations;
using EmployeeManagement.Model.EmployeeModel;
using EmployeeManagement.SwaggerConfig;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//Serilog Configuration
var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


//Controller Implementation
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//Implementing swagger Documentation using Xml file Path
builder.Services.AddSwaggerGen(c => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});


//configuration of ApiVersioning For Swagger Documentation
builder.Services.AddApiVersioningConfigured();


// Add a Swagger generator and Automatic Request and Response annotations:
//builder.Services.AddSwaggerSwashbuckleConfigured();


//Implementation of MediaTR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());


//Fluent Validation for Lower Version
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


//Fluent Validation for Higher Version 
//builder.Services.AddFluentValidation(s => { s.RegisterValidatorsFromAssemblyContaining<Program>(); }) ;


//Implementation of DbContext in the Connection of "TestDb"
builder.Services.AddDbContext<EmployeeDbcontext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("TestDb")));


//Dependency Injection for Global Fluent Validation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var descriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });
}

//Implementation of Global Error Handling MiddleWare
app.AddGlobalErrorHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
