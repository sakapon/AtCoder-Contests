using System;
using System.Linq;

class C
{
	static void Main()
	{
		Console.ReadLine();
		var a = Console.ReadLine().Split().Select(int.Parse).ToArray();
		Console.WriteLine(Enumerable.Range(1, 100).Min(p => a.Sum(x => (x - p) * (x - p))));
	}
}
