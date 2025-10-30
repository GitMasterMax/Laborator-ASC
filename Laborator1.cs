using System;

class Program
{
    static void Main()
    {
        Console.Write("Introdu numarul: ");
        string? numar = Console.ReadLine();

        Console.Write("Introdu baza sursa (2-16): ");
        string? inputSursa = Console.ReadLine();

        Console.Write("Introdu baza destinatie (2-16): ");
        string? inputDestinatie = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(numar) || 
            string.IsNullOrWhiteSpace(inputSursa) || 
            string.IsNullOrWhiteSpace(inputDestinatie))
        {
            Console.WriteLine("Eroare: valorile introduse nu pot fi goale!");
        }
        else
        {
            try
            {
                int bazaSursa = int.Parse(inputSursa);
                int bazaDestinatie = int.Parse(inputDestinatie);
                int numarDecimal = Convert.ToInt32(numar, bazaSursa);
                string rezultat = Convert.ToString(numarDecimal, bazaDestinatie).ToUpper();

                Console.WriteLine($"\nRezultatul in baza {bazaDestinatie} este: {rezultat}");
            }
            catch (Exception)
            {
                Console.WriteLine("Eroare: numărul sau bazele introduse nu sunt valide!");
            }
        }

        Console.WriteLine("\nApasă Enter pentru a închide...");
        Console.ReadLine();
    }
}
