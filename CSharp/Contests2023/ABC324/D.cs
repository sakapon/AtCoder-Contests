using System;
using System.Linq;

class D
{
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var s = Console.ReadLine().ToCharArray();

		Array.Sort(s);

		var r = 0;
		for (long x = 0; x < 10_000_000; x++)
		{
			var x2 = x * x;
			var xs = x2.ToString().PadLeft(n, '0').ToCharArray();
			Array.Sort(xs);

			if (s.SequenceEqual(xs)) r++;
		}
		return r;
	}
}
