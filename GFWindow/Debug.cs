using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFWindow
{
    internal static class Debug
    {
        public static void Log<T>(T log) => Console.WriteLine(log);
        public static void Log<T>(T log, LogLevel level)
        {
            ConsoleColor color;
            switch (level)
            {
                case LogLevel.Debug:
                    color = (ConsoleColor)LogLevel.Debug;
                    break;
                case LogLevel.Info:
                    color = (ConsoleColor)LogLevel.Info;
                    break;
                case LogLevel.Ready:
                    color = (ConsoleColor)LogLevel.Ready;
                    break;
                case LogLevel.Warning:
                    color = (ConsoleColor)LogLevel.Warning;
                    break;
                case LogLevel.Error:
                    color = (ConsoleColor)LogLevel.Error;
                    break;
                case LogLevel.Fatal:
                    color = (ConsoleColor)LogLevel.Fatal;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }
            Console.ForegroundColor = color;
            Console.WriteLine(log);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Log(object log, LogLevel level)
        {
            ConsoleColor color;
            switch (level)
            {
                case LogLevel.Debug:
                    color = (ConsoleColor)LogLevel.Debug;
                    break;
                case LogLevel.Info:
                    color = (ConsoleColor)LogLevel.Info;
                    break;
                case LogLevel.Ready:
                    color = (ConsoleColor)LogLevel.Ready;
                    break;
                case LogLevel.Warning:
                    color = (ConsoleColor)LogLevel.Warning;
                    break;
                case LogLevel.Error:
                    color = (ConsoleColor)LogLevel.Error;
                    break;
                case LogLevel.Fatal:
                    color = (ConsoleColor)LogLevel.Fatal;
                    break;
                default:
                    color = ConsoleColor.White;
                    break;
            }
            Console.ForegroundColor = color;
            Console.WriteLine(log);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogWarn<T>(T log)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(log);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void LogError()
        {
            throw new InvalidOperationException();
        }
        public static void LogError<T>(T log)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(log);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public enum LogLevel
        {
            Debug = ConsoleColor.Cyan,
            Info = ConsoleColor.White,
            Ready = ConsoleColor.Green,
            Warning = ConsoleColor.Yellow,
            Error = ConsoleColor.Red,
            Fatal = ConsoleColor.DarkRed
        }
    }
}
