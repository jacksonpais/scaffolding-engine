using ScaffoldingEngine.CLI.Handlers;
using System.CommandLine;

namespace ScaffoldingEngine.CLI.Commands
{
    public class AddSolutionCommand
    {
        public static Command Build()
        {
            var newCommand = new Command("new")
            {
                Description = "Create new resources"
            };

            var solutionCommand = new Command("solution")
            {
                Description = "Create a new solution"
            };

            var nameArgument = new Argument<string>("name")
            {
                Description = "Solution name"
            };

            var pathOption = new Option<string>("--path")
            {
                Description = "Target root path",
                Required = true
            };

            solutionCommand.Arguments.Add(nameArgument);
            solutionCommand.Options.Add(pathOption);

            solutionCommand.SetAction(async (parseResult, token) =>
            {
                var name = parseResult.GetValue<string>("name");
                var path = parseResult.GetValue<string>("--path");

                await AddSolutionHandler.Handle(name!, path!);
            });

            newCommand.Subcommands.Add(solutionCommand);

            return newCommand;
        }
    }
}
