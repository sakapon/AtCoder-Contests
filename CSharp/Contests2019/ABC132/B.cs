using System;
using System.Linq;

class B
{
	static void Main()
	{
		Console.ReadLine();
		var p = Console.ReadLine().Split().Select(int.Parse).ToArray();

		Console.WriteLine(Enumerable.Range(1, p.Length - 2).Count(i => new[] { p[i - 1], p[i], p[i + 1] }.OrderBy(x => x).ElementAt(1) == p[i]));
	}
}
