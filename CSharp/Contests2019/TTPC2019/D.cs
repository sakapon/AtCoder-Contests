using System;
using System.Collections.Generic;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var X = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var ps = Primes(X.Max());
		var d = new int[4]; d[0] = X.Length;
		foreach (var i in X.Select(x => x == 7 ? 3 : ps.Contains(x - 2) ? 2 : 1)) d[i]++;
		Console.WriteLine(Wins(d) ? "An" : "Ai");
	}

	static HashSet<int> Primes(int M)
	{
		var ps = new List<int>();
		for (var i = 2; i <= M; i++)
		{
			var ri = (int)Math.Sqrt(i);
			if (ps.TakeWhile(p => p <= ri).All(p => i % p != 0)) ps.Add(i);
		}
		return new HashSet<int>(ps);
	}

	static bool Wins(int[] d)
	{
		if (d[0] == 1) return true;
		if (d[0] == 2) return d[1] == 1 || d[2] == 1 || d[3] == 1;

		if (d[1] > 0)
		{
			var d2 = (int[])d.Clone();
			d2[0]--; d2[1]--;
			if (!Wins(d2)) return true;
		}
		if (d[2] > 0)
		{
			var d2 = (int[])d.Clone();
			d2[0]--; d2[2]--;
			if (!Wins(d2)) return true;
		}
		if (d[2] > 0)
		{
			var d2 = (int[])d.Clone();
			d2[1]++; d2[2]--;
			if (!Wins(d2)) return true;
		}
		if (d[3] > 0)
		{
			var d2 = (int[])d.Clone();
			d2[0]--; d2[3]--;
			if (!Wins(d2)) return true;
		}
		if (d[2] > 0)
		{
			var d2 = (int[])d.Clone();
			d2[1]++; d2[3]--;
			if (!Wins(d2)) return true;
		}
		if (d[2] > 0)
		{
			var d2 = (int[])d.Clone();
			d2[2]++; d2[3]--;
			if (!Wins(d2)) return true;
		}
		return false;
	}
}
