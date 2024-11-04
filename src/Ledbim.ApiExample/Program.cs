using Ledbim.ApiExample.Application;
using Ledbim.Core.Extensions;
using Ledbim.Core.Security;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var Configuration = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

builder.Services.AddCustomJwtBearer(Configuration);

builder.Services.AddControllers();

builder.Services.AddApplication(Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    // Known proxies veya networkler
    options.KnownProxies.Add(System.Net.IPAddress.Parse("127.0.0.1"));
});

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Ledbim", Version = "v1" });
    swagger.SwaggerDoc("v2", new OpenApiInfo { Title = "Ledbim", Version = "v2" });
    swagger.SwaggerDoc("v3", new OpenApiInfo { Title = "Ledbim", Version = "v3" });

    swagger.EnableAnnotations();
    swagger.OperationFilter<SwaggerIgnoreOperationFilter>();

    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Bearer",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter Your token in the text input below.\r\n\r\nExample: \"12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme =  "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddApiVersioning(opt =>
{
    opt.AssumeDefaultVersionWhenUnspecified = false;//false olursa api versiyon göndermezsek çalýþmaz
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new MediaTypeApiVersionReader("Version")//giden istekte api versiyonunu görmemizi saðlar
                                                    );

});
builder.Services.AddVersionedApiExplorer(config =>
{
    config.GroupNameFormat = "'v'VVV";
    config.SubstituteApiVersionInUrl = true;
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<IPWhitelistMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ledbim v1");
    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Ledbim v2");
    c.SwaggerEndpoint("/swagger/v3/swagger.json", "Ledbim v3");
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});

app.UseRouting();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();
app.UseRequestLocalization();

//UserExtensions.UserConfigure(app.Services.GetRequiredService<IHttpContextAccessor>());

app.MapControllers();

app.Run();
