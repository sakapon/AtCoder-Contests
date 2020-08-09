using System;
using System.Linq;

class A
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var a = new int[n].Select(_ => decimal.Parse(Console.ReadLine())).ToArray();

		var d = a.Select(x =>
		{
			var s = x.ToString().TrimEnd('0');
			var i = s.IndexOf('.');
			var p10 = i == -1 ? 0 : s.Length - 1 - i;
			var d2 = 1M;
			for (int k = 0; k < p10; k++) d2 *= 10;
			var d1 = x * d2;

			while (d1 % 2 == 0 && d2 % 2 == 0)
			{
				d1 /= 2;
				d2 /= 2;
			}
			while (d1 % 5 == 0 && d2 % 5 == 0)
			{
				d1 /= 5;
				d2 /= 5;
			}

			int p2 = 0, p5 = 0;
			while (d1 % 2 == 0)
			{
				d1 /= 2;
				p2++;
			}
			while (d1 % 5 == 0)
			{
				d1 /= 5;
				p5++;
			}
			while (d2 % 2 == 0)
			{
				d2 /= 2;
				p2--;
			}
			while (d2 % 5 == 0)
			{
				d2 /= 5;
				p5--;
			}
			return (p2, p5);
		}).GroupBy(x => x).ToDictionary(g => g.Key, g => g.LongCount());

		var r = 0L;
		foreach (var (x, c1) in d)
		{
			foreach (var (y, c2) in d)
			{
				if (x.p2 + y.p2 < 0 || x.p5 + y.p5 < 0) continue;
				r += c1 * c2;
				if (x == y) r -= c1;
			}
		}
		Console.WriteLine(r / 2);
	}
}
