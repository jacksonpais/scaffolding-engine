using ScaffoldingEngine.CLI.Handlers;
using System.CommandLine;

namespace ScaffoldingEngine.CLI.Commands
{
    public static class AddFeatureCommand
    {
        public static Command Build()
        {
            var addCommand = new Command("add")
            {
                Description = "Add resources"
            };

            var featureCommand = new Command("feature")
            {
                Description = "Create a new feature"
            };

            // Argument (name is mandatory now)
            var featureNameArgument = new Argument<string>("name")
            {
                Description = "Feature name"
            };

            // Option (name mandatory, description set separately)
            var pathOption = new Option<string>("--path")
            {
                Description = "Target folder path",
                Required = true
            };

            // 🔥 NEW API usage
            featureCommand.Arguments.Add(featureNameArgument);
            featureCommand.Options.Add(pathOption);

            // 🔥 SetAction replaces SetHandler
            featureCommand.SetAction(async (parseResult, cancellationToken) =>
            {
                var name = parseResult.GetValue<string>("name");
                var path = parseResult.GetValue<string>("--path");

                await AddFeatureHandler.Handle(name!, path!);
            });

            addCommand.Subcommands.Add(featureCommand);

            return addCommand;
        }
    }
}
