
using FinanceManagementSystem.IServices.Services;
using FinanceManagementSystem.IServices;
using Microsoft.Extensions.Configuration;
using FinanceManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FinanceManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .WriteTo.Console()
                   .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                   .CreateLogger();

            Log.Information("Started");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();
            // Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddDbContext<FinanceDBContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    policyBuilder => policyBuilder.AllowAnyOrigin()
                                                 .AllowAnyMethod()
                                                 .AllowAnyHeader());

            });

       
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IIncomeService, IncomeService>();
            builder.Services.AddScoped<IBudgetService, BudgetService>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            var app = builder.Build();

            app.UseCors("AllowAllOrigins");
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
