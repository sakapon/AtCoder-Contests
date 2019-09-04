using System;
using System.Linq;

class D
{
	static void Main()
	{
		Console.ReadLine();
		var X = Console.ReadLine().Split().Select(int.Parse).ToArray();
		var ps = PrimeFlags(X.Max());
		var d = new int[3];
		foreach (var i in X.Select(x => x == 7 ? 3 : ps[x - 2] ? 2 : 1)) d[i - 1]++;
		Console.WriteLine(d.Select(x => x % 2).GroupBy(x => x).Count() > 1 ? "An" : "Ai");
	}

	static bool[] PrimeFlags(int M)
	{
		var rM = (int)Math.Sqrt(M);
		var b = new bool[M + 1]; b[2] = true;
		for (int i = 3; i <= M; i += 2) b[i] = true;
		for (int p = 3; p <= rM; p++) if (b[p]) for (var i = 3 * p; i <= M; i += 2 * p) b[i] = false;
		return b;
	}
}
