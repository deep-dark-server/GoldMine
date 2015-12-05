using Gnu.Getopt;
using System;
using System.Reflection;

namespace GoldMine.OperationTool
{
    public abstract class Command
    {
        public abstract LongOpt CliOption { get; }

        public abstract ConsoleKey InvokeKey { get; }

        public abstract void Run();

        public override string ToString()
        {
            return MethodBase.GetCurrentMethod().DeclaringType.Name;
        }
    }
}