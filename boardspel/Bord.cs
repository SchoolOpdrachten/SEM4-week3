using System.Collections;

public class Bord
{
    public string[,] BordLijst { get; set; }

    public Bord()
    {
        BordLijst = initBord(7);
    }

    public string[,] initBord(int grootte)
    {
        var lijst = new string[grootte, grootte];
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

    public string PlaatsInBord(Coordinaat c, string character)
    {
        BordLijst[c.rij, c.column] = character;
        return "gezet";
    }

    public bool spelKlaar()
    {
        int aantalH = 0;
        int aantalB = 0;
        for (int row = 0; row < BordLijst.GetLength(0); row++)
            for (int column = 0; column < BordLijst.GetLength(1); column++)
            {
                if (BordLijst[row, column] == "H") aantalH++;
                if (BordLijst[row, column] == "B") aantalB++;
            }
        if (aantalH == 0 || aantalB == 0) return true;
        return false;
    }

    private string[] getRow(int rijnr)
    {
        int rijLengte = BordLijst.GetLength(0);
        string[] rij = new string[rijLengte];
        for (var i = 0; i < rijLengte; i++)
            rij[i] = BordLijst[rijnr, i];
        return rij;
    }

    private string[] getColumn(int columnnr)
    {
        int columnLengte = BordLijst.GetLength(0);
        string[] column = new string[columnLengte];
        for (var i = 0; i < columnLengte; i++)
            column[i] = BordLijst[columnnr, i];
        return column;
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
            if (i == 0) Console.Write("   ");
            Console.Write($" {i} |");
        }
        Console.WriteLine();
        for (int i = 0; i < BordLijst.GetLength(0); i++)
        {
            Console.Write($"{i} |");
            for (int j = 0; j < BordLijst.GetLength(1); j++)
            {
                if (BordLijst[i, j] == null)
                    Console.Write(" - |");
                else Console.Write(String.Format(" {0} |", BordLijst[i, j]));
            }
            Console.WriteLine();
        }
    }

    internal string VerplaatsZet(Coordinaat van, Coordinaat naar, string character)
    {
        BordLijst[van.rij, van.column] = null;
        return PlaatsInBord(naar, character);
    }
}
