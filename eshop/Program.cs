using eshop.Application;
using eshop.Filters;
using eshop.Middlewares;
using FluentResults;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Configuration;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .Enrich.WithProperty("ApplicationName", "eshop")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.File(new CompactJsonFormatter(), "logs.txt")
    .CreateLogger();

builder.Services.AddSerilog(logger);
Log.Logger = logger;

builder.Services.RegisterApplicationServices(builder.Configuration);



builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<IncomingRequestFilter>();
    options.Filters.Add<GlobalExceptionFilter>();
})
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var modelErrors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(x => x.Exception?.Message ?? x.ErrorMessage)
            .ToArray();
            var result = Result.Fail(modelErrors);

            return new BadRequestObjectResult(result.ToString());
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAnyOrigin",
       policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

//auth


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();


app.UseAuthentication();
app.UseMiddleware<LogUserMiddleware>();


app.UseSerilogRequestLogging(ops =>
{
    ops.Logger = logger;
});
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
}


//app.UseHttpsRedirection();

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowAnyOrigin");




app.UseAuthorization();


app.MapControllers();

app.Run();
