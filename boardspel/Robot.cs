
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
        var alleMogelijkSprongStukken = MogelijkeSprongStukken(bord);
        if (Slim) SlimmeRobot(bord, alleMogelijkKopieStukken, alleMogelijkSprongStukken);
        else willekeurigeRobot(bord, alleMogelijkKopieStukken, alleMogelijkSprongStukken);
    }

    private void SlimmeRobot(Bord bord, List<Coordinaat> alleMogelijkKopieStukken, List<Tuple<Coordinaat, Coordinaat>> alleMogelijkSprongStukken)
    {
        var aantalKopieInfects = MeesteInfects(bord, alleMogelijkKopieStukken);
        var sprongStukken = new List<Coordinaat>();
        foreach(var sprongStuk in alleMogelijkSprongStukken) {
            sprongStukken.Add(sprongStuk.Item2);
        }
        var aantalSprongInfects = MeesteInfects(bord, sprongStukken);
        var sprong = alleMogelijkSprongStukken.Select(s => s).Where(s => s.Item2 == aantalSprongInfects.Item1).FirstOrDefault();

        if (aantalKopieInfects.Item2 >= aantalSprongInfects.Item2) this.kopieerZet(bord, aantalKopieInfects.Item1);
        else if (aantalKopieInfects.Item2 < aantalSprongInfects.Item2) this.zetStap(bord, sprong.Item1, sprong.Item2);
    }

    private Tuple<Coordinaat, int> MeesteInfects(Bord bord, List<Coordinaat> alleMogelijkheden)
    {
        Coordinaat besteZet = null;
        int maxInfects = 0;
        foreach (var zet in alleMogelijkheden)
        {
            var aangrenzendeStukken = bord.AangrenzendeStukken(zet);
            int aantalInfect = 0;
            foreach (var aangrenzende in aangrenzendeStukken)
            {
                if (bord.BordLijst[aangrenzende.rij, aangrenzende.column] != this.character && bord.BordLijst[aangrenzende.rij, aangrenzende.column] != null)
                    aantalInfect++;
            }
            if (aantalInfect > maxInfects)
            {
                besteZet = zet;
                maxInfects = aantalInfect;
            }
        }
        if (besteZet == null) {
            int nummer = new Random().Next(0, alleMogelijkheden.Count - 1);
            besteZet = alleMogelijkheden[nummer];
        }
        return Tuple.Create(besteZet, maxInfects);
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

    private List<Tuple<Coordinaat, Coordinaat>> MogelijkeSprongStukken(Bord bord)
    {
        var alleMogelijkSprongStukken = new List<Tuple<Coordinaat, Coordinaat>>();
        foreach (var stuk in bord.JouwStukken(character))
            foreach (var kopieStuk in bord.AangrenzendeStukken(stuk))
                foreach (var sprongStuk in bord.AangrenzendeStukken(kopieStuk))
                    if (bord.isValideSprong(stuk, sprongStuk, character) && sprongStuk != kopieStuk && bord.BordLijst[sprongStuk.rij, sprongStuk.column] == null && !alleMogelijkSprongStukken.Contains(Tuple.Create(stuk, sprongStuk)) && !bord.AangrenzendeStukken(stuk).Contains(sprongStuk))
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