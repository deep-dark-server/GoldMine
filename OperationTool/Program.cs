using GoldMine.ServerBase.Init;
using GoldMine.ServerBase.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoldMine.OperationTool
{
    public class Program
    {
        public static bool IsInteractive
        {
            get; private set;
        }

        private bool _isExit;
        private readonly Dictionary<ConsoleKey, Command> _commands = new Dictionary<ConsoleKey, Command>();

        private static void Main(string[] args)
        {
            PostAppInit.Do();
            var pg = new Program();
            pg.LoadCommands();
            pg.ProgramMain(args);
        }

        private void ProgramMain(string[] args)
        {
            if (args.Length < 1)
            {
                IsInteractive = true;
                while (!_isExit)
                {
                    PrintMenu();
                    Console.WriteLine("Press key in [] to perform command, Ctrl+Q to exit");
                    var keyInfo = Console.ReadKey();

                    if (ProcessCommand(keyInfo)) continue;

                    Console.Clear();
                    Console.WriteLine("Invalid Command. (Don't press any key modifiers for command)");
                    ToMainMenu();
                }
            }
            else
            {
                var options = new Options();
                if (!CommandLine.Parser.Default.ParseArguments(args, options)) return;
                IsInteractive = false;
                foreach (var cmd in _commands.Values.Where(cmd => cmd.IsRunnable(options)))
                {
                    cmd.Run();
                }
            }
        }

        public static void ToMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Press enter to go back to menu");
            Console.ReadLine();
            Console.Clear();
        }

        private void LoadCommands()
        {
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(
                type => type.IsClass &&
                type.IsSubclassOf(typeof(Command)) &&
                !type.IsAbstract);
            foreach (var type in types)
            {
                var commandObj = (Command)Activator.CreateInstance(type);
                _commands.Add(commandObj.InvokeKey, commandObj);
            }
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("== Menu ==");
            foreach (var command in _commands)
            {
                Console.Write("[");
                Console.Write(command.Key);
                Console.Write("]");
                Console.Write(" ");
                Console.WriteLine(command.Value.ToString());
            }
            Console.WriteLine();
        }

        private bool ProcessCommand(ConsoleKeyInfo key)
        {
            if (key.Modifiers.HasFlag(ConsoleModifiers.Control))
            {
                if (key.Key != ConsoleKey.Q) return false;
                _isExit = true;
                return true;
            }
            else if (key.Modifiers.HasFlag(ConsoleModifiers.Alt) || key.Modifiers.HasFlag(ConsoleModifiers.Shift))
            {
                return false;
            }

            if (_commands.ContainsKey(key.Key))
            {
                var command = _commands[key.Key];

                try
                {
                    Console.Clear();
                    command.Run();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine($"There were some errors while running command {command}");
                    e.WriteLog();
                    ToMainMenu();
                }
                return true;
            }
            else
                return false;
        }
    }
}