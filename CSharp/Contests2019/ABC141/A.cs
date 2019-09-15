using System;

class A
{
	static void Main()
	{
		var a = new[] { "Sunny", "Cloudy", "Rainy" };
		Console.WriteLine(a[(Array.IndexOf(a, Console.ReadLine()) + 1) % 3]);
	}
}
