﻿using System.Collections;

Console.Clear();
Bord bord = new Bord();
Console.WriteLine("Welkom bij het spel");
List<Speler> spelers = new List<Speler>();
foreach (var i in new string[] { "H", "B" })
{
    Console.WriteLine($"Speler {i} naam: ");
    var spelerNaam = Console.ReadLine();
    // var spelerNaam = "robot";
    if (spelerNaam.ToLower() == "robot")
        spelers.Add(new Robot(i));
    else spelers.Add(new Speler(spelerNaam, i));
}
Console.Clear();

bool spelKlaar = false;
int HWin = 0;
int BWin = 0;
int gelijkspel = 0;
for (int i = 0; i < 200; i++)
{
    spelKlaar = false;
    while (!spelKlaar)
    {
        foreach (var speler in spelers)
        {
            Console.Clear();
            spelKlaar = bord.spelKlaar(speler.character);
            if (spelKlaar) break;
            if (speler is Robot)
            {
                var robot = (Robot)speler;
                robot.RobotMove(bord);
                bord.PrintBord();
                // Thread.Sleep(10);
                continue;
            }
            bord.PrintBord();
            Console.WriteLine($"speler: {speler.Naam}");
            Console.WriteLine($"[V]erplaats of [K]opieer je kledingstuk {speler.character}");
            string keuze = Console.ReadLine();
            bool zetGelukt = false;
            while (!zetGelukt)
            {
                if (keuze.ToLower() == "v" || keuze.ToLower() == "verplaats")
                {
                    var van = Gebruiker.vraagCoordinaat();
                    var naar = Gebruiker.vraagCoordinaat();
                    zetGelukt = speler.zetStap(bord, van, naar);
                }
                if (keuze.ToLower() == "k" || keuze.ToLower() == "kopieer")
                {
                    var coordinaat = Gebruiker.vraagCoordinaat();
                    zetGelukt = speler.kopieerZet(bord, coordinaat);
                }
                Console.WriteLine(zetGelukt ? "gezet" : "zet is ongeldig");
            }
        }
    }
    bord.PrintBord();
    string winnaar = bord.getWinnaar();
    if (winnaar == "H") HWin++;
    if (winnaar == "B") BWin++;
    if (winnaar == "gelijkspel") gelijkspel++;
    Console.WriteLine($"GG {winnaar}");
    Console.ReadKey();
    bord.BordLijst = bord.initBord(7);
}
Console.WriteLine($"H: {HWin}, B: {BWin}, Gelijkspel: {gelijkspel}");