using System;


class SumOfFiveNumbers
{
    static void Main()
    {
        string numberLine = Console.ReadLine();
        string[] strNumber = numberLine.Split(' ');
        decimal result = 0;

        for (int i = 0; i < strNumber.Length; i++)
        {
            result += Convert.ToDecimal(strNumber[i]);
        }

        Console.WriteLine("{0} ", result);
    }
}




