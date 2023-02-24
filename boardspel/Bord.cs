using System.Collections;

public class Bord
{
    public string[,] BordLijst { get; set; }
    public Stapel<string[,]> stapel { get; set; }
    public const int GROOTTE = 7;

    public Bord()
    {
        stapel = new Stapel<string[,]>();
        BordLijst = initBord();
        stapel.duw(BordLijst);
    }

    public string[,] initBord()
    {
        var lijst = new string[GROOTTE, GROOTTE];
        lijst[6, 0] = "H";
        lijst[6, 1] = "H";
        lijst[5, 0] = "H";
        lijst[5, 1] = "H";
        lijst[0, 5] = "B";
        lijst[0, 6] = "B";
        lijst[1, 5] = "B";
        lijst[1, 6] = "B";
        return lijst;
    }

    public bool isValideSprong(Coordinaat van, Coordinaat naar, string character)
    {
        if (Math.Abs(van.rij - naar.rij) <= 2 && Math.Abs(van.column - naar.column) <= 2)
            if (BordLijst[naar.rij, naar.column] == null)
                return true;
        return false;
    }

    public bool isValidePlek(Coordinaat c, string character)
    {
        if (c.rij < 0 || c.column < 0 || c.rij > BordLijst.GetLength(0) - 1 || c.column > BordLijst.GetLength(1) - 1) return false;
        if (BordLijst[c.rij, c.column] != null) return false;

        var jouwStukken = JouwStukken(character);
        var validPlekken = new List<Coordinaat>();
        foreach (var jouwStuk in jouwStukken)
        {
            var stukken = AangrenzendeStukken(jouwStuk);
            foreach (var stuk in stukken)
                if (BordLijst[stuk.rij, stuk.column] == null) validPlekken.Add(stuk);
        }
        foreach (var plek in validPlekken)
            if (plek.Equals(c)) return true;
        return false;
    }

    public bool StapTerug()
    {
        var lijst = stapel.pak();
        if (lijst == null) return false;
        BordLijst = lijst;
        return true;
    }

    public bool PlaatsInBord(Coordinaat c, Speler speler)
    {
        var nieuwBord = BordLijst.Clone() as string[,];

        nieuwBord[c.rij, c.column] = speler.character;
        var bord = Infect(c, speler, nieuwBord);
        if (speler is not Robot) stapel.duw(BordLijst);
        BordLijst = bord;
        return true;
    }
    internal bool VerplaatsZet(Coordinaat van, Coordinaat naar, Speler speler)
    {
        var nieuwBord = BordLijst.Clone() as string[,];
        nieuwBord[van.rij, van.column] = null;

        nieuwBord[naar.rij, naar.column] = speler.character;
        var bord = Infect(naar, speler, nieuwBord);
        if (speler is not Robot) stapel.duw(BordLijst);
        BordLijst = bord;
        return true;
    }

    private string[,] Infect(Coordinaat naar, Speler speler, string[,]? nieuwBord)
    {
        var aangrenzende = AangrenzendeStukken(naar);
        foreach (var plek in aangrenzende)
        {
            if (nieuwBord[plek.rij, plek.column] == speler.character || nieuwBord[plek.rij, plek.column] == null) continue;
            nieuwBord[plek.rij, plek.column] = speler.character;
        }
        return nieuwBord;
    }

    public bool spelKlaar(string character)
    {
        bool spelKlaar = false;
        int mogelijkheid = 0;
        for (int row = 0; row < BordLijst.GetLength(0); row++)
        {
            for (int column = 0; column < BordLijst.GetLength(1); column++)
            {
                if (isValidePlek(new Coordinaat(row, column), character))
                {
                    mogelijkheid++;
                    break;
                }
            }
            if (mogelijkheid > 0) break;
        }

        int aantalH = 0;
        int aantalB = 0;
        int aantalLeeg = 0;
        for (int row = 0; row < BordLijst.GetLength(0); row++)
            for (int column = 0; column < BordLijst.GetLength(1); column++)
            {
                if (BordLijst[row, column] == "H") aantalH++;
                if (BordLijst[row, column] == "B") aantalB++;
                if (BordLijst[row, column] == null) aantalLeeg++;
            }
        if (aantalH == 0 || aantalB == 0 || aantalLeeg == 0 || mogelijkheid == 0) spelKlaar = true;
        return spelKlaar;
    }

    public List<Coordinaat> AangrenzendeStukken(Coordinaat c)
    {
        var stukken = new List<Coordinaat>();
        for (int rij = -1; rij < 2; rij++)
            for (int column = -1; column < 2; column++)
            {
                int rijnr = c.rij + rij;
                int columnnr = c.column + column;
                if (rijnr < 0 || rijnr > 6) continue;
                if (columnnr < 0 || columnnr > 6) continue;
                stukken.Add(new Coordinaat(rijnr, columnnr));
            }
        return stukken;
    }

    public List<Coordinaat> JouwStukken(string character)
    {
        var stukken = new List<Coordinaat>();
        for (int row = 0; row < BordLijst.GetLength(0); row++)
        {
            for (int column = 0; column < BordLijst.GetLength(1); column++)
            {
                if (BordLijst[row, column] != character) continue;
                stukken.Add(new Coordinaat(row, column));
            }
        }
        return stukken;
    }

    public void PrintBord()
    {
        for (int i = 0; i < BordLijst.GetLength(0); i++)
        {
            if (i == 0) Console.Write("  ");
            Console.Write($" {i} ");
        }
        Console.WriteLine();
        for (int i = 0; i < BordLijst.GetLength(0); i++)
        {
            Console.Write($"{i} ");
            for (int j = 0; j < BordLijst.GetLength(1); j++)
            {
                Console.BackgroundColor = ConsoleColor.White;
                if (BordLijst[i, j] == null)
                    Console.Write(" - ");
                else
                {
                    if (BordLijst[i, j] == "H") Console.BackgroundColor = ConsoleColor.Blue;
                    if (BordLijst[i, j] == "B") Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(String.Format(" {0} ", BordLijst[i, j]));
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();
        }
        Console.BackgroundColor = ConsoleColor.Black;
    }


    internal string getWinnaar()
    {
        int aantalH = 0;
        int aantalB = 0;
        for (int row = 0; row < BordLijst.GetLength(0); row++)
            for (int column = 0; column < BordLijst.GetLength(1); column++)
            {
                if (BordLijst[row, column] == "H") aantalH++;
                if (BordLijst[row, column] == "B") aantalB++;
            }
        if (aantalH == aantalB) return "gelijkspel";
        else if (aantalH < aantalB) return "B";
        else return "H";
    }
}
