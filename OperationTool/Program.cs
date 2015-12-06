using Gnu.Getopt;
using GoldMine.ServerBase.Init;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GoldMine.OperationTool
{
    public class Program
    {
        public readonly static ILog LogError = LogManager.GetLogger("Error");

        public static bool IsInteractive
        {
            get; private set;
        }
        private bool isExit = false;
        private Dictionary<ConsoleKey, Command> commands = new Dictionary<ConsoleKey, Command>();
        private Dictionary<int, Command> optDict = new Dictionary<int, Command>();

        private static void Main(string[] args)
        {
            PostAppInit.Do();
            Program pg = new Program();
            pg.LoadCommands();
            pg.ProgramMain(args);
        }

        private void ProgramMain(string[] args)
        {
            Getopt getOpt = new Getopt("GoldMineOperation", args, "", optDict.Values.Select(value => value.CliOption).ToArray());
            int c;
            while ((c = getOpt.getopt()) != -1)
            {
                Command cmd;
                if (optDict.TryGetValue(c, out cmd))
                {
                    IsInteractive = false;
                    cmd.Run();
                }
                else
                {
                    IsInteractive = true;
                    ConsoleKeyInfo keyInfo;
                    while (!isExit)
                    {
                        PrintMenu();
                        Console.WriteLine("Press key in [] to perform command, Ctrl+Q to exit");
                        keyInfo = Console.ReadKey();
                        if (!ProcessCommand(keyInfo))
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Command. (Don't press any key modifiers for command)");
                            ToMainMenu();
                        }
                    }
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
                commands.Add(commandObj.InvokeKey, commandObj);
                optDict.Add(commandObj.CliOption.Val, commandObj);
            }
        }

        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("== Menu ==");
            foreach (var command in commands)
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
                if (key.Key == ConsoleKey.Q)
                {
                    isExit = true;
                    return true;
                }
                return false;
            }
            else if (key.Modifiers.HasFlag(ConsoleModifiers.Alt) || key.Modifiers.HasFlag(ConsoleModifiers.Shift))
            {
                return false;
            }

            if (commands.ContainsKey(key.Key))
            {
                var command = commands[key.Key];

                try
                {
                    Console.Clear();
                    command.Run();
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("There were some errors while running command {0}", command.ToString());
                    LogError.Error(e.ToString());
                    ToMainMenu();
                }
                return true;
            }
            else
                return false;
        }
    }
}