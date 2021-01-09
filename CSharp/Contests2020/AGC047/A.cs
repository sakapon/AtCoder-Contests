using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var d = new int[n].Select(_ => Factorize(Console.ReadLine())).GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());

		(int p2, int p5) Factorize(string s)
		{
			var i = s.IndexOf('.');
			var d1 = long.Parse(s.Replace(".", ""));
			var d2 = i == -1 ? 1 : long.Parse($"1{new string('0', s.Length - 1 - i)}");

			int p2 = 0, p5 = 0;
			while (d1 % 2 == 0)
			{
				d1 /= 2; p2++;
			}
			while (d1 % 5 == 0)
			{
				d1 /= 5; p5++;
			}
			while (d2 % 2 == 0)
			{
				d2 /= 2; p2--;
			}
			while (d2 % 5 == 0)
			{
				d2 /= 5; p5--;
			}
			return (p2, p5);
		}

		var r = 0L;
		foreach (var (x, cx) in d)
			foreach (var (y, cy) in d)
			{
				if (x.p2 + y.p2 < 0 || x.p5 + y.p5 < 0) continue;
				r += cx * cy;
				if (x == y) r -= cx;
			}
		Console.WriteLine(r / 2);
	}
}
