using System.Collections;

Bord bord = new Bord();
Console.WriteLine("Welkom bij het spel");
var speler1naam = Console.ReadLine();
Speler speler1 = new Speler(speler1naam, "H");
var speler2naam = Console.ReadLine();
Speler speler2 = new Speler(speler2naam, "B");
List<Speler> spelers = new List<Speler>() { speler1, speler2 };

bool spelKlaar = bord.spelKlaar();
while (!spelKlaar)
{

    foreach (var speler in spelers)
    {
        if (spelKlaar) break;
        bord.PrintBord();
        Console.WriteLine($"speler: {speler.character}");
        Console.WriteLine("[V]erplaats of [K]opieer je kledingstuk");
        string keuze = Console.ReadLine();
        if (keuze.ToLower() == "v" || keuze.ToLower() == "verplaats") Console.WriteLine(speler.zetStap(bord));
        if (keuze.ToLower() == "k" || keuze.ToLower() == "kopieer") Console.WriteLine(speler.kopieerZet(bord));
        spelKlaar = bord.spelKlaar();
    }
}

Console.WriteLine("GG");