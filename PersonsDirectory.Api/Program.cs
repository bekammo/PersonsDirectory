using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonsDirectory.Api.Middlewares;
using PersonsDirectory.Api.Validators;
using PersonsDirectory.Application.Commands.Handlers;
using PersonsDirectory.Application.Interfaces;
using PersonsDirectory.Infrastructure.Persistence;
using PersonsDirectory.Infrastructure.Repositories;
using PersonsDirectory.Infrastructure.Services;
using PersonsDirectory.Infrastructure.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonsDirectoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddScoped<IImageService, ImageService>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var imageDirectory = config.GetValue<string>("ImageSettings:ImageDirectory")
        ?? throw new ArgumentNullException("ImageSettings:ImageDirectory", "Image directory configuration is missing.");
    return new ImageService(imageDirectory);
});


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreatePersonCommandHandler).Assembly));
builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Persons Directory API v1");
        c.RoutePrefix = string.Empty; 
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorLoggingMiddleware>();
app.UseMiddleware<LocalizationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
