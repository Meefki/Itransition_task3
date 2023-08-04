using System.Text;

namespace task3
{
    internal class GameSession
    {
        private const int KEY_LENGTH = 256 / 8;

        private readonly KeyGenerator _keyGenerator;
        private readonly HmacSha3Generator _hmacGenerator;
        private readonly Rules _rules;
        private readonly Help _help;
        private readonly ConsoleHelper _console;

        public GameSession(string[] moves)
        {
            _keyGenerator = new();
            _hmacGenerator = new(KEY_LENGTH);
            _rules = new(moves);
            _help = new(_rules);
            _console = new();
        }

        public bool Run()
        {
            byte[] key = _keyGenerator.Create(KEY_LENGTH);
            int compMove = _rules.CompMove();
            string hmac = _hmacGenerator.Create(key, _rules.Moves[compMove]);

            _console.WriteInfoMessage($"HMAC: {hmac}", ConsoleColor.Yellow);

            string menu = GetMenu(_rules.Moves);

            int userMove = -1;
            while (true)
            {
                _console.WriteInfoMessage(menu, ConsoleColor.Blue);

                string userMoveString = _console.ReadMove();

                switch (userMoveString)
                {
                    case "0":
                        return false;
                    case "?":
                        _help.PrintHelp();
                        continue;
                    default:
                        bool result = int.TryParse(userMoveString, out int userMoveIndex);
                        if (!result || 0 > userMoveIndex || _rules.Moves.Count < userMoveIndex)
                        {
                            _console.WriteErrorMessage("Wrong number of move or command!");
                            continue;
                        }
                        userMove = userMoveIndex - 1;
                        break;
                };

                if (userMove >= 0)
                    break;
            }

            PrintResult(compMove, userMove, key);
            return true;
        }

        private string ConvertResultToString(int res) => res switch
        {
            -1 => "You lose! :(",
            0 => "Draw!",
            1 => "You win!",
            _ => throw new ArgumentException("Wrong result value")
        };

        private string GetMenu(IList<string> moves)
        {
            StringBuilder sb = new();
            sb.Append("Available moves:\n");
            for (int i = 0; i < moves.Count; i++)
            {
                sb.Append($"{i + 1} - {moves[i]}\n");
            }
            sb.Append("0 - exit\n");
            sb.Append("? - help");
            return sb.ToString() ?? "";
        }

        private void PrintResult(int compMove, int userMove, byte[] key)
        {
            _console.WriteInfoMessage($"Your move: {_rules.Moves[userMove]}");
            _console.WriteInfoMessage($"Computer move: {_rules.Moves[compMove]}");

            int roundResult = _rules.CalculateWinner(compMove, userMove);
            string winnerMessage = ConvertResultToString(roundResult);

            _console.WriteInfoMessage(winnerMessage);

            string keyStr = BitConverter.ToString(key).Replace("-", "");
            _console.WriteInfoMessage($"HMAC key: {keyStr}", ConsoleColor.Yellow);
            _console.WriteInfoMessage();
            _console.WriteInfoMessage(new string('-', 80));
        }

    }
}
