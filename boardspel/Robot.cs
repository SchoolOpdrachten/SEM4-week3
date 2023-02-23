
public class Robot : Speler
{

    public Robot(string character) : base("ROBOT", character) { }

    internal void RobotMove(Bord bord)
    {
        var mijnStukken = bord.JouwStukken(character);
        var alleMogelijkKopieStukken = new List<Coordinaat>();
        foreach (var stuk in mijnStukken) {
            foreach( var kopieStuk in bord.AangrenzendeStukken(stuk)) {
                if (bord.isValidePlek(kopieStuk, character) && kopieStuk != stuk && !alleMogelijkKopieStukken.Contains(kopieStuk)) {
                    alleMogelijkKopieStukken.Add(kopieStuk);
                }
            }
        }
        if (alleMogelijkKopieStukken.Count == 0) return;
        var random = new Random();
        int randomNummer = random.Next(0, alleMogelijkKopieStukken.Count-1);
        this.kopieerZet(bord, alleMogelijkKopieStukken[randomNummer]);
    }

}