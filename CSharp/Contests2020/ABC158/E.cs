using System;
using System.Linq;

class E
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static void Main()
	{
		var (n, p) = Read2();
		var s = Console.ReadLine().Select(c => c - '0').ToArray();

		if (p == 2 || p == 5)
		{
			Console.WriteLine(s.Select((x, i) => x % p == 0 ? i + 1 : 0L).Sum());
			return;
		}

		var ps = new int[n + 1];
		ps[n] = 0;
		for (int i = n - 1, p10 = 1; i >= 0; --i, p10 *= 10, p10 %= p)
			ps[i] = (s[i] * p10 + ps[i + 1]) % p;

		Console.WriteLine(ps.GroupBy(x => x).Select(g => g.LongCount()).Sum(x => x * (x - 1) / 2));
	}
}
