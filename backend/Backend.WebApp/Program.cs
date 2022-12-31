using Backend.BusinessLogic.Base;
using Backend.DataAccess;
using Backend.WebApp.Code;
using Backend.WebApp.Code.ExtensionMethods;
using Backend.WebApp.Code.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalExceptionFilterAttribute));
});

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(BaseService).Assembly);

builder.Services.AddDbContext<WorkoutBuddyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<UnitOfWork>();


builder.Services.AddPresentation();

builder.Services.AddBackendBusinessLogic();

builder.Services.AddHttpContext();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
 {
     options.RequireHttpsMetadata = false;
     options.SaveToken = true;
     options.TokenValidationParameters = new TokenValidationParameters()
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidAudience = configuration["JWT:ValidAudience"],
         ValidIssuer = configuration["JWT:ValidIssuer"],
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
     };

 });


/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("tokens", p =>
    {
        p.AddAuthenticationSchemes("jwt", "introspection");
        p.RequireAuthenticatedUser();
    });
});
*/

/*builder.Services.AddAuthentication("BackendCookies")
       .AddCookie("BackendCookies", options =>
       {
           options.AccessDeniedPath = new PathString("/Account/Login");
           options.LoginPath = new PathString("/Account/Login");
       });

*/
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", p =>
{
    p.WithOrigins("https://localhost:3000/", "https://localhost:7132/")
    .AllowAnyOrigin()
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
