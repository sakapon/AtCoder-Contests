using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var x = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(1, 100).Min(p => x.Sum(v => (v - p) * (v - p))));
	}
}
