using System.Collections;

Console.Clear();
Bord bord = new Bord();
Console.WriteLine("Welkom bij het spel");
Console.WriteLine("Speler 1 naam: ");
var speler1naam = Console.ReadLine();
Speler speler1 = new Speler(speler1naam, "H");
Console.WriteLine("Speler 2 naam: ");
var speler2naam = Console.ReadLine();
Speler speler2 = new Speler(speler2naam, "B");
List<Speler> spelers = new List<Speler>() { speler1, speler2 };
Console.Clear();

bool spelKlaar = bord.spelKlaar();
while (!spelKlaar)
{

    foreach (var speler in spelers)
    {
        if (spelKlaar) break;
        bord.PrintBord();
        Console.WriteLine($"speler: {speler.Naam}");
        Console.WriteLine("[V]erplaats of [K]opieer je kledingstuk");
        string keuze = Console.ReadLine();
        bool zetGelukt = false;
        while (!zetGelukt)
        {
            if (keuze.ToLower() == "v" || keuze.ToLower() == "verplaats") zetGelukt = speler.zetStap(bord);
            if (keuze.ToLower() == "k" || keuze.ToLower() == "kopieer") zetGelukt = speler.kopieerZet(bord);
            Console.WriteLine(zetGelukt ? "gezet" : "zet is ongeldig");
        }
        spelKlaar = bord.spelKlaar();
        Console.Clear();
    }
}

Console.WriteLine("GG");