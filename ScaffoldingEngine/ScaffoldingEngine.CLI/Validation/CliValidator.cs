namespace ScaffoldingEngine.CLI.Validation
{
    public static class CliValidator
    {
        public static void ValidateFeature(string name, string path)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Feature name is required.");

            if (!IsValidName(name))
                throw new ArgumentException("Feature name must be alphanumeric (PascalCase).");

            if (!Directory.Exists(path))
                throw new ArgumentException("Target path does not exist.");
        }

        public static void ValidateSolution(string name, string path)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Solution name is required.");

            if (!IsValidName(name))
                throw new ArgumentException("Invalid solution name.");

            if (!Directory.Exists(path))
                throw new ArgumentException("Target path does not exist.");

            var solutionPath = Path.Combine(path, name);

            if (Directory.Exists(solutionPath))
                throw new InvalidOperationException("Solution already exists.");
        }

        private static bool IsValidName(string name)
        {
            return name.All(char.IsLetterOrDigit);
        }
    }
}
