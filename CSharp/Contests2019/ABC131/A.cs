using System;
using System.Linq;

class A
{
	static void Main()
	{
		var s = Console.ReadLine();
		Console.WriteLine(Enumerable.Range(0, 3).Any(i => s[i] == s[i + 1]) ? "Bad" : "Good");
	}
}
