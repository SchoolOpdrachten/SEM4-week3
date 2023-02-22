using System.Collections;

Bord bord = new Bord(7);
Console.WriteLine("Welkom bij het spel");
var speler1naam = Console.ReadLine();
Speler speler1 = new Speler(speler1naam, "H");
var speler2naam = Console.ReadLine();
Speler speler2 = new Speler(speler2naam, "B");
List<Speler> spelers = new List<Speler>() { speler1, speler2 };

while (true)
{

    foreach (var speler in spelers)
    {
        bord.PrintBord();
        Console.WriteLine(speler.character);
        var uitkomst = speler1.maakMove(bord);
        Console.WriteLine(uitkomst);
    }
}