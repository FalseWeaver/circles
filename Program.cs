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
            string answer = "";
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

                Console.WriteLine("spceial");
                answer = Console.ReadLine();
            }
            if (answer.ToLower() != "s")
            {
                adding = 2;
            }
            length += adding;
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
                            array[x, y] = "[V]";
                            CheckForAbove(x, y, ref array);
                        }
                        //triggers only once for the first row of the part of the circle that is touching the left wall
                        //try changing it to a [T] to see where if you don't understand
                        if (y == 0 && last && specialX == -1)
                        {
                            array[x, y] = "[V]";
                            specialX = x;
                        }
                        continue;
                    }

                    if (last) //the right side
                    { 
                        array[x, y-1] = "[V]";
                        last = false;
                        CheckForAbove(x, y-1, ref array);
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
                array[half + half - specialX, i] = "[V]";
                array[half, i] = "[?]";
            }
            //this overrides the top part of the rightmost part of the circle, try changing it to a [T] to see where if you don't understand
            array[specialX, length - 1] = "[V]";
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
                        default:
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            //Console.ForegroundColor = ConsoleColor.Blue;
                            //array[i, j] = "[V]";
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
    static void CheckForAbove(int x, int y, ref string[,] array)
    {
        if (x == 0)
        {
            return;
        }
        if (array[x - 1, y] == "[V]" || array[x - 1, y] == "[F]")
        {
            array[x - 1, y] = "[F]";
            array[x, y] = "[F]";
        }
    }
}