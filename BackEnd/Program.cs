using BeansAPI.Models;
using Hangfire;
using Hangfire.Storage.SQLite;

GlobalConfiguration.Configuration.UseSQLiteStorage();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BeanContext>();
builder.Services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage());
builder.Services.AddHangfireServer();
builder.WebHost.UseUrls("http://0.0.0.0:5000"); // Allow access when running in Docker

WebApplication app = builder.Build();

// Ensure the database is created and seeded, and schedule a daily job to choose the bean of the day.
using (IServiceScope scope = app.Services.CreateScope())
{
    BeanContext context = scope.ServiceProvider.GetRequiredService<BeanContext>();
    context.Database.EnsureCreated();

    RecurringJob.RemoveIfExists("ChooseBeanOfTheDay");
    RecurringJob.AddOrUpdate(
        "ChooseBeanOfTheDay",
        () => context.ChooseBeanOfTheDay(),
        Cron.Daily(0)); // Run every day at midnight
}

// In a real environment, we wouldn't use swagger in production, but for ease of testing for Tombola, we will
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
