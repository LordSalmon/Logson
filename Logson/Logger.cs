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

        public Logger Information(string project, string message)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(this.LogConfig.BuildMessage(project, message));
            Console.ResetColor();
            return this;
        }

        public Logger Warning(string project, string message)
        {
            this.WriteMessage(ConsoleColor.Magenta, project, message);
            return this;
        }

        public Logger Debug(string project, string message)
        {
            this.WriteMessage(ConsoleColor.DarkCyan, project, message);
            return this;
        }

        public Logger Error(string project, string message)
        {
            this.WriteMessage(ConsoleColor.Red, project, message);
            return this;
        }

        public Logger Success(string project, string message)
        {
            this.WriteMessage(ConsoleColor.Green, project, message);
            return this;
        }

        public Logger NewSection()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(new string(this.LogConfig.NewSectionCharacter, this.LogConfig.NewSectionLength == -1 ? Console.WindowWidth : this.LogConfig.NewSectionLength));
            Console.ResetColor();
            return this;
        }

        private async Task<bool> WriteMessage(ConsoleColor foreGroundColor, string project, string message)
        {
            Console.ForegroundColor = foreGroundColor;
            if (this.LogConfig.UseBackgroundColor)
            {
                Console.BackgroundColor = this.LogConfig.BackgroundColor;
            }

            string finalMessage = this.LogConfig.BuildMessage(project, message);
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