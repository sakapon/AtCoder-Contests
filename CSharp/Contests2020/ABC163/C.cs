using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = Console.ReadLine().Split().Select(int.Parse).ToLookup(x => x);
		Console.WriteLine(string.Join("\n", Enumerable.Range(1, n).Select(i => d[i].Count())));
	}
}
