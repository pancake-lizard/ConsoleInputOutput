using System;

class FormattingNumbers
{
    static void Main()
    {
        int a;
        while (!int.TryParse(Console.ReadLine(), out a) || a < 0 || a > 500) Console.WriteLine("Invalid input!!!");

        double b;
        while (!double.TryParse(Console.ReadLine(), out b)) Console.WriteLine("Invalid input!!!");

        double c;
        while (!double.TryParse(Console.ReadLine(), out c)) Console.WriteLine("Invalid input!!!");

        Console.Write("|{0, -10:X}|{1}|{2:F3}|", a, Convert.ToString(a, 2).PadLeft(10, '0'), b.ToString().PadLeft(10, ' '));



        bool check = Convert.ToString(c).IndexOf(".") > 0;
        Console.WriteLine(check ? "{0:0.000}         |" : "{0}", c);

        Console.WriteLine();
    }
}
