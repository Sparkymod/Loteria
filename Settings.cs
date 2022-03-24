﻿using Loteria.Database;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Loteria
{
    public static class Settings
    {
        public static void InitDatabase()
        {
            using DatabaseManager manager = new();
            if (manager.DatabaseExists(GetDatabase()))
            {
                Log.Logger.Information("Database already exists.");
            }
        }

        #region Connection Strings
        /// <summary>
        /// Load Database settings.
        /// </summary>
        /// <returns></returns>
        internal static string GetConnectionString()
        {
            string server = Environment.GetEnvironmentVariable("DB_IP") ?? "";
            string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "";
            string database = Environment.GetEnvironmentVariable("DB_NAME") ?? "";
            string user = Environment.GetEnvironmentVariable("DB_USER") ?? "";
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

            return $"SERVER={server};PORT={port};USER={user};PASSWORD={password};DATABASE={database};";
        }

        internal static string GetConnectionStringWithoutDB()
        {
            string server = Environment.GetEnvironmentVariable("DB_IP") ?? "";
            string port = Environment.GetEnvironmentVariable("DB_PORT") ?? "";
            string user = Environment.GetEnvironmentVariable("DB_USER") ?? "";
            string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

            return $"SERVER={server};PORT={port};USER={user};PASSWORD={password};";
        }

        internal static string GetDatabase() => Environment.GetEnvironmentVariable("DB_NAME") ?? "";

        #endregion

        public static bool IsDevelopingMode()
        {
            string result = Environment.GetEnvironmentVariable("DEVELOPMENT") ?? "";
            return result == "true";
        }

        public static Logger InitializeSerilog()
        {
            Log.Logger = Serilog.Config().CreateLogger();
            return Serilog.Config().CreateLogger();
        }

        public static string GetURL()
        {
            DotEnv.Load();
            return Environment.GetEnvironmentVariable("USE_URL") ?? "";
        }

        public static void ReleaseMemory()
        {
            GC.Collect(3);
            GC.WaitForPendingFinalizers();
        }
    }

    // Serilog Settings.
    public static class Serilog
    {
        public static string Template { get; set; } = "{Timestamp:HH:mm:ss} [{Level:u4}]: {Message:lj} {NewLine}" + "{Exception}";
        public static string FileTemplate { get; set; } = "{Timestamp} [{Level:u4}]: {Message:lj} {NewLine}" + "{Exception}";

        /// <summary>
        /// Custom configuration for serilog to show on console and save to file.
        /// </summary>
        public static LoggerConfiguration Config()
        {
            string logPath = Path.Combine(Paths.PRODUCTION_DIR, $"{AppDomain.CurrentDomain.FriendlyName}-Logs.log");
            if (!File.Exists(logPath))
            {
                File.Create(logPath);
            }
            return new LoggerConfiguration()
                .MinimumLevel.Override("Default", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: Theme.RDKSerilogTheme, outputTemplate: Template)
                .WriteTo.File(logPath, LogEventLevel.Error, outputTemplate: FileTemplate);
        }
    }

    // Diferent styles for specific things.
    public static class Theme
    {
        public static CustomConsoleTheme RDKSerilogTheme { get; } = new CustomConsoleTheme();

        public sealed class CustomConsoleTheme : ConsoleTheme
        {
            /// <summary>
            /// True if styling applied by the theme is written into the output, and can thus be
            /// buffered and measured.
            /// </summary>
            public override bool CanBuffer => false;

            /// <summary>
            /// Begin a span of text in the specified <paramref name="style"/>.
            /// </summary>
            /// <param name="output">Output destination.</param>
            /// <param name="style">Style to apply.</param>
            /// <returns></returns>
            protected override int ResetCharCount => 0;

            /// <summary>
            /// Reset the output to un-styled colors.
            /// </summary>
            /// <param name="output">The output.</param>
            public override void Reset(TextWriter output)
            {
                Console.ResetColor();
            }

            // Custom RDK Theme
            /// <summary>
            /// The number of characters written by the <see cref="Reset(TextWriter)"/> method.
            /// </summary>
            public override int Set(TextWriter output, ConsoleThemeStyle style)
            {
                Console.BackgroundColor = ConsoleColor.Black;

                switch (style)
                {
                    case ConsoleThemeStyle.Text:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleThemeStyle.SecondaryText:
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case ConsoleThemeStyle.TertiaryText:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case ConsoleThemeStyle.Null:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case ConsoleThemeStyle.Number:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case ConsoleThemeStyle.LevelInformation:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case ConsoleThemeStyle.LevelWarning:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case ConsoleThemeStyle.LevelError:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case ConsoleThemeStyle.LevelFatal:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    default:
                        break;
                }
                return 0;
            }
        }
    }

    // Dot Environment Settings.
    public static class DotEnv
    {
        public static string FilePath { get; set; } = string.Empty;

        public static void Load(string filepath = ".env")
        {
            FilePath = Settings.IsDevelopingMode() ? Path.Combine(Paths.SOLUTION_DIR, filepath) : Path.Combine(Paths.PRODUCTION_DIR, filepath);

            if (!File.Exists(FilePath))
            {
                StreamWriter writer = new(FilePath) { AutoFlush = true };

                writer.WriteLine("# Database Info");
                writer.WriteLine("DB_IP=localhost");
                writer.WriteLine("DB_PORT=3306");
                writer.WriteLine("DB_NAME=dbName");
                writer.WriteLine("DB_USER=root");
                writer.WriteLine("DB_PASSWORD=admin");
                writer.WriteLine("DEVELOPMENT=false");
                writer.WriteLine("USE_URL=http://localhost:5000");
                writer.Close();
            }
            foreach (string line in File.ReadAllLines(FilePath))
            {
                string[] parts = line.Split('=', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                {
                    continue;
                }

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }

    public static class Paths
    {
        public static readonly string SOLUTION_DIR = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "../../../.."));
        public static readonly string PRODUCTION_DIR = Environment.CurrentDirectory;
    }
}
