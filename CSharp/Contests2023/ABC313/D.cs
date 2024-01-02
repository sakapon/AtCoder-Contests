using System;
using System.Linq;

class D
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static (int, int) Read2() { var a = Read(); return (a[0], a[1]); }
	static int Query(int[] x)
	{
		Console.WriteLine($"? {string.Join(" ", x)}");
		return int.Parse(Console.ReadLine());
	}
	static void Main() => Console.WriteLine($"! {string.Join(" ", Solve())}");
	static int[] Solve()
	{
		var (n, k) = Read2();

		var a = new int[n];

		for (int i = 0; i < k + 1; i++)
		{
			// i を除く k 個の和の偶奇
			a[(i + k) % (k + 1)] = Query(Enumerable.Range(i, k).Select(j2 => j2 % (k + 1) + 1).ToArray());
		}

		var s = a.Sum() % 2;

		for (int i = 0; i < k + 1; i++)
		{
			a[i] ^= s;
		}

		s ^= a[k];
		s ^= a[k - 1];
		var rk = Enumerable.Range(1, k).ToArray();

		for (int i = k + 1; i < n; i++)
		{
			rk[^1] = i + 1;
			a[i] = Query(rk) ^ s;
		}

		return a;
	}
}
