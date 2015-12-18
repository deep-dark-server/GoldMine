using CommandLine;
using CommandLine.Text;

namespace GoldMine.OperationTool
{
    public class Options
    {
        [Option('a', "adduser", DefaultValue = true, HelpText = "Add user to DB")]
        public bool AddUser { get; set; }

        [HelpOption]
        public string Help()
        {
            return HelpText.AutoBuild(this, (current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}