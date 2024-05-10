// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

using CalculatorLibrary;

class Program
{
    static void Main(string[] args)
    {
        bool endApp = false;

        Calculator calculator = new Calculator();

        // display title for the C# app
        Console.WriteLine("Console Calculator with C#");
        Console.WriteLine("--------------------------"); 

        while(!endApp)
        {
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // ask the user to enter the first number
            Console.Write("Type a number, then press enter");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not a valid number, please try again");
                numInput1 = Console.ReadLine();
            }

            // ask the user to enter the second number
            Console.Write("Type a second number, then press enter");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while(!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not a valid number, please try again");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();

            // Validate
            if (op == null || !Regex.IsMatch(op, "[a|s|m|d]"))
            {
                Console.WriteLine("Invalid operant");
            }
            else
            {
                try
                {
                    result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                    if (double.IsNaN(result))
                    {
                        Console.WriteLine("This operation will results in nan");
                    }
                    else
                    {
                        Console.WriteLine("The result is: {0:0.##}\n", result);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Oh no, an exception occured. \n - Details: " + e.Message);
                }
            }
            Console.WriteLine("--------------------\n");

            // Wait for the user to respond
            Console.Write("Enter n if you would like to quit; other keys to continue");
            endApp = Console.ReadLine() == "n";
            Console.WriteLine("\n");
        }
        return;
    }
}