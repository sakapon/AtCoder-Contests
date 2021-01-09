using System;
using System.Linq;

class C2
{
	static void Main()
	{
		Console.ReadLine();
		var x = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var p = Math.Round(x.Average());
		Console.WriteLine(x.Sum(v => (v - p) * (v - p)));
	}
}
