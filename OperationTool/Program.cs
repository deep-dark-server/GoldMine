using System;
using System.Collections.Generic;

namespace GoldMine.OperationTool
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Program pg = new Program();
            pg.Main();
        }

        private bool isExit = false;
        private Dictionary<ConsoleKey, Command> commands;

        private void Main()
        {
            ConsoleKeyInfo keyInfo;
            while (!isExit)
            {
                Console.WriteLine("Press key in [] to perform command, Ctrl+Q to exit");
                keyInfo = Console.ReadKey();
                while (!ProcessCommand(keyInfo))
                {
                    Console.WriteLine("Invalid Command. (Don't press any key modifiers for command)");
                    keyInfo = Console.ReadKey();
                }
            }
        }

        private void LoadCommands()
        {
            
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
                if (!command.Run())
                {
                    Console.WriteLine("There were some errors while running command {0}", command.ToString());
                }
                Console.WriteLine("Press enter to go back to menu");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            else
                return false;
        }
    }
}