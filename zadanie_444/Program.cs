using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using zadanie_444.configuration;
using zadanie_444.services;

namespace zadanie_444
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AppOptions>(hostContext.Configuration.GetSection(AppOptions.SectionName));

                    services.AddTransient<IFileEnumeratorService, FileEnumeratorService>();
                    services.AddTransient<IStatisticsCalculator, StatisticsCalculator>();
                    services.AddTransient<ICsvReportGenerator, CsvReportGenerator>();
                
                    services.AddTransient<ApplicationOrchestrator>();
                })
                .Build();

            var appOrchestrator = host.Services.GetRequiredService<ApplicationOrchestrator>();
        
            appOrchestrator.Run();
        }
    }
}