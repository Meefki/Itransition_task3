namespace task3
{
    internal class ConsoleHelper
    {
        public void WriteErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{message}\n");
            Console.ResetColor();
        }

        public void WriteInfoMessage(string message = "", ConsoleColor messageColor = ConsoleColor.White)
        {
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public string ReadMove()
        {
            Console.Write("Enter your move: ");
            return Console.ReadLine() ?? "0";
        }
    }
}
