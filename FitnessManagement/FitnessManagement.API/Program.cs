
using FitnessManagement.BL.Intefaces;
using FitnessManagement.BL.Services;
using FitnessManagement.EF.Repositories;
using System.Configuration;
namespace FitnessManagement.API {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            string dataLayer = System.Configuration.ConfigurationManager.AppSettings["DataLayer"];
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EFconnection"].ConnectionString;
            // Add services to the container.
            builder.Services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<IMemberRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).MemberRepository);
            builder.Services.AddSingleton<MemberService>();
            builder.Services.AddSingleton<IEquipmentRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).EquipmentRepository);
            builder.Services.AddSingleton<EquipmentService>();
            builder.Services.AddSingleton<IReservationRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).ReservationRepository);
            builder.Services.AddSingleton<ReservationService>();
            builder.Services.AddSingleton<IProgramRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).ProgramRepository);
            builder.Services.AddSingleton<ProgramService>();
            builder.Services.AddSingleton<IRunningSessionRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).RunningRepository);
            builder.Services.AddSingleton<RunningSessionService>();
            builder.Services.AddSingleton<ICyclingRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).CyclingRepository);
            builder.Services.AddSingleton<CyclingSessionService>();
            builder.Services.AddSingleton<ITrainingRepository>(r => FitnessDataLayerProvider.FitnessDatalayerFactory.GeefRepositories(connectionString, dataLayer).TrainingRepository);
            builder.Services.AddSingleton<TrainingService>();
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
