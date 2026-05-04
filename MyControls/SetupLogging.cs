using PostSharp.Aspects;
using Serilog;
using Serilog.Events;
using System;
using System.IO;

namespace MyControls
{
    public class SetupLogging
    {
        [ModuleInitializer(0)]
        public static void Init()
        {
            Initialize();
        }
        public static void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "debug.log"),
            restrictedToMinimumLevel: LogEventLevel.Debug, rollingInterval: RollingInterval.Day,
            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Debug}")

            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "info.log"),
            restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day,
            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Information}")

            .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LogFiles", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}", "error.log"),
            restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day,
             outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}")
            .CreateLogger();
        }
    }
}
