namespace OpenDutch
{
    internal class Program
    {
        private static bool ConfirmExit()
        {
            Console.Write("Are you sure you want to exit? (or just translate the word 'exit'?) (y/n): ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }
            return input.Equals("y", StringComparison.OrdinalIgnoreCase) || input.Equals("yes", StringComparison.OrdinalIgnoreCase);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("OpenDutch - English to Dutch Translator, without NMT");
            Console.WriteLine("A probably better version of the Georgetown-IBM translator");
            Console.WriteLine("Made by Novixx Systems, licensed under the GNU GPL v3");

            Translator.Init();

            while (true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine();
                    continue;
                }
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    if (ConfirmExit())
                    {
                        break;
                    }
                }

                string output = Translator.Translate(input);
                if (string.IsNullOrEmpty(output))
                {
                    Console.WriteLine("Bug: No translation found, shouldn't happen but apparently does.");
                }
                else
                {
                    Console.WriteLine($"Translation: {output}");
                }
            }
        }
    }
}
