namespace Circles;

class Program
{
    static void Main(string[] args)
    {
        bool eternityRepeat = true;
        while (eternityRepeat)
        {
            bool repeat = true;
            int length = 0;
            int adding = 0;
            int specialX = -1;
            while (repeat)
            {
                Console.WriteLine("Length?");
                int flength = Convert.ToInt32(Console.ReadLine());
                length = flength * 2 + 1;
                if (length >= 3)
                {
                    repeat = false;
                }
            }
            string[,] array = new string[length, length];
            double center = Math.Ceiling(length / 2d); //3x3=2, 5x5=3, 7x7=4
            center -= 1d;
            if (length % 2 == 0)
            {
                center += 0.5d;
            }
            for (int x = 0; x < length ; x++)
            {
                bool last = false;
                for (int y = 0; y < length; y++)
                {
                    var calc = Math.Sqrt(Math.Pow(Math.Abs(x - center), 2) + Math.Pow(Math.Abs(y - center), 2));
                    if (Math.Abs(calc - 1) > 0.1 && length - adding == 3)
                    {
                        calc = 1.5;
                    }

                    if (calc < (length - adding) / 2d)
                    {
                        if (last)
                        {
                            array[x, y] = "[X]";
                        }
                        else if (y == 0)
                        {
                            array[x, y] = "[X]";
                            last = true;
                        }
                        else //the left side
                        {
                            last = true;
                            var symbol = x < center ? @"[/]" : @"[\]";
                            array[x, y] = symbol;
                            CheckForAbove(x, y, ref array, (int)center);
                        }
                        //triggers only once for the first row of the part of the circle that is touching the left wall
                        //try changing it to a [T] to see where if you don't understand
                        if (y == 0 && last && specialX == -1)
                        {
                            array[x, y] = @"[/]";
                            specialX = x;
                        }
                        continue;
                    }

                    if (last) //the right side
                    { 
                        var symbol = x < center ? @"[\]" : @"[/]";
                        array[x, y-1] = symbol;
                        last = false;
                        CheckForAbove(x, y-1, ref array, (int)center);
                    }
                    array[x, y] = "[ ]";
                }
            }
            //half point of the circle (the center)
            int half = (int)center;
            //this repeats twice, once when the y (the column) is 0, the x (the row) is at the bottom of part of the circle that is touching the (in this case, left)
            //wall and then when the y (the column) is last one, the x (the row) is at the bottom of part of the circle that is touching the (in this case, right) wall
            //try changing it to a [T] to see where if you don't understand
            for (int i = 0; i <= length; i+=length-1)
            {
                var symbol = i == 0 ? @"[\]" : @"[/]";
                array[half + half - specialX, i] = symbol;
                //array[half, i] = "[?]";
            }
            //this overrides the top part of the rightmost part of the circle, try changing it to a [T] to see where if you don't understand
            array[specialX, length - 1] = @"[\]";
            //pretty much an unnecessary leftover, it checks if the length of the whole circle is divisible by two and if yes, it adds an O into the center
            if (length % 2 != 0)
            {
                array[(int)center, (int)center] = "[O]";
            }
            //this just writes out the circles with each "box" in a different color
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    switch (array[i, j])
                    {
                        case "[X]":
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case "[ ]":
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                        case "[O]":
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case "[V]":
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case "[?]":
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case "[T]":
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case "[F]":
                            Console.ForegroundColor = ConsoleColor.White;
                            array[i, j] = "[X]";
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                    }
                    if (length == 3)
                    {
                        if (array[i, j] != "[ ]" && array[i, j] != "[O]")
                        {
                            array[i, j] = "[X]";
                        }
                    }
                    Console.Write(array[i, j]);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine("enter to again");
        string response = Console.ReadLine();
        if (response != "\r\n")
        {
            eternityRepeat = false;
        }
    }
    static void CheckForAbove(int x, int y, ref string[,] array, int center)
    {
        if (x == 0)
        {
            return;
        }
        if (x > center)
        {
            if (array[x - 1, y] != "[X]" && array[x - 1, y] != "[ ]")
            {
                array[x - 1, y] = "[F]";
            }
            if (array[x - 1, y] != "[F]") return;
            array[x - 1, y] = "[F]";
        }
        else
        {
            if (array[x - 1, y] != "[X]" && array[x - 1, y] != "[ ]")
            {
                array[x, y] = "[F]";
            }
            if (array[x - 1, y] != "[F]") return;
            array[x, y] = "[F]";
        }

    }
}