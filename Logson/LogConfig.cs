using System;
using System.IO;
using System.Linq;

namespace Logson
{
    public class LogConfig
    {
        public bool AddTimeStamp { get; set; }
        public bool SaveToFile { get; set; }
        public string LogFilePath { get; set; }
        public string Separator { get; set; }
        public bool UseBackgroundColor { get; set; }
        public int MaxProjectLength { get; set; }
        public int MaxActionLength { get; set; }
        public ConsoleColor BackgroundColor { get; set; }
        public bool CapitalizeAction { get; set; }
        public char NewSectionCharacter { get; set; }
        public int NewSectionLength { get; set; }

        public string BuildMessage(string project, string action, string message)
        {
            string result = "";
            if (AddTimeStamp)
            {
                result += $"[ {DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")} ]";
            }

            if (project.Length > MaxProjectLength)
            {
                project = project.Substring(0, MaxProjectLength);
            }
            else
            {
                project = new string(' ', (MaxProjectLength - project.Length) / 2) + project +
                          new string(' ', (MaxProjectLength - project.Length) / 2);
            }

            project += project.Length % 2 == 1 ? " " : "";

            if (CapitalizeAction)
            {
                string[] actionSplit = action.Split(" ");
                string actionEdit = action;

                for (int i = 0; i < actionSplit.Length; i++)
                {
                    actionSplit[i] = actionSplit[i][0].ToString().ToUpper() + actionSplit[i].Substring(1);
                }

                action = String.Join("", actionSplit);
            }

            if (action.Length > MaxActionLength)
            {
                action = action.Substring(0, MaxActionLength);
            }
            else
            {
                action = new string(' ', (MaxActionLength - action.Length) / 2) + action +
                         new string(' ', (MaxActionLength - action.Length) / 2);
            }

            return result += $"[ {project} ][ {action} ] {Separator} {message}";
        }

        public LogConfig SetLogFile(string logFilePath)
        {
            this.SaveToFile = true;
            this.LogFilePath = logFilePath;
            if (!File.Exists(logFilePath))
            {
                File.Create(logFilePath).Close();
            }

            return this;
        }
    }
}