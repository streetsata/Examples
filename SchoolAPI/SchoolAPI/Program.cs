using MongoDB.Driver;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;
using SchoolAPI.Services;

namespace SchoolAPI
{
    public class Program
    {
        protected Program() { }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<SchoolDatabaseSettings>(builder.Configuration.GetSection("SchoolDatabaseSettings"));
            builder.Services.AddSingleton<IMongoClient>(_ => {
                var connectionString = builder.Configuration.GetSection("SchoolDatabaseSettings:ConnectionString").Value;
                return new MongoClient(connectionString);
            });
            builder.Services.AddSingleton<IStudentService, StudentService>();
            builder.Services.AddSingleton<ICourseService, CourseService>();

            var app = builder.Build();

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
