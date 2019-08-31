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
		var d = new int[4];
		foreach (var i in X.Select(x => x == 7 ? 3 : ps.Contains(x - 2) ? 2 : 1)) d[i]++;
		Console.WriteLine(d.Skip(1).Select(x => x % 2).GroupBy(x => x).Count() > 1 ? "An" : "Ai");
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
}
