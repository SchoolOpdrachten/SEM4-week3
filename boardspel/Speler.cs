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

    public bool zetStap(Bord bord, Coordinaat van, Coordinaat naar)
    {
        if (bord.isValideSprong(van, naar, character) && isJouwVak(van, bord))
        {
            return bord.VerplaatsZet(van, naar, character);
        }
        return false;
    }

    private bool isJouwVak(Coordinaat van, Bord bord)
    {
        var jouwStukken = bord.JouwStukken(character);
        foreach (var vak in jouwStukken)
            if (vak.Equals(van)) return true;
        return false;
    }

    public bool kopieerZet(Bord bord, Coordinaat coordinaat)
    {
        if (bord.isValidePlek(coordinaat, character))
        {
            return bord.PlaatsInBord(coordinaat, character);
        }
        return false;
    }

    private static Coordinaat vraagCoordinaat()
    {
        Console.Write("Rij: ");
        int y = int.Parse(Console.ReadLine());
        Console.Write("Column: ");
        int x = int.Parse(Console.ReadLine());
        Coordinaat coordinaat = new Coordinaat(y, x);
        return coordinaat;
    }
}