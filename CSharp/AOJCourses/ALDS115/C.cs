using System;
using System.Linq;

class C
{
	static void Main()
	{
		var e = 0;
		Console.WriteLine(new int[int.Parse(Console.ReadLine())].Select(_ => Console.ReadLine().Split().Select(int.Parse).ToArray()).OrderBy(p => p[1]).Where(p => e < p[0]).Select(p => e = p[1]).Count());
	}
}
