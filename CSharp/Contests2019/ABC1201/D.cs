using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var d = Console.ReadLine().Select((c, i) => new { c, i }).GroupBy(_ => _.c).Select(g => g.Select(_ => _.i).ToArray()).ToArray();

		int r = 0, j;
		foreach (var i1 in d.Select(x => x[0]))
			foreach (var i3 in d.Select(x => x.Last()).Where(i3 => i3 - i1 >= 2))
				foreach (var a in d)
				{
					if ((j = Array.BinarySearch(a, i1 + 1)) < 0) j = ~j;
					if (j < a.Length && a[j] < i3) r++;
				}
		Console.WriteLine(r);
	}
}
