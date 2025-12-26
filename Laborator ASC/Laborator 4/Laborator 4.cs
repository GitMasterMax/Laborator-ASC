using System;
using System.Threading;

class Program
{
    static void Main()
    {
        DateTime acum;
        int h, m, s;
        int h1, h2;
        int m1, m2;
        int s1, s2;
        
        Console.CursorVisible = false;

        Console.WriteLine("Ceas BCD (Binary Coded Decimal)");
        Console.WriteLine("Apasati orice tasta pentru a opri...");
        Console.WriteLine();

        while (!Console.KeyAvailable)
        {
            acum = DateTime.Now;
            h = acum.Hour;
            m = acum.Minute;
            s = acum.Second;

            h1 = h / 10; h2 = h % 10;
            m1 = m / 10; m2 = m % 10;
            s1 = s / 10; s2 = s % 10;

            Console.SetCursorPosition(0, 4);

            Console.WriteLine(" H   h     M   m     S   s  ");
            Console.WriteLine("----------------------------");

            Console.Write((h1 & 8) != 0 ? " ●   " : " ○   ");
            Console.Write((h2 & 8) != 0 ? "●     " : "○     ");
            Console.Write((m1 & 8) != 0 ? "●   " : "○   ");
            Console.Write((m2 & 8) != 0 ? "●     " : "○     ");
            Console.Write((s1 & 8) != 0 ? "●   " : "○   ");
            Console.WriteLine((s2 & 8) != 0 ? "●   " : "○   ");

            Console.Write((h1 & 4) != 0 ? " ●   " : " ○   ");
            Console.Write((h2 & 4) != 0 ? "●     " : "○     ");
            Console.Write((m1 & 4) != 0 ? "●   " : "○   ");
            Console.Write((m2 & 4) != 0 ? "●     " : "○     ");
            Console.Write((s1 & 4) != 0 ? "●   " : "○   ");
            Console.WriteLine((s2 & 4) != 0 ? "●   " : "○   ");

            Console.Write((h1 & 2) != 0 ? " ●   " : " ○   ");
            Console.Write((h2 & 2) != 0 ? "●     " : "○     ");
            Console.Write((m1 & 2) != 0 ? "●   " : "○   ");
            Console.Write((m2 & 2) != 0 ? "●     " : "○     ");
            Console.Write((s1 & 2) != 0 ? "●   " : "○   ");
            Console.WriteLine((s2 & 2) != 0 ? "●   " : "○   ");

            Console.Write((h1 & 1) != 0 ? " ●   " : " ○   ");
            Console.Write((h2 & 1) != 0 ? "●     " : "○     ");
            Console.Write((m1 & 1) != 0 ? "●   " : "○   ");
            Console.Write((m2 & 1) != 0 ? "●     " : "○     ");
            Console.Write((s1 & 1) != 0 ? "●   " : "○   ");
            Console.WriteLine((s2 & 1) != 0 ? "●   " : "○   ");

            Console.WriteLine("----------------------------");
            Console.WriteLine($" Ora clasica: {h:00}:{m:00}:{s:00}   ");

            Thread.Sleep(1000);
        }
    }
}