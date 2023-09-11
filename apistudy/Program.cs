using apistudy.Atrubuts;
using apistudy.interfaces;
using apistudy.Models;
using apistudy.Models.Entityies;
using apistudy.Servesess;
using apistudy.Seting;
using apistudy.Utillites;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.IdentityModel.Tokens;

using ServiceStack.Text;

using System.Configuration;
using System.Text;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddTransient<IHostingEnvironment, HostingEnvironment>();
builder.Services.AddScoped<HostingEnvironment>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailingService, MailingService>();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();


//add Identity DbContext
builder.Services.AddDbContext<AppIdentityDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityDBConnection")));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<CategoryServess>();
builder.Services.AddScoped<ProductServess>();
builder.Services.AddScoped<FileUploadService>();
builder.Services.AddScoped<Unitofwork>();
//builder.Services.AddScoped<AllowedExtensionsAttribute>();
//builder.Services.AddScoped<MaxFileSizeAttribute>();



// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    // Set the default authentication scheme for successful authentication
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

    // Set the default challenge scheme for authentication challenges
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    // Set to true in production to require HTTPS for metadata requests
    o.RequireHttpsMetadata = false;

    // Set to true to persist the token after validation
    o.SaveToken = false;

    // Configure token validation parameters
    o.TokenValidationParameters = new TokenValidationParameters
    {
        // Validate the issuer's signing key
        ValidateIssuerSigningKey = true,

        // Validate the issuer of the token
        ValidateIssuer = true,

        // Validate the intended audience of the token
        ValidateAudience = true,

        // Validate the token's expiration time
        ValidateLifetime = true,

        // Set the valid issuer for your application
        ValidIssuer = builder.Configuration["JWT:Issuer"],

        // Set the valid audience for your application
        ValidAudience = builder.Configuration["JWT:Audience"],

        // Set the issuer's signing key generated from your secret key
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder. Configuration["JWT:Key"])),

        // Set the maximum acceptable clock skew for token expiration
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
