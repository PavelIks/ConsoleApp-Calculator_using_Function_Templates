using System;
using System.IO;

namespace CalculatorUsingFunctionTemplates
{
    static class MathOperations
    {
        public static double Sum(double a, double b)
        {
            return a + b;
        }
        public static double Minus(double a, double b)
        {
            return a - b;
        }
        public static double Multiply(double a, double b)
        {
            return a * b;
        }
        public static double Devide(double a, double b)
        {
            return a / b;
        }
    }

    class Program
    {
        static void Error()
        {
            Console.Clear();
            Console.WriteLine("Неверное значение.");
        }

        delegate T CurrentOperation<T>(T num1, T num2);
        static void Main()
        {
            double a, b;
            while (true)
            {
                try
                {
                    Console.Write("Введите первое число: ");
                    a = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Неверное значение.");
                    continue;
                }
                Console.Clear();
                while (true)
                {
                    try
                    {
                        Console.Write("Введите второе число: ");
                        b = Convert.ToDouble(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.Clear();
                        Console.WriteLine("Неверное значение.");
                        continue;
                    }
                }
                break;
            }

            ConsoleKeyInfo input = new ConsoleKeyInfo();
            CurrentOperation<double> currentOperation = new CurrentOperation<double>(MathOperations.Sum);
            while (input.Key != ConsoleKey.D0)
            {
                Console.Clear();
                Console.WriteLine(a + " " + b);
                Console.WriteLine("1 - сумма");
                Console.WriteLine("2 - разность");
                Console.WriteLine("3 - деление");
                Console.WriteLine("4 - умножение");
                Console.WriteLine("5 - посмотреть историю ответов");
                Console.WriteLine("0 - выход");
                input = Console.ReadKey(true);
                Console.Clear();
                switch (input.Key)
                {
                    case ConsoleKey.D1:
                        {
                            currentOperation = MathOperations.Sum;
                        }
                        break;
                    case ConsoleKey.D2:
                        {
                            currentOperation = MathOperations.Minus;
                        }
                        break;
                    case ConsoleKey.D3:
                        if (b == 0)
                        {
                            Console.WriteLine("Нельзя делить на ноль.");
                            Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
                            Console.ReadKey(true);
                            continue;
                        }
                        currentOperation = MathOperations.Devide;
                        break;
                    case ConsoleKey.D4:
                        {
                            currentOperation = MathOperations.Multiply;
                        }
                        break;
                    case ConsoleKey.D5:
                        if (!File.Exists("log.txt"))
                        {
                            Console.WriteLine("У Вас нет истории ответов.");
                            Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
                            Console.ReadKey(true);
                            continue;
                        }
                        string[] lines = File.ReadAllLines("log.txt");
                        for (int i = 0; i < lines.Length; i++)
                        {
                            Console.WriteLine(lines[i]);
                        }
                        Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
                        Console.ReadKey(true);
                        continue;
                    default: continue;
                }

                File.AppendAllText("log.txt", DateTime.Now.ToString() + " | Ответ: " + currentOperation?.Invoke(a, b) + Environment.NewLine);
                Console.WriteLine("Ответ: " + currentOperation(a, b));
                Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
                Console.ReadKey(true);
            }
            Console.WriteLine("Вы вышли");
            Console.Read();
        }
    }
}