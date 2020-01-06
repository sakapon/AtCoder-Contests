using System;
using System.Linq;

class C
{
	static void Main()
	{
		var n = int.Parse(Console.ReadLine());
		var ps = new int[n].Select(_ => int.Parse(Console.ReadLine()))
			.Select(a => new int[a].Select(i => Console.ReadLine().Split().Select(int.Parse).ToArray()).ToArray())
			.ToArray();

		var p2 = new int[n + 1];
		p2[0] = 1;
		for (int i = 1; i <= n; i++) p2[i] = 2 * p2[i - 1];

		var M = 0;
		for (int f = 0; f < p2[n]; f++) M = Math.Max(M, Assert(f, ps, p2));
		Console.WriteLine(M);
	}

	static int Assert(int f, int[][][] ps, int[] p2)
	{
		var c = 0;
		for (int i = 0; i < ps.Length; i++)
		{
			if ((f & p2[i]) == 0) continue;
			c++;
			foreach (var xy in ps[i])
				if (((f & p2[xy[0] - 1]) > 0 ? 1 : 0) != xy[1]) return 0;
		}
		return c;
	}
}
