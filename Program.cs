﻿using task3;

internal class Program
{
    private static GameSession _gameSession = null!;
    private static ConsoleHelper _console = null!;

    private static void Main(string[] args)
    {
        args = new string[] { "Rock", "Paper", "Scissors" };

        ValidateInput(args);
        Init(args);

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

    private static void Init(string[] args)
    {
        _gameSession = new(args);
        _console = new();
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

    private static bool ValidateInput(string[] args)
    {
        if (args.Length < 3)
        {
            _console.WriteErrorMessage($"Amount of input parameters should be more then 2 ({args.Length} now)!");
            Console.ReadKey();
            return false;
        }

        if (args.Length % 2 != 1)
        {
            _console.WriteErrorMessage($"Amount of input parameters shouldn't be even!");
            Console.ReadKey();
            return false;
        }

        if (IsContainDuplicates(args))
        {
            _console.WriteErrorMessage("The input parameters contain duplicates!");
            Console.ReadKey();
            return false;
        }

        return true;
    }
}