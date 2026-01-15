using System;
using System.IO; 
using System.Text; 

class Program
{
    static void Main()
    {
        string caleFisier;
        int bytesPerLine;
        FileStream fs;
        byte[] buffer;
        int bytesRead, i;
        long offset;
        char c;
        
        bytesPerLine = 16; 

        Console.Write("Introduceti calea catre fisier (ex: text.txt): ");
        caleFisier = Console.ReadLine();

        if (!File.Exists(caleFisier))
        {
            Console.WriteLine("Eroare: Fisierul nu a fost gasit!");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\n--- HEX VIEWER: " + caleFisier + " ---\n");
        Console.WriteLine("Offset    00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F  Decoded Text");
        Console.WriteLine("-----------------------------------------------------------------------");

        try
        {
            using (fs = new FileStream(caleFisier, FileMode.Open, FileAccess.Read))
            {
                buffer = new byte[bytesPerLine];
                offset = 0;

                while ((bytesRead = fs.Read(buffer, 0, bytesPerLine)) > 0)
                {
                    Console.Write(offset.ToString("X8") + ": ");

                    for (i = 0; i < bytesPerLine; i++)
                    {
                        if (i < bytesRead)
                        {
                            Console.Write(buffer[i].ToString("X2") + " ");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }

                    Console.Write(" | ");

                    for (i = 0; i < bytesRead; i++)
                    {
                        c = (char)buffer[i];

                        if (!char.IsControl(c) && buffer[i] < 127) 
                        {
                            Console.Write(c);
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }

                    Console.WriteLine();
                    offset += bytesRead;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("A aparut o eroare la citirea fisierului: " + e.Message);
        }

        Console.WriteLine("\nApasa orice tasta pentru a iesi...");
        Console.ReadKey();
    }
}