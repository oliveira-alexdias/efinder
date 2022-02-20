using Microsoft.AspNetCore.Diagnostics;

namespace EFinder.API.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication Configure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseGlobalExceptionHandler();
        
        app.UseAuthorization();

        app.MapControllers();

        app.Run();

        return app;
    }

    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(c => c.Run(async context =>
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            var response = new { Error = exception?.Message };
            await context.Response.WriteAsJsonAsync(response);
        }));
    }
}