using System;

class Program
{
    static void Main()
    {
        string numarSursa;
        int b1, b2;
        long numarBaza10;
        string rezultat;
        int i, valoareCifra, rest;
        char caracterCifra;
        int putere;

        Console.Write("Introduceti numarul (in baza sursa) : ");
        numarSursa = Console.ReadLine().ToUpper();

        Console.Write("Baza sursa (b1) : ");
        b1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Baza destinatie (b2) : ");
        b2 = Convert.ToInt32(Console.ReadLine());

        numarBaza10 = 0;
        putere = 1;

        for (i = numarSursa.Length - 1; i >= 0; i--)
        {
            if (numarSursa[i] >= '0' && numarSursa[i] <= '9')
            {
                valoareCifra = numarSursa[i] - '0';
            }
            else
            {
                valoareCifra = numarSursa[i] - 'A' + 10;
            }

            if (valoareCifra >= b1)
            {
                Console.WriteLine($"Eroare: Cifra {numarSursa[i]} nu este valida in baza {b1}!");
                return; 
            }

            numarBaza10 = numarBaza10 + (valoareCifra * putere);
            putere = putere * b1;
        }

        Console.WriteLine($"\n[Info] Valoarea in baza 10 este: {numarBaza10}");

        rezultat = "";

        if (numarBaza10 == 0)
        {
            rezultat = "0";
        }
        else
        {
            while (numarBaza10 > 0)
            {
                rest = (int)(numarBaza10 % b2);
                
                if (rest < 10)
                {
                    caracterCifra = (char)(rest + '0');
                }
                else
                {
                    caracterCifra = (char)(rest - 10 + 'A');
                }

                rezultat = caracterCifra + rezultat;

                numarBaza10 = numarBaza10 / b2;
            }
        }

        Console.WriteLine($"\nRezultatul in baza {b2} este: {rezultat}");
        Console.ReadKey();
    }
}