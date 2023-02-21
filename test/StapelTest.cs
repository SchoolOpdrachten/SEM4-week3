namespace test;

public class StapelTest
{
    [Fact]
    public void IntNaarStapel()
    {
        var stapel = new Stapel<int>();
        stapel.duw(1);
        Assert.Equal(1, stapel.bovenste.waarde);
    }

    [Fact]
    public void StringNaarStapel()
    {
        var stapel = new Stapel<string>();
        stapel.duw("hallo wereld");
        Assert.Equal("hallo wereld", stapel.bovenste.waarde);
    }

    [Fact]
    public void VolgordeStapel()
    {
        var stapel = new Stapel<int>();
        stapel.duw(1);
        stapel.duw(2);
        Assert.Equal(2, stapel.pak());
        Assert.Equal(1, stapel.pak());
    }

    [Fact]
    public void GeenBeginItem()
    {
        var stapel = new Stapel<int>();
        Assert.Null(stapel.bovenste);
    }

    [Fact]
    public void PakExeption()
    {
        var stapel = new Stapel<string>();
        Assert.Throws<Exception>(() => stapel.pak());
    }
}