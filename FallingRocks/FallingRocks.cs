/* Implement the "Falling Rocks" game in the text console.
 * A small dwarf stays at the bottom of the screen and can move left
 * and right (by the arrows keys). A number of rocks of different sizes and forms constantly fall down
 * and you need to avoid a crash.
 * Rocks are the symbols ^, @, *, &, +, %, $, #, !, ., ;,
 * - distributed with appropriate density. The dwarf is (O). 
 * Ensure a constant game speed by Thread.Sleep(150).
 *Implement collision detection and scoring system.
 */
using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
public struct Object
{
    public int x;
    public int y;
    public int size;
    public string symbol;
    public ConsoleColor color;
}

class FallingRocks
{
    //an array for types of rocks
    static char[] rockElement = new char[] { '^', '@', '*', '&', '+', '-', '%', '$', '#', '!', '.', ';' };
    static int numberOfRocksOnLine = 0;
    static void PrintRockOnPosition(int x, int y, string symbol, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(symbol);
    }

    static void PrintGameString(int x, int y, string str, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    static void Main()
    {
        double speed = 50.0;
        double acceleration = 0.5;
        int playfieldWidth = 40;
        int livesCount = 5;
        int lines = 0;
        Console.BufferHeight = Console.WindowHeight = 20;
        Console.BufferWidth = Console.WindowWidth = 60;
        Object dwarf = new Object();
        dwarf.x = playfieldWidth / 2 - 1;
        dwarf.y = Console.WindowHeight - 1;
        dwarf.color = ConsoleColor.Cyan;
        dwarf.symbol = "(0)";
        Random randomGenerator = new Random();
        List<Object> objects = new List<Object>();
        while (true)
        {
            //speed increase with 0.5
            speed += acceleration;
            if (speed > 450)
            {
                speed = 450;
            }
            bool collision = false;
            {
                int chance = randomGenerator.Next(0, 100);
                //set variable for determine in 29% of cases there is no falling rocks, in other cases - 1,2 to max 3 rocks on a row
                if (chance > 70)
                {
                    numberOfRocksOnLine = 0;
                }
                else
                {
                    numberOfRocksOnLine = randomGenerator.Next(1, 4);
                }
                //generate rocks
                for (int i = 1; i <= numberOfRocksOnLine; i++)
                {
                    Object newRock = new Object();
                    newRock.color = (ConsoleColor)randomGenerator.Next((int)ConsoleColor.Green, (int)ConsoleColor.Yellow + 1);
                    newRock.y = 0;
                    char type = rockElement[randomGenerator.Next(0, rockElement.Length)];
                    //each rock can be with different size
                    int size = randomGenerator.Next(1, 3);
                    newRock.size = size;
                    newRock.x = randomGenerator.Next(0, playfieldWidth + 1) - size;
                    if (newRock.x < 0)
                    {
                        newRock.x = 0;
                    }
                    while (size > 0)
                    {
                        newRock.symbol += type;
                        size--;
                    }

                    objects.Add(newRock);
                }
            }
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (dwarf.x - 1 >= 0)
                    {
                        dwarf.x--;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if ((dwarf.x + 2) + 1 < playfieldWidth)
                    {
                        dwarf.x++;
                    }
                }
            }
            //changing positioning on each rock simulate falling down
            List<Object> newList = new List<Object>();
            for (int i = 0, prev = 0; i < objects.Count; i++)
            {
                Object oldRock = objects[i];
                Object newObject = new Object();
                newObject.x = oldRock.x;
                newObject.y = oldRock.y + 1;
                newObject.size = oldRock.size;
                newObject.symbol = oldRock.symbol;
                newObject.color = oldRock.color;
                if (newObject.symbol == "@" && (((newObject.x <= dwarf.x && (newObject.x + newObject.size) > dwarf.x) || (newObject.x <= (dwarf.x + 2) && (newObject.x + newObject.size) >= dwarf.x + 2)) && newObject.y == dwarf.y))
                {
                    //when the dwarf catch rock with type - a single @, its lives increased
                    livesCount++;
                    PrintRockOnPosition(oldRock.x, oldRock.y, "+1", ConsoleColor.Blue);
                    Thread.Sleep(800);
                }
                else if (((newObject.x <= dwarf.x && (newObject.x + newObject.size) > dwarf.x) || (newObject.x <= (dwarf.x + 2) && (newObject.x + newObject.size) >= dwarf.x + 2)) && newObject.y == dwarf.y)
                {
                    livesCount--;
                    collision = true;
                    if (livesCount == 0)
                    {
                        PrintGameString(playfieldWidth + 2, 4, "GAME OVER!", ConsoleColor.Red);
                        PrintGameString(playfieldWidth + 2, 8, "PRESS [ENTER]", ConsoleColor.Red);
                        PrintGameString(playfieldWidth + 2, 10, "TO EXIT!", ConsoleColor.Red);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                }
                else if (newObject.y == dwarf.y && prev != newObject.y) //when dwarf pass between rocks he taken line
                {
                    prev = newObject.y;
                    lines++;
                    if (lines % 100 == 0)
                    {
                        //each 100 lines the speed of falling rocks decrease with 50
                        speed -= 50;
                        if (speed > 450)
                        {
                            speed = 450;
                        }
                    }
                }
                if (newObject.y < Console.WindowHeight)
                {
                    newList.Add(newObject);
                }

            }
            objects = newList;
            Console.Clear();
            if (collision)   //draw position on each rock on the screen and draw the dwarf
                             //when dwarf meets a rock - detect collision - draw XXX
            {
                objects.Clear();
                PrintRockOnPosition(dwarf.x, dwarf.y, "XXX", ConsoleColor.Red);
            }
            else
            {
                PrintRockOnPosition(dwarf.x, dwarf.y, dwarf.symbol, dwarf.color);
            }
            foreach (Object rock in objects)
            {
                PrintRockOnPosition(rock.x, rock.y, rock.symbol, rock.color);
            }
            //write actual data for the game and result - count lives and passed lines
            PrintGameString(playfieldWidth + 2, 2, "FALLING ROCKS", ConsoleColor.Blue);
            PrintGameString(playfieldWidth + 2, 6, "lives: " + livesCount + " @", ConsoleColor.White);
            PrintGameString(playfieldWidth + 2, 7, "lines: " + lines, ConsoleColor.White);
            PrintGameString(playfieldWidth + 2, 10, "speed: " + (int)speed, ConsoleColor.White);
            //max speed of falling rocks that can be reached is 600-450 -> 150 ms
            Thread.Sleep((int)(600 - speed));
        }
    }
}