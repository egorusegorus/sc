using System;

class HelloWorld 
{
    static void Main(string[] args) 
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Herzlich willkommen in SubnetCalculator");
        Console.WriteLine("");
        
        int[] Ip = IpEingabe();
        int CidrVar = Cidr();
        int[] SubnetzMaske = SubMaske(CidrVar);
        int[] NetzerkAdresse = new int[4];
        int[] BroadCastAdresse = new int[4];
        
        NetzwekAdresseMethode(NetzerkAdresse, Ip, SubnetzMaske, ref BroadCastAdresse);
        
        Console.WriteLine("Sie haben:            " + string.Join(".", Ip) + "/" + CidrVar + " eingegeben");
        Console.WriteLine("Subnetzwerkmaske:     " + string.Join(".", SubnetzMaske) + " = " + CidrVar + " CIDR Index");
        Console.WriteLine("Netzwerkadresse:      " + string.Join(".", NetzerkAdresse));
        Console.WriteLine("Erste freie Adresse:  " + NetzerkAdresse[0]+"."+ NetzerkAdresse[1]+"."+ NetzerkAdresse[2]+"."+ (NetzerkAdresse[3]+1));
        Console.WriteLine("Letzte freie Adresse: " + BroadCastAdresse[0]+"."+ BroadCastAdresse[1]+"."+ BroadCastAdresse[2]+"."+ (BroadCastAdresse[3]-1));
        Console.WriteLine("Broadcastadresse:     " + string.Join(".", BroadCastAdresse));
    }


public static int[] SubMaske(int CidrVar)
{
    int[] SubnetzMaske = new int[4];
    
    int fullOctets = CidrVar / 8; // Liczba pełnych oktetów (o wartości 255)
    int remainingBits = CidrVar % 8; // Pozostałe bity w oktetach

    for (int i = 0; i < fullOctets; i++)
    {
        SubnetzMaske[i] = 255;
    }

    if (fullOctets < 4)
    {
        SubnetzMaske[fullOctets] = (byte)(255 << (8 - remainingBits) & 255);
    }

    return SubnetzMaske;
}
    public static int[] NetzwekAdresseMethode(int[] NetzwerkAdresse, int[] Ip, int[] SubnetzMaske, ref int[] BroadCastAdresse)
    {
        for (int i = 0; i < 4; i++)
        {
            NetzwerkAdresse[i] = Ip[i] & SubnetzMaske[i];
            BroadCastAdresse[i] = NetzwerkAdresse[i] | (255 - SubnetzMaske[i]);
        }

        return NetzwerkAdresse;
    }

    public static int Cidr()
    {
        int Cidr = -1;
        bool WertIstInOrdnung = false;
        
        while (!WertIstInOrdnung)
        {
            Console.WriteLine("Bitte geben Sie den CIDR Index ein (0-32):");
            string? ip = Console.ReadLine();

            if (ip != null)
            {
                int test;
                try
                {
                    test = Convert.ToInt32(ip);
                    if (test >= 0 && test <= 32)
                    {
                        Cidr = test;
                        WertIstInOrdnung = true;
                    }
                    else
                    {
                        Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl im Bereich von 0 bis 32 ein.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine gültige Zahl ein.");
                }
            }
            else
            {
                Console.WriteLine("Eingabe ist ungültig. Bitte geben Sie einen Wert ein.");
            }
        }

        return Cidr;
    }

    public static int[] IpEingabe()
    {
        int[] IP = new int[4];

        for (int i = 0; i < 4; i++)
        {
            int a = i + 1;
            bool WertIstInOrdnung = false;
            while (!WertIstInOrdnung)
            {
                Console.WriteLine("Bitte geben Sie den " + a + "-ten Teil der IP-Adresse ein (0-255):");
                string? ip = Console.ReadLine();

                if (ip != null)
                {
                    int test;
                    try
                    {
                        test = Convert.ToInt32(ip);
                        if (test >= 0 && test <= 255)
                        {
                            IP[i] = test;
                            WertIstInOrdnung = true;
                        }
                        else
                        {
                            Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl im Bereich von 0 bis 255 ein.");
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine gültige Zahl ein.");
                    }
                }
                else
                {
                    Console.WriteLine("Eingabe ist ungültig. Bitte geben Sie einen Wert ein.");
                }
            }
        }
        return IP;
    }
}
