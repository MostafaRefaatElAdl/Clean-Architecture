
using Microsoft.Extensions.Options;
using OnionArch.Application;
using OnionArch.Filters;
using OnionArch.Infrastructure;
using OnionArch.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers(Options => Options.Filters.Add<ErrorHandlingFilter>());
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    //Middleware approch=> 
    //app.UseMiddleware<ErrorHandlingMiddleware>();

    
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
