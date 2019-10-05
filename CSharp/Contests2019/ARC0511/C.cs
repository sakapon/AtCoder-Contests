using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var s = new int[n].Select(_ => Console.ReadLine().Replace("AB", "!")).ToArray();

		var r = s.Sum(x => x.Count(c => c == '!'));
		var d = s.Select(x => (x[0] == 'B' ? 1 : 0) | (x.Last() == 'A' ? 2 : 0)).GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
		for (int i = 0; i < 4; i++) if (!d.ContainsKey(i)) d[i] = 0;
		if (d[3] > 0)
		{
			r += d[3] - 1;
			if (d[1] > 0) { r++; d[1]--; }
			if (d[2] > 0) { r++; d[2]--; }
		}
		r += Math.Min(d[1], d[2]);
		Console.WriteLine(r);
	}
}
