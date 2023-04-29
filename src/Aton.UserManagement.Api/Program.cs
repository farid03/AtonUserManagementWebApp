using Aton.UserManagement.Bll.Extensions;
using Aton.UserManagement.Dal.Extensions;
using Aton.UserManagement.Api.Middleware;
using Aton.UserManagement.Api.NamingPolicies;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy(); });

services.AddEndpointsApiExplorer();

// add swagger
services.AddSwaggerGen(o => { o.CustomSchemaIds(x => x.FullName); });

//add validation
services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = true;
});


//add dependencies
services
    .AddBll()
    .AddDalInfrastructure(builder.Configuration)
    .AddDalRepositories();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.MapControllers();
app.MigrateUp();
app.Run();