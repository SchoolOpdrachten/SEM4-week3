Stapel<int> stapel = new Stapel<int>();
stapel.duw(1);
stapel.duw(2);
stapel.duw(3);

Console.WriteLine(stapel.pak());
Console.WriteLine(stapel.pak());

stapel.duw(4);

Console.WriteLine(stapel.pak());
Console.WriteLine(stapel.pak());

Stapel<string> stapel1 = new Stapel<string>();
stapel1.duw("hallo wereld");
stapel1.duw("hallo wereld2");

Console.WriteLine(stapel1.pak());