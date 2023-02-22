public class Coordinaat
{
    public int rij { get; set; }
    public int column { get; set; }

    public Coordinaat(int rij, int column)
    {
        this.rij = rij;
        this.column = column;
    }

    public override bool Equals(object? obj)
    {
        return obj is Coordinaat coordinaat &&
               rij == coordinaat.rij &&
               column == coordinaat.column;
    }
}