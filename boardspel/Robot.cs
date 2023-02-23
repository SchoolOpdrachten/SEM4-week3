
public class Robot : Speler
{
    private bool Slim { get; set; }
    public Robot(string naam, string character) : base($"ROBOT {naam}", character)
    {
        if (naam.ToLower() == "slim" || naam.ToLower() == "s") Slim = true;
        else Slim = false;
    }
    
    internal void RobotMove(Bord bord)
    {
        var alleMogelijkKopieStukken = MogelijkeKopieStukken(bord);
        var alleMogelijkSprongStukken = MogelijkeSprongStukken(bord, alleMogelijkKopieStukken);
        if (Slim) SlimmeRobot(bord, alleMogelijkKopieStukken, alleMogelijkSprongStukken);
        else willekeurigeRobot(bord, alleMogelijkKopieStukken, alleMogelijkSprongStukken);
    }

    private void SlimmeRobot(Bord bord, List<Coordinaat> alleMogelijkKopieStukken, List<Tuple<Coordinaat, Coordinaat>> alleMogelijkSprongStukken)
    {
        
    }
    private void willekeurigeRobot(Bord bord, List<Coordinaat> alleMogelijkKopieStukken, List<Tuple<Coordinaat, Coordinaat>> alleMogelijkSprongStukken)
    {
        var random = new Random();
        var gelukt = false;
        while (!gelukt)
        {
            if (random.Next() % 2 == 0 || alleMogelijkSprongStukken.Count == 0)
            {
                int randomNummer = random.Next(0, alleMogelijkKopieStukken.Count - 1);
                gelukt = this.kopieerZet(bord, alleMogelijkKopieStukken[randomNummer]);
            }
            else
            {
                int randomNummer = random.Next(0, alleMogelijkSprongStukken.Count - 1);
                gelukt = this.zetStap(bord, alleMogelijkSprongStukken[randomNummer].Item1, alleMogelijkSprongStukken[randomNummer].Item2);
            }
        }
    }

    private List<Tuple<Coordinaat, Coordinaat>> MogelijkeSprongStukken(Bord bord, List<Coordinaat> alleMogelijkKopieStukken)
    {
        var alleMogelijkSprongStukken = new List<Tuple<Coordinaat, Coordinaat>>();
        var test = (new Coordinaat(1, 1), new Coordinaat(2, 2));
        foreach (var stuk in bord.JouwStukken(character))
            foreach (var kopieStuk in alleMogelijkKopieStukken)
                foreach (var sprongStuk in bord.AangrenzendeStukken(kopieStuk))
                    if (bord.isValideSprong(stuk, sprongStuk, character) && sprongStuk != kopieStuk && !bord.JouwStukken(character).Contains(sprongStuk) && !alleMogelijkSprongStukken.Contains(Tuple.Create(stuk, sprongStuk)) && !alleMogelijkKopieStukken.Contains(sprongStuk))
                        alleMogelijkSprongStukken.Add(Tuple.Create(stuk, sprongStuk));
        return alleMogelijkSprongStukken;
    }

    private List<Coordinaat> MogelijkeKopieStukken(Bord bord)
    {
        var mijnStukken = bord.JouwStukken(character);
        var alleMogelijkKopieStukken = new List<Coordinaat>();
        foreach (var stuk in mijnStukken)
        {
            foreach (var kopieStuk in bord.AangrenzendeStukken(stuk))
            {
                if (bord.isValidePlek(kopieStuk, character) && kopieStuk != stuk && !alleMogelijkKopieStukken.Contains(kopieStuk))
                {
                    alleMogelijkKopieStukken.Add(kopieStuk);
                }
            }
        }
        return alleMogelijkKopieStukken;
    }
}