using System;

class SumOfThreeNumbers
{
    static void Main()
    {
        double firstNumber = double.Parse(Console.ReadLine());
        double secondNumber = double.Parse(Console.ReadLine());
        double thirdNumber = double.Parse(Console.ReadLine());

        double sum = firstNumber + secondNumber + thirdNumber;

        Console.WriteLine(sum);
    }
}

