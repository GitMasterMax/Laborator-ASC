using System;

class Program
{
    static void Main()
    {
        int stareCurenta;  
        char intrare; 
        bool ruleaza; 
        
        int dispense;      
        int returnNickel;  
        int returnDime;    


        stareCurenta = 0; 
        ruleaza = true;

        Console.WriteLine("--- AUTOMAT DE VANZARI (Pret: 20 centi) ---");
        Console.WriteLine("Monede acceptate: N (5c), D (10c), Q (25c)");
        Console.WriteLine("Apasati X pentru a iesi.\n");

        while (ruleaza)
        {
            dispense = 0;
            returnNickel = 0;
            returnDime = 0;

            Console.Write($"Stare curenta: {stareCurenta} centi. ");
            Console.Write("Introduceti moneda (N/D/Q): ");
            
            intrare = Char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();


            switch (stareCurenta)
            {
                case 0:
                    if (intrare == 'N')      { stareCurenta = 5; } 
                    else if (intrare == 'D') { stareCurenta = 10; }     
                    else if (intrare == 'Q') 
                    { 
                        stareCurenta = 0; 
                        dispense = 1; returnNickel = 1; returnDime = 0;
                    }
                    else if (intrare == 'X') { ruleaza = false; }
                    break;

                case 5:
                    if (intrare == 'N')      { stareCurenta = 10; }    
                    else if (intrare == 'D') { stareCurenta = 15; }          
                    else if (intrare == 'Q') 
                    { 
                        stareCurenta = 0;
                        dispense = 1; returnNickel = 0; returnDime = 1;
                    }
                    else if (intrare == 'X') { ruleaza = false; }
                    break;

                case 10:
                    if (intrare == 'N')      { stareCurenta = 15; } 
                    else if (intrare == 'D') 
                    { 
                        stareCurenta = 0;
                        dispense = 1; returnNickel = 0; returnDime = 0;
                    }
                    else if (intrare == 'Q') 
                    { 
                        stareCurenta = 0;
                        dispense = 1; returnNickel = 1; returnDime = 1;
                    }
                    else if (intrare == 'X') { ruleaza = false; }
                    break;


                case 15:
                    if (intrare == 'N') 
                    { 
                        stareCurenta = 0;
                        dispense = 1; returnNickel = 0; returnDime = 0;
                    }
                    else if (intrare == 'D') 
                    { 
                        stareCurenta = 0;
                        dispense = 1; returnNickel = 1; returnDime = 0;
                    }
                    else if (intrare == 'Q') 
                    {
                        Console.WriteLine(">> Ati introdus prea multi bani (40c). Returnez rest 20c.");
                        stareCurenta = 0;
                        dispense = 1; returnNickel = 0; returnDime = 1;
                    }
                    else if (intrare == 'X') { ruleaza = false; }
                    break;
            }

            if (ruleaza)
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine($"Output (Bits): {dispense}{returnNickel}{returnDime}");
                
                if (dispense == 1)
                {
                    Console.WriteLine(">> PRODUS ELIBERAT! [==============]");
                }
                if (returnNickel == 1 && returnDime == 1)
                {
                    Console.WriteLine(">> Rest: 15 centi (Nickel + Dime)");
                }                    
                else if (returnNickel == 1)
                {
                    Console.WriteLine(">> Rest: 5 centi (Nickel)");
                }                    
                else if (returnDime == 1)
                {
                    Console.WriteLine(">> Rest: 10 centi (Dime)");
                }                                    
                Console.WriteLine("----------------------------------\n");
            }
        }
    }
}