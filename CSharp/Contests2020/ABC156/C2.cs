using System;
using System.Linq;

class C2
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var p = Math.Round(a.Average());
		Console.WriteLine(a.Sum(x => (x - p) * (x - p)));
	}
}
