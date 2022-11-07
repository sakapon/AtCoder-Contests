using System;

class C
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		// 大きい値のインデックス
		var i = n - 2;
		while (p[i] < p[i + 1]) i--;

		// p[i] > p[j] を満たす最大の j
		var j = i + 1;
		while (j + 1 < n && p[i] > p[j + 1]) j++;

		(p[i], p[j]) = (p[j], p[i]);
		Array.Reverse(p, i + 1, n - i - 1);

		return string.Join(" ", p);
	}
}
