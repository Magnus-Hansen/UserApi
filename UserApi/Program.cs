using UsersLib;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: "AllowAll",
                                      policy =>
                                      {
                                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                      });
        });

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddSingleton(new UserRepo());

        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseAuthorization();

        app.UseCors("AllowAll");

        app.MapControllers();

        app.Run();
    }
}