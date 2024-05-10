using System.Diagnostics;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator log");
            Trace.WriteLine(string.Format("Started {0}", System.DateTime.Now.ToString()));

        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "Not A Number" for invalid operation

            // Use a switch statement to do math
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    Trace.WriteLine($"{num1} + {num2} = {result}");
                    break;
                case "s":
                    result = num1 - num2;
                    Trace.WriteLine($"{num1} - {num2} = {result}");
                    break;
                case "m":
                    result = num1 * num2;
                    Trace.WriteLine($"{num1} * {num2} = {result}");
                    break;
                case "d":
                    // ask the user to enter a non-zero diviser
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        Trace.WriteLine($"{num1} / {num2} = {result}");
                    }
                    break;

                default:
                    break;
            }
            return result;
        }
    }
}
