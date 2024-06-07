using EdgedGoods.Api.Endpoints;
using EdgedGoods.Application;
using EdgedGoods.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapProductEndpoints();
}

app.Run();