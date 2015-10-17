using System;

namespace GoldMine.OperationTool
{
    public abstract class Command
    {
        public abstract ConsoleKey InvokeKey { get; }
        public abstract bool Run();
    }
}