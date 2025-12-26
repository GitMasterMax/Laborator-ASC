using System;

class Program
{
    static void Main()
    {
        string numarSursa, intregStr, fractieStr;
        string rezultatIntreg, rezultatFractie;
        int b1, b2;
        int punctIndex, i, valoareCifra, limit;
        long intregB10;
        double fractieB10;
        int digit;
        char c;

        Console.Write("Numarul (cu punct . pentru zecimale): ");
        numarSursa = Console.ReadLine().ToUpper();

        Console.Write("Baza sursa (b1): ");
        b1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Baza destinatie (b2): ");
        b2 = Convert.ToInt32(Console.ReadLine());

        punctIndex = numarSursa.IndexOf('.');
        
        if (punctIndex != -1)
        {
            intregStr = numarSursa.Substring(0, punctIndex);
            fractieStr = numarSursa.Substring(punctIndex + 1);
        }
        else
        {
            intregStr = numarSursa;
            fractieStr = "";
        }

        intregB10 = 0;
        long putere = 1;
        for (i = intregStr.Length - 1; i >= 0; i--)
        {
            if (intregStr[i] >= '0' && intregStr[i] <= '9')
            {
              valoareCifra = intregStr[i] - '0';
  
            }
            else
            {
              valoareCifra = intregStr[i] - 'A' + 10;  
            } 

            intregB10 += valoareCifra * putere;
            putere *= b1;
        }

        rezultatIntreg = "";
        if (intregB10 == 0)
        {
          rezultatIntreg = "0";  
        } 
        else
        {
            while (intregB10 > 0)
            {
                long rest = intregB10 % b2;
                if (rest < 10)
                {
                   c = (char)(rest + '0');  
                }
                else
                {
                    c = (char)(rest - 10 + 'A');
                } 
                
                rezultatIntreg = c + rezultatIntreg;
                intregB10 /= b2;
            }
        }
        
        if (fractieStr != "")
        {
            fractieB10 = 0.0;
            double putereFract = 1.0 / b1; 

            for (i = 0; i < fractieStr.Length; i++)
            {
                if (fractieStr[i] >= '0' && fractieStr[i] <= '9')
                {
                  valoareCifra = fractieStr[i] - '0';  
                }
                else
                {
                  valoareCifra = fractieStr[i] - 'A' + 10;  
                } 

                fractieB10 += valoareCifra * putereFract;
                putereFract /= b1;
            }

            rezultatFractie = "";
            limit = 0;

            while (fractieB10 > 0.000000001 && limit < 15)
            {
                fractieB10 *= b2;
                
                digit = (int)fractieB10; 

                if (digit < 10)
                {
                   c = (char)(digit + '0'); 
                }
                else
                {
                    c = (char)(digit - 10 + 'A');
                } 

                rezultatFractie += c;

                fractieB10 -= digit; 
                limit++;
            }
        }
        else
        {
            rezultatFractie = "0";
        }

        if (fractieStr != "")
        {
            Console.WriteLine($"\nRezultat: {rezultatIntreg}.{rezultatFractie}");
        }
        else
        {
            Console.WriteLine($"\nRezultat: {rezultatIntreg}");
        }

        Console.ReadKey();
    }
}