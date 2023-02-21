Bord bord = new Bord(7);
Console.WriteLine("Welkom bij het spel");
var speler1naam = Console.ReadLine();
Speler speler1 = new Speler(speler1naam, "H");
var speler2naam = Console.ReadLine();
Speler speler2 = new Speler(speler2naam, "B");

bord.PlaatsInBord(6, 3, "H");

while(true) {
    bord.PrintBord();

    speler1.maakMove(bord);
    speler2.maakMove(bord);

}