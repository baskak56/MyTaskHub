using Api.UseCases.Users;
using Api.UseCases.Users.Interfaces;
using Api.UseCases.Tasks;
using Api.UseCases.Tasks.Interfaces;
using Dal;
using Logic;
using DI;
using Microsoft.OpenApi.Models;
using Api.Middleware;
using Dal.Context;
using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Services;
using Logic.Tasks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api;

/// <summary>
/// Конфигурация приложения
/// </summary>
public sealed class Startup
{
    /// <summary>
    /// Конфигурация приложения
    /// </summary>
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Окружение приложения
    /// </summary>
    private IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Environment = env;
    }

    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddDal();
        services.AddLogic();
        services.AddSingleton<Singleton1, Singleton1>();
        services.AddSingleton<Singleton2, Singleton2>();
        services.AddScoped<Scoped1, Scoped1>();
        services.AddScoped<Scoped2, Scoped2>();
        services.AddTransient<Transient1, Transient1>();
        services.AddTransient<Transient2, Transient2>();

        // 👇 ДОБАВЛЯЕМ РЕГИСТРАЦИЮ БАЗЫ ДАННЫХ
        services.AddDbContext<TaskDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("Postgres")));

        // 👇 ДОБАВЛЯЕМ РЕГИСТРАЦИЮ РЕПОЗИТОРИЯ
        services.AddScoped<ITaskRepository, TaskRepository>();

        // 👇 ДОБАВЛЯЕМ РЕГИСТРАЦИЮ СЕРВИСА
        services.AddScoped<ITaskService, TaskService>();

        // UseCases для пользователей
        services.AddScoped<IManageUserUseCase, ManageUserUseCase>();

        // UseCases для задач
        services.AddScoped<IManageTaskUseCase, ManageTaskUseCase>();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskHub Api",
                Version = "v1"
            });
        });
    }

    /// <summary>
    /// Конфигурация middleware пайплайна
    /// </summary>
    /// <param name="app">Построитель приложения</param>
    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskHub API v1");
            });
        }
        app.UseMiddleware<ResponseTimeMiddleware>();
        app.UseMiddleware<StudentInfoMiddleware>();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}