
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MiniSocialNetworkApi.Data;
using MiniSocialNetworkApi.Models.Domain;
using MiniSocialNetworkApi.AutoMapper;
using AutoMapper;

namespace MiniSocialNetworkApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();



            //Before adding the authentication just add Identity solution
            //builder.Services.AddIdentityCore<ApplicationUser>()//Add the core Identity system for managing users
            //    .AddRoles<IdentityRole>()//Enable role management
            //    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("SocialMedia")// Register a system that generates secure tokens for users
            //    .AddEntityFrameworkStores<AppDbContext>()//Store all Identity data in a database using Entity Framework Core
            //    .AddDefaultTokenProviders();


            // Instead of writing this code :AddIdentityCore<ApplicationUser>()//Add the core Identity system for managing users
            //    .AddRoles<IdentityRole>()//Enable role management
            //    we can write just for direct  configuration : AddIdentity<ApplicationUser , IdentityRole>() because it diectly gives total configuration of the Identity system with user and role management. But in our case we don't need role management so we can use AddIdentityCore<ApplicationUser>() which is more lightweight and only provides the core features for user management without roles.
           
            
            builder.Services
                .AddIdentityCore<ApplicationUser>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>("SocialMedia")
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnection")));


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });


            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.AllowedForNewUsers = true;
            });


            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}
