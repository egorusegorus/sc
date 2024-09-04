using System;

class SubnetCalculator
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Herzlich willkommen in SubnetCalculator\n");
        
        int[] ip = IpEingabe();
        int cidrVar = Cidr();
        int[] subnetzMaske = SubMaske(cidrVar);
        int[] netzwerkAdresse = new int[4];
        int[] broadCastAdresse = new int[4];
        
        BerechneNetzwerkUndBroadcastAdresse(netzwerkAdresse, ip, subnetzMaske, ref broadCastAdresse);
        
        Console.WriteLine($"Sie haben:            {string.Join('.', ip)}/{cidrVar} eingegeben");
        Console.WriteLine($"Subnetzwerkmaske:     {string.Join('.', subnetzMaske)} = {cidrVar} CIDR Index");
        Console.WriteLine($"Netzwerkadresse:      {string.Join('.', netzwerkAdresse)}");
        Console.WriteLine($"Erste freie Adresse:  {string.Join('.', netzwerkAdresse[0], netzwerkAdresse[1], netzwerkAdresse[2], netzwerkAdresse[3] + 1)}");
        Console.WriteLine($"Letzte freie Adresse: {string.Join('.', broadCastAdresse[0], broadCastAdresse[1], broadCastAdresse[2], broadCastAdresse[3] - 1)}");
        Console.WriteLine($"Broadcastadresse:     {string.Join('.', broadCastAdresse)}");
    }

    // Methode zur Berechnung der Subnetzmaske basierend auf dem CIDR-Wert
    public static int[] SubMaske(int cidrVar)
    {
        int[] subnetzMaske = new int[4];
        int volleOktetten = cidrVar / 8; // Anzahl der vollständigen Oktetten (255)
        int verbleibendeBits = cidrVar % 8; // Verbleibende Bits im nächsten Oktett

        for (int i = 0; i < volleOktetten; i++)
        {
            subnetzMaske[i] = 255;
        }

        if (volleOktetten < 4)
        {
            subnetzMaske[volleOktetten] = (byte)(255 << (8 - verbleibendeBits) & 255);
        }

        return subnetzMaske;
    }

    // Methode zur Berechnung der Netzwerk- und Broadcast-Adresse
    public static void BerechneNetzwerkUndBroadcastAdresse(int[] netzwerkAdresse, int[] ip, int[] subnetzMaske, ref int[] broadCastAdresse)
    {
        for (int i = 0; i < 4; i++)
        {
            netzwerkAdresse[i] = ip[i] & subnetzMaske[i];
            broadCastAdresse[i] = netzwerkAdresse[i] | (255 - subnetzMaske[i]);
        }
    }

    // Methode zur Eingabe und Validierung des CIDR-Werts
    public static int Cidr()
    {
        int cidr = -1;
        bool wertIstInOrdnung = false;
        
        while (!wertIstInOrdnung)
        {
            Console.WriteLine("Bitte geben Sie den CIDR Index ein (0-32):");
            string? eingabe = Console.ReadLine();

            if (int.TryParse(eingabe, out int test) && test >= 0 && test <= 32)
            {
                cidr = test;
                wertIstInOrdnung = true;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl im Bereich von 0 bis 32 ein.");
            }
        }

        return cidr;
    }

    // Methode zur Eingabe und Validierung der IP-Adresse
    public static int[] IpEingabe()
    {
        int[] ip = new int[4];

        for (int i = 0; i < 4; i++)
        {
            bool wertIstInOrdnung = false;
            while (!wertIstInOrdnung)
            {
                Console.WriteLine($"Bitte geben Sie den {i + 1}. Teil der IP-Adresse ein (0-255):");
                string? eingabe = Console.ReadLine();

                if (int.TryParse(eingabe, out int teilIp) && teilIp >= 0 && teilIp <= 255)
                {
                    ip[i] = teilIp;
                    wertIstInOrdnung = true;
                }
                else
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie eine Zahl im Bereich von 0 bis 255 ein.");
                }
            }
        }
        return ip;
    }
}
