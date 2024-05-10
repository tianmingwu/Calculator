using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        public string LogMode { get; private set; }
        JsonWriter Writer { get; set; }

        /// <summary>
        /// Initialize a Calculator object with log; logMode = "none", "json" or "trace"
        /// </summary>
        /// <param name="logMode"></param>
        public Calculator(string logMode)
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            LogMode = logMode;

            Writer = new JsonTextWriter(logFile)
            {
                Formatting = Formatting.Indented
            };

            switch (logMode.ToLower())
            {
                case "none":
                    break;
                case "trace":
                    Trace.Listeners.Add(new TextWriterTraceListener(logFile));
                    Trace.AutoFlush = true;
                    Trace.WriteLine("Starting Calculator log");
                    Trace.WriteLine(string.Format("Started {0}", System.DateTime.Now.ToString()));
                    break;
                case "json":
                    Writer.WriteStartObject();
                    Writer.WritePropertyName("Operations");
                    Writer.WriteStartArray();
                    break;
                default:
                    throw (new NotImplementedException());                    
            }
        }
        public double DoOperation(double num1, double num2, string op)
        {
            double result = double.NaN; // Default value is "Not A Number" for invalid operation

            if (LogMode=="json")
            {
                Writer.WriteStartObject();
                Writer.WritePropertyName("Operand1");
                Writer.WriteValue(num1);
                Writer.WritePropertyName("Operand2");
                Writer.WriteValue(num2);
                Writer.WritePropertyName("Operation");
            }
            // Use a switch statement to do math
            switch (op)
            {
                case "a":
                    result = num1 + num2;
                    if (LogMode == "trace")
                    { Trace.WriteLine($"{num1} + {num2} = {result}"); }
                    else if (LogMode == "json")
                    { Writer.WriteValue("Add"); }
                    break;
                case "s":
                    result = num1 - num2;
                    if (LogMode == "trace")
                    { Trace.WriteLine($"{num1} - {num2} = {result}"); }
                    else if (LogMode == "json")
                    { Writer.WriteValue("Subtract"); }
                    break;
                case "m":
                    result = num1 * num2;
                    if (LogMode == "trace")
                    { Trace.WriteLine($"{num1} * {num2} = {result}"); }
                    else if (LogMode == "json")
                    { Writer.WriteValue("Multiply"); }
                    break;
                case "d":
                    // ask the user to enter a non-zero diviser
                    if (num2 != 0)
                    {
                        result = num1 / num2;
                        if (LogMode == "trace")
                        { Trace.WriteLine($"{num1} / {num2} = {result}"); }
                        else if (LogMode == "json")
                        { Writer.WriteValue("Divide"); }
                    }
                    break;

                default:
                    break;
            }
            if (LogMode == "json")
            {
                Writer.WritePropertyName("Result");
                try
                {
                    Writer.WriteValue(result);
                }
                catch (JsonWriterException)
                {
                    Writer.WriteValue("NaN");
                }
                Writer.WriteEndObject();
            }
            return result;
        }
    
        public void Finish()
        {
            if (LogMode == "json")
            {
                Writer.WriteEndArray();
                Writer.WriteEndObject();
                Writer.Close();
            }
        }
    }
}
