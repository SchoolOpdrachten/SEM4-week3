using System.Collections;

Console.Clear();
Bord bord = new Bord();
Console.WriteLine("Welkom bij het spel");
List<Speler> spelers = new List<Speler>();
foreach (var i in new string[] { "H", "B" })
{
    Console.WriteLine($"Speler {i} naam: ");
    var spelerNaam = Console.ReadLine();
    if (spelerNaam.ToLower().Split(" ")[0] == "robot")
        spelers.Add(new Robot(spelerNaam.ToLower().Split(" ")[1], i));
    else spelers.Add(new Speler(spelerNaam, i));
}
Console.Clear();

bool spelKlaar = false;
int HWin = 0;
int BWin = 0;
int gelijkspel = 0;
for (int i = 0; i < 100; i++)
{
    spelKlaar = false;
    while (!spelKlaar)
    {
        foreach (var speler in spelers)
        {
            Console.Clear();
            spelKlaar = bord.spelKlaar(speler.character);
            if (spelKlaar) break;
            if (speler is Robot)
            {
                var robot = (Robot)speler;
                robot.RobotMove(bord);
                bord.PrintBord();
                Thread.Sleep(100);
                continue;
            }

            // speler is aan de beurt
            bord.PrintBord();
            Console.WriteLine($"speler {speler.character}: {speler.Naam}");
            bool zetGelukt = false;
            while (!zetGelukt)
            {
                Console.WriteLine($"[V]erplaats of [K]opieer je kledingstuk {speler.character} of neem een [S]tap [T]erug");
                string keuze = Console.ReadLine();
                if (keuze.ToLower() == "t" || keuze.ToLower() == "s" || keuze.ToLower() == "terug")
                {
                    bord.StapTerug();
                    // Console.Clear();
                    bord.PrintBord();
                }
                if (keuze.ToLower() == "v" || keuze.ToLower() == "verplaats")
                {
                    System.Console.WriteLine("Van:");
                    var van = Gebruiker.vraagCoordinaat();
                    System.Console.WriteLine("Naar:");
                    var naar = Gebruiker.vraagCoordinaat();
                    zetGelukt = speler.zetStap(bord, van, naar);
                }
                if (keuze.ToLower() == "k" || keuze.ToLower() == "kopieer")
                {
                    var coordinaat = Gebruiker.vraagCoordinaat();
                    zetGelukt = speler.kopieerZet(bord, coordinaat);
                }
                Console.WriteLine(zetGelukt ? "" : "ongeldig");
            }
            Console.Clear();
            bord.PrintBord();
            Thread.Sleep(1000);
        }
    }
    bord.PrintBord();
    string winnaar = bord.getWinnaar();
    if (winnaar == "H") HWin++;
    if (winnaar == "B") BWin++;
    if (winnaar == "gelijkspel") gelijkspel++;
    Console.WriteLine($"GG {winnaar}");
    // Console.ReadKey();
    bord.BordLijst = bord.initBord();
}
Console.WriteLine($"H: {HWin}, B: {BWin}, Gelijkspel: {gelijkspel}");