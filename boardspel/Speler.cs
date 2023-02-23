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

    public bool zetStap(Bord bord)
    {
        Console.WriteLine("Van: ");
        Coordinaat van = vraagCoordinaat();
        Console.WriteLine("Naar: ");
        Coordinaat naar = vraagCoordinaat();
        if (isValideSprong(van, naar, bord) && isJouwVak(van, bord))
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

    public bool kopieerZet(Bord bord)
    {
        Coordinaat coordinaat = vraagCoordinaat();

        if (isValidePlek(coordinaat, bord))
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

    private bool isValideSprong(Coordinaat van, Coordinaat naar, Bord bord) {
        if (Math.Abs(van.rij - naar.rij) <= 2 && Math.Abs(van.column - naar.column) <= 2) 
            if(!isValidePlek(naar, bord))
                return true;
        
        return false;
    }

    private bool isValidePlek(Coordinaat c, Bord bord)
    {
        if (bord.BordLijst[c.rij, c.column] != null) return false;

        var jouwStukken = bord.JouwStukken(character);
        var validPlekken = new List<Coordinaat>();
        foreach (var jouwStuk in jouwStukken)
        {
            var stukken = bord.AangrenzendeStukken(jouwStuk);
            foreach (var stuk in stukken) 
                if(bord.BordLijst[stuk.rij, stuk.column] == null) validPlekken.Add(stuk);
        }
        foreach (var plek in validPlekken)
            if (plek.Equals(c)) return true;
        return false;
    }
}