using System;
using System.Collections.Generic;
using System.Linq;

class E2
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var a = Read();

		var pf = new PrimeFactorization(1000000);
		var u = new bool[1000000 + 1];

		if (IsPairwiseCoprime()) return "pairwise coprime";
		if (a.Aggregate(Gcd) == 1) return "setwise coprime";
		return "not coprime";

		bool IsPairwiseCoprime()
		{
			foreach (var x in a)
			{
				foreach (var f in pf.GetFactorTypes(x))
				{
					if (u[f]) return false;
					u[f] = true;
				}
			}
			return true;
		}
	}

	static int Gcd(int a, int b) { for (int r; (r = a % b) > 0; a = b, b = r) ; return b; }
}

public class PrimeFactorization
{
	int nMax;
	int[] minPs;

	public PrimeFactorization(int nMax)
	{
		this.nMax = nMax;

		// nMax 以下のすべての数に対する、最小の素因数 O(nMax)?
		minPs = new int[nMax + 1];
		for (int p = 2; p <= nMax; ++p)
			if (minPs[p] == 0)
				for (int x = p; x <= nMax; x += p)
					if (minPs[x] == 0)
						minPs[x] = p;
	}

	// 素因数分解
	public int[] GetFactors(int n)
	{
		var r = new List<int>();

		while (n > 1)
		{
			r.Add(minPs[n]);
			n /= minPs[n];
		}
		return r.ToArray();
	}

	// 素因数分解 (重複なし)
	public int[] GetFactorTypes(int n)
	{
		var r = new List<int>();

		var f = 1;
		while (n > 1)
		{
			if (f != minPs[n]) r.Add(f = minPs[n]);
			n /= minPs[n];
		}
		return r.ToArray();
	}
}
