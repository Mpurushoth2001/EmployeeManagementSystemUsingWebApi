using EmployeeManagement.Behaviour;
using EmployeeManagement.Middleware.AuthenticationMiddleware;
using EmployeeManagement.Middleware.ExceptionMiddleware;
using EmployeeManagement.Model.EmployeeModel;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Serilog Configuration

var logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
#endregion

//Controller Implementation
builder.Services.AddControllers();

#region Swagger Configuration

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    // ReportApiVersions will return the "api-supported-versions" and "api-deprecated-versions" headers.
    options.ReportApiVersions = true;

    // Set a default version when it's not provided,
    // e.g., for backward compatibility when applying versioning on existing APIs
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Combine (or not) API Versioning Mechanisms:
    //options.ApiVersionReader = ApiVersionReader.Combine(
    //        // The Default versioning mechanism which reads the API version from the "api-version" Query String paramater.
    //        //new QueryStringApiVersionReader("api-version")
    //        // Use the following, if you would like to specify the version as a custom HTTP Header.
    //        //new HeaderApiVersionReader("Accept-Version"),
    //        // Use the following, if you would like to specify the version as a Media Type Header.
    //        new MediaTypeApiVersionReader("api-version")
    //    );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    // Format the version as "v{Major}.{Minor}.{Patch}" (e.g. v1.0.0).
    options.GroupNameFormat = "'v'VVV";

    // Note: this option is only necessary when versioning by url segment. the SubstitutionFormat
    // can also be used to control the format of the API version in route templates
    options.SubstituteApiVersionInUrl = true;
});

//Implementing swagger Documentation using Xml file Path
builder.Services.AddSwaggerGen(c => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {               
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            new string[] {}
        }
    });
});

#endregion

//configuration of ApiVersioning For Swagger Documentation
//builder.Services.AddApiVersioningConfigured();

#region MediatR Configuration

//Implementation of MediaTR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

#endregion

#region Fluent Validation Configuration
//Fluent Validation for Lower Version
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


//Fluent Validation for Higher Version 
//builder.Services.AddFluentValidation(s => { s.RegisterValidatorsFromAssemblyContaining<Program>(); }) ;

//Dependency Injection for Global Fluent Validation
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

#endregion

#region DbContext

//Implementation of DbContext in the Connection of "TestDb"
builder.Services.AddDbContext<EmployeeDbcontext>
    (option => option.UseSqlServer(builder.Configuration.GetConnectionString("TestDb")));

#endregion

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

app.UseMiddleware<BasicAuthorization>();

//Implementation of Global Error Handling MiddleWare
app.AddGlobalErrorHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
