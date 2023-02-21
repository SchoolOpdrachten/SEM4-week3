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

    public void maakMove(Bord bord) {
        Console.WriteLine("sup");
        int x = int.Parse(Console.ReadLine());
        int y = int.Parse(Console.ReadLine());

        Console.WriteLine("sup1");
        bord.PlaatsInBord(x, y, character);
    }
}