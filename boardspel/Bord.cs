public class Bord
{
    public string[,] BordLijst { get; set; }

    public Bord(int grootte)
    {
        initBord(grootte);
    }

    public void initBord(int grootte)
    {
        BordLijst = new string[grootte, grootte];
        BordLijst[6, 0] = "H";
        BordLijst[6, 1] = "H";
        BordLijst[5, 0] = "H";
        BordLijst[5, 1] = "H";
        BordLijst[0, 5] = "B";
        BordLijst[0, 6] = "B";
        BordLijst[1, 5] = "B";
        BordLijst[1, 6] = "B";

    }

    public void PlaatsInBord(int x, int y, string character)
    {
        BordLijst[y, x] = character;
        PrintBord();
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
}
