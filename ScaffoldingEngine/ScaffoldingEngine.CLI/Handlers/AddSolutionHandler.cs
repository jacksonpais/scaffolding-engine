using System.Diagnostics;

namespace ScaffoldingEngine.CLI.Handlers
{
    public class AddSolutionHandler
    {
        public static async Task Handle(string solutionName, string rootPath)
        {
            try
            {
                var solutionPath = Path.Combine(rootPath, solutionName);

                Console.WriteLine($"Creating solution at: {solutionPath}");

                Directory.CreateDirectory(solutionPath);

                // Step 1: Create solution
                await RunCommand("dotnet", $"new sln -n {solutionName}", solutionPath);

                // Step 2: Create projects
                await CreateProject(solutionPath, solutionName, "Api", "webapi");
                await CreateProject(solutionPath, solutionName, "Application", "classlib");
                await CreateProject(solutionPath, solutionName, "Domain", "classlib");
                await CreateProject(solutionPath, solutionName, "Infrastructure", "classlib");

                // Step 3: Add projects to solution
                await AddProjectToSolution(solutionPath, solutionName);

                // Step 4: Add references
                await AddReferences(solutionPath, solutionName);

                Console.WriteLine("✅ Solution created successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error:");
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task CreateProject(string solutionPath, string solutionName, string suffix, string template)
        {
            var projectName = $"{solutionName}.{suffix}";
            await RunCommand("dotnet", $"new {template} -n {projectName}", solutionPath);
        }

        private static async Task AddProjectToSolution(string solutionPath, string solutionName)
        {
            var projects = new[] { "Api", "Application", "Domain", "Infrastructure" };

            foreach (var project in projects)
            {
                var projectPath = $"{solutionName}.{project}/{solutionName}.{project}.csproj";
                await RunCommand("dotnet", $"sln add {projectPath}", solutionPath);
            }
        }

        private static async Task AddReferences(string solutionPath, string solutionName)
        {
            await RunCommand("dotnet",
                $"add {solutionName}.Api/{solutionName}.Api.csproj reference {solutionName}.Application/{solutionName}.Application.csproj",
                solutionPath);

            await RunCommand("dotnet",
                $"add {solutionName}.Application/{solutionName}.Application.csproj reference {solutionName}.Domain/{solutionName}.Domain.csproj",
                solutionPath);

            await RunCommand("dotnet",
                $"add {solutionName}.Infrastructure/{solutionName}.Infrastructure.csproj reference {solutionName}.Application/{solutionName}.Application.csproj",
                solutionPath);
        }

        private static async Task RunCommand(string fileName, string arguments, string workingDirectory)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = arguments,
                    WorkingDirectory = workingDirectory,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            process.Start();

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            process.WaitForExit();

            if (!string.IsNullOrWhiteSpace(output))
                Console.WriteLine(output);

            if (!string.IsNullOrWhiteSpace(error))
                Console.WriteLine(error);

            if (process.ExitCode != 0)
                throw new Exception($"Command failed: {fileName} {arguments}");
        }
    }
}
