public class Stapel<T>
{
    public Item? bovenste = null;
    public Item duw(T waarde)
    {
        Item item = new Item { waarde = waarde, vorige = bovenste };
        bovenste = item;
        return item;
    }
    public T? pak()
    {
        if (bovenste == null)
            return default(T);
        Item pak = bovenste;
        bovenste = bovenste.vorige;
        return pak.waarde;
    }

    public class Item
    {
        public T? waarde;
        public Item? vorige;
    }
}