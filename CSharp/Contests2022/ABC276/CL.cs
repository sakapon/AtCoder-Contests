using System;

class CL
{
	static int[] Read() => Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
	static void Main() => Console.WriteLine(Solve());
	static object Solve()
	{
		var n = int.Parse(Console.ReadLine());
		var p = Read();

		PrevPermutation(p);
		return string.Join(" ", p);
	}

	public static bool PrevPermutation(int[] p)
	{
		var n = p.Length;

		// p[i] > p[i + 1] を満たす最大の i
		var i = n - 2;
		while (i >= 0 && p[i] <= p[i + 1]) i--;
		if (i == -1) return false;

		// p[i] > p[j] を満たす最大の j
		var j = i + 1;
		while (j + 1 < n && p[i] > p[j + 1]) j++;

		(p[i], p[j]) = (p[j], p[i]);
		Array.Reverse(p, i + 1, n - i - 1);
		return true;
	}
}
