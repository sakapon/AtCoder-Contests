using System;
using System.Linq;

class A
{
	static void Main()
	{
		var d = Console.ReadLine().GroupBy(c => c).ToDictionary(g => g.Key, g => g.ToArray());
		Console.WriteLine(d.Count == 2 && d.First().Value.Length == 2 ? "Yes" : "No");
	}
}
