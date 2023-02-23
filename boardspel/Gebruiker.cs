public class Gebruiker : Speler
{
    public Gebruiker(string naam, string character) : base(naam, character) { }

    public static Coordinaat vraagCoordinaat()
    {
        Console.Write("Rij: ");
        int y = int.Parse(Console.ReadLine());
        Console.Write("Column: ");
        int x = int.Parse(Console.ReadLine());
        Coordinaat coordinaat = new Coordinaat(y, x);
        return coordinaat;
    }
}