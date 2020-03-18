using System;
using System.Linq;

class M
{
	static void Main()
	{
		Func<int[]> read = () => Console.ReadLine().Split().Select(int.Parse).ToArray();
		var ms = read().Select(x => new int[x].Select(_ => read()).ToArray()).ToArray();
		Console.WriteLine(Last(0, 100000, x => ms[0].Select(m => m[1] - x * m[0]).Concat(new[] { ms[1].Max(m => m[1] - x * m[0]) }).OrderBy(v => -v).Take(5).Sum() >= 0));
	}

	static double Last(double l, double r, Func<double, bool> f)
	{
		double m;
		while (Math.Round(r - l, 6) > 0) if (f(m = r - (r - l) / 2)) l = m; else r = m;
		return l;
	}
}
