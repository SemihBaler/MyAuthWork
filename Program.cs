using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyAuthWork.Constants;

namespace MyAuthWork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddEndpointsApiExplorer();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddCors(options =>
            //{
            //    options.AddPolicy("MyCorsPolicy", builder =>
            //    {
            //        builder.AllowAnyOrigin() // Ýstekleri herhangi bir kaynaktan kabul et
            //               .AllowAnyMethod() // Tüm HTTP metodlarýný kabul et (GET, POST, PUT, DELETE, vb.)
            //               .AllowAnyHeader(); // Tüm HTTP baþlýklarýný kabul et
            //    });
            //});
            builder.Services.AddScoped<TokenManager>();
            builder.Services.AddSession();
            builder.Services.AddFluentValidation(x =>
            {
                x.RegisterValidatorsFromAssemblyContaining<Program>();
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "semih",
                        ValidAudience = "semih",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

                    };
                });



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });


            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=MvcLogin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}