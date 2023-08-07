using task3;

internal class Program
{
    private static GameSession _gameSession = null!;
    private static ConsoleHelper _console = null!;

    private static void Main(string[] args)
    {
        _console = new();

        string errorMsg = ValidateInput(args);
        if (!string.IsNullOrEmpty(errorMsg))
        {
            _console.WriteErrorMessage(errorMsg);
            _console.WriteInfoMessage("Press any key to close the programm...", ConsoleColor.White);
            Console.ReadKey();
            return;
        }
            

        _gameSession = new(args);

        bool isFirstRound = true;

        while (true)
        {
            if (!isFirstRound)
                _console.WriteInfoMessage("Next round:");

            bool isContinue = _gameSession.Run();

            if (!isContinue)
                return;
        }
    }

    private static bool IsContainDuplicates(string[] args)
    {
        List<string> sortedArgs = args.OrderBy(arg => arg).ToList();

        for (int i = 0; i < sortedArgs.Count - 1; i++)
        {
            if (sortedArgs[i] == sortedArgs[i + 1])
                return true;
        }

        return false;
    }

    private static string ValidateInput(string[] args)
    {
        if (args.Length < 3)
        {
            return $"Amount of input parameters should be more then 2 ({args.Length} now)!";
        }

        if (args.Length % 2 != 1)
        {
            return $"Amount of input parameters shouldn't be even!";
        }

        if (IsContainDuplicates(args))
        {
            return "The input parameters contain duplicates!";
        }

        return string.Empty;
    }
}