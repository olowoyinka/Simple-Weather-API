using System.Net;
using Weather.API.Installer;
using Weather.Core.Responses;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InstallServicesInAssenmbly(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather RESTful API v1");
});

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.Use(async (context, next) =>
{
    await next();

    if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
    {
        var responseBody = new ErrorResponseDTO(HttpStatusCode.Unauthorized, new string[] { "Unauthorized access" });
        await context.Response.WriteAsJsonAsync(responseBody);
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
    {
        var responseBody = new ErrorResponseDTO(HttpStatusCode.Forbidden, new string[] { "Forbidden access. Contact system administrator" });
        await context.Response.WriteAsJsonAsync(responseBody);
    }

    if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
    {
        var responseBody = new ErrorResponseDTO(HttpStatusCode.MethodNotAllowed, new string[] { "Endpoint doesn't exist" });
        await context.Response.WriteAsJsonAsync(responseBody);
    }
});

app.UseResponseCompression();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();