using System;
using System.IO;
using System.Threading.Tasks;

namespace Logson
{
    public class Logger
    {
        public LogConfig LogConfig { get; set; }

        public Logger(LogConfig config)
        {
            this.LogConfig = config;
        }

        public Logger Information(string project, string action, string message)
        {
            WriteMessage(ConsoleColor.Magenta, project, action, message);
            return this;
        }

        public Logger Warning(string project, string action, string message)
        {
            WriteMessage(ConsoleColor.Yellow, project, action, message);
            return this;
        }

        public Logger Debug(string project, string action, string message)
        {
            WriteMessage(ConsoleColor.DarkCyan, project, action, message);
            return this;
        }

        public Logger Error(string project, string action, string message)
        {
            WriteMessage(ConsoleColor.Red, project, action, message);
            return this;
        }

        public Logger Success(string project, string action, string message)
        {
            WriteMessage(ConsoleColor.Green, project, action, message);
            return this;
        }

        public Logger NewSection()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(new string(this.LogConfig.NewSectionCharacter, this.LogConfig.NewSectionLength == -1 ? Console.WindowWidth : this.LogConfig.NewSectionLength));
            Console.ResetColor();
            return this;
        }

        private async Task<bool> WriteMessage(ConsoleColor foreGroundColor, string project, string action, string message)
        {
            Console.ForegroundColor = foreGroundColor;
            if (this.LogConfig.UseBackgroundColor)
            {
                Console.BackgroundColor = this.LogConfig.BackgroundColor;
            }

            string finalMessage = this.LogConfig.BuildMessage(project, action, message);
            Console.WriteLine(finalMessage);
            if (this.LogConfig.SaveToFile)
            {
                await File.AppendAllLinesAsync(this.LogConfig.LogFilePath, new[] {finalMessage});
            }

            Console.ResetColor();
            return true;
        }
    }
}