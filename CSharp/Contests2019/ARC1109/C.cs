using System;
using System.Linq;

class C
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var n = int.Parse(Console.ReadLine());
		var a = read();
		var b = read();
		var ab = a.Zip(b, (x, y) => new { x, y }).OrderBy(_ => _.y).ThenBy(_ => _.x).ToArray();
		b = ab.Select(_ => _.y).ToArray();

		if (a.OrderBy(x => x).Where((x, i) => x > b[i]).Any()) { Console.WriteLine("No"); return; }

		int c = 0, k = 0;
		for (; c == 0 || k > 0; c++)
			if ((k = Array.BinarySearch(b, ab[k].x)) < 0) k = ~k;
		Console.WriteLine(c != n ? "Yes" : "No");
	}
}
