using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static int ParseRegister(string reg)
    {
        string numarStr;
        
        reg = reg.Replace(",", "").Trim();
        
        if (reg.StartsWith("%r"))
        {
            numarStr = reg.Substring(2);
            return Convert.ToInt32(numarStr);
        }
        else if (reg == "%r0") 
        {
            return 0; 
        }    
        return 0;
    }

    static void Main()
    {

        string[] linii;
        string linie, mnemonic;
        string[] parti;
        string caleFisier, fisierOutput;
        int pc; 
        int i;
        
        Dictionary<string, int> simboluri = new Dictionary<string, int>();
        
        uint instructiuneBinara;
        int op, rd, rs1, rs2, op3, op2, cond, imm22, simm13;
        bool eImediat;
        int adresaDestinatie, offset;

        Console.Write("Introduceti numele fisierului sursa (.asm): ");
        caleFisier = Console.ReadLine();
        fisierOutput = "output.hex"; 

        if (!File.Exists(caleFisier))
        {
            Console.WriteLine("Fisierul nu exista!");
            return;
        }

        linii = File.ReadAllLines(caleFisier);

        Console.WriteLine("\n--- PASUL 1: Mapare Etichete ---");
        pc = 0; 
        
        for (i = 0; i < linii.Length; i++)
        {
            linie = linii[i].Trim();
            
            if (linie.Length == 0 || linie.StartsWith("!"))
            {
               continue; 
            }
            
            if (linie.EndsWith(":"))
            {
                string numeEticheta = linie.Substring(0, linie.Length - 1);
                simboluri.Add(numeEticheta, pc);
                Console.WriteLine($"Gasit eticheta '{numeEticheta}' la adresa {pc}");
            }
            else if (!linie.StartsWith("."))
            {
                pc += 4;
            }
        }

        Console.WriteLine("\n--- PASUL 2: Generare Cod Masina ---");
        StreamWriter sw = new StreamWriter(fisierOutput);
        
        pc = 0;
        
        for (i = 0; i < linii.Length; i++)
        {
            linie = linii[i].Trim();
    
            if (linie.Length == 0 || linie.StartsWith("!") || linie.EndsWith(":") || linie.StartsWith("."))
            {
                continue;
            }

            parti = linie.Split(new char[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            mnemonic = parti[0].ToLower();

            instructiuneBinara = 0;
            
            if (mnemonic == "sethi")
            {
                op = 0; op2 = 4; 
                imm22 = Convert.ToInt32(parti[1]);
                rd = ParseRegister(parti[2]);

                instructiuneBinara = (uint)((op << 30) | (rd << 25) | (op2 << 22) | (imm22 & 0x3FFFFF));
            }
            else if (mnemonic.StartsWith("b"))
            {
                op = 0; op2 = 2; 
                
                cond = 0; 
                if (mnemonic == "be") {cond = 1;}
                else if (mnemonic == "bne") {cond = 9;} 
                else if (mnemonic == "bcs") {cond = 5;} 
                else if (mnemonic == "bneg") {cond = 6;}
                else if (mnemonic == "ba") {cond = 8;}  

                string etichetaTinta = parti[1];
                
                if (simboluri.ContainsKey(etichetaTinta))
                {
                    adresaDestinatie = simboluri[etichetaTinta];
                    offset = (adresaDestinatie - pc) >> 2; 
                }
                else
                {
                    offset = 0;
                    Console.WriteLine($"Eroare: Eticheta '{etichetaTinta}' nedefinita!");
                }

                instructiuneBinara = (uint)((op << 30) | (cond << 25) | (op2 << 22) | (offset & 0x3FFFFF));
            }
            else if (mnemonic == "call")
            {
                op = 1;
                string etichetaTinta = parti[1];
                if (simboluri.ContainsKey(etichetaTinta))
                {
                    adresaDestinatie = simboluri[etichetaTinta];
                    offset = (adresaDestinatie - pc) >> 2;
                }
                else 
                {
                    offset = 0;
                }

                instructiuneBinara = (uint)((op << 30) | (offset & 0x3FFFFFFF));
            }
            else
            {
                
                op = 2; 
                op3 = 0;
                rd = 0; rs1 = 0; rs2 = 0; simm13 = 0; eImediat = false;

                switch (mnemonic)
                {
                    case "addcc": op = 2; op3 = 0x10; break; 
                    case "andcc": op = 2; op3 = 0x11; break; 
                    case "orcc":  op = 2; op3 = 0x12; break; 
                    case "orncc": op = 2; op3 = 0x16; break; 
                    case "sll":   op = 2; op3 = 0x25; break; 
                    case "srl":   op = 2; op3 = 0x26; break; 
                    case "ld":    op = 3; op3 = 0x00; break; 
                    case "st":    op = 3; op3 = 0x04; break;
                    case "jmpl":  op = 2; op3 = 0x38; break; 
                    default: 
                    {
                        Console.WriteLine($"Instructiune necunoscuta: {mnemonic}"); 
                        break;
                    }    
                }

                if (mnemonic == "ld" || mnemonic == "st")
                {
                    rs1 = ParseRegister(parti[1]);
                    
                    if (parti[2].StartsWith("%")) 
                    {
                        rs2 = ParseRegister(parti[2]);
                        eImediat = false;
                    }
                    else
                    {
                        simm13 = Convert.ToInt32(parti[2]);
                        eImediat = true;
                    }

                    rd = ParseRegister(parti[3]);
                }
                else
                {
                    rd = ParseRegister(parti[3]);
                    
                    rs1 = ParseRegister(parti[1]);

                    if (parti[2].StartsWith("%"))
                    {
                        rs2 = ParseRegister(parti[2]);
                        eImediat = false;
                    }
                    else
                    {
                        simm13 = Convert.ToInt32(parti[2]);
                        eImediat = true;
                    }
                }

                
                instructiuneBinara = (uint)(op << 30) | (uint)(rd << 25) | (uint)(op3 << 19) | (uint)(rs1 << 14);

                if (eImediat)
                {
                    instructiuneBinara |= (1u << 13);
                    instructiuneBinara |= (uint)(simm13 & 0x1FFF);
                }
                else
                {
                    instructiuneBinara |= (uint)(rs2 & 0x1F);
                }
            }
            string hexOutput = instructiuneBinara.ToString("X8");
            Console.WriteLine($"PC: {pc:D4} | {linie,-20} -> 0x{hexOutput}");
            sw.WriteLine(hexOutput);

            pc += 4;
        }

        sw.Close();
        Console.WriteLine($"\nAsamblare completa! Rezultatul a fost scris in '{fisierOutput}'");
        Console.ReadKey();
    }
}