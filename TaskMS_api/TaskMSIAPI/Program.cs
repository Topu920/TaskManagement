using Application.IoC;
using Infrastructure.IoC;
using NSwag;
using NSwag.Generation.Processors.Security;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "CleanArchitecture API";
    configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "Authorization",
        In = OpenApiSecurityApiKeyLocation.Header,
        Description = "Type into the textBox: Bearer {your JWT token}."
    });

    configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
   

}
app.UseOpenApi();
app.UseSwaggerUi3(config => config.TransformToExternalPath = (internalUiRoute, request) =>
{
    
    if (internalUiRoute.StartsWith("/") == true && internalUiRoute.StartsWith(request.PathBase) == false)
    {
        return request.PathBase + internalUiRoute;
    }
    else
    {
        return internalUiRoute;
    }
});
app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.MapControllers();

app.Run();
