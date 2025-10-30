using System;

class Program
{
    static void Main()
    {
        const string cifre = "0123456789ABCDEF";

        Console.Write("Baza sursă (b1): ");
        if (!int.TryParse(Console.ReadLine(), out int b1)) { Console.WriteLine("Valoare invalidă!"); return; }

        Console.Write("Baza destinație (b2): ");
        if (!int.TryParse(Console.ReadLine(), out int b2)) { Console.WriteLine("Valoare invalidă!"); return; }

        Console.Write("Număr în baza b1: ");
        string numar = (Console.ReadLine() ?? "0").ToUpper();

        string[] parti = numar.Split('.');
        string parteIntreaga = parti[0];
        string parteFractionara = parti.Length > 1 ? parti[1] : "";

        double valoare = 0;
        foreach (char c in parteIntreaga)
            valoare = valoare * b1 + cifre.IndexOf(c);

        double factor = 1.0 / b1;
        foreach (char c in parteFractionara)
        {
            valoare += cifre.IndexOf(c) * factor;
            factor /= b1;
        }

        long intPart = (long)valoare;
        double fracPart = valoare - intPart;
        string rezultat = "";

        if (intPart == 0) rezultat = "0";
        while (intPart > 0)
        {
            rezultat = cifre[(int)(intPart % b2)] + rezultat;
            intPart /= b2;
        }

        if (parteFractionara.Length > 0)
        {
            rezultat += ".";
            for (int i = 0; i < 8 && fracPart > 0; i++)
            {
                fracPart *= b2;
                int cifra = (int)fracPart;
                rezultat += cifre[cifra];
                fracPart -= cifra;
            }
        }

        Console.WriteLine($"Rezultat: {rezultat}");
        Console.ReadKey();
    }
}
