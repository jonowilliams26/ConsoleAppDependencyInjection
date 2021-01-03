using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleAppDependencyInjection
{
    public class Program
    {
        private readonly ILogger<Program> logger;
        private readonly ServiceA serviceA;


        static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            host.Services.GetRequiredService<Program>().Run();
        }

        public Program(ILogger<Program> logger, ServiceA serviceA)
        {
            this.logger = logger;
            this.serviceA = serviceA;
        }

        public void Run()
        {
            logger.LogInformation("Program is running.");
            serviceA.DoSomething();
            logger.LogInformation("Program is completed.");
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddTransient<Program>();
                    services.AddTransient<ServiceA>();
                    services.AddTransient<ServiceB>();
                });
        }
    }

    public class ServiceA
    {
        private readonly ServiceB serviceB;

        public ServiceA(ServiceB serviceB)
        {
            this.serviceB = serviceB;
        }

        public void DoSomething() => serviceB.DoSomething();
    }

    public class ServiceB
    {
        private readonly ILogger<ServiceB> logger;

        public ServiceB(ILogger<ServiceB> logger)
        {
            this.logger = logger;
        }

        public void DoSomething() => logger.LogInformation("Service B is doing something.");
    }
}
