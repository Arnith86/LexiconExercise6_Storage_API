using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StorageApi.Data;


namespace StorageApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<StorageApiContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("StorageApiContext") 
                ?? throw new InvalidOperationException("Connection string 'StorageApiContext' not found.")));

			// Add services to the container.
			// Registers AutoMapper with the dependency injection container.
			// It scans the current domain for assemblies that contain classes that implement the
			// IMapper interface (mapping profiles). 
			builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());  // Make entity to DTO mapping easier
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseSwaggerUI(option =>
                //{
                //    option.SwaggerEndpoint("/openapi/v1.json", "v1");
                //});
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
