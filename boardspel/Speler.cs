using System.Collections;

public class Speler
{
    public string Naam { get; set; }
    public int Score { get; set; }
    public string character { get; set; }

    public Speler(string naam, string character)
    {
        Naam = naam;
        this.character = character;
    }

    public string maakMove(Bord bord)
    {
        Console.Write("Rij: ");
        int y = int.Parse(Console.ReadLine());
        Console.Write("Column: ");
        int x = int.Parse(Console.ReadLine());
        Coordinaat coordinaat = new Coordinaat(y, x);

        if (isValidePlek(coordinaat, bord))
        {
            return bord.PlaatsInBord(coordinaat, character);
        }
        return "geen valide plek om te plaatsen";
    }

    private bool isValidePlek(Coordinaat c, Bord bord)
    {
        var aangrenzendeStukken = bord.AangrenzendeStukken(character);
        foreach (var coordinaat in aangrenzendeStukken) {
            if (coordinaat.Equals(c)) return true;
        }
        return false;
    }
}